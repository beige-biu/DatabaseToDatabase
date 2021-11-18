using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MD_PLAN_OUTPUT_IN
    {
        
        /// <summary>
        /// Desc:计划产量下发时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:每天计划产量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PLAN_OUTPUT_DAY {get;set;}

    }
}
