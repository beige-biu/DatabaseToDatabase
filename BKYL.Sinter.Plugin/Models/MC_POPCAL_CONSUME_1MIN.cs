using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MC_POPCAL_CONSUME_1MIN
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:原料编码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int CODE {get;set;}

        /// <summary>
        /// Desc:原料消耗 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double VALUE {get;set;}
        /// <summary>
        /// 班次
        /// </summary>
        public int  CLASS_TYPE { get; set; }

    }
}
