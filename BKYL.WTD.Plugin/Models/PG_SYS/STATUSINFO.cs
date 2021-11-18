using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.WTD.Plugin.Models
{
    public class VM_STATUSINFO
    {
        /// <summary>
        /// 高炉编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 高炉描述
        /// </summary>
        public string BFESNO { get; set; }
        /// <summary>
        /// 时间间隔（分钟）
        /// </summary>
        public double STATUS { get; set; }
        /// <summary>
        /// 最新采集时间
        /// </summary>
        public DateTime TIMESTAMP { get; set; }
        /// <summary>
        /// 间隔标准
        /// </summary>
        public int RANGETIME { get; set; }
        public bool not_connect_flag;
        /// <summary>
        /// 状态标签
        /// </summary>
        public bool NOT_CONNECT_FLAG { get; set; }
        //{
        //    get { return not_connect_flag; }
        //    set { value = not_connect_flag; } }

    }
}
public class WTD_BF_STATUSLOG
{
    /// <summary>
    /// 记录时间
    /// </summary>
    public DateTime TIMESTAMP { get; set; }

    /// <summary>
    /// 高炉编号
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// 高炉描述
    /// </summary>
    public string BFESNO { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string CONTENT { get; set; }
}