
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BFES.DataAccess;

using System.Data;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using BKYL.Sinter.Plugin.Models;

namespace BKYL.Sinter.Plugin
{/// <summary>
/// 李海森
/// </summary>
    public class Con_bfes : BaseJob
    {

        public override void InitTask()
        {
            //加载配置 MC_POPCAL_PAR
        }

        public override void RunTask(DateTime currentTime)
        {
            string log = "";
            try
            {
                //源数据库 表
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    // 数据库查询

                }

                //目标数据库 表
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    List<M_MES_SINTER> M_MES_SINTER_ = GET_M_MES_SINTER(iDataBase);
                    MES_TQMIR23_IOOP(M_MES_SINTER_, iDataBase);
                }

                Outputlog(log);
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                //释放资源
                // log = null;
            }
        }
        public List<M_MES_SINTER> GET_M_MES_SINTER(IDataBase iDataBase)
        {
            string str_sql = string.Format("select  a.l2_code AS l2_CODE ,a.l2_name  AS l2_NAME,b.mat_prod_code AS MAT_PROD_CODE ,b.mat_sample_no AS MAT_SAMPLE_NO,b.MAT_SAMPLE_TIME as MAT_SAMPLE_TIME ,b.SEQUENCE_NO as SEQUENCE_NO  from M_MATERIAL_COOD a, MES_TQMIR21_IOOP b where a.l3_code=b.mat_prod_code and b.flag3='0'  order by b.SEQUENCE_NO");

            return iDataBase.GetList<M_MES_SINTER>(str_sql);
        }
        public void MES_TQMIR23_IOOP(List<M_MES_SINTER> M_MES_SINTER_, IDataBase iDataBase)
        {

            List<MES_TQMIR23_IOOP> MES_TQMIR23_IOOP_ = new List<Models.MES_TQMIR23_IOOP>();
            if (M_MES_SINTER_.Count > 0)
            {
                for (int i = 0; i < M_MES_SINTER_.Count; i++)
                {
                    MES_TQMIR23_IOOP_.Clear();
                    MES_TQMIR23_IOOP_ = iDataBase.Queryable<MES_TQMIR23_IOOP>().Where(M => M.MAT_SAMPLE_NO == M_MES_SINTER_[i].MAT_SAMPLE_NO  && M.SEQUENCE_NO== M_MES_SINTER_[i].SEQUENCE_NO).ToList();
                    if (MES_TQMIR23_IOOP_.Count > 0)
                    {
                        set_save(MES_TQMIR23_IOOP_, M_MES_SINTER_[i].l2_CODE, M_MES_SINTER_[i].l2_NAME, M_MES_SINTER_[i].MAT_PROD_CODE, M_MES_SINTER_[i].MAT_SAMPLE_TIME, M_MES_SINTER_[i].SEQUENCE_NO);

                    }
                }
            }

        }
        public void set_save(List<MES_TQMIR23_IOOP> qita, int l2_CODE, string l2_NAME, string MAT_PROD_CODE, string MAT_SAMPLE_TIME, int SEQUENCE_NO)
        {
            string log = "";
            MD_L3_L2_ANA_IN_ORIG MD_L3_L2_ANA_IN_ORIG_ = null;
            MD_L3_L2_ANA_IN_ORIG_ = new MD_L3_L2_ANA_IN_ORIG();


            for (int i = 0; i < qita.Count; i++)
            {
                MD_L3_L2_ANA_IN_ORIG_.TIMESTAMP = DateTime.Now;
                MD_L3_L2_ANA_IN_ORIG_.REPORTTIME = DateTime.ParseExact(qita[0].REC_REVISE_TIME, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                MD_L3_L2_ANA_IN_ORIG_.SAMPLETIME = DateTime.ParseExact(MAT_SAMPLE_TIME, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);

                MD_L3_L2_ANA_IN_ORIG_.SAMPLE_CODE = qita[0].MAT_SAMPLE_NO;
                MD_L3_L2_ANA_IN_ORIG_.L2_CODE = l2_CODE;
                MD_L3_L2_ANA_IN_ORIG_.NAME_CODE = MAT_PROD_CODE;
                MD_L3_L2_ANA_IN_ORIG_.MATERIAL_NAME = l2_NAME;
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "TFE")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_TFE = qita[i].ANALYSIS_VALUE;
                }

                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "FEO")
                {
                    if (l2_CODE == 701)
                    {
                        log = string.Format(qita[0].MAT_SAMPLE_NO + "---" + SEQUENCE_NO.ToString() + "----" + qita[i].ANALYSIS_VALUE.ToString());
                        Log.WriteLine(log);
                    }
                    MD_L3_L2_ANA_IN_ORIG_.C_FEO = qita[i].ANALYSIS_VALUE;
                }

                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "CAO")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_CAO = qita[i].ANALYSIS_VALUE;
                }

                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "SIO2")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_SIO2 = qita[i].ANALYSIS_VALUE;
                }

                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "AL2O3")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_AL2O3 = qita[i].ANALYSIS_VALUE;
                }

                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "MGO")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_MGO = qita[i].ANALYSIS_VALUE;
                }

                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S" || qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "STD")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_S = qita[i].ANALYSIS_VALUE;
                }

                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "P")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_P = qita[i].ANALYSIS_VALUE;
                }

                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "C" || qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "FCAD" || qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "FC")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_C = qita[i].ANALYSIS_VALUE;
                }

                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "MN")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_MN = qita[i].ANALYSIS_VALUE;
                }

                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "LOI")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_LOT = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "R" || qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "R2" || qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "CAO_SIO2")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_R = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "H2O")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_H2O = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "AD")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_ASH = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "VD")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_VOLATILES = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "TTO2")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_TIO2 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "K2O" || qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "K20")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_K2O = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "NA2O")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_NA2O = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "PBO")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_PBO = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "ZNO")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_ZNO = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "TI")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_TI = qita[i].ANALYSIS_VALUE;
                }

                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "AS")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_AS = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "CU")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_CU = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "PB")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_PB = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "ZN")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_ZN = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "K")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_K = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "NA")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_NA = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "CR")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_CR = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "NI")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_NI = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "MNO")
                {
                    MD_L3_L2_ANA_IN_ORIG_.C_MNO = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "TI6Z3")
                {
                    MD_L3_L2_ANA_IN_ORIG_.TI6Z3 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "DI")
                {
                    MD_L3_L2_ANA_IN_ORIG_.DI = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "KMZS0Z5")
                {
                    MD_L3_L2_ANA_IN_ORIG_.SCRENING = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "RDI_0Z5")
                {
                    MD_L3_L2_ANA_IN_ORIG_.RDI_0Z5 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "RDI_0Z5")
                {
                    MD_L3_L2_ANA_IN_ORIG_.RDI_0Z5 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "RDI_3Z15" || qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "RDIZ3Z15")
                {
                    MD_L3_L2_ANA_IN_ORIG_.RDI_3Z15 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "RDIZ6Z3")
                {
                    MD_L3_L2_ANA_IN_ORIG_.RDI_Z6Z3 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S_5")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_5 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S10_5")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_10_5 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S16_10")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_16_10 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S25_16")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_25_16 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S40_25")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_40_25 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "SZ40")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_Z40 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S_3")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_3 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "SZ3" || qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "ADE3")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_Z3 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S_1")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_LOW_1 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S1_3" || qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "ADE2")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_1_3 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S_20")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_LOW_20 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S20_90")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_20_90 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "SZ90")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_UP_90 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "SMZ180")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_M_UP_180 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "SM_200")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_M_180_200 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "SMZ200")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_M_LOW_200 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S10_30")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_10_30 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S_0Z04")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_0Z04 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S_0Z075")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_0Z075 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S_0Z15")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_0Z15 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S_0Z5")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_0Z5 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S_1")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_0Z25_1 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "SZ6Z3" || qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S_6Z3")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_6Z3 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S_10")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_10 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S20_10")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_20_10 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S25_10" || qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "SZ10")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_25_10 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S25_20")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_25_20 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S25_31.5")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_UP_25 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S30_25")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_30_25 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "SZ30")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_Z30 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S0_15")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_0_15 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "SZ15")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_UP_15 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S_30")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_LOW_30 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S_25")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_LOW_25 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "SZ40")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_UP_40 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S40_60")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_40_60 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "SZ60")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_UP_60 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "M10")
                {
                    MD_L3_L2_ANA_IN_ORIG_.M10 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "M25")
                {
                    MD_L3_L2_ANA_IN_ORIG_.M25 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "M40")
                {
                    MD_L3_L2_ANA_IN_ORIG_.M40 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "MAD" || qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "MAR")
                {
                    MD_L3_L2_ANA_IN_ORIG_.MAD = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "CRI")
                {
                    MD_L3_L2_ANA_IN_ORIG_.CRI = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "CSR")
                {
                    MD_L3_L2_ANA_IN_ORIG_.CSR = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "GWR")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GWR = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "HEATQTY" || qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "HEATQTY-1" || qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "HEATQTY-2")
                {
                    MD_L3_L2_ANA_IN_ORIG_.HEATQTY = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "HEATQTY" || qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "HEATQTY-1" || qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "HEATQTY-2")
                {
                    MD_L3_L2_ANA_IN_ORIG_.HEATQTY = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "CCS" || qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "KYN")
                {
                    MD_L3_L2_ANA_IN_ORIG_.CCS = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "RI")
                {
                    MD_L3_L2_ANA_IN_ORIG_.RI = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "RSI")
                {
                    MD_L3_L2_ANA_IN_ORIG_.RSI = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "RUANHKS")
                {
                    MD_L3_L2_ANA_IN_ORIG_.RUANHKS = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "RUANHZL")
                {
                    MD_L3_L2_ANA_IN_ORIG_.RUANHZL = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "RUANHQJ")
                {
                    MD_L3_L2_ANA_IN_ORIG_.RUANHQJ = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "RONGHKS")
                {
                    MD_L3_L2_ANA_IN_ORIG_.RONGHKS = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "RONGHZL")
                {
                    MD_L3_L2_ANA_IN_ORIG_.RONGHZL = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "RONGHQJ")
                {
                    MD_L3_L2_ANA_IN_ORIG_.RONGHQJ = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "YC")
                {
                    MD_L3_L2_ANA_IN_ORIG_.YC = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "RRCHD")
                {
                    MD_L3_L2_ANA_IN_ORIG_.RRCHD = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "RDZS")
                {
                    MD_L3_L2_ANA_IN_ORIG_.RDZS = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "DI_0Z5")
                {
                    MD_L3_L2_ANA_IN_ORIG_.DI_0Z5 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "DI_3Z15")
                {
                    MD_L3_L2_ANA_IN_ORIG_.DI_3Z15 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "DI_6Z3")
                {
                    MD_L3_L2_ANA_IN_ORIG_.DI_6Z3 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "GRIT_10_6Z3")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_10_6Z3 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "SZ31Z5")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_UP_31Z5 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "SZ25")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_31Z5_25 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S_6Z3")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_LOW_6Z3 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S16_10" || qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "SZ10")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_UP_10 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S20_6Z3")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_20_6Z3 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S8_5")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_8_5 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "S16_8")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_16_8 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "SZ5")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_UP_5 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "AOL")
                {
                    MD_L3_L2_ANA_IN_ORIG_.AOL = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "SZ16")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_UP_16 = qita[i].ANALYSIS_VALUE;
                }
                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == ">1mm")
                {
                    MD_L3_L2_ANA_IN_ORIG_.GRIT_UP_1 = qita[i].ANALYSIS_VALUE;
                }

                if (qita[i].ANALYSE_ITEM_CODE.ToUpper().Trim() == "CAOZMGO")
                {
                    MD_L3_L2_ANA_IN_ORIG_.CAOZMGO = qita[i].ANALYSIS_VALUE;
                }
                // Set_Admin_add(MD_L3_L2_ANA_IN_ORIG_);
                Get_update1(qita[0].MAT_SAMPLE_NO, qita[i].ANALYSE_ITEM_CODE);
            }
            Set_Admin_add(MD_L3_L2_ANA_IN_ORIG_);
            Get_update(qita[0].MAT_SAMPLE_NO, SEQUENCE_NO);
        }

        public bool Set_Admin_add(MD_L3_L2_ANA_IN_ORIG model)
        {
            bool listmodel = true;

            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                listmodel = (bool)iDataBase.Insert(model);
                return listmodel;
            }
        }

        public bool Get_update(string ziduan, int SEQUENCE_NO)
        {
            bool listmodel = true;
            string sql = "";

            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                try
                {
                    iDataBase.BeginTran();
                    sql = string.Format("update MES_TQMIR21_IOOP SET flag3='1' where  MAT_SAMPLE_NO='{0}'and SEQUENCE_NO='{1}'", ziduan, SEQUENCE_NO);
                    BFES.DataAccess.SqlMapper.Execute(iDataBase.GetConnection(), sql, "");
                    iDataBase.CommitTran();
                    return listmodel = true;
                }
                catch
                {
                    iDataBase.RollbackTran();
                    return listmodel = false;
                }
            }
        }

        public bool Get_update1(string ziduan, string ANALYSE_ITEM_CODE)
        {
            bool listmodel = true;
            string sql = "";

            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                try
                {
                    iDataBase.BeginTran();
                    sql = string.Format("update MES_TQMIR23_IOOP SET flag3='1' where  MAT_SAMPLE_NO='{0}'and ANALYSE_ITEM_CODE='{1}'", ziduan, ANALYSE_ITEM_CODE);
                    BFES.DataAccess.SqlMapper.Execute(iDataBase.GetConnection(), sql, "");
                    iDataBase.CommitTran();
                    return listmodel = true;
                }
                catch
                {
                    iDataBase.RollbackTran();
                    return listmodel = false;
                }
            }
        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }
    }
}
