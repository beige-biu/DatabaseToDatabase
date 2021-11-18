
using BFES.DataAccess;
using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BKYL.Jobs.Base.Plugin
{
    class Job_CalFormula : BaseJob
    {
        public override void InitTask()
        {
            try
            {
                RefreshConfig();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void RunTask(DateTime currentTime)
        {
            Outputlog("Job_CalFormula：开始计算");
           
            Thread.Sleep(1000);
           
           
            Outputlog("Job_CalFormula：结束计算");
        }

        public override void RunTaskException(DateTime currentTime, Exception exception)
        {
            //输入错误到界面
            GlobalObject.RichTextErrorLog.AppendTextByAsync("时间:" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + " 任务进程：" + tableConfig.FolderName + "->" + tableConfig.FileName + "\r\n " + exception.Message, System.Drawing.Color.Red);
            //输出错误到日志
            Writelog(exception.Message + exception.StackTrace);
        }
        public void RefreshConfig()
        {
            using (IDataBase iDataBase = DalFactory.GreateIDataBase(S_DBSource))
            {
                try
                {
                    Outputlog("Job_CalFormula：开始更新参数...");

                    


                   
                    Outputlog("Job_CalFormula：更新参数完成！");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}
