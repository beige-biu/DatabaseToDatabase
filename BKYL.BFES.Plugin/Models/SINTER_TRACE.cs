using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models
{
    public class SINTER_TRACE
    {
        
        /// <summary>
        /// Desc:取样时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:物料名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MATERIAL_NAME {get;set;}

        /// <summary>
        /// Desc:到炉顶所需时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double TIMES_TOTOP {get;set;}

        /// <summary>
        /// Desc:预计入炉时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIME_TOBF {get;set;}

        /// <summary>
        /// Desc:全铁 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_TFE_CONT {get;set;}

        /// <summary>
        /// Desc:氧化亚铁 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_FEO_CONT {get;set;}

        /// <summary>
        /// Desc:二氧化硅 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_SIO2_CONT {get;set;}

        /// <summary>
        /// Desc:氧化钙 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_CAO_CONT {get;set;}

        /// <summary>
        /// Desc:氧化镁 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_MGO_CONT {get;set;}

        /// <summary>
        /// Desc:三氧化二铝 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_AL2O3_CONT {get;set;}

        /// <summary>
        /// Desc:转鼓指数 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_TI6Z3 {get;set;}

        /// <summary>
        /// Desc:RDI+3.15 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_RDI_3Z15 { get;set;}

        /// <summary>
        /// Desc:二元碱度cao/siO2 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double MR_R2 {get;set;}
        /// <summary>
        /// Desc:试样号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string M_SAMPLE_NO { get; set; }

    }
}
