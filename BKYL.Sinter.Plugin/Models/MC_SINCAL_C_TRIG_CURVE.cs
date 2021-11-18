using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MC_SINCAL_C_TRIG_CURVE
    {

        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:燃料配比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_RATIO { get; set; }

        /// <summary>
        /// Desc:烧返配比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_RATIO { get; set; }

        /// <summary>
        /// Desc:综合烧损 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_MIX_SP_LOT { get; set; }

        /// <summary>
        /// Desc:非燃料含碳 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_NON_FUEL_SP_C { get; set; }

        /// <summary>
        /// Desc:碱度目标 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_R_AIM { get; set; }

        /// <summary>
        /// Desc:实际配料碱度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_R_PV { get; set; }

        /// <summary>
        /// Desc:化验碱度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_R_TEST { get; set; }

        /// <summary>
        /// Desc:FEO目标 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FEO_AIM { get; set; }

        /// <summary>
        /// Desc:化验FEO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FEO_TEST { get; set; }

        /// <summary>
        /// Desc:C目标 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_C_AIM { get; set; }

        /// <summary>
        /// Desc:实际配料含碳 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_C_PV { get; set; }

    }
}
