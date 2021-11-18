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

    class XC1500_ConvertMesData : BaseJob
    {
        List<string> s = new List<string>();
        StringBuilder ss = new StringBuilder();
        Dictionary<string, string> dic = new Dictionary<string, string>();

        Dictionary<string, string> dicIron = new Dictionary<string, string>();
        Dictionary<string, string> dicSlag = new Dictionary<string, string>();

        Dictionary<string, List<MES_TQMIR21_IOOP>> IronSlag_MES_TQMIR21_IOOP = new Dictionary<string, List<MES_TQMIR21_IOOP>>();


        DataTable HopperModel = null;
        List<MES_PUBLIC_MATERIAL_2> MespublicList = null;
        List<L3MATERIAL> L3materialList = null;
        string log = "";
        string[] columnNames;
        string[] dataFiledNameArr;
        public override void InitTask()
        {
            #region 设置对应字典
            dic.Add("Al2O3", "M_AL2O3_CONT");
            dic.Add("CAO_SIO2", "M_BASICITY");
            dic.Add("CAO_2SIO2", "M_BASICITY");
            dic.Add("R2", "M_BASICITY");
            dic.Add("FC", "M_C_CONT");
            dic.Add("CaO", "M_CAO_CONT");
            dic.Add("Ad", "M_COKEASH");
            dic.Add("FeO", "M_FEO_CONT");
            dic.Add("K2O", "M_K2O_CONT");
            dic.Add("MgO", "M_MGO_CONT");
            dic.Add("TMn", "M_MN_CONT");
            dic.Add("MnO", "M_MNO_CONT");
            dic.Add("H2O", "M_MOISTURE");
            dic.Add("Mt", "M_MOISTURE");

            dic.Add("Na2O", "M_NA2O_CONT");
            dic.Add("P", "M_P_CONT");
            dic.Add("Pb", "M_PB_CONT");
            dic.Add("S", "M_S_CONT");
            dic.Add("SiO2", "M_SIO2_CONT");
            dic.Add("TFe", "M_TFE_CONT");
            dic.Add("TiO2", "M_TIO2_CONT");
            dic.Add("Vd", "M_VOLA_CONT");
            dic.Add("Zn", "M_ZN_CONT");
            dic.Add("ADE2", "M_ADE2");
            dic.Add("ADE3", "M_ADE3");
            dic.Add("AS", "M_AS");
            dic.Add("Cu", "M_CU");
            dic.Add("LOI", "M_LOI");
            dic.Add("ADE1", "M_ADE1");
            dic.Add("ZnO", "M_ZNO");
            dic.Add("TS", "M_TS");
            dic.Add("AMS", "M_AMS");
            dic.Add("ADE4", "M_ADE4");
            dic.Add("ADE5", "M_ADE5");
            dic.Add("ADE6", "M_ADE6");
            dic.Add("Std", "M_S_CONT");
            dic.Add("C", "M_C");
            dic.Add("M25", "M_M25");
            dic.Add("M40", "M_M40");
            dic.Add("Mad", "M_MAD");
            dic.Add("CRI", "M_CRI");
            dic.Add("CSR", "M_CSR");
            dic.Add("SZ60", "M_SZ60");
            dic.Add("SZ40", "M_SZ40");
            dic.Add("S40_25", "M_S40_25");
            dic.Add("S_25", "M_S_25");
            dic.Add("S_30", "M_S_30");
            dic.Add("S40_60", "M_S40_60");
            dic.Add("M10", "M_M10");
            dic.Add("Ti", "M_TI");
            dic.Add("SZ31Z5", "M_SZ31Z5");
            dic.Add("SZ25", "M_SZ25");
            dic.Add("SZ10", "M_SZ10");
            dic.Add("SZ6Z3", "M_SZ6Z3");
            dic.Add("S__6Z3", "M_S_6Z3");
            dic.Add("RDIZ6Z3", "M_RDIZ6Z3");
            dic.Add("RDIZ3Z15", "M_RDIZ3Z15");
            dic.Add("RDI_0Z5", "M_RDI_0Z5");
            dic.Add("RI", "M_RI");
            dic.Add("RUANHKS", "M_RUANHKS");
            dic.Add("RUANHZL", "M_RUANHZL");
            dic.Add("RUANHQJ", "M_RUANHQJ");
            dic.Add("RONGHKS", "M_RONGHKS");
            dic.Add("RONGHZL", "M_RONGHZL");
            dic.Add("RONGHQJ", "M_RONGHQJ");
            dic.Add("YC", "M_YC");
            dic.Add("RRCHD", "M_RRCHD");
            dic.Add("DI_0Z5", "M_DI_0Z5");
            dic.Add("D_3Z15", "M_D_3Z15");
            dic.Add("DI_6Z3", "M_DI_6Z3");
            dic.Add("RDZS", "M_RDZS");
            dic.Add("Star", "M_S_CONT");
            dic.Add("KZNA", "M_KZNA");
            dic.Add("GWR", "M_GWR");
            dic.Add("HEATQTY", "M_HEATQTY");
            dic.Add("Mar", "M_MAR");
            dic.Add("SMZ180", "M_SMZ180");
            dic.Add("SM_200", "M_SM_200");
            dic.Add("SMZ200", "M_SMZ200");
            dic.Add("SZ16", "M_SZ16");
            dic.Add("S16_10", "M_S16_10");
            dic.Add("S10_5", "M_S10_5");
            dic.Add("S_5", "M_S_5");
            dic.Add("S20_6Z3", "M_S20_6Z3");
            dic.Add("S8_5", "M_S8_5");
            dic.Add("DI", "M_DI");
            dic.Add("KYN", "M_KYN");
            dic.Add("RSI", "M_RSI");
            dic.Add("CR", "M_CR");
            dic.Add("S8_16", "M_S8_16");
            dic.Add("TI6Z3", "M_TI6Z3");
            dic.Add("CCS", "M_CCS");
            dic.Add("KMZS0Z5", "M_KMZS0Z5");
            dic.Add("S25_16", "M_S25_16");
            dic.Add("RDI_3Z15", "M_RDI_3Z15");
            dic.Add("S_10", "M_S_10");
            dic.Add("S10_30", "M_S10_30");
            dic.Add("SZ30", "M_SZ30");
            dic.Add("S_8", "M_S8");
            dic.Add("S__8", "M_S8");
            dic.Add("S25_31.5", "M_S25_31D5");
            dic.Add("S6.3_25", "M_S6D3_25");
            dic.Add("SZ31.5", "M_SZ31D5");

            dic.Add("S25_40", "M_S25_40");
            dic.Add("HEATQTY-2", "M_HEATQTY_2");
            dic.Add("S0_45", "M_S0_45");
            dic.Add("S_Z75", "M_S_Z75");
            dic.Add("HSKMXS", "M_HSKMXS");
            dic.Add("S0_30", "M_S0_30");
            dic.Add("S_6Z3", "M_S_6Z3");

            dic.Add("Fe2O3", "M_FE2O3_CONT");
            dic.Add("P2O5", "M_P2O5_CONT");
            dic.Add("S20_90", "M_S20_90");
            dic.Add("SZ90", "M_SZ90");
            dic.Add("S_20", "M_S20");


            #endregion
            #region 铁水字典
            dicIron.Add("C", "C");
            dicIron.Add("Si", "SI");
            dicIron.Add("Mn", "MN");
            dicIron.Add("P", "P");
            dicIron.Add("S", "S");
            dicIron.Add("Cu", "CU");
            dicIron.Add("CR", "CR");
            dicIron.Add("Ti", "TI");
            dicIron.Add("Ni", "NI");
            dicIron.Add("AS", "A_S");
            dicIron.Add("CO", "CO");
            dicIron.Add("Pb", "PB");
            dicIron.Add("Sn", "SN");
            #endregion
            #region 炉渣字典
            dicSlag.Add("SiO2", "SIO2");
            dicSlag.Add("CaO", "CAO");
            dicSlag.Add("FeO", "FEO");
            dicSlag.Add("MnO", "MNO");
            dicSlag.Add("MgO", "MGO");
            dicSlag.Add("Al2O3", "AL2O3");
            dicSlag.Add("K2O", "K2O");
            dicSlag.Add("Na2O", "NA2O");
            dicSlag.Add("TiO2", "TIO2");
            dicSlag.Add("S", "S");
            dicSlag.Add("R2", "R2");
            dicSlag.Add("CAO_2SIO2", "CAO_2SIO2");
            #endregion
            #region 字段
            columnNames = new string[]{ "TIMESTAMP",
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
                                        "M_COKEASH",
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
                                        "M_ADE2",
                                        "M_ADE3",
                                        "M_AS",
                                        "M_CU",
                                        "M_LOI",
                                        "M_ADE1",
                                        "M_ZNO",
                                        "M_TS",
                                        "M_AMS",
                                        "M_ADE4",
                                        "M_ADE5",
                                        "M_ADE6",
                                        "M_STD",
                                        "M_C",
                                        "M_M25",
                                        "M_M40",
                                        "M_MAD",
                                        "M_CRI",
                                        "M_CSR",
                                        "M_SZ60",
                                        "M_SZ40",
                                        "M_S40_25",
                                        "M_S_25",
                                        "M_S_30",
                                        "M_S40_60",
                                        "M_M10",
                                        "M_TI",
                                        "M_SZ31Z5",
                                        "M_SZ25",
                                        "M_SZ10",
                                        "M_SZ6Z3",
                                        "M_S_6Z3",
                                        "M_RDIZ6Z3",
                                        "M_RDIZ3Z15",
                                        "M_RDI_0Z5",
                                        "M_RI",
                                        "M_RUANHKS",
                                        "M_RUANHZL",
                                        "M_RUANHQJ",
                                        "M_RONGHKS",
                                        "M_RONGHZL",
                                        "M_RONGHQJ",
                                        "M_YC",
                                        "M_RRCHD",
                                        "M_DI_0Z5",
                                        "M_D_3Z15",
                                        "M_DI_6Z3",
                                        "M_RDZS",
                                        "M_STAR",
                                        "M_KZNA",
                                        "M_GWR",
                                        "M_HEATQTY",
                                        "M_MAR",
                                        "M_SMZ180",
                                        "M_SM_200",
                                        "M_SMZ200",
                                        "M_SZ16",
                                        "M_S16_10",
                                        "M_S10_5",
                                        "M_S_5",
                                        "M_S20_6Z3",
                                        "M_S8_5",
                                        "M_DI",
                                        "M_KYN",
                                        "M_RSI",
                                        "M_CR",
                                        "M_S8_16",
                                        "M_TI6Z3",
                                        "M_CCS",
                                        "M_KMZS0Z5",
                                        "M_S25_16",
                                        "M_RDI_3Z15",
                                        "M_S_10",
                                        "M_S10_30",
                                        "M_SZ30",
                                        "M_S8",
                                        "M_S25_31D5",
                                        "M_S6D3_25",
                                        "M_SZ31D5",
                                        "M_MT",
                                        "M_S25_40",
                                        "M_HEATQTY_2",
                                        "M_S0_45",
                                        "M_S_Z75",
                                        "M_HSKMXS",
                                        "M_S0_30",
                                        "M_S20_90",
                                        "M_SZ90",
                                        "M_S20",
                                        "MAT_SAMPLE_NO",
                                        "MAT_SAMPLE_TIME"


                                         };
            dataFiledNameArr = new string[] { "TIMESTAMP",
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
                                "M_COKEASH",
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
                                "M_ADE2",
                                "M_ADE3",
                                "M_AS",
                                "M_CU",
                                "M_LOI",
                                "M_ADE1",
                                "M_ZNO",
                                "M_TS",
                                "M_AMS",
                                "M_ADE4",
                                "M_ADE5",
                                "M_ADE6",
                                "M_STD",
                                "M_C",
                                "M_M25",
                                "M_M40",
                                "M_MAD",
                                "M_CRI",
                                "M_CSR",
                                "M_SZ60",
                                "M_SZ40",
                                "M_S40_25",
                                "M_S_25",
                                "M_S_30",
                                "M_S40_60",
                                "M_M10",
                                "M_TI",
                                "M_SZ31Z5",
                                "M_SZ25",
                                "M_SZ10",
                                "M_SZ6Z3",
                                "M_S_6Z3",
                                "M_RDIZ6Z3",
                                "M_RDIZ3Z15",
                                "M_RDI_0Z5",
                                "M_RI",
                                "M_RUANHKS",
                                "M_RUANHZL",
                                "M_RUANHQJ",
                                "M_RONGHKS",
                                "M_RONGHZL",
                                "M_RONGHQJ",
                                "M_YC",
                                "M_RRCHD",
                                "M_DI_0Z5",
                                "M_D_3Z15",
                                "M_DI_6Z3",
                                "M_RDZS",
                                "M_STAR",
                                "M_KZNA",
                                "M_GWR",
                                "M_HEATQTY",
                                "M_MAR",
                                "M_SMZ180",
                                "M_SM_200",
                                "M_SMZ200",
                                "M_SZ16",
                                "M_S16_10",
                                "M_S10_5",
                                "M_S_5",
                                "M_S20_6Z3",
                                "M_S8_5",
                                "M_DI",
                                "M_KYN",
                                "M_RSI",
                                "M_CR",
                                "M_S8_16",
                                "M_TI6Z3",
                                "M_CCS",
                                "M_KMZS0Z5",
                                "M_S25_16",
                                "M_RDI_3Z15",
                                "M_S_10",
                                "M_S10_30",
                                "M_SZ30",
                                "M_S8",
                                "M_S25_31D5",
                                "M_S6D3_25",
                                "M_SZ31D5",
                                "M_MT",
                                "M_S25_40",
                                "M_HEATQTY_2",
                                "M_S0_45",
                                "M_S_Z75",
                                "M_HSKMXS",
                                "M_S0_30",
                                "M_S20_90",
                                "M_SZ90",
                                "M_S20",
                                 "MAT_SAMPLE_NO",
                                 "MAT_SAMPLE_TIME"
                                     };
            #endregion
        }
        public override void RunTask(DateTime currentTime)
        {
            //currentTime = "2019-07-31 23:00:00".ToDate();
            DateTime startTime = currentTime.AddDays(-2);
            #region 处理原燃料

            List<L3MATERIAL> L3MaterialList = null;
            List<MES_TQMIR23_IOOP> MES_TQMIR23_IOOP_List = new List<MES_TQMIR23_IOOP>();
            List<MES_TQMIR21_IOOP> MES_TQMIR21_IOOP_List = new List<MES_TQMIR21_IOOP>();
            GetBaseData();
            List<L3ANALYSI> L3anaList = new List<L3ANALYSI>();
            L3ANALYSI l3ana = null;
            try
            {
                L3MaterialList = GetUseL3material();
                foreach (L3MATERIAL item in L3MaterialList)
                {
                    MES_TQMIR21_IOOP_List = GetSampleNo(item, startTime, currentTime);

                    if (MES_TQMIR21_IOOP_List.Count == 0) //如果本次没有查询到料种的试样编码
                    {
                        continue;
                    }
                    foreach (MES_TQMIR21_IOOP m in MES_TQMIR21_IOOP_List)
                    {
                        if (L3anaList.FindIndex(c => c.MAT_SAMPLE_NO == m.MAT_SAMPLE_NO) >= 0)
                        {
                            continue;
                        }
                        l3ana = new L3ANALYSI();
                        l3ana.M_CODE = item.CODE;
                        l3ana.M_CODE3 = item.CODEL3;
                        l3ana.MAT_SAMPLE_NO = m.MAT_SAMPLE_NO;
                        l3ana.TIMESTAMP = DateTime.ParseExact(m.REC_REVISE_TIME, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);

                        if (string.IsNullOrEmpty(m.MAT_SAMPLE_TIME))
                        {
                            l3ana.MAT_SAMPLE_TIME = null;
                        }
                        else
                        {
                            l3ana.MAT_SAMPLE_TIME =
                                DateTime.ParseExact(m.MAT_SAMPLE_TIME, "yyyyMMddHHmmss",
                                System.Globalization.CultureInfo.CurrentCulture);
                        }
                        MES_TQMIR23_IOOP_List = GetSampleValue(m, 0);

                        if (MES_TQMIR23_IOOP_List.Count == 0) //如果有试样编码，但是化验成分还没有出来
                        {
                            continue;
                        }
                        GetModelValue(MES_TQMIR23_IOOP_List, dic, ref l3ana);
                        L3anaList.Add(l3ana);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                L3MaterialList.Clear();
                MES_TQMIR23_IOOP_List.Clear();
                MES_TQMIR21_IOOP_List.Clear();
                L3MaterialList = null;
                MES_TQMIR23_IOOP_List = null;
                MES_TQMIR21_IOOP_List = null;
            }

            L3anaList = DealRepeatData(L3anaList);

            int delete = 0;
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                try
                {
                    idatabase.BeginTran();
                    delete = idatabase.ExecuteCommand($"delete from L3ANALYSI where timestamp>={DataBaseFactory.ConvertTimeString(startTime, S_DBSource.DBType)} and timestamp<={DataBaseFactory.ConvertTimeString(currentTime, S_DBSource.DBType)}");
                    log = $"L3ANALYSI成功删除{delete}条记录\r\n";
                    idatabase.BulkInsert<L3ANALYSI>("L3ANALYSI", columnNames, dataFiledNameArr, L3anaList);
                    log += $"L3ANALYSI成功插入{L3anaList.Count}条记录\r\n";
                    idatabase.CommitTran();
                    Outputlog(log);
                }
                catch (Exception)
                {
                    idatabase.RollbackTran();
                    throw;
                }
                finally
                {
                    L3anaList.Clear();
                    L3anaList = null;
                }
            }
            #endregion
            GetSampleNo(startTime, currentTime);

            #region 处理铁水

            string[] columnNames1 = { "TIMESTAMP", "TAPNUMB", "C", "SI", "MN", "P", "S", "CU", "CR", "TI", "V", "NI", "MO", "K2O", "NA2O", "ZNO", "A_S", "CO", "PB", "SN", "MAT_SAMPLE_NO", "MAT_SAMPLE_TIME" };
            string[] dataFiledNameArr1 = { "TIMESTAMP", "TAPNUMB", "C", "SI", "MN", "P", "S", "CU", "CR", "TI", "V", "NI", "MO", "K2O", "NA2O", "ZNO", "A_S", "CO", "PB", "SN", "MAT_SAMPLE_NO", "MAT_SAMPLE_TIME" };

            List<L3TAPPING_IRON_ANA> IronList = new List<L3TAPPING_IRON_ANA>();
            L3TAPPING_IRON_ANA Iron = null;
            try
            {
                if (IronSlag_MES_TQMIR21_IOOP["C60600100100002"].Count > 0)
                {
                    foreach (MES_TQMIR21_IOOP item in IronSlag_MES_TQMIR21_IOOP["C60600100100002"])
                    {
                        if (IronList.FindIndex(m => m.MAT_SAMPLE_NO == item.MAT_SAMPLE_NO) >= 0)
                        {
                            continue;
                        }
                        Iron = new L3TAPPING_IRON_ANA();
                        Iron.TAPNUMB = item.IRON_DEGR;
                        Iron.TIMESTAMP = DateTime.ParseExact(item.REC_REVISE_TIME, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                        Iron.MAT_SAMPLE_NO = item.MAT_SAMPLE_NO;
                        if (string.IsNullOrEmpty(item.MAT_SAMPLE_TIME))
                        {
                            Iron.MAT_SAMPLE_TIME = null;
                        }
                        else
                        {
                            Iron.MAT_SAMPLE_TIME =
                                DateTime.ParseExact(item.MAT_SAMPLE_TIME, "yyyyMMddHHmmss",
                                System.Globalization.CultureInfo.CurrentCulture);
                        }
                        MES_TQMIR23_IOOP_List = GetSampleValue(item, 0);
                        if (MES_TQMIR23_IOOP_List.Count > 0)
                        {
                            GetModelValue(MES_TQMIR23_IOOP_List, dicIron, ref Iron);
                        }

                        IronList.Add(Iron);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                IronSlag_MES_TQMIR21_IOOP.Remove("C60600100100002");
                MES_TQMIR23_IOOP_List = null;
            }

            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                try
                {
                    if (IronList.Count > 0)
                    {
                        DataBaseFactory.IsEnableLogEvent = true;
                        idatabase.BeginTran();
                        delete = idatabase.ExecuteCommand($"delete from L3TAPPING_IRON_ANA where timestamp>={DataBaseFactory.ConvertTimeString(startTime, S_DBSource.DBType)} and timestamp<={DataBaseFactory.ConvertTimeString(currentTime, S_DBSource.DBType)}");
                        log = $"L3TAPPING_IRON_ANA成功删除{delete}条记录\r\n";
                        idatabase.BulkInsert<L3TAPPING_IRON_ANA>("L3TAPPING_IRON_ANA", columnNames1, dataFiledNameArr1, IronList);
                        log += $"L3TAPPING_IRON_ANA成功插入{IronList.Count}条记录\r\n";
                        idatabase.CommitTran();
                        Outputlog(log);
                    }
                }
                catch (Exception)
                {
                    idatabase.RollbackTran();
                    throw;
                }
                finally
                {
                    IronList.Clear();
                    IronList = null;
                }
            }
            #endregion

            #region 处理炉渣
            string[] columnNames2 ={"TIMESTAMP","TAPNUMB","SIO2","CAO","MNO","MGO","P2O5","AL2O3","FEO","FE2O3","TFE","CAF2","K2O","NA2O","V2O5","TIO2",
"S","ZNO","R2","R3","R4","CAO_2SIO2","MAT_SAMPLE_NO","MAT_SAMPLE_TIME"};
            string[] dataFiledNameArr2 ={"TIMESTAMP","TAPNUMB","SIO2","CAO","MNO","MGO","P2O5","AL2O3","FEO","FE2O3","TFE","CAF2","K2O","NA2O","V2O5","TIO2",
"S","ZNO","R2","R3","R4","CAO_2SIO2","MAT_SAMPLE_NO","MAT_SAMPLE_TIME"};
            List<L3TAPPING_SLAG_ANA> SlagList = new List<L3TAPPING_SLAG_ANA>();
            L3TAPPING_SLAG_ANA Slag = null;
            try
            {
                if (IronSlag_MES_TQMIR21_IOOP["C60600100300002A"].Count > 0)
                {
                    foreach (MES_TQMIR21_IOOP item in IronSlag_MES_TQMIR21_IOOP["C60600100300002A"])
                    {
                        if (SlagList.FindIndex(m => m.MAT_SAMPLE_NO == item.MAT_SAMPLE_NO) >= 0)
                        {
                            continue;
                        }
                        Slag = new L3TAPPING_SLAG_ANA();
                        Slag.TAPNUMB = item.IRON_DEGR;
                        Slag.TIMESTAMP = DateTime.ParseExact(item.REC_REVISE_TIME, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                        Slag.MAT_SAMPLE_NO = item.MAT_SAMPLE_NO;
                        if (string.IsNullOrEmpty(item.MAT_SAMPLE_TIME))
                        {
                            Slag.MAT_SAMPLE_TIME = null;
                        }
                        else
                        {
                            Slag.MAT_SAMPLE_TIME =
                                DateTime.ParseExact(item.MAT_SAMPLE_TIME, "yyyyMMddHHmmss",
                                System.Globalization.CultureInfo.CurrentCulture);
                        }

                        MES_TQMIR23_IOOP_List = GetSampleValue(item, 0);
                        if (MES_TQMIR23_IOOP_List.Count > 0)
                        {
                            GetModelValue(MES_TQMIR23_IOOP_List, dicSlag, ref Slag);
                        }
                        SlagList.Add(Slag);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                IronSlag_MES_TQMIR21_IOOP.Remove("C60600100300002A");
                MES_TQMIR23_IOOP_List = null;
            }

            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                try
                {
                    if (SlagList.Count > 0)
                    {
                        idatabase.BeginTran();
                        delete = idatabase.ExecuteCommand($"delete from L3TAPPING_SLAG_ANA where timestamp>={DataBaseFactory.ConvertTimeString(startTime, S_DBSource.DBType)} and timestamp<={DataBaseFactory.ConvertTimeString(currentTime, S_DBSource.DBType)}");
                        log = $"L3TAPPING_SLAG_ANA成功删除{delete}条记录\r\n";
                        idatabase.BulkInsert<L3TAPPING_SLAG_ANA>("L3TAPPING_SLAG_ANA", columnNames2, dataFiledNameArr2, SlagList);
                        log += $"L3TAPPING_SLAG_ANA成功插入{SlagList.Count}条记录\r\n";
                        idatabase.CommitTran();
                        Outputlog(log);
                    }

                }
                catch (Exception)
                {
                    idatabase.RollbackTran();
                    throw;
                }
                finally
                {
                    SlagList.Clear();
                    SlagList = null;
                }
            }
            #endregion
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }
        private void GetBaseData()
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                HopperModel = idatabase.GetDataTable("select * from MES_TMP_HOPPER_2 where record_time= (select max(record_time) from MES_TMP_HOPPER_2)");
                MespublicList = idatabase.Queryable<MES_PUBLIC_MATERIAL_2>().ToList();
                L3materialList = idatabase.Queryable<L3MATERIAL>().ToList();
            }
        }
        private List<L3MATERIAL> GetUseL3material()
        {
            List<L3MATERIAL> UsefulL3material = new List<L3MATERIAL>();
            if (HopperModel != null)
            {
                for (int i = 0; i < HopperModel.Columns.Count; i++)
                {
                    L3MATERIAL tempModel = L3materialList.Find(match => match.CODEL1 == HopperModel.Rows[0][i].ToString());
                    if (tempModel != null && !UsefulL3material.Contains(tempModel))
                    {
                        UsefulL3material.Add(tempModel);
                    }
                }
            }
            UsefulL3material.AddRange(L3materialList.FindAll(match => match.CODE >= 1 && match.CODE <= 9)); //除尘灰
            UsefulL3material.AddRange(L3materialList.FindAll(match => match.CODE >= 10 && match.CODE <= 19));//煤粉
            return UsefulL3material;
        }
        private List<MES_TQMIR21_IOOP> GetSampleNo(L3MATERIAL model, DateTime startTime, DateTime endTime)
        {
            List<MES_TQMIR21_IOOP> result = new List<MES_TQMIR21_IOOP>();
            List<MES_TQMIR21_IOOP> MES_TQMIR21_IOOPList = new List<MES_TQMIR21_IOOP>();
            if (model != null)
            {
                using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    if (model.CODE >= 10 && model.CODE <= 19)
                    {

                        double UMFFLAG = idatabase.GetDouble($"select a_umfflag from AV05M_PLC"); //大喷煤标志
                        if (UMFFLAG == 0) //如果是小喷煤
                        {
                            MES_TQMIR21_IOOPList = idatabase.GetList<MES_TQMIR21_IOOP>(
                         string.Format("select * from MES_TQMIR21_IOOP where MAT_PROD_CODE='{2}' " +
                         "and to_date(REC_REVISE_TIME,'yyyymmddhh24miss')>=to_date('{0}','yyyy/mm/dd hh24:mi:ss')" +
                         " and to_date(REC_REVISE_TIME,'yyyymmddhh24miss')<=to_date('{1}','yyyy/mm/dd hh24:mi:ss')" +
                         " and MAT_SAMPLE_NO  like 'MMF%' ", startTime, endTime, model.CODEL3)); //大喷煤UMF 小喷煤 MMF
                        }
                        else //如果是大喷煤
                        {
                            MES_TQMIR21_IOOPList = idatabase.GetList<MES_TQMIR21_IOOP>(
                         string.Format("select * from MES_TQMIR21_IOOP where MAT_PROD_CODE='{2}' " +
                         "and to_date(REC_REVISE_TIME,'yyyymmddhh24miss')>=to_date('{0}','yyyy/mm/dd hh24:mi:ss')" +
                         " and to_date(REC_REVISE_TIME,'yyyymmddhh24miss')<=to_date('{1}','yyyy/mm/dd hh24:mi:ss')" +
                         " and MAT_SAMPLE_NO  like 'UMF%' ", startTime, endTime, model.CODEL3)); //大喷煤UMF 小喷煤 MMF
                        }
                    }
                    else if (model.CODE == 1) //布袋
                    {
                        MES_TQMIR21_IOOPList = idatabase.GetList<MES_TQMIR21_IOOP>(
                            string.Format("select * from MES_TQMIR21_IOOP where MAT_PROD_CODE='{2}' " +
                            "and to_date(REC_REVISE_TIME,'yyyymmddhh24miss')>=to_date('{0}','yyyy/mm/dd hh24:mi:ss')" +
                            " and to_date(REC_REVISE_TIME,'yyyymmddhh24miss')<=to_date('{1}','yyyy/mm/dd hh24:mi:ss')" +
                            " and MAT_SAMPLE_NO  like 'KWH%20' ", startTime, endTime, model.CODEL3));
                    }
                    else if (model.CODE == 2) //重力
                    {
                        MES_TQMIR21_IOOPList = idatabase.GetList<MES_TQMIR21_IOOP>(
                            string.Format("select * from MES_TQMIR21_IOOP where MAT_PROD_CODE='{2}' " +
                            "and to_date(REC_REVISE_TIME,'yyyymmddhh24miss')>=to_date('{0}','yyyy/mm/dd hh24:mi:ss')" +
                            " and to_date(REC_REVISE_TIME,'yyyymmddhh24miss')<=to_date('{1}','yyyy/mm/dd hh24:mi:ss')" +
                            " and MAT_SAMPLE_NO  like 'KWH%10' ", startTime, endTime, model.CODEL3));
                    }
                    else if (model.CODE >= 50 && model.CODE <= 59)  //烧结
                    {
                        MES_TQMIR21_IOOPList = idatabase.GetList<MES_TQMIR21_IOOP>(
                           string.Format("select * from MES_TQMIR21_IOOP where MAT_PROD_CODE='{2}' " +
                           "and to_date(REC_REVISE_TIME,'yyyymmddhh24miss')>=to_date('{0}','yyyy/mm/dd hh24:mi:ss')" +
                           " and to_date(REC_REVISE_TIME,'yyyymmddhh24miss')<=to_date('{1}','yyyy/mm/dd hh24:mi:ss')" +
                           " and MAT_SAMPLE_NO  like 'RSJ%'", startTime, endTime, model.CODEL3));
                    }
                    else
                    {
                        MES_TQMIR21_IOOPList = idatabase.GetList<MES_TQMIR21_IOOP>(
                            string.Format("select * from MES_TQMIR21_IOOP where MAT_PROD_CODE='{2}' " +
                            "and to_date(REC_REVISE_TIME,'yyyymmddhh24miss')>=to_date('{0}','yyyy/mm/dd hh24:mi:ss')" +
                            " and to_date(REC_REVISE_TIME,'yyyymmddhh24miss')<=to_date('{1}','yyyy/mm/dd hh24:mi:ss')" +
                            " and MAT_SAMPLE_NO not like 'FSJ%' and MAT_SAMPLE_NO not like 'HSJ%'", startTime, endTime, model.CODEL3));
                    }
                }
            }
            MES_TQMIR21_IOOPList = MES_TQMIR21_IOOPList.OrderByDescending(match => match.REC_REVISE_TIME).ToList();
            foreach (MES_TQMIR21_IOOP item in MES_TQMIR21_IOOPList)
            {
                MES_TQMIR21_IOOP m = result.Find(match => match.MAT_SAMPLE_NO == item.MAT_SAMPLE_NO);
                if (m == null)
                {
                    result.Add(item);
                }
            }
            return result;
        }
        private List<MES_TQMIR23_IOOP> GetSampleValue(MES_TQMIR21_IOOP model, int Flag)
        {
            List<MES_TQMIR23_IOOP> Result = new List<MES_TQMIR23_IOOP>();
            if (model != null)
            {
                List<MES_TQMIR23_IOOP> MES_TQMIR23_IOOPList = new List<MES_TQMIR23_IOOP>();
                if (model != null)
                {
                    using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
                    {
                        MES_TQMIR23_IOOPList = idatabase.GetList<MES_TQMIR23_IOOP>(string.Format("select SEQUENCE_NO,ANALYSE_ITEM_CODE,ANALYSIS_VALUE from MES_TQMIR23_IOOP where MAT_SAMPLE_NO='{0}'", model.MAT_SAMPLE_NO));
                    }
                }
                //如果处理检化验成分
                if (Flag == 0)
                {
                    if (MES_TQMIR23_IOOPList.Count > 0)
                    {
                        MES_TQMIR23_IOOPList = MES_TQMIR23_IOOPList.OrderByDescending(match => match.SEQUENCE_NO).ToList();
                        foreach (MES_TQMIR23_IOOP item in MES_TQMIR23_IOOPList)
                        {
                            if (Result.Find(m => m.ANALYSE_ITEM_CODE == item.ANALYSE_ITEM_CODE) == null)
                            {
                                Result.Add(item);
                            }

                        }
                    }
                }
                else
                {
                    Result = MES_TQMIR23_IOOPList;
                }

            }
            return Result;
        }
        private void GetModelValue<T>(List<MES_TQMIR23_IOOP> Listmodel, Dictionary<string, string> Dic, ref T Model)
        {

            Type t = typeof(T);
            if (Listmodel.Count > 0)
            {

                foreach (MES_TQMIR23_IOOP item in Listmodel)
                {
                    if (!Dic.ContainsKey(item.ANALYSE_ITEM_CODE)) //如果字典中不包含检化验项
                    {
                        if (!s.Contains(item.ANALYSE_ITEM_CODE))
                        {
                            s.Add(item.ANALYSE_ITEM_CODE);
                            ss.Append(item.ANALYSE_ITEM_CODE + ",");
                            Log.WriteLine(ss.ToString()); //将未包含的部分写入日志
                        }
                    }
                    else
                    {
                        if (t.GetProperty(Dic[item.ANALYSE_ITEM_CODE]) != null)
                        {
                            t.GetProperty(Dic[item.ANALYSE_ITEM_CODE]).SetValue(Model, item.ANALYSIS_VALUE, null);
                        }
                    }

                }
            }
        }
        //修改同一个料种一个时间点两个样的问题，将时间加一秒
        private List<L3ANALYSI> DealRepeatData(List<L3ANALYSI> ListModel)
        {
            List<L3ANALYSI> listtemp = new List<L3ANALYSI>();
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

        /// <summary>
        /// 读取铁水与炉渣
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>       
        private void GetSampleNo(DateTime startTime, DateTime endTime)
        {
            List<MES_TQMIR21_IOOP> MES_TQMIR21_IOOPList = new List<MES_TQMIR21_IOOP>();
            string sql = "";
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {

                sql = string.Format("select * from ( " +
                               " select * from MES_TQMIR21_IOOP t where mat_sample_no like 'T71%E_1' or mat_sample_no like 'T72%E_1' union" +
                              " select * from MES_TQMIR21_IOOP t where mat_sample_no like 'T71%F_1' or mat_sample_no like 'T72%F_1' union" +
                               " select * from MES_TQMIR21_IOOP t where mat_sample_no like 'T71%D_1' or mat_sample_no like 'T72%D_1' union" +
                               " select * from MES_TQMIR21_IOOP t where mat_sample_no like 'T71%L01' or mat_sample_no like 'T72%L01' )" +
                           " where to_date(REC_REVISE_TIME,'yyyymmddhh24miss')>= to_date('{0}', 'yyyy/mm/dd hh24:mi:ss') and to_date(REC_REVISE_TIME,'yyyymmddhh24miss')<= to_date('{1}', 'yyyy/mm/dd hh24:mi:ss')", startTime, endTime);


                MES_TQMIR21_IOOPList = idatabase.GetList<MES_TQMIR21_IOOP>(sql);

                List<MES_TQMIR21_IOOP> IronL01Sample = MES_TQMIR21_IOOPList.FindAll(m => m.MAT_SAMPLE_NO.Contains("L01"));

                MES_TQMIR21_IOOPList.RemoveAll(m => m.MAT_SAMPLE_NO.Contains("L01"));

                if (IronL01Sample != null && IronL01Sample.Count > 0)
                {
                    foreach (MES_TQMIR21_IOOP item in IronL01Sample)
                    {
                        int index = MES_TQMIR21_IOOPList.FindIndex(m => m.MAT_SAMPLE_NO.Contains(item.MAT_SAMPLE_NO.Substring(0, 8))); //查找是否存在铁次号，如果不存在，插入L01的样
                        if (index < 0)
                        {
                            MES_TQMIR21_IOOPList.Add(item);
                        }
                    }
                }

                if (IronSlag_MES_TQMIR21_IOOP.ContainsKey("C60600100100002"))
                {
                    IronSlag_MES_TQMIR21_IOOP.Remove("C60600100100002");
                }
                IronSlag_MES_TQMIR21_IOOP.Add("C60600100100002", MES_TQMIR21_IOOPList);

                sql = string.Format("select * from MES_TQMIR21_IOOP where to_date(REC_REVISE_TIME,'yyyymmddhh24miss')>=to_date('{0}','yyyy/mm/dd hh24:mi:ss') and to_date(REC_REVISE_TIME,'yyyymmddhh24miss')<=to_date('{1}','yyyy/mm/dd hh24:mi:ss')   and MAT_PROD_CODE='C60600100300002A'  and MAT_SAMPLE_NO like 'T7%'", startTime, endTime);
                if (IronSlag_MES_TQMIR21_IOOP.ContainsKey("C60600100300002A"))
                {
                    IronSlag_MES_TQMIR21_IOOP.Remove("C60600100300002A");
                }
                MES_TQMIR21_IOOPList = idatabase.GetList<MES_TQMIR21_IOOP>(sql);
                IronSlag_MES_TQMIR21_IOOP.Add("C60600100300002A", MES_TQMIR21_IOOPList);
            }
        }

       
    }
}
