using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.WTD.Plugin.Models
{
    public  class PARAMETERS
    {

        /// <summary>
        /// 现场数据库1
        /// </summary>
        public string DB_Connect1 { get; set; }
        ///// <summary>
        ///// 现场数据库类型1 
        ///// </summary>
        //public int DB_Type1 { get; set; }
        /// <summary>
        /// 现场数据库2
        /// </summary>
        public string DB_Connect2 { get; set; }
        ///// <summary>
        ///// 现场数据库类型2 
        ///// </summary>
        //public int DB_Type2 { get; set; }
        /// <summary>
        /// 现场数据库3
        /// </summary>
        public string DB_Connect3 { get; set; }
        ///// <summary>
        ///// 现场数据库类型3 
        ///// </summary>
        //public int DB_Type3 { get; set; }

        /// <summary>
        /// 高炉编号
        /// </summary>
        public string BF_Id { get; set; }

        /// <summary>
        /// 热流向前推时间(单位:小时)
        /// </summary>
        public int AVG_INTERVAL_HOUR { get; set; }

        /// <summary>
        /// 平均值差 单位：百分比%
        /// </summary>
        public double AVG_DIFF { get; set; }
    }


}


public class LIST_CONFIG_UPDATE_DATETIME
{
    /// <summary>
    /// 表名
    /// </summary>
    public string NAME { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime LAST_USER_UPDATE { get; set; }
}