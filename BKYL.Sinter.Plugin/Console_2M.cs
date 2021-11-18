using BFES.DataAccess;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;
using BKYL.Sinter.Plugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin
{
    /// <summary>
    /// 二混质粒参数 yanhao
    /// </summary>
    public class Console_2M : BaseJob
    {
        public override void InitTask()
        {       
        }
        public override void RunTask(DateTime currentTime)
        {
            string log = "";
            try
            {
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    //获取相关配置model
                    M_PICAL_PAR M_PICAL_PAR_ = GET_M_PICAL_PAR(iDataBase);
                    MC_MICAL_PAR MC_MICAL_PAR_ = GET_MC_MICAL_PAR(iDataBase);              
                    M_PICAL_2M_RESULT M_PICAL_2M_RESULT = GET_M_PICAL_2M_result(iDataBase);//获取最大时间
                    MD_PHY_PARTICLE_INFO_IN MD_PHY_PARTICLE_INFO_IN = GET_MD_PHY_PARTICLE_INFO_IN(iDataBase);//获取来源表信息                            
                    //如果来源表有新的数据插入
                    if (MD_PHY_PARTICLE_INFO_IN.TIMESTAMP > M_PICAL_2M_RESULT.TIMESTAMP)
                    {
                        
                        T_BLEND_PLC_1MIN T_BLEND_PLC_1MIN = GET_T_BLEND_PLC_1MIN(MC_MICAL_PAR_, M_PICAL_PAR_,iDataBase);//获取GET_T_BLEND_PLC_1MIN平均值
                       
                        MC_MICAL_RESULT MC_MICAL_RESULT_ = GET_MC_MICAL_RESULT(MD_PHY_PARTICLE_INFO_IN,iDataBase);//获取时间2M_1M_TIME
                        T_BLEND_PLC_1MIN T_BLEND_PLC_1MIN_M= GET_T_BLEND_PLC_1MIN_M(MC_MICAL_PAR_, MC_MICAL_RESULT_,M_PICAL_PAR_, iDataBase);//获取一混加水量 一混转速
                        
                        List<MC_SINCAL_INTERFACE_TRIG_MIN> MC_SINCAL_INTERFACE_TRIG_MIN = GET_MC_SINCAL_INTERFACE_TRIG_MIN(MC_MICAL_PAR_, MC_MICAL_RESULT_,M_PICAL_PAR_, iDataBase);//获取MC_SINCAL_INTERFACE_TRIG_MIN平均值(溶剂)                       
                           
                        MC_SINCAL_INTERFACE_TRIG_MIN MC_SINCAL_INTERFACE_TRIG_MIN_M = GET_MC_SINCAL_INTERFACE_TRIG_MIN_M(MC_MICAL_PAR_, MC_MICAL_RESULT_, M_PICAL_PAR_, iDataBase);//获取烧返配比                       
                     
                        MD_PHY_PARTICLE_INFO_IN MD_PHY_PARTICLE_INFO_IN_HY = GET_MD_PHY_PARTICLE_INFO_IN_HY(MC_MICAL_PAR_, MC_MICAL_RESULT_, M_PICAL_PAR_, iDataBase);//获取混匀矿粒度
                                
                        SET_SAVE(MD_PHY_PARTICLE_INFO_IN, T_BLEND_PLC_1MIN, T_BLEND_PLC_1MIN_M, MC_SINCAL_INTERFACE_TRIG_MIN, MC_SINCAL_INTERFACE_TRIG_MIN_M, MD_PHY_PARTICLE_INFO_IN_HY);
     
                    }
                }
                Outputlog(log);
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message + ex.StackTrace);
                throw;
            }
            finally
            {
                log = null;
            }

        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }

        /// <summary>
        /// 读取参数
        /// </summary>
        /// <param name="iDataBase"></param>
        /// <returns></returns>
        public M_PICAL_PAR GET_M_PICAL_PAR(IDataBase iDataBase)
        {
            string str_sql = string.Format("select * from M_PICAL_PAR");
            return iDataBase.GetSingle<M_PICAL_PAR>(str_sql);
        }

        public MC_MICAL_PAR GET_MC_MICAL_PAR(IDataBase iDataBase)
        {
            string str_sql = string.Format("select * from MC_MICAL_PAR");
            return iDataBase.GetSingle<MC_MICAL_PAR>(str_sql);
        }

        public MC_MICAL_RESULT GET_MC_MICAL_RESULT(MD_PHY_PARTICLE_INFO_IN MD_PHY_PARTICLE_INFO_IN, IDataBase iDataBase)
        {
            DateTime SAMPLTIME = MD_PHY_PARTICLE_INFO_IN.TIMESTAMP.ToDate();
            string str_sql = string.Format("select * from MC_MICAL_RESULT where TIMESTAMP=(select max(timestamp)  from MC_MICAL_RESULT where timestamp <= to_date('" + SAMPLTIME + "','yyyy-mm-dd hh24:mi:ss'))and datanum=2");
            return iDataBase.GetSingle<MC_MICAL_RESULT>(str_sql);
        }
        //获取最大时间   
        public M_PICAL_2M_RESULT GET_M_PICAL_2M_result(IDataBase iDataBase)
        {
            string str_sql = string.Format("select max(TIMESTAMP)  as TIMESTAMP from M_PICAL_2M_result");
            return iDataBase.GetSingle<M_PICAL_2M_RESULT>(str_sql);
        }
        public MD_PHY_PARTICLE_INFO_IN GET_MD_PHY_PARTICLE_INFO_IN(IDataBase iDataBase)
        {
            string str_sql = string.Format("select * from MD_PHY_PARTICLE_INFO_IN where TIMESTAMP=(select max(TIMESTAMP) from MD_PHY_PARTICLE_INFO_IN where 1=1 and L2_CODE =901)");
            return iDataBase.GetSingle<MD_PHY_PARTICLE_INFO_IN>(str_sql);
        }
        public T_BLEND_PLC_1MIN GET_T_BLEND_PLC_1MIN(MC_MICAL_PAR MC_MICAL_PAR_, M_PICAL_PAR M_PICAL_PAR_,IDataBase iDataBase)
        {
            DateTime maxDate = iDataBase.Queryable<T_BLEND_PLC_1MIN>().Max(m => m.TIMESTAMP).ToDate();//获取最大时间            
            double TIME_2M = MC_MICAL_PAR_.PAR_2M_U_TIME.ToDouble();//获取倒推分钟数
            DateTime DTmaxDate = maxDate.AddMinutes(-TIME_2M);//获取倒退最大时间
            double PAR_T1 = M_PICAL_PAR_.PAR_T1.ToDouble();//获取平均时间分钟数
            DateTime DTminDate = DTmaxDate.AddMinutes(-PAR_T1);//获取倒退最小时间
            string str_sql = string.Format("select  round(avg(B_1M_NEX_BELT_W_2H_1),3) B_1M_NEX_BELT_W_2H_1,round(avg(B_2M_TARGET_WATER), 3) B_2M_TARGET_WATER,round(avg(B_2M_FLOW_PV), 3) B_2M_FLOW_PV,  round(avg(B_2M_MIXER_RATE), 3) B_2M_MIXER_RATE  from T_BLEND_PLC_1MIN  where timestamp > = to_date('" + DTminDate + "','yyyy-mm-dd hh24:mi:ss')  and  timestamp < to_date('" + DTmaxDate + "','yyyy-mm-dd hh24:mi:ss')");
            return iDataBase.GetSingle<T_BLEND_PLC_1MIN>(str_sql);
        }
        public T_BLEND_PLC_1MIN GET_T_BLEND_PLC_1MIN_M(MC_MICAL_PAR MC_MICAL_PAR_, MC_MICAL_RESULT MC_MICAL_RESULT_, M_PICAL_PAR M_PICAL_PAR_, IDataBase iDataBase)
        {
            DateTime maxDate = iDataBase.Queryable<T_BLEND_PLC_1MIN>().Max(m => m.TIMESTAMP).ToDate();//获取最大时间            
            double TIME_2M = MC_MICAL_PAR_.PAR_2M_U_TIME.ToDouble();//获取倒推分钟数
            double TIME_1M__2M = MC_MICAL_RESULT_.MICAL_2M_1M_TIME.ToDouble();//获取倒推分钟数
            double T2 = (TIME_2M + TIME_1M__2M).ToDouble();
            DateTime DTmaxDate = maxDate.AddMinutes(-T2);//获取倒退最大时间
            double PAR_T1 = M_PICAL_PAR_.PAR_T1.ToDouble();//获取平均时间分钟数
            DateTime DTminDate = DTmaxDate.AddMinutes(-PAR_T1);//获取倒退最小时间
            string str_sql = string.Format("select  round(avg(B_1M_FT_PV),3) B_1M_FT_PV,round(avg(B_1M_MIXER_RATE), 3) B_1M_MIXER_RATE  from T_BLEND_PLC_1MIN  where timestamp > = to_date('" + DTminDate + "','yyyy-mm-dd hh24:mi:ss')  and  timestamp < to_date('" + DTmaxDate + "','yyyy-mm-dd hh24:mi:ss')");
            return iDataBase.GetSingle<T_BLEND_PLC_1MIN>(str_sql);
        }     
        public List<MC_SINCAL_INTERFACE_TRIG_MIN> GET_MC_SINCAL_INTERFACE_TRIG_MIN(MC_MICAL_PAR MC_MICAL_PAR_, MC_MICAL_RESULT MC_MICAL_RESULT_, M_PICAL_PAR M_PICAL_PAR_, IDataBase iDataBase)
        {
            DateTime maxDate = iDataBase.Queryable<MC_SINCAL_INTERFACE_TRIG_MIN>().Max(m => m.TIMESTAMP).ToDate();//获取最大时间            
            double TIME_2X = MC_MICAL_PAR_.PAR_2M_U_TIME.ToDouble();//获取倒推分钟数
            double TIME_1M_2M = MC_MICAL_RESULT_.MICAL_2M_1M_TIME.ToDouble();//获取倒推分钟数
            double TIME_1M_MAT = MC_MICAL_RESULT_.MICAL_1M_MAT_TIME.ToDouble();
            double T3 = (TIME_2X + TIME_1M_2M+ TIME_1M_MAT).ToDouble();
            DateTime DTmaxDate = maxDate.AddMinutes(-T3);//获取倒退最大时间
            double PAR_T1 = M_PICAL_PAR_.PAR_T1.ToDouble();//获取平均时间分钟数
            DateTime DTminDate = DTmaxDate.AddMinutes(-PAR_T1);//获取倒退最小时间
            string str_sql = string.Format("select sincal_cood, avg(SINCAL_ORE_SP) as SINCAL_ORE_SP from MC_SINCAL_INTERFACE_TRIG_MIN  where sincal_cood between  400  and 499 and timestamp between to_date('"+ DTminDate + "', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + DTmaxDate + "', 'yyyy-mm-dd hh24:mi:ss')group by sincal_cood");
            return iDataBase.GetList<MC_SINCAL_INTERFACE_TRIG_MIN>(str_sql);          
        }
        public MC_SINCAL_INTERFACE_TRIG_MIN GET_MC_SINCAL_INTERFACE_TRIG_MIN_M(MC_MICAL_PAR MC_MICAL_PAR_, MC_MICAL_RESULT MC_MICAL_RESULT_, M_PICAL_PAR M_PICAL_PAR_, IDataBase iDataBase)
        {
            DateTime maxDate = iDataBase.Queryable<MC_SINCAL_INTERFACE_TRIG_MIN>().Max(m => m.TIMESTAMP).ToDate();//获取最大时间            
            double TIME_2X = MC_MICAL_PAR_.PAR_2M_U_TIME.ToDouble();//获取倒推分钟数
            double TIME_1M_2M = MC_MICAL_RESULT_.MICAL_2M_1M_TIME.ToDouble();//获取倒推分钟数
            double TIME_1M_MAT = MC_MICAL_RESULT_.MICAL_1M_MAT_TIME.ToDouble();
            double T3 = (TIME_2X + TIME_1M_2M + TIME_1M_MAT).ToDouble();
            DateTime DTmaxDate = maxDate.AddMinutes(-T3);//获取倒退最大时间
            double PAR_T1 = M_PICAL_PAR_.PAR_T1.ToDouble();//获取平均时间分钟数
            DateTime DTminDate = DTmaxDate.AddMinutes(-PAR_T1);//获取倒退最小时间
            string str_sql = string.Format("select  avg(SINCAL_ORE_SP) as SINCAL_ORE_SP from MC_SINCAL_INTERFACE_TRIG_MIN where sincal_cood='601' and timestamp between to_date('" + DTminDate + "', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + DTmaxDate + "', 'yyyy-mm-dd hh24:mi:ss')");
            return iDataBase.GetSingle<MC_SINCAL_INTERFACE_TRIG_MIN>(str_sql);
        }
        public MD_PHY_PARTICLE_INFO_IN GET_MD_PHY_PARTICLE_INFO_IN_HY(MC_MICAL_PAR MC_MICAL_PAR_, MC_MICAL_RESULT MC_MICAL_RESULT_, M_PICAL_PAR M_PICAL_PAR_, IDataBase iDataBase)
        {
            DateTime maxDate = iDataBase.Queryable<MD_PHY_PARTICLE_INFO_IN>().Max(m => m.TIMESTAMP).ToDate();//获取最大时间            
            double TIME_2X = MC_MICAL_PAR_.PAR_2M_U_TIME.ToDouble();//获取倒推分钟数
            double TIME_1M_2M = MC_MICAL_RESULT_.MICAL_2M_1M_TIME.ToDouble();//获取倒推分钟数
            double TIME_1M_MAT = MC_MICAL_RESULT_.MICAL_1M_MAT_TIME.ToDouble();
            double T3 = (TIME_2X + TIME_1M_2M + TIME_1M_MAT).ToDouble();
            DateTime DTmaxDate = maxDate.AddMinutes(-T3);//获取倒退最大时间
            double PAR_T1 = M_PICAL_PAR_.PAR_T1.ToDouble();//获取平均时间分钟数
            DateTime DTminDate = DTmaxDate.AddMinutes(-PAR_T1);//获取倒退最小时间
            string str_sql = string.Format("select 100-avg(GRIT_UP_3) as GRIT_UP_3 from MD_PHY_PARTICLE_INFO_IN  where l2_code ='101' and timestamp between to_date('" + DTminDate + "', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + DTmaxDate + "', 'yyyy-mm-dd hh24:mi:ss')");
            return iDataBase.GetSingle<MD_PHY_PARTICLE_INFO_IN>(str_sql);
        }
        public void SET_SAVE(MD_PHY_PARTICLE_INFO_IN MD_PHY_PARTICLE_INFO_IN, T_BLEND_PLC_1MIN T_BLEND_PLC_1MIN, T_BLEND_PLC_1MIN T_BLEND_PLC_1MIN_M, List<MC_SINCAL_INTERFACE_TRIG_MIN> MC_SINCAL_INTERFACE_TRIG_MIN, MC_SINCAL_INTERFACE_TRIG_MIN MC_SINCAL_INTERFACE_TRIG_MIN_M, MD_PHY_PARTICLE_INFO_IN MD_PHY_PARTICLE_INFO_IN_HY)
        {
            string log = "";
            M_PICAL_2M_RESULT M_PICAL_2M_RESULT_M = null;
            M_PICAL_2M_RESULT_M = new M_PICAL_2M_RESULT();
            M_PICAL_2M_RESULT_M.TIMESTAMP = MD_PHY_PARTICLE_INFO_IN.TIMESTAMP.ToDate();
            M_PICAL_2M_RESULT_M.PICAL_2M_TIME = MD_PHY_PARTICLE_INFO_IN.TIMESTAMP.ToDate();
            M_PICAL_2M_RESULT_M.PICAL_2M_GRIT_1 = MD_PHY_PARTICLE_INFO_IN.GRIT_LOW_1.ToDouble();
            M_PICAL_2M_RESULT_M.PICAL_2M_GRIT_1_3 = MD_PHY_PARTICLE_INFO_IN.GRIT_1_3.ToDouble();
            M_PICAL_2M_RESULT_M.PICAL_2M_GRIT_3 = MD_PHY_PARTICLE_INFO_IN.GRIT_UP_3.ToDouble();

            M_PICAL_2M_RESULT_M.PICAL_2M_FILL_RATE = T_BLEND_PLC_1MIN.B_1M_NEX_BELT_W_2H_1.ToDouble();
            M_PICAL_2M_RESULT_M.PICAL_2M_TARGET_WATER = T_BLEND_PLC_1MIN.B_2M_TARGET_WATER.ToDouble();
            M_PICAL_2M_RESULT_M.PICAL_2M_FT_PV = T_BLEND_PLC_1MIN.B_2M_FLOW_PV.ToDouble();
            M_PICAL_2M_RESULT_M.PICAL_2M_MIXER_RATE = T_BLEND_PLC_1MIN.B_2M_MIXER_RATE.ToDouble();

            M_PICAL_2M_RESULT_M.PICAL_2M_1M_FT_PV = T_BLEND_PLC_1MIN_M.B_1M_FT_PV.ToDouble();
            M_PICAL_2M_RESULT_M.PICAL_2M_1M_MIXER_RATE = T_BLEND_PLC_1MIN_M.B_1M_MIXER_RATE.ToDouble();


            if (MC_SINCAL_INTERFACE_TRIG_MIN.Count == 1)
            {
                M_PICAL_2M_RESULT_M.PICAL_2M_SOL_BILL_1 = MC_SINCAL_INTERFACE_TRIG_MIN[0].SINCAL_ORE_SP.ToDouble();
            }
            if (MC_SINCAL_INTERFACE_TRIG_MIN.Count == 2)
            {
                M_PICAL_2M_RESULT_M.PICAL_2M_SOL_BILL_1 = MC_SINCAL_INTERFACE_TRIG_MIN[0].SINCAL_ORE_SP.ToDouble();
                M_PICAL_2M_RESULT_M.PICAL_2M_SOL_BILL_2 = MC_SINCAL_INTERFACE_TRIG_MIN[1].SINCAL_ORE_SP.ToDouble();
            }
            if (MC_SINCAL_INTERFACE_TRIG_MIN.Count >= 3)
            {
                M_PICAL_2M_RESULT_M.PICAL_2M_SOL_BILL_1 = MC_SINCAL_INTERFACE_TRIG_MIN[0].SINCAL_ORE_SP.ToDouble();
                M_PICAL_2M_RESULT_M.PICAL_2M_SOL_BILL_2 = MC_SINCAL_INTERFACE_TRIG_MIN[1].SINCAL_ORE_SP.ToDouble();
                M_PICAL_2M_RESULT_M.PICAL_2M_SOL_BILL_3 = MC_SINCAL_INTERFACE_TRIG_MIN[2].SINCAL_ORE_SP.ToDouble();
            }

            M_PICAL_2M_RESULT_M.PICAL_RE_BILL_DRY = MC_SINCAL_INTERFACE_TRIG_MIN_M.SINCAL_ORE_SP.ToDouble();
            M_PICAL_2M_RESULT_M.PICAL_2M_IRON_GRIT = MD_PHY_PARTICLE_INFO_IN_HY.GRIT_UP_3.ToDouble();  
                     
            if (Set_Admin_add(M_PICAL_2M_RESULT_M))
            {
                log = "保存完成";
            }
            else
            {
                log = "保存失败";
            }
            Outputlog(log);

        }
        public bool Set_Admin_add(M_PICAL_2M_RESULT model)
        {
            bool listmodel = true;

            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                listmodel = (bool)iDataBase.Insert(model);
                return listmodel;
            }
        }



    }
}
