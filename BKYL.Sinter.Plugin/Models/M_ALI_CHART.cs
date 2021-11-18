using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class M_ALI_CHART
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:FeO目标 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_FE_CENTER {get;set;}

        /// <summary>
        /// Desc:FeO检验 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_FEO {get;set;}

        /// <summary>
        /// Desc:C目标 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_C_A {get;set;}

        /// <summary>
        /// Desc:C实际 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_C_DC {get;set;}

    }
}
