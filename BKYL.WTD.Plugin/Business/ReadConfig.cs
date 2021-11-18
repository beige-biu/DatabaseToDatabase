using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BKYL.WTD.Plugin.Models;
using BKYL.WTD.Plugin.Utils;

namespace BKYL.WTD.Plugin
{
   public static class ReadConfig
    {
        /// <summary>
        /// 获取多个配置表或指定名称的配置表
        /// </summary>
        /// <param name="tablename"></param>
        public static void Get_All_Config(string tablename="ALL")
        {
            if (tablename == "ALL" || tablename.ToUpper() == "WTD_GROUP_CONFIG")
            {
                //获取水温差组配置
                Globals.Wtd_Group_Config = DataCollection.Get_Data_List<WTD_GROUP_CONFIG>(1, "WTD_GROUP_CONFIG", null, " ORDER BY GROUPNO ASC ");
            }
            if (tablename == "ALL" || tablename.ToUpper() == "WTD_MONITOR_CONFIG")
            {
                //获取水温差监测点配置
                Globals.Wtd_Monitor_Monfig = DataCollection.Get_Data_List<WTD_MONITOR_CONFIG>(1, "WTD_MONITOR_CONFIG", null, " ORDER BY GROUPNO ASC ");
            }

            if (tablename == "ALL" || tablename.ToUpper() == "WTD_TAP_STATISTICS_CONFIG")
            {
                string sql = @"SELECT M.*,T.REGIONMARK, T.REGIONDESC
                                FROM WTD_MONITOR_CONFIG AS M INNER JOIN
                                WTD_TAP_STATISTICS_CONFIG AS T ON M.GROUPNO = T.GROUPNO
                                AND M.PIPENO = T.PIPENO ORDER BY T.GROUPNO, T.PIPENO ASC";
                //获取水温差监测点配置
                Globals.Wtd_Tap_Statistics_Config = DataCollection.Get_Data_List<WTD_TAP_STATISTICS_CONFIG>(1, "WTD_MONITOR_CONFIG", sql, null);
            }
        }
    }
}
