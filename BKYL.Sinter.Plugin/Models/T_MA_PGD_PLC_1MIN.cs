using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class T_MA_PGD_PLC_1MIN
    {

        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc: 1#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double B01_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 1#风箱温度右1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B01_TE_R_1_AVG {get;set;}

        /// <summary>
        /// Desc: 2#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B02_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 2#风箱温度右1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B02_TE_R_1_AVG {get;set;}

        /// <summary>
        /// Desc: 3#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B03_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 3#风箱温度右1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B03_TE_R_1_AVG {get;set;}

        /// <summary>
        /// Desc: 4#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B04_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 4#风箱温度右1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B04_TE_R_1_AVG {get;set;}

        /// <summary>
        /// Desc: 5#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B05_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 5#风箱温度右1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B05_TE_R_1_AVG {get;set;}

        /// <summary>
        /// Desc: 6#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B06_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 6#风箱温度右1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B06_TE_R_1_AVG {get;set;}

        /// <summary>
        /// Desc: 7#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B07_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 7#风箱温度右1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B07_TE_R_1_AVG {get;set;}

        /// <summary>
        /// Desc: 8#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B08_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 8#风箱温度右1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B08_TE_R_1_AVG {get;set;}

        /// <summary>
        /// Desc: 9#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B09_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 9#风箱温度右1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B09_TE_R_1_AVG {get;set;}

        /// <summary>
        /// Desc: 10#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B10_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 10#风箱温度右1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B10_TE_R_1_AVG {get;set;}

        /// <summary>
        /// Desc: 11#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B11_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 11#风箱温度左2取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B11_TE_L_2_AVG {get;set;}

        /// <summary>
        /// Desc: 11#风箱温度左3取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B11_TE_L_3_AVG {get;set;}

        /// <summary>
        /// Desc: 11#风箱温度左4取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B11_TE_L_4_AVG {get;set;}

        /// <summary>
        /// Desc: 11#风箱温度左5取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B11_TE_L_5_AVG {get;set;}

        /// <summary>
        /// Desc: 11#风箱温度左6取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B11_TE_L_6_AVG {get;set;}

        /// <summary>
        /// Desc: 11#风箱温度左7取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B11_TE_L_7_AVG {get;set;}

        /// <summary>
        /// Desc: 12#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B12_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 12#风箱温度左2取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B12_TE_L_2_AVG {get;set;}

        /// <summary>
        /// Desc: 12#风箱温度左3取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B12_TE_L_3_AVG {get;set;}

        /// <summary>
        /// Desc: 12#风箱温度左4取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B12_TE_L_4_AVG {get;set;}

        /// <summary>
        /// Desc: 12#风箱温度左5取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B12_TE_L_5_AVG {get;set;}

        /// <summary>
        /// Desc: 12#风箱温度左6取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B12_TE_L_6_AVG {get;set;}

        /// <summary>
        /// Desc: 12#风箱温度左7取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B12_TE_L_7_AVG {get;set;}

        /// <summary>
        /// Desc: 13#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B13_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 13#风箱温度左2取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B13_TE_L_2_AVG {get;set;}

        /// <summary>
        /// Desc: 13#风箱温度左3取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B13_TE_L_3_AVG {get;set;}

        /// <summary>
        /// Desc: 13#风箱温度左4取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B13_TE_L_4_AVG {get;set;}

        /// <summary>
        /// Desc: 13#风箱温度左5取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B13_TE_L_5_AVG {get;set;}

        /// <summary>
        /// Desc: 13#风箱温度左6取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B13_TE_L_6_AVG {get;set;}

        /// <summary>
        /// Desc: 13#风箱温度左7取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B13_TE_L_7_AVG {get;set;}

        /// <summary>
        /// Desc: 14#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B14_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 14#风箱温度左2取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B14_TE_L_2_AVG {get;set;}

        /// <summary>
        /// Desc: 14#风箱温度左3取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B14_TE_L_3_AVG {get;set;}

        /// <summary>
        /// Desc: 14#风箱温度左4取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B14_TE_L_4_AVG {get;set;}

        /// <summary>
        /// Desc: 14#风箱温度左5取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B14_TE_L_5_AVG {get;set;}

        /// <summary>
        /// Desc: 14#风箱温度左6取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B14_TE_L_6_AVG {get;set;}

        /// <summary>
        /// Desc: 14#风箱温度左7取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B14_TE_L_7_AVG {get;set;}

        /// <summary>
        /// Desc: 15#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B15_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 15#风箱温度左2取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B15_TE_L_2_AVG {get;set;}

        /// <summary>
        /// Desc: 15#风箱温度左3取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B15_TE_L_3_AVG {get;set;}

        /// <summary>
        /// Desc: 15#风箱温度左4取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B15_TE_L_4_AVG {get;set;}

        /// <summary>
        /// Desc: 15#风箱温度左5取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B15_TE_L_5_AVG {get;set;}

        /// <summary>
        /// Desc: 15#风箱温度左6取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B15_TE_L_6_AVG {get;set;}

        /// <summary>
        /// Desc: 15#风箱温度左7取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B15_TE_L_7_AVG {get;set;}

        /// <summary>
        /// Desc: 16#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B16_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 16#风箱温度左2取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B16_TE_L_2_AVG {get;set;}

        /// <summary>
        /// Desc: 16#风箱温度左3取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B16_TE_L_3_AVG {get;set;}

        /// <summary>
        /// Desc: 16#风箱温度左4取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B16_TE_L_4_AVG {get;set;}

        /// <summary>
        /// Desc: 16#风箱温度左5取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B16_TE_L_5_AVG {get;set;}

        /// <summary>
        /// Desc: 16#风箱温度左6取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B16_TE_L_6_AVG {get;set;}

        /// <summary>
        /// Desc: 16#风箱温度左7取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B16_TE_L_7_AVG {get;set;}

        /// <summary>
        /// Desc: 17#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B17_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 17#风箱温度左2取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B17_TE_L_2_AVG {get;set;}

        /// <summary>
        /// Desc: 17#风箱温度左3取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B17_TE_L_3_AVG {get;set;}

        /// <summary>
        /// Desc: 17#风箱温度左4取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B17_TE_L_4_AVG {get;set;}

        /// <summary>
        /// Desc: 17#风箱温度左5取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B17_TE_L_5_AVG {get;set;}

        /// <summary>
        /// Desc: 17#风箱温度左6取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B17_TE_L_6_AVG {get;set;}

        /// <summary>
        /// Desc: 17#风箱温度左7取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B17_TE_L_7_AVG {get;set;}

        /// <summary>
        /// Desc: 18#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B18_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 18#风箱温度左2取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B18_TE_L_2_AVG {get;set;}

        /// <summary>
        /// Desc: 18#风箱温度左3取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B18_TE_L_3_AVG {get;set;}

        /// <summary>
        /// Desc: 18#风箱温度左4取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B18_TE_L_4_AVG {get;set;}

        /// <summary>
        /// Desc: 18#风箱温度左5取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B18_TE_L_5_AVG {get;set;}

        /// <summary>
        /// Desc: 18#风箱温度左6取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B18_TE_L_6_AVG {get;set;}

        /// <summary>
        /// Desc: 18#风箱温度左7取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B18_TE_L_7_AVG {get;set;}

        /// <summary>
        /// Desc: 19#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B19_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 19#风箱温度左2取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B19_TE_L_2_AVG {get;set;}

        /// <summary>
        /// Desc: 19#风箱温度左3取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B19_TE_L_3_AVG {get;set;}

        /// <summary>
        /// Desc: 19#风箱温度左4取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B19_TE_L_4_AVG {get;set;}

        /// <summary>
        /// Desc: 19#风箱温度左5取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B19_TE_L_5_AVG {get;set;}

        /// <summary>
        /// Desc: 19#风箱温度左6取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B19_TE_L_6_AVG {get;set;}

        /// <summary>
        /// Desc: 19#风箱温度左7取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B19_TE_L_7_AVG {get;set;}

        /// <summary>
        /// Desc: 20#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B20_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 20#风箱温度左2取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B20_TE_L_2_AVG {get;set;}

        /// <summary>
        /// Desc: 20#风箱温度左3取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B20_TE_L_3_AVG {get;set;}

        /// <summary>
        /// Desc: 20#风箱温度左4取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B20_TE_L_4_AVG {get;set;}

        /// <summary>
        /// Desc: 20#风箱温度左5取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B20_TE_L_5_AVG {get;set;}

        /// <summary>
        /// Desc: 20#风箱温度左6取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B20_TE_L_6_AVG {get;set;}

        /// <summary>
        /// Desc: 20#风箱温度左7取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B20_TE_L_7_AVG {get;set;}

        /// <summary>
        /// Desc: 21#风箱温度左1取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B21_TE_L_1_AVG {get;set;}

        /// <summary>
        /// Desc: 21#风箱温度左2取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B21_TE_L_2_AVG {get;set;}

        /// <summary>
        /// Desc: 21#风箱温度左3取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B21_TE_L_3_AVG {get;set;}

        /// <summary>
        /// Desc: 21#风箱温度左4取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B21_TE_L_4_AVG {get;set;}

        /// <summary>
        /// Desc: 21#风箱温度左5取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B21_TE_L_5_AVG {get;set;}

        /// <summary>
        /// Desc: 21#风箱温度左6取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B21_TE_L_6_AVG {get;set;}

        /// <summary>
        /// Desc: 21#风箱温度左7取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B21_TE_L_7_AVG {get;set;}

        /// <summary>
        /// Desc: 1#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B01_PT_L {get;set;}

        /// <summary>
        /// Desc: 1#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B01_PT_R {get;set;}

        /// <summary>
        /// Desc: 2#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B02_PT_L {get;set;}

        /// <summary>
        /// Desc: 2#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B02_PT_R {get;set;}

        /// <summary>
        /// Desc: 3#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B03_PT_L {get;set;}

        /// <summary>
        /// Desc: 3#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B03_PT_R {get;set;}

        /// <summary>
        /// Desc: 4#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B04_PT_L {get;set;}

        /// <summary>
        /// Desc: 4#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B04_PT_R {get;set;}

        /// <summary>
        /// Desc: 5#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B05_PT_L {get;set;}

        /// <summary>
        /// Desc: 5#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B05_PT_R {get;set;}

        /// <summary>
        /// Desc: 6#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B06_PT_L {get;set;}

        /// <summary>
        /// Desc: 6#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B06_PT_R {get;set;}

        /// <summary>
        /// Desc: 7#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B07_PT_L {get;set;}

        /// <summary>
        /// Desc: 7#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B07_PT_R {get;set;}

        /// <summary>
        /// Desc: 8#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B08_PT_L {get;set;}

        /// <summary>
        /// Desc: 8#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B08_PT_R {get;set;}

        /// <summary>
        /// Desc: 9#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B09_PT_L {get;set;}

        /// <summary>
        /// Desc: 9#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B09_PT_R {get;set;}

        /// <summary>
        /// Desc: 10#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B10_PT_L {get;set;}

        /// <summary>
        /// Desc: 10#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B10_PT_R {get;set;}

        /// <summary>
        /// Desc: 11#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B11_PT_L {get;set;}

        /// <summary>
        /// Desc: 11#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B11_PT_R {get;set;}

        /// <summary>
        /// Desc: 12#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B12_PT_L {get;set;}

        /// <summary>
        /// Desc: 12#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B12_PT_R {get;set;}

        /// <summary>
        /// Desc: 13#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B13_PT_L {get;set;}

        /// <summary>
        /// Desc: 13#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B13_PT_R {get;set;}

        /// <summary>
        /// Desc: 14#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B14_PT_L {get;set;}

        /// <summary>
        /// Desc: 14#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B14_PT_R {get;set;}

        /// <summary>
        /// Desc: 15#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B15_PT_L {get;set;}

        /// <summary>
        /// Desc: 15#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B15_PT_R {get;set;}

        /// <summary>
        /// Desc: 16#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B16_PT_L {get;set;}

        /// <summary>
        /// Desc: 16#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B16_PT_R {get;set;}

        /// <summary>
        /// Desc: 17#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B17_PT_L {get;set;}

        /// <summary>
        /// Desc: 17#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B17_PT_R {get;set;}

        /// <summary>
        /// Desc: 18#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B18_PT_L {get;set;}

        /// <summary>
        /// Desc: 18#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B18_PT_R {get;set;}

        /// <summary>
        /// Desc: 19#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B19_PT_L {get;set;}

        /// <summary>
        /// Desc: 19#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B19_PT_R {get;set;}

        /// <summary>
        /// Desc: 20#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B20_PT_L {get;set;}

        /// <summary>
        /// Desc: 20#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B20_PT_R {get;set;}

        /// <summary>
        /// Desc: 21#风箱左压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B21_PT_L {get;set;}

        /// <summary>
        /// Desc: 21#风箱右压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B21_PT_R {get;set;}

        /// <summary>
        /// Desc:1#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B01_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:1#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B01_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:2#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B02_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:2#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B02_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:3#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B03_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:3#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B03_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:4#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B04_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:4#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B04_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:5#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B05_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:5#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B05_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:6#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B06_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:6#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B06_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:7#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B07_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:7#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B07_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:8#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B08_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:8#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B08_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:9#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B09_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:9#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B09_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:10#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B10_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:10#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B10_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:11#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B11_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:11#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B11_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:12#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B12_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:12#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B12_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:13#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B13_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:13#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B13_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:14#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B14_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:14#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B14_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:15#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B15_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:15#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B15_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:16#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B16_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:16#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B16_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:17#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B17_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:17#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B17_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:18#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B18_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:18#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B18_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:19#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B19_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:19#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B19_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:20#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B20_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:20#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B20_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:21#左风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B21_ZT_L_SP {get;set;}

        /// <summary>
        /// Desc:21#右风箱开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B21_ZT_R_SP {get;set;}

        /// <summary>
        /// Desc:1#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B01_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:1#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B01_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:2#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B02_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:2#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B02_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:3#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B03_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:3#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B03_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:4#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B04_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:4#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B04_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:5#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B05_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:5#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B05_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:6#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B06_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:6#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B06_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:7#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B07_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:7#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B07_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:8#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B08_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:8#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B08_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:9#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B09_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:9#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B09_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:10#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B10_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:10#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B10_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:11#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B11_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:11#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B11_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:12#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B12_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:12#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B12_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:13#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B13_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:13#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B13_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:14#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B14_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:14#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B14_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:15#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B15_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:15#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B15_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:16#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B16_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:16#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B16_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:17#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B17_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:17#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B17_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:18#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B18_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:18#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B18_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:19#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B19_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:19#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B19_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:20#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B20_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:20#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B20_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:21#左风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B21_ZT_L_PV {get;set;}

        /// <summary>
        /// Desc:21#右风箱开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double B21_ZT_R_PV {get;set;}

        /// <summary>
        /// Desc:点火器对应风箱开度上限 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_B_IGNITER_ZT_UP {get;set;}

        /// <summary>
        /// Desc:点火器对应风箱开度下限 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_B_IGNITER_ZT_LOW {get;set;}

        /// <summary>
        /// Desc:除去点火器部分风箱开度上限 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_BQC_IGNITER_ZT_UP {get;set;}

        /// <summary>
        /// Desc:除去点火器部分风箱开度下限 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_BQC_IGNITER_ZT_LOW {get;set;}

        /// <summary>
        /// Desc:BTP目标值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_BTR_AIM {get;set;}

        /// <summary>
        /// Desc:BRP目标值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_BRP_AIM {get;set;}

        /// <summary>
        /// Desc:主抽1风机启停信号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public bool PS_MA_FAN_1_SL {get;set;}

        /// <summary>
        /// Desc:主抽2风机启停信号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public bool PS_MA_FAN_2_SL {get;set;}

        /// <summary>
        /// Desc:主抽1风机转速（频率）设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_FAN_1_SP_FRE {get;set;}

        /// <summary>
        /// Desc:主抽2风机转速（频率）设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_FAN_2_SP_FRE {get;set;}

        /// <summary>
        /// Desc:主抽1风机转速（频率）反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_FAN_1_PV_FRE {get;set;}

        /// <summary>
        /// Desc:主抽2风机转速（频率）反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_FAN_2_PV_FRE {get;set;}

        /// <summary>
        /// Desc:主抽1阀门开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_VE_1_SP_OD {get;set;}

        /// <summary>
        /// Desc:主抽2阀门开度设定值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_VE_2_SP_OD {get;set;}

        /// <summary>
        /// Desc:主抽1阀门开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_VE_1_PV_OD {get;set;}

        /// <summary>
        /// Desc:主抽2阀门开度反馈值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_VE_2_PV_OD {get;set;}

        /// <summary>
        /// Desc:主抽1烧结大烟道压力(大烟道压力左)取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_SB_1_FLUE_PT {get;set;}

        /// <summary>
        /// Desc:主抽2烧结大烟道压力(大烟道压力右)取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_SB_2_FLUE_PT {get;set;}

        /// <summary>
        /// Desc:主抽1烧结大烟道温度取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_SB_1_FLUE_TE {get;set;}

        /// <summary>
        /// Desc:主抽2烧结大烟道温度取平均 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_SB_2_FLUE_TE {get;set;}

        /// <summary>
        /// Desc:主抽1烧结大烟道烟气流量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_SB_1_FLUE_FT {get;set;}

        /// <summary>
        /// Desc:主抽2烧结大烟道烟气流量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_SB_2_FLUE_FT {get;set;}

        /// <summary>
        /// Desc:1#主抽风机出口压力显示值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_OUT_1_FLUE_PT {get;set;}

        /// <summary>
        /// Desc:1#主抽风机入口压力显示值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_IN_1_FLUE_PT {get;set;}

        /// <summary>
        /// Desc:1#主抽风机出口氧含量显示值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_OUT_1_FLUE_O2 {get;set;}

        /// <summary>
        /// Desc:2#主抽风机出口压力显示值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_OUT_2_FLUE_PI {get;set;}

        /// <summary>
        /// Desc:2#主抽风机入口压力显示值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_IN_2_FLUE_PI {get;set;}

        /// <summary>
        /// Desc:2#主抽风机出口氧含量显示值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_OUT_2_FLUE_O2 {get;set;}

        /// <summary>
        /// Desc:1#主抽风机出口粉尘浓度显示值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_OUT_1_FLUE_DT {get;set;}

        /// <summary>
        /// Desc:2#主抽风机出口粉尘浓度显示值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_OUT_2_FLUE_DT {get;set;}

        /// <summary>
        /// Desc:1#主抽风机出口温度显示值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_OUT_1_FLUE_TI {get;set;}

        /// <summary>
        /// Desc:2#主抽风机出口温度显示值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_OUT_2_FLUE_TI {get;set;}

        /// <summary>
        /// Desc:主抽电机电流1 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_1_EC {get;set;}

        /// <summary>
        /// Desc:主抽电机电流2 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_MA_2_EC {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE1 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE2 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE3 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE4 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE5 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE6 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE7 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE8 {get;set;}

        /// <summary>
        /// Desc:1号脱硫增压风机启停信号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public bool FGD1_FAN_SL {get;set;}

        /// <summary>
        /// Desc:2号脱硫增压风机启停信号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public bool FGD2_FAN_SL {get;set;}

        /// <summary>
        /// Desc:1号脱硫增压风机电流 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD1_FAN_EC {get;set;}

        /// <summary>
        /// Desc:2号脱硫增压风机电流 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD2_FAN_EC {get;set;}

        /// <summary>
        /// Desc:1号脱硫塔负压P1 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD1_P1 {get;set;}

        /// <summary>
        /// Desc:1号脱硫塔负压P2 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD1_P2 {get;set;}

        /// <summary>
        /// Desc:2号脱硫塔负压P1 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD2_P1 {get;set;}

        /// <summary>
        /// Desc:2号脱硫塔负压P2 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD2_P2 {get;set;}

        /// <summary>
        /// Desc:1号脱硫增压风机风门功率 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD1_FLUE_W {get;set;}

        /// <summary>
        /// Desc:2号脱硫增压风机风门功率 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD2_FLUE_W {get;set;}

        /// <summary>
        /// Desc:1号主抽烟道风量q 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double MA_1_AIR {get;set;}

        /// <summary>
        /// Desc:2号主抽烟道风量q 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double MA_2_AIR {get;set;}

        /// <summary>
        /// Desc:1号主抽电机功率W 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double MA_1_W {get;set;}

        /// <summary>
        /// Desc:2号主抽电机功率W 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double MA_2_W {get;set;}

        /// <summary>
        /// Desc:1号脱硫塔差压1 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD1_DP1 {get;set;}

        /// <summary>
        /// Desc:1号脱硫塔压差2 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD1_DP2 {get;set;}

        /// <summary>
        /// Desc:1号脱硫塔顶温度1 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD1_TI1 {get;set;}

        /// <summary>
        /// Desc:1号脱硫塔顶温度2 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD1_TI2 {get;set;}

        /// <summary>
        /// Desc:1号脱硫塔入口温度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD1_IN_TI {get;set;}

        /// <summary>
        /// Desc:1号脱硫塔出口温度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD1_OUT_TI {get;set;}

        /// <summary>
        /// Desc:1号入口粉尘 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD1_IN_DT {get;set;}

        /// <summary>
        /// Desc:1号出口粉尘 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD1_OUT_DT {get;set;}

        /// <summary>
        /// Desc:1号除尘器差压 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD1_DT_DP {get;set;}

        /// <summary>
        /// Desc:1号出口SO2 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD1_IN_SO2 {get;set;}

        /// <summary>
        /// Desc:1号入口SO2 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD1_OUT_SO2 {get;set;}

        /// <summary>
        /// Desc:1号入口氮氧化物 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD1_IN_NO {get;set;}

        /// <summary>
        /// Desc:1号出口氮氧化物 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD1_OUT_NO {get;set;}

        /// <summary>
        /// Desc:2号脱硫塔差压1 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD2_DP1 {get;set;}

        /// <summary>
        /// Desc:2号脱硫塔差压2 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD2_DP2 {get;set;}

        /// <summary>
        /// Desc:2号脱硫塔顶温度1 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD2_TI1 {get;set;}

        /// <summary>
        /// Desc:2号脱硫塔顶温度2 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD2_TI2 {get;set;}

        /// <summary>
        /// Desc:2号脱硫塔入口温度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD2_IN_TI {get;set;}

        /// <summary>
        /// Desc:2号脱硫塔出口温度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD2_OUT_TI {get;set;}

        /// <summary>
        /// Desc:2号入口粉尘 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD2_IN_DT {get;set;}

        /// <summary>
        /// Desc:2号出口粉尘 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD2_OUT_DT {get;set;}

        /// <summary>
        /// Desc:2号除尘器差压 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD2_DT_DP {get;set;}

        /// <summary>
        /// Desc:2号出口SO2 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD2_IN_SO2 {get;set;}

        /// <summary>
        /// Desc:2号入口SO2 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD2_OUT_SO2 {get;set;}

        /// <summary>
        /// Desc:2号入口氮氧化物 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD2_IN_NO {get;set;}

        /// <summary>
        /// Desc:2号出口氮氧化物 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD2_OUT_NO {get;set;}

        /// <summary>
        /// Desc:1号脱硫增压风机频率（或开度) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD1_FAN_FQ {get;set;}

        /// <summary>
        /// Desc:2号脱硫增压风机频率（或开度) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD2_FAN_FQ {get;set;}

        /// <summary>
        /// Desc:1号出口O2量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD1_OUT_O2 {get;set;}

        /// <summary>
        /// Desc:2号出口O2量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double FGD2_OUT_O2 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE9 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE10 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE11 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE12 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE13 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE14 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE15 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE16 {get;set;}

        /// <summary>
        /// Desc:循环烟气量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_LOOP_SK {get;set;}

        /// <summary>
        /// Desc:循环烟气温度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_LOOP_SK_TI {get;set;}

        /// <summary>
        /// Desc:循环烟气增压风机压力 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PS_LOOP_SK_PI {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE17 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE18 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE19 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE20 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE21 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE22 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE23 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE24 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE25 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double SPARE26 {get;set;}

    }
}
