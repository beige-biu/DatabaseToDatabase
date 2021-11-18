using BKYL.WTD.Plugin.Models;
using BKYL.WTD.Plugin.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace BKYL.WTD.Plugin
{

    public class Globals
    {
        private static PARAMETERS parameters = null;

        /// <summary>
        /// 获取ini配置数据
        /// </summary>
        public static PARAMETERS Parameters
        {
            get
            {
                if (parameters == null)
                {
                    parameters = Util.GetParameters();
                }
                return parameters;
            }
        }
        /// <summary>
        ///  存储所有热流强度plc表
        /// </summary>
        public static DataSet ds_HeatFlow_PLC { get; set; }

        /// <summary>
        ///  存储所有热流强度plc表
        /// </summary>
        public static DataSet ds_HeatFlow_01H { get; set; }


        /// <summary>
        ///  指定时间（48）小时均值
        /// </summary>
        public static DataSet ds_HeatFlow_48H_Avg { get; set; }


        /// <summary>
        /// 水温差组配置
        /// </summary>
        public static List<WTD_GROUP_CONFIG> Wtd_Group_Config { get; set; }
        /// <summary>
        /// 监测点配置表
        /// </summary>
        public static List<WTD_MONITOR_CONFIG> Wtd_Monitor_Monfig { get; set; }

        /// <summary>
        /// 铁口配置表
        /// </summary>
        public static List<WTD_TAP_STATISTICS_CONFIG> Wtd_Tap_Statistics_Config { get; set; }
        
        /// <summary>
        /// 查询所有PLC表 CONVERT(VARCHAR(19),REFDATE,120) REFDATE 
        /// </summary>
        public static string sql_plc =@" SELECT NAME  FROM SYSOBJECTS 
                                    WHERE XTYPE = 'U' 
                                    AND NAME LIKE ('%{0}%') 
                                    AND NAME NOT LIKE('%AUTO%') 
                                    AND NAME NOT LIKE('%DEVICE%')
                                    AND NAME NOT LIKE('%VIRTUAL%')
                                    AND NAME NOT IN('WTD_DETAIL_PLC_V')  
                                    ORDER BY NAME ";
        /// <summary>
        /// 查询所有配置表
        /// </summary>
        public static string sql_all_config = @"SELECT OBJECT_NAME(OBJECT_ID) AS NAME                                              
                                        FROM   SYS.DM_DB_INDEX_USAGE_STATS 
                                        WHERE  OBJECT_ID IN (SELECT ID 
                                                             FROM   SYSOBJECTS 
                                                             WHERE  XTYPE = 'U' 
                                                                    AND NAME LIKE ( '%CONFIG%' ) AND ( NAME LIKE ( '%WTD%' ) or NAME LIKE ( '%FS_%' )) AND NAME NOT IN('WTD_VIRTUAL_CONFIG')) 
                                                                    {0}
                                                                    GROUP BY  OBJECT_NAME(OBJECT_ID)
                                                                    ORDER BY NAME ";

        public static string sql_refeash_config = @"SELECT OBJECT_NAME(OBJECT_ID) AS NAME,max(LAST_USER_UPDATE ) LAST_USER_UPDATE                                                   
                                        FROM   SYS.DM_DB_INDEX_USAGE_STATS 
                                        WHERE  OBJECT_ID IN (SELECT ID 
                                                             FROM   SYSOBJECTS 
                                                             WHERE  XTYPE = 'U' 
                                                                    AND NAME LIKE ( '%CONFIG%' ) AND  ( NAME LIKE ( '%WTD%' ) or NAME LIKE ( '%FS_%' ))  AND NAME NOT IN('WTD_VIRTUAL_CONFIG')) 
                                                                    AND  LAST_USER_UPDATE IS NOT NULL
                                                                    group by  OBJECT_NAME(OBJECT_ID)
                                                                    ORDER BY NAME ";
        public static string sql_alarm = @"SELECT * FROM WTD_ALARM WHERE TIMESTAMP>'{0}' ";
        /// <summary>
        /// 数据库最大时间
        /// </summary>
        public static DateTime DB_Max_DateTime_PLC { get; set; }

        /// <summary>
        /// 数据库最大时间
        /// </summary>
        public static DateTime DB_Max_DateTime_PLC2 { get; set; }


        /// <summary>
        /// 配置表最新更新时间
        /// </summary>
        public static List<LIST_CONFIG_UPDATE_DATETIME> List_Config_Update_Datetime { get; set; }

        /// <summary>
        /// 高炉连接状态集合
        /// </summary>
        public static List<VM_STATUSINFO> List_StatusInfo { get; set; }
    }
}
