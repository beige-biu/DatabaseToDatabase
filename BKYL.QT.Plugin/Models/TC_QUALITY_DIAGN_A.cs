using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Models
{
    public class TC_QUALITY_DIAGN_A
    {

        /// <summary>
        /// Desc:插入时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        ///// <summary>
        ///// Desc:成品矿平均粒度(原数据) 
        ///// Default:- 
        ///// Nullable:True 
        ///// </summary>
        //public double TC_GRIT { get; set; }

        /// <summary>
        /// Desc:入机小时生球量（焙烧机给料量）(原数据) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double TC_FQ_W{ get; set; }

        /// <summary>
        /// Desc:15#风箱温度(原数据) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double TC_TE_15 { get; set; }

        /// <summary>
        /// Desc:带式机机速(原数据) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double TC_PH_BL_EP { get; set; }

        /// <summary>
        /// Desc:台车料厚(原数据) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double TC_T_H_BCD { get; set; }

        /// <summary>
        /// Desc:台车料厚偏差(原数据) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double TC_T_H_BCD_PD { get; set; }

        /// <summary>
        /// Desc:鼓干鼓风机压力(原数据) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double TC_FT_DD_TP { get; set; }

        /// <summary>
        /// Desc:主引风机温度(原数据) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double TC_FT_ML_TE { get; set; }

        /// <summary>
        /// Desc:回热风机温度(原数据) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double TC_FT_RH_TE { get; set; }

        /// <summary>
        /// Desc:烧嘴4区温度(原数据) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double TC_BURNERQ4_TE { get; set; }

        /// <summary>
        /// Desc:烧嘴5区温度(原数据) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double TC_BURNERQ5_TE { get; set; }

        /// <summary>
        /// Desc:烧嘴6区温度(原数据) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double TC_BURNERQ6_TE { get; set; }

        /// <summary>
        /// Desc:主引风机转速(原数据) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double TC_FT_ML_ST { get; set; }

        /// <summary>
        /// Desc:回热风机转速(原数据) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double TC_FT_RH_ST { get; set; }

        /// <summary>
        /// Desc:冷却风机转速(原数据) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double TC_FT_CL_ST { get; set; }

        /// <summary>
        /// Desc:Tfe 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double TC_TFE { get; set; }

        /// <summary>
        /// Desc:FeO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double TC_FEO { get; set; }

        /// <summary>
        /// Desc:抗压强度(原数据) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double TC_CS { get; set; }

        /// <summary>
        /// Desc:转鼓指数(原数据) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double TC_DRIX { get; set; }

    }
}
