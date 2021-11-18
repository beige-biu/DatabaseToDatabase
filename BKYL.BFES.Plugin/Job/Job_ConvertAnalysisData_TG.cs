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
    class Job_ConvertAnalysisData_TG : BaseJob
    {
        string log = "";

        Dictionary<string, string> dicOre = new Dictionary<string, string>();
        Dictionary<double, string> dicOreAna = new Dictionary<double, string>();
        Dictionary<string, string> dicIron = new Dictionary<string, string>();
        Dictionary<string, string> dicSlag = new Dictionary<string, string>();

        public override void InitTask()
        {
            #region L3_SAMPLE_CUR_INFO字典

            dicOre.Add("+16mm", "M_SIZEUP16");
            dicOre.Add("+25mm", "M_SIZEUP25");
            dicOre.Add("+3mm", "M_SIZEUP3");
            dicOre.Add("+40mm", "M_SIZEUP40");
            dicOre.Add("+50mm", "M_SIZEUP50");
            dicOre.Add("+5mm", "M_SIZEUP5");
            dicOre.Add("+75mm", "M_SIZEUP75");
            dicOre.Add("0.5-1mm", "M_SIZE05_1");
            dicOre.Add("-0.5mm", "M_SIZEUNDER05");
            dicOre.Add("10-16mm", "M_SIZE10_16");
            dicOre.Add("10-25mm", "M_SIZE10_25");
            dicOre.Add("1-3mm", "M_SIZE1_3");
            dicOre.Add("15-25mm", "M_SIZE15_25");
            dicOre.Add("-15mm", "M_SIZEUNDER15");
            dicOre.Add("16-25mm", "M_SIZE16_25");
            dicOre.Add("-200目", "M_SIZEUNDER200");
            dicOre.Add("25-36mm", "M_SIZE25_36");
            dicOre.Add("25-40mm", "M_SIZE25_40");
            dicOre.Add("25-50mm", "M_SIZE25_50");
            dicOre.Add("3-5mm", "M_SIZE3_5");
            dicOre.Add("36-40mm", "M_SIZE36_40");
            dicOre.Add("40-50mm", "M_SIZE40_50");
            dicOre.Add("50-75mm", "M_SIZE50_75");
            dicOre.Add("5-10mm", "M_SIZE5_10");
            dicOre.Add("5-25mm", "M_SIZE5_25");
            dicOre.Add("5-8mm", "M_SIZE5_8");
            dicOre.Add("-5mm", "M_SIZEUNDER5");
            dicOre.Add("8-16mm", "M_SIZE8_16");
            dicOre.Add("8-25mm", "M_SIZE8_25");
            dicOre.Add("8-40mm", "M_SIZE8_40");
            dicOre.Add("-8mm", "M_SIZEUNDER8");
            dicOre.Add("Al2O3", "M_AL2O3_CONT");
            dicOre.Add("CaF2", "M_CAF2_CONT");
            dicOre.Add("CaO", "M_CAO_CONT");
            dicOre.Add("Fe2O3", "M_FE2O3_CONT");
            dicOre.Add("FeO", "M_FEO_CONT");
            dicOre.Add("K2O", "M_K2O_CONT");
            dicOre.Add("M10", "M_COKE_M10");
            dicOre.Add("M40", "M_COKE_M40");
            dicOre.Add("MgO", "M_MGO_CONT");
            dicOre.Add("TMn", "M_MN_CONT");
            dicOre.Add("MnO", "M_MNO_CONT");
            dicOre.Add("Na2O", "M_NA2O_CONT");
            dicOre.Add("P2O5", "M_P2O5_CONT");
            dicOre.Add("Pb", "M_PB_CONT");
            dicOre.Add("R2", "M_R2");
            dicOre.Add("R3", "M_R3");
            dicOre.Add("R4", "M_R4");
            dicOre.Add("RDI", "M_RDI");
            dicOre.Add("RI", "M_RI");
            dicOre.Add("SiO2", "M_SIO2_CONT");
            dicOre.Add("SO3", "M_SO3_CONT");
            dicOre.Add("TFe", "M_TFE_CONT");
            dicOre.Add("TiO2", "M_TIO2_CONT");
            dicOre.Add("Zn", "M_ZN_CONT");
            dicOre.Add("爆炸性", "M_BAOZHA");
            dicOre.Add("反应后强度", "M_COKE_CSR");
            dicOre.Add("蜂焦", "M_COKE_FENG");
            dicOre.Add("固定碳", "M_C_CONT");
            dicOre.Add("合格率", "M_HEGE_RATIO");
            dicOre.Add("黑头焦", "M_COKE_HEITOU");
            dicOre.Add("灰份", "M_COKEASH");
            dicOre.Add("挥发份", "M_VOLA_CONT");
            dicOre.Add("抗磨强度", "M_KANGMO_INDEX");
            dicOre.Add("抗压强度", "M_KANGYA_PELLET");
            dicOre.Add("平均粒径", "M_AVGSIZE");
            dicOre.Add("全量", "M_QUANLIANG");
            dicOre.Add("全硫", "M_S_CONT");
            dicOre.Add("热爆裂性", "M_REBAOLIE");
            dicOre.Add("热态反应性", "M_COKE_CRI");
            dicOre.Add("烧损", "M_SHAOSUN");
            dicOre.Add("收到基低位发热量", "M_STAND_HEAT");
            dicOre.Add("水份", "M_MOISTURE");
            dicOre.Add("着火点", "M_FIREPOINT");
            dicOre.Add("转鼓强度", "M_ZHUANGU_INDEX");
            dicOre.Add("P", "M_P_CONT");
            dicOre.Add("Mn", "M_MN_CONT");
            dicOre.Add("S", "M_S_CONT");



            #endregion

            #region TAPPING_IRON_LAB字典
            dicIron.Add("C", "HMC");
            dicIron.Add("Ni", "HMNI");
            dicIron.Add("Si", "HMSI");
            dicIron.Add("Ti", "HMTI");
            dicIron.Add("S", "HMS");
            dicIron.Add("Cr", "HMCR");
            dicIron.Add("Mn", "HMMN");
            dicIron.Add("P", "HMP");
            #endregion

            #region TAPPING_SLAG_LAB字典
            dicSlag.Add("Mn", "SLAGMN");
            dicSlag.Add("P", "SLAGP");
            dicSlag.Add("S", "SLAGS");
            dicSlag.Add("全量", "SLAG_QUANLIANG");
            dicSlag.Add("K2O", "SLAGK2O");
            dicSlag.Add("Na2O", "SLAGNA2O");
            dicSlag.Add("Pb", "SLAGPB");
            dicSlag.Add("Zn", "SLAGZN");
            dicSlag.Add("SiO2", "SLAGSIO2");
            dicSlag.Add("CaO", "SLAGCAO");
            dicSlag.Add("MgO", "SLAGMGO");
            dicSlag.Add("Al2O3", "SLAGAL2O3");
            dicSlag.Add("MnO", "SLAGMNO");
            dicSlag.Add("TFe", "SLAGFE");
            dicSlag.Add("FeO", "SLAGFEO");
            dicSlag.Add("TiO2", "SLAGTIO2");
            dicSlag.Add("R2", "SLAGR2");
            dicSlag.Add("R3", "SLAGR3");
            dicSlag.Add("R4", "SLAGR4");
            #endregion
        }
        public override void RunTask(DateTime currentTime)
        {
            #region L3ANALYSI字段名
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
                                    "M_ZN_CONT",
                                    "M_COKEASH",
                                    "M_CAF2_CONT",
                                    "M_VOLA_CONT",
                                    "M_H_CONT",
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
                                    "M_COKE_CRI",
                                    "M_COKE_CSR",
                                    "M_ZHUANGU_INDEX",
                                    "M_R2",
                                    "M_R3",
                                    "M_KANGYA_PELLET",
                                    "M_SHAOSUN",
                                    "M_PENGZHANGRATIO",
                                    "M_AS_CONT",
                                    "M_CR_CONT",
                                    "M_CU_CONT",
                                    "M_SN_CONT",
                                    "M_H2O_CONT",
                                    "M_RI",
                                    "SMP_NO",
                                    "SMP_TYPE",
                                    "SERIAL_NO",
                                    "M_SIZEUNDER5",
                                    "R_HOUR_MSG",
                                    "M_SO3_CONT",
                                    "M_COKE_M10",
                                    "M_COKE_M40",
                                    "M_KANGMO_INDEX",
                                    "M_COKE_FENG",
                                    "M_HEGE_RATIO",
                                    "M_COKE_HEITOU",
                                    "M_R4",
                                    "M_REBAOLIE",
                                    "M_BAOZHA",
                                    "M_STAND_HEAT",
                                    "M_FIREPOINT",
                                    "M_QUANLIANG",
                                    "M_RDI",
                                    "M_SIZEUNDER200",
                                    "M_SIZEUNDER05",
                                    "M_SIZE05_1",
                                    "M_SIZE1_3",
                                    "M_SIZE3_5",
                                    "M_SIZEUP3",
                                    "M_SIZEUP5",
                                    "M_SIZE5_8",
                                    "M_SIZE8_16",
                                    "M_SIZE5_10",
                                    "M_SIZE10_25",
                                    "M_SIZE25_50",
                                    "M_SIZEUP50",
                                    "M_SIZEUNDER15",
                                    "M_SIZE15_25",
                                    "M_SIZE25_36",
                                    "M_SIZE36_40",
                                    "M_SIZE25_40",
                                    "M_SIZE40_50",
                                    "M_SIZE50_75",
                                    "M_SIZEUP75",
                                    "M_AVGSIZE",
                                    "M_SIZE5_25",
                                    "M_SIZEUP40",
                                    "M_SIZE40_75",
                                    "M_SIZEUP25",
                                    "M_SIZEUP16",
                                    "M_SIZE10_16",
                                    "M_SIZE16_25",
                                    "M_SIZE8_25",
                                    "M_SIZE8_40",
                                    "M_SIZEUNDER8"

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
                                    "M_ZN_CONT",
                                    "M_COKEASH",
                                    "M_CAF2_CONT",
                                    "M_VOLA_CONT",
                                    "M_H_CONT",
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
                                    "M_COKE_CRI",
                                    "M_COKE_CSR",
                                    "M_ZHUANGU_INDEX",
                                    "M_R2",
                                    "M_R3",
                                    "M_KANGYA_PELLET",
                                    "M_SHAOSUN",
                                    "M_PENGZHANGRATIO",
                                    "M_AS_CONT",
                                    "M_CR_CONT",
                                    "M_CU_CONT",
                                    "M_SN_CONT",
                                    "M_H2O_CONT",
                                    "M_RI",
                                    "SMP_NO",
                                    "SMP_TYPE",
                                    "SERIAL_NO",
                                    "M_SIZEUNDER5",
                                    "R_HOUR_MSG",
                                    "M_SO3_CONT",
                                    "M_COKE_M10",
                                    "M_COKE_M40",
                                    "M_KANGMO_INDEX",
                                    "M_COKE_FENG",
                                    "M_HEGE_RATIO",
                                    "M_COKE_HEITOU",
                                    "M_R4",
                                    "M_REBAOLIE",
                                    "M_BAOZHA",
                                    "M_STAND_HEAT",
                                    "M_FIREPOINT",
                                    "M_QUANLIANG",
                                    "M_RDI",
                                    "M_SIZEUNDER200",
                                    "M_SIZEUNDER05",
                                    "M_SIZE05_1",
                                    "M_SIZE1_3",
                                    "M_SIZE3_5",
                                    "M_SIZEUP3",
                                    "M_SIZEUP5",
                                    "M_SIZE5_8",
                                    "M_SIZE8_16",
                                    "M_SIZE5_10",
                                    "M_SIZE10_25",
                                    "M_SIZE25_50",
                                    "M_SIZEUP50",
                                    "M_SIZEUNDER15",
                                    "M_SIZE15_25",
                                    "M_SIZE25_36",
                                    "M_SIZE36_40",
                                    "M_SIZE25_40",
                                    "M_SIZE40_50",
                                    "M_SIZE50_75",
                                    "M_SIZEUP75",
                                    "M_AVGSIZE",
                                    "M_SIZE5_25",
                                    "M_SIZEUP40",
                                    "M_SIZE40_75",
                                    "M_SIZEUP25",
                                    "M_SIZEUP16",
                                    "M_SIZE10_16",
                                    "M_SIZE16_25",
                                    "M_SIZE8_25",
                                    "M_SIZE8_40",
                                    "M_SIZEUNDER8"
                                    };
            #endregion

            # region TAPPING_SLAG_LAB字段名
            string[] ColumnArrySlag = {   "ANALYSIS_TIME",
                                            "TAPPING_NUM",
                                            "SLAGCAO",
                                            "SLAGSIO2",
                                            "SLAGAL2O3",
                                            "SLAGMGO",
                                            "SLAGR2",
                                            "SLAGTIO2",
                                            "SAMPLEID",
                                            "SLAGS",
                                            "SLAGFEO",
                                            "SLAGFE",
                                            "SLAGP",
                                            "SLAGMNO",
                                            "SLAGMN",
                                            "SLAGR3",
                                            "SLAGR4",
                                            "SLAGK2O",
                                            "SLAGNA2O",
                                            "SLAGPB",
                                            "SLAGZN",
                                            "SLAG_QUANLIANG",
                                            "CLASS" };
            string[] ValueArrySlag = {   "ANALYSIS_TIME",
                                            "TAPPING_NUM",
                                            "SLAGCAO",
                                            "SLAGSIO2",
                                            "SLAGAL2O3",
                                            "SLAGMGO",
                                            "SLAGR2",
                                            "SLAGTIO2",
                                            "SAMPLEID",
                                            "SLAGS",
                                            "SLAGFEO",
                                            "SLAGFE",
                                            "SLAGP",
                                            "SLAGMNO",
                                            "SLAGMN",
                                            "SLAGR3",
                                            "SLAGR4",
                                            "SLAGK2O",
                                            "SLAGNA2O",
                                            "SLAGPB",
                                            "SLAGZN",
                                            "SLAG_QUANLIANG",
                                            "CLASS" };
            #endregion

            #region TAPPING_IRON_LAB字段名
            string[] ColumnArryIron = {   "ANALYSIS_TIME",
                                            "TAPNUMB",
                                            "HMSI",
                                            "HMS",
                                            "HMP",
                                            "HMMN",
                                            "HMC",
                                            "HMTI",
                                            "SAMPLEID",
                                            "HMCU",
                                            "HMAS",
                                            "HMCR",
                                            "HMNI",
                                            "CLASS" };
            string[] ValueArryIron = {  "ANALYSIS_TIME",
                                            "TAPNUMB",
                                            "HMSI",
                                            "HMS",
                                            "HMP",
                                            "HMMN",
                                            "HMC",
                                            "HMTI",
                                            "SAMPLEID",
                                            "HMCU",
                                            "HMAS",
                                            "HMCR",
                                            "HMNI",
                                            "CLASS" };
            #endregion

            DateTime endTime = currentTime;
            DateTime startTime = currentTime.AddDays(-4);
            List<L3MATERIAL> L3material = GetL3Material();
            List<Models.TG.L3ANALYSI> ReusltList = new List<Models.TG.L3ANALYSI>();
            List<Models.TG.TAPPING_IRON_LAB> IronReusltList = new List<Models.TG.TAPPING_IRON_LAB>();
            List<Models.TG.TAPPING_SLAG_LAB> SlagReusltList = new List<Models.TG.TAPPING_SLAG_LAB>();
            List<L3_SAMPLE_CUR_INFO> dataList = new List<L3_SAMPLE_CUR_INFO>();
            List<L3_SAMPLE_CUR_INFO> MATERIAL_ANALYSI = null;
            try
            {
                if (L3material != null && L3material.Count > 0)
                {
                    foreach (L3MATERIAL item in L3material)
                    {
                        MATERIAL_ANALYSI = Get_MATERIAL_ANALYSI(item, startTime, endTime);
                        if (MATERIAL_ANALYSI.Count == 0) //如果本次没有查询到该时间范围内L3material中该料种的检化验原始数据
                        {
                            continue;
                        }

                        if (MATERIAL_ANALYSI != null && MATERIAL_ANALYSI.Count > 0)
                        {
                            List<Models.TG.L3ANALYSI> reusltList = CalL3ana(MATERIAL_ANALYSI, item);
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
                MATERIAL_ANALYSI = Get_IronSlag_ANALYSI(new L3MATERIAL() { CODEL1 = "FEYW" }, startTime, endTime);
                    if (MATERIAL_ANALYSI != null && MATERIAL_ANALYSI.Count > 0)
                    {
                        List<Models.TG.TAPPING_IRON_LAB> IronreusltList = CalL3anaIron(MATERIAL_ANALYSI);
                        if (IronreusltList != null && IronreusltList.Count > 0)
                        {
                            IronReusltList.AddRange(IronreusltList);
                        }
                    IronreusltList?.Clear();
                    IronreusltList = null;
                        MATERIAL_ANALYSI.Clear();
                        MATERIAL_ANALYSI = null;
                    }
                MATERIAL_ANALYSI = Get_IronSlag_ANALYSI(new L3MATERIAL() { CODEL1 = "ZHYW" }, startTime, endTime);
                    if (MATERIAL_ANALYSI != null && MATERIAL_ANALYSI.Count > 0)
                    {
                        List<Models.TG.TAPPING_SLAG_LAB> SlagreusltList = CalL3anaSlag(MATERIAL_ANALYSI);
                        if (SlagreusltList != null && SlagreusltList.Count > 0)
                        {
                            SlagReusltList.AddRange(SlagreusltList);
                        }
                    SlagreusltList?.Clear();
                    SlagreusltList = null;
                        MATERIAL_ANALYSI.Clear();
                        MATERIAL_ANALYSI = null;
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
            //IronReusltList = DealRepeatData(IronReusltList);
            //SlagreusltList = DealRepeatData(SlagreusltList);

            //插入数据L3ANALYSI
            int delete1 = 0;
            int delete2 = 0;
            if (ReusltList != null && ReusltList.Count > 0)
            {
                using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    try
                    {
                        idatabase.BeginTran();//
                        delete1 = idatabase.ExecuteCommand(string.Format("delete from L3ANALYSI where timestamp>={0} and timestamp<={1}", DataBaseFactory.ConvertTimeString(startTime, S_DBSource.DBType), DataBaseFactory.ConvertTimeString(endTime, S_DBSource.DBType)));
                        log = string.Format("L3ANALYSI成功删除{0}条记录！", delete1);
                        Outputlog(log);
                        idatabase.BulkInsert<Models.TG.L3ANALYSI>("L3ANALYSI", ColumnArry, ValueArry, ReusltList);
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
            //插入数据TAPPING_IRON_LAB
            if (IronReusltList != null && IronReusltList.Count > 0)
            {
                using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    try
                    {
                        idatabase.BeginTran();//
                        delete1 = idatabase.ExecuteCommand(string.Format("delete from TAPPING_IRON_LAB where ANALYSIS_TIME >= {0} and ANALYSIS_TIME <= {1}", DataBaseFactory.ConvertTimeString(startTime, S_DBSource.DBType), DataBaseFactory.ConvertTimeString(endTime, S_DBSource.DBType)));
                        log = string.Format("TAPPING_IRON_LAB成功删除{0}条记录！", delete1);
                        Outputlog(log);
                        idatabase.BulkInsert<Models.TG.TAPPING_IRON_LAB>("TAPPING_IRON_LAB", ColumnArryIron, ValueArryIron, IronReusltList);
                        idatabase.CommitTran();
                        log = string.Format("TAPPING_IRON_LAB成功插入{0}条记录！", IronReusltList.Count);
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
                        IronReusltList.Clear();
                        IronReusltList = null;
                    }
                }
            }
            //插入数据TAPPING_SLAG_LAB
            if (SlagReusltList != null && SlagReusltList.Count > 0)
            {
                using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    try
                    {
                        idatabase.BeginTran();//
                        delete1 = idatabase.ExecuteCommand(string.Format("delete from TAPPING_SLAG_LAB where ANALYSIS_TIME >= {0} and ANALYSIS_TIME <= {1}", DataBaseFactory.ConvertTimeString(startTime, S_DBSource.DBType), DataBaseFactory.ConvertTimeString(endTime, S_DBSource.DBType)));
                        log = string.Format("TAPPING_SLAG_LAB成功删除{0}条记录！", delete1 + delete2);
                        Outputlog(log);
                        idatabase.BulkInsert<Models.TG.TAPPING_SLAG_LAB>("TAPPING_SLAG_LAB", ColumnArrySlag, ValueArrySlag, SlagReusltList);
                        idatabase.CommitTran();
                        log = string.Format("TAPPING_SLAG_LAB成功插入{0}条记录！", SlagReusltList.Count);
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
                        SlagReusltList.Clear();
                        SlagReusltList = null;
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

        private List<L3MATERIAL> GetL3Material()
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
            {
                return iDataBase.Queryable<L3MATERIAL>().Where(m => m.M_TYPE != -1).ToList();//
            }
        }
        private List<L3_SAMPLE_CUR_INFO> Get_MATERIAL_ANALYSI(L3MATERIAL model, DateTime startTime, DateTime endTime)
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
            {
                return iDataBase.Queryable<L3_SAMPLE_CUR_INFO>().Where(m => m.ANALYSE_TIME >= startTime && m.ANALYSE_TIME <= endTime).ToList()
                    .Where(m => m.SAMPLE == model.CODEL1).ToList();
            }
        }
        private List<L3_SAMPLE_CUR_INFO> Get_IronSlag_ANALYSI(L3MATERIAL model, DateTime startTime, DateTime endTime)
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
            {
                return iDataBase.Queryable<L3_SAMPLE_CUR_INFO>().Where(m => m.ANALYSE_TIME >= startTime && m.ANALYSE_TIME <= endTime).ToList()
                    .Where(m => m.SAMPLEIRON == model.CODEL1).ToList();
            }
        }
        private List<L3_SET_TYPE_ITEMS> Get_MaterialItems(L3MATERIAL model, DateTime startTime, DateTime endTime)
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
            {
                return iDataBase.Queryable<L3_SET_TYPE_ITEMS>().Where(m => m.SAMPLE_TYPE_NAME == model.CODEL3).ToList();
            }
        }

        private List<Models.TG.L3ANALYSI> CalL3ana(List<L3_SAMPLE_CUR_INFO> dataList, L3MATERIAL model)
        {
            List<Models.TG.L3ANALYSI> reusltList = null;
            Models.TG.L3ANALYSI newmodel = null;
            List<L3_SET_TYPE_ITEMS> tempitemList = null;
            if (dataList != null && dataList.Count > 0)
            {
                reusltList = new List<Models.TG.L3ANALYSI>();
                tempitemList = new List<L3_SET_TYPE_ITEMS>();

                IEnumerable<IGrouping<String, L3_SAMPLE_CUR_INFO>> AnalysiGroup = dataList.GroupBy(s => s.SAMPLE_TYPE_CODE);
                int INDEX = 0;
                foreach (IGrouping<String, L3_SAMPLE_CUR_INFO> item in AnalysiGroup)
                {
                    List<L3_SAMPLE_CUR_INFO> dataSource = item.ToList();
                    using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                    {
                        tempitemList = iDataBase.Queryable<L3_SET_TYPE_ITEMS>().Where(m => m.SAMPLE_TYPE_CODE == dataSource[INDEX].SAMPLE_TYPE_CODE).OrderBy(s => s.ELE_SEQ).ToList();
                    }
                    INDEX++;
                    if (dataSource.Count > 0)
                    {
                       
                        foreach (L3_SAMPLE_CUR_INFO material in item)
                        {
                            newmodel = new Models.TG.L3ANALYSI()
                            { M_CODE = model.CODE, M_CODE3 = model.CODEL3, TIMESTAMP = dataSource[0].ANALYSE_TIME, SMP_NO = dataSource[0].SAMPLE_ID, SMP_TYPE = dataSource[0].SAMPLE_TYPE_CODE };
                            for (int i = 0; i < tempitemList.Count; i++)//
                            {
                                if (tempitemList[i].ELE_NAME != null)
                                {
                                if (dicOre.ContainsKey(tempitemList[i].ELE_NAME))
                                {
                                    Object Value = CustomModelInfo.GetModelItemValue(material, $"ELE_{tempitemList[i].ELE_SEQ}");
                                    if (Value != null)
                                    {
                                        CustomModelInfo.SetModelItemValue(newmodel, dicOre[tempitemList[i].ELE_NAME], Value);
                                    }
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

        private List<Models.TG.TAPPING_IRON_LAB> CalL3anaIron(List<L3_SAMPLE_CUR_INFO> dataList)
        {
            List<Models.TG.TAPPING_IRON_LAB> reusltList = null;
            Models.TG.TAPPING_IRON_LAB newmodel = null;
            List<L3_SET_TYPE_ITEMS> tempitemList = null;
            if (dataList != null)
            {
                reusltList = new List<Models.TG.TAPPING_IRON_LAB>();
                tempitemList = new List<L3_SET_TYPE_ITEMS>();
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    tempitemList = iDataBase.Queryable<L3_SET_TYPE_ITEMS>().Where(m => m.SAMPLE_TYPE_CODE == "FEY").OrderBy(s => s.ELE_SEQ).ToList();
                }

                foreach (L3_SAMPLE_CUR_INFO material in dataList)
                {
                    newmodel = new Models.TG.TAPPING_IRON_LAB()
                    { ANALYSIS_TIME = material.ANALYSE_TIME, TAPNUMB = material.TAP_NO.ToInt(), CLASS = material.TEAM_NAME};
                    for (int i = 0; i < tempitemList.Count; i++)//循环判断各字段、值是否为空，对均不为空的进行赋值,因为为空ToString会报错
                    {
                        if (dicIron.ContainsKey(tempitemList[i].ELE_NAME))
                        {
                            Object Value = CustomModelInfo.GetModelItemValue(material, $"ELE_{tempitemList[i].ELE_SEQ}");
                            if (Value != null)
                            {
                                CustomModelInfo.SetModelItemValue(newmodel, dicIron[tempitemList[i].ELE_NAME], Value);
                            }
                        }
                    }
                }
                reusltList.Add(newmodel);
            }
            return reusltList;
        }

        private List<Models.TG.TAPPING_SLAG_LAB> CalL3anaSlag(List<L3_SAMPLE_CUR_INFO> dataList)
        {
            List<Models.TG.TAPPING_SLAG_LAB> reusltList = null;
            Models.TG.TAPPING_SLAG_LAB newmodel = null;
            List<L3_SET_TYPE_ITEMS> tempitemList = null;
            if (dataList != null)
            {
                reusltList = new List<Models.TG.TAPPING_SLAG_LAB>();
                tempitemList = new List<L3_SET_TYPE_ITEMS>();
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    tempitemList = iDataBase.Queryable<L3_SET_TYPE_ITEMS>().Where(m => m.SAMPLE_TYPE_CODE == "ZHY").OrderBy(s => s.ELE_SEQ).ToList();
                }

                foreach (L3_SAMPLE_CUR_INFO material in dataList)
                {
                    newmodel = new Models.TG.TAPPING_SLAG_LAB()
                    { ANALYSIS_TIME = material.ANALYSE_TIME, TAPPING_NUM = material.TAP_NO.ToInt(), CLASS = material.TEAM_NAME};
                    for (int i = 0; i < tempitemList.Count; i++)//循环判断各字段、值是否为空，对均不为空的进行赋值,因为为空ToString会报错
                    {
                        if (dicSlag.ContainsKey(tempitemList[i].ELE_NAME))
                        {
                            Object Value = CustomModelInfo.GetModelItemValue(material, $"ELE_{tempitemList[i].ELE_SEQ}");
                            if (Value != null)
                            {
                                CustomModelInfo.SetModelItemValue(newmodel, dicSlag[tempitemList[i].ELE_NAME], Value);
                            }
                        }
                    }
                }
                reusltList.Add(newmodel);
            }
            return reusltList;
        }

        private List<Models.TG.L3ANALYSI> DealRepeatData(List<Models.TG.L3ANALYSI> ListModel)
        {
            List<Models.TG.L3ANALYSI> listtemp = new List<Models.TG.L3ANALYSI>();
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

        //private List<Models.TG.TAPPING_IRON_LAB> DealRepeatDataIron(List<Models.TG.TAPPING_IRON_LAB> ListModel)
        //{
        //    List<Models.TG.TAPPING_IRON_LAB> listtemp = new List<Models.TG.TAPPING_IRON_LAB>();
        //    if (ListModel != null && ListModel.Count > 0)
        //    {
        //        ListModel = ListModel.OrderBy(x => x.TIMESTAMP).ToList();

        //        foreach (var item in ListModel)
        //        {
        //            if (listtemp.Find(t => t.TAPNUMB == item.TAPNUMB && t.ANALYSIS_TIME == item.ANALYSIS_TIME) != null)
        //            {
        //                item.TIMESTAMP = listtemp.FindAll(x => x.TAPNUMB == item.TAPNUMB).Max(match => match.TIMESTAMP).AddSeconds(1);
        //            }

        //            listtemp.Add(item);
        //        }
        //    }
        //    return listtemp;
        //}

        //private List<Models.TG.TAPPING_SLAG_LAB> DealRepeatDataSlag(List<Models.TG.TAPPING_SLAG_LAB> ListModel)
        //{
        //    List<Models.TG.TAPPING_SLAG_LAB> listtemp = new List<Models.TG.TAPPING_SLAG_LAB>();
        //    if (ListModel != null && ListModel.Count > 0)
        //    {
        //        ListModel = ListModel.OrderBy(x => x.TIMESTAMP).ToList();

        //        foreach (var item in ListModel)
        //        {
        //            if (listtemp.Find(t => t.TAPPING_NUM == item.TAPPING_NUM && t.ANALYSIS_TIME == item.ANALYSIS_TIME) != null)
        //            {
        //                item.TIMESTAMP = listtemp.FindAll(x => x.TAPPING_NUM == item.TAPPING_NUM).Max(match => match.TIMESTAMP).AddSeconds(1);
        //            }
        //            listtemp.Add(item);
        //        }

        //    }
        //    return listtemp;
        //}
    }
}

