using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models
{
    public class HEARTHBALANCE_IRONDIFF
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:班次 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BC {get;set;}

        /// <summary>
        /// Desc:班别 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BB {get;set;}

        /// <summary>
        /// Desc:铁次号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string TAPNUMB {get;set;}

        /// <summary>
        /// Desc:铁口 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string HOLENUMB {get;set;}

        /// <summary>
        /// Desc:出铁开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime TAP_START {get;set;}

        /// <summary>
        /// Desc:出铁结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime TAP_END {get;set;}

        /// <summary>
        /// Desc:理论出铁量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double IRON_THEORY {get;set;}

        /// <summary>
        /// Desc:时间出铁量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double IRON_REAL {get;set;}

        /// <summary>
        /// Desc:盈亏铁量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double IRONDIFF {get;set;}

        public Double SI { get; set; }

        public double? HMTP { get; set; }
        /// <summary>
        /// 出铁速度
        /// </summary>
        public double? TAP_SPEED { get; set; }
        /// <summary>
        /// 见渣时间
        /// </summary>
        public DateTime  SLAG_START { get; set; }
        /// <summary>
        /// 出铁率
        /// </summary>
        public double? IRONPRODRATIO { get; set; }
        /// <summary>
        /// 出渣率
        /// </summary>
        public double? SLAGPRODRATIO { get; set; }

    }
}
