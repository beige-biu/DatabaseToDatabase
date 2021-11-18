using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BFES.DataAccess;
using System.Data;
using BKYL.Sinter.Plugin.Models;
using BKYL.Jobs.Base;
using BKYL.Jobs.Base.Utils;

namespace BKYL.Sinter.Plugin
{
    public class SinterOutHistory : BaseJob
    {
        //烧结矿产量曲线
        public override void InitTask()
        {
            //加载配置yanhao/lihaisen(修改)
        }
        public override void RunTask(DateTime currentTime)
        {
            return;//已经在产量模型中实现了zyh
            DateTime timed = DateTime.Now.Date.ToString().ToDate().AddHours(-4);
            DateTime timedd = DateTime.Now.Date.ToString().ToDate().AddHours(8);

            DateTime timen = DateTime.Now.Date.ToString().ToDate().AddHours(8);
            DateTime timenn = DateTime.Now.Date.ToString().ToDate().AddHours(20);

            DateTime timea = DateTime.Now.Date.ToString().ToDate().AddHours(-4);
            DateTime timeal = DateTime.Now.Date.ToString().ToDate().AddHours(20);
         
            if((currentTime.Hour == 20 && currentTime.Minute > 0)|| currentTime.Hour > 20)
            {
                timed = timed.AddDays(1);
                timedd = timedd.AddDays(1);

                timen = timen.AddDays(1);
                timenn = timenn.AddDays(1);

                timea = timea.AddDays(1);
                timeal = timeal.AddDays(1);      

            }
                string log = "";
            MC_POPCAL_RESULT_DAY list = null;

            MC_SINCAL_RESULT_1MIN list_t_d = null;
            MC_SINCAL_RESULT_1MIN list_t_n = null;
            MC_SINCAL_RESULT_1MIN list_t_all = null;

            T_BURN_COLE_PLC_1MIN list_a_d = null;
            T_BURN_COLE_PLC_1MIN list_a_n = null;
            T_BURN_COLE_PLC_1MIN list_a_all = null;

            List<T_MATCH_PLC_1MIN> list_T_MATCH_PLC_1MIN = new List<T_MATCH_PLC_1MIN>();

            string str_sql = null;
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {

                //str_sql = string.Format("Select nvl(POPCAL_D_ALL_OUT_PL,0) as POPCAL_D_ALL_OUT_PL,nvl(POPCAL_D_OUT_PL,0) as POPCAL_D_OUT_PL,nvl(POPCAL_D_N_OUT_PL,0) as POPCAL_D_N_OUT_PL from MC_POPCAL_RESULT_DAY  where timestamp = (select max(timestamp) from MC_POPCAL_RESULT_DAY where  trunc(timestamp) = trunc(sysdate))");
                str_sql = string.Format("Select nvl(POPCAL_D_ALL_OUT_PL,0) as POPCAL_D_ALL_OUT_PL,nvl(POPCAL_D_OUT_PL,0) as POPCAL_D_OUT_PL,nvl(POPCAL_D_N_OUT_PL,0) as POPCAL_D_N_OUT_PL from MC_POPCAL_RESULT_DAY  where timestamp = (select max(timestamp) from MC_POPCAL_RESULT_DAY where timestamp >  to_date('" + timea + "','yyyy-mm-dd hh24:mi:ss')  and  timestamp <= to_date('" + timeal + "','yyyy-mm-dd hh24:mi:ss') )");
                list = iDataBase.GetSingle<MC_POPCAL_RESULT_DAY>(str_sql);//获取DAY表最新一条数据（计划产量的值）

            
                str_sql = string.Format("select NVL(SUM(SINCAL_OUTPUT_PV) ,0) AS SINCAL_OUTPUT_PV   from MC_SINCAL_result_1min    where timestamp >  to_date('" + timed + "','yyyy-mm-dd hh24:mi:ss')  and  timestamp <= to_date('" + timedd + "','yyyy-mm-dd hh24:mi:ss')");
                list_t_d = iDataBase.GetSingle<MC_SINCAL_RESULT_1MIN>(str_sql);//获取理论夜班

                str_sql = string.Format("select  NVL(SUM(SINCAL_OUTPUT_PV) ,0) AS SINCAL_OUTPUT_PV from MC_SINCAL_result_1min    where timestamp >  to_date('" + timen + "','yyyy-mm-dd hh24:mi:ss')  and  timestamp <= to_date('" + timenn + "','yyyy-mm-dd hh24:mi:ss')");
                list_t_n = iDataBase.GetSingle<MC_SINCAL_RESULT_1MIN>(str_sql);//获取理论白班

                str_sql = string.Format("select  NVL(SUM(SINCAL_OUTPUT_PV) ,0) AS SINCAL_OUTPUT_PV from MC_SINCAL_result_1min    where timestamp >  to_date('" + timea + "','yyyy-mm-dd hh24:mi:ss')  and  timestamp <= to_date('" + timeal + "','yyyy-mm-dd hh24:mi:ss')");
                list_t_all = iDataBase.GetSingle<MC_SINCAL_RESULT_1MIN>(str_sql);//获取理论全天


                str_sql = string.Format("select  round(NVL(SUM(SPARE32),0),3) AS SPARE32  from T_BURN_COLE_PLC_1MIN    where  SPARE32>0 and timestamp >  to_date('" + timed + "','yyyy-mm-dd hh24:mi:ss')  and  timestamp <= to_date('" + timedd + "','yyyy-mm-dd hh24:mi:ss')");
                list_a_d = iDataBase.GetSingle<T_BURN_COLE_PLC_1MIN>(str_sql);//获取实际夜班

                str_sql = string.Format("select  round(NVL(SUM(SPARE32) ,0),3) AS SPARE32  from T_BURN_COLE_PLC_1MIN    where SPARE32>0  and timestamp >  to_date('" + timen + "','yyyy-mm-dd hh24:mi:ss')  and  timestamp <= to_date('" + timenn + "','yyyy-mm-dd hh24:mi:ss')");
                list_a_n = iDataBase.GetSingle<T_BURN_COLE_PLC_1MIN>(str_sql);//获取实际白班

                str_sql = string.Format("select  round(NVL(SUM(SPARE32) ,0),3) AS SPARE32  from T_BURN_COLE_PLC_1MIN    where SPARE32>0  and timestamp >  to_date('" + timea + "','yyyy-mm-dd hh24:mi:ss')  and  timestamp <= to_date('" + timeal + "','yyyy-mm-dd hh24:mi:ss')");
                list_a_all = iDataBase.GetSingle<T_BURN_COLE_PLC_1MIN>(str_sql);//获取实际全天

                DateTime dtime1 = DateTime.Now.AddMinutes(-5);
                DateTime dtime2 = DateTime.Now;
                string str_col = "timestamp,M_ACTUAL_W_1,M_ACTUAL_W_2,M_ACTUAL_W_3,M_ACTUAL_W_4,M_ACTUAL_W_5,M_ACTUAL_W_6,M_ACTUAL_W_7,M_ACTUAL_W_8,M_ACTUAL_W_9,M_ACTUAL_W_10,M_ACTUAL_W_11,M_ACTUAL_W_12,M_ACTUAL_W_13,M_ACTUAL_W_14,M_ACTUAL_W_15,M_ACTUAL_W_16,M_ACTUAL_W_17,M_ACTUAL_W_18,M_ACTUAL_W_19";
                str_sql = string.Format(" select {0} from  T_MATCH_PLC_1MIN   where timestamp >  to_date('" + dtime1 + "','yyyy-mm-dd hh24:mi:ss')  and  timestamp <= to_date('" + dtime2 + "','yyyy-mm-dd hh24:mi:ss')", str_col, dtime1, dtime2);
                list_T_MATCH_PLC_1MIN = iDataBase.GetList<T_MATCH_PLC_1MIN>(str_sql);
                double pOPCAL_D_SIN_RM = jsPOPCAL_D_SIN_RM(list_T_MATCH_PLC_1MIN);

                str_sql = string.Format(" select round(NVL(AVG(SINCAL_MIX_PV_LOT),0),3) AS SINCAL_MIX_PV_LOT from  MC_SINCAL_result_1min   where timestamp >  to_date('" + dtime1 + "','yyyy-mm-dd hh24:mi:ss')  and  timestamp <= to_date('" + dtime2 + "','yyyy-mm-dd hh24:mi:ss')", str_col, dtime1, dtime2);
                double pOPCAL_D_LOT = iDataBase.GetDouble(str_sql);

                iDataBase.BeginTran();
                try
                {
                    if (list != null)
                    {
                        str_sql = string.Format("insert into MC_POPCAL_OUT_CURVE(TIMESTAMP,P_AL_OUTPUT,P_D_OUTPUT,P_N_OUTPUT,T_AL_OUTPUT,T_D_OUTPUT,T_N_OUTPUT,I_AL_OUTPUT,I_D_OUTPUT,I_N_OUTPUT,POPCAL_D_SIN_RM,POPCAL_D_LOT)values(to_date('" + currentTime + "','yyyy-mm-dd hh24:mi:ss'),'" + list.POPCAL_D_ALL_OUT_PL + "','" + list.POPCAL_D_OUT_PL + "','" + list.POPCAL_D_N_OUT_PL + "','" + list_t_all.SINCAL_OUTPUT_PV + "','" + list_t_n.SINCAL_OUTPUT_PV + "','" + list_t_d.SINCAL_OUTPUT_PV + "','" + list_a_all.SPARE32 + "','" + list_a_n.SPARE32 + "','" + list_a_d.SPARE32 + "','{0}','{1}')", pOPCAL_D_SIN_RM, pOPCAL_D_LOT);
                        
                    }
                    else
                    {
                        str_sql = string.Format("insert into MC_POPCAL_OUT_CURVE(TIMESTAMP,P_AL_OUTPUT,P_D_OUTPUT,P_N_OUTPUT,T_AL_OUTPUT,T_D_OUTPUT,T_N_OUTPUT,I_AL_OUTPUT,I_D_OUTPUT,I_N_OUTPUT,POPCAL_D_SIN_RM,POPCAL_D_LOT)values(to_date('" + currentTime + "','yyyy-mm-dd hh24:mi:ss'),'0','0','0','" + list_t_all.SINCAL_OUTPUT_PV + "','" + list_t_n.SINCAL_OUTPUT_PV + "','" + list_t_d.SINCAL_OUTPUT_PV + "','" + list_a_all.SPARE32 + "','" + list_a_n.SPARE32 + "','" + list_a_d.SPARE32 + "','{0}','{1}')", pOPCAL_D_SIN_RM, pOPCAL_D_LOT);                    
                    }
                    int aa = iDataBase.ExecuteCommand(str_sql);
                        if (aa > 0)
                        {
                            iDataBase.CommitTran();
                            log = string.Format("执行成功");
                        }

                   
                }
                catch (Exception ex)
                {
                    iDataBase.RollbackTran();
                    log = string.Format("执行失败。原因：{0}", ex.Message);
                }
                finally
                {
                    Outputlog(log);

                    log = null;
                }
            }
        }


