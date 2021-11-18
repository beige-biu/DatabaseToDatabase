using BFES.DataAccess;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using BKYL.QT.Plugin.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Job
{
    /// <summary>
    /// 阴世伟：原料优选和自动评价开发--1分钟原料质量评价结果、1分钟不同原料搭配及球团矿质量关联
    /// </summary>
    public class Job_CalQualityRelations_QT : BaseJob
    {
        // 数据接收表
        List<L2_MATERIAL_ANA> TC_MaterialAna_List = null;
        //采集集合，最新俩条数据
        List<R_MATERIAL_PLC_01M> material_01m_List = null;
        L3_MATERIAL TC_MaterialSingle = null;
        TC_CHANGE_RESULT tc_Change_Result = null;
        public override void InitTask()
        {
        }

        public override void RunTask(DateTime currentTime)
        {
            // 计算铁矿粉综合价值和指数
            CalQuality();
            //判断是否换料并记录   
            //IsChangedAndLog(currentTime);
        }
        /// <summary>
        /// 计算铁矿粉综合价值和指数
        /// </summary>
        private void CalQuality()
        {
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    //读取原料成分表  
                    TC_MaterialAna_List = GetMaterialList(iDataBase);
                    if (TC_MaterialAna_List.Count != 0)
                    {
                        //计算铁粉价值和指数并写入数据库
                        Cal_ValueAndExponent(iDataBase);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("插入Error:" + ex.Message + ex.StackTrace);
            }
            finally
            {
                if (TC_MaterialAna_List != null)
                {
                    TC_MaterialAna_List.Clear();
                    TC_MaterialAna_List = null;
                }
            }
        }
        /// <summary>
        /// 判断是否换料并记录
        /// </summary>
        private void IsChangedAndLog(DateTime currentTime)
        {
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    //最新俩行数据
                    material_01m_List = GetMaterialPlc_01_List(iDataBase);
                    if (material_01m_List != null && material_01m_List.Count == 2)
                    {
                        //数据是否是错误值-9999
                        if (!JudgeIsErrorValue())
                        {
                            //俩条数据时间是否相同   
                            if (material_01m_List[0].TIMESTAMP != material_01m_List[1].TIMESTAMP)
                            {
                                string OldName = "";
                                string NewName = "";
                                //料种是否变化
                                if (JudgeIsMaterialValue(material_01m_List[0], material_01m_List[1]))
                                {
                                    //总量是否变化
                                    if (!JudgeIsChargeSum(material_01m_List[0], material_01m_List[1], iDataBase))
                                    {
                                        //总和不相等，换料，生成配方
                                        Charged(material_01m_List[0], material_01m_List[1], iDataBase, ref NewName, ref OldName);
                                        //插入改变后数据
                                        InsertChangeResult(iDataBase, currentTime, OldName, NewName, true);
                                    }
                                }
                                else
                                {
                                    //原料增减变化仓位，则原料换料，生成配方
                                    Charged(material_01m_List[0], material_01m_List[1], iDataBase, ref NewName, ref OldName);
                                    //插入改变后数据
                                    InsertChangeResult(iDataBase, currentTime, OldName, NewName, true);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("插入Error:" + ex.Message + ex.StackTrace);
            }
            finally
            {
                if (material_01m_List != null)
                {
                    material_01m_List.Clear();
                    material_01m_List = null;
                }
                TC_MaterialSingle = null;
                tc_Change_Result = null;
            }
        }

        /// <summary>
        /// 得到原料成分表中M_CAL_FLAG=0
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <returns></returns>
        private List<L2_MATERIAL_ANA> GetMaterialList(IDataBase iDataBase)
        {
            string sqlStr = string.Format("select * from L2_MATERIAL_ANA where M_CAL_FLAG=0");
            return iDataBase.GetList<L2_MATERIAL_ANA>(sqlStr);
        }
        /// <summary>
        /// 得到原料系统PLC表最新俩条
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <returns></returns>
        private List<R_MATERIAL_PLC_01M> GetMaterialPlc_01_List(IDataBase iDataBase)
        {
            string sqlStr = string.Format("select * from R_MATERIAL_PLC_01M where (timestamp = (select max(timestamp) from R_MATERIAL_PLC_01M) or timestamp = (select max(timestamp) from R_MATERIAL_PLC_01M) - interval '1' MINUTE) order by timestamp desc");
            return iDataBase.GetList<R_MATERIAL_PLC_01M>(sqlStr);
        }

        /// <summary>
        /// 插入变料后数据
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <param name="currentTime"></param>
        /// <param name="OldName"></param>
        /// <param name="NewName"></param>
        private void InsertChangeResult(IDataBase iDataBase, DateTime currentTime, string OldName, string NewName, bool mterialChange)
        {
            try
            {
                if (mterialChange)
                {
                    string sql = string.Format("insert into TC_CHANGE_RESULT (TIMESTAMP,M_PF_NAME,M_NAME,M_ESS_BER) values ({0},'{1}','{2}','{3}')", DataBaseFactory.ConvertTimeString(currentTime, DataBaseType.Oracle), OldName, NewName, 1);
                    iDataBase.ExecuteCommand(sql);
                }
                else
                {
                    string sql = string.Format("insert into TC_CHANGE_RESULT (TIMESTAMP,M_PF_NAME,M_NAME,M_ESS_BER) values ({0},'{1}','{2}','{3}')", DataBaseFactory.ConvertTimeString(currentTime, DataBaseType.Oracle), OldName, NewName, 0);
                    iDataBase.ExecuteCommand(sql);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("插入Error:" + ex.Message + ex.StackTrace);
            }
        }
        /// <summary>
        /// 计算铁矿粉综合价值和指数并写入数据库
        /// </summary>
        /// <param name="iDataBase"></param>
        private void Cal_ValueAndExponent(IDataBase iDataBase)
        {

            double Vt = 0.0;
            double Vp = 0.0;
            double PBO = 0.0;   //待转化传入该数值

            //计算铁矿粉综合价值和指数
            for (int i = 0; i < TC_MaterialAna_List.Count; i++)
            {
                Vt = TC_MaterialAna_List[i].M_TFE * (1 / (100 + (2 * (TC_MaterialAna_List[i].M_CAO / TC_MaterialAna_List[i].M_SIO2) * (TC_MaterialAna_List[i].M_SIO2 + TC_MaterialAna_List[i].M_AL2O3)) - (2 * (TC_MaterialAna_List[i].M_CAO + TC_MaterialAna_List[i].M_MGO)) + (1.5 * (TC_MaterialAna_List[i].M_S + TC_MaterialAna_List[i].M_P + 5 * TC_MaterialAna_List[i].M_K2O + TC_MaterialAna_List[i].M_NA2O + PBO /*+ ****PBO***** */+ TC_MaterialAna_List[i].M_ZNO + 0/*+ ***CuO*****/+ 0 /*+ ***As2O3*****/ + 5 * 0 /*+ ***CL*****/)) + (TC_MaterialAna_List[i].M_C1 * (TC_MaterialAna_List[i].M_LOI + TC_MaterialAna_List[i].M_C2 * TC_MaterialAna_List[i].M_LM))));
                Vp = Vt / TC_MaterialAna_List[i].M_PERVALUE;
                if (double.IsNaN((double)Vt))
                {
                    TC_MaterialAna_List[i].M_COST = 0;
                    TC_MaterialAna_List[i].M_INDEX = 0;
                }
                else
                {
                    TC_MaterialAna_List[i].M_COST = Vt;
                    TC_MaterialAna_List[i].M_INDEX = Vp;
                }
                try
                {
                    string sql = string.Format("update L2_MATERIAL_ANA SET M_COST={0},M_INDEX={1},M_CAL_FLAG=1 where TIMESTAMP={2}", TC_MaterialAna_List[i].M_COST, TC_MaterialAna_List[i].M_INDEX, DataBaseFactory.ConvertTimeString(TC_MaterialAna_List[i].TIMESTAMP, DataBaseType.Oracle));
                    iDataBase.ExecuteCommand(sql);
                }
                catch (Exception ex)
                {
                    throw new Exception("查询插入Error:" + ex.Message + ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// 判断是否变化
        /// </summary>
        /// <param name="iDataBase"></param>
        private bool JudgeIsErrorValue()
        {
            bool ret = false;
            for (int j = 0; j < 7; j++)
            {
                if ((double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[0], "A_SP_W_" + (j + 1)) == -9999)
                {
                    ret = true;
                    break;
                }
                if ((double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[1], "A_SP_W_" + (j + 1)) == -9999)
                {
                    ret = true;
                    break;
                }
            }
            if ((double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[0], "A_SP_W_" + 1) == 0.0 &&
                       (double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[0], "A_SP_W_" + 2) == 0.0 &&
                       (double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[0], "A_SP_W_" + 3) == 0.0 &&
                       (double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[0], "A_SP_W_" + 4) == 0.0 &&
                       (double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[0], "A_SP_W_" + 5) == 0.0 &&
                       (double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[0], "A_SP_W_" + 6) == 0.0 &&
                       (double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[0], "A_SP_W_" + 7) == 0.0)
            {
                ret = true;
            }
            if ((double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[1], "A_SP_W_" + 1) == 0.0 &&
                   (double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[1], "A_SP_W_" + 2) == 0.0 &&
                   (double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[1], "A_SP_W_" + 3) == 0.0 &&
                   (double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[1], "A_SP_W_" + 4) == 0.0 &&
                   (double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[1], "A_SP_W_" + 5) == 0.0 &&
                   (double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[1], "A_SP_W_" + 6) == 0.0 &&
                   (double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[1], "A_SP_W_" + 7) == 0.0)
            {
                ret = true;
            }
            return ret;
        }
        /// <summary>
        /// 判断料种是否增加减少
        /// </summary>
        /// <returns></returns>
        private bool JudgeIsMaterialValue(R_MATERIAL_PLC_01M NewClass, R_MATERIAL_PLC_01M OldClass)
        {
            bool ret = true;
            int[] newC = new int[7];
            int[] oldC = new int[7];
            for (int j = 0; j < 7; j++)
            {
                newC[j] = (int)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(NewClass, "A_CODE_" + (j + 1));
                oldC[j] = (int)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(OldClass, "A_CODE_" + (j + 1));
            }
            #region 数组去重排序
            SortedList lstNew = new SortedList();
            SortedList lstOld = new SortedList();
            foreach (int a in newC)
            {
                if (lstNew.Contains(a))
                    continue;
                lstNew.Add(a, a);
            }
            foreach (int a in oldC)
            {
                if (lstOld.Contains(a))
                    continue;
                lstOld.Add(a, a);
            }
            int[] newA = new int[lstNew.Count];
            for (int i = 0; i < lstNew.Count; i++)
            {
                newA[i] = (int)lstNew.GetByIndex(i);
            }
            int[] oldA = new int[lstOld.Count];
            for (int i = 0; i < lstOld.Count; i++)
            {
                oldA[i] = (int)lstOld.GetByIndex(i);
            }
            #endregion
            if (newA.Length == oldA.Length)
            {
                for (int i = 0; i < newA.Length; i++)
                {
                    if (newA.Length == 1 && oldA.Length == 1 && newA[0] == 0 && oldA[0] == 0)
                    {
                        throw new Exception("精铁粉料种号为0"); ;
                    }

                    if (newA[i] == oldA[i])
                    {
                        ret = true;
                    }
                    else
                    {
                        ret = false;
                        return ret;
                    }
                }
            }
            else
            {
                ret = false;
                return ret;
            }
            return ret;
        }
        /// <summary>
        /// 判断总量是否变化
        /// </summary>
        /// <returns></returns>
        private bool JudgeIsChargeSum(R_MATERIAL_PLC_01M NewClass, R_MATERIAL_PLC_01M OldClass, IDataBase iDataBase)
        {
            double sumNew = 0.0;
            //计算精铁矿设定下料总和                    
            sumNew = CalNewSum();
            double sumOld = 0.0;
            //计算精铁矿设定下料总和                    
            sumOld = CalOldSum();
            bool ret = false;
            double tempNew = 0.0;
            double tempOld = 0.0;
            #region 行转列  取单个对象字段成数组  
            List<MaterialCodeSp> materList = new List<MaterialCodeSp>();
            for (int j = 0; j < 7; j++)
            {
                MaterialCodeSp temp = new MaterialCodeSp();
                temp.newCode = (int)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(NewClass, "A_CODE_" + (j + 1));
                temp.newSP_W = (double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[0], "A_SP_W_" + (j + 1));
                temp.oldCode = (int)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(OldClass, "A_CODE_" + (j + 1));
                temp.oldSP_W = (double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[1], "A_SP_W_" + (j + 1));
                materList.Add(temp);
            }
            #endregion
            var tempList = materList.GroupBy(t => t.newCode).ToList();
            foreach (var item in tempList)
            {
                for (int i = 0; i < materList.Count; i++)
                {
                    if (materList[i].newCode == item.Key)
                    {
                        tempNew += materList[i].newSP_W;
                    }
                    if (materList[i].oldCode == item.Key)
                    {
                        tempOld += materList[i].oldSP_W;
                    }
                }
                if (sumNew == 0 && sumOld == 0)
                {
                    return true;
                }
                double newB = Math.Round(tempNew / sumNew * 100, 2);
                double oldB = Math.Round(tempOld / sumOld * 100, 2);
                if (newB == oldB)
                {
                    ret = true;
                    tempNew = 0.0;
                    tempOld = 0.0;
                }
                else
                {
                    ret = false;
                    break;
                }
            }
            return ret;
        }
        /// <summary>
        /// 原料变化后，生成配方
        /// </summary>
        /// <param name="NewClass">新的数据对象</param>
        /// <param name="OldClases">就得数据对象</param>
        /// <param name="iDataBase"></param>
        /// <param name="newStr">新配方</param>
        /// <param name="oldStr">旧配方</param>
        private void Charged(R_MATERIAL_PLC_01M NewClass, R_MATERIAL_PLC_01M OldClass, IDataBase iDataBase, ref string newStr, ref string oldStr)
        {
            double sum1 = 0.0;
            double temp1 = 0.0;
            //计算精铁矿设定下料总和                    
            sum1 = CalNewSum();
            #region 行转列  取单个对象字段成数组   
            List<MaterialCodeSp> materList = new List<MaterialCodeSp>();
            for (int j = 0; j < 7; j++)
            {
                MaterialCodeSp temp = new MaterialCodeSp();
                temp.newCode = (int)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(NewClass, "A_CODE_" + (j + 1));
                temp.newSP_W = (double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[0], "A_SP_W_" + (j + 1));
                temp.oldCode = (int)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(OldClass, "A_CODE_" + (j + 1));
                temp.oldSP_W = (double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[1], "A_SP_W_" + (j + 1));
                materList.Add(temp);
            }
            for (int j = 0; j < 6; j++)
            {
                MaterialCodeSp temp = new MaterialCodeSp();
                temp.newCode = (int)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(NewClass, "A_CODE_" + (j + 12));
                temp.newSP_W = (double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[0], "A_SP_W_" + (j + 12));
                temp.oldCode = (int)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(OldClass, "A_CODE_" + (j + 12));
                temp.oldSP_W = (double)BFES.Utils.CustomModelInfo.GetModelItemValue<R_MATERIAL_PLC_01M>(material_01m_List[1], "A_SP_W_" + (j + 12));
                materList.Add(temp);
            }
            var tempList = materList.GroupBy(t => t.newCode).ToList();
            foreach (var item in tempList)
            {
                //新配方   原料名称 
                string sql1 = string.Format("select * from L3_MATERIAL where M_L2_CODE = {0}", item.Key);
                TC_MaterialSingle = iDataBase.GetSingle<L3_MATERIAL>(sql1);

                if (TC_MaterialSingle != null)
                {
                    if (TC_MaterialSingle.M_MAT_DESC != "无物料")
                    {
                        //求个原料的和
                        for (int i = 0; i < materList.Count; i++)
                        {
                            if (item.Key == materList[i].newCode)
                            {
                                temp1 += materList[i].newSP_W;
                            }
                        }
                        newStr += TC_MaterialSingle.M_MAT_DESC + ":" + (sum1 == 0.0 ? 0 : Math.Round(temp1 / sum1 * 100, 2)) + "%" + "+";
                        temp1 = 0.0;
                    }
                }
            }
            string sql = string.Format("select * from TC_CHANGE_RESULT where timestamp = (select max(timestamp) from TC_CHANGE_RESULT)");
            tc_Change_Result = iDataBase.GetSingle<TC_CHANGE_RESULT>(sql);
            newStr += material_01m_List[0].TIMESTAMP;
            if (tc_Change_Result == null)
            {
                oldStr = newStr;
            }
            if (tc_Change_Result != null)
            {
                oldStr = tc_Change_Result.M_NAME;
            }
            #endregion
        }
        /// <summary>
        /// 计算精铁矿下料总和
        /// </summary>
        /// <returns></returns>
        private double CalNewSum()
        {
            double sum1 = 0.0;
            sum1 += (double)material_01m_List[0].A_SP_W_1;
            sum1 += (double)material_01m_List[0].A_SP_W_2;
            sum1 += (double)material_01m_List[0].A_SP_W_3;
            sum1 += (double)material_01m_List[0].A_SP_W_4;
            sum1 += (double)material_01m_List[0].A_SP_W_5;
            sum1 += (double)material_01m_List[0].A_SP_W_6;
            sum1 += (double)material_01m_List[0].A_SP_W_7;
            return sum1;
        }
        private double CalOldSum()
        {
            double sum1 = 0.0;
            sum1 += (double)material_01m_List[1].A_SP_W_1;
            sum1 += (double)material_01m_List[1].A_SP_W_2;
            sum1 += (double)material_01m_List[1].A_SP_W_3;
            sum1 += (double)material_01m_List[1].A_SP_W_4;
            sum1 += (double)material_01m_List[1].A_SP_W_5;
            sum1 += (double)material_01m_List[1].A_SP_W_6;
            sum1 += (double)material_01m_List[1].A_SP_W_7;
            return sum1;
        }
    }
}