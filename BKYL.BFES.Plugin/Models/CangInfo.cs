using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin
{

    public class CangInfo
    {
        /// <summary>
        /// 仓编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 料种
        /// </summary>
        public int CODE { get; set; }
        /// <summary>
        /// 料种名称
        /// </summary>
        public string CODEDESC { get; set; }
        /// <summary>
        /// 密度
        /// </summary>
        public double Density { get; set; }
        /// <summary>
        /// 仓名称
        /// </summary>
        public string CangName { get; set; }
        /// <summary>
        /// 台底长
        /// </summary>
        public double DownL { get; set; }
        /// <summary>
        /// 台底宽
        /// </summary>
        public double DownW { get; set; }
        /// <summary>
        /// 台底高
        /// </summary>
        public double DownH { get; set; }
        /// <summary>
        /// 上方长
        /// </summary>
        public double UpL { get; set; }
        /// <summary>
        /// 上方宽
        /// </summary>
        public double UpW { get; set; }
        /// <summary>
        /// 上方高
        /// </summary>
        public double UpH { get; set; }
        /// <summary>
        /// 差异
        /// </summary>
        public double ChaYi { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// 仓位
        /// </summary>
        public double CangWei { get; set; }
    }
}

