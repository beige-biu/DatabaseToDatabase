using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MC_SINCAL_INTERFACE_VA
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:总料量SP 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double T_TOTAL_SP_W_5S {get;set;}

        /// <summary>
        /// Desc:总料量PV 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double T_TOTAL_PV_W_5S {get;set;}

        /// <summary>
        /// Desc:干料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double SINCAL_FLTMIXSUMDW {get;set;}

        /// <summary>
        /// Desc:总料量建议值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double SUG_MAT_ITY {get;set;}

        /// <summary>
        /// Desc:目标含碳 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double SINCAL_C_A {get;set;}

        /// <summary>
        /// Desc:目标碱度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double SINCAL_R_A {get;set;}

        /// <summary>
        /// Desc:碱度调整值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double SINCAL_R_C {get;set;}

        /// <summary>
        /// Desc:含碳调整值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double SINCAL_C_DC {get;set;}

    }
}
