using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MC_POPCAL_RESULT_CONSUME
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:原料编号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? CODE {get;set;}

        /// <summary>
        /// Desc:原料消耗 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? VALUE {get;set;}
        public int CLASS_TYPE { get; set; }

    }
}
