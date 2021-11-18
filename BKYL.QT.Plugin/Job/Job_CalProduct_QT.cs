using BFES.DataAccess;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using BKYL.QT.Plugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Job
{
    /// <summary>
    ///路绳鑫：质量评价-15分钟累计产量及1小时累计产量
    /// </summary>
    public class Job_CalProduct_QT : BaseJob
    {
        #region 采集数据表
        List<R_PRODUCT_PLC_01M> rProductList = null;
        #endregion
        public override void InitTask()
        {
        }
        public override void RunTask(DateTime currentTime)
        {
            try
            {
                DateTime S_Max_Date;
                DateTime T_Max_Date;
                double opSum = 0;
                TC_SHIFT_NUMBER shiftNub = null;
                //获取原表及目标表的最大时间
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    string sql_Smax = string.Format("select max({0}) from {1} where 1=1 ", tableConfig.S_TableSequential, tableConfig.S_TableName);
                    S_Max_Date = iDataBase.GetDateTime(sql_Smax);
                    string sql_Tmax = string.Format("select max({0}) from {1} where 1=1 ", tableConfig.T_TableSequential, tableConfig.T_TableName);
                    T_Max_Date = iDataBase.GetDateTime(sql_Tmax);
                    if (T_Max_Date == DateTime.MinValue)
                    {
                        string sql_Smin = string.Format("select min({0}) from {1} where 1=1 ", tableConfig.S_TableSequential, tableConfig.S_TableName);
                        T_Max_Date = iDataBase.GetDateTime(sql_Smin);
                    }

                    while (true)
                    {
                        if (T_Max_Date >= S_Max_Date)
                        {
                            break;
                        }
                        else
                        {
                            DateTime startTime = T_Max_Date;
                            DateTime endTime = T_Max_Date;

                            int mins = 0;
                            if (Task_Fre == 3600)
                            {
                                mins = 60 - startTime.Minute;
                                endTime = startTime.AddMinutes(mins);
                            }
                            else if (Task_Fre == 1800)
                            {
                                mins = Math.Abs(30 - startTime.Minute);
                                if (mins != 0 && mins != 30)
                                {
                                    endTime = startTime.AddMinutes(mins);
                                }
                                else
                                {
                                    endTime = startTime.AddSeconds(Task_Fre);
                                }
                            }
                            else if (Task_Fre == 900)
                            {
                                if (startTime.Minute == 0 || startTime.Minute == 15 || startTime.Minute == 30 || startTime.Minute == 45)
                                {
                                    endTime = startTime.AddSeconds(Task_Fre);
                                }
                                else
                                {
                                    endTime = startTime.AddMinutes(15 - startTime.Minute % 15);
                                }
                            } 
                            //获取当前时间段的数据并求和：rProductList
                            opSum = GetCurrentTSpanOP(startTime, endTime, iDataBase);
                            //感知当前时间在白天还是晚上，并在“ TC_SHIFT_NUMBER”表中输出当班班次
                            shiftNub = GetCurrentClasses(endTime, iDataBase);
                            if (shiftNub != null)
                            {
                                //组合数据，插入               
                                string values = string.Format("{0},{1},'{2}','{3}'", DataBaseFactory.ConvertTimeString(endTime, DataBaseType.Oracle), opSum, shiftNub.TC_SHIT_NUMBER, shiftNub.TC_SHIT_NAME);
                                string insertSql = string.Format("insert into {0} values({1})", tableConfig.T_TableName, values);
                                iDataBase.ExecuteCommand(insertSql);
                            }  
                            T_Max_Date = endTime;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("查询插入Error:" + ex.Message + ex.StackTrace);
            }
        }
        /// <summary>
        /// 获取当前时间点的班次
        /// </summary>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        private TC_SHIFT_NUMBER GetCurrentClasses(DateTime currentTime, IDataBase iDataBase)
        {
            string sqlStr = string.Format("select * from TC_SHIFT_NUMBER where timestamp = (select Min(timestamp) from TC_SHIFT_NUMBER where timestamp >= {0})", DataBaseFactory.ConvertTimeString(currentTime, DataBaseType.Oracle));
            try
            {
                return iDataBase.GetSingle<TC_SHIFT_NUMBER>(sqlStr);
            }
            catch (Exception ex)
            {
                throw new Exception("获取时间Error:" + ex.Message + ex.StackTrace);
            }
        }
        /// <summary>
        /// 获取当前时间段的总产量 
        /// </summary>
        /// <returns></returns>
        private double GetCurrentTSpanOP(DateTime startTime, DateTime endTime, IDataBase iDataBase)
        {
            double opSum;
            string sqlStr = string.Format("select * from {0} where timestamp>{1} and timestamp <={2} ", tableConfig.S_TableName, DataBaseFactory.ConvertTimeString(startTime, DataBaseType.Oracle), DataBaseFactory.ConvertTimeString(endTime, DataBaseType.Oracle));
            try
            {
                rProductList = iDataBase.GetList<R_PRODUCT_PLC_01M>(sqlStr);
                opSum = (double)rProductList.Select(p => p.A_W_C4_ACC).ToArray().Sum();
                return opSum;
            }
            catch (Exception ex)
            {
                throw new Exception("获取产量Error:" + ex.Message + ex.StackTrace);
            }
            finally
            {
                rProductList?.Clear();
                rProductList = null;
            }
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }
    }
}
