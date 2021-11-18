using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpConfig;
using BKYL.WTD.Plugin.Models;
using System.IO;

namespace BKYL.WTD.Plugin.Utils
{
    public static class Util
    {
        public static string path = AppDomain.CurrentDomain.BaseDirectory + @"Config\WTD\PARAMETERS.ini";
        /// <summary>
        /// 读取ini文件到实体对象
        /// </summary>
        /// <returns></returns>
        public static PARAMETERS GetParameters()
        {
            if (!File.Exists(path)) return null;
            Configuration configuration = Configuration.LoadFromFile(path);;
            return configuration[typeof(PARAMETERS).Name].CreateObject<PARAMETERS>();
        }


    }
}
