using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Models
{
    public class TC_ROASTER_WAVE_10M
    {
        /// <summary>
        /// Desc:插入时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:成品矿平均粒度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_GRIT { get; set; }

        /// <summary>
        /// Desc:入机小时生球量（焙烧机给料量） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_FQ_W { get; set; }

        /// <summary>
        /// Desc:15#风箱温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_TE_15 { get; set; }

        /// <summary>
        /// Desc:焙烧段烟罩压力3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_PH_TP { get; set; }
        /// <summary>
        /// Desc:鼓干鼓风机压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_FT_DD_TP { get; set; }

        /// <summary>
        /// Desc:鼓干段烟罩温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_FT_BT_TP { get; set; }

        /// <summary>
        /// Desc:铺底料料位(铺底料重) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_UC_BL_W { get; set; }

        /// <summary>
        /// Desc:卸料仓料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_PH_BL_W { get; set; }

        /// <summary>
        /// Desc:带式机机速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_PH_BL_EP { get; set; }

        /// <summary>
        /// Desc:台车料厚 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_T_H_BCD { get; set; }

        /// <summary>
        /// Desc:台车料厚偏差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_T_H_BCD_PD { get; set; }

        /// <summary>
        /// Desc:二冷段烟罩温度2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_PH_R_TE { get; set; }

        /// <summary>
        /// Desc:鼓干鼓风机温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_FT_DD_TE { get; set; }

        /// <summary>
        /// Desc:主引风机温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_FT_ML_TE { get; set; }

        /// <summary>
        /// Desc:回热风机温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_FT_RH_TE { get; set; }

        /// <summary>
        /// Desc:抽干段压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_PH_PI_TP { get; set; }

        /// <summary>
        /// Desc:烧嘴3区温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_BURNERQ3_TE { get; set; }

        /// <summary>
        /// Desc:烧嘴4区温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_BURNERQ4_TE { get; set; }

        /// <summary>
        /// Desc:烧嘴5区温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_BURNERQ5_TE { get; set; }

        /// <summary>
        /// Desc:烧嘴6区温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_BURNERQ6_TE { get; set; }

        /// <summary>
        /// Desc:预热段烟罩1区温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_ROASTERQ1_TE { get; set; }

        /// <summary>
        /// Desc:预热段烟罩4区温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_ROASTERQ4_TE { get; set; }

        /// <summary>
        /// Desc:预热段烟罩5区温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_ROASTERQ5_TE { get; set; }

        /// <summary>
        /// Desc:主引风机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_FT_ML_ST { get; set; }

        /// <summary>
        /// Desc:回热风机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_FT_RH_ST { get; set; }

        /// <summary>
        /// Desc:冷却风机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_FT_CL_ST { get; set; }

        /// <summary>
        /// Desc:皂土量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_BTE { get; set; }

        /// <summary>
        /// Desc:瞬时湿返率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_WR { get; set; }

        /// <summary>
        /// Desc:干燥后水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_DRY_A_WATER { get; set; }

        /// <summary>
        /// Desc:混合后水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_A_MIX { get; set; }

        /// <summary>
        /// Desc:8#风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_BELLOWS08_TP { get; set; }

        /// <summary>
        /// Desc:16#风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_BELLOWS16_TP { get; set; }

        /// <summary>
        /// Desc:21#风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_BELLOWS21_TP { get; set; }

    }
}
