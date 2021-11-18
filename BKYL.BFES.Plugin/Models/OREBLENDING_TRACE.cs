using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class OREBLENDING_TRACE
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:烧结到高炉槽上时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime CS_TIME {get;set;}

        /// <summary>
        /// Desc:出铁时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TAPPING_TIME {get;set;}
        /// <summary>
        /// Desc:炉顶时间
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime? TOPBIN_TIME { get; set; }

        /// <summary>
        /// Desc:软熔带时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime? COHESIVE_TIME { get; set; }

    }
}
