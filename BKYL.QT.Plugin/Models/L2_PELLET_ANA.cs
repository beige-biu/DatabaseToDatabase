using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Models
{
    public class L2_PELLET_ANA
    {

        /// <summary>
        /// Desc:日期 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:原料配方 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_MAT_NAME { get; set; }

        /// <summary>
        /// Desc:批次 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_BATCH { get; set; }

        /// <summary>
        /// Desc:Tfe 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_TFE { get; set; }

        /// <summary>
        /// Desc:FeO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_FEO { get; set; }

        /// <summary>
        /// Desc:SiO2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SIO2 { get; set; }

        /// <summary>
        /// Desc:Al2O3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_AL2O3 { get; set; }

        /// <summary>
        /// Desc:CaO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_CAO { get; set; }

        /// <summary>
        /// Desc:MgO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_MGO { get; set; }

        /// <summary>
        /// Desc:S 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_S { get; set; }

        /// <summary>
        /// Desc:P 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_P { get; set; }

        /// <summary>
        /// Desc:TiO2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_TIO2 { get; set; }

        /// <summary>
        /// Desc:H2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_H2O { get; set; }

        /// <summary>
        /// Desc:抗压强度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_CS { get; set; }

        /// <summary>
        /// Desc:碱度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_R { get; set; }

        /// <summary>
        /// Desc:转鼓指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DRIX { get; set; }

        /// <summary>
        /// Desc:班次 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_SHIFT { get; set; }

        /// <summary>
        /// Desc:取样时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? M_SAMPLE_TIME { get; set; }

        /// <summary>
        /// Desc:粒度                              
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_GRIT { get; set; }

        /// <summary>
        /// Desc:抗磨指数                          
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_WEAR_RESISTANCE_INDEX { get; set; }

        /// <summary>
        /// Desc:膨胀倍数                          
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_EXPANSION_RATIO { get; set; }

        /// <summary>
        /// Desc:球团班次1234 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_QTCLASS_NAME_ID { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_ID_SHIFT { get; set; }

        /// <summary>
        /// Desc:Pb  
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_PB { get; set; }

        /// <summary>
        /// Desc:Sn  
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SN { get; set; }

        /// <summary>
        /// Desc:Bi 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BI { get; set; }

        /// <summary>
        /// Desc:Sb 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SB { get; set; }

        /// <summary>
        /// Desc:As   
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_AS { get; set; }

        /// <summary>
        /// Desc:CaF2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_CAF2 { get; set; }

        /// <summary>
        /// Desc:MnO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_MNO { get; set; }

        /// <summary>
        /// Desc:F 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_F { get; set; }

        /// <summary>
        /// Desc:K2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_K2O { get; set; }

        /// <summary>
        /// Desc:Na2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_NA2O { get; set; }

        /// <summary>
        /// Desc:Ig 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_IG { get; set; }

        /// <summary>
        /// Desc:TFE_FEO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_TFE_FEO { get; set; }

        /// <summary>
        /// Desc:筛分指数   
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SCREENING_INDEX { get; set; }

        /// <summary>
        /// Desc:RDI_6_3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_RDI_6_3 { get; set; }

        /// <summary>
        /// Desc:RI （还原度） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_RI { get; set; }

        /// <summary>
        /// Desc:熔融滴落试验  
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_MELT_DROP_TEST { get; set; }

        /// <summary>
        /// Desc:胶质价 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_COLLOID_PRICE { get; set; }

        /// <summary>
        /// Desc:ZnO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ZNO { get; set; }

        /// <summary>
        /// Desc:焙烧评价关联标志0是未关联，1是关联完成
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_CAL_FLAG { get; set; }

        /// <summary>
        /// 质量诊断数据关联标志0是未计算，1是计算完成
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_FLAG { get; set; }

    }
}
