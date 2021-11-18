using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class ORE_TRACK_BIGDATA
    {
        
        /// <summary>
        /// Desc:配矿方案编号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ORDER_NO {get;set;}

        /// <summary>
        /// Desc:开堆时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? BEGIN_STORE_DATE {get;set;}

        /// <summary>
        /// Desc:开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? START_TIME {get;set;}

        /// <summary>
        /// Desc:结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? END_TIME {get;set;}

        /// <summary>
        /// Desc:TFE 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TFE {get;set;}

        /// <summary>
        /// Desc:FEO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FEO { get;set;}

        /// <summary>
        /// Desc:R 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R {get;set;}

        /// <summary>
        /// Desc:SIO2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SIO2 {get;set;}

        /// <summary>
        /// Desc:AL2O3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? AL2O3 {get;set;}

        /// <summary>
        /// Desc:CAO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? CAO {get;set;}

        /// <summary>
        /// Desc:MGO
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MGO {get;set;}

        /// <summary>
        /// Desc:K2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? K2O {get;set;}

        /// <summary>
        /// Desc:NA2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? NA2O {get;set;}

        /// <summary>
        /// Desc:ZN 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ZN {get;set;}

        /// <summary>
        /// Desc:P 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? P {get;set;}

        /// <summary>
        /// Desc:S 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? S {get;set;}

        /// <summary>
        /// Desc:TI 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TI {get;set;}

        /// <summary>
        /// Desc:MNO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MNO {get;set;}

        /// <summary>
        /// Desc:AS 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_AS {get;set;}

        /// <summary>
        /// Desc:CR 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? CR {get;set;}

        /// <summary>
        /// Desc:转鼓指数(T) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? DRUM_INDEX {get;set;}

        /// <summary>
        /// Desc:还原度(RI) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? RI {get;set;}

        /// <summary>
        /// Desc:>40mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_40MM {get;set;}

        /// <summary>
        /// Desc:40－25mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_4025MM {get;set;}

        /// <summary>
        /// Desc:25-16mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? S25_16 {get;set;}

        /// <summary>
        /// Desc:16－10mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_1610MM {get;set;}

        /// <summary>
        /// Desc:10－5mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_105MM {get;set;}

        /// <summary>
        /// Desc:<5mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_5MM {get;set;}

        /// <summary>
        /// Desc:低温还原强度(RDI||6.3) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? LOW_TEMP_STRENGTH {get;set;}

        /// <summary>
        /// Desc:低温还原粉化(RDI+3.15) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? LOW_TEMPERATURE_REDUCTION {get;set;}

        /// <summary>
        /// Desc:低温还原抗磨(RDI－0.5) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? LOW_TEMPE_ANTIWEAR {get;set;}

        /// <summary>
        /// Desc:软化开始 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SOFTENING_START {get;set;}

        /// <summary>
        /// Desc:软化终了 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SOFTENING_END {get;set;}

        /// <summary>
        /// Desc:软化区间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SOFTENING_INTERVAL {get;set;}

        /// <summary>
        /// Desc:熔融开始 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MELTING_START {get;set;}

        /// <summary>
        /// Desc:熔融终了 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MELTING_END {get;set;}

        /// <summary>
        /// Desc:熔融区间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MELTING_INTERVAL {get;set;}

        /// <summary>
        /// Desc:压差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PRESSURE_DIFF {get;set;}

        /// <summary>
        /// Desc:软容层厚度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SOFT_LAYER {get;set;}

        /// <summary>
        /// Desc:抗压强度(N) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? COMPRESSIVE {get;set;}

        /// <summary>
        /// Desc:熔滴指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? DROPLET {get;set;}

        /// <summary>
        /// Desc:烧结矿总产量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINTER_YIELD {get;set;}

        /// <summary>
        /// Desc:日均产量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? DAY_YIELD_AVG {get;set;}

        /// <summary>
        /// Desc:机速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BC_SIN_MS_PV {get;set;}

        /// <summary>
        /// Desc:利用系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? UTILIZATION_FACTOR {get;set;}

        /// <summary>
        /// Desc:作业率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? OPERATING_RATE {get;set;}

        /// <summary>
        /// Desc:蒸汽产量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? STEAM_YIELD {get;set;}

        /// <summary>
        /// Desc:二混后粒度（大于3mm） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SECONDARY_MIXED_SIZE {get;set;}

        /// <summary>
        /// Desc:二次混合料水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SECONDARY_MIX_H2O {get;set;}

        /// <summary>
        /// Desc:焦屑粒度（大于3mm） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? COKE_SIZE {get;set;}

        /// <summary>
        /// Desc:焦屑水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? WATER {get;set;}

        /// <summary>
        /// Desc:料层厚度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? LAYER_THICKNESS {get;set;}

        /// <summary>
        /// Desc:抽风负压 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? NEGATIVE {get;set;}

        /// <summary>
        /// Desc:主抽频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PS_MA_FAN_PV_FRE {get;set;}

        /// <summary>
        /// Desc:内返矿比例 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? RETURN_RATIO {get;set;}

        /// <summary>
        /// Desc:点火用煤气单耗 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MQ_DH {get;set;}

        /// <summary>
        /// Desc:电耗 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? DN_DH {get;set;}

        /// <summary>
        /// Desc:固体燃耗 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GT_RH {get;set;}

        /// <summary>
        /// Desc:固体燃耗(干) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GT_RH_G {get;set;}

        /// <summary>
        /// Desc:生石灰单耗 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SSH_DH {get;set;}

        /// <summary>
        /// Desc:点火温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? DH_WD {get;set;}

        /// <summary>
        /// Desc:大烟道温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? DYD_WD {get;set;}

        /// <summary>
        /// Desc:终点温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ZD_WD {get;set;}

        /// <summary>
        /// Desc:氮氧化物（入口） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_R_NO {get;set;}

        /// <summary>
        /// Desc:二氧化硫（入口） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_R_SO2 {get;set;}

        /// <summary>
        /// Desc:粉尘（入口） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_R_FC {get;set;}

        /// <summary>
        /// Desc:氧气含量（入口） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_R_O2 {get;set;}

        /// <summary>
        /// Desc:湿度（入口) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_R_SD {get;set;}

        /// <summary>
        /// Desc:"流量（入口)" 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_R_LL {get;set;}

        /// <summary>
        /// Desc:烟气温度压力（入口) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_R_TEMP {get;set;}

        /// <summary>
        /// Desc:氮氧化物（出口） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_C_NO {get;set;}

        /// <summary>
        /// Desc:二氧化硫（出口） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_C_SO2 {get;set;}

        /// <summary>
        /// Desc:粉尘（出口） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_C_FC {get;set;}

        /// <summary>
        /// Desc:氧气含量（出口） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_C_O2 {get;set;}

        /// <summary>
        /// Desc:湿度（出口) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_C_SD {get;set;}

        /// <summary>
        /// Desc:"流量（出口)" 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_C_LL {get;set;}

        /// <summary>
        /// Desc:烟气温度压力（出口) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_C_TEMP {get;set;}

        /// <summary>
        /// Desc:氮氧化物（出口）折算值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_C_NO_Z {get;set;}

        /// <summary>
        /// Desc:二氧化硫（出口）折算值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_C_SO2_Z {get;set;}

        /// <summary>
        /// Desc:粉尘（出口）折算值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_C_FC_Z {get;set;}

        /// <summary>
        /// Desc:氮氧化物（脱除效率） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_T_NO {get;set;}

        /// <summary>
        /// Desc:二氧化硫（脱除效率） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_T_SO2 {get;set;}

        /// <summary>
        /// Desc:粉尘（脱除效率） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_T_FC {get;set;}

        /// <summary>
        /// Desc:压力（入口）
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_R_YL { get; set; }
        /// <summary>
        /// Desc:压力（出口）
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TL_C_YL { get; set; }

    }
}
