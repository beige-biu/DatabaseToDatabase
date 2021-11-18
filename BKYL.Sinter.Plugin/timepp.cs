
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
    public class timepp : BaseJob
    {
        public override void InitTask()
        {
         
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
                    // 数据库查询
                
                   List<OREBLENDING_TRACE> OREBLENDING_TRACE_ = GET_OREBLENDING_TRACE(iDataBase, currentTime);
                    if (OREBLENDING_TRACE_.Count>0)
                    {
                        for (int a = 0; a < OREBLENDING_TRACE_.Count; a++)
                        {
                            if (OREBLENDING_TRACE_[a].CS_TIME != null)
                            {
                               List<MC_TIME_YIELD> MC_TIME_YIELD_ = GET_MC_TIME_YIELD(OREBLENDING_TRACE_[a].CS_TIME, iDataBase);
                                if (MC_TIME_YIELD_.Count==0)
                                {
                                    MC_MICAL_RESULT MC_MICAL_RESULT_ = GET_MC_MICAL_RESULT(OREBLENDING_TRACE_[a].CS_TIME, iDataBase);
                                    if (MC_MICAL_RESULT_.MICAL_BU_C_SAMP_TIME != null)
                                    {
                                        SET_MC_TIME_YIELD(MC_MICAL_RESULT_, OREBLENDING_TRACE_[a].TOPBIN_TIME, OREBLENDING_TRACE_[a].COHESIVE_TIME, OREBLENDING_TRACE_[a].CS_TIME, OREBLENDING_TRACE_[a].TAPPING_TIME);
                                    }
                                }
                            }
                        }
                    }
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

        public void SET_MC_TIME_YIELD(MC_MICAL_RESULT MC_MICAL_RESULT_, DateTime? TOPBIN_TIME, DateTime? COHESIVE_TIME, DateTime CS_TIME, DateTime TAPPING_TIME)
        {
            string log;
            MC_TIME_YIELD MC_TIME_YIELD_ = null;
            MC_TIME_YIELD_ = new MC_TIME_YIELD();
            if (MC_MICAL_RESULT_ != null )
            {
                //检测到数据  开始计算公式SINCAL_MIX_PV_LOT
                try
                {
                    MC_TIME_YIELD_.TIMESTAMP = DateTime.Now;
                    MC_TIME_YIELD_.TOPBIN_TIME = TOPBIN_TIME;
                    MC_TIME_YIELD_.COHESIVE_TIME = COHESIVE_TIME;
                    MC_TIME_YIELD_.MICAL_MATCH_TIME = MC_MICAL_RESULT_.MICAL_MATCH_TIME;
                    MC_TIME_YIELD_.MICAL_BU_C_SIN_TIME = MC_MICAL_RESULT_.MICAL_BU_C_SIN_TIME;
                    MC_TIME_YIELD_.MICAL_BU_C_SAMP_TIME = CS_TIME;
                    MC_TIME_YIELD_.TAPPING_TIME = TAPPING_TIME;
                    if (Set_Admin_add(MC_TIME_YIELD_))
                    {
                        log = "保存完成";
                    }
                    else
                    {
                        log = "保存失败";
                       
                    }
                }
                catch (Exception ex)
                {
                    log = string.Format("执行失败。原因：{0}", ex.Message);
                }
            }
        }

        public  List<MC_TIME_YIELD> GET_MC_TIME_YIELD (DateTime CS_TIME, IDataBase iDataBase)
        {
            string str_sql = string.Format("select * from MC_TIME_YIELD where  MICAL_BU_C_SAMP_TIME =to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')", CS_TIME);
            return iDataBase.GetList<MC_TIME_YIELD>(str_sql);
        }
        public MC_MICAL_RESULT GET_MC_MICAL_RESULT(DateTime CS_TIME, IDataBase iDataBase)
        {
          
            string str_sql = string.Format("select * from (select * from MC_MICAL_RESULT WHERE DATANUM=14 and TIMESTAMP <to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')order by TIMESTAMP desc) where rownum<2", CS_TIME);
            return iDataBase.GetSingle<MC_MICAL_RESULT>(str_sql);
        }

        public List<OREBLENDING_TRACE> GET_OREBLENDING_TRACE(IDataBase iDataBase, DateTime currentTime)
        {
            
            DateTime strat;
            strat = currentTime.AddDays(-3);
            string str_sql = string.Format("select * from OREBLENDING_TRACE WHERE  CS_TIME >to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')order by CS_TIME desc ", strat);
            return iDataBase.GetList<OREBLENDING_TRACE>(str_sql);
        }
        public bool Set_Admin_add(MC_TIME_YIELD model)
        {
            bool listmodel = false;
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                listmodel = (bool)iDataBase.Insert(model);
                return listmodel;
            }
        }
        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            Writelog(exception.Message + exception.StackTrace);
        }
    }
}
