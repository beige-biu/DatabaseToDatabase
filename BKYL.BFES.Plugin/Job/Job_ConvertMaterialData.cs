using BFES.DataAccess;
using BFES.Models;
using BKYL.BFES.Plugin.Models;
using BKYL.Jobs.Base.Utils;
using BKYL.Jobs.Base;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin
{
    class Job_ConvertMaterialData : BaseJob
    {
        string log = "";

        Dictionary<string, string> dicOre = new Dictionary<string, string>();
        Dictionary<double, string> dicOreAna = new Dictionary<double, string>();

        public override void InitTask()
        {
            #region LT_MATERIAL_ANALYSI
            dicOre.Add("FeO", "M_FEO_CONT");
            dicOre.Add("CaO", "M_CAO_CONT");
            dicOre.Add("Al2O3", "M_AL2O3_CONT");
            dicOre.Add("MgO", "M_MGO_CONT");
            dicOre.Add("SiO2", "M_SIO2_CONT");
            dicOre.Add("P", "M_P_CONT");
            dicOre.Add("Ro", "M_R");
            dicOre.Add("S", "M_S_CONT");
            dicOre.Add("TFe", "M_TFE_CONT");
            dicOre.Add("粒度+40", "M_SIZEABOVE40_CONT");
            dicOre.Add("粒度-5", "M_SINTER_SIZEUNDER5");
            dicOre.Add("粒度10-16", "M_SINTER_SIZE10_16");
            dicOre.Add("粒度16-25", "M_SINTER_SIZE16_25");
            dicOre.Add("粒度25-40", "M_SINTER_SIZE25_40");
            dicOre.Add("粒度5-10", "M_SINTER_SIZE5_10");
            dicOre.Add("筛分指数", "M_SHAIFEN");
            dicOre.Add("转鼓指数", "M_ZHUANGU_INDEX");
            dicOre.Add("抗压强度", "M_KANGYA");
            dicOre.Add("TiO2", "M_TIO2_CONT");
            dicOre.Add("RDI+3.15", "M_RDI_315");
            dicOre.Add("RDI+6.3", "M_RDI_63");
            dicOre.Add("RDI-0.5", "M_RDI_05");
            dicOre.Add("jyl", "M_DISCHARGEWEIGH");
            
            #endregion
        }
        public override void RunTask(DateTime currentTime)
        {
            #region 字段名
            string[] ColumnArry = { "TIMESTAMP",
                                    "M_CODE",
                                    "M_CODE3",
                                    "M_C_CONT",
                                    "M_TFE_CONT",
                                    "M_FE2O3_CONT",
                                    "M_FEO_CONT",
                                    "M_SIO2_CONT",
                                    "M_AL2O3_CONT",
                                    "M_CAO_CONT",
                                    "M_MGO_CONT",
                                    "M_NA2O_CONT",
                                    "M_K2O_CONT",
                                    "M_MN_CONT",
                                    "M_S_CONT",
                                    "M_P_CONT",
                                    "M_TIO2_CONT",
                                    "M_MOISTURE",
                                    "M_BASICITY",
                                    "M_ZN_CONT",
                                    "M_ASH",
                                    "M_CAF2_CONT",
                                    "M_VOLA_CONT",
                                    "M_H_CONT",
                                    "M_PART_SIZE",
                                    "M_MNO_CONT",
                                    "M_MNO2_CONT",
                                    "M_FES_CONT",
                                    "M_FES2_CONT",
                                    "M_V2O5_CONT",
                                    "M_CUO_CONT",
                                    "M_CU2O_CONT",
                                    "M_CO2_CONT",
                                    "M_SO2_CONT",
                                    "M_P2O5_CONT",
                                    "M_PB_CONT",
                                    "M_STD_CONT",
                                    "M_SIZEABOVE40_CONT",
                                    "M_SIZEUNDER10_CONT",
                                    "M_COKE_CRI",
                                    "M_COKE_CSR",
                                    "M_SIZEABOVE75_CONT",
                                    "M_SIZEABOVE25_CONT",
                                    "M_SIZEUNDER25_CONT",
                                    "M_SIZEABOVE100_CONT",
                                    "M_ZHUANGU_INDEX",
                                    "M_SINTER_SIZEABOVE10",
                                    "M_SINTER_SIZEABOVE5",
                                    "M_SINTER_SIZEUNDER5",
                                    "M_O2",
                                    "M_SIZEUNDER30_CONT",
                                    "M_SIZE30_40_CONT",
                                    "M_SIZE40_60_CONT",
                                    "M_SIZE60_80_CONT",
                                    "M_SIZEABOVE80_CONT",
                                    "M_SHAIFEN",
                                    "M_SINTER_SIZE5_10",
                                    "M_SINTER_SIZE10_16",
                                    "M_SINTER_SIZE16_25",
                                    "M_SINTER_SIZE25_40",
                                    "M_SINTER_SIZEABOVE40",
                                    "M_SULFUR",
                                    "M_CR2O3",
                                    "M_KANGYA",
                                    "M_KANGMO",
                                    "M_WATER_ABSORPTION_2H",
                                    "M_GRANULARITY_325",
                                    "M_EXPANSION_RATIO",
                                    "M_H2O",
                                    "M_MONTMORILLONITE",
                                    "M_SIZEUNDER5",
                                    "M_R",
                                    "M_COKE_M40",
                                    "M_COKE_M10",
                                    "M_RDI_315",
                                    "M_RDI_05",
                                    "M_RDI_63",
                                    "M_DISCHARGEWEIGH"
                                    };
            string[] ValueArry = { "TIMESTAMP",
                                    "M_CODE",
                                    "M_CODE3",
                                    "M_C_CONT",
                                    "M_TFE_CONT",
                                    "M_FE2O3_CONT",
                                    "M_FEO_CONT",
                                    "M_SIO2_CONT",
                                    "M_AL2O3_CONT",
                                    "M_CAO_CONT",
                                    "M_MGO_CONT",
                                    "M_NA2O_CONT",
                                    "M_K2O_CONT",
                                    "M_MN_CONT",
                                    "M_S_CONT",
                                    "M_P_CONT",
                                    "M_TIO2_CONT",
                                    "M_MOISTURE",
                                    "M_BASICITY",
                                    "M_ZN_CONT",
                                    "M_ASH",
                                    "M_CAF2_CONT",
                                    "M_VOLA_CONT",
                                    "M_H_CONT",
                                    "M_PART_SIZE",
                                    "M_MNO_CONT",
                                    "M_MNO2_CONT",
                                    "M_FES_CONT",
                                    "M_FES2_CONT",
                                    "M_V2O5_CONT",
                                    "M_CUO_CONT",
                                    "M_CU2O_CONT",
                                    "M_CO2_CONT",
                                    "M_SO2_CONT",
                                    "M_P2O5_CONT",
                                    "M_PB_CONT",
                                    "M_STD_CONT",
                                    "M_SIZEABOVE40_CONT",
                                    "M_SIZEUNDER10_CONT",
                                    "M_COKE_CRI",
                                    "M_COKE_CSR",
                                    "M_SIZEABOVE75_CONT",
                                    "M_SIZEABOVE25_CONT",
                                    "M_SIZEUNDER25_CONT",
                                    "M_SIZEABOVE100_CONT",
                                    "M_ZHUANGU_INDEX",
                                    "M_SINTER_SIZEABOVE10",
                                    "M_SINTER_SIZEABOVE5",
                                    "M_SINTER_SIZEUNDER5",
                                    "M_O2",
                                    "M_SIZEUNDER30_CONT",
                                    "M_SIZE30_40_CONT",
                                    "M_SIZE40_60_CONT",
                                    "M_SIZE60_80_CONT",
                                    "M_SIZEABOVE80_CONT",
                                    "M_SHAIFEN",
                                    "M_SINTER_SIZE5_10",
                                    "M_SINTER_SIZE10_16",
                                    "M_SINTER_SIZE16_25",
                                    "M_SINTER_SIZE25_40",
                                    "M_SINTER_SIZEABOVE40",
                                    "M_SULFUR",
                                    "M_CR2O3",
                                    "M_KANGYA",
                                    "M_KANGMO",
                                    "M_WATER_ABSORPTION_2H",
                                    "M_GRANULARITY_325",
                                    "M_EXPANSION_RATIO",
                                    "M_H2O",
                                    "M_MONTMORILLONITE",
                                    "M_SIZEUNDER5",
                                    "M_R",
                                    "M_COKE_M40",
                                    "M_COKE_M10",
                                    "M_RDI_315",
                                    "M_RDI_05",
                                    "M_RDI_63",
                                    "M_DISCHARGEWEIGH"
                                    };
            #endregion
            DateTime endTime = currentTime;
            DateTime startTime = currentTime.AddDays(-4);
            List<Models.AG.L3MATERIAL> L3material = GetL3Material();
            List<Models.AG.L3ANALYSI> ReusltList = new List<Models.AG.L3ANALYSI>();
            List<LT_MATERIAL_ANALYSI> dataList = new List<LT_MATERIAL_ANALYSI>();
            try
            {
                if (L3material != null && L3material.Count > 0)
                {
                    foreach (Models.AG.L3MATERIAL item in L3material)
                    {
                        List<LT_MATERIAL_ANALYSI> MATERIAL_ANALYSI = Get_MATERIAL_ANALYSI(item, startTime, endTime);
                        if (MATERIAL_ANALYSI.Count == 0) //如果本次没有查询到该时间范围内L3material中该料种的检化验原始数据
                        {
                            continue;
                        }
                        if (MATERIAL_ANALYSI != null && MATERIAL_ANALYSI.Count > 0)
                        {
                            List<Models.AG.L3ANALYSI> reusltList = CalL3ana(MATERIAL_ANALYSI, item);
                            if (reusltList != null && reusltList.Count > 0)
                            {
                                ReusltList.AddRange(reusltList);
                            }
                            reusltList?.Clear();
                            reusltList = null;
                            MATERIAL_ANALYSI.Clear();
                            MATERIAL_ANALYSI = null;
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                Log.WriteLine(ee.Message + "\n" + ee.StackTrace);
                throw new Exception(ee.Message + "\n" + ee.StackTrace);
            }
            finally
            {
                L3material?.Clear();
                L3material = null;
            }

            ReusltList = DealRepeatData(ReusltList);

            //插入数据
            int delete1 = 0;
            int delete2 = 0;
            if (ReusltList != null && ReusltList.Count > 0)
            {
                using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    try
                    {
                        idatabase.BeginTran();//由于检化验只包含矿石和熔剂化验数据，故删除时只删除矿石和熔剂的数据
                        delete1 = idatabase.ExecuteCommand(string.Format("delete from L3ANALYSI where timestamp>={0} and timestamp<={1} and M_CODE >=50 ", DataBaseFactory.ConvertTimeString(startTime, S_DBSource.DBType), DataBaseFactory.ConvertTimeString(endTime, S_DBSource.DBType)));
                        delete2 = idatabase.ExecuteCommand(string.Format("delete from L3ANALYSI where timestamp>={0} and timestamp<={1} and M_CODE >=20 and M_CODE <=29", DataBaseFactory.ConvertTimeString(startTime, S_DBSource.DBType), DataBaseFactory.ConvertTimeString(endTime, S_DBSource.DBType)));
                        log = string.Format("L3ANALYSI成功删除{0}条记录！", delete1 + delete2);
                        Outputlog(log);
                        idatabase.BulkInsert<Models.AG.L3ANALYSI>("L3ANALYSI", ColumnArry, ValueArry, ReusltList);
                        idatabase.CommitTran();
                        log = string.Format("L3ANALYSI成功插入{0}条记录！", ReusltList.Count);
                        Outputlog(log);
                    }
                    catch (Exception ee)
                    {
                        idatabase.RollbackTran();
                        Outputlog(log);
                        Log.WriteLine(ee.Message + "\n" + ee.StackTrace);
                        throw new Exception(ee.Message + "\n" + ee.StackTrace);
                    }
                    finally
                    {
                        ReusltList.Clear();
                        ReusltList = null;
                    }
                }
            }
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);

        }

        private List<Models.AG.L3MATERIAL> GetL3Material()
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
            {
                return iDataBase.Queryable<Models.AG.L3MATERIAL>().Where(m => m.M_TYPE != 2 && m.M_TYPE != 4 && m.M_TYPE != 5).ToList();//鞍钢检化验系统不包括焦炭煤粉的化验数据
            }
        }
        private List<LT_MATERIAL_ANALYSI> Get_MATERIAL_ANALYSI(Models.AG.L3MATERIAL model, DateTime startTime, DateTime endTime)
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
            {
                return iDataBase.Queryable<LT_MATERIAL_ANALYSI>().Where(m => m.SAMPLE_NAME == model.CODEL3 && m.SENDSAMPLE_TIME >= startTime && m.SENDSAMPLE_TIME <= endTime).ToList();
            }
        }
        private List<Models.AG.L3ANALYSI> CalL3ana(List<LT_MATERIAL_ANALYSI> dataList, Models.AG.L3MATERIAL model)
        {
            List<Models.AG.L3ANALYSI> reusltList = null;
            Models.AG.L3ANALYSI newmodel = null;
            if (dataList != null && dataList.Count > 0)
            {
                reusltList = new List<Models.AG.L3ANALYSI>();
                IEnumerable<IGrouping<String, LT_MATERIAL_ANALYSI>> AnalysiGroup = dataList.GroupBy(s => s.SAMPLE_MUNB);
                foreach (IGrouping<String, LT_MATERIAL_ANALYSI> item in AnalysiGroup)
                {
                    List<LT_MATERIAL_ANALYSI> dataSource = item.ToList();
                    if (dataSource.Count > 0)
                    {
                        newmodel = new Models.AG.L3ANALYSI() { M_CODE = model.CODE, M_CODE3 = model.CODEL3, TIMESTAMP = dataSource[0].SENDSAMPLE_TIME };
                        foreach (LT_MATERIAL_ANALYSI material in item)
                        {
                            Type t = typeof(Models.AG.L3ANALYSI);
                            Type O_t = typeof(LT_MATERIAL_ANALYSI);
                            for (int i = 1; i <= 20; i++)//循环判断各字段、值是否为空，对均不为空的进行赋值,因为为空ToString会报错
                            {
                                object Key = O_t.GetProperty($"HXYS{i}").GetValue(material, null);
                                if (Key != null && dicOre.ContainsKey(Key.ToString()))
                                {
                                    object Value = O_t.GetProperty($"HXCF{i}").GetValue(material, null);
                                    if (Value != null)
                                    {
                                        t.GetProperty(dicOre[Key.ToString()]).SetValue(newmodel, Value.ToDouble(), null);
                                    }
                                }
                            }
                        }
                        reusltList.Add(newmodel);
                    }
                }
            }
            return reusltList;
        }
        private List<Models.AG.L3ANALYSI> DealRepeatData(List<Models.AG.L3ANALYSI> ListModel)
        {
            List<Models.AG.L3ANALYSI> listtemp = new List<Models.AG.L3ANALYSI>();
            if (ListModel != null && ListModel.Count > 0)
            {
                ListModel = ListModel.OrderBy(x => x.TIMESTAMP).ToList();

                foreach (var item in ListModel)
                {
                    if (listtemp.Find(t => t.M_CODE == item.M_CODE && t.TIMESTAMP == item.TIMESTAMP) != null)
                    {
                        item.TIMESTAMP = listtemp.FindAll(x => x.M_CODE == item.M_CODE).Max(match => match.TIMESTAMP).AddSeconds(1);
                    }

                    listtemp.Add(item);
                }

            }
            return listtemp;
        }
    }
}

