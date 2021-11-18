using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Job
{
    public class TestJob : Jobs.Base.BaseJob
    {
        public override void InitTask()
        {
        }

        public override void RunTask(DateTime currentTime)
        {
            Outputlog("执行前当前时间" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "执行时间" + currentTime.ToString("yyyy-MM-dd HH:mm:ss"));
            System.Threading.Thread.Sleep(6000);
            Outputlog("执行后当前时间" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "执行时间" + currentTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
