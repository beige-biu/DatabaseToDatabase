using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MC_TIME_YIELD
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:炉顶时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? TOPBIN_TIME {get;set;}

        /// <summary>
        /// Desc:软熔带时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? COHESIVE_TIME {get;set;}

        /// <summary>
        /// Desc:配料室记录时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? MICAL_MATCH_TIME {get;set;}

        /// <summary>
        /// Desc:烧结记录时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? MICAL_BU_C_SIN_TIME {get;set;}

        /// <summary>
        /// Desc:取样记录时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? MICAL_BU_C_SAMP_TIME {get;set;}
        /// <summary>
        /// Desc:取样记录时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? TAPPING_TIME { get; set; }
        

    }
}
