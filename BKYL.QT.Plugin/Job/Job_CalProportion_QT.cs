using BFES.DataAccess;
using BFES.Utils;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using BKYL.QT.Plugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BKYL.QT.Plugin.Job
{
    /// <summary>
    /// 阴世伟：原料优选和自动评价开发--12小时占比计算过程
    /// </summary>
    public class Job_CalProportion_QT : BaseJob
    {
        DateTime startTime;
        DateTime endTime;
        //消耗占比表
        List<TC_POPCAL_RESULT_CONSUME> tPopcalResultList = null;
        //排班表
        TC_SHIFT_NUMBER tcNumber = null;
        public override void InitTask()
        {
        }
        public override void RunTask(DateTime currentTime)
        {
            #region 早晚8点，计算占比
            //直接判断早晚8点，因为间隔执行时1小时,程序启动为整八点
            if ((currentTime.Hour == 8) || (currentTime.Hour == 20))
            {
                endTime = currentTime;
                startTime = endTime.AddHours(-12);
                try
                {
                    using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                    {
                        //计算出1 - 199各种料的各自总和，铁精粉总和是在代码中加出来的
                        tPopcalResultList = GetPopcalResultList(iDataBase);
                        if (tPopcalResultList.Count != 0)
                        {
                            //计算铁粉和添加剂占比
                            Cal_RESULT(iDataBase, currentTime);
                            //直接把结果类写入到数据表中
                            InsertResult(iDataBase);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("插入Error:" + ex.Message + ex.StackTrace);
                }
                finally
                {
                    if (tPopcalResultList != null)
                    {
                        tPopcalResultList.Clear();
                        tPopcalResultList = null;
                    }
                }
            }
            #endregion
        }
        /// <summary>
        /// 读取原料分钟消耗量表
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <returns></returns>
        private List<TC_POPCAL_RESULT_CONSUME> GetPopcalResultList(IDataBase iDataBase)
        {
            string sqlStr = string.Format("select t.DT_CODE, t.DT_VALUE from(select tc_code as DT_CODE, sum(tc_value) as DT_VALUE from {0} where TIMESTAMP > {1} and TIMESTAMP <= {2} and tc_code <> 100 group by tc_code order by tc_code) t where t.DT_VALUE is not null", tableConfig.S_TableName, DataBaseFactory.ConvertTimeString(startTime, DataBaseType.Oracle), DataBaseFactory.ConvertTimeString(endTime, DataBaseType.Oracle));
            return iDataBase.GetList<TC_POPCAL_RESULT_CONSUME>(sqlStr);
        }
        /// <summary>
        ///  插入占比数据
        /// </summary>
        /// <param name="iDataBase"></param>
        private void InsertResult(IDataBase iDataBase)
        {
            try
            {
                iDataBase.BeginTran();
                string insertSql = string.Format("insert into {0} (TIMESTAMP,DT_CODE,DT_VALUE,DT_RATIO,DT_CLASS_TYPE) values (:TIMESTAMP,:DT_CODE,:DT_VALUE,:DT_RATIO,:DT_CLASS_TYPE )", tableConfig.T_TableName);
                BFES.DataAccess.SqlMapper.Execute(iDataBase.GetConnection(), insertSql, tPopcalResultList);
                iDataBase.CommitTran();
            }
            catch (Exception ex)
            {
                iDataBase.RollbackTran();
                throw new Exception("插入Error:" + ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 求铁精粉总和，求各种精铁粉、添加剂的占比
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="currentTime"></param>
        private void Cal_RESULT(IDataBase iDataBase, DateTime currentTime)
        {
            double CodeAllSum = 0.0;
            //求精铁矿粉总和
            for (int i = 0; i < tPopcalResultList.Count; i++)
            {
                int code = tPopcalResultList[i].DT_CODE;
                if (99 >= code && code > 0)
                {
                    CodeAllSum += (double)tPopcalResultList[i].DT_VALUE;
                }
            }
            //各种精铁矿粉与总铁矿粉的占比
            for (int i = 0; i < tPopcalResultList.Count; i++)
            {
                tPopcalResultList[i].DT_RATIO = CodeAllSum == 0.0 ? 0 : Math.Round(((double)tPopcalResultList[i].DT_VALUE / CodeAllSum) * 100, 2);
            }
            foreach (var a in tPopcalResultList)
            {
                //返回该班次（白 / 夜）
                a.DT_CLASS_TYPE = GetCurrentClasses(currentTime, iDataBase);
                a.TIMESTAMP = currentTime;
            }
        }
        /// <summary>
        /// 获取当前时间点的班次
        /// </summary>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        private string GetCurrentClasses(DateTime currentTime, IDataBase iDataBase)
        {
            string clssName = null;
            string sqlStr = string.Format("select * from TC_SHIFT_NUMBER where timestamp=(select min(timestamp) from( select * from TC_SHIFT_NUMBER where timestamp > {0})calcat)", DataBaseFactory.ConvertTimeString(currentTime, DataBaseType.Oracle));
            try
            {
                tcNumber = iDataBase.GetSingle<TC_SHIFT_NUMBER>(sqlStr);
                if (tcNumber != null)
                {
                    return tcNumber.TC_SHIT_NAME;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("获取班次Error:" + ex.Message + ex.StackTrace);
            }
            finally
            {
                tcNumber = null;
            }
            return clssName;
        }
    }
}
