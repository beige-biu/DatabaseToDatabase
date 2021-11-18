using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Models
{
     public class PCA_RESULT
    {
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_SPE { get; set; }

        /// <summary>
        /// Desc:SPE控制值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_SPE_CT_VALUE { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_T2 { get; set; }

        /// <summary>
        /// Desc:T2控制值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_T2_CT_VALUE { get; set; }

        /// <summary>
        /// Desc:成品矿平均粒度贡献率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_GRIT_CR { get; set; }

        /// <summary>
        /// Desc:入机小时生球量贡献率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_FQ_W_CR { get; set; }

        /// <summary>
        /// Desc:15#风箱温度贡献率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_TE_15_CR { get; set; }

        /// <summary>
        /// Desc:带式机机速贡献率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_PH_BL_EP_CR { get; set; }

        /// <summary>
        /// Desc:台车料厚贡献率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_T_H_BCD_CR { get; set; }

        /// <summary>
        /// Desc:台车料厚偏差贡献率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_T_H_BCD_PD_CR { get; set; }

        /// <summary>
        /// Desc:鼓干鼓风机压力贡献率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_FT_DD_TP_CR { get; set; }

        /// <summary>
        /// Desc:主引风机温度贡献率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_FT_ML_TE_CR { get; set; }

        /// <summary>
        /// Desc:回热风机温度贡献率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_FT_RH_TE_CR { get; set; }

        /// <summary>
        /// Desc:烧嘴4区温度贡献率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_BURNERQ4_TE_CR { get; set; }

        /// <summary>
        /// Desc:烧嘴5区温度贡献率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_BURNERQ5_TE_CR { get; set; }

        /// <summary>
        /// Desc:烧嘴6区温度贡献率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_BURNERQ6_TE_CR { get; set; }

        /// <summary>
        /// Desc:主引风机转速贡献率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_FT_ML_ST_CR { get; set; }

        /// <summary>
        /// Desc:回热风机转速贡献率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_FT_RH_ST_CR { get; set; }

        /// <summary>
        /// Desc:冷却风机转速贡献率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_FT_CL_ST_CR { get; set; }


    }
}
