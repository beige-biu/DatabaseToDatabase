using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MC_MICAL_PAR
    {
        
        /// <summary>
        /// Desc:插入时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:配料所需时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_MAT_U_T {get;set;}

        /// <summary>
        /// Desc:进一混皮带长度(1H-1) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_1H_1 {get;set;}

        /// <summary>
        /// Desc:一混所用时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_1M_U_TIME {get;set;}

        /// <summary>
        /// Desc:进二混皮带长度(2H-1) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_2H_1 {get;set;}

        /// <summary>
        /// Desc:二混所用时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_2M_U_TIME {get;set;}

        /// <summary>
        /// Desc:出二混皮带长度(Z2-1) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_Z2_1 {get;set;}

        /// <summary>
        /// Desc:进梭试布料器皮带长度(S-1) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_S1 {get;set;}

        /// <summary>
        /// Desc:布料所需的时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_CLOTH_U_TIME {get;set;}

        /// <summary>
        /// Desc:1#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B01 {get;set;}

        /// <summary>
        /// Desc:2#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B02 {get;set;}

        /// <summary>
        /// Desc:3#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B03 {get;set;}

        /// <summary>
        /// Desc:4#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B04 {get;set;}

        /// <summary>
        /// Desc:5#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B05 {get;set;}

        /// <summary>
        /// Desc:6#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B06 {get;set;}

        /// <summary>
        /// Desc:7#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B07 {get;set;}

        /// <summary>
        /// Desc:8#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B08 {get;set;}

        /// <summary>
        /// Desc:9#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B09 {get;set;}

        /// <summary>
        /// Desc:10#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B10 {get;set;}

        /// <summary>
        /// Desc:11#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B11 {get;set;}

        /// <summary>
        /// Desc:12#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B12 {get;set;}

        /// <summary>
        /// Desc:13#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B13 {get;set;}

        /// <summary>
        /// Desc:14#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B14 {get;set;}

        /// <summary>
        /// Desc:15#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B15 {get;set;}

        /// <summary>
        /// Desc:16#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B16 {get;set;}

        /// <summary>
        /// Desc:17#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B17 {get;set;}

        /// <summary>
        /// Desc:18#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B18 {get;set;}

        /// <summary>
        /// Desc:19#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B19 {get;set;}

        /// <summary>
        /// Desc:20#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B20 {get;set;}

        /// <summary>
        /// Desc:21#风箱的宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_B21 {get;set;}

        /// <summary>
        /// Desc:破碎所需时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_BREAK_U_TIME {get;set;}

        /// <summary>
        /// Desc:烧结机长度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_SIN {get;set;}

        /// <summary>
        /// Desc:环冷机周长 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_RC {get;set;}

        /// <summary>
        /// Desc:板式给矿机所用时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_PF_U_TIME {get;set;}

        /// <summary>
        /// Desc:进料筛板式给料机下皮带长度(SF-1) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_SF_1 {get;set;}

        /// <summary>
        /// Desc:筛分所用的时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SCREEN_U_TIME {get;set;}

        /// <summary>
        /// Desc:检化验位置皮带距离长度(P_8) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LENGTH_P_8 {get;set;}

        /// <summary>
        /// Desc:料流追踪计算周期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_T1 {get;set;}

        /// <summary>
        /// Desc:指导采样校准周期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_T2 {get;set;}

        /// <summary>
        /// Desc:1H-1进一混皮带速度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SPE_1H_1 {get;set;}

        /// <summary>
        /// Desc:2H-1进二混皮带速度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SPE_2H_1 {get;set;}

        /// <summary>
        /// Desc:Z2-1出二混皮带速度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SPE_Z2_1 {get;set;}

        /// <summary>
        /// Desc:S-1进布料器皮带速度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SPE_S_1 {get;set;}

        /// <summary>
        /// Desc:SF-1进筛分皮带速度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SPE_SF_1 {get;set;}

        /// <summary>
        /// Desc:P-8成品皮带皮带速度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SPE_P_8 {get;set;}

        /// <summary>
        /// Desc:采集值取平均时间段 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_AVG_T {get;set;}

        /// <summary>
        /// 铁料料仓上限
        /// </summary>
        public Double? PAR_IRON_BUNK_UP { get; set; }
        /// <summary>
        /// 溶剂料仓上限
        /// </summary>
        public Double? PAR_SOL_BUNK_UP { get; set; }
        /// <summary>
        /// 灰类料仓上限
        /// </summary>
        public Double? PAR_DUST_BUNK_UP { get; set; }
        /// <summary>
        /// 燃料料仓上限
        /// </summary>
        public Double? PAR_FUEL_BUNK_UP { get; set; }

        /// <summary>
        /// Desc:取样时间间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_T3 { get; set; }

        /// <summary>
        /// Desc:混合料密度
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_BL_DENSITY { get; set; }
        /// <summary>
        /// Desc:台车宽度
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_TROLLEY_L { get; set; }
        /// <summary>
        /// Desc:取样点距成品皮带开始位置修正系数
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_K { get; set; }
    }
}
