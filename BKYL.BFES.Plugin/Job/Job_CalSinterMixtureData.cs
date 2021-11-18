using BKYL.Jobs.Base;
using BFES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BFES.DataAccess;
using BKYL.Jobs.Base.Utils;
using BKYL.BFES.Plugin.Models;
using System.Data;

namespace BKYL.BFES.Plugin
{
    class Job_CalSinterMixtureData : BaseJob
    {
        int iCalDays;
        
        public override void InitTask()
        {
            iCalDays = 7;
        }

        public override void RunTask(DateTime currentTime)
        {
            DateTime currentClassBeginTime = GetCurentClassStartTime(currentTime);
            DateTime StartCalTime = currentClassBeginTime.AddDays(-iCalDays);
            for (DateTime CalTime = StartCalTime; CalTime <= currentClassBeginTime; CalTime = CalTime.AddHours(12))
            {
                CalcularOneClassData(CalTime);
            }
        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }


        public  DateTime GetCurentClassStartTime(DateTime currentTime)
        {
            DateTime dtTemp = currentTime.AddHours(-8);
            if (dtTemp.Minute == 0 && dtTemp.Second == 0 && (dtTemp.Hour == 0 || dtTemp.Hour == 12))
            {
                return currentTime.AddHours(-12);
            }
            else
            {
                return dtTemp.AddHours(-(dtTemp.Hour % 12) + 8).AddMinutes(-dtTemp.Minute).AddSeconds(-dtTemp.Second);
            }
           
        }
        public void CalcularOneClassData(DateTime endTime)
        {
            int iIndex = 0;
            int iStartIndex = 0;
            SINTER_MIXTURE_DATA smd;
            List <SINTER_MIXTURE_DATA> L_SINTER_MIXTURE_DATA = new List<SINTER_MIXTURE_DATA>();
            List<BATCHHIS_DUMP> L_BATCHHIS_DUMP = getBatchhisDumpData(endTime);
            Dictionary<int, double> dic_code_weight = DealBatchhisDumpData(L_BATCHHIS_DUMP);
            List<L3MATERIAL> L_L3MATERIAL = getL3Material();
            List<L3MATERIAL_CONFIG> L_L3MATERIAL_CONFIG = GetL3MaterialConfig();
            MATERIAL_BALANCE_CLASS MaterialBalanceClass = GetMaterialBalanceClassData(endTime);
            List<MATERIAL_BALANCE_BAT> L_MATERIAL_BALANCE_BAT = GetMaterialBalanceBat(endTime);
            List<AV05M> L_AV05M = GetAV05M(endTime);
            double dblWeightFKL = getJZFromMES_TYMIRD3_IOOP(endTime);
            List<L3TAPPING_IRON_ANA> L_L3TAPPING_IRON_ANA = GetL3TappingIronAna(endTime);
            List<L3TAPPING_SLAG_ANA> L_L3TAPPING_SLAG_ANA = GetL3TappingSlagAna(endTime);
            if (MaterialBalanceClass == null)
            {
                return;
            }
            //理论班产
            double LLBC = MaterialBalanceClass.I_MHMT_OUT;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime,1,"理论班产",LLBC));
            //焦比
            double cokeRadio = MaterialBalanceClass.I_COKE_RATIO;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 2, "焦比", cokeRadio));
            //煤比
            double coalRadio = MaterialBalanceClass.I_COAL_RATIO;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 3, "煤比", coalRadio));
            //焦丁比
            double nutCokeRadio = MaterialBalanceClass.I_NUTCOKE_RATIO;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 4, "焦丁比", nutCokeRadio));

            //燃料比
            double fuelRadio = cokeRadio + coalRadio + nutCokeRadio;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 5, "燃料比", fuelRadio));
            //理论渣量
            double slagWeight = MaterialBalanceClass.I_MSLAG_OUT;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 6, "理论渣量", slagWeight));
            //综合入炉品位
            double tFeInBF = MaterialBalanceClass.I_ORE_IN > 0 ? MaterialBalanceClass.I_MHMT_OUT * 0.945 / MaterialBalanceClass.I_ORE_IN * 100 : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 7, "综合入炉品位", tFeInBF));

            //K2O入炉负荷
            double k2oburden = MaterialBalanceClass.I_MHMT_OUT > 0 ?
                L_MATERIAL_BALANCE_BAT.Sum(m => m.K2O_CRUDEORE + m.K2O_COAL + m.K2O_COKE + m.K2O_PELLET + m.K2O_SINTER) / MaterialBalanceClass.I_MHMT_OUT
                : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 8, "K2O入炉负荷", k2oburden));
            //Na2O入炉负荷
            double na2oburden = MaterialBalanceClass.I_MHMT_OUT > 0 ?
                L_MATERIAL_BALANCE_BAT.Sum(m => m.NA2O_CRUDEORE + m.NA2O_COAL + m.NA2O_COKE + m.NA2O_PELLET + m.NA2O_SINTER) / MaterialBalanceClass.I_MHMT_OUT
                : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 9, "Na2O入炉负荷", na2oburden));
            //Zn入炉负荷
            double ZnOburden = MaterialBalanceClass.I_MHMT_OUT > 0 ?
                L_MATERIAL_BALANCE_BAT.Sum(m => m.ZNO_CRUDEORE + m.ZNO_COAL + m.ZNO_COKE + m.ZNO_PELLET + m.ZNO_SINTER) / MaterialBalanceClass.I_MHMT_OUT: 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 10, "Zn入炉负荷", ZnOburden));

            //p入炉负荷
            double Pburden = MaterialBalanceClass.I_MHMT_OUT > 0 ?
                L_MATERIAL_BALANCE_BAT.Sum(m => m.I_P_ORE) / MaterialBalanceClass.I_MHMT_OUT : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 11, "p入炉负荷", Pburden));
            //Mn入炉负荷
            double Mnburden = MaterialBalanceClass.I_MHMT_OUT > 0 ?
                L_MATERIAL_BALANCE_BAT.Sum(m => m.I_MNO_ORE) / MaterialBalanceClass.I_MHMT_OUT : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 12, "Mn入炉负荷", Mnburden));
            //Ti入炉负荷
            double Tiburden = MaterialBalanceClass.I_MHMT_OUT > 0 ?
                L_MATERIAL_BALANCE_BAT.Sum(m => m.I_TIO2_ORE) / MaterialBalanceClass.I_MHMT_OUT : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 13, "Ti入炉负荷", Tiburden));
            //碱金属入炉负荷
            double bmburden = MaterialBalanceClass.I_MHMT_OUT > 0 ?
                L_MATERIAL_BALANCE_BAT.Sum(m => m.NA2O_CRUDEORE + m.NA2O_COAL + m.NA2O_COKE + m.NA2O_PELLET + m.NA2O_SINTER + 
                m.K2O_CRUDEORE + m.K2O_COAL + m.K2O_COKE + m.K2O_PELLET + m.K2O_SINTER) / MaterialBalanceClass.I_MHMT_OUT : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 14, "碱金属入炉负荷", bmburden));

            //槽下返矿率
            double reorerate = MaterialBalanceClass.I_ORE_IN > 0 ? dblWeightFKL / MaterialBalanceClass.I_ORE_IN * 100 : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 25, "槽下返矿率", reorerate));

            //风温
            double bt = L_AV05M.Where(m => m.A_BT5M > 0).Count() > 0 ? L_AV05M.Where(m=>m.A_BT5M>0).Average(m=>m.A_BT5M):0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 16, "风温", bt));
            //风量
            double bv = L_AV05M.Where(m => m.A_BV5M > 0).Count() > 0 ? L_AV05M.Where(m => m.A_BV5M > 0).Average(m => m.A_BV5M) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 17, "风量", bv));
            //富氧
            double bvo = L_AV05M.Where(m => m.A_BVO > 0).Count() > 0 ? L_AV05M.Where(m => m.A_BVO > 0).Average(m => m.A_BVO) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 18, "富氧", bvo));
            //煤气利用率
            double a_etaco = L_AV05M.Where(m => m.A_ETACO > 0).Count() > 0 ? L_AV05M.Where(m => m.A_ETACO > 0).Average(m => m.A_ETACO) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 19, "煤气利用率", a_etaco));
            //顶温
            double ttop = L_AV05M.Where(m => m.A_BP5M > 0).Count() > 0 ? L_AV05M.Where(m => m.A_BP5M > 0).Average(m => m.A_BP5M) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 20, "风压", ttop));
            //顶压
            double tp = L_AV05M.Where(m => m.A_TP5M > 0).Count() > 0 ? L_AV05M.Where(m => m.A_TP5M > 0).Average(m => m.A_TP5M) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 21, "顶压", tp));
            //透气性指数
            double i_K = L_AV05M.Where(m => (m.A_BP5M - m.A_TP5M) > 0).Count() > 0 ? (bv / (L_AV05M.Where(m => (m.A_BP5M - m.A_TP5M) > 0).Average(m => m.A_BP5M - m.A_TP5M))) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 22, "透气性指数", i_K));

          


            //炉渣成分
            double SLAG_SiO2 = L_L3TAPPING_SLAG_ANA.Where(m => m.SIO2 > 0).Count() > 0 ? L_L3TAPPING_SLAG_ANA.Where(m => m.SIO2 > 0).Average(m => (double)m.SIO2) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 50, "SiO2", SLAG_SiO2,"高炉炉渣成分"));
            double  SLAG_CaO = L_L3TAPPING_SLAG_ANA.Where(m => m.CAO > 0).Count() > 0 ? L_L3TAPPING_SLAG_ANA.Where(m => m.CAO > 0).Average(m => (double)m.CAO) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 51, "CaO", SLAG_CaO, "高炉炉渣成分"));
            double SLAG_MgO = L_L3TAPPING_SLAG_ANA.Where(m => m.MGO > 0).Count() > 0 ? L_L3TAPPING_SLAG_ANA.Where(m => m.MGO > 0).Average(m => (double)m.MGO) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 52, "MgO", SLAG_MgO, "高炉炉渣成分"));
            double SLAG_Al2O3 = L_L3TAPPING_SLAG_ANA.Where(m => m.AL2O3 > 0).Count() > 0 ? L_L3TAPPING_SLAG_ANA.Where(m => m.AL2O3 > 0).Average(m => (double)m.AL2O3) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 53, "Al2O3", SLAG_Al2O3, "高炉炉渣成分"));
            double SLAG_R2 = L_L3TAPPING_SLAG_ANA.Where(m => m.R2 > 0).Count() > 0 ? L_L3TAPPING_SLAG_ANA.Where(m => m.R2 > 0).Average(m => (double)m.R2) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 54, "R2", SLAG_R2, "高炉炉渣成分"));
            double SLAG_S = L_L3TAPPING_SLAG_ANA.Where(m => m.S > 0).Count() > 0 ? L_L3TAPPING_SLAG_ANA.Where(m => m.S > 0).Average(m => (double)m.S) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 55, "S", SLAG_S, "高炉炉渣成分"));
            double SLAG_K2O = L_L3TAPPING_SLAG_ANA.Where(m => m.K2O > 0).Count() > 0 ? L_L3TAPPING_SLAG_ANA.Where(m => m.K2O > 0).Average(m => (double)m.K2O) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 56, "K2O", SLAG_K2O, "高炉炉渣成分"));
            double SLAG_Na2O = L_L3TAPPING_SLAG_ANA.Where(m => m.NA2O > 0).Count() > 0 ? L_L3TAPPING_SLAG_ANA.Where(m => m.NA2O > 0).Average(m => (double)m.NA2O) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 57, "Na2O", SLAG_Na2O, "高炉炉渣成分"));
            double SLAG_MnO = L_L3TAPPING_SLAG_ANA.Where(m => m.MNO > 0).Count() > 0 ? L_L3TAPPING_SLAG_ANA.Where(m => m.MNO > 0).Average(m => (double)m.MNO) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 58, "MnO", SLAG_MnO, "高炉炉渣成分"));
            double SLAG_TiO2 = L_L3TAPPING_SLAG_ANA.Where(m => m.TIO2 > 0).Count() > 0 ? L_L3TAPPING_SLAG_ANA.Where(m => m.TIO2 > 0).Average(m => (double)m.TIO2) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 59, "TiO2", SLAG_TiO2, "高炉炉渣成分"));
            double SLAG_FeO = L_L3TAPPING_SLAG_ANA.Where(m => m.FEO > 0).Count() > 0 ? L_L3TAPPING_SLAG_ANA.Where(m => m.FEO > 0).Average(m => (double)m.FEO) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 60, "FeO", SLAG_FeO, "高炉炉渣成分"));

            //铁水成分
            double Iron_Si = L_L3TAPPING_IRON_ANA.Where(m => m.SI > 0).Count() > 0 ? L_L3TAPPING_IRON_ANA.Where(m => m.SI > 0).Average(m => (double)m.SI) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 70, "Si", Iron_Si, "高炉铁水成分"));
            double Iron_Mn = L_L3TAPPING_IRON_ANA.Where(m => m.MN > 0).Count() > 0 ? L_L3TAPPING_IRON_ANA.Where(m => m.MN > 0).Average(m => (double)m.MN) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 71, "Mn", Iron_Mn, "高炉铁水成分"));
            double Iron_Cu = L_L3TAPPING_IRON_ANA.Where(m => m.CU > 0).Count() > 0 ? L_L3TAPPING_IRON_ANA.Where(m => m.CU > 0).Average(m => (double)m.CU ) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 72, "Cu", Iron_Cu, "高炉铁水成分"));
            double Iron_Ti = L_L3TAPPING_IRON_ANA.Where(m => m.TI > 0).Count() > 0 ? L_L3TAPPING_IRON_ANA.Where(m => m.TI > 0).Average(m => (double)m.TI ) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 73, "Ti", Iron_Ti, "高炉铁水成分"));
            double Iron_Ni = L_L3TAPPING_IRON_ANA.Where(m => m.NI > 0).Count() > 0 ? L_L3TAPPING_IRON_ANA.Where(m => m.NI > 0).Average(m => (double)m.NI) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 74, "Ni", Iron_Ni, "高炉铁水成分"));
            double Iron_P = L_L3TAPPING_IRON_ANA.Where(m => m.P > 0).Count() > 0 ? L_L3TAPPING_IRON_ANA.Where(m => m.P > 0).Average(m => (double)m.P) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 75, "P", Iron_P, "高炉铁水成分"));
            double Iron_S = L_L3TAPPING_IRON_ANA.Where(m => m.S > 0).Count() > 0 ? L_L3TAPPING_IRON_ANA.Where(m => m.S > 0).Average(m => (double)m.S) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 76, "S", Iron_S, "高炉铁水成分"));
            double Iron_As = L_L3TAPPING_IRON_ANA.Where(m => m.A_S > 0).Count() > 0 ? L_L3TAPPING_IRON_ANA.Where(m => m.A_S > 0).Average(m => (double)m.A_S) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 77, "As", Iron_As, "高炉铁水成分"));
            double Iron_Cr = L_L3TAPPING_IRON_ANA.Where(m => m.CR > 0).Count() > 0 ? L_L3TAPPING_IRON_ANA.Where(m => m.CR > 0).Average(m => (double)m.CR) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 78, "Cr", Iron_Cr, "高炉铁水成分"));
            double Iron_CO = L_L3TAPPING_IRON_ANA.Where(m => m.CO > 0).Count() > 0 ? L_L3TAPPING_IRON_ANA.Where(m => m.CO > 0).Average(m => (double)m.CO ) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 79, "CO", Iron_CO, "高炉铁水成分"));
            double Iron_Pb = L_L3TAPPING_IRON_ANA.Where(m => m.PB > 0).Count() > 0 ? L_L3TAPPING_IRON_ANA.Where(m => m.PB > 0).Average(m => (double)m.PB ) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 80, "Pb", Iron_Pb, "高炉铁水成分"));
            double Iron_Sn = L_L3TAPPING_IRON_ANA.Where(m => m.SN > 0).Count() > 0 ? L_L3TAPPING_IRON_ANA.Where(m => m.SN > 0).Average(m => (double)m.SN ) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 81, "Sn", Iron_Sn, "高炉铁水成分"));
            double Iron_C = L_L3TAPPING_IRON_ANA.Where(m => m.C > 0).Count() > 0 ? L_L3TAPPING_IRON_ANA.Where(m => m.C > 0).Average(m => (double)m.C) : 0;
            L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime, 82, "C", Iron_C, "高炉铁水成分"));

           


            //焦炭
            L3MATERIAL_CONFIG L3MaterialConfig_JT = L_L3MATERIAL_CONFIG.FindLast(m => m.M_TYPE == 5);
            Dictionary<int, double> dic_code_weight_JT = dic_code_weight.Where
                (m => m.Key >= L3MaterialConfig_JT.CODE_MIN && m.Key <= L3MaterialConfig_JT.CODE_MAX && m.Value > 0).
                ToDictionary(p => p.Key, o => o.Value);
            double sumWeight_JT = dic_code_weight_JT.Sum(m=>m.Value);
            dic_code_weight_JT = dic_code_weight_JT.OrderBy(m => m.Key).ToDictionary(p => p.Key, o => o.Value);
            iIndex = 0;
            iStartIndex = 100;
            foreach (var item in dic_code_weight_JT)
            {
                L_SINTER_MIXTURE_DATA.Add(new SINTER_MIXTURE_DATA(endTime,
                    iStartIndex + iIndex, 
                    $"焦炭{iIndex + 1}比例",
                    item.Value / sumWeight_JT * 100, 
                    L_L3MATERIAL.FindLast(m => m.CODE == item.Key).NAME));
                iIndex++;
            }


            //烧结
            double sumWeight_SJ = 0;//烧结总重
            L3MATERIAL_CONFIG L3MaterialConfig_SJ = L_L3MATERIAL_CONFIG.FindLast(m => m.M_TYPE == 6);
            Dictionary<int, double> dic_code_weight_SJ = dic_code_weight.Where
                (m => m.Key >= L3MaterialConfig_SJ.CODE_MIN && m.Key <= L3MaterialConfig_SJ.CODE_MAX && m.Value > 0).
                ToDictionary(p => p.Key, o => o.Value);
            sumWeight_SJ = dic_code_weight_SJ.Sum(m => m.Value);
            dic_code_weight_SJ = dic_code_weight_SJ.OrderBy(m => m.Key).ToDictionary(p => p.Key, o => o.Value);
            //球团
            double sumWeight_QT= 0;//球团总重
            L3MATERIAL_CONFIG L3MaterialConfig_QT = L_L3MATERIAL_CONFIG.FindLast(m => m.M_TYPE == 7);
            Dictionary<int, double> dic_code_weight_QT = dic_code_weight.Where
                (m => m.Key >= L3MaterialConfig_QT.CODE_MIN && m.Key <= L3MaterialConfig_QT.CODE_MAX && m.Value > 0).
                ToDictionary(p => p.Key, o => o.Value);
            sumWeight_QT = dic_code_weight_QT.Sum(m => m.Value);
            dic_code_weight_QT = dic_code_weight_QT.OrderBy(m => m.Key).ToDictionary(p => p.Key, o => o.Value);
            //块矿
            double sumWeight_KK = 0;//块矿总重
            L3MATERIAL_CONFIG L3MaterialConfig_KK = L_L3MATERIAL_CONFIG.FindLast(m => m.M_TYPE == 8);
            Dictionary<int, double> dic_code_weight_KK = dic_code_weight.Where
                (m => m.Key >= L3MaterialConfig_KK.CODE_MIN && m.Key <= L3MaterialConfig_KK.CODE_MAX && m.Value > 0).
                ToDictionary(p => p.Key, o => o.Value);
            sumWeight_KK = dic_code_weight_KK.Sum(m => m.Value);
            dic_code_weight_KK = dic_code_weight_KK.OrderBy(m => m.Key).ToDictionary(p => p.Key, o => o.Value);


            double sumWeight_Ore = sumWeight_SJ + sumWeight_QT + sumWeight_KK; //矿石总重
            //烧结比例
            iIndex = 0;
            iStartIndex = 110;

            smd = new SINTER_MIXTURE_DATA();
            smd.TIMESTAMP = endTime;
            smd.S_ID = iStartIndex + iIndex;
            smd.S_NAME = $"烧结比例";
            smd.S_VALUE = sumWeight_SJ / sumWeight_Ore * 100;
            L_SINTER_MIXTURE_DATA.Add(smd);
            foreach (var item in dic_code_weight_SJ)
            {
                iIndex++;
                smd = new SINTER_MIXTURE_DATA();
                smd.TIMESTAMP = endTime;
                double weight = item.Value;
                smd.S_ID = iStartIndex + iIndex;
                smd.S_NAME = $"烧结{iIndex}比例";
                smd.S_VALUE = weight / sumWeight_Ore * 100;
                smd.S_DESC = L_L3MATERIAL.FindLast(m => m.CODE == item.Key).NAME;
                L_SINTER_MIXTURE_DATA.Add(smd);
            }

            //球团比例

            iIndex = 0;
            iStartIndex = 120;

            smd = new SINTER_MIXTURE_DATA();
            smd.TIMESTAMP = endTime;
            smd.S_ID = iStartIndex + iIndex;
            smd.S_NAME = $"球团比例";
            smd.S_VALUE = sumWeight_QT / sumWeight_Ore * 100;
            L_SINTER_MIXTURE_DATA.Add(smd);
            foreach (var item in dic_code_weight_QT)
            {
                iIndex++;
                smd = new SINTER_MIXTURE_DATA();
                smd.TIMESTAMP = endTime;
                double weight = item.Value;
                smd.S_ID = iStartIndex + iIndex;
                smd.S_NAME = $"球团{iIndex }比例";
                smd.S_VALUE = weight / sumWeight_Ore * 100;
                smd.S_DESC = L_L3MATERIAL.FindLast(m => m.CODE == item.Key).NAME;
                L_SINTER_MIXTURE_DATA.Add(smd);
            }

            //块矿比例
            iIndex = 0;
            iStartIndex = 130;

            smd = new SINTER_MIXTURE_DATA();
            smd.TIMESTAMP = endTime;
            smd.S_ID = iStartIndex + iIndex;
            smd.S_NAME = $"块矿比例";
            smd.S_VALUE = sumWeight_KK / sumWeight_Ore * 100;
            L_SINTER_MIXTURE_DATA.Add(smd);
           
            foreach (var item in dic_code_weight_KK)
            {
                iIndex++;
                smd = new SINTER_MIXTURE_DATA();
                smd.TIMESTAMP = endTime;
                double weight = item.Value;
                smd.S_ID = iStartIndex + iIndex;
                smd.S_NAME = $"块矿{iIndex }比例";
                smd.S_VALUE = weight / sumWeight_Ore * 100;
                smd.S_DESC = L_L3MATERIAL.FindLast(m => m.CODE == item.Key).NAME;
                L_SINTER_MIXTURE_DATA.Add(smd);
            }

            SaveDateToDB(L_SINTER_MIXTURE_DATA);
        }

        public List<L3TAPPING_IRON_ANA> GetL3TappingIronAna(DateTime endTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                string strSql = $"select * from L3TAPPING_IRON_ANA where  " +
                    $"timestamp >{DataBaseFactory.ConvertTimeString(endTime.AddHours(-12), DataBaseType.Oracle)}" +
                    $" and timestamp <= {DataBaseFactory.ConvertTimeString(endTime, DataBaseType.Oracle)}";
                return idatabase.GetList<L3TAPPING_IRON_ANA>(strSql);
            }
        }
        public List<L3TAPPING_SLAG_ANA> GetL3TappingSlagAna(DateTime endTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                string strSql = $"select * from L3TAPPING_SLAG_ANA where  " +
                    $"timestamp >{DataBaseFactory.ConvertTimeString(endTime.AddHours(-12), DataBaseType.Oracle)}" +
                    $" and timestamp <= {DataBaseFactory.ConvertTimeString(endTime, DataBaseType.Oracle)}";
                return idatabase.GetList<L3TAPPING_SLAG_ANA>(strSql);
            }
        }
        public List<AV05M> GetAV05M(DateTime endTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                string strSql = $"select * from AV05M where  " +
                    $"timestamp >{DataBaseFactory.ConvertTimeString(endTime.AddHours(-12), DataBaseType.Oracle)}" +
                    $" and timestamp <= {DataBaseFactory.ConvertTimeString(endTime, DataBaseType.Oracle)}";
                return idatabase.GetList<AV05M>(strSql);
            }
        }
        public List<MATERIAL_BALANCE_BAT> GetMaterialBalanceBat (DateTime endTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                string strSql = $"select * from MATERIAL_BALANCE_BAT where  "+
                    $"timestamp >{DataBaseFactory.ConvertTimeString(endTime.AddHours(-12), DataBaseType.Oracle)}"+
                    $" and timestamp <= {DataBaseFactory.ConvertTimeString(endTime, DataBaseType.Oracle)}";
                return idatabase.GetList<MATERIAL_BALANCE_BAT>(strSql);
            }
        }

        MATERIAL_BALANCE_CLASS GetMaterialBalanceClassData(DateTime endTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                string strSql = $"select * from MATERIAL_BALANCE_CLASS where timestamp = {DataBaseFactory.ConvertTimeString(endTime, DataBaseType.Oracle)}";
                return idatabase.GetSingle<MATERIAL_BALANCE_CLASS>(strSql);
            }
        }


        public double getJZFromMES_TYMIRD3_IOOP(DateTime endTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                return idatabase.GetDouble(string.Format("select sum(net_wt) from MES_TYMIRD3_IOOP "+
                    "where to_date(rec_create_time,'yyyy-mm-dd hh24:mi:ss') > {0} and to_date(rec_create_time,'yyyy-mm-dd hh24:mi:ss') <= {1} and  "+
                    " supply_dept_code = 6220 and mat_prod_code = '4724'",
               DataBaseFactory.ConvertTimeString(endTime.AddHours(-12), DataBaseType.Oracle),
               DataBaseFactory.ConvertTimeString(endTime, DataBaseType.Oracle)));
            }
        }
        public List<L3MATERIAL> getL3Material()
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                return idatabase.GetList<L3MATERIAL>("select * from L3MATERIAL");
            }
        }
        public List<L3MATERIAL_CONFIG> GetL3MaterialConfig()
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                return idatabase.GetList<L3MATERIAL_CONFIG>("select * from L3MATERIAL_CONFIG");
            }
        }
        public List<BATCHHIS_DUMP> getBatchhisDumpData(DateTime endTime)
        {
            using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
            {
                string strSql = string.Format("select * from BATCHHIS_DUMP where timestamp > {0} and timestamp <= {1}",
               DataBaseFactory.ConvertTimeString(endTime.AddHours(-12), DataBaseType.Oracle),
               DataBaseFactory.ConvertTimeString(endTime, DataBaseType.Oracle));
                return idatabase.GetList<BATCHHIS_DUMP>(strSql);
            }
        }
        public Dictionary<int ,double> DealBatchhisDumpData(List<BATCHHIS_DUMP> L_BATCHHIS_DUMP)
        {
            Dictionary<int, double> dic_code_weight = new Dictionary<int, double>();
            foreach (BATCHHIS_DUMP batchhisDump in L_BATCHHIS_DUMP)
            {
                if (dic_code_weight.ContainsKey(batchhisDump.CODE1)) { dic_code_weight[batchhisDump.CODE1] += batchhisDump.WEIGHT1 * (1 - batchhisDump.WATER1 / 100); } else { dic_code_weight.Add(batchhisDump.CODE1, batchhisDump.WEIGHT1 * (1 - batchhisDump.WATER1 / 100)); }
                if (dic_code_weight.ContainsKey(batchhisDump.CODE2)) { dic_code_weight[batchhisDump.CODE2] += batchhisDump.WEIGHT2 * (1 - batchhisDump.WATER2 / 100); } else { dic_code_weight.Add(batchhisDump.CODE2, batchhisDump.WEIGHT2 * (1 - batchhisDump.WATER2 / 100)); }
                if (dic_code_weight.ContainsKey(batchhisDump.CODE3)) { dic_code_weight[batchhisDump.CODE3] += batchhisDump.WEIGHT3 * (1 - batchhisDump.WATER3 / 100); } else { dic_code_weight.Add(batchhisDump.CODE3, batchhisDump.WEIGHT3 * (1 - batchhisDump.WATER3 / 100)); }
                if (dic_code_weight.ContainsKey(batchhisDump.CODE4)) { dic_code_weight[batchhisDump.CODE4] += batchhisDump.WEIGHT4 * (1 - batchhisDump.WATER4 / 100); } else { dic_code_weight.Add(batchhisDump.CODE4, batchhisDump.WEIGHT4 * (1 - batchhisDump.WATER4 / 100)); }
                if (dic_code_weight.ContainsKey(batchhisDump.CODE5)) { dic_code_weight[batchhisDump.CODE5] += batchhisDump.WEIGHT5 * (1 - batchhisDump.WATER5 / 100); } else { dic_code_weight.Add(batchhisDump.CODE5, batchhisDump.WEIGHT5 * (1 - batchhisDump.WATER5 / 100)); }
                if (dic_code_weight.ContainsKey(batchhisDump.CODE6)) { dic_code_weight[batchhisDump.CODE6] += batchhisDump.WEIGHT6 * (1 - batchhisDump.WATER6 / 100); } else { dic_code_weight.Add(batchhisDump.CODE6, batchhisDump.WEIGHT6 * (1 - batchhisDump.WATER6 / 100)); }
                if (dic_code_weight.ContainsKey(batchhisDump.CODE7)) { dic_code_weight[batchhisDump.CODE7] += batchhisDump.WEIGHT7 * (1 - batchhisDump.WATER7 / 100); } else { dic_code_weight.Add(batchhisDump.CODE7, batchhisDump.WEIGHT7 * (1 - batchhisDump.WATER7 / 100)); }
                if (dic_code_weight.ContainsKey(batchhisDump.CODE8)) { dic_code_weight[batchhisDump.CODE8] += batchhisDump.WEIGHT8 * (1 - batchhisDump.WATER8 / 100); } else { dic_code_weight.Add(batchhisDump.CODE8, batchhisDump.WEIGHT8 * (1 - batchhisDump.WATER8 / 100)); }
                if (dic_code_weight.ContainsKey(batchhisDump.CODE9)) { dic_code_weight[batchhisDump.CODE9] += batchhisDump.WEIGHT9 * (1 - batchhisDump.WATER9 / 100); } else { dic_code_weight.Add(batchhisDump.CODE9, batchhisDump.WEIGHT9 * (1 - batchhisDump.WATER9 / 100)); }
                if (dic_code_weight.ContainsKey(batchhisDump.CODE10)) { dic_code_weight[batchhisDump.CODE10] += batchhisDump.WEIGHT10 * (1 - batchhisDump.WATER10 / 100); } else { dic_code_weight.Add(batchhisDump.CODE10, batchhisDump.WEIGHT10 * (1 - batchhisDump.WATER10 / 100)); }
                if (dic_code_weight.ContainsKey(batchhisDump.CODE11)) { dic_code_weight[batchhisDump.CODE11] += batchhisDump.WEIGHT11 * (1 - batchhisDump.WATER11 / 100); } else { dic_code_weight.Add(batchhisDump.CODE11, batchhisDump.WEIGHT11 * (1 - batchhisDump.WATER11 / 100)); }
                if (dic_code_weight.ContainsKey(batchhisDump.CODE12)) { dic_code_weight[batchhisDump.CODE12] += batchhisDump.WEIGHT12 * (1 - batchhisDump.WATER12 / 100); } else { dic_code_weight.Add(batchhisDump.CODE12, batchhisDump.WEIGHT12 * (1 - batchhisDump.WATER12 / 100)); }
                if (dic_code_weight.ContainsKey(batchhisDump.CODE13)) { dic_code_weight[batchhisDump.CODE13] += batchhisDump.WEIGHT13 * (1 - batchhisDump.WATER13 / 100); } else { dic_code_weight.Add(batchhisDump.CODE13, batchhisDump.WEIGHT13 * (1 - batchhisDump.WATER13 / 100)); }
                if (dic_code_weight.ContainsKey(batchhisDump.CODE14)) { dic_code_weight[batchhisDump.CODE14] += batchhisDump.WEIGHT14 * (1 - batchhisDump.WATER14 / 100); } else { dic_code_weight.Add(batchhisDump.CODE14, batchhisDump.WEIGHT14 * (1 - batchhisDump.WATER14 / 100)); }
                if (dic_code_weight.ContainsKey(batchhisDump.CODE15)) { dic_code_weight[batchhisDump.CODE15] += batchhisDump.WEIGHT15 * (1 - batchhisDump.WATER15 / 100); } else { dic_code_weight.Add(batchhisDump.CODE15, batchhisDump.WEIGHT15 * (1 - batchhisDump.WATER15 / 100)); }
                if (dic_code_weight.ContainsKey(batchhisDump.CODE16)) { dic_code_weight[batchhisDump.CODE16] += batchhisDump.WEIGHT16 * (1 - batchhisDump.WATER16 / 100); } else { dic_code_weight.Add(batchhisDump.CODE16, batchhisDump.WEIGHT16 * (1 - batchhisDump.WATER16 / 100)); }
                if (dic_code_weight.ContainsKey(batchhisDump.CODE17)) { dic_code_weight[batchhisDump.CODE17] += batchhisDump.WEIGHT17 * (1 - batchhisDump.WATER17 / 100); } else { dic_code_weight.Add(batchhisDump.CODE17, batchhisDump.WEIGHT17 * (1 - batchhisDump.WATER17 / 100)); }
            }
            return dic_code_weight;
        }
        public void SaveDateToDB(List<SINTER_MIXTURE_DATA> L_SMD)
        {
            if (L_SMD.Count > 0)
            {
                using (IDataBase idatabase = DalFactory.GreateIDataBase(S_DBSource))
                {
                    try
                    {
                        string deleteSQL = $"delete from SINTER_MIXTURE_DATA where timestamp = {DataBaseFactory.ConvertTimeString(L_SMD[0].TIMESTAMP, DataBaseType.Oracle)}";
                                                 
                        idatabase.BeginTran();
                        idatabase.ExecuteCommand(deleteSQL);
                        idatabase.InsertRange(L_SMD);
                        idatabase.CommitTran();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        idatabase.RollbackTran();
                    }
                }
            }
        }
    }
    public   class CMaterialData
    {
       public  int index { get; set; }
       public  double weight { get; set; }
       public  double radio { get; set; }
       public  string name { get; set; }
    }
}
