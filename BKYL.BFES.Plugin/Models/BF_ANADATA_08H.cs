using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models.AG
{
    public class BF_ANADATA_08H
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:风量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BV {get;set;}

        /// <summary>
        /// Desc:风压 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BP {get;set;}

        /// <summary>
        /// Desc:煤气利用率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ETACO {get;set;}

        /// <summary>
        /// Desc:十字测温中心点 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_CROSS0 {get;set;}

        /// <summary>
        /// Desc:次中心4点均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_CROSS1 {get;set;}

        /// <summary>
        /// Desc:第2位置4点均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_CROSS2 {get;set;}

        /// <summary>
        /// Desc:第3位置4点均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_CROSS3 {get;set;}

        /// <summary>
        /// Desc:第4位置4点均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_CROSS4 {get;set;}

        /// <summary>
        /// Desc:边缘4点均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_CROSS5 {get;set;}

        /// <summary>
        /// Desc:Z 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_Z {get;set;}

        /// <summary>
        /// Desc:W 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_W {get;set;}

        /// <summary>
        /// Desc:顶温1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP1 {get;set;}

        /// <summary>
        /// Desc:顶温2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP2 {get;set;}

        /// <summary>
        /// Desc:顶温3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP3 {get;set;}

        /// <summary>
        /// Desc:顶温4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP4 {get;set;}

        /// <summary>
        /// Desc:炉喉正东 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5330 {get;set;}

        /// <summary>
        /// Desc:炉喉正南 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5331 {get;set;}

        /// <summary>
        /// Desc:炉喉正西 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5332 {get;set;}

        /// <summary>
        /// Desc:炉喉正北 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5333 {get;set;}

        /// <summary>
        /// Desc:探尺差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SLIDDIFF {get;set;}

        /// <summary>
        /// Desc:下料速度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SPEED {get;set;}

        /// <summary>
        /// Desc:铁水温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_HTMP {get;set;}

        /// <summary>
        /// Desc:两铁口铁水温度差合格率(偏差波动在±20℃内占比) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_DHTMP_RATE {get;set;}

        /// <summary>
        /// Desc:SI均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SI {get;set;}

        /// <summary>
        /// Desc:两铁口炉温差合格率(偏差波动在±0.15内占比) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_DSI_RATE {get;set;}

        /// <summary>
        /// Desc:见渣率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SLAG_RATE {get;set;}

        /// <summary>
        /// Desc:风口回旋区宽度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_RACEWAY_HEIGHT {get;set;}

        /// <summary>
        /// Desc:回旋区工作面积与炉缸截面积比率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_RACEWAY_RATIO {get;set;}

        /// <summary>
        /// Desc:风口回旋区深度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_RACEWAY_LENGTH {get;set;}

        /// <summary>
        /// Desc:1#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E1 {get;set;}

        /// <summary>
        /// Desc:2#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E2 {get;set;}

        /// <summary>
        /// Desc:3#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E3 {get;set;}

        /// <summary>
        /// Desc:4#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E4 {get;set;}

        /// <summary>
        /// Desc:5#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E5 {get;set;}

        /// <summary>
        /// Desc:6#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E6 {get;set;}

        /// <summary>
        /// Desc:7#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E7 {get;set;}

        /// <summary>
        /// Desc:8#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E8 {get;set;}

        /// <summary>
        /// Desc:9#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E9 {get;set;}

        /// <summary>
        /// Desc:10#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E10 {get;set;}

        /// <summary>
        /// Desc:11#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E11 {get;set;}

        /// <summary>
        /// Desc:12#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E12 {get;set;}

        /// <summary>
        /// Desc:13#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E13 {get;set;}

        /// <summary>
        /// Desc:14#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E14 {get;set;}

        /// <summary>
        /// Desc:15#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E15 {get;set;}

        /// <summary>
        /// Desc:16#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E16 {get;set;}

        /// <summary>
        /// Desc:17#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E17 {get;set;}

        /// <summary>
        /// Desc:18#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E18 {get;set;}

        /// <summary>
        /// Desc:19#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E19 {get;set;}

        /// <summary>
        /// Desc:20#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E20 {get;set;}

        /// <summary>
        /// Desc:21#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E21 {get;set;}

        /// <summary>
        /// Desc:22#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E22 {get;set;}

        /// <summary>
        /// Desc:23#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E23 {get;set;}

        /// <summary>
        /// Desc:24#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E24 {get;set;}

        /// <summary>
        /// Desc:25#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E25 {get;set;}

        /// <summary>
        /// Desc:26#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E26 {get;set;}

        /// <summary>
        /// Desc:27#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E27 {get;set;}

        /// <summary>
        /// Desc:28#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E28 {get;set;}

        /// <summary>
        /// Desc:29#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E29 {get;set;}

        /// <summary>
        /// Desc:30#风口鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E30 {get;set;}

        /// <summary>
        /// Desc:炉腹煤气量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BVSG {get;set;}

        /// <summary>
        /// Desc:炉腹煤气指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_XBVSG {get;set;}

        /// <summary>
        /// Desc:煤气阻力指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_K {get;set;}

        /// <summary>
        /// Desc:鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_E {get;set;}

        /// <summary>
        /// Desc:理燃温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TF {get;set;}

        /// <summary>
        /// Desc:富氧量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BVO {get;set;}

        /// <summary>
        /// Desc:标准风速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_STD_V {get;set;}

        /// <summary>
        /// Desc:中心加焦比例 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_CENTERCOKE {get;set;}

        /// <summary>
        /// Desc:煤比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_M {get;set;}

        /// <summary>
        /// Desc:风量8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BV_STD {get;set;}

        /// <summary>
        /// Desc:风压8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BP_STD {get;set;}

        /// <summary>
        /// Desc:煤气利用率8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ETACO_STD {get;set;}

        /// <summary>
        /// Desc:十字测温中心点8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_CROSS0_STD {get;set;}

        /// <summary>
        /// Desc:次中心4点均值8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_CROSS1_STD {get;set;}

        /// <summary>
        /// Desc:第2位置4点均值8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_CROSS2_STD {get;set;}

        /// <summary>
        /// Desc:第3位置4点均值8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_CROSS3_STD {get;set;}

        /// <summary>
        /// Desc:第4位置4点均值8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_CROSS4_STD {get;set;}

        /// <summary>
        /// Desc:边缘4点均值8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_CROSS5_STD {get;set;}

        /// <summary>
        /// Desc:Z8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_Z_STD {get;set;}

        /// <summary>
        /// Desc:W8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_W_STD {get;set;}

        /// <summary>
        /// Desc:顶温18H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP1_STD {get;set;}

        /// <summary>
        /// Desc:顶温28H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP2_STD {get;set;}

        /// <summary>
        /// Desc:顶温38H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP3_STD {get;set;}

        /// <summary>
        /// Desc:顶温48H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP4_STD {get;set;}

        /// <summary>
        /// Desc:炉喉正东8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5330_STD {get;set;}

        /// <summary>
        /// Desc:炉喉正南8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5331_STD {get;set;}

        /// <summary>
        /// Desc:炉喉正西8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5332_STD {get;set;}

        /// <summary>
        /// Desc:炉喉正北8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5333_STD {get;set;}

        /// <summary>
        /// Desc:探尺差8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SLIDDIFF_STD {get;set;}

        /// <summary>
        /// Desc:下料速度8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SPEED_STD {get;set;}

        /// <summary>
        /// Desc:铁水温度8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_HTMP_STD {get;set;}

        /// <summary>
        /// Desc:两铁口铁水温度差合格率(偏差波动在±20℃内占比)8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_DHTMP_RATE_STD {get;set;}

        /// <summary>
        /// Desc:SI均值8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SI_STD {get;set;}

        /// <summary>
        /// Desc:两铁口炉温差合格率(偏差波动在±0.15内占比)8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_DSI_RATE_STD {get;set;}

        /// <summary>
        /// Desc:见渣率8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SLAG_RATE_STD {get;set;}

        /// <summary>
        /// Desc:风口回旋区宽度8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_RACEWAY_HEIGHT_STD {get;set;}

        /// <summary>
        /// Desc:回旋区工作面积与炉缸截面积比率8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_RACEWAY_RATIO_STD {get;set;}

        /// <summary>
        /// Desc:风口回旋区深度8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_RACEWAY_LENGTH_STD {get;set;}

        /// <summary>
        /// Desc:1#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E1_STD {get;set;}

        /// <summary>
        /// Desc:2#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E2_STD {get;set;}

        /// <summary>
        /// Desc:3#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E3_STD {get;set;}

        /// <summary>
        /// Desc:4#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E4_STD {get;set;}

        /// <summary>
        /// Desc:5#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E5_STD {get;set;}

        /// <summary>
        /// Desc:6#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E6_STD {get;set;}

        /// <summary>
        /// Desc:7#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E7_STD {get;set;}

        /// <summary>
        /// Desc:8#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E8_STD {get;set;}

        /// <summary>
        /// Desc:9#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E9_STD {get;set;}

        /// <summary>
        /// Desc:10#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E10_STD {get;set;}

        /// <summary>
        /// Desc:11#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E11_STD {get;set;}

        /// <summary>
        /// Desc:12#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E12_STD {get;set;}

        /// <summary>
        /// Desc:13#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E13_STD {get;set;}

        /// <summary>
        /// Desc:14#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E14_STD {get;set;}

        /// <summary>
        /// Desc:15#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E15_STD {get;set;}

        /// <summary>
        /// Desc:16#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E16_STD {get;set;}

        /// <summary>
        /// Desc:17#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E17_STD {get;set;}

        /// <summary>
        /// Desc:18#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E18_STD {get;set;}

        /// <summary>
        /// Desc:19#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E19_STD {get;set;}

        /// <summary>
        /// Desc:20#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E20_STD {get;set;}

        /// <summary>
        /// Desc:21#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E21_STD {get;set;}

        /// <summary>
        /// Desc:22#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E22_STD {get;set;}

        /// <summary>
        /// Desc:23#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E23_STD {get;set;}

        /// <summary>
        /// Desc:24#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E24_STD {get;set;}

        /// <summary>
        /// Desc:25#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E25_STD {get;set;}

        /// <summary>
        /// Desc:26#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E26_STD {get;set;}

        /// <summary>
        /// Desc:27#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E27_STD {get;set;}

        /// <summary>
        /// Desc:28#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E28_STD {get;set;}

        /// <summary>
        /// Desc:29#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E29_STD {get;set;}

        /// <summary>
        /// Desc:30#风口鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_E30_STD {get;set;}

        /// <summary>
        /// Desc:炉腹煤气量8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BVSG_STD {get;set;}

        /// <summary>
        /// Desc:炉腹煤气指数8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_XBVSG_STD {get;set;}

        /// <summary>
        /// Desc:煤气阻力指数8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_K_STD {get;set;}

        /// <summary>
        /// Desc:鼓风动能8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_E_STD {get;set;}

        /// <summary>
        /// Desc:理燃温度8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TF_STD {get;set;}

        /// <summary>
        /// Desc:富氧量8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BVO_STD {get;set;}

        /// <summary>
        /// Desc:标准风速8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_STD_V_STD {get;set;}

        /// <summary>
        /// Desc:中心加焦比例8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_CENTERCOKE_STD {get;set;}

        /// <summary>
        /// Desc:煤比8H标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_M_STD {get;set;}

        /// <summary>
        /// Desc:炉芯温度均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5057E {get;set;}

        /// <summary>
        /// Desc:次中心温度1(1铁口方位)均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5047D {get;set;}

        /// <summary>
        /// Desc:次中心温度2(2铁口方位)均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5046D {get;set;}

        /// <summary>
        /// Desc:次中心温度3(3铁口方位)均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5044D {get;set;}

        /// <summary>
        /// Desc:次中心温度4(3铁口对面)均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5054D {get;set;}

        /// <summary>
        /// Desc:侧壁2层温度1(1铁口方位)均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5156B {get;set;}

        /// <summary>
        /// Desc:侧壁2层温度2(2铁口方位)均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5159B {get;set;}

        /// <summary>
        /// Desc:侧壁2层温度3(3铁口方位)均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5148B {get;set;}

        /// <summary>
        /// Desc:侧壁2层温度4(3铁口对面)均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5140B {get;set;}

        /// <summary>
        /// Desc:侧壁3层温度1(1铁口方位)均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5176B {get;set;}

        /// <summary>
        /// Desc:侧壁3层温度2(2铁口方位)均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5177B {get;set;}

        /// <summary>
        /// Desc:侧壁3层温度3(3铁口方位)均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5173B {get;set;}

        /// <summary>
        /// Desc:侧壁3层温度4(3铁口对面)均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5169B {get;set;}

        /// <summary>
        /// Desc:侧壁7层温度1(1铁口方位)均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5217B {get;set;}

        /// <summary>
        /// Desc:侧壁7层温度2(2铁口方位)均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5214B {get;set;}

        /// <summary>
        /// Desc:侧壁7层温度3(3铁口方位)均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5212B {get;set;}

        /// <summary>
        /// Desc:侧壁7层温度4(3铁口对面)均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5216B {get;set;}

        /// <summary>
        /// Desc:炉芯温度标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5057E_STD {get;set;}

        /// <summary>
        /// Desc:次中心温度1(1铁口方位)标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5047D_STD {get;set;}

        /// <summary>
        /// Desc:次中心温度2(2铁口方位)标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5046D_STD {get;set;}

        /// <summary>
        /// Desc:次中心温度3(3铁口方位)标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5044D_STD {get;set;}

        /// <summary>
        /// Desc:次中心温度4(3铁口对面)标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5054D_STD {get;set;}

        /// <summary>
        /// Desc:侧壁2层温度1(1铁口方位)标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5156B_STD {get;set;}

        /// <summary>
        /// Desc:侧壁2层温度2(2铁口方位)标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5159B_STD {get;set;}

        /// <summary>
        /// Desc:侧壁2层温度3(3铁口方位)标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5148B_STD {get;set;}

        /// <summary>
        /// Desc:侧壁2层温度4(3铁口对面)标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5140B_STD {get;set;}

        /// <summary>
        /// Desc:侧壁3层温度1(1铁口方位)标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5176B_STD {get;set;}

        /// <summary>
        /// Desc:侧壁3层温度2(2铁口方位)标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5177B_STD {get;set;}

        /// <summary>
        /// Desc:侧壁3层温度3(3铁口方位)标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5173B_STD {get;set;}

        /// <summary>
        /// Desc:侧壁3层温度4(3铁口对面)标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5169B_STD {get;set;}

        /// <summary>
        /// Desc:侧壁7层温度1(1铁口方位)标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5217B_STD {get;set;}

        /// <summary>
        /// Desc:侧壁7层温度2(2铁口方位)标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5214B_STD {get;set;}

        /// <summary>
        /// Desc:侧壁7层温度3(3铁口方位)标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5212B_STD {get;set;}

        /// <summary>
        /// Desc:侧壁7层温度4(3铁口对面)标准差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_5216B_STD {get;set;}

    }
}
