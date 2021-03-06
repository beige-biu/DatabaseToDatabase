using BKYL.Jobs.Base.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace BKYL.TransferDB
{
    static class Program
    {
        private static System.Threading.Mutex mutex;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //处理非UI线程异常   
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string processFlag = string.Format("BKYL.TransferDB-{0}", ConfigurationManager.AppSettings["ProcessName"]);
            mutex = new System.Threading.Mutex(true, processFlag);
            if (mutex.WaitOne(0, false))
            {
                //处理未捕获的异常   
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常   
                Application.ThreadException += Application_ThreadException;

                Application.Run(new TransferDBMainForm());
            }
            else
            {
                MessageBox.Show(string.Format("应用程序已经在运行中！唯一标识码:{0} ", ConfigurationManager.AppSettings["ProcessName"]), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var str = "";
            var strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now + "\r\n";
            var error = e.Exception;
            if (error != null)
            {
                str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                    error.GetType().Name, error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("应用程序线程错误:{0}", e);
            }
            Log.WriteLine(str, @"未处理异常\线程中未处理的异常");
            if (((e.Exception).GetType()).BaseType.Name.ToUpper() == "DBEXCEPTION")
            {

                MessageBox.Show("<b><color=red>客户端与服务器通信时出现问题，网络中断！！！</color></b>\n具体原因：<color=blue>" + e.Exception.Message + "</color>", "数据库通信错误");
            }
            else
            {
                MessageBox.Show(str,"系统错误");
            }
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var str = "";
            var error = e.ExceptionObject as Exception;
            var strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now + "\r\n";
            if (error != null)
            {
                str = string.Format(strDateInfo + "Application UnhandledException:{0};\n\r堆栈信息:{1}", error.Message,
                    error.StackTrace);
            }
            else
            {
                str = string.Format("Application UnhandledError:{0}", e);
            }

            Log.WriteLine(str, @"未处理异常\主程序中未处理异常");
            MessageBox.Show("系统错误", str);
        }
    }
}
