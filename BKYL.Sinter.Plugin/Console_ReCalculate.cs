using BFES.DataAccess;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using BKYL.Sinter.Plugin;
using BKYL.Sinter.Plugin.Models;
using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin
{
    /// <summary>
    /// 粒度分析透气性计算 赵飞
    /// </summary>
    public class Console_ReCalculate : BaseJob
    {
        /// <summary>
        /// 定义一个全局的透气性表的最大时间
        /// </summary>
        private DateTime MaxTime_BREATH { get; set; }
        public override void InitTask()
        {
            MaxTime_BREATH = BREATH_RESULT();
        }

        public override void RunTask(DateTime currentTime)
        {
            // DataAdd(currentTime);
            DateTime Max_PICAL_1M = PICAL_1M_result();
            //如果来源表的最大时间比透气表的来源时间大，说明有新的数据插入,计算透气相关性
            if (Max_PICAL_1M > MaxTime_BREATH)
            {

                //计算并插入
                SqlInsert(Max_PICAL_1M);
                //把来源表的最大时间赋值给透气表的timestamp
                MaxTime_BREATH = Max_PICAL_1M;
            }

            else
            {
            }
        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }
        /// <summary>
        /// 数据加工
        /// </summary>
        //private void DataAdd(DateTime Max_PICAL_1M)
        //{
        //    try
        //    {

        //        using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
        //        {
        //            DataTable MC_MICAL_PAR = iDataBase.GetDataTable(@"select 
        //                *    from MC_MICAL_PAR");
        //            //二混采样最大时间
        //            DateTime _t4 = iDataBase.GetDateTime("select max(PICAL_2M_TIME) PICAL_2M_TIME from M_PICAL_2M_result");
        //            DataTable M_PICAL_par = iDataBase.GetDataTable(@"select PAR_K1,PAR_K2,PAR_n,PAR_T2,PAR_T1,PAR_B from M_PICAL_par");

        //            DataTable MC_MICAL_RESULT = iDataBase.GetDataTable("select MICAL_CLO_2M_TIME from MC_MICAL_RESULT  where 1=1 and datanum=3 and MICAL_BLEND_2M_TIME<=" + DataBaseFactory.ConvertTimeString(_t4, T_DBSource.DBType));
        //            DataTable _MC_MICAL_RESULT = iDataBase.GetDataTable("select MICAL_1M_MAT_TIME,MICAL_2M_1M_TIME from MC_MICAL_RESULT where 1=1 and datanum=2 and timestamp<=" + DataBaseFactory.ConvertTimeString(_t4, T_DBSource.DBType) + " order by timestamp desc");

        //            double CLO_TIME = MC_MICAL_PAR.Rows[0]["PAR_CLOTH_U_TIME"].ToDouble();
        //            double MICAL_1M_MAT_TIME = _MC_MICAL_RESULT.Rows[0]["MICAL_1M_MAT_TIME"].ToDouble();
        //            //double t4 = SIN_TIME + CLO_TIME + Ls1 / Vs1 + Lz21 / Vz21;


        //            double CLO_2M_TIME = MC_MICAL_RESULT.Rows[0]["MICAL_CLO_2M_TIME"].ToDouble();
        //            double PAR_T2 = M_PICAL_par.Rows[0]["PAR_T2"].ToDouble();
        //            double _2M_TIME = MC_MICAL_PAR.Rows[0]["PAR_2M_U_TIME"].ToDouble();

        //            //二混采样时间向后正推时间t9，采集值：料厚、点火温度（平均）、
        //            //点火段温度1、点火段温度2、点火段温度3、台车速度（平均）、垂直烧结速度（平均）、铺底料厚度                     
        //            double t9 = CLO_TIME + CLO_2M_TIME - _2M_TIME;
        //            //正推时间t9
        //            DateTime BeginTime = _t4.AddMinutes(t9);
        //            DateTime EndTime = BeginTime.AddMinutes(PAR_T2);
        //            //布料厚度
        //            BeginTime = iDataBase.GetDateTime("select min(timestamp) TimeStamp from T_CLOTH_PLC_1MIN where TimeStamp>=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            EndTime = BeginTime.AddMinutes(PAR_T2);
        //            DataTable T_CLOTH_PLC_1MIN = iDataBase.GetDataTable(@"select avg(C_THICK_PV) C_THICK_PV,avg(C_BED_MATERAL_TH) C_BED_MATERAL_TH from T_CLOTH_PLC_1MIN where timestamp>=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) + " and timestamp<=" + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
        //            double C_THICK_PV = T_CLOTH_PLC_1MIN.Rows[0]["C_THICK_PV"].ToDouble();
        //            //铺底料厚度
        //            double PICAL_BREATH_BED_TH = T_CLOTH_PLC_1MIN.Rows[0]["C_BED_MATERAL_TH"].ToDouble();
        //            //点火温度 1 2 3
        //            BeginTime = _t4.AddMinutes(t9);
        //            BeginTime = iDataBase.GetDateTime("select min(TIMESTAMP)TIMESTAMP from T_BURN_COLE_PLC_1MIN where timestamp>=" + DataBaseFactory.ConvertTimeString(_t4, T_DBSource.DBType));
        //            EndTime = BeginTime.AddMinutes(PAR_T2);
        //            DataTable T_BURN_COLE_PLC_1MIN = iDataBase.GetDataTable(@"select  round(avg(BC_AIM_TE),3) BC_AIM_TE,
        //                                                                      round(avg(BC_IG_01_TE),3) BC_IG_01_TE,
        //                                                                      round(avg(BC_IG_02_TE),3) BC_IG_02_TE,
        //                                                                      round(avg(BC_IG_03_TE),3) BC_IG_03_TE,
        //                                                                      round(avg(BC_SIN_MS_PV),3) BC_SIN_MS_PV
        //                                                                       from T_BURN_COLE_PLC_1MIN where timestamp between "
        //                                                                          + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) +
        //                                                                          " and " + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
        //            double PICAL_BREATH_AIM_TE = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_AIM_TE"].ToDouble();
        //            double PICAL_BREATH_IG_01_TE = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_IG_01_TE"].ToDouble();
        //            double PICAL_BREATH_IG_02_TE = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_IG_02_TE"].ToDouble();
        //            double PICAL_BREATH_IG_03_TE = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_IG_03_TE"].ToDouble();
        //            double PICAL_BREATH_SIN_MS_PV = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_SIN_MS_PV"].ToDouble();
        //            //垂直烧结速度
        //            BeginTime = _t4.AddMinutes(t9);
        //            BeginTime = iDataBase.GetDateTime("select min(TIMESTAMP) from MC_BTPCAL_result_1min where timestamp>=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            EndTime = BeginTime.AddMinutes(PAR_T2);
        //            DataTable MC_BTPCAL_result_1min = iDataBase.GetDataTable(@"select avg(BTPCAL_V) BTPCAL_V from MC_BTPCAL_result_1min where timestamp>="
        //                                                                       + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) +
        //                                                                      " and timestamp<=" + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
        //            double PICAL_VER_SIN_SP = MC_BTPCAL_result_1min.Rows[0]["BTPCAL_V"].ToDouble();

        //            //风量负压 主抽温度
        //            BeginTime = _t4.AddMinutes(t9);
        //            BeginTime = iDataBase.GetDateTime("select min(TimeStamp) from T_MA_PGD_PLC_1MIN where timestamp>=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            EndTime = BeginTime.AddMinutes(PAR_T2);
        //            string sql = @"select avg(PS_MA_SB_1_FLUE_FT) PS_MA_SB_1_FLUE_FT,--p1
        //                               avg(PS_MA_SB_2_FLUE_FT) PS_MA_SB_2_FLUE_FT,--p2
        //                               avg(PS_MA_SB_1_FLUE_PT) PS_MA_SB_1_FLUE_PT,--q1
        //                               avg(PS_MA_SB_2_FLUE_PT) PS_MA_SB_2_FLUE_PT,--q2
        //                                          avg(PS_MA_SB_1_FLUE_TE)PS_MA_SB_1_FLUE_TE,--主抽1温度
        //                                          avg(PS_MA_SB_2_FLUE_TE)PS_MA_SB_2_FLUE_TE--主抽2温度
        //                            from T_MA_PGD_PLC_1MIN where timestamp<=" + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType) + " and timestamp>=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType);
        //            DataTable T_MA_PGD_PLC_1MIN = iDataBase.GetDataTable(sql);
        //            //
        //            double PS_MA_SB_1_FLUE_TE = T_MA_PGD_PLC_1MIN.Rows[0]["PS_MA_SB_1_FLUE_FT"].ToDouble();
        //            double PS_MA_SB_2_FLUE_TE = T_MA_PGD_PLC_1MIN.Rows[0]["PS_MA_SB_2_FLUE_TE"].ToDouble();
        //            double PICAL_BREATH_MA_TE = (PS_MA_SB_1_FLUE_TE + PS_MA_SB_2_FLUE_TE) / 2;
        //            //主抽1烧结大烟道烟气流量
        //            double Q1 = T_MA_PGD_PLC_1MIN.Rows[0]["PS_MA_SB_1_FLUE_FT"].ToDouble();
        //            //主抽2烧结大烟道烟气流量
        //            double Q2 = T_MA_PGD_PLC_1MIN.Rows[0]["PS_MA_SB_2_FLUE_FT"].ToDouble();
        //            double Q = Q1 + Q2;
        //            //主抽1烧结大烟道压力(大烟道压力左)取平均
        //            double P1 = T_MA_PGD_PLC_1MIN.Rows[0]["PS_MA_SB_1_FLUE_PT"].ToDouble();
        //            //主抽2烧结大烟道压力(大烟道压力右)取平均
        //            double P2 = T_MA_PGD_PLC_1MIN.Rows[0]["PS_MA_SB_2_FLUE_PT"].ToDouble();
        //            double P = Math.Abs(P1 + P2);
        //            double K1 = M_PICAL_par.Rows[0]["PAR_K1"].ToDouble();
        //            double K2 = M_PICAL_par.Rows[0]["PAR_K2"].ToDouble();
        //            double H = T_CLOTH_PLC_1MIN.Rows[0]["C_THICK_PV"].ToDouble();
        //            double B = M_PICAL_par.Rows[0]["PAR_B"].ToDouble();
        //            double L = MC_MICAL_PAR.Rows[0]["PAR_LENGTH_SIN"].ToDouble();
        //            double A = B * L;
        //            double n = M_PICAL_par.Rows[0]["PAR_n"].ToDouble();
        //            double JPU = Q * (1 - K1) / (K2 * A) * (Math.Pow(H / P, n));
        //            if (JPU == double.NaN) { JPU = 1; }
        //            // 在二混采样时间采集：二混粒度（＜1mm、1-3mm、＞3mm）、二混后水分、二混后温度
        //            BeginTime = _t4;
        //            BeginTime = iDataBase.GetDateTime("select min(timestamp)timestamp from  T_BLEND_PLC_1MIN where timestamp>=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            EndTime = BeginTime.AddMinutes(PAR_T2);
        //            DataTable _T_BLEND_PLC_1MIN = iDataBase.GetDataTable(@"select  avg(B_2M_FLOW_PV) B_2M_FLOW_PV ,  
        //                                                                 avg(B_1M_FT_PV) B_1M_FT_PV,
        //                                                                 avg(B_2M_MIXER_RATE) B_2M_MIXER_RATE,
        //                                                                 avg(B_2M_NEX_WATER_AVG) B_2M_NEX_WATER_AVG
        //                                                                 from T_BLEND_PLC_1MIN WHERE
        //                                                                 timestamp<=" + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType) +
        //                                                                 " and timestamp>=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            DataTable MD_PHY_PARTICLE_INFO_IN = iDataBase.GetDataTable(@"select 
        //                   round(avg(GRIT_LOW_1),3) GRIT_LOW_1,round(avg(MATERIAL_TE),3) MATERIAL_TE,
        //                    round(avg(GRIT_1_3),3) GRIT_1_3,round(avg(GRIT_UP_3),3)GRIT_UP_3,3 from MD_PHY_PARTICLE_INFO_IN where L2_CODE between 900 and 999 and
        //                    timestamp between " + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType) +
        //                    " and " + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            //二混水分
        //            double B_2M_NEX_WATER_AVG = _T_BLEND_PLC_1MIN.Rows[0]["B_2M_NEX_WATER_AVG"].ToDouble();
        //            //<1
        //            double PICAL_BREATH_2M_GRIT_1 = MD_PHY_PARTICLE_INFO_IN.Rows[0]["GRIT_LOW_1"].ToDouble();
        //            //1-3
        //            double PICAL_BREATH_2M_GRIT_1_3 = MD_PHY_PARTICLE_INFO_IN.Rows[0]["GRIT_1_3"].ToDouble();
        //            //>3
        //            double PICAL_BREATH_2M_GRIT_3 = MD_PHY_PARTICLE_INFO_IN.Rows[0]["GRIT_UP_3"].ToDouble();
        //            //平均粒度
        //            double PICAL_BREATH_2M_GRIT_AVG = PICAL_BREATH_2M_GRIT_1 * 1 + PICAL_BREATH_2M_GRIT_1_3 * 1.5 + PICAL_BREATH_2M_GRIT_3 * 3;
        //            //温度
        //            double PICAL_BREATH_BLE_TE = MD_PHY_PARTICLE_INFO_IN.Rows[0]["MATERIAL_TE"].ToDouble();

        //            double t5 = _2M_TIME;
        //            //二混采样时间往前倒推2M_TIME ，采集当时：二混填充率、二混加水、二混转速
        //            BeginTime = _t4.AddMinutes(-_2M_TIME);//采样时间
        //            BeginTime = iDataBase.GetDateTime("select max(TimeStamp) TimeStamp from T_BLEND_PLC_1MIN where TimeStamp<=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            EndTime = BeginTime.AddMinutes(-PAR_T2);
        //            string twoMix = string.Format(@"select 
        //                                    avg(B_2M_FLOW_PV) B_2M_FLOW_PV, 
        //                                    avg(B_2M_MIXER_RATE)B_2M_MIXER_RATE
        //                                    from T_BLEND_PLC_1MIN where timestamp between {0} and {1}",
        //                                DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType),
        //                                DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            T_BLEND_PLC_1MIN t_BLEND_PLC_1MIN = iDataBase.GetList<T_BLEND_PLC_1MIN>(twoMix).FirstOrDefault();
        //            double PICAL_BREATH_2M_FT_PV = _T_BLEND_PLC_1MIN.Rows[0]["B_2M_FLOW_PV"].ToDouble();//二混加水量
        //            double PICAL_BREATH_2M_MIX_RATE = _T_BLEND_PLC_1MIN.Rows[0]["B_2M_MIXER_RATE"].ToDouble();//二混转速

        //            double PICAL_2M_FILL_RATE = iDataBase.GetDataTable("select avg(PICAL_2M_FILL_RATE) PICAL_2M_FILL_RATE  from M_PICAL_2M_result where timestamp between " + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType) + " and " + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType)).Rows[0]["PICAL_2M_FILL_RATE"].ToDouble();



        //            //t6 往前倒推时间t6到一混后，采集当时：一混粒度（＜1mm、1-3mm、＞3mm）、一混后水分 
        //            double _1M_TIME = MC_MICAL_PAR.Rows[0]["PAR_1M_U_TIME"].ToDouble();
        //            double _2M_1M_TIME = _MC_MICAL_RESULT.Rows[0]["MICAL_2M_1M_TIME"].ToDouble();//---
        //            double t6 = _2M_TIME + _2M_1M_TIME - _1M_TIME;
        //            BeginTime = _t4.AddMinutes(-t6);
        //            //获取比倒推时间点小的最大值
        //            BeginTime = iDataBase.GetDateTime("select max(TimeStamp) from MC_SINCAL_INTERFACE_TRIG_MIN where TimeStamp <=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            EndTime = BeginTime.AddMinutes(-PAR_T2);
        //            string _GRIT = string.Format("select * from MC_SINCAL_INTERFACE_TRIG_MIN where sincal_cood between 400 and 499 and TIMESTAMP Between {0} and {1}",
        //                DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType), DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            List<MC_SINCAL_INTERFACE_TRIG_MIN> GRIT = iDataBase.GetList<MC_SINCAL_INTERFACE_TRIG_MIN>(_GRIT);
        //            GRIT = GRIT.Where((x, i) => GRIT.FindIndex(z => z.SINCAL_ID == x.SINCAL_ID) == i).ToList();
        //            //一混粒度 <1  1-3 >3
        //            BeginTime = _t4.AddMinutes(-t6);
        //            BeginTime = iDataBase.GetDateTime("select max(timestamp) timestamp  from MD_PHY_PARTICLE_INFO_IN where timestamp<=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            EndTime = BeginTime.AddMinutes(-PAR_T2);
        //            string OneMix = string.Format("select avg(GRIT_LOW_1) GRIT_LOW_1,avg(GRIT_1_3)GRIT_1_3,avg(GRIT_UP_3) GRIT_UP_3 from MD_PHY_PARTICLE_INFO_IN where L2_CODE between 800 and 899 and timestamp between {0} and {1}", DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType), DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            MD_PHY_PARTICLE_INFO_IN _mD_PHY_PARTICLE_INFO_IN = iDataBase.GetList<MD_PHY_PARTICLE_INFO_IN>(OneMix).FirstOrDefault();
        //            double? GRIT_1 = _mD_PHY_PARTICLE_INFO_IN.GRIT_LOW_1;
        //            double? GRIT_1_3 = _mD_PHY_PARTICLE_INFO_IN.GRIT_1_3;
        //            double? GRIT_3 = _mD_PHY_PARTICLE_INFO_IN.GRIT_UP_3;


        //            DataTable T_BLEND_PLC_1MIN_t6 = iDataBase.GetDataTable(@"select avg(B_1M_NEX_WATER_AVG) B_1M_NEX_WATER_AVG,
        //                                                                    avg(B_2M_NEX_WATER_AVG) B_2M_NEX_WATER_AVG
        //                                                                                    from T_BLEND_PLC_1MIN
        //                                                                    where timestamp <="
        //                                                                   + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType) +
        //                                                                   " and timestamp >= " + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            double PICAL_BREATH_1M_NEX_WAT = T_BLEND_PLC_1MIN_t6.Rows[0]["B_1M_NEX_WATER_AVG"].ToDouble();
        //            double PICAL_BREATH_2M_NEX_WAT = T_BLEND_PLC_1MIN_t6.Rows[0]["B_2M_NEX_WATER_AVG"].ToDouble();
        //            //二混采样时间往前倒推时间t7 到一混前，采集当时：一混填充率、一混加水、一混转速、
        //            double _1M_MAT_TIME = _MC_MICAL_RESULT.Rows[0]["MICAL_1M_MAT_TIME"].ToDouble();
        //            double t7 = _2M_TIME + _2M_1M_TIME;
        //            BeginTime = _t4.AddMinutes(-t7);
        //            BeginTime = iDataBase.GetDateTime("select Max(TimeStamp) from M_PICAL_1M_result where TimeStamp <=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            EndTime = BeginTime.AddMinutes(-PAR_T2);
        //            //一混填充
        //            DataTable M_PICAL_1M_result = iDataBase.GetDataTable(@"select avg(PICAL_1M_FILL_RATE) PICAL_1M_FILL_RATE 
        //                                                            from M_PICAL_1M_result where timestamp <=" + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType) +
        //                                                            " and timestamp >=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            double PICAL_1M_FILL_RATE = M_PICAL_1M_result.Rows[0]["PICAL_1M_FILL_RATE"].ToDouble();
        //            //透气性计算对应填充率
        //            double PICAL_BREATH_FILL_RATE = (PICAL_1M_FILL_RATE + PICAL_2M_FILL_RATE) / 2;

        //            //一混加水、一混转速    T_BLEND_PLC_1MIN表
        //            DataTable _T_BLEND_PLC_1MIN_t7 = iDataBase.GetDataTable("select round(avg(B_1M_FT_PV),3) B_1M_FT_PV,round(avg(B_1M_MIXER_RATE),3) B_1M_MIXER_RATE  from T_BLEND_PLC_1MIN where timestamp <=" + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType) + " and timestamp >=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            //一混水分
        //            double PICAL_BREATH_1M_FT_PV = _T_BLEND_PLC_1MIN_t7.Rows[0]["B_1M_FT_PV"].ToDouble();//一混加水量
        //            double PICAL_BREATH_1M_MIX_RATE = _T_BLEND_PLC_1MIN_t7.Rows[0]["B_1M_MIXER_RATE"].ToDouble();//一混转速


        //            //二混采样时间往前倒推时间t8（改为t3）到配料，采集当时：
        //            //溶剂配比1、溶剂配比2、溶剂配比3、精矿配比、烧返配比、焦炭配比、焦炭粒度、铁料粒度、MgO含量、碱度
        //            double t3 = _2M_TIME + _2M_1M_TIME + _1M_MAT_TIME;
        //            //二混采样时间往前倒推时间t3
        //            EndTime = _t4.AddMinutes(-t3);
        //            EndTime = iDataBase.GetDateTime("select Max(TimeStamp) TimeStamp from MC_SINCAL_INTERFACE_TRIG_MIN where TimeStamp<=" + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
        //            BeginTime = EndTime.AddMinutes(-PAR_T2);
        //            //溶剂配比
        //            string solvent = string.Format("select * from  MC_SINCAL_INTERFACE_TRIG_MIN where sincal_cood between 400 and 499   and timestamp between " + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) + " and " + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType) + " and SINCAL_ORE_TC=1 order by sincal_cood ");
        //            List<MC_SINCAL_INTERFACE_TRIG_MIN> l_MC_SINCAL_INTERFACE_TRIG_MIN = iDataBase.GetList<MC_SINCAL_INTERFACE_TRIG_MIN>(solvent);
        //            //for (int i = 0; i < l_MC_SINCAL_INTERFACE_TRIG_MIN.Count; i++)
        //            //{
        //            //    for (int j = 0; j < l_MC_SINCAL_INTERFACE_TRIG_MIN.Count; j++)
        //            //    {
        //            //        if (i != j)
        //            //        {
        //            //            if (l_MC_SINCAL_INTERFACE_TRIG_MIN[i].SINCAL_ID == l_MC_SINCAL_INTERFACE_TRIG_MIN[j].SINCAL_ID)
        //            //            {
        //            //                l_MC_SINCAL_INTERFACE_TRIG_MIN.RemoveAt(j);
        //            //            }
        //            //        }
        //            //    }
        //            //}
        //            //   去重
        //            l_MC_SINCAL_INTERFACE_TRIG_MIN = l_MC_SINCAL_INTERFACE_TRIG_MIN.Where((x, i) => l_MC_SINCAL_INTERFACE_TRIG_MIN.FindIndex(z => z.SINCAL_ID == x.SINCAL_ID) == i).ToList();
        //            IEnumerable<IGrouping<int?, MC_SINCAL_INTERFACE_TRIG_MIN>> query = l_MC_SINCAL_INTERFACE_TRIG_MIN.GroupBy(m => m.SINCAL_COOD);
        //            string result = string.Empty;
        //            foreach (IGrouping<int?, MC_SINCAL_INTERFACE_TRIG_MIN> info in query)
        //            {
        //                List<MC_SINCAL_INTERFACE_TRIG_MIN> sl = info.ToList();//分组后的集合 
        //                double? sum = sl.Sum(m => m.SINCAL_ORE_DRY_BILL);
        //                result += sum.ToString() + ",";
        //            }
        //            if (query.Count() == 2)
        //            {
        //                result += "0,";
        //            }
        //            if (query.Count() == 1)
        //            {
        //                result += "0,0,";
        //            }
        //            //溶剂配比1,2,3
        //            result = result.Substring(0, result.Length - 1);
        //            //混匀矿粒度

        //            MD_PHY_PARTICLE_INFO_IN mix = iDataBase.GetList<MD_PHY_PARTICLE_INFO_IN>(@"
        //               select avg(GRIT_LOW_1) GRIT_LOW_1,avg(GRIT_1_3) GRIT_1_3,avg(GRIT_UP_3) GRIT_UP_3 
        //               from MD_PHY_PARTICLE_INFO_IN where L2_CODE between 100 and 199  and timestamp between " + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) + " and " + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType)).FirstOrDefault();

        //            //精矿配比

        //            MD_BLEND_BATCH_LIST_IN l_MD_BLEND_BATCH_LIST_IN = iDataBase.GetList<MD_BLEND_BATCH_LIST_IN>(@"
        //                  select avg(DRY_BILL) DRY_BILL from MD_BLEND_BATCH_LIST_IN
        //                              where SAMPLE_CODE between 200 and 299 and TIMESTAMP between 
        //                   " + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) + " and " + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType)).FirstOrDefault();
        //            //烧返 600-699
        //            string fire = string.Format("select avg(SINCAL_ORE_DRY_BILL) SINCAL_ORE_DRY_BILL from  MC_SINCAL_INTERFACE_TRIG_MIN where sincal_cood between 600 and 699   and timestamp between " + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) + " and " + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType) + " and SINCAL_ORE_TC=1 order by sincal_cood ");
        //            List<MC_SINCAL_INTERFACE_TRIG_MIN> fire_MC_SINCAL_INTERFACE_TRIG_MIN = iDataBase.GetList<MC_SINCAL_INTERFACE_TRIG_MIN>(fire);
        //            double? PICAL_RE_BILL_DRY = fire_MC_SINCAL_INTERFACE_TRIG_MIN[0].SINCAL_ORE_DRY_BILL;
        //            //焦炭配比 也就是燃料配比
        //            string carbon = string.Format("select avg(SINCAL_ORE_DRY_BILL) SINCAL_ORE_DRY_BILL from  MC_SINCAL_INTERFACE_TRIG_MIN where sincal_cood between 300 and 399   and timestamp between " + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) + " and " + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType) + " and SINCAL_ORE_TC=1 order by sincal_cood ");
        //            List<MC_SINCAL_INTERFACE_TRIG_MIN> carbon_MC_SINCAL_INTERFACE_TRIG_MIN = iDataBase.GetList<MC_SINCAL_INTERFACE_TRIG_MIN>(carbon);
        //            //焦炭粒度
        //            string c_granularity = string.Format("select avg(GRIT_UP_3) GRIT_UP_3 from MD_PHY_PARTICLE_INFO_IN where L2_CODE between 300 and 399 and timestamp between {0} and {1}", DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType), DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            MD_PHY_PARTICLE_INFO_IN carbon_granularity = iDataBase.GetList<MD_PHY_PARTICLE_INFO_IN>(c_granularity).FirstOrDefault();
        //            double? PICAL_BREATH_COKE_GRI = carbon_granularity.GRIT_UP_3;
        //            //铁料粒度（混匀矿粒度）
        //            string m_granularity = string.Format("select avg(GRIT_UP_3) GRIT_UP_3 from MD_PHY_PARTICLE_INFO_IN where L2_CODE between 100 and 199 and timestamp between {0} and {1}", DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType), DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            MD_PHY_PARTICLE_INFO_IN mix_m_granularity = iDataBase.GetList<MD_PHY_PARTICLE_INFO_IN>(m_granularity).FirstOrDefault();
        //            //烧返粒度
        //            string f_granularity = string.Format("select avg(GRIT_UP_3) GRIT_UP_3 from MD_PHY_PARTICLE_INFO_IN where L2_CODE between 600 and 699 and timestamp between {0} and {1}", DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType), DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            MD_PHY_PARTICLE_INFO_IN fire_granularity = iDataBase.GetList<MD_PHY_PARTICLE_INFO_IN>(f_granularity).FirstOrDefault();
        //            //Mgo含量 碱度
        //            string _mC_SINCAL_RESULT_1MIN = string.Format(@"select 
        //                                                           avg(SINCAL_SIN_PV_MGO) SINCAL_SIN_PV_MGO,
        //                                                           avg( SINCAL_SIN_PV_R) SINCAL_SIN_PV_R
        //                                                           from MC_SINCAL_RESULT_1MIN where
        //                                                            timestamp between {0} and {1}"
        //                                                       , DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType),
        //                                                        DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
        //            MC_SINCAL_RESULT_1MIN mC_SINCAL_RESULT_1MIN = iDataBase.GetList<MC_SINCAL_RESULT_1MIN>(_mC_SINCAL_RESULT_1MIN).FirstOrDefault();
        //            double? PICAL_BREATH_PV_MGO = mC_SINCAL_RESULT_1MIN.SINCAL_SIN_PV_MGO;
        //            double? PICAL_BREATH_PV_R = mC_SINCAL_RESULT_1MIN.SINCAL_SIN_PV_R;
        //            DataTable datasource = GetSource();
        //            string[] arr = FieldString("0").Split(',');
        //            //相关计算的字段 PICAL_JPU  PICAL_BREATH_SOL_BILL_1   PICAL_BREATH_COKE_BILL   PICAL_BREATH_PV_MGO  PICAL_BREATH_PV_R
        //            // PICAL_BREATH_2M_NEX_WAT  PICAL_BREATH_2M_GRIT_AVG  PICAL_BREATH_BED_TH   PICAL_H
        //            // PICAL_BREATH_AIM_TE   PICAL_P  PICAL_BREATH_SRM_BILL PICAL_BREATH_COKE_GRI
        //            //PICAL_BREATH_SPARE1
        //            DataTable source = iDataBase.GetDataTable(@"select 
        //                    PICAL_JPU,
        //                    PICAL_BREATH_SOL_BILL_1,
        //                    PICAL_BREATH_SOL_BILL_2,
        //                    PICAL_BREATH_SOL_BILL_3,
        //                    PICAL_BREATH_COKE_BILL,
        //                    PICAL_BREATH_PV_MGO,
        //                    PICAL_BREATH_PV_R,
        //                    PICAL_BREATH_2M_NEX_WAT,
        //                    PICAL_BREATH_2M_GRIT_AVG,
        //                    PICAL_BREATH_BED_TH,
        //                    PICAL_H,
        //                    PICAL_BREATH_AIM_TE,
        //                    PICAL_P,
        //                    PICAL_BREATH_SRM_BILL,
        //                    PICAL_BREATH_COKE_GRI,
        //                    PICAL_RELAT_BR_BL_BLEND,
        //                    PICAL_BREATH_SPARE1
        //                    from M_PICAL_BREATH_RESULT where rownum between 1 and 100 order by timestamp desc
        //                    ");
        //            DataRow row = source.NewRow();
        //            row["PICAL_JPU"] = JPU;
        //            row["PICAL_BREATH_SOL_BILL_1"] = result.Split(',')[0];
        //            row["PICAL_BREATH_COKE_BILL"] = carbon_MC_SINCAL_INTERFACE_TRIG_MIN[0].SINCAL_ORE_DRY_BILL;
        //            row["PICAL_BREATH_PV_MGO"] = PICAL_BREATH_PV_MGO == null ? 0 : PICAL_BREATH_PV_MGO;
        //            row["PICAL_BREATH_PV_R"] = PICAL_BREATH_PV_R == null ? 0 : PICAL_BREATH_PV_R;
        //            row["PICAL_BREATH_2M_NEX_WAT"] = PICAL_BREATH_2M_NEX_WAT;
        //            row["PICAL_BREATH_2M_GRIT_AVG"] = PICAL_BREATH_2M_GRIT_AVG;
        //            row["PICAL_BREATH_BED_TH"] = PICAL_BREATH_BED_TH;
        //            row["PICAL_H"] = C_THICK_PV;
        //            row["PICAL_BREATH_AIM_TE"] = PICAL_BREATH_AIM_TE;//点火温度
        //            row["PICAL_P"] = P;
        //            row["PICAL_BREATH_SRM_BILL"] = PICAL_RE_BILL_DRY;
        //            row["PICAL_RELAT_BR_BL_BLEND"] = PICAL_BREATH_BLE_TE;
        //            row["PICAL_BREATH_COKE_GRI"] = 100 - PICAL_BREATH_COKE_GRI == null ? 0 : PICAL_BREATH_COKE_GRI;//焦炭粒度
        //            row["PICAL_BREATH_SPARE1"] = 100 - fire_granularity.GRIT_UP_3 == null ? 0 : fire_granularity.GRIT_UP_3;//少返粒度
        //            int r_length = result.Split(',').Length;
        //            switch (r_length)
        //            {
        //                case 1:
        //                    row["PICAL_BREATH_SOL_BILL_2"] = 0;
        //                    row["PICAL_BREATH_SOL_BILL_3"] = 0;
        //                    break;
        //                case 2:
        //                    row["PICAL_BREATH_SOL_BILL_3"] = 0;
        //                    break;
        //                default:
        //                    row["PICAL_BREATH_SOL_BILL_2"] = result.Split(',')[1];
        //                    row["PICAL_BREATH_SOL_BILL_3"] = result.Split(',')[2];
        //                    break;
        //            }

        //            source.Rows.Add(row);
        //            string valueList = HistoryCalculate(source);
        //            string sql_ = string.Format(@"insert into M_PICAL_BREATH_RESULT
        //                     (
        //                        TIMESTAMP,--0
        //                        PICAL_Q,
        //                        PICAL_Q1,
        //                        PICAL_Q2,
        //                        PICAL_P,
        //                        PICAL_P1,
        //                        PICAL_P2,
        //                        PICAL_H,
        //                        PICAL_A,
        //                        PICAL_JPU,
        //                        PICAL_VER_SIN_SP,--10
        //                        PICAL_BREATH_1M_GRIT_1,
        //                        PICAL_BREATH_1M_GRIT_1_3,
        //                        PICAL_BREATH_1M_GRIT_3,
        //                        PICAL_BREATH_2M_GRIT_1,
        //                        PICAL_BREATH_2M_GRIT_1_3,
        //                        PICAL_BREATH_2M_GRIT_3,
        //                        PICAL_BREATH_FILL_RATE,
        //                        PICAL_BREATH_1M_FT_PV,
        //                        PICAL_BREATH_2M_FT_PV,
        //                        PICAL_BREATH_1M_MIX_RATE,--20
        //                        PICAL_BREATH_2M_MIX_RATE,
        //                        PICAL_BREATH_1M_NEX_WAT,
        //                        PICAL_BREATH_2M_NEX_WAT,
        //                        PICAL_BREATH_2M_GRIT_AVG,--24
        //                      --PICAL_BREATH_BLE_TE,
        //                        PICAL_BREATH_SOL_BILL_1,
        //                        PICAL_BREATH_SOL_BILL_2,
        //                        PICAL_BREATH_SOL_BILL_3,
        //                        PICAL_BREATH_IRON_GRIT,
        //                        PICAL_BREATH_CON_BILL,
        //                        PICAL_BREATH_SRM_BILL,--30
        //                        PICAL_BREATH_COKE_BILL,
        //                        PICAL_BREATH_AIM_TE,
        //                        PICAL_BREATH_IG_01_TE,
        //                        PICAL_BREATH_IG_02_TE,
        //                        PICAL_BREATH_IG_03_TE,
        //                        PICAL_BREATH_SIN_MS_PV,
        //                        PICAL_BREATH_PV_MGO,
        //                        PICAL_BREATH_PV_R,
        //                        PICAL_BREATH_BED_TH,
        //                        PICAL_BREATH_MA_TE,--40
        //                        PICAL_BREATH_COKE_GRI,--41
        //                        PICAL_BREATH_SPARE1,--42

        //                        PICAL_RELAT_BR_BL_FLUX,
        //                        PICAL_RELAT_BR_BL_FUEL,
        //                        PICAL_RELAT_BR_BL_MGO,
        //                        PICAL_RELAT_BR_R,
        //                        PICAL_RELAT_BR_WAT,
        //                        PICAL_RELAT_BR_MIX_GR,
        //                        PICAL_RELAT_BR_BED_TH,
        //                        PICAL_RELAT_BR_THICK,
        //                        PICAL_RELAT_BR_BL_IG_TE,
        //                        PICAL_RELAT_BR_FLUE_PT,
        //                        PICAL_RELAT_BR_MA_TE,
        //                        PICAL_RELAT_BR_BL_SINBACK,
        //                        PICAL_RELAT_BR_COKE_GRI,
        //                        PICAL_RELAT_BR_BL_BLEND
        //                        PICAL_BREATH_SPARE11

        //                     )
        //                    values(
        //                         {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},
        //                                            {16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32}
        //                           ,{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43}
        //                           )",
        //                       DataBaseFactory.ConvertTimeString(Max_PICAL_1M, T_DBSource.DBType), //0
        //                                             Q,
        //                                             Q1,
        //                                             Q2,
        //                                             P,
        //                                             P1,
        //                                             P2,
        //                                             H,
        //                                             A,
        //                                             JPU,     //9                                  
        //                                             PICAL_VER_SIN_SP,
        //                                             GRIT_1,
        //                                             GRIT_1_3,
        //                                             GRIT_3,//13

        //                                             PICAL_BREATH_2M_GRIT_1,
        //                                             PICAL_BREATH_2M_GRIT_1_3,
        //                                             PICAL_BREATH_2M_GRIT_3,

        //                                             PICAL_BREATH_FILL_RATE,
        //                                             PICAL_BREATH_1M_FT_PV,
        //                                             PICAL_BREATH_2M_FT_PV,//19
        //                                             PICAL_BREATH_1M_MIX_RATE,
        //                                            PICAL_BREATH_2M_MIX_RATE,
        //                                            PICAL_BREATH_1M_NEX_WAT,
        //                                            PICAL_BREATH_2M_NEX_WAT,
        //                                            PICAL_BREATH_2M_GRIT_AVG,
        //                                           row["PICAL_BREATH_SOL_BILL_1"],
        //                                           row["PICAL_BREATH_SOL_BILL_2"],
        //                                           row["PICAL_BREATH_SOL_BILL_3"],//27
        //                                           100 - mix.GRIT_UP_3,//混匀矿粒度  28
        //                                           l_MD_BLEND_BATCH_LIST_IN.BLEND_DRY_RATIO,//精矿配比29
        //                                           PICAL_RE_BILL_DRY,//烧返配比
        //                                           carbon_MC_SINCAL_INTERFACE_TRIG_MIN[0].SINCAL_ORE_DRY_BILL,//焦炭配比
        //                                            PICAL_BREATH_AIM_TE,//32
        //                                            PICAL_BREATH_IG_01_TE,
        //                                            PICAL_BREATH_IG_02_TE,
        //                                            PICAL_BREATH_IG_03_TE,
        //                                            PICAL_BREATH_SIN_MS_PV,//36
        //                                            PICAL_BREATH_PV_MGO,
        //                                            PICAL_BREATH_PV_R,
        //                                            PICAL_BREATH_BED_TH,
        //                                            PICAL_BREATH_MA_TE,//36
        //                                          100 - PICAL_BREATH_COKE_GRI,//37
        //                                          100 - fire_granularity.GRIT_UP_3,//烧返粒度 42
        //                                            valueList);//
        //            iDataBase.ExecuteCommand(sql_);


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteLine(ex.Message, typeof(Console_ReCalculate).Name);
        //        throw;
        //    }
        //}
        /// <summary>
        /// 相关计算结果
        /// </summary>
        /// <param name="datasource"></param>
        /// <returns></returns>
        private List<double> HistoryCalculate(DataTable datasource)
        {
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    List<double> doubleY = new List<double>();
                    List<double[]> list1 = new List<double[]>();
                    for (int i = 0; i < datasource.Columns.Count; i++)
                    {
                        double[] D = new double[datasource.Columns.Count];
                        for (int j = 0; j < datasource.Rows.Count; j++)
                        {
                            string n = datasource.Rows[j][datasource.Columns[i].ColumnName].ToString();
                            if (string.IsNullOrEmpty(n)) { n = "0"; }
                            D[i] = Convert.ToDouble(n);
                        }
                        list1.Add(D);
                    }
                    double[] RELEVANCE_SZ_2 = new double[list1.Count];
                    for (int i = 0; i < list1.Count; i++)
                    {
                        RELEVANCE_SZ_2[i] = Correlation.Spearman(list1[0], list1[i]);
                        doubleY.Add(RELEVANCE_SZ_2[i]);
                    }
                    doubleY.RemoveAt(0);

                    string valueList = string.Empty;
                    for (int i = 0; i < doubleY.Count; i++)
                    {
                        if (doubleY[i] == double.NaN) { doubleY[i] = 0; }
                        valueList += doubleY[i] + ",";
                    }
                    valueList = valueList.Substring(0, valueList.Length - 1);
                    return doubleY;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message, typeof(Console_ReCalculate).Name);
                throw;
            }

        }

        private DataTable GetSource()
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                string sql = string.Format("select * from M_PICAL_BREATH_RESULT where 1=1 and rownum between 1 and 100 order by timestamp desc");
                DataTable datasource = iDataBase.GetDataTable(sql);
                return datasource;
            }
        }
        private string FieldString(string config_id)
        {
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    List<M_CONFIG_RELATED> l_M_CONFIG_RELATED = iDataBase.GetList<M_CONFIG_RELATED>("select * from M_CONFIG_RELATED where id='" + config_id + "'");
                    string fieldList = l_M_CONFIG_RELATED.First().RELATEDA + ",";

                    for (int i = 0; i < l_M_CONFIG_RELATED.Count; i++)
                    {
                        fieldList += l_M_CONFIG_RELATED[i].RELATEDB + ",";
                    }
                    fieldList = fieldList.Substring(0, fieldList.Length - 1);
                    return fieldList;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message, typeof(Console_ReCalculate).Name);
                throw;
            }

        }
        /// <summary>
        /// 垂直烧结速度
        /// </summary>
        /// <returns></returns>
        public double GetMC_BTPCAL_result_1min()
        {
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    DataTable MC_MICAL_PAR = iDataBase.GetDataTable(@"select 
                        *    from MC_MICAL_PAR");
                    DateTime _t4 = iDataBase.GetDateTime("select max(PICAL_2M_TIME) PICAL_2M_TIME from M_PICAL_2M_result");
                    DataTable M_PICAL_par = iDataBase.GetDataTable(@"select PAR_K1,PAR_K2,PAR_n,PAR_T2,PAR_T1,PAR_B from M_PICAL_par");

                    DataTable MC_MICAL_RESULT = iDataBase.GetDataTable("select MICAL_CLO_2M_TIME from MC_MICAL_RESULT  where 1=1 and datanum=3 and MICAL_BLEND_2M_TIME<=" + DataBaseFactory.ConvertTimeString(_t4, T_DBSource.DBType));
                    DataTable _MC_MICAL_RESULT = iDataBase.GetDataTable("select MICAL_1M_MAT_TIME,MICAL_2M_1M_TIME from MC_MICAL_RESULT where 1=1 and datanum=2 and timestamp<=" + DataBaseFactory.ConvertTimeString(_t4, T_DBSource.DBType) + " order by timestamp desc");

                    double CLO_TIME = MC_MICAL_PAR.Rows[0]["PAR_CLOTH_U_TIME"].ToDouble();
                    double MICAL_1M_MAT_TIME = _MC_MICAL_RESULT.Rows[0]["MICAL_1M_MAT_TIME"].ToDouble();
                    double CLO_2M_TIME = MC_MICAL_RESULT.Rows[0]["MICAL_CLO_2M_TIME"].ToDouble();
                    double PAR_T2 = M_PICAL_par.Rows[0]["PAR_T2"].ToDouble();
                    double _2M_TIME = MC_MICAL_PAR.Rows[0]["PAR_2M_U_TIME"].ToDouble();
                    double t9 = CLO_TIME + CLO_2M_TIME - _2M_TIME;
                    //垂直烧结速度
                    DateTime BeginTime = _t4.AddMinutes(t9);
                    BeginTime = iDataBase.GetDateTime("select min(TIMESTAMP) from MC_BTPCAL_result_1min where timestamp>=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
                    DateTime EndTime = BeginTime.AddMinutes(PAR_T2);
                    DataTable MC_BTPCAL_result_1min = iDataBase.GetDataTable(@"select avg(BTPCAL_V) BTPCAL_V from MC_BTPCAL_result_1min where timestamp>="
                                                                               + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) +
                                                                              " and timestamp<=" + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                    double PICAL_VER_SIN_SP = MC_BTPCAL_result_1min.Rows[0]["BTPCAL_V"].ToDouble();
                    return PICAL_VER_SIN_SP;
                }
            }
            catch (Exception)
            {
                Log.WriteLine(typeof(Console_ReCalculate).Name);
                throw;
            }

        }

        /// <summary>
        /// 透气性表的最大时间
        /// </summary>
        /// <returns></returns>
        private DateTime BREATH_RESULT()
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                return iDataBase.GetDateTime("select max(timestamp) from M_PICAL_BREATH_RESULT");
            }
        }
        /// <summary>
        /// 一混最大时间
        /// </summary>
        /// <returns></returns>
        private DateTime PICAL_1M_result()
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                DateTime Max_PICAL_1M = iDataBase.GetDateTime("select max(timestamp) from M_PICAL_1M_result");
                return Max_PICAL_1M;
            }
        }
        /// <summary>
        /// 二混采样时间向后正推时间t9，采集
        ///点火温度（平均）、点火段温度1、点火段温度2、点火段温度3、台车速度（平均）
        /// </summary>
        /// <returns></returns>
        public DataTable GetT_BURN_COLE_PLC_1MIN()
        {
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    DataTable MC_MICAL_PAR = iDataBase.GetDataTable(@"select 
                        *    from MC_MICAL_PAR");
                    DateTime _t4 = iDataBase.GetDateTime("select max(PICAL_2M_TIME) PICAL_2M_TIME from M_PICAL_2M_result");
                    DataTable M_PICAL_par = iDataBase.GetDataTable(@"select PAR_K1,PAR_K2,PAR_n,PAR_T2,PAR_T1,PAR_B from M_PICAL_par");

                    DataTable MC_MICAL_RESULT = iDataBase.GetDataTable("select MICAL_CLO_2M_TIME from MC_MICAL_RESULT  where 1=1 and datanum=3 and MICAL_BLEND_2M_TIME<=" + DataBaseFactory.ConvertTimeString(_t4, T_DBSource.DBType));
                    DataTable _MC_MICAL_RESULT = iDataBase.GetDataTable("select MICAL_1M_MAT_TIME,MICAL_2M_1M_TIME from MC_MICAL_RESULT where 1=1 and datanum=2 and timestamp<=" + DataBaseFactory.ConvertTimeString(_t4, T_DBSource.DBType) + " order by timestamp desc");

                    double CLO_TIME = MC_MICAL_PAR.Rows[0]["PAR_CLOTH_U_TIME"].ToDouble();
                    double MICAL_1M_MAT_TIME = _MC_MICAL_RESULT.Rows[0]["MICAL_1M_MAT_TIME"].ToDouble();
                    double CLO_2M_TIME = MC_MICAL_RESULT.Rows[0]["MICAL_CLO_2M_TIME"].ToDouble();
                    double PAR_T2 = M_PICAL_par.Rows[0]["PAR_T2"].ToDouble();
                    double _2M_TIME = MC_MICAL_PAR.Rows[0]["PAR_2M_U_TIME"].ToDouble();
                    double t9 = CLO_TIME + CLO_2M_TIME - _2M_TIME;
                    //正推时间t9

                    //点火温度 1 2 3
                    DateTime BeginTime = _t4.AddMinutes(t9);
                    BeginTime = iDataBase.GetDateTime("select min(TIMESTAMP)TIMESTAMP from T_BURN_COLE_PLC_1MIN where timestamp>=" + DataBaseFactory.ConvertTimeString(_t4, T_DBSource.DBType));
                    DateTime EndTime = BeginTime.AddMinutes(PAR_T2);
                    DataTable T_BURN_COLE_PLC_1MIN = iDataBase.GetDataTable(@"select  round(avg(BC_AIM_TE),3) BC_AIM_TE,
                                                                              round(avg(BC_IG_01_TE),3) BC_IG_01_TE,
                                                                              round(avg(BC_IG_02_TE),3) BC_IG_02_TE,
                                                                              round(avg(BC_IG_03_TE),3) BC_IG_03_TE,
                                                                              round(avg(BC_SIN_MS_PV),3) BC_SIN_MS_PV
                                                                               from T_BURN_COLE_PLC_1MIN where timestamp between "
                                                                                  + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) +
                                                                                  " and " + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                    double PICAL_BREATH_AIM_TE = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_AIM_TE"].ToDouble();//点火温度
                    double PICAL_BREATH_IG_01_TE = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_IG_01_TE"].ToDouble();//点火温度 1 
                    double PICAL_BREATH_IG_02_TE = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_IG_02_TE"].ToDouble();//点火温度  2 
                    double PICAL_BREATH_IG_03_TE = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_IG_03_TE"].ToDouble(); //点火温度 3
                    double PICAL_BREATH_SIN_MS_PV = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_SIN_MS_PV"].ToDouble();//台车速度


                    return T_BURN_COLE_PLC_1MIN;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message, typeof(Console_ReCalculate).Name);
                throw;
            }
        }
        /// <summary>
        /// 二混采样时间向后正推时间t9，采集值：布料料厚、铺底料厚度    
        /// </summary>
        /// <returns></returns>
        public DataTable GetT_CLOTH_PLC_1MIN()
        {
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    DateTime _t4 = iDataBase.GetDateTime("select max(PICAL_2M_TIME) PICAL_2M_TIME from M_PICAL_2M_result");
                    DataTable MC_MICAL_PAR = iDataBase.GetDataTable(@"select 
                        *    from MC_MICAL_PAR");
                   // DateTime EndTime = iDataBase.GetDateTime("select max(timestamp) from MC_MICAL_RESULT  where 1=1 and datanum=3 and MICAL_BLEND_2M_TIME<="+DataBaseFactory.ConvertTimeString(_t4,T_DBSource.DBType));
                    DataTable MC_MICAL_RESULT = iDataBase.GetDataTable("select MICAL_CLO_2M_TIME from MC_MICAL_RESULT  where 1=1 and datanum=3 and MICAL_BLEND_2M_TIME<=" + DataBaseFactory.ConvertTimeString(_t4, T_DBSource.DBType)+" order by timestamp desc");
                   // EndTime = iDataBase.GetDateTime("select max(timestamp) from MC_MICAL_RESULT where 1=1 and datanum=2 and timestamp<="+DataBaseFactory.ConvertTimeString(_t4,T_DBSource.DBType));
                    DataTable _MC_MICAL_RESULT = iDataBase.GetDataTable("select MICAL_1M_MAT_TIME,MICAL_2M_1M_TIME from MC_MICAL_RESULT where 1=1 and datanum=2 and timestamp<=" + DataBaseFactory.ConvertTimeString(_t4, T_DBSource.DBType) + " order by timestamp desc");
                    double CLO_TIME = MC_MICAL_PAR.Rows[0]["PAR_CLOTH_U_TIME"].ToDouble();
                    double MICAL_1M_MAT_TIME = _MC_MICAL_RESULT.Rows[0]["MICAL_1M_MAT_TIME"].ToDouble();
                    double CLO_2M_TIME = MC_MICAL_RESULT.Rows[0]["MICAL_CLO_2M_TIME"].ToDouble();
                    DataTable M_PICAL_par = iDataBase.GetDataTable(@"select PAR_K1,PAR_K2,PAR_n,PAR_T2,PAR_T1,PAR_B from M_PICAL_par");
                    double PAR_T2 = M_PICAL_par.Rows[0]["PAR_T2"].ToDouble();
                    double _2M_TIME = MC_MICAL_PAR.Rows[0]["PAR_2M_U_TIME"].ToDouble();
                    double t9 = CLO_TIME + CLO_2M_TIME - _2M_TIME;
                    //正推时间t9
                    DateTime BeginTime = _t4.AddMinutes(t9);
                    //布料厚度
                    BeginTime = iDataBase.GetDateTime("select min(timestamp) TimeStamp from T_CLOTH_PLC_1MIN where TimeStamp>=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));

                 DateTime  EndTime = BeginTime.AddMinutes(PAR_T2);
                    DataTable T_CLOTH_PLC_1MIN = iDataBase.GetDataTable(@"select avg(C_THICK_PV) C_THICK_PV,avg(C_BED_MATERAL_TH) C_BED_MATERAL_TH from T_CLOTH_PLC_1MIN where timestamp>=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) + " and timestamp<=" + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                    double C_THICK_PV = T_CLOTH_PLC_1MIN.Rows[0]["C_THICK_PV"].ToDouble();
                    //铺底料厚度
                    double PICAL_BREATH_BED_TH = T_CLOTH_PLC_1MIN.Rows[0]["C_BED_MATERAL_TH"].ToDouble();
                    return T_CLOTH_PLC_1MIN;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message, typeof(Console_ReCalculate).Name);
                throw;
            }

        }
        /// <summary>
        /// 后正推时间t9，计算值：透气性指数、风量Q、负压P、主抽温度
        /// </summary>
        /// <returns></returns>
        public DataTable GetT_MA_PGD_PLC_1MIN()
        {
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    DateTime _t4 = iDataBase.GetDateTime("select max(PICAL_2M_TIME) PICAL_2M_TIME from M_PICAL_2M_result");
                    DataTable MC_MICAL_PAR = iDataBase.GetDataTable(@"select 
                        *    from MC_MICAL_PAR");
                    DataTable MC_MICAL_RESULT = iDataBase.GetDataTable("select MICAL_CLO_2M_TIME from MC_MICAL_RESULT  where 1=1 and datanum=3 and MICAL_BLEND_2M_TIME<=" + DataBaseFactory.ConvertTimeString(_t4, T_DBSource.DBType));
                    DataTable _MC_MICAL_RESULT = iDataBase.GetDataTable("select MICAL_1M_MAT_TIME,MICAL_2M_1M_TIME from MC_MICAL_RESULT where 1=1 and datanum=2 and timestamp<=" + DataBaseFactory.ConvertTimeString(_t4, T_DBSource.DBType) + " order by timestamp desc");
                    double CLO_TIME = MC_MICAL_PAR.Rows[0]["PAR_CLOTH_U_TIME"].ToDouble();
                    double MICAL_1M_MAT_TIME = _MC_MICAL_RESULT.Rows[0]["MICAL_1M_MAT_TIME"].ToDouble();
                    double CLO_2M_TIME = MC_MICAL_RESULT.Rows[0]["MICAL_CLO_2M_TIME"].ToDouble();
                    DataTable M_PICAL_par = iDataBase.GetDataTable(@"select PAR_K1,PAR_K2,PAR_n,PAR_T2,PAR_T1,PAR_B from M_PICAL_par");
                    double PAR_T2 = M_PICAL_par.Rows[0]["PAR_T2"].ToDouble();
                    double _2M_TIME = MC_MICAL_PAR.Rows[0]["PAR_2M_U_TIME"].ToDouble();
                    double t9 = CLO_TIME + CLO_2M_TIME - _2M_TIME;
                    //正推时间t9
                    DateTime BeginTime = _t4.AddMinutes(-t9);
                   // BeginTime = _t4.AddMinutes(t9);
                    BeginTime = iDataBase.GetDateTime("select max(TimeStamp) from T_MA_PGD_PLC_1MIN where timestamp<=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
                    DateTime EndTime = BeginTime.AddMinutes(-PAR_T2);
                    string sql = @"select round(avg(PS_MA_SB_1_FLUE_FT),3) PS_MA_SB_1_FLUE_FT,--q1
                                      round(avg(PS_MA_SB_2_FLUE_FT),3) PS_MA_SB_2_FLUE_FT,--q2
                                      round(avg(PS_MA_SB_1_FLUE_PT),3) PS_MA_SB_1_FLUE_PT,--p1
                                      round(avg(PS_MA_SB_2_FLUE_PT),3) PS_MA_SB_2_FLUE_PT,--p2
                                                 round(avg(PS_MA_SB_1_FLUE_TE),3)PS_MA_SB_1_FLUE_TE,--主抽1温度
                                                 round(avg(PS_MA_SB_2_FLUE_TE),3)PS_MA_SB_2_FLUE_TE--主抽2温度
                                    from T_MA_PGD_PLC_1MIN where timestamp between " + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType) + " and " + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType);
                    DataTable T_MA_PGD_PLC_1MIN = iDataBase.GetDataTable(sql);
                    return T_MA_PGD_PLC_1MIN;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message, typeof(Console_ReCalculate).Name);
                throw;
            }
        }
        /// <summary>
        /// 在二混采样时间采集）到二混后，采集当时：二混粒度（＜1mm、1-3mm、＞3mm）、、二混后温度
        /// </summary>
        /// <returns></returns>
        public DataTable GetMD_PHY_PARTICLE_INFO_IN()
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                //采样时间
                DateTime _t4 = iDataBase.GetDateTime("select max(PICAL_2M_TIME) PICAL_2M_TIME from M_PICAL_2M_result");
                DataTable M_PICAL_par = iDataBase.GetDataTable(@"select PAR_K1,PAR_K2,PAR_n,PAR_T2,PAR_T1,PAR_B from M_PICAL_par");
                double PAR_T2 = M_PICAL_par.Rows[0]["PAR_T2"].ToDouble();
                DateTime BeginTime = _t4;
                BeginTime = iDataBase.GetDateTime("select min(sampletime)timestamp from  MD_PHY_PARTICLE_INFO_IN where sampletime>=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
                DateTime EndTime = BeginTime.AddMinutes(PAR_T2);
                string s = string.Format(@"select 
                           round(avg(GRIT_LOW_1),3) GRIT_LOW_1,round(avg(MATERIAL_TE),3) MATERIAL_TE,
                            round(avg(GRIT_1_3),3) GRIT_1_3,round(avg(GRIT_UP_3),3)GRIT_UP_3,3 from MD_PHY_PARTICLE_INFO_IN where L2_CODE between 900 and 999 and
                            sampletime between " + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) +
                           " and " + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                DataTable MD_PHY_PARTICLE_INFO_IN = iDataBase.GetDataTable(@"select 
                           round(avg(GRIT_LOW_1),3) GRIT_LOW_1,round(avg(MATERIAL_TE),3) MATERIAL_TE,
                            round(avg(GRIT_1_3),3) GRIT_1_3,round(avg(GRIT_UP_3),3)GRIT_UP_3,3 from MD_PHY_PARTICLE_INFO_IN where L2_CODE between 900 and 999 and
                            sampletime between " + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) +
                           " and " + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));

                //<1
                double PICAL_BREATH_2M_GRIT_1 = MD_PHY_PARTICLE_INFO_IN.Rows[0]["GRIT_LOW_1"].ToDouble();
                //1-3
                double PICAL_BREATH_2M_GRIT_1_3 = MD_PHY_PARTICLE_INFO_IN.Rows[0]["GRIT_1_3"].ToDouble();
                //>3
                double PICAL_BREATH_2M_GRIT_3 = MD_PHY_PARTICLE_INFO_IN.Rows[0]["GRIT_UP_3"].ToDouble();
                //平均粒度
                double PICAL_BREATH_2M_GRIT_AVG = PICAL_BREATH_2M_GRIT_1 * 1 + PICAL_BREATH_2M_GRIT_1_3 * 1.5 + PICAL_BREATH_2M_GRIT_3 * 3;
                //温度
                double PICAL_BREATH_BLE_TE = MD_PHY_PARTICLE_INFO_IN.Rows[0]["MATERIAL_TE"].ToDouble();
                return MD_PHY_PARTICLE_INFO_IN;
            }
        }
        /// <summary>
        ///  在二混采样时间采集）到二混后，采集当时二混水分
        /// </summary>
        /// <returns></returns>
        public double Get_T_BLEND_PLC_1MIN()
        {
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    DateTime _t4 = iDataBase.GetDateTime("select max(PICAL_2M_TIME) PICAL_2M_TIME from M_PICAL_2M_result");
                    DateTime BeginTime = _t4;
                    DataTable M_PICAL_par = iDataBase.GetDataTable(@"select PAR_K1,PAR_K2,PAR_n,PAR_T2,PAR_T1,PAR_B from M_PICAL_par");
                    double PAR_T2 = M_PICAL_par.Rows[0]["PAR_T2"].ToDouble();
                    BeginTime = iDataBase.GetDateTime("select max(timestamp)timestamp from  T_BLEND_PLC_1MIN where timestamp<=" + DataBaseFactory.ConvertTimeString(_t4, T_DBSource.DBType));
                    DateTime EndTime = BeginTime.AddMinutes(PAR_T2);
                    
                    DataTable _T_BLEND_PLC_1MIN = iDataBase.GetDataTable(@"select                                                                   
                                                                         avg(B_2M_NEX_WATER_AVG) B_2M_NEX_WATER_AVG
                                                                         from T_BLEND_PLC_1MIN WHERE
                                                                         timestamp between " + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) +
                                                                        " and  " + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                    double B_2M_NEX_WATER_AVG = _T_BLEND_PLC_1MIN.Rows[0]["B_2M_NEX_WATER_AVG"].ToDouble();
                    return B_2M_NEX_WATER_AVG;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message, typeof(Console_ReCalculate).Name);
                throw;
            }
        }
        /// <summary>
        /// 二混采样时间往前倒推2M_TIME ，采集当时：二混填充率、二混加水、二混转速
        /// </summary>
        /// <returns></returns>
        public T_BLEND_PLC_1MIN GetT_BLEND_PLC_1MIN()
        {
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    DateTime _t4 = iDataBase.GetDateTime("select max(PICAL_2M_TIME) PICAL_2M_TIME from M_PICAL_2M_result");
                    DataTable M_PICAL_par = iDataBase.GetDataTable(@"select PAR_K1,PAR_K2,PAR_n,PAR_T2,PAR_T1,PAR_B from M_PICAL_par");
                    DataTable MC_MICAL_PAR = iDataBase.GetDataTable(@"select 
                        *    from MC_MICAL_PAR");

                    double _2M_TIME = MC_MICAL_PAR.Rows[0]["PAR_2M_U_TIME"].ToDouble();
                    DataTable _MC_MICAL_RESULT = iDataBase.GetDataTable("select MICAL_1M_MAT_TIME,MICAL_2M_1M_TIME from MC_MICAL_RESULT where 1=1 and datanum=2 and timestamp<=" + DataBaseFactory.ConvertTimeString(_t4, T_DBSource.DBType) + " order by timestamp desc");
                    double _2M_1M_TIME = _MC_MICAL_RESULT.Rows[0]["MICAL_2M_1M_TIME"].ToDouble();
                    DateTime BeginTime = _t4.AddMinutes(-_2M_TIME);//采样时间
                    double PAR_T2 = M_PICAL_par.Rows[0]["PAR_T2"].ToDouble();
                    BeginTime = iDataBase.GetDateTime("select max(TimeStamp) TimeStamp from T_BLEND_PLC_1MIN where TimeStamp<=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
                    DateTime EndTime = BeginTime.AddMinutes(-PAR_T2);
                    string twoMix = string.Format(@"select 
                                           round(avg(B_2M_FLOW_PV),3) B_2M_FLOW_PV, 
                                            round(avg(B_2M_MIXER_RATE),3) B_2M_MIXER_RATE,
                                           round(avg(B_1M_NEX_BELT_W_2H_1),3)B_1M_NEX_BELT_W_2H_1
                                            from T_BLEND_PLC_1MIN where timestamp between {0} and {1}",
                                       DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType),
                                       DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
                    T_BLEND_PLC_1MIN t_BLEND_PLC_1MIN = iDataBase.GetList<T_BLEND_PLC_1MIN>(twoMix).FirstOrDefault();
                    double? B_2M_FLOW_PV = t_BLEND_PLC_1MIN.B_2M_FLOW_PV;
                    double? B_2M_MIXER_RATE = t_BLEND_PLC_1MIN.B_2M_MIXER_RATE;
                    double? B_1M_NEX_BELT_W_2H_1 = t_BLEND_PLC_1MIN.B_1M_NEX_BELT_W_2H_1;//二混填充率
                    return t_BLEND_PLC_1MIN;
                    //  B_2M_FLOW_PV
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message, typeof(Console_ReCalculate).Name);
                throw;
            }
        }
        /// <summary>
        /// 二混采样时间往前倒推t6
        /// 到一混后，采集当时：一混粒度（＜1mm、1-3mm、＞3mm)
        /// </summary>
        /// <returns></returns>
        public MD_PHY_PARTICLE_INFO_IN Get_MD_PHY_PARTICLE_INFO_IN()
        {
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    DateTime _t4 = iDataBase.GetDateTime("select max(PICAL_2M_TIME) PICAL_2M_TIME from M_PICAL_2M_result");
                    DataTable MC_MICAL_PAR = iDataBase.GetDataTable(@"select 
                        *    from MC_MICAL_PAR");
                    double _2M_TIME = MC_MICAL_PAR.Rows[0]["PAR_2M_U_TIME"].ToDouble();
                    DataTable M_PICAL_par = iDataBase.GetDataTable(@"select PAR_K1,PAR_K2,PAR_n,PAR_T2,PAR_T1,PAR_B from M_PICAL_par");
                    DataTable _MC_MICAL_RESULT = iDataBase.GetDataTable("select MICAL_1M_MAT_TIME,MICAL_2M_1M_TIME from MC_MICAL_RESULT where 1=1 and datanum=2 and timestamp<=" + DataBaseFactory.ConvertTimeString(_t4, T_DBSource.DBType) + " order by timestamp desc");
                    double _1M_TIME = MC_MICAL_PAR.Rows[0]["PAR_1M_U_TIME"].ToDouble();
                    double _2M_1M_TIME = _MC_MICAL_RESULT.Rows[0]["MICAL_2M_1M_TIME"].ToDouble();//
                    double PAR_T2 = M_PICAL_par.Rows[0]["PAR_T2"].ToDouble();
                    double t6 = _2M_TIME + _2M_1M_TIME - _1M_TIME;
                    DateTime BeginTime = _t4.AddMinutes(-t6);
                    //获取比倒推时间点小的最大值
                    BeginTime = iDataBase.GetDateTime("select max(sampletime) from MD_PHY_PARTICLE_INFO_IN where  L2_CODE between 800 and 899 and sampletime <=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
                    DateTime EndTime = BeginTime.AddMinutes(-PAR_T2);
                    string OneMix = string.Format("select avg(GRIT_LOW_1) GRIT_LOW_1,avg(GRIT_1_3)GRIT_1_3,avg(GRIT_UP_3) GRIT_UP_3 from MD_PHY_PARTICLE_INFO_IN where L2_CODE between 800 and 899 and sampletime between {0} and {1}", DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType), DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
                    MD_PHY_PARTICLE_INFO_IN _mD_PHY_PARTICLE_INFO_IN = iDataBase.GetList<MD_PHY_PARTICLE_INFO_IN>(OneMix).FirstOrDefault();
                    double? GRIT_1 = _mD_PHY_PARTICLE_INFO_IN.GRIT_LOW_1;
                    double? GRIT_1_3 = _mD_PHY_PARTICLE_INFO_IN.GRIT_1_3;
                    double? GRIT_3 = _mD_PHY_PARTICLE_INFO_IN.GRIT_UP_3;
                    return _mD_PHY_PARTICLE_INFO_IN;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message, typeof(Console_ReCalculate).Name);
                throw;
            }
        }
        /// <summary>
        /// 二混采样时间往前倒推t6
        /// 到一混后，采集当时一混后水分
        /// </summary>
        /// <returns></returns>
        public double Get_B_1M_NEX_WATER_AVG()
        {
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    DateTime _t4 = iDataBase.GetDateTime("select max(PICAL_2M_TIME) PICAL_2M_TIME from M_PICAL_2M_result");
                    DataTable MC_MICAL_PAR = iDataBase.GetDataTable(@"select 
                        *    from MC_MICAL_PAR");
                    double _2M_TIME = MC_MICAL_PAR.Rows[0]["PAR_2M_U_TIME"].ToDouble();
                    DataTable M_PICAL_par = iDataBase.GetDataTable(@"select PAR_K1,PAR_K2,PAR_n,PAR_T2,PAR_T1,PAR_B from M_PICAL_par");
                    DataTable _MC_MICAL_RESULT = iDataBase.GetDataTable("select MICAL_1M_MAT_TIME,MICAL_2M_1M_TIME from MC_MICAL_RESULT where 1=1 and datanum=2 and timestamp<=" + DataBaseFactory.ConvertTimeString(_t4, T_DBSource.DBType) + " order by timestamp desc");
                    double _1M_TIME = MC_MICAL_PAR.Rows[0]["PAR_1M_U_TIME"].ToDouble();
                    double _2M_1M_TIME = _MC_MICAL_RESULT.Rows[0]["MICAL_2M_1M_TIME"].ToDouble();//
                    double PAR_T2 = M_PICAL_par.Rows[0]["PAR_T2"].ToDouble();
                    double t6 = _2M_TIME + _2M_1M_TIME - _1M_TIME;
                    DateTime EndTime = _t4.AddMinutes(-t6);
                    EndTime = iDataBase.GetDateTime("select max(timestamp) timestamp from T_BLEND_PLC_1MIN where timestamp<=" + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                    DateTime BeginTime = EndTime.AddMinutes(-PAR_T2);
                    DataTable T_BLEND_PLC_1MIN_t6 = iDataBase.GetDataTable(@"select avg(B_1M_NEX_WATER_AVG) B_1M_NEX_WATER_AVG                                                                 
                                                                                            from T_BLEND_PLC_1MIN
                                                                            where timestamp <="
                                                                               + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType) +
                                                                               " and timestamp >= " + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
                    double B_1M_NEX_WATER_AVG = T_BLEND_PLC_1MIN_t6.Rows[0]["B_1M_NEX_WATER_AVG"].ToDouble();
                    return B_1M_NEX_WATER_AVG;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message, typeof(Console_ReCalculate).Name);
                throw;
            }
        }
        /// <summary>
        /// 采样时间往前倒推时间t7
        /// 采集当时：一混填充率     
        /// </summary>
        /// <returns></returns>
        public double GetPICAL_1M_FILL_RATE()
        {
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    DateTime _t4 = iDataBase.GetDateTime("select max(PICAL_2M_TIME) PICAL_2M_TIME from M_PICAL_2M_result");
                    DataTable MC_MICAL_PAR = iDataBase.GetDataTable(@"select 
                        *    from MC_MICAL_PAR");
                    DataTable M_PICAL_par = iDataBase.GetDataTable(@"select PAR_K1,PAR_K2,PAR_n,PAR_T2,PAR_T1,PAR_B from M_PICAL_par");
                    double PAR_T2 = M_PICAL_par.Rows[0]["PAR_T2"].ToDouble();
                    DataTable _MC_MICAL_RESULT = iDataBase.GetDataTable(@"select MICAL_1M_MAT_TIME,MICAL_2M_1M_TIME from MC_MICAL_RESULT where 1=1
                    and datanum=2 and timestamp<=" + DataBaseFactory.ConvertTimeString(_t4, T_DBSource.DBType) + " order by timestamp desc");
                    double _2M_TIME = MC_MICAL_PAR.Rows[0]["PAR_2M_U_TIME"].ToDouble();
                    double _2M_1M_TIME = _MC_MICAL_RESULT.Rows[0]["MICAL_2M_1M_TIME"].ToDouble();
                    double t7 = _2M_TIME + _2M_1M_TIME;
                    DateTime EndTime = _t4.AddMinutes(-t7);
                    EndTime = iDataBase.GetDateTime("select max(timestamp) from T_BLEND_PLC_1MIN where timestamp<=" + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                    DateTime BeginTime = EndTime.AddMinutes(PAR_T2);
                    DataTable T_BLEND_PLC_1MIN = iDataBase.GetDataTable("select round(avg(B_1M_PRE_BELT_W_1H_1),3)B_1M_PRE_BELT_W_1H_1  from T_BLEND_PLC_1MIN ");
                    double PICAL_1M_FILL_RATE = T_BLEND_PLC_1MIN.Rows[0]["B_1M_PRE_BELT_W_1H_1"].ToDouble();
                    return PICAL_1M_FILL_RATE;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message, typeof(Console_ReCalculate).Name);
                throw;
            }
        }
        /// <summary>
        ///  采样时间往前倒推时间t7
        /// 采集当时  一混加水、一混转速
        /// </summary>
        /// <returns></returns>
        public T_BLEND_PLC_1MIN Get_T_BLEND_PLC_1MIN_t7()
        {
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    DateTime _t4 = iDataBase.GetDateTime("select max(PICAL_2M_TIME) PICAL_2M_TIME from M_PICAL_2M_result");
                    DataTable MC_MICAL_PAR = iDataBase.GetDataTable(@"select 
                        *    from MC_MICAL_PAR");
                    DataTable M_PICAL_par = iDataBase.GetDataTable(@"select PAR_K1,PAR_K2,PAR_n,PAR_T2,PAR_T1,PAR_B from M_PICAL_par");
                    double PAR_T2 = M_PICAL_par.Rows[0]["PAR_T2"].ToDouble();
                    DataTable _MC_MICAL_RESULT = iDataBase.GetDataTable(@"select MICAL_1M_MAT_TIME,MICAL_2M_1M_TIME from MC_MICAL_RESULT where 1=1
                    and datanum=2 and timestamp<=" + DataBaseFactory.ConvertTimeString(_t4, T_DBSource.DBType) + " order by timestamp desc");
                    double _2M_TIME = MC_MICAL_PAR.Rows[0]["PAR_2M_U_TIME"].ToDouble();
                    double _2M_1M_TIME = _MC_MICAL_RESULT.Rows[0]["MICAL_2M_1M_TIME"].ToDouble();
                    double t7 = _2M_TIME + _2M_1M_TIME;
                    DateTime BeginTime = _t4.AddMinutes(-t7);
                    BeginTime = iDataBase.GetDateTime("select max(timestamp)timestamp from T_BLEND_PLC_1MIN where timestamp<=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
                    DateTime EndTime = BeginTime.AddMinutes(-PAR_T2);
                    string s = string.Format("select round(avg(B_1M_FT_PV),3) B_1M_FT_PV,round(avg(B_1M_MIXER_RATE),3) B_1M_MIXER_RATE  from T_BLEND_PLC_1MIN where timestamp <=" + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType) + " and timestamp >=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType));
                    T_BLEND_PLC_1MIN t_BLEND_PLC_1MIN = iDataBase.GetList<T_BLEND_PLC_1MIN>("select round(avg(B_1M_FT_PV),3) B_1M_FT_PV,round(avg(B_1M_MIXER_RATE),3) B_1M_MIXER_RATE  from T_BLEND_PLC_1MIN where timestamp >=" + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType) + " and timestamp <=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType)).FirstOrDefault();
                    double ?B_1M_MIXER_RATE = t_BLEND_PLC_1MIN.B_1M_MIXER_RATE;
                    return t_BLEND_PLC_1MIN;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message, typeof(Console_ReCalculate).Name);
                throw;
            }
        }
        /// <summary>
        /// 二混采样时间往前倒推时间t3
        /// 溶剂配比1、溶剂配比2、溶剂配比3...
        /// </summary>
        /// <returns></returns>
        public M_PICAL_BREATH_RESULT GetMC_SINCAL_INTERFACE_TRIG()
        {
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    DateTime _t4 = iDataBase.GetDateTime("select max(PICAL_2M_TIME) PICAL_2M_TIME from M_PICAL_2M_result");
                    DataTable MC_MICAL_PAR = iDataBase.GetDataTable(@"select 
                        *    from MC_MICAL_PAR");
                    DataTable M_PICAL_par = iDataBase.GetDataTable(@"select PAR_K1,PAR_K2,PAR_n,PAR_T2,PAR_T1,PAR_B from M_PICAL_par");
                    DataTable _MC_MICAL_RESULT = iDataBase.GetDataTable(@"select MICAL_1M_MAT_TIME,MICAL_2M_1M_TIME from MC_MICAL_RESULT where 1=1
                    and datanum=2 and timestamp<=" + DataBaseFactory.ConvertTimeString(_t4, T_DBSource.DBType) + " order by timestamp desc");
                    double _2M_TIME = MC_MICAL_PAR.Rows[0]["PAR_2M_U_TIME"].ToDouble();
                    double _2M_1M_TIME = _MC_MICAL_RESULT.Rows[0]["MICAL_2M_1M_TIME"].ToDouble();
                    double _1M_MAT_TIME = _MC_MICAL_RESULT.Rows[0]["MICAL_1M_MAT_TIME"].ToDouble();
                    double t3 = _2M_TIME + _2M_1M_TIME + _1M_MAT_TIME;
                    DateTime EndTime = _t4.AddMinutes(-t3);
                    double PAR_T2 = M_PICAL_par.Rows[0]["PAR_T2"].ToDouble();
                    EndTime = iDataBase.GetDateTime("select Max(TimeStamp) TimeStamp from MC_SINCAL_INTERFACE_TRIG_MIN where TimeStamp<=" + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                    DateTime BeginTime = EndTime.AddMinutes(-PAR_T2);
                    string solvent = string.Format(@"select * from  MC_SINCAL_INTERFACE_TRIG_MIN where sincal_cood between 400 and 499   and timestamp 
        between " + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) + " and " + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType) +
               " and SINCAL_ORE_TC=1 order by sincal_cood ");
                    List<MC_SINCAL_INTERFACE_TRIG_MIN> l_MC_SINCAL_INTERFACE_TRIG_MIN = iDataBase.GetList<MC_SINCAL_INTERFACE_TRIG_MIN>(solvent);
                    //去除重复的料仓号
                    l_MC_SINCAL_INTERFACE_TRIG_MIN = l_MC_SINCAL_INTERFACE_TRIG_MIN.Where((x, i) => l_MC_SINCAL_INTERFACE_TRIG_MIN.FindIndex(z => z.SINCAL_ID == x.SINCAL_ID) == i).ToList();
                    //通过料种分组
                    IEnumerable<IGrouping<int?, MC_SINCAL_INTERFACE_TRIG_MIN>> query = l_MC_SINCAL_INTERFACE_TRIG_MIN.GroupBy(m => m.SINCAL_COOD);
                    List<double> D = new List<double>();
                    M_PICAL_BREATH_RESULT model = new M_PICAL_BREATH_RESULT();
                    foreach (IGrouping<int?, MC_SINCAL_INTERFACE_TRIG_MIN> info in query)
                    {
                        List<MC_SINCAL_INTERFACE_TRIG_MIN> sl = info.ToList();//分组后的集合 
                        double? sum = sl.Sum(m => m.SINCAL_ORE_DRY_BILL);
                        D.Add(sum.ToDouble());
                    }
                    switch (D.Count)
                    {
                        case 1:
                            model.PICAL_BREATH_SOL_BILL_1 = D[0];
                            model.PICAL_BREATH_SOL_BILL_2 = null;
                            model.PICAL_BREATH_SOL_BILL_3 = null;
                            break;
                        case 2:
                            model.PICAL_BREATH_SOL_BILL_1 = D[0];
                            model.PICAL_BREATH_SOL_BILL_2 = D[1];
                            model.PICAL_BREATH_SOL_BILL_3 = null;
                            break;
                        default:
                            model.PICAL_BREATH_SOL_BILL_1 = D[0];
                            model.PICAL_BREATH_SOL_BILL_2 = D[1];
                            model.PICAL_BREATH_SOL_BILL_3 = D[2]; ;
                            break;
                    }
                    //精矿配比
                    EndTime = _t4;
                    EndTime = iDataBase.GetDateTime(@"select max(timestamp) from MD_BLEND_BATCH_LIST_IN where SAMPLE_CODE between 200 and 299 and timestamp<="
                    + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                    BeginTime = EndTime.AddMinutes(-PAR_T2);
                    string ss = string.Format(@"  select avg(DRY_BILL) DRY_BILL from MD_BLEND_BATCH_LIST_IN
                                      where SAMPLE_CODE between 200 and 299 and TIMESTAMP between 
                           " + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) + " and " + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                    MD_BLEND_BATCH_LIST_IN l_MD_BLEND_BATCH_LIST_IN = iDataBase.GetList<MD_BLEND_BATCH_LIST_IN>(@"
                          select avg(DRY_BILL) DRY_BILL from MD_BLEND_BATCH_LIST_IN
                                      where SAMPLE_CODE between 200 and 299 and TIMESTAMP between 
                           " + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) + " and " + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType)).FirstOrDefault();
                    model.PICAL_BREATH_CON_BILL = l_MD_BLEND_BATCH_LIST_IN.DRY_BILL;
                    //烧返配比
                    EndTime = _t4;
                    EndTime = iDataBase.GetDateTime(@"select max(timestamp) from  MC_SINCAL_INTERFACE_TRIG_MIN where sincal_cood between 600 and 699 and timestamp<"
                     + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                    BeginTime = EndTime.AddMinutes(-PAR_T2);
                    string fire = string.Format("select avg(SINCAL_ORE_DRY_BILL) SINCAL_ORE_DRY_BILL from  MC_SINCAL_INTERFACE_TRIG_MIN where sincal_cood between 600 and 699   and timestamp between " + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) + " and " + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType) + " and SINCAL_ORE_TC=1 order by sincal_cood ");
                    List<MC_SINCAL_INTERFACE_TRIG_MIN> fire_MC_SINCAL_INTERFACE_TRIG_MIN = iDataBase.GetList<MC_SINCAL_INTERFACE_TRIG_MIN>(fire);
                    double? PICAL_BREATH_SRM_BILL = fire_MC_SINCAL_INTERFACE_TRIG_MIN[0].SINCAL_ORE_DRY_BILL;
                    model.PICAL_BREATH_SRM_BILL = PICAL_BREATH_SRM_BILL;
                    //焦炭配比 也是燃料配比
                    EndTime = _t4;
                    EndTime = iDataBase.GetDateTime(@"select max(timestamp) from  MC_SINCAL_INTERFACE_TRIG_MIN where sincal_cood between 300 and 399 and timestamp<"
                     + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                    BeginTime = EndTime.AddMinutes(-PAR_T2);
                    string carbon = string.Format(@"select avg(SINCAL_ORE_DRY_BILL) SINCAL_ORE_DRY_BILL from  MC_SINCAL_INTERFACE_TRIG_MIN where 
                    sincal_cood between 300 and 399   
                   and timestamp between " + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) +
                   " and " + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType) +
                    " and SINCAL_ORE_TC=1 order by sincal_cood ");
                    List<MC_SINCAL_INTERFACE_TRIG_MIN> carbon_MC_SINCAL_INTERFACE_TRIG_MIN = iDataBase.GetList<MC_SINCAL_INTERFACE_TRIG_MIN>(carbon);
                    double? PICAL_BREATH_COKE_BILL = carbon_MC_SINCAL_INTERFACE_TRIG_MIN[0].SINCAL_ORE_DRY_BILL;
                    model.PICAL_BREATH_COKE_BILL = PICAL_BREATH_COKE_BILL;

                    //焦炭粒度
                    EndTime = _t4.AddMinutes(-t3);
                    string s= @"select max(sampletime) from  MD_PHY_PARTICLE_INFO_IN where L2_CODE between 300 and 399 and sampletime<="
                     + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType);
                    EndTime = iDataBase.GetDateTime(@"select max(sampletime) from  MD_PHY_PARTICLE_INFO_IN where L2_CODE between 300 and 399 and sampletime<="
                     + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                    BeginTime = EndTime.AddMinutes(-PAR_T2);
                    string c_granularity = string.Format("select avg(GRIT_UP_3) GRIT_UP_3 from MD_PHY_PARTICLE_INFO_IN where L2_CODE between 300 and 399 and sampletime between {0} and {1}", DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType), DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                    MD_PHY_PARTICLE_INFO_IN carbon_granularity = iDataBase.GetList<MD_PHY_PARTICLE_INFO_IN>(c_granularity).FirstOrDefault();
                    double? PICAL_BREATH_COKE_GRI = 100 - carbon_granularity.GRIT_UP_3;
                    model.PICAL_BREATH_COKE_GRI = PICAL_BREATH_COKE_GRI;
                    //烧返粒度
                    EndTime = _t4.AddMinutes(-t3); ;
                    EndTime = iDataBase.GetDateTime("select max(sampletime) from MD_PHY_PARTICLE_INFO_IN where  L2_CODE between 600 and 699  and sampletime<= " + DataBaseFactory.ConvertTimeString(EndTime,T_DBSource.DBType));
                    string f_granularity = string.Format("select avg(GRIT_UP_3) GRIT_UP_3 from MD_PHY_PARTICLE_INFO_IN where L2_CODE between 600 and 699 and sampletime between {0} and {1}", DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType), DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                    MD_PHY_PARTICLE_INFO_IN fire_granularity = iDataBase.GetList<MD_PHY_PARTICLE_INFO_IN>(f_granularity).FirstOrDefault();
                    double? PICAL_BREATH_SPARE1 = 100 - fire_granularity.GRIT_UP_3;
                    model.PICAL_BREATH_SPARE1 = PICAL_BREATH_SPARE1;
                    //铁料粒度（混匀矿粒度）
                    EndTime = _t4.AddMinutes(-t3);
                    EndTime = iDataBase.GetDateTime(@"select max(sampletime)timestamp from  MD_PHY_PARTICLE_INFO_IN where L2_CODE between 100 and 199 and sampletime<="
                     + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                    BeginTime = EndTime.AddMinutes(-PAR_T2);
                    string m_granularity = string.Format("select avg(GRIT_UP_3) GRIT_UP_3 from MD_PHY_PARTICLE_INFO_IN where L2_CODE between 100 and 199 and sampletime between {0} and {1}", DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType), DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                    MD_PHY_PARTICLE_INFO_IN mix_m_granularity = iDataBase.GetList<MD_PHY_PARTICLE_INFO_IN>(m_granularity).FirstOrDefault();
                    model.PICAL_BREATH_IRON_GRIT = 100 - mix_m_granularity.GRIT_UP_3;
                    //Mgo含量碱度
                    EndTime = _t4.AddMinutes(-t3);
                    EndTime = iDataBase.GetDateTime("select max(timestamp) timestamp from MC_SINCAL_RESULT_1MIN where timestamp<="+DataBaseFactory.ConvertTimeString(EndTime,T_DBSource.DBType));
                    BeginTime = EndTime.AddMinutes(-PAR_T2);
                    string _mC_SINCAL_RESULT_1MIN = string.Format(@"select 
                                                                   avg(SINCAL_SIN_PV_MGO) SINCAL_SIN_PV_MGO,
                                                                   avg( SINCAL_SIN_PV_R) SINCAL_SIN_PV_R
                                                                   from MC_SINCAL_RESULT_1MIN where
                                                                    timestamp between {0} and {1}"
                                                              , DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType),
                                                               DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                    MC_SINCAL_RESULT_1MIN mC_SINCAL_RESULT_1MIN = iDataBase.GetList<MC_SINCAL_RESULT_1MIN>(_mC_SINCAL_RESULT_1MIN).FirstOrDefault();
                    double? PICAL_BREATH_PV_MGO = mC_SINCAL_RESULT_1MIN.SINCAL_SIN_PV_MGO;
                    double? PICAL_BREATH_PV_R = mC_SINCAL_RESULT_1MIN.SINCAL_SIN_PV_R;
                    model.PICAL_BREATH_PV_MGO = PICAL_BREATH_PV_MGO;
                    model.PICAL_BREATH_PV_R = PICAL_BREATH_PV_R;
                    return model;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message, typeof(Console_ReCalculate).Name);
                throw;
            }
        }
        /// <summary>
        /// 根据采集值得到计算值
        /// </summary>
        /// <returns></returns>
        public M_PICAL_BREATH_RESULT GetCalculateByCollection()
        {
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    DataTable source = iDataBase.GetDataTable(@"select 
                            PICAL_JPU,
                            PICAL_BREATH_SOL_BILL_1,
                          -- PICAL_BREATH_SOL_BILL_2,
                          --PICAL_BREATH_SOL_BILL_3,
                            PICAL_BREATH_COKE_BILL,
                            PICAL_BREATH_PV_MGO,
                            PICAL_BREATH_PV_R,--碱度
                                             --混合料水分特性
                            PICAL_BREATH_2M_NEX_WAT,
                            PICAL_BREATH_2M_GRIT_AVG,
                            PICAL_BREATH_BED_TH,--透气性和铺底料厚度相关性
                            PICAL_H,
                            PICAL_BREATH_AIM_TE,
                            PICAL_P,
                            PICAL_BREATH_MA_TE,
                            PICAL_BREATH_SRM_BILL,
                            PICAL_BREATH_COKE_GRI,
                            PICAL_RELAT_BR_BL_BLEND,
                            PICAL_BREATH_SPARE1
                            from M_PICAL_BREATH_RESULT where rownum between 1 and 100 order by timestamp desc
                            ");
                    DataTable M_PICAL_par = iDataBase.GetDataTable(@"select PAR_K1,PAR_K2,PAR_n,PAR_T2,PAR_T1,PAR_B from M_PICAL_par");
                    DataTable T_MA_PGD_PLC_1MIN = GetT_MA_PGD_PLC_1MIN();
                    DataTable T_CLOTH_PLC_1MIN = GetT_CLOTH_PLC_1MIN();
                    DataTable MD_PHY_PARTICLE_INFO_IN = GetMD_PHY_PARTICLE_INFO_IN();
                    DataTable T_BURN_COLE_PLC_1MIN = GetT_BURN_COLE_PLC_1MIN();
                    DataTable MC_MICAL_PAR = iDataBase.GetDataTable(@"select 
                        *    from MC_MICAL_PAR");
                    double? PICAL_BREATH_SOL_BILL_1 = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_SOL_BILL_1;
                    double? PICAL_BREATH_SOL_BILL_2 = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_SOL_BILL_2;
                    double? PICAL_BREATH_SOL_BILL_3 = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_SOL_BILL_3;
                    double? PICAL_BREATH_COKE_BILL = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_COKE_BILL;
                    double? PICAL_BREATH_PV_MGO = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_PV_MGO;
                    double? PICAL_BREATH_COKE_GRI = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_COKE_GRI;//焦炭粒度
                    double? PICAL_BREATH_PV_R = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_PV_R;
                    double? PICAL_BREATH_SRM_BILL = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_SRM_BILL;//烧返配比
                    double? PICAL_BREATH_SPARE1 = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_SPARE1;//烧返粒度
                    double PICAL_BREATH_BLE_TE = MD_PHY_PARTICLE_INFO_IN.Rows[0]["MATERIAL_TE"].ToDouble();//混合料温度
                    double PS_MA_SB_1_FLUE_TE = T_MA_PGD_PLC_1MIN.Rows[0]["PS_MA_SB_1_FLUE_TE"].ToDouble();
                    double PS_MA_SB_2_FLUE_TE = T_MA_PGD_PLC_1MIN.Rows[0]["PS_MA_SB_2_FLUE_TE"].ToDouble();
                    double PICAL_BREATH_2M_NEX_WAT = Get_T_BLEND_PLC_1MIN();//二混后水分
                    double PICAL_BREATH_MA_TE = (PS_MA_SB_1_FLUE_TE + PS_MA_SB_2_FLUE_TE) / 2;
                    double PICAL_BREATH_AIM_TE = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_AIM_TE"].ToDouble();//点火温度
                    double PICAL_BREATH_IG_01_TE = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_IG_01_TE"].ToDouble();//点火温度 1 
                    double PICAL_BREATH_IG_02_TE = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_IG_02_TE"].ToDouble();//点火温度  2 
                    double PICAL_BREATH_IG_03_TE = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_IG_03_TE"].ToDouble(); //点火温度 3
                    double PICAL_BREATH_SIN_MS_PV = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_SIN_MS_PV"].ToDouble();//台车速度
                    double C_THICK_PV = T_CLOTH_PLC_1MIN.Rows[0]["C_THICK_PV"].ToDouble();//布料厚度
                    //PICAL_BREATH_MA_TE
                    //<1
                    double PICAL_BREATH_2M_GRIT_1 = MD_PHY_PARTICLE_INFO_IN.Rows[0]["GRIT_LOW_1"].ToDouble();
                    //1-3
                    double PICAL_BREATH_2M_GRIT_1_3 = MD_PHY_PARTICLE_INFO_IN.Rows[0]["GRIT_1_3"].ToDouble();
                    //>3
                    double PICAL_BREATH_2M_GRIT_3 = MD_PHY_PARTICLE_INFO_IN.Rows[0]["GRIT_UP_3"].ToDouble();
                    double PICAL_BREATH_BED_TH = T_CLOTH_PLC_1MIN.Rows[0]["C_BED_MATERAL_TH"].ToDouble();
                    //平均粒度
                    double PICAL_BREATH_2M_GRIT_AVG = PICAL_BREATH_2M_GRIT_1 * 1 + PICAL_BREATH_2M_GRIT_1_3 * 1.5 + PICAL_BREATH_2M_GRIT_3 * 3;
                    //主抽1烧结大烟道烟气流量
                    double Q1 = T_MA_PGD_PLC_1MIN.Rows[0]["PS_MA_SB_1_FLUE_FT"].ToDouble();
                    //主抽2烧结大烟道烟气流量
                    double Q2 = T_MA_PGD_PLC_1MIN.Rows[0]["PS_MA_SB_2_FLUE_FT"].ToDouble();
                    double Q = Q1 + Q2;
                    //主抽1烧结大烟道压力(大烟道压力左)取平均
                    double P1 = T_MA_PGD_PLC_1MIN.Rows[0]["PS_MA_SB_1_FLUE_PT"].ToDouble();
                    //主抽2烧结大烟道压力(大烟道压力右)取平均
                    double P2 = T_MA_PGD_PLC_1MIN.Rows[0]["PS_MA_SB_2_FLUE_PT"].ToDouble();
                    double P = Math.Abs(P1 + P2)/2;
                    
                    double K1 = M_PICAL_par.Rows[0]["PAR_K1"].ToDouble();
                    double K2 = M_PICAL_par.Rows[0]["PAR_K2"].ToDouble();
                    double H = T_CLOTH_PLC_1MIN.Rows[0]["C_THICK_PV"].ToDouble();
                    double B = M_PICAL_par.Rows[0]["PAR_B"].ToDouble();
                    double L = MC_MICAL_PAR.Rows[0]["PAR_LENGTH_SIN"].ToDouble();
                    double A = B * L;
                    double n = M_PICAL_par.Rows[0]["PAR_n"].ToDouble();
                    double JPU = Q * (1 - K1) / (K2 * A) * (Math.Pow(H / P, n));
                    DataRow row = source.NewRow();
                    row["PICAL_JPU"] = JPU;
                    row["PICAL_BREATH_SOL_BILL_1"] = PICAL_BREATH_SOL_BILL_1;
                    // row["PICAL_BREATH_SOL_BILL_2"] = PICAL_BREATH_SOL_BILL_2;
                    // row["PICAL_BREATH_SOL_BILL_3"] = PICAL_BREATH_SOL_BILL_3;
                    row["PICAL_BREATH_COKE_BILL"] = PICAL_BREATH_COKE_BILL;
                    row["PICAL_BREATH_PV_MGO"] = PICAL_BREATH_PV_MGO;
                    row["PICAL_BREATH_PV_R"] = PICAL_BREATH_PV_R;
                    row["PICAL_BREATH_2M_NEX_WAT"] = PICAL_BREATH_2M_NEX_WAT;
                    row["PICAL_BREATH_2M_GRIT_AVG"] = PICAL_BREATH_2M_GRIT_AVG;
                    row["PICAL_BREATH_BED_TH"] = PICAL_BREATH_BED_TH;//铺底料厚度
                    row["PICAL_H"] = C_THICK_PV;
                    row["PICAL_BREATH_AIM_TE"] = PICAL_BREATH_AIM_TE;//点火温度
                    row["PICAL_P"] = P;
                    row["PICAL_BREATH_SRM_BILL"] = PICAL_BREATH_SRM_BILL;
                    row["PICAL_RELAT_BR_BL_BLEND"] = PICAL_BREATH_BLE_TE;
                    row["PICAL_BREATH_MA_TE"] = PICAL_BREATH_MA_TE;
                    row["PICAL_BREATH_COKE_GRI"] = PICAL_BREATH_COKE_GRI;//焦炭粒度
                    row["PICAL_BREATH_SPARE1"] = PICAL_BREATH_SPARE1;//烧返粒度
                    source.Rows.Add(row);
                    List<double> D = HistoryCalculate(source);
                    M_PICAL_BREATH_RESULT model = new M_PICAL_BREATH_RESULT();
                    model.PICAL_RELAT_BR_BL_FLUX = D[0];
                    model.PICAL_RELAT_BR_BL_FUEL = D[1];
                    model.PICAL_RELAT_BR_BL_MGO = D[2];
                    model.PICAL_RELAT_BR_R = D[3];
                    model.PICAL_RELAT_BR_WAT = D[4];
                    model.PICAL_RELAT_BR_MIX_GR = D[5];
                    model.PICAL_RELAT_BR_BED_TH = D[6];
                    model.PICAL_RELAT_BR_THICK = D[7];
                    model.PICAL_RELAT_BR_BL_IG_TE = D[8];
                    model.PICAL_RELAT_BR_FLUE_PT = D[9];
                    model.PICAL_RELAT_BR_MA_TE = D[10];
                    model.PICAL_RELAT_BR_BL_SINBACK = D[11];
                    model.PICAL_RELAT_BR_COKE_GRI = D[12];
                    model.PICAL_RELAT_BR_BL_BLEND = D[13];
                    model.PICAL_BREATH_SPARE11 = D[14];
                    return model;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message,typeof(Console_ReCalculate).Name);
                throw;
            }
        }
        /// <summary>
        /// 获取model对象 插入数据
        /// </summary>
        /// <param name="Max_PICAL_1M"></param>
        public void SqlInsert(DateTime Max_PICAL_1M)
        {
            string log = "";
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    DataTable M_PICAL_par = iDataBase.GetDataTable(@"select PAR_K1,PAR_K2,PAR_n,PAR_T2,PAR_T1,PAR_B from M_PICAL_par");
                    DataTable MC_MICAL_PAR = iDataBase.GetDataTable(@"select 
                        *    from MC_MICAL_PAR");
                    double Q1 = GetT_MA_PGD_PLC_1MIN().Rows[0]["PS_MA_SB_1_FLUE_FT"].ToDouble();
                    double Q2 = GetT_MA_PGD_PLC_1MIN().Rows[0]["PS_MA_SB_2_FLUE_FT"].ToDouble();
                    double Q = Q1 + Q2;
                    double T1 = GetT_MA_PGD_PLC_1MIN().Rows[0]["PS_MA_SB_1_FLUE_TE"].ToDouble();
                    double T2 = GetT_MA_PGD_PLC_1MIN().Rows[0]["PS_MA_SB_2_FLUE_TE"].ToDouble();
                    double P1 = GetT_MA_PGD_PLC_1MIN().Rows[0]["PS_MA_SB_1_FLUE_PT"].ToDouble();
                    double P2 = GetT_MA_PGD_PLC_1MIN().Rows[0]["PS_MA_SB_2_FLUE_PT"].ToDouble();
                    double P = Math.Abs(P1 + P2)/2;
                    double K1 = M_PICAL_par.Rows[0]["PAR_K1"].ToDouble();
                    double K2 = M_PICAL_par.Rows[0]["PAR_K2"].ToDouble();
                    double H = GetT_CLOTH_PLC_1MIN().Rows[0]["C_THICK_PV"].ToDouble();
                    double B = M_PICAL_par.Rows[0]["PAR_B"].ToDouble();
                    double L = MC_MICAL_PAR.Rows[0]["PAR_LENGTH_SIN"].ToDouble();
                    double A = B * L;
                    double n = M_PICAL_par.Rows[0]["PAR_n"].ToDouble();
                    double JPU = Q * (1 - K1) / (K2 * A) * (Math.Pow(H / P, n));
                    //垂直烧结速度
                    double PICAL_VER_SIN_SP = GetMC_BTPCAL_result_1min();
                    MD_PHY_PARTICLE_INFO_IN _mD_PHY_PARTICLE_INFO_IN = Get_MD_PHY_PARTICLE_INFO_IN();
                    double? PICAL_BREATH_1M_GRIT_1 = _mD_PHY_PARTICLE_INFO_IN.GRIT_LOW_1;
                    double? PICAL_BREATH_1M_GRIT_1_3 = _mD_PHY_PARTICLE_INFO_IN.GRIT_1_3;
                    double? PICAL_BREATH_1M_GRIT_3 = _mD_PHY_PARTICLE_INFO_IN.GRIT_UP_3;

                    DataTable MD_PHY_PARTICLE_INFO_IN = GetMD_PHY_PARTICLE_INFO_IN();
                    //<1
                    double PICAL_BREATH_2M_GRIT_1 = MD_PHY_PARTICLE_INFO_IN.Rows[0]["GRIT_LOW_1"].ToDouble();
                    //1-3
                    double PICAL_BREATH_2M_GRIT_1_3 = MD_PHY_PARTICLE_INFO_IN.Rows[0]["GRIT_1_3"].ToDouble();
                    //>3
                    double PICAL_BREATH_2M_GRIT_3 = MD_PHY_PARTICLE_INFO_IN.Rows[0]["GRIT_UP_3"].ToDouble();
                    //平均粒度
                    double PICAL_BREATH_2M_GRIT_AVG = PICAL_BREATH_2M_GRIT_1 * 1 + PICAL_BREATH_2M_GRIT_1_3 * 1.5 + PICAL_BREATH_2M_GRIT_3 * 3;
                    //温度
                    double PICAL_BREATH_BLE_TE = MD_PHY_PARTICLE_INFO_IN.Rows[0]["MATERIAL_TE"].ToDouble();
                    //一混填充率
                    double B_1M_PRE_BELT_W_1H_ = GetPICAL_1M_FILL_RATE();
                    //二混填充率
                    double? B_1M_NEX_BELT_W_2H_1 = GetT_BLEND_PLC_1MIN().B_1M_NEX_BELT_W_2H_1;
                    //二混加水量
                    double? PICAL_BREATH_2M_FT_PV = GetT_BLEND_PLC_1MIN().B_2M_FLOW_PV;
                    //二混转速
                    double? PICAL_BREATH_2M_MIX_RATE = GetT_BLEND_PLC_1MIN().B_2M_MIXER_RATE;
                    //透气性计算对应填充率
                    double? PICAL_BREATH_FILL_RATE = (B_1M_PRE_BELT_W_1H_ + B_1M_NEX_BELT_W_2H_1) / 2;
                    T_BLEND_PLC_1MIN t_BLEND_PLC_1MIN = Get_T_BLEND_PLC_1MIN_t7();
                    //一混加水
                    double? PICAL_BREATH_1M_FT_PV = t_BLEND_PLC_1MIN.B_1M_FT_PV;
                    //一混转速
                    double? PICAL_BREATH_1M_MIX_RATE = t_BLEND_PLC_1MIN.B_1M_MIXER_RATE;
                    //一混后水分
                    double PICAL_BREATH_1M_NEX_WAT = Get_B_1M_NEX_WATER_AVG();
                    //二混后水分
                    double PICAL_BREATH_2M_NEX_WAT = Get_T_BLEND_PLC_1MIN();
                    //溶剂配比1
                    double? PICAL_BREATH_SOL_BILL_1 = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_SOL_BILL_1;
                    //溶剂配比2
                    double? PICAL_BREATH_SOL_BILL_2 = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_SOL_BILL_2;
                    //溶剂配比3
                    double? PICAL_BREATH_SOL_BILL_3 = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_SOL_BILL_3;
                    //Mgo含量
                    double? PICAL_BREATH_PV_MGO = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_PV_MGO;
                    //碱度
                    double? PICAL_BREATH_PV_R = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_PV_R;
                    //混匀矿粒度
                    double? PICAL_BREATH_IRON_GRIT = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_IRON_GRIT;
                    //精矿配比
                    double? PICAL_BREATH_CON_BILL = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_CON_BILL;
                    //烧返配比
                    double? PICAL_BREATH_SRM_BILL = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_SRM_BILL;
                    //燃料配比
                    double? PICAL_BREATH_COKE_BILL = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_COKE_BILL;
                    //点火温度
                    DataTable T_BURN_COLE_PLC_1MIN = GetT_BURN_COLE_PLC_1MIN();
                    double PICAL_BREATH_AIM_TE = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_AIM_TE"].ToDouble();//点火温度
                    double PICAL_BREATH_IG_01_TE = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_IG_01_TE"].ToDouble();//点火温度 1 
                    double PICAL_BREATH_IG_02_TE = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_IG_02_TE"].ToDouble();//点火温度  2 
                    double PICAL_BREATH_IG_03_TE = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_IG_03_TE"].ToDouble(); //点火温度 3
                    double PICAL_BREATH_SIN_MS_PV = T_BURN_COLE_PLC_1MIN.Rows[0]["BC_SIN_MS_PV"].ToDouble();//台车速度
                    DataTable T_CLOTH_PLC_1MIN = GetT_CLOTH_PLC_1MIN();
                    //铺底料厚度
                    double PICAL_BREATH_BED_TH = T_CLOTH_PLC_1MIN.Rows[0]["C_BED_MATERAL_TH"].ToDouble(); ;
                    //主抽温度  PS_MA_SB_1_FLUE_FT+PS_MA_SB_2_FLUE_FT
                    double PICAL_BREATH_MA_TE = (T1+T2) / 2;
                    //焦炭粒度
                    double? PICAL_BREATH_COKE_GRI = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_COKE_GRI;
                    //烧返粒度
                    double? PICAL_BREATH_SPARE1 = GetMC_SINCAL_INTERFACE_TRIG().PICAL_BREATH_SPARE1;
                    M_PICAL_BREATH_RESULT m = GetCalculateByCollection();
                    M_PICAL_BREATH_RESULT model = new M_PICAL_BREATH_RESULT()
                    {
                        TIMESTAMP = Max_PICAL_1M,
                        PICAL_Q = Q,
                        PICAL_Q1 = Q1,
                        PICAL_Q2 = Q2,
                        PICAL_P = P,
                        PICAL_P1 = P1,
                        PICAL_P2 = P2,
                        PICAL_H = H,
                        PICAL_A = A,
                        PICAL_JPU = JPU,
                        PICAL_VER_SIN_SP = PICAL_VER_SIN_SP,
                        PICAL_BREATH_1M_GRIT_1 = PICAL_BREATH_1M_GRIT_1,
                        PICAL_BREATH_1M_GRIT_1_3 = PICAL_BREATH_1M_GRIT_1_3,
                        PICAL_BREATH_1M_GRIT_3 = PICAL_BREATH_1M_GRIT_3,
                        PICAL_BREATH_2M_GRIT_1 = PICAL_BREATH_2M_GRIT_1,
                        PICAL_BREATH_2M_GRIT_1_3 = PICAL_BREATH_2M_GRIT_1_3,
                        PICAL_BREATH_2M_GRIT_3 = PICAL_BREATH_2M_GRIT_3,
                        PICAL_BREATH_FILL_RATE = PICAL_BREATH_FILL_RATE,
                        PICAL_BREATH_1M_FT_PV = PICAL_BREATH_1M_FT_PV,//空
                        PICAL_BREATH_2M_FT_PV = PICAL_BREATH_2M_FT_PV,
                        PICAL_BREATH_1M_MIX_RATE = PICAL_BREATH_1M_MIX_RATE,
                        PICAL_BREATH_2M_MIX_RATE = PICAL_BREATH_2M_MIX_RATE,
                        PICAL_BREATH_1M_NEX_WAT = PICAL_BREATH_1M_NEX_WAT,
                        PICAL_BREATH_2M_NEX_WAT = PICAL_BREATH_2M_NEX_WAT,
                        PICAL_BREATH_2M_GRIT_AVG = PICAL_BREATH_2M_GRIT_AVG,
                        PICAL_BREATH_BLE_TE = PICAL_BREATH_BLE_TE,
                        PICAL_BREATH_SOL_BILL_1 = PICAL_BREATH_SOL_BILL_1,
                        PICAL_BREATH_SOL_BILL_2 = PICAL_BREATH_SOL_BILL_2,
                        PICAL_BREATH_SOL_BILL_3 = PICAL_BREATH_SOL_BILL_2,
                        PICAL_BREATH_IRON_GRIT = PICAL_BREATH_IRON_GRIT,//混匀矿粒度
                        PICAL_BREATH_CON_BILL = PICAL_BREATH_CON_BILL,//精矿配比
                        PICAL_BREATH_SRM_BILL = PICAL_BREATH_SRM_BILL,//烧返配比 空
                        PICAL_BREATH_COKE_BILL = PICAL_BREATH_COKE_BILL,//燃料配比
                        PICAL_BREATH_AIM_TE = PICAL_BREATH_AIM_TE,
                        PICAL_BREATH_IG_01_TE = PICAL_BREATH_IG_01_TE,
                        PICAL_BREATH_IG_02_TE = PICAL_BREATH_IG_02_TE,
                        PICAL_BREATH_IG_03_TE = PICAL_BREATH_IG_03_TE,
                        PICAL_BREATH_SIN_MS_PV = PICAL_BREATH_SIN_MS_PV,
                        PICAL_BREATH_PV_MGO = PICAL_BREATH_PV_MGO,
                        PICAL_BREATH_PV_R = PICAL_BREATH_PV_R,
                        PICAL_BREATH_BED_TH = PICAL_BREATH_BED_TH,//铺底料厚度  40
                        PICAL_BREATH_MA_TE = PICAL_BREATH_MA_TE,
                        PICAL_BREATH_COKE_GRI = PICAL_BREATH_COKE_GRI,
                        PICAL_BREATH_SPARE1 = PICAL_BREATH_SPARE1,
                        PICAL_RELAT_BR_BL_FLUX = m.PICAL_RELAT_BR_BL_FLUX,
                        PICAL_RELAT_BR_BL_FUEL = m.PICAL_RELAT_BR_BL_FUEL,
                        PICAL_RELAT_BR_BL_MGO = m.PICAL_RELAT_BR_BL_MGO,
                        PICAL_RELAT_BR_R = m.PICAL_RELAT_BR_R,
                        PICAL_RELAT_BR_WAT = m.PICAL_RELAT_BR_WAT,
                        PICAL_RELAT_BR_MIX_GR = m.PICAL_RELAT_BR_MIX_GR,
                        PICAL_RELAT_BR_BED_TH = m.PICAL_RELAT_BR_BED_TH,
                        PICAL_RELAT_BR_THICK = m.PICAL_RELAT_BR_THICK,
                        PICAL_RELAT_BR_BL_IG_TE = m.PICAL_RELAT_BR_BL_IG_TE,
                        PICAL_RELAT_BR_FLUE_PT = m.PICAL_RELAT_BR_FLUE_PT,
                        PICAL_RELAT_BR_MA_TE = m.PICAL_RELAT_BR_MA_TE,
                        PICAL_RELAT_BR_BL_SINBACK = m.PICAL_RELAT_BR_BL_SINBACK,
                        PICAL_RELAT_BR_COKE_GRI = m.PICAL_RELAT_BR_COKE_GRI,
                        PICAL_RELAT_BR_BL_BLEND = m.PICAL_RELAT_BR_COKE_GRI,
                        PICAL_BREATH_SPARE11 = m.PICAL_RELAT_BR_COKE_GRI,
                    };
                    string sql_ = string.Format(@"insert into M_PICAL_BREATH_RESULT
                             (
                                TIMESTAMP,--0
                                PICAL_Q,
                                PICAL_Q1,
                                PICAL_Q2,
                                PICAL_P,
                                PICAL_P1,
                                PICAL_P2,
                                PICAL_H,
                                PICAL_A,
                                PICAL_JPU,
                                PICAL_VER_SIN_SP,--10
                                PICAL_BREATH_1M_GRIT_1,
                                PICAL_BREATH_1M_GRIT_1_3,
                                PICAL_BREATH_1M_GRIT_3,
                                PICAL_BREATH_2M_GRIT_1,
                                PICAL_BREATH_2M_GRIT_1_3,
                                PICAL_BREATH_2M_GRIT_3,
                                PICAL_BREATH_FILL_RATE,
                                PICAL_BREATH_1M_FT_PV,
                                PICAL_BREATH_2M_FT_PV,
                                PICAL_BREATH_1M_MIX_RATE,--20
                                PICAL_BREATH_2M_MIX_RATE,
                                PICAL_BREATH_1M_NEX_WAT,
                                PICAL_BREATH_2M_NEX_WAT,
                                PICAL_BREATH_2M_GRIT_AVG,--24
                                PICAL_BREATH_BLE_TE,
                                PICAL_BREATH_SOL_BILL_1,
                                PICAL_BREATH_SOL_BILL_2,
                                PICAL_BREATH_SOL_BILL_3,
                                PICAL_BREATH_IRON_GRIT,
                                PICAL_BREATH_CON_BILL,
                                PICAL_BREATH_SRM_BILL,--30
                                PICAL_BREATH_COKE_BILL,
                                PICAL_BREATH_AIM_TE,
                                PICAL_BREATH_IG_01_TE,
                                PICAL_BREATH_IG_02_TE,
                                PICAL_BREATH_IG_03_TE,
                                PICAL_BREATH_SIN_MS_PV,
                                PICAL_BREATH_PV_MGO,
                                PICAL_BREATH_PV_R,
                                PICAL_BREATH_BED_TH,
                                PICAL_BREATH_MA_TE,--40
                                PICAL_BREATH_COKE_GRI,--41
                                PICAL_BREATH_SPARE1,--42
                               
                                PICAL_RELAT_BR_BL_FLUX,
                                PICAL_RELAT_BR_BL_FUEL,
                                PICAL_RELAT_BR_BL_MGO,
                                PICAL_RELAT_BR_R,
                                PICAL_RELAT_BR_WAT,
                                PICAL_RELAT_BR_MIX_GR,
                                PICAL_RELAT_BR_BED_TH,
                                PICAL_RELAT_BR_THICK,
                                PICAL_RELAT_BR_BL_IG_TE,
                                PICAL_RELAT_BR_FLUE_PT,
                                PICAL_RELAT_BR_MA_TE,
                                PICAL_RELAT_BR_BL_SINBACK,
                                PICAL_RELAT_BR_COKE_GRI,
                                PICAL_RELAT_BR_BL_BLEND,
                                PICAL_BREATH_SPARE11

                             )
                            values(
                                 {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},
                                                    {16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32}
                                   ,{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43},{44},{45},{46},{47},{48},{49},{50},{51},{52},{53},
                                    {54},{55},{56},{57},{58}
                                   )",
                       DataBaseFactory.ConvertTimeString(model.TIMESTAMP.ToDate(), T_DBSource.DBType),//0
                        model.PICAL_Q,//1
                        model.PICAL_Q1,//2
                        model.PICAL_Q2,//3
                        model.PICAL_P,
                        model.PICAL_P1,
                        model.PICAL_P2,
                        model.PICAL_H,
                        model.PICAL_A,
                        model.PICAL_JPU,
                        model.PICAL_VER_SIN_SP,//10
                        model.PICAL_BREATH_1M_GRIT_1,
                        model.PICAL_BREATH_1M_GRIT_1_3,
                        model.PICAL_BREATH_1M_GRIT_3,
                        model.PICAL_BREATH_2M_GRIT_1,
                        model.PICAL_BREATH_2M_GRIT_1_3,
                        model.PICAL_BREATH_2M_GRIT_3,
                        model.PICAL_BREATH_FILL_RATE,
                        model.PICAL_BREATH_1M_FT_PV,
                        model.PICAL_BREATH_2M_FT_PV,
                        model.PICAL_BREATH_1M_MIX_RATE,//20
                        model.PICAL_BREATH_2M_MIX_RATE,
                        model.PICAL_BREATH_1M_NEX_WAT,
                        model.PICAL_BREATH_2M_NEX_WAT,
                        model.PICAL_BREATH_2M_GRIT_AVG,
                        model.PICAL_BREATH_BLE_TE,
                        model.PICAL_BREATH_SOL_BILL_1,
                        model.PICAL_BREATH_SOL_BILL_2,
                        model.PICAL_BREATH_SOL_BILL_3,
                        model.PICAL_BREATH_IRON_GRIT,//混匀矿粒度
                        model.PICAL_BREATH_CON_BILL,//30精矿配比
                        model.PICAL_BREATH_SRM_BILL,//烧返配比
                        model.PICAL_BREATH_COKE_BILL,//燃料配比
                        model.PICAL_BREATH_AIM_TE,
                        model.PICAL_BREATH_IG_01_TE,
                        model.PICAL_BREATH_IG_02_TE,
                        model.PICAL_BREATH_IG_03_TE,
                        model.PICAL_BREATH_SIN_MS_PV,//37
                        model.PICAL_BREATH_PV_MGO,
                        model.PICAL_BREATH_PV_R,
                        model.PICAL_BREATH_BED_TH,//铺底料厚度  40
                        model.PICAL_BREATH_MA_TE,//主抽温度
                        model.PICAL_BREATH_COKE_GRI,//
                        model.PICAL_BREATH_SPARE1,//43

                        model.PICAL_RELAT_BR_BL_FLUX,//44
                        model.PICAL_RELAT_BR_BL_FUEL,
                        model.PICAL_RELAT_BR_BL_MGO,
                        model.PICAL_RELAT_BR_R,
                        model.PICAL_RELAT_BR_WAT,
                        model.PICAL_RELAT_BR_MIX_GR,
                        model.PICAL_RELAT_BR_BED_TH,//50
                        model.PICAL_RELAT_BR_THICK,
                        model.PICAL_RELAT_BR_BL_IG_TE,//52
                        model.PICAL_RELAT_BR_FLUE_PT,
                        model.PICAL_RELAT_BR_MA_TE,
                        model.PICAL_RELAT_BR_BL_SINBACK,
                        model.PICAL_RELAT_BR_COKE_GRI,
                        model.PICAL_RELAT_BR_BL_BLEND,
                        model.PICAL_BREATH_SPARE11//58
                        );
                    //执行sql
                    iDataBase.ExecuteCommand(sql_);
                    log = "执行成功";
                    Outputlog(log);
                }
            }
            catch (Exception ex)
            {
                log = string.Format("执行失败，失败原因{0}",ex.Message);
                Log.WriteLine(ex.Message, typeof(Console_ReCalculate).Name);
                throw;
            }
            finally
            {
                log = "";
            }

        }
    }
}
