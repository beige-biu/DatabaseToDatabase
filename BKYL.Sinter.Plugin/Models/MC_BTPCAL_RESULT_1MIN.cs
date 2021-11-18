using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MC_BTPCAL_RESULT_1MIN
    {
        
        /// <summary>
        /// Desc:插入时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:风箱最高温度值是否在最后一个风箱（是 = 1，否 = 0） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? BTPCAL_B_LAST_FLAG {get;set;}

        /// <summary>
        /// Desc:模型1MIN计算启停信号标志位（1：启用，0：禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? BTPCAL_SL_1MIN_FLAG {get;set;}

        /// <summary>
        /// Desc:终点调整启停标志位（1：启用，0：禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? BTPCAL_AD_FLAG {get;set;}

        /// <summary>
        /// Desc:第一条曲线11号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_1_11 {get;set;}

        /// <summary>
        /// Desc:第一条曲线12号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_1_12 {get;set;}

        /// <summary>
        /// Desc:第一条曲线13号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_1_13 {get;set;}

        /// <summary>
        /// Desc:第一条曲线14号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_1_14 {get;set;}

        /// <summary>
        /// Desc:第一条曲线15号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_1_15 {get;set;}

        /// <summary>
        /// Desc:第一条曲线16号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_1_16 {get;set;}

        /// <summary>
        /// Desc:第一条曲线17号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_1_17 {get;set;}

        /// <summary>
        /// Desc:第一条曲线18号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_1_18 {get;set;}

        /// <summary>
        /// Desc:第一条曲线19号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_1_19 {get;set;}

        /// <summary>
        /// Desc:第一条曲线20号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_1_20 {get;set;}

        /// <summary>
        /// Desc:第一条曲线21号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_1_21 {get;set;}

        /// <summary>
        /// Desc:第二条曲线11号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_2_11 {get;set;}

        /// <summary>
        /// Desc:第二条曲线12号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_2_12 {get;set;}

        /// <summary>
        /// Desc:第二条曲线13号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_2_13 {get;set;}

        /// <summary>
        /// Desc:第二条曲线14号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_2_14 {get;set;}

        /// <summary>
        /// Desc:第二条曲线15号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_2_15 {get;set;}

        /// <summary>
        /// Desc:第二条曲线16号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_2_16 {get;set;}

        /// <summary>
        /// Desc:第二条曲线17号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_2_17 {get;set;}

        /// <summary>
        /// Desc:第二条曲线18号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_2_18 {get;set;}

        /// <summary>
        /// Desc:第二条曲线19号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_2_19 {get;set;}

        /// <summary>
        /// Desc:第二条曲线20号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_2_20 {get;set;}

        /// <summary>
        /// Desc:第二条曲线21号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_2_21 {get;set;}

        /// <summary>
        /// Desc:第三条曲线11号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_3_11 {get;set;}

        /// <summary>
        /// Desc:第三条曲线12号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_3_12 {get;set;}

        /// <summary>
        /// Desc:第三条曲线13号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_3_13 {get;set;}

        /// <summary>
        /// Desc:第三条曲线14号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_3_14 {get;set;}

        /// <summary>
        /// Desc:第三条曲线15号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_3_15 {get;set;}

        /// <summary>
        /// Desc:第三条曲线16号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_3_16 {get;set;}

        /// <summary>
        /// Desc:第三条曲线17号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_3_17 {get;set;}

        /// <summary>
        /// Desc:第三条曲线18号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_3_18 {get;set;}

        /// <summary>
        /// Desc:第三条曲线19号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_3_19 {get;set;}

        /// <summary>
        /// Desc:第三条曲线20号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_3_20 {get;set;}

        /// <summary>
        /// Desc:第三条曲线21号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_3_21 {get;set;}

        /// <summary>
        /// Desc:第四条曲线11号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_4_11 {get;set;}

        /// <summary>
        /// Desc:第四条曲线12号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_4_12 {get;set;}

        /// <summary>
        /// Desc:第四条曲线13号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_4_13 {get;set;}

        /// <summary>
        /// Desc:第四条曲线14号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_4_14 {get;set;}

        /// <summary>
        /// Desc:第四条曲线15号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_4_15 {get;set;}

        /// <summary>
        /// Desc:第四条曲线16号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_4_16 {get;set;}

        /// <summary>
        /// Desc:第四条曲线17号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_4_17 {get;set;}

        /// <summary>
        /// Desc:第四条曲线18号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_4_18 {get;set;}

        /// <summary>
        /// Desc:第四条曲线19号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_4_19 {get;set;}

        /// <summary>
        /// Desc:第四条曲线20号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_4_20 {get;set;}

        /// <summary>
        /// Desc:第四条曲线21号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_4_21 {get;set;}

        /// <summary>
        /// Desc:第五条曲线11号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_5_11 {get;set;}

        /// <summary>
        /// Desc:第五条曲线12号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_5_12 {get;set;}

        /// <summary>
        /// Desc:第五条曲线13号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_5_13 {get;set;}

        /// <summary>
        /// Desc:第五条曲线14号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_5_14 {get;set;}

        /// <summary>
        /// Desc:第五条曲线15号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_5_15 {get;set;}

        /// <summary>
        /// Desc:第五条曲线16号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_5_16 {get;set;}

        /// <summary>
        /// Desc:第五条曲线17号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_5_17 {get;set;}

        /// <summary>
        /// Desc:第五条曲线18号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_5_18 {get;set;}

        /// <summary>
        /// Desc:第五条曲线19号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_5_19 {get;set;}

        /// <summary>
        /// Desc:第五条曲线20号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_5_20 {get;set;}

        /// <summary>
        /// Desc:第五条曲线21号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_5_21 {get;set;}

        /// <summary>
        /// Desc:第六条曲线11号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_6_11 {get;set;}

        /// <summary>
        /// Desc:第六条曲线12号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_6_12 {get;set;}

        /// <summary>
        /// Desc:第六条曲线13号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_6_13 {get;set;}

        /// <summary>
        /// Desc:第六条曲线14号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_6_14 {get;set;}

        /// <summary>
        /// Desc:第六条曲线15号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_6_15 {get;set;}

        /// <summary>
        /// Desc:第六条曲线16号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_6_16 {get;set;}

        /// <summary>
        /// Desc:第六条曲线17号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_6_17 {get;set;}

        /// <summary>
        /// Desc:第六条曲线18号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_6_18 {get;set;}

        /// <summary>
        /// Desc:第六条曲线19号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_6_19 {get;set;}

        /// <summary>
        /// Desc:第六条曲线20号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_6_20 {get;set;}

        /// <summary>
        /// Desc:第六条曲线21号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_6_21 {get;set;}

        /// <summary>
        /// Desc:第七条曲线11号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_7_11 {get;set;}

        /// <summary>
        /// Desc:第七条曲线12号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_7_12 {get;set;}

        /// <summary>
        /// Desc:第七条曲线13号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_7_13 {get;set;}

        /// <summary>
        /// Desc:第七条曲线14号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_7_14 {get;set;}

        /// <summary>
        /// Desc:第七条曲线15号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_7_15 {get;set;}

        /// <summary>
        /// Desc:第七条曲线16号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_7_16 {get;set;}

        /// <summary>
        /// Desc:第七条曲线17号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_7_17 {get;set;}

        /// <summary>
        /// Desc:第七条曲线18号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_7_18 {get;set;}

        /// <summary>
        /// Desc:第七条曲线19号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_7_19 {get;set;}

        /// <summary>
        /// Desc:第七条曲线20号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_7_20 {get;set;}

        /// <summary>
        /// Desc:第七条曲线21号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_7_21 {get;set;}

        /// <summary>
        /// Desc:第八条（平均）曲线11号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_8_AVG_11 {get;set;}

        /// <summary>
        /// Desc:第八条（平均）曲线12号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_8_AVG_12 {get;set;}

        /// <summary>
        /// Desc:第八条（平均）曲线13号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_8_AVG_13 {get;set;}

        /// <summary>
        /// Desc:第八条（平均）曲线14号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_8_AVG_14 {get;set;}

        /// <summary>
        /// Desc:第八条（平均）曲线15号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_8_AVG_15 {get;set;}

        /// <summary>
        /// Desc:第八条（平均）曲线16号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_8_AVG_16 {get;set;}

        /// <summary>
        /// Desc:第八条（平均）曲线17号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_8_AVG_17 {get;set;}

        /// <summary>
        /// Desc:第八条（平均）曲线18号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_8_AVG_18 {get;set;}

        /// <summary>
        /// Desc:第八条（平均）曲线19号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_8_AVG_19 {get;set;}

        /// <summary>
        /// Desc:第八条（平均）曲线20号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_8_AVG_20 {get;set;}

        /// <summary>
        /// Desc:第八条（平均）曲线21号风箱温度值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_TE_8_AVG_21 {get;set;}

        /// <summary>
        /// Desc: 1#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B01_PT_L {get;set;}

        /// <summary>
        /// Desc: 1#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B01_PT_R {get;set;}

        /// <summary>
        /// Desc: 2#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B02_PT_L {get;set;}

        /// <summary>
        /// Desc: 2#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B02_PT_R {get;set;}

        /// <summary>
        /// Desc: 3#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B03_PT_L {get;set;}

        /// <summary>
        /// Desc: 3#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B03_PT_R {get;set;}

        /// <summary>
        /// Desc: 4#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B04_PT_L {get;set;}

        /// <summary>
        /// Desc: 4#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B04_PT_R {get;set;}

        /// <summary>
        /// Desc: 5#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B05_PT_L {get;set;}

        /// <summary>
        /// Desc: 5#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B05_PT_R {get;set;}

        /// <summary>
        /// Desc: 6#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B06_PT_L {get;set;}

        /// <summary>
        /// Desc: 6#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B06_PT_R {get;set;}

        /// <summary>
        /// Desc: 7#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B07_PT_L {get;set;}

        /// <summary>
        /// Desc: 7#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B07_PT_R {get;set;}

        /// <summary>
        /// Desc: 8#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B08_PT_L {get;set;}

        /// <summary>
        /// Desc: 8#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B08_PT_R {get;set;}

        /// <summary>
        /// Desc: 9#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B09_PT_L {get;set;}

        /// <summary>
        /// Desc: 9#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B09_PT_R {get;set;}

        /// <summary>
        /// Desc: 10#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B10_PT_L {get;set;}

        /// <summary>
        /// Desc: 10#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B10_PT_R {get;set;}

        /// <summary>
        /// Desc: 11#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B11_PT_L {get;set;}

        /// <summary>
        /// Desc: 11#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B11_PT_R {get;set;}

        /// <summary>
        /// Desc: 12#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B12_PT_L {get;set;}

        /// <summary>
        /// Desc: 12#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B12_PT_R {get;set;}

        /// <summary>
        /// Desc: 13#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B13_PT_L {get;set;}

        /// <summary>
        /// Desc: 13#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B13_PT_R {get;set;}

        /// <summary>
        /// Desc: 14#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B14_PT_L {get;set;}

        /// <summary>
        /// Desc: 14#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B14_PT_R {get;set;}

        /// <summary>
        /// Desc: 15#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B15_PT_L {get;set;}

        /// <summary>
        /// Desc: 15#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B15_PT_R {get;set;}

        /// <summary>
        /// Desc: 16#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B16_PT_L {get;set;}

        /// <summary>
        /// Desc: 16#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B16_PT_R {get;set;}

        /// <summary>
        /// Desc: 17#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B17_PT_L {get;set;}

        /// <summary>
        /// Desc: 17#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B17_PT_R {get;set;}

        /// <summary>
        /// Desc: 18#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B18_PT_L {get;set;}

        /// <summary>
        /// Desc: 18#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B18_PT_R {get;set;}

        /// <summary>
        /// Desc: 19#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B19_PT_L {get;set;}

        /// <summary>
        /// Desc: 19#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B19_PT_R {get;set;}

        /// <summary>
        /// Desc: 20#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B20_PT_L {get;set;}

        /// <summary>
        /// Desc: 20#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B20_PT_R {get;set;}

        /// <summary>
        /// Desc: 21#风箱左压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B21_PT_L {get;set;}

        /// <summary>
        /// Desc: 21#风箱右压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B21_PT_R {get;set;}

        /// <summary>
        /// Desc:1#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B1_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:1#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B1_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:2#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B2_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:2#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B2_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:3#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B3_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:3#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B3_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:4#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B4_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:4#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B4_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:5#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B5_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:5#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B5_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:6#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B6_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:6#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B6_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:7#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B7_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:7#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B7_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:8#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B8_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:8#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B8_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:9#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B9_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:9#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B9_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:10#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B10_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:10#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B10_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:11#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B11_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:11#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B11_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:12#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B12_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:12#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B12_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:13#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B13_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:13#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B13_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:14#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B14_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:14#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B14_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:15#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B15_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:15#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B15_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:16#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B16_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:16#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B16_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:17#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B17_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:17#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B17_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:18#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B18_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:18#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B18_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:19#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B19_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:19#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B19_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:20#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B20_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:20#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B20_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:21#左风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B21_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:21#右风箱开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B21_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:1#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B1_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:1#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B1_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:2#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B2_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:2#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B2_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:3#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B3_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:3#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B3_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:4#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B4_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:4#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B4_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:5#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B5_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:5#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B5_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:6#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B6_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:6#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B6_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:7#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B7_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:7#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B7_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:8#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B8_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:8#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B8_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:9#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B9_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:9#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B9_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:10#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B10_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:10#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B10_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:11#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B11_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:11#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B11_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:12#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B12_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:12#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B12_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:13#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B13_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:13#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B13_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:14#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B14_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:14#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B14_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:15#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B15_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:15#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B15_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:16#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B16_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:16#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B16_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:17#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B17_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:17#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B17_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:18#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B18_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:18#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B18_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:19#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B19_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:19#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B19_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:20#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B20_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:20#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B20_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:21#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B21_ZT_L_FK {get;set;}

        /// <summary>
        /// Desc:21#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_B21_ZT_R_FK {get;set;}

        /// <summary>
        /// Desc:主抽1风机转速（频率）设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_FAN_1_SP_FRE {get;set;}

        /// <summary>
        /// Desc:主抽2风机转速（频率）设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_FAN_2_SP_FRE {get;set;}

        /// <summary>
        /// Desc:主抽1风机转速（频率）反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_FAN_1_PV_FRE {get;set;}

        /// <summary>
        /// Desc:主抽2风机转速（频率）反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_FAN_2_PV_FRE {get;set;}

        /// <summary>
        /// Desc:主抽1入口导叶门开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_VE_1_SP_OD {get;set;}

        /// <summary>
        /// Desc:主抽2入口导叶门开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_VE_2_SP_OD {get;set;}

        /// <summary>
        /// Desc:主抽1入口导叶门开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_VE_1_PV_OD {get;set;}

        /// <summary>
        /// Desc:主抽2入口导叶门开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_VE_2_PV_OD {get;set;}

        /// <summary>
        /// Desc:BTP位置目标值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_AIM_BTP {get;set;}

        /// <summary>
        /// Desc:BRP位置目标值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_AIM_BRP {get;set;}

        /// <summary>
        /// Desc:烧结台车料厚1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_SMALL_SG_PV_1 {get;set;}

        /// <summary>
        /// Desc:烧结台车料厚2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_SMALL_SG_PV_2 {get;set;}

        /// <summary>
        /// Desc:烧结台车料厚3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_SMALL_SG_PV_3 {get;set;}

        /// <summary>
        /// Desc:烧结台车料厚4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_SMALL_SG_PV_4 {get;set;}

        /// <summary>
        /// Desc:烧结台车料厚5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_SMALL_SG_PV_5 {get;set;}

        /// <summary>
        /// Desc:烧结台车料厚6 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_SMALL_SG_PV_6 {get;set;}

        /// <summary>
        /// Desc:烧结台车料厚7 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_SMALL_SG_PV_7 {get;set;}

        /// <summary>
        /// Desc:烧结台车平均料厚 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_SMALL_SG_PV_AVG {get;set;}

        /// <summary>
        /// Desc:烧结机机速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_SIN_MS_SP {get;set;}

        /// <summary>
        /// Desc:主抽1大烟道温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_SB_1_FLUE_TE {get;set;}

        /// <summary>
        /// Desc:主抽2大烟道温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_SB_2_FLUE_TE {get;set;}

        /// <summary>
        /// Desc:主抽大烟道温度平均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_SB_FLUE_TE_AVG {get;set;}

        /// <summary>
        /// Desc:主抽1大烟道压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_SB_1_FLUE_PT {get;set;}

        /// <summary>
        /// Desc:主抽2大烟道压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_SB_2_FLUE_PT {get;set;}

        /// <summary>
        /// Desc:第一条二次曲线-方程2次系数（a） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_1_A {get;set;}

        /// <summary>
        /// Desc:第一条二次曲线-方程1次系数（b） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_1_B {get;set;}

        /// <summary>
        /// Desc:第一条二次曲线-方程常数值（c） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_1_C {get;set;}

        /// <summary>
        /// Desc:第一条二次曲线-BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE2_1_X_BTP {get;set;}

        /// <summary>
        /// Desc:第一条二次曲线-BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE2_1_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第二条二次曲线-方程2次系数（a） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_2_A {get;set;}

        /// <summary>
        /// Desc:第二条二次曲线-方程1次系数（b） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_2_B {get;set;}

        /// <summary>
        /// Desc:第二条二次曲线-方程常数值（c） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_2_C {get;set;}

        /// <summary>
        /// Desc:第二条二次曲线-BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE2_2_X_BTP {get;set;}

        /// <summary>
        /// Desc:第二条二次曲线-BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE2_2_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第三条二次曲线-方程2次系数（a） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_3_A {get;set;}

        /// <summary>
        /// Desc:第三条二次曲线-方程1次系数（b） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_3_B {get;set;}

        /// <summary>
        /// Desc:第三条二次曲线-方程常数值（c） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_3_C {get;set;}

        /// <summary>
        /// Desc:第三条二次曲线-BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE2_3_X_BTP {get;set;}

        /// <summary>
        /// Desc:第三条二次曲线-BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE2_3_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第四条二次曲线-方程2次系数（a） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_4_A {get;set;}

        /// <summary>
        /// Desc:第四条二次曲线-方程1次系数（b） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_4_B {get;set;}

        /// <summary>
        /// Desc:第四条二次曲线-方程常数值（c） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_4_C {get;set;}

        /// <summary>
        /// Desc:第四条二次曲线-BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE2_4_X_BTP {get;set;}

        /// <summary>
        /// Desc:第四条二次曲线-BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE2_4_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第五条二次曲线-方程2次系数（a） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_5_A {get;set;}

        /// <summary>
        /// Desc:第五条二次曲线-方程1次系数（b） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_5_B {get;set;}

        /// <summary>
        /// Desc:第五条二次曲线-方程常数值（c） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_5_C {get;set;}

        /// <summary>
        /// Desc:第五条二次曲线-BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE2_5_X_BTP {get;set;}

        /// <summary>
        /// Desc:第五条二次曲线-BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE2_5_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第六条二次曲线-方程2次系数（a） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_6_A {get;set;}

        /// <summary>
        /// Desc:第六条二次曲线-方程1次系数（b） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_6_B {get;set;}

        /// <summary>
        /// Desc:第六条二次曲线-方程常数值（c） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_6_C {get;set;}

        /// <summary>
        /// Desc:第六条二次曲线-BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE2_6_X_BTP {get;set;}

        /// <summary>
        /// Desc:第六条二次曲线-BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE2_6_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第七条二次曲线-方程2次系数（a） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_7_A {get;set;}

        /// <summary>
        /// Desc:第七条二次曲线-方程1次系数（b） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_7_B {get;set;}

        /// <summary>
        /// Desc:第七条二次曲线-方程常数值（c） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_7_C {get;set;}

        /// <summary>
        /// Desc:第七条二次曲线-BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE2_7_X_BTP {get;set;}

        /// <summary>
        /// Desc:第七条二次曲线-BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE2_7_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第八条（平均）二次曲线-方程2次系数（a） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_8_A {get;set;}

        /// <summary>
        /// Desc:第八条（平均）二次曲线-方程1次系数（b） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_8_B {get;set;}

        /// <summary>
        /// Desc:第八条（平均）二次曲线-方程常数值（c） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? BTPCAL_QE2_8_C {get;set;}

        /// <summary>
        /// Desc:第八条（平均）二次曲线-BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE2_8_X_AVG_BTP {get;set;}

        /// <summary>
        /// Desc:第八条（平均）二次曲线-BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE2_8_TE_AVG_BTP {get;set;}

        /// <summary>
        /// Desc:第一条四次曲线-BRP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_1_X_BRP {get;set;}

        /// <summary>
        /// Desc:第一条四次曲线-BRP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_1_TE_BRP {get;set;}

        /// <summary>
        /// Desc:第一条四次曲线-BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_1_X_BTP {get;set;}

        /// <summary>
        /// Desc:第一条四次曲线-BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_1_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第二条四次曲线-BRP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_2_X_BRP {get;set;}

        /// <summary>
        /// Desc:第二条四次曲线-BRP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_2_TE_BRP {get;set;}

        /// <summary>
        /// Desc:第二条四次曲线-BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_2_X_BTP {get;set;}

        /// <summary>
        /// Desc:第二条四次曲线-BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_2_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第三条四次曲线-BRP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_3_X_BRP {get;set;}

        /// <summary>
        /// Desc:第三条四次曲线-BRP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_3_TE_BRP {get;set;}

        /// <summary>
        /// Desc:第三条四次曲线-BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_3_X_BTP {get;set;}

        /// <summary>
        /// Desc:第三条四次曲线-BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_3_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第四条四次曲线-BRP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_4_X_BRP {get;set;}

        /// <summary>
        /// Desc:第四条四次曲线-BRP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_4_TE_BRP {get;set;}

        /// <summary>
        /// Desc:第四条四次曲线-BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_4_X_BTP {get;set;}

        /// <summary>
        /// Desc:第四条四次曲线-BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_4_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第五条四次曲线-BRP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_5_X_BRP {get;set;}

        /// <summary>
        /// Desc:第五条四次曲线-BRP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_5_TE_BRP {get;set;}

        /// <summary>
        /// Desc:第五条四次曲线-BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_5_X_BTP {get;set;}

        /// <summary>
        /// Desc:第五条四次曲线-BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_5_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第六条四次曲线-BRP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_6_X_BRP {get;set;}

        /// <summary>
        /// Desc:第六条四次曲线-BRP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_6_TE_BRP {get;set;}

        /// <summary>
        /// Desc:第六条四次曲线-BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_6_X_BTP {get;set;}

        /// <summary>
        /// Desc:第六条四次曲线-BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_6_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第七条四次曲线-BRP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_7_X_BRP {get;set;}

        /// <summary>
        /// Desc:第七条四次曲线-BRP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_7_TE_BRP {get;set;}

        /// <summary>
        /// Desc:第七条四次曲线-BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_7_X_BTP {get;set;}

        /// <summary>
        /// Desc:第七条四次曲线-BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_7_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第八条（平均）四次曲线-BRP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_8_X_AVG_BRP {get;set;}

        /// <summary>
        /// Desc:第八条（平均）四次曲线-BRP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_8_TE_AVG_BRP {get;set;}

        /// <summary>
        /// Desc:第八条（平均）四次曲线-BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_8_X_AVG_BTP {get;set;}

        /// <summary>
        /// Desc:第八条（平均）四次曲线-BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_QE4_8_TE_AVG_BTP {get;set;}

        /// <summary>
        /// Desc:第一条曲线输出BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_OUT_1_X_BTP {get;set;}

        /// <summary>
        /// Desc:第一条曲线输出BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_OUT_1_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第二条曲线输出BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_OUT_2_X_BTP {get;set;}

        /// <summary>
        /// Desc:第二条曲线输出BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_OUT_2_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第三条曲线输出BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_OUT_3_X_BTP {get;set;}

        /// <summary>
        /// Desc:第三条曲线输出BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_OUT_3_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第四条曲线输出BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_OUT_4_X_BTP {get;set;}

        /// <summary>
        /// Desc:第四条曲线输出BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_OUT_4_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第五条曲线输出BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_OUT_5_X_BTP {get;set;}

        /// <summary>
        /// Desc:第五条曲线输出BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_OUT_5_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第六条曲线输出BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_OUT_6_X_BTP {get;set;}

        /// <summary>
        /// Desc:第六条曲线输出BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_OUT_6_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第七条曲线输出BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_OUT_7_X_BTP {get;set;}

        /// <summary>
        /// Desc:第七条曲线输出BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_OUT_7_TE_BTP {get;set;}

        /// <summary>
        /// Desc:第八条（平均）曲线输出BTP位置 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_OUT_8_X_AVG_BTP {get;set;}

        /// <summary>
        /// Desc:第八条（平均）曲线输出BTP位置温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_OUT_8_TE_AVG_BTP {get;set;}

        /// <summary>
        /// Desc:BTP位置风箱号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? BTPCAL_BTP_NUM {get;set;}

        /// <summary>
        /// Desc:BRP位置风箱号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? BTPCAL_BRP_NUM {get;set;}

        /// <summary>
        /// Desc:垂直烧结速度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_V {get;set;}

        /// <summary>
        /// Desc:透气性指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_AIR_INDEX {get;set;}

        /// <summary>
        /// Desc:烧结机实际机速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_SIN_MS_PV {get;set;}

        /// <summary>
        /// Desc:倒推时间内烧结机实际机速平均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_SIN_MS_TM_PV {get;set;}

        /// <summary>
        /// Desc:垂直烧结速度计算倒推平均料厚 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_SG_TM_PV {get;set;}

        /// <summary>
        /// Desc:倒推时间（烧结终点到雷达料位计时间） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BTPCAL_T_N {get;set;}

    }
}
