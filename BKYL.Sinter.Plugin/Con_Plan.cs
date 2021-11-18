
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
{
   /// <summary>
   /// 按条件读取月计划
   /// </summary>
    public class Con_Plan : BaseJob
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
                  List<TPSIPDP_IOOP> TPSIPDP_IOOP_= GET_TPSIPDP_IOOP(iDataBase);
                    if (TPSIPDP_IOOP_.Count>0)
                    {
                        set_save(TPSIPDP_IOOP_);
                    }
                    cal_plan_day(iDataBase, currentTime);



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

        public List<TPSIPDP_IOOP> GET_TPSIPDP_IOOP(IDataBase iDataBase)
        {
            string str_sql = string.Format("select * from TPSIPDP_IOOP where PROC_UNIT = 'S' and UNIT_NO = 6 and flag1='0'");

            return iDataBase.GetList<TPSIPDP_IOOP>(str_sql);
        }
        public List<TPSIPDP_IOOP_PLAN> GET_TPSIPDP_IOOP_PLAN(IDataBase iDataBase)
        {
           string str_sql = "select * from TPSIPDP_IOOP_PLAN  order by PROD_MON desc";
           return iDataBase.GetList<TPSIPDP_IOOP_PLAN>(str_sql);
        }
        public void cal_plan_day(IDataBase iDataBase, DateTime currentTime)
        {
            MD_PLAN_OUTPUT_IN MD_MD_PLAN_OUTPUT_IN_ = null;
            MD_MD_PLAN_OUTPUT_IN_ = new MD_PLAN_OUTPUT_IN();
            DateTime starttime;
            DateTime endtime;
            DateTime endtime1;
            string M_Hour = currentTime.Hour.ToString();
            string M_Min = currentTime.Minute.ToString();
            int M_DAY1 = 0;
            int M_DAY2 = 0;
            DateTime TIME=DateTime.MinValue;
            DateTime TIME1 = DateTime.MinValue;
            double M_PLAN_OUTPUT_DAY = 0;
            double PROD_ITEM_VALUE =0;
            if (M_Hour == "20" && M_Min == "1")
            {
                List<TPSIPDP_IOOP_PLAN> TPSIPDP_IOOP_PLAN_ = GET_TPSIPDP_IOOP_PLAN(iDataBase);

            DateTime bi = DateTime.Now;
            int Dqmoe = bi.Month;
            int DDAY = bi.Day;
            int jqmoe = TPSIPDP_IOOP_PLAN_[0].PROD_MON.ToDate().Month;
            if ((Dqmoe != jqmoe) && (DDAY < 25))
            {
                 TIME = TPSIPDP_IOOP_PLAN_[1].PROD_MON.ToDate().AddMonths(-1);
                 TIME1 = TPSIPDP_IOOP_PLAN_[1].PROD_MON.ToDate();
                PROD_ITEM_VALUE = TPSIPDP_IOOP_PLAN_[1].PROD_ITEM_VALUE;
            }
           else if ((Dqmoe == jqmoe) && (DDAY < 25))
            {
                TIME = TPSIPDP_IOOP_PLAN_[0].PROD_MON.ToDate().AddMonths(-1);
                TIME1 = TPSIPDP_IOOP_PLAN_[0].PROD_MON.ToDate();
                PROD_ITEM_VALUE = TPSIPDP_IOOP_PLAN_[0].PROD_ITEM_VALUE;
            }
            else if ((Dqmoe != jqmoe) && (DDAY > 25))
            {
                TIME = TPSIPDP_IOOP_PLAN_[0].PROD_MON.ToDate().AddMonths(-1);
                TIME1 = TPSIPDP_IOOP_PLAN_[0].PROD_MON.ToDate();
                PROD_ITEM_VALUE = TPSIPDP_IOOP_PLAN_[0].PROD_ITEM_VALUE;
            }
                int M_Month_ = TIME.Month.ToInt();
                int M_year = TIME.Year.ToInt();
                int DAT = MonthsByDay(M_year, M_Month_);
                int DAT_25_END = 0;

                if (TPSIPDP_IOOP_PLAN_ != null)
                {
                    string M_Day = currentTime.Day.ToString();
                    int M_Day1_1 = currentTime.Day.ToInt();
                    M_DAY1 = DAT - 25;//计算执行月份前一个月剩余天数
                    M_DAY2 = M_DAY1 + 25;//计算执行月份周期的天数
                                         //判断是否是每月的最后一天，如果是，则计算下个月开始第一天的计划产量
                    if (M_Day == "25")
                    {
                        M_PLAN_OUTPUT_DAY = PROD_ITEM_VALUE / M_DAY2;
                    }
                    else
                    {
                        string a = TIME.Year.ToString() + "-" + TIME.Month.ToString() + "-25" + " 20:00:01";
                        starttime = Convert.ToDateTime(TIME.Year.ToString() + "-" + TIME.Month.ToString() + "-25" + " 20:00:01");
                        endtime1 = Convert.ToDateTime(TIME1.Year.ToString() + "-" + TIME1.ToDate().Month.ToString() + "-25" + " 20:00:00");
                        //endtime1 = Convert.ToDateTime(TIME1.Year.ToString() + "-" + TIME1.ToDate().Month.ToString() + "-23" + " 20:00:00");
                    //计算实际产量（上个月25号到本月25号）
                      double M_SPARE32 = GET_T_BURN_COLE_PLC_1MIN(iDataBase, starttime, endtime1);
                        //本月25号到本月结束的每天计划产量
                        if (M_Day1_1 > 25)
                        {
                            DAT_25_END =DAT - M_Day1_1 + 25;//（当前执行月份的上个月的天数-当天日期+25天）到25号以后下个执行月份已经有了所以这么写
                        }
                        else if (M_Day1_1 < 25 || M_Day1_1 >= 1)
                        {
                            DAT_25_END = M_DAY2 - M_DAY1 - M_Day1_1;
                        }
                        ///判断计划月产量大于实际分钟产量的和
                        if (PROD_ITEM_VALUE > M_SPARE32)
                        {
                            M_PLAN_OUTPUT_DAY  = Math.Round((PROD_ITEM_VALUE - M_SPARE32) / DAT_25_END);
                        }
                        else
                        {
                            M_PLAN_OUTPUT_DAY = 0;
                        }
                    }
                    MD_MD_PLAN_OUTPUT_IN_.TIMESTAMP = currentTime;
                    MD_MD_PLAN_OUTPUT_IN_.PLAN_OUTPUT_DAY = M_PLAN_OUTPUT_DAY;
                    //保存
                    Set_Md_plan_output_in_add(MD_MD_PLAN_OUTPUT_IN_);

               }
            }
        }
        public double GET_T_BURN_COLE_PLC_1MIN(IDataBase iDataBase, DateTime startTime, DateTime endTime)
        {
            string str_sql = string.Format("select sum(SPARE32) from T_BURN_COLE_PLC_1MIN where SPARE32>0 and timestamp > to_date('" + startTime + "','yyyy-mm-dd hh24:mi:ss')and timestamp <= to_date('" + endTime + "','yyyy-mm-dd hh24:mi:ss')");
            return iDataBase.GetDouble(str_sql);
        }

        public void set_save(List<TPSIPDP_IOOP> qati)
        {
            TPSIPDP_IOOP_PLAN MD_TPSIPDP_IOOP_PLAN_ = null;
            MD_TPSIPDP_IOOP_PLAN_ = new TPSIPDP_IOOP_PLAN();
            for (int i = 0; i < qati.Count; i++)
            {
                //string PROD_MON_ = qati[i].PROD_MON + "000000";
                MD_TPSIPDP_IOOP_PLAN_.REC_CREATOR = qati[i].REC_CREATOR;
                MD_TPSIPDP_IOOP_PLAN_.REC_CREATE_TIME = DateTime.ParseExact(qati[i].REC_CREATE_TIME, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                MD_TPSIPDP_IOOP_PLAN_.REC_REVISOR = qati[i].REC_REVISOR;
               // MD_TPSIPDP_IOOP_PLAN_.REC_REVISE_TIME = DateTime.ParseExact(qati[i].REC_REVISE_TIME, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                MD_TPSIPDP_IOOP_PLAN_.PROD_MON = DateTime.ParseExact(qati[i].PROD_MON, "yyyyMM", System.Globalization.CultureInfo.CurrentCulture);
                MD_TPSIPDP_IOOP_PLAN_.WORK_DATE = qati[i].WORK_DATE;
                MD_TPSIPDP_IOOP_PLAN_.PROC_UNIT = qati[i].PROC_UNIT;
                MD_TPSIPDP_IOOP_PLAN_.UNIT_NO = qati[i].UNIT_NO;
                MD_TPSIPDP_IOOP_PLAN_.PROD_ITEM_CODE = qati[i].PROD_ITEM_CODE;
                MD_TPSIPDP_IOOP_PLAN_.PROD_ITEM_VALUE = qati[i].PROD_ITEM_VALUE;
                MD_TPSIPDP_IOOP_PLAN_.FLAG1 = qati[i].FLAG1;
                MD_TPSIPDP_IOOP_PLAN_.FLAG2 = qati[i].FLAG2;
                MD_TPSIPDP_IOOP_PLAN_.FLAG3 = qati[i].FLAG3;
                MD_TPSIPDP_IOOP_PLAN_.FLAG4 = qati[i].FLAG4;
                MD_TPSIPDP_IOOP_PLAN_.FLAG5 = qati[i].FLAG5;
                MD_TPSIPDP_IOOP_PLAN_.FLAG6 = qati[i].FLAG6;
                MD_TPSIPDP_IOOP_PLAN_.COMM_FLAG = qati[i].COMM_FLAG;
                MD_TPSIPDP_IOOP_PLAN_.COMM_SND_FLAG = qati[i].COMM_SND_FLAG;
                MD_TPSIPDP_IOOP_PLAN_.COMM_SND_TIME = qati[i].COMM_SND_TIME;
                MD_TPSIPDP_IOOP_PLAN_.COMM_SND_REMARK = qati[i].COMM_SND_REMARK;
                MD_TPSIPDP_IOOP_PLAN_.COMM_RECV_FLAG = qati[i].COMM_RECV_FLAG;
                MD_TPSIPDP_IOOP_PLAN_.COMM_RECV_TIME = qati[i].COMM_RECV_TIME;
                MD_TPSIPDP_IOOP_PLAN_.COMM_RECV_REMARK = qati[i].COMM_RECV_REMARK;

                Set_Admin_add(MD_TPSIPDP_IOOP_PLAN_);
            }
            Get_update();
        }
        public bool Set_Admin_add(TPSIPDP_IOOP_PLAN model)
        {
            bool listmodel = true;

            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                listmodel = (bool)iDataBase.Insert(model);
                return listmodel;
            }
        }

        public bool Set_Md_plan_output_in_add(MD_PLAN_OUTPUT_IN model)
        {
            bool listmodel = true;

            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                listmodel = (bool)iDataBase.Insert(model);
                return listmodel;
            }
        }
        public bool Get_update()
        {
            bool listmodel = true;
            string sql = "";

            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                try
                {
                    iDataBase.BeginTran();
                    sql = "update TPSIPDP_IOOP SET flag1='1' where  PROC_UNIT = 'S' and UNIT_NO = 6";
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

        /// <summary>
        /// 该月分的天数
        /// </summary>
        /// <returns></returns>
        public int MonthsByDay(int year, int mon)
        {
            DateTime DateTime2 = DateTime.Parse(string.Format("{0}/{1}/1", year, mon));
            DateTime DateTime1 = DateTime2.AddMonths(1);
            int dateDiff = 0;
            TimeSpan ts = DateTime1.Subtract(DateTime2).Duration();

            dateDiff = ts.Days;
            return dateDiff;
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }
    }
}
