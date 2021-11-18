using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MC_POPCAL_PAR
    {
        
        /// <summary>
        /// Desc:固体燃耗计算周期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SO_T {get;set;}

        /// <summary>
        /// Desc:为理论产量与实际产量的修正系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_K {get;set;}
        public Double? PAR_K1 { get; set; }

        public Double? PAR_K2 { get; set; }

    }
}