        /// <summary>
        /// 历史烧结返矿百分比平均值
        /// </summary>
        /// <param name="list2"></param>
        /// <returns></returns>
        private double jsPOPCAL_D_SIN_RM(List<T_MATCH_PLC_1MIN> list2)
        {
            double pjz = 0;
            //(M_ACTUAL_W_T_8 +M_ACTUAL_W_T_9)/(M_ACTUAL_W_T_1 +M_ACTUAL_W_T_2+M_ACTUAL_W_T_3+M_ACTUAL_W_T_4+M_ACTUAL_W_T_5+M_ACTUAL_W_T_6+M_ACTUAL_W_T_7+M_ACTUAL_W_T_8+M_ACTUAL_W_T_9+M_ACTUAL_W_T_10+M_ACTUAL_W_T_11+M_ACTUAL_W_T_12+M_ACTUAL_W_T_13+M_ACTUAL_W_T_14+M_ACTUAL_W_T_15+M_ACTUAL_W_T_16+M_ACTUAL_W_T_17+M_ACTUAL_W_T_18+M_ACTUAL_W_T_19 ))*100
            double mm = 0;
            double zz = 0;
            int numCount = 0;
            foreach (var item in list2)
            {
                mm = (item.M_ACTUAL_W_8 + item.M_ACTUAL_W_9);
                zz = (item.M_ACTUAL_W_1 + item.M_ACTUAL_W_2 + item.M_ACTUAL_W_3 + item.M_ACTUAL_W_4 + item.M_ACTUAL_W_5 + item.M_ACTUAL_W_6 + item.M_ACTUAL_W_7 + item.M_ACTUAL_W_8 + item.M_ACTUAL_W_9 + item.M_ACTUAL_W_10 + item.M_ACTUAL_W_11 + item.M_ACTUAL_W_12 + item.M_ACTUAL_W_13 + item.M_ACTUAL_W_14 + item.M_ACTUAL_W_15 + item.M_ACTUAL_W_16 + item.M_ACTUAL_W_17 + item.M_ACTUAL_W_18 + item.M_ACTUAL_W_19);
                if (mm != 0 && zz != 0)
                {
                    pjz += (mm / zz) * 100;
                    numCount++;
                }
                

            }
            if (numCount != 0)
            {
                return Math.Round(pjz / numCount, 3);
            }
            else
            {
                return 0;
            }
        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }
    }
}
