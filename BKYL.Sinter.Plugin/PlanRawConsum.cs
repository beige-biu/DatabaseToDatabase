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
    public class PlanRawConsum : BaseJob
    {//原料消耗结果（MC_POPCAL_CONSUME_1MIN求和）
        public override void InitTask()
        {
            ///取 MC_POPCAL_RESULT_CONSUME 最大时间  付给MAx
            //加载配置yanhao
        }
        public override void RunTask(DateTime currentTime)
        {
            DateTime time = DateTime.Now.Date.ToString().ToDate();
            int isLoadTime = 0;
            if (currentTime.Hour >= 00 && currentTime.Hour < 08)
            {
                isLoadTime = 8;//白班
            }
            else if (currentTime.Hour >= 08 && currentTime.Hour < 20)
            {
                isLoadTime = 20;//夜班
            }
            else
            {
                isLoadTime = 0;
            }
            if (isLoadTime == 8 || isLoadTime == 20)
            {
                string log = "";
                List<MC_POPCAL_CONSUME_1MIN> list = null;
                List<MC_POPCAL_CONSUME_1MIN> list1 = null;

                string str_sql = null;
                using (IDataBase iDataBase = DalFactory.GreateIDataBase(T_DBSource))
                {
                    //01min 表  max  >MAX

                    str_sql = string.Format("select " + DataBaseFactory.ConvertTimeString(currentTime, T_DBSource.DBType) + " as TIMESTAMP,CODE,sum(value)as VALUE,2 as CLASS_TYPE   from  MC_POPCAL_CONSUME_1MIN where TIMESTAMP between  to_date('" + time.AddHours(-4) + "','yyyy-mm-dd hh24:mi:ss') and to_date('" + time.AddHours(20) + "','yyyy-mm-dd hh24:mi:ss')  group by  code");
                    list = iDataBase.GetList<MC_POPCAL_CONSUME_1MIN>(str_sql);//全天

                    if (isLoadTime == 20)
                    {
                        str_sql = string.Format("select  " + DataBaseFactory.ConvertTimeString(currentTime, T_DBSource.DBType) + " as TIMESTAMP,1 as CLASS_TYPE,code,sum(value)as VALUE  from  MC_POPCAL_CONSUME_1MIN where TIMESTAMP between  to_date('" + time.AddHours(8) + "','yyyy-mm-dd hh24:mi:ss') and to_date('" + time.AddHours(20) + "','yyyy-mm-dd hh24:mi:ss')  group by  code");
                        list1 = iDataBase.GetList<MC_POPCAL_CONSUME_1MIN>(str_sql);//夜班
                    }
                    else         
                    {
                        str_sql = string.Format("select  " + DataBaseFactory.ConvertTimeString(currentTime, T_DBSource.DBType) + " as TIMESTAMP,0 as CLASS_TYPE,code,sum(value) as VALUE from  MC_POPCAL_CONSUME_1MIN where TIMESTAMP between  to_date('" + time.AddHours(-4) + "','yyyy-mm-dd hh24:mi:ss') and to_date('" + time.AddHours(8) + "','yyyy-mm-dd hh24:mi:ss')  group by  code");
                        list1 = iDataBase.GetList<MC_POPCAL_CONSUME_1MIN>(str_sql);//白班
                    }
                    System.Data.Common.DbConnection con= iDataBase.GetConnection();
                    System.Data.Common.DbTransaction tran = iDataBase.GetConnection().BeginTransaction();
                    try
                    {
                        str_sql = "insert into MC_POPCAL_RESULT_CONSUME (TIMESTAMP,CODE,VALUE,CLASS_TYPE) values (:TIMESTAMP,:CODE,:VALUE,:CLASS_TYPE)";
                        BFES.DataAccess.SqlMapper.Execute(con, str_sql, list, tran);
                        BFES.DataAccess.SqlMapper.Execute(con, str_sql, list1, tran);

                        tran.Commit();
                        log = string.Format("执行成功");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        iDataBase.RollbackTran();
                        log = string.Format("执行失败。原因：{0}", ex.Message);
                    }
                    finally
                    {
                        Outputlog(log);
                        if (list != null)
                        {
                            list.Clear();
                            list = null;
                        }
                        if (list1 != null)
                        {
                            list1.Clear();
                            list1 = null;
                        }
                        log = null;
                    }
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
