using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class M_PICAL_BREATH_RESULT
    {
        
        /// <summary>
        /// Desc:插入时间
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:垂直烧结速度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_VER_SIN_SP {get;set;}

        /// <summary>
        /// Desc:透气性计算对应一混粒度＜1mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_1M_GRIT_1 {get;set;}

        /// <summary>
        /// Desc:透气性计算对应一混粒度1mm-3mm
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_1M_GRIT_1_3 {get;set;}

        /// <summary>
        /// Desc:透气性计算对应一混粒度>3mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_1M_GRIT_3 {get;set;}

        /// <summary>
        /// Desc:透气性计算对应二混粒度＜1mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_2M_GRIT_1 {get;set;}

        /// <summary>
        /// Desc:透气性计算对应二混粒度1mm-3mm
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_2M_GRIT_1_3 {get;set;}

        /// <summary>
        /// Desc:透气性计算对应二混粒度>3mm
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_2M_GRIT_3 {get;set;}

        /// <summary>
        /// Desc:透气性计算对应填充率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_FILL_RATE {get;set;}

        /// <summary>
        /// Desc:透气性计算对应一混加水量
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_1M_FT_PV {get;set;}

        /// <summary>
        /// Desc:透气性计算对应二混加水量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_2M_FT_PV {get;set;}

        /// <summary>
        /// Desc:透气性计算对应一混转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_1M_MIX_RATE {get;set;}

        /// <summary>
        /// Desc:透气性计算对应二混转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_2M_MIX_RATE {get;set;}

        /// <summary>
        /// Desc:透气性计算对应一混后水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_1M_NEX_WAT {get;set;}

        /// <summary>
        /// Desc:透气性计算对应二混后水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_2M_NEX_WAT {get;set;}

        /// <summary>
        /// Desc:透气性计算对应混合料温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_BLE_TE {get;set;}

        /// <summary>
        /// Desc:透气性计算对应溶剂配比1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SOL_BILL_1 {get;set;}

        /// <summary>
        /// Desc:透气性计算对应溶剂配比2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SOL_BILL_2 {get;set;}

        /// <summary>
        /// Desc:透气性计算对应溶剂配比3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SOL_BILL_3 {get;set;}

        /// <summary>
        /// Desc:透气性计算对应混匀矿粒度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_IRON_GRIT {get;set;}

        /// <summary>
        /// Desc:透气性计算对应精矿配比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_CON_BILL {get;set;}

        /// <summary>
        /// Desc:透气性计算对应烧返配比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SRM_BILL {get;set;}

        /// <summary>
        /// Desc:透气性计算对应燃料配比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_COKE_BILL {get;set;}

        /// <summary>
        /// Desc:透气性计算对应目标点火温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_AIM_TE {get;set;}

        /// <summary>
        /// Desc:透气性计算对应点火段温度1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_IG_01_TE {get;set;}

        /// <summary>
        /// Desc:透气性计算对应点火段温度2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_IG_02_TE {get;set;}

        /// <summary>
        /// Desc:透气性计算对应点火段温度3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_IG_03_TE {get;set;}

        /// <summary>
        /// Desc:透气性计算对应台车速度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SIN_MS_PV {get;set;}

        /// <summary>
        /// Desc:透气性计算对应烧返粒度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE1 {get;set;}

        /// <summary>
        /// Desc:透气性计算对应烧返粒度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE2 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE3 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE4 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE5 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE6 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE7 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE8 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE9 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE10 {get;set;}

        /// <summary>
        /// Desc:透气性和溶剂配比相关性 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_RELAT_BR_BL_FLUX {get;set;}

        /// <summary>
        /// Desc:透气性和精矿配比相关性 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_RELAT_BR_BL_CON {get;set;}

        /// <summary>
        /// Desc:透气性和烧返配比相关性 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_RELAT_BR_BL_SINBACK {get;set;}

        /// <summary>
        /// Desc:透气性和燃料配比相关性 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_RELAT_BR_BL_FUEL {get;set;}

        /// <summary>
        /// Desc:透气性和混合料粒度相关性
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_RELAT_BR_MIX_GR {get;set;}

        /// <summary>
        /// Desc:透气性和混合料温度相关性 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_RELAT_BR_BL_BLEND {get;set;}

        /// <summary>
        /// Desc:透气性和水分相关性 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_RELAT_BR_WAT {get;set;}

        /// <summary>
        /// Desc:透气性和点火温度相关性
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_RELAT_BR_BL_IG_TE {get;set;}

        /// <summary>
        /// Desc:透气性和料层厚度相关性
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_RELAT_BR_THICK {get;set;}

        /// <summary>
        /// Desc:透气性和台车速度相关性 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_RELAT_BR_SIN_SPEED {get;set;}

        /// <summary>
        /// Desc:透气性和风量相关性
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_RELAT_BR_FLUE_FT {get;set;}

        /// <summary>
        /// Desc:透气性和风压相关性
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_RELAT_BR_FLUE_PT {get;set;}

        /// <summary>
        /// Desc:透气性和烧返粒度相关性
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE11 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE12 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE13 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE14 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE15 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE16 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE17 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE18 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE19 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_SPARE20 {get;set;}

        /// <summary>
        /// Desc:主抽1、2总烟气流量（风量） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_Q {get;set;}

        /// <summary>
        /// Desc:主抽1烧结大烟道烟气流量
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_Q1 {get;set;}

        /// <summary>
        /// Desc:主抽2烧结大烟道烟气流量
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_Q2 {get;set;}

        /// <summary>
        /// Desc:主抽1、2烧结大烟道压力平均值（负压）
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_P {get;set;}

        /// <summary>
        /// Desc:主抽1烧结大烟道压力(大烟道压力左)取平均 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_P1 {get;set;}

        /// <summary>
        /// Desc:主抽2烧结大烟道压力(大烟道压力又)取平均
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_P2 {get;set;}

        /// <summary>
        /// Desc:平均布料厚度
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_H {get;set;}

        /// <summary>
        /// Desc:抽风面积
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_A {get;set;}

        /// <summary>
        /// Desc:透气性指数
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_JPU {get;set;}

        /// <summary>
        /// Desc:焦炭粒度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_COKE_GRI {get;set;}

        /// <summary>
        /// Desc:透气性对应主抽温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_MA_TE {get;set;}

        /// <summary>
        /// Desc:透气性计算对应铺底料厚度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_BED_TH {get;set;}

        /// <summary>
        /// Desc:透气性计算对应碱度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_PV_R {get;set;}

        /// <summary>
        /// Desc:透气性计算对应Mgo含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_PV_MGO {get;set;}

        /// <summary>
        /// Desc:透气性计算对应二混后平均粒度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_BREATH_2M_GRIT_AVG {get;set;}

        /// <summary>
        /// Desc:透气性计算对应二混后平均粒度
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_RELAT_BR_COKE_GRI {get;set;}

        /// <summary>
        /// Desc:透气性和主抽温度相关性
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_RELAT_BR_MA_TE {get;set;}

        /// <summary>
        /// Desc:透气性和铺底料厚度相关性
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_RELAT_BR_BED_TH {get;set;}

        /// <summary>
        /// Desc:透气性和碱度相关性
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_RELAT_BR_R {get;set;}

        /// <summary>
        /// Desc:透气性和MgO含量相关性
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_RELAT_BR_BL_MGO {get;set;}

    }
}
