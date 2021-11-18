
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
    public class Console_Interface_min : BaseJob
    {
        ///烧结配料界面计算每分钟转历史lihaisen
        public override void InitTask()
        {
            //加载配置
        }

        public override void RunTask(DateTime currentTime)
        {
            string log = "";
            DateTime datetme;
            try
            {
                datetme = Convert.ToDateTime(currentTime.ToShortDateString() + " " + currentTime.ToShortTimeString() + ":00");

                List<MC_SINCAL_INTERFACE_TRIG> Mc_sincal_ = Get_MC_SINCAL_INTERFACE_TRIG();

                if (Mc_sincal_.Count > 0)
                {
                    for (int i = 0; i < Mc_sincal_.Count; i++)
                    {
                        Mc_sincal_[i].TIMESTAMP = datetme;
                    }
                    if (Set_save(Mc_sincal_))
                    {
                        log = "烧结配料界面计算每分钟转历史完成";
                    }
                    else
                    {
                        log = "烧结配料界面计算每分钟转历史失败";
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
        public List<MC_SINCAL_INTERFACE_TRIG> Get_MC_SINCAL_INTERFACE_TRIG()
        {
            string Temp = "";
            //源数据库 表
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                string strSQL = "select max(timestamp)  from MC_SINCAL_INTERFACE_TRIG";
                Temp = iDataBase.GetString(strSQL);
                DateTime strtimestamp = Convert.ToDateTime(Temp);
                return iDataBase.Queryable<MC_SINCAL_INTERFACE_TRIG>().Where(m => m.TIMESTAMP == strtimestamp).OrderBy(M => M.BLANKING_ID).ToList();
            }
        }
        public bool Set_save(List<MC_SINCAL_INTERFACE_TRIG> indictionList)
        {
            //目标数据库 表
            ///    indictionList.ConvertAll<List<MC_SINCAL_INTERFACE_TRIG_MIN>>(input => input as List<MC_SINCAL_INTERFACE_TRIG_MIN>);
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
            {
                try
                {
                    string sql = "";
                    iDataBase.BeginTran();
                    sql = "insert into MC_SINCAL_INTERFACE_TRIG_MIN (TIMESTAMP,ID,SINCAL_ID,SINCAL_COOD,BLANKING_ID, SINCAL_ML ,SINCAL_ORE_RATIO,SINCAL_ORE_SP,SINCAL_ORE_PV,"
                             + "SINCAL_ORE_DRY_BILL,SINCAL_ORE_TC,SINCAL_ORE_H2O_SP,SINCAL_ORE_H2O_PV,SINCAL_ORE_BK_SP,SINCAL_ORE_BK_PV,SINCAL_ORE_BK_DV,"
                             + "SINCAL_ORE_WET_BILL,SINCAL_ORE_FRE,SINCAL_ORE_ATIVE,SINCAL_ORE_WET) values (:TIMESTAMP,:ID,:SINCAL_ID,:SINCAL_COOD,:BLANKING_ID, :SINCAL_ML,:SINCAL_ORE_RATIO,:SINCAL_ORE_SP,:SINCAL_ORE_PV,"
                             + ":SINCAL_ORE_DRY_BILL,:SINCAL_ORE_TC,:SINCAL_ORE_H2O_SP,:SINCAL_ORE_H2O_PV,:SINCAL_ORE_BK_SP,:SINCAL_ORE_BK_PV,:SINCAL_ORE_BK_DV,"
                             + ":SINCAL_ORE_WET_BILL,:SINCAL_ORE_FRE,:SINCAL_ORE_ATIVE,:SINCAL_ORE_WET )";
                    BFES.DataAccess.SqlMapper.Execute(iDataBase.GetConnection(), sql, indictionList);
                    iDataBase.CommitTran();
                    return true;
                }
                catch (Exception ex)
                {
                    iDataBase.RollbackTran();
                    return false;
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
