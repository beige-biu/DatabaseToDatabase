using BFES.DataAccess;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin
{
    /// <summary>
    /// 1min存一次 赵飞
    /// </summary>
    public class ConSole_M_PICAL_BREATH_RESULT_1MIN : BaseJob
    {
        /// <summary>
        /// 目标表的最大时间
        /// </summary>
        private DateTime M_PICAL_BREATH_RESULT_1MIN_MaxTime { get; set; }
        public override void InitTask()
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                M_PICAL_BREATH_RESULT_1MIN_MaxTime = iDataBase.GetDateTime("select Max(TimeStamp) from M_PICAL_BREATH_RESULT_1MIN");
            }
        }

        public override void RunTask(DateTime currentTime)
        {
            string log = string.Empty;
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                  DateTime  T_MA_PGD_PLC_1MIN_MaxTime = iDataBase.GetDateTime("select Max(TimeStamp) from T_MA_PGD_PLC_1MIN");
                    //来源表的最大时间大于目标表 证明有新的数据插入
                    double PAR_CY = GetPar_CY();
                    TimeSpan sp = T_MA_PGD_PLC_1MIN_MaxTime - M_PICAL_BREATH_RESULT_1MIN_MaxTime;
                    if (sp.TotalMinutes>=PAR_CY)
                    {
                        DataTable MC_MICAL_PAR = iDataBase.GetDataTable(@"select 
                        *    from MC_MICAL_PAR");
                        DataTable M_PICAL_par = iDataBase.GetDataTable(@"select PAR_K1,PAR_K2,PAR_n,PAR_T2,PAR_T1,PAR_B from M_PICAL_par");
                        double PAR_T2 = M_PICAL_par.Rows[0]["PAR_T2"].ToDouble();
                        //从来源表的最大时间开始向前推PAR_T2
                        DateTime EndTime = iDataBase.GetDateTime("select Max(TimeStamp) TimeStamp from T_MA_PGD_PLC_1MIN");
                        DateTime BeginTime = EndTime.AddMinutes(-PAR_T2);
                        string sql = @"select round(avg(PS_MA_SB_1_FLUE_FT),3) PS_MA_SB_1_FLUE_FT,--p1
                                      round(avg(PS_MA_SB_2_FLUE_FT),3) PS_MA_SB_2_FLUE_FT,--p2
                                      round(avg(PS_MA_SB_1_FLUE_PT),3) PS_MA_SB_1_FLUE_PT,--q1
                                      round(avg(PS_MA_SB_2_FLUE_PT),3) PS_MA_SB_2_FLUE_PT,--q2
                                                 round(avg(PS_MA_SB_1_FLUE_TE),3)PS_MA_SB_1_FLUE_TE,--主抽1温度
                                                 round(avg(PS_MA_SB_2_FLUE_TE),3) PS_MA_SB_2_FLUE_TE--主抽2温度
                                    from T_MA_PGD_PLC_1MIN where timestamp<=" + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType) + " and timestamp>=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType);
                        DataTable T_MA_PGD_PLC_1MIN = iDataBase.GetDataTable(sql);
                        string aa = string.Format("select avg(C_THICK_PV) C_THICK_PV from T_CLOTH_PLC_1MIN where timestamp>=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) + " and timestamp<=" + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                        DataTable T_CLOTH_PLC_1MIN = iDataBase.GetDataTable(@"select round(avg(C_THICK_PV),3) C_THICK_PV from T_CLOTH_PLC_1MIN where timestamp>=" + DataBaseFactory.ConvertTimeString(BeginTime, T_DBSource.DBType) + " and timestamp<=" + DataBaseFactory.ConvertTimeString(EndTime, T_DBSource.DBType));
                        //主抽1烧结大烟道烟气流量
                        double Q1 = T_MA_PGD_PLC_1MIN.Rows[0]["PS_MA_SB_1_FLUE_FT"].ToDouble();
                        //主抽2烧结大烟道烟气流量
                        double Q2 = T_MA_PGD_PLC_1MIN.Rows[0]["PS_MA_SB_2_FLUE_FT"].ToDouble();
                        double Q = Q1/60 + Q2/60;
                        //主抽1烧结大烟道压力(大烟道压力左)取平均
                        double P1 = T_MA_PGD_PLC_1MIN.Rows[0]["PS_MA_SB_1_FLUE_PT"].ToDouble();
                        //主抽2烧结大烟道压力(大烟道压力右)取平均
                        double P2 = T_MA_PGD_PLC_1MIN.Rows[0]["PS_MA_SB_2_FLUE_PT"].ToDouble();
                        double P =Math.Abs(P1 + P2)/2;
                        double K1 = M_PICAL_par.Rows[0]["PAR_K1"].ToDouble();
                        double K2 = M_PICAL_par.Rows[0]["PAR_K2"].ToDouble();
                        double H = T_CLOTH_PLC_1MIN.Rows[0]["C_THICK_PV"].ToDouble();
                        double B = M_PICAL_par.Rows[0]["PAR_B"].ToDouble();
                        double L = MC_MICAL_PAR.Rows[0]["PAR_LENGTH_SIN"].ToDouble();
                        double A = B * L;
                        double n = M_PICAL_par.Rows[0]["PAR_n"].ToDouble();
                        double a = Math.Pow((H /P), n);
                        double JPU = K2 * ((Q * (1 - K1)) / A) * (Math.Pow(H / (P * 1000), n));
                        if (P==0)
                        {
                            log = "分母为0不进行计算";
                            return;
                        }

                        string _sql = string.Format(@"insert into M_PICAL_BREATH_RESULT_1MIN
                                  (TIMESTAMP,
                                    PICAL_Q,
                                    PICAL_Q1,
                                    PICAL_Q2,
                                    PICAL_P,
                                    PICAL_P1,
                                    PICAL_P2,
                                    PICAL_H,
                                    PICAL_A,
                                    PICAL_JPU
                                    )
                                    values
                                     (
                                      {0},
                                      {1},
                                      {2},
                                      {3},
                                      {4},
                                      {5},
                                      {6},
                                      {7},
                                      {8},
                                      {9}
                                     )    
                                ", DataBaseFactory.ConvertTimeString(T_MA_PGD_PLC_1MIN_MaxTime, T_DBSource.DBType),
                                    Q, Q1, Q2, P, P1, P2, H, A, JPU
                                    );
                        iDataBase.ExecuteCommand(_sql);
                        M_PICAL_BREATH_RESULT_1MIN_MaxTime = T_MA_PGD_PLC_1MIN_MaxTime;
                        log = "执行成功";
                        Outputlog(log);
                    }
                   
                }
            }
            catch (Exception ex)
            {
                log = string.Format("执行失败,失败原因{0}", ex.Message);
                Log.WriteLine(ex.Message, typeof(ConSole_M_PICAL_BREATH_RESULT_1MIN).Name);
                throw;
            }
            finally
            {
                log = string.Empty;
            }
        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }
        private double GetPar_CY()
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
               double PAR_CY=iDataBase.GetDouble("select PAR_CY from M_PICAL_par");
                return PAR_CY;
            }
               
        }
    }
}
