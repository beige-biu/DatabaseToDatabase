using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
   
    public class MC_MICAL_RESULT
    {
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:数据编号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int DATANUM { get; set; }

        /// <summary>
        /// Desc:配料室记录时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? MICAL_MATCH_TIME { get; set; }

        /// <summary>
        /// Desc:混匀矿配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_BLO_RATIO_DRY { get; set; }

        /// <summary>
        /// Desc:直供料1配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_DRF_RATIO_1_DRY { get; set; }

        /// <summary>
        /// Desc:直供料2配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_DRF_RATIO_2_DRY { get; set; }

        /// <summary>
        /// Desc:直供料3配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_DRF_RATIO_3_DRY { get; set; }

        /// <summary>
        /// Desc:直供料4配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_DRF_RATIO_4_DRY { get; set; }

        /// <summary>
        /// Desc:烧返矿配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_RE_RATIO_DRY { get; set; }

        /// <summary>
        /// Desc:石灰石配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_LIMS_RATIO_DRY { get; set; }

        /// <summary>
        /// Desc:自产生石灰配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_SLIMS_RATIO_DRY { get; set; }

        /// <summary>
        /// Desc:除尘灰配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_DUST_RATIO_DRY { get; set; }

        /// <summary>
        /// Desc:生石灰配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_QLIME_RATIO_DRY { get; set; }

        /// <summary>
        /// Desc:焦屑配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_COKE_RATIO_DRY { get; set; }

        /// <summary>
        /// Desc:混匀矿配比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_BLO_RATIO_WET { get; set; }

        /// <summary>
        /// Desc:直供料1配比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_DRF_RATIO_1_WET { get; set; }

        /// <summary>
        /// Desc:直供料2配比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_DRF_RATIO_2_WET { get; set; }

        /// <summary>
        /// Desc:直供料3配比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_DRF_RATIO_3_WET { get; set; }

        /// <summary>
        /// Desc:直供料4配比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_DRF_RATIO_4_WET { get; set; }

        /// <summary>
        /// Desc:烧返矿配比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_RE_RATIO_WET { get; set; }

        /// <summary>
        /// Desc:石灰石配比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_LIMS_RATIO_WET { get; set; }

        /// <summary>
        /// Desc:自产生石灰配比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_SLIMS_RATIO_WET { get; set; }

        /// <summary>
        /// Desc:除尘灰配比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_DUST_RATIO_WET { get; set; }

        /// <summary>
        /// Desc:生 石 灰 配 比（ 湿 ） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_QLIME_RATIO_WET { get; set; }

        /// <summary>
        /// Desc:焦屑配比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_COKE_RATIO_WET { get; set; }

        /// <summary>
        /// Desc:焦炭粒度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_COKE_GRI { get; set; }

        /// <summary>
        /// Desc:磁铁矿配比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MAGT_BILL { get; set; }

        /// <summary>
        /// Desc:配料混合料FeO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_FEO { get; set; }

        /// <summary>
        /// Desc:配料混合料SiO2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_SIO2 { get; set; }

        /// <summary>
        /// Desc:配料混合料MgO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_MGO { get; set; }

        /// <summary>
        /// Desc:配料混合料Al2O3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_AL2O3 { get; set; }

        /// <summary>
        /// Desc:配料混合料碱度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_R { get; set; }

        /// <summary>
        /// Desc:配料混合料含碳 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_C { get; set; }

        /// <summary>
        /// Desc:总料量SP 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_TOTAL_SP { get; set; }

        /// <summary>
        /// Desc:总料量PV 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MATCH_TOTAL_PV { get; set; }

        /// <summary>
        /// Desc:一混记录时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? MICAL_BLEND_1M_TIME { get; set; }

        /// <summary>
        /// Desc:1H-1皮带秤值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BLEND_BELT_W_1H1 { get; set; }

        /// <summary>
        /// Desc:一混转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BLEND_1M_RATE { get; set; }

        /// <summary>
        /// Desc:一混目标水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BLEND_1M_TARGET_WATER { get; set; }

        /// <summary>
        /// Desc:一混设定加水量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BLEND_1M_FT_SP { get; set; }

        /// <summary>
        /// Desc:一混实际加水量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BLEND_1M_FT_PV { get; set; }

        /// <summary>
        /// Desc:一混后检测水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BLEND_1M_NEX_WAT_AVG { get; set; }

        /// <summary>
        /// Desc:一混到配料倒推时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_1M_MAT_TIME { get; set; }

        /// <summary>
        /// Desc:二混记录时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? MICAL_BLEND_2M_TIME { get; set; }

        /// <summary>
        /// Desc:2H-1皮带秤值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BLEND_BELT_W_2H1 { get; set; }

        /// <summary>
        /// Desc:二混转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BLEND_2M_RATE { get; set; }

        /// <summary>
        /// Desc:二混目标水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BLEND_2M_TARGET_WATER { get; set; }

        /// <summary>
        /// Desc:二混设定加水量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BLEND_2M_FLOW_SP { get; set; }

        /// <summary>
        /// Desc:二混实际加水量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BLEND_2M_FLOW_PV { get; set; }

        /// <summary>
        /// Desc:二混后检测水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BLEND_2M_NEX_WAT_AVG { get; set; }

        /// <summary>
        /// Desc:二混到一混倒推时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_2M_1M_TIME { get; set; }

        /// <summary>
        /// Desc:布料记录时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? MICAL_CLOTH_DST_TIME { get; set; }

        /// <summary>
        /// Desc:圆辊转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_CLOTH_STICK_PV_RATE { get; set; }

        /// <summary>
        /// Desc:烧结料厚 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_CLOTH_THICK_AVG { get; set; }

        /// <summary>
        /// Desc:混合料槽料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_CLOTH_BL_TAR_LEVEL { get; set; }

        /// <summary>
        /// Desc:Z2-1皮带秤值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_CLOTH_2M_W_Z2_1 { get; set; }

        /// <summary>
        /// Desc:布料到二混倒推时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_CLO_2M_TIME { get; set; }

        /// <summary>
        /// Desc:点火记录时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? MICAL_BU_C_IGNI_TIME { get; set; }

        /// <summary>
        /// Desc:点火温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BU_C_IG_AIM_TE { get; set; }

        /// <summary>
        /// Desc:煤气流量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BU_C_IG_GAS_PV { get; set; }

        /// <summary>
        /// Desc:空气流量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BU_C_IG_AIR_PV { get; set; }

        /// <summary>
        /// Desc:空燃比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BU_C_AIM_AFR { get; set; }

        /// <summary>
        /// Desc:烧结记录时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? MICAL_BU_C_SIN_TIME { get; set; }

        /// <summary>
        /// Desc:烧结机设定机速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BU_C_SIN_MS_SP { get; set; }

        /// <summary>
        /// Desc:烧结机实际机速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BU_C_SIN_MS_PV { get; set; }

        /// <summary>
        /// Desc:主抽大烟道温度 （两个平均值） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MA_SB_FLUE_TE { get; set; }

        /// <summary>
        /// Desc:主抽大烟道压力（两个平均值） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MA_SB_FLUE_PT { get; set; }

        /// <summary>
        /// Desc:主抽大烟道流量（两个之和） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_MA_SB_FLUE_FT { get; set; }

        /// <summary>
        /// Desc:透气性指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BREATH { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BU_C_LOCAT_BRP { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BU_C_BRP_TE { get; set; }

        /// <summary>
        /// Desc:BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BU_C_LOCAT_BTP { get; set; }

        /// <summary>
        /// Desc:BTP温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BU_C_BTP_TE { get; set; }

        /// <summary>
        /// Desc:烧结18到1号风箱倒推时间时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_SIN_1_18_TIME { get; set; }

        /// <summary>
        /// Desc:烧结15到1号风箱倒推时间时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_SIN_1_15_TIME { get; set; }

        /// <summary>
        /// Desc:烧结12到1号风箱倒推时间时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_SIN_1_12_TIME { get; set; }

        /// <summary>
        /// Desc:烧结9到1号风箱倒推时间时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_SIN_1_9_TIME { get; set; }

        /// <summary>
        /// Desc:烧结6到1号风箱倒推时间时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_SIN_1_6_TIME { get; set; }

        /// <summary>
        /// Desc:烧结3到1号风箱倒推时间时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_SIN_1_3_TIME { get; set; }

        /// <summary>
        /// Desc:烧结工序所需时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_SIN_U_TIME { get; set; }

        /// <summary>
        /// Desc:环冷记录时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? MICAL_BU_C_RC_TIME { get; set; }

        /// <summary>
        /// Desc:入口温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BU_C_RC_B_IN_TE { get; set; }

        /// <summary>
        /// Desc:出口温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BU_C_RC_B_OUT_TE { get; set; }

        /// <summary>
        /// Desc:环冷机设定机速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BU_C_RC_SPEED_SP { get; set; }

        /// <summary>
        /// Desc:环冷机实际机速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BU_C_RC_SPEED_PV { get; set; }

        /// <summary>
        /// Desc:环冷工序所需时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_RC_U_TIME { get; set; }

        /// <summary>
        /// Desc:筛分记录时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? MICAL_BU_C_SCREEN_TIME { get; set; }

        /// <summary>
        /// Desc:铺底料皮带秤值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BU_C_BED_MATE_AO_FT { get; set; }

        /// <summary>
        /// Desc:返矿皮带秤值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BU_C_COLD_AO_FT { get; set; }

        /// <summary>
        /// Desc:成品皮带秤 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BU_C_FP_BELT_FT_AVG { get; set; }

        /// <summary>
        /// Desc:成品率计算 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BU_C_FP_RATE { get; set; }

        /// <summary>
        /// Desc:筛分到环冷倒推时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_SCR_RC_TIME { get; set; }

        /// <summary>
        /// Desc:取样记录时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? MICAL_BU_C_SAMP_TIME { get; set; }

        /// <summary>
        /// Desc:取样到筛分倒推时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_SAM_SCR_TIME { get; set; }

        /// <summary>
        /// Desc:流程总时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? MICAL_SAM_MAT_TIME { get; set; }

        /// <summary>
        /// Desc:目标料批在混料槽下料耗时 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MICAL_BL_BUNK_TIME { get; set; }

        /// <summary>
        /// Desc:模型计算状态（1：正常计算；2：异常停机计算；3：开机初始计算；） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? MICAL_M_STATE_FLAG { get; set; }

        /// <summary>
        /// Desc:环冷机状态（1：环冷机运转正常；0：环冷机运转异常） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? MICAL_RC_STATE_FLAG { get; set; }

        /// <summary>
        /// Desc:烧结机状态（1：烧结机运转正常；0：烧结机运转异常） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? MICAL_SIN_STATE_FLAG { get; set; }

    }
}
