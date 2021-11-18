using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class AV05M
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:风量(混风后热风流量) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BV5M {get;set;}

        /// <summary>
        /// Desc:风量(混风前冷风流量) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BV5M_COOL {get;set;}

        /// <summary>
        /// Desc:风压(混风后热风压力) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BP5M {get;set;}

        /// <summary>
        /// Desc:风压(混风前冷风压力) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BP5M_COOL {get;set;}

        /// <summary>
        /// Desc:平均热风温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BT5M {get;set;}

        /// <summary>
        /// Desc:平均冷风温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BT5M_COOL {get;set;}

        /// <summary>
        /// Desc:平均顶压 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TP5M {get;set;}

        /// <summary>
        /// Desc:富氧量(m3/h) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BVO {get;set;}

        /// <summary>
        /// Desc:小时喷煤量(t/h) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_WINJ {get;set;}

        /// <summary>
        /// Desc:质量鼓风湿度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_MOI {get;set;}

        /// <summary>
        /// Desc:炉顶煤气CO含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_CO {get;set;}

        /// <summary>
        /// Desc:炉顶煤气CO2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_CO2 {get;set;}

        /// <summary>
        /// Desc:炉顶煤气H2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_H2 {get;set;}

        /// <summary>
        /// Desc:标高21.6米270度方向静压力PE03018 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SPL11 {get;set;}

        /// <summary>
        /// Desc:标高21.6米0度方向静压力PE03019 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SPL12 {get;set;}

        /// <summary>
        /// Desc:标高21.6米90度方向静压力PE03020 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SPL13 {get;set;}

        /// <summary>
        /// Desc:标高21.6米180度方向静压力PE03021 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SPL14 {get;set;}

        /// <summary>
        /// Desc:标高25.52米270度方向静压力PE03022 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SPL21 {get;set;}

        /// <summary>
        /// Desc:标高25.52米0度方向静压力PE03023 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SPL22 {get;set;}

        /// <summary>
        /// Desc:标高25.52米90度方向静压力PE03024 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SPL23 {get;set;}

        /// <summary>
        /// Desc:标高25.52米180度方向静压力PE03025 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SPL24 {get;set;}

        /// <summary>
        /// Desc:十字测温中心 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE1301 {get;set;}

        /// <summary>
        /// Desc:十字测温东北次中心 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE1302 {get;set;}

        /// <summary>
        /// Desc:十字测温东北 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE1303 {get;set;}

        /// <summary>
        /// Desc:十字测温东北 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE1304 {get;set;}

        /// <summary>
        /// Desc:十字测温东北边缘 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE1305 {get;set;}

        /// <summary>
        /// Desc:十字测温东南次中心 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE1306 {get;set;}

        /// <summary>
        /// Desc:十字测温东南 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE1307 {get;set;}

        /// <summary>
        /// Desc:十字测温东南 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE1308 {get;set;}

        /// <summary>
        /// Desc:十字测温东南边缘 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE1309 {get;set;}

        /// <summary>
        /// Desc:十字测温西南次中心 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE1310 {get;set;}

        /// <summary>
        /// Desc:十字测温西南 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE1311 {get;set;}

        /// <summary>
        /// Desc:十字测温西南 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE1312 {get;set;}

        /// <summary>
        /// Desc:十字测温西南边缘 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE1313 {get;set;}

        /// <summary>
        /// Desc:十字测温西北次中心 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE1314 {get;set;}

        /// <summary>
        /// Desc:十字测温西北 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE1315 {get;set;}

        /// <summary>
        /// Desc:十字测温西北 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE1316 {get;set;}

        /// <summary>
        /// Desc:十字测温西北边缘 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE1317 {get;set;}

        /// <summary>
        /// Desc:平均顶温 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOPAVG {get;set;}

        /// <summary>
        /// Desc:顶温TE0601 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP1 {get;set;}

        /// <summary>
        /// Desc:顶温TE0602 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP2 {get;set;}

        /// <summary>
        /// Desc:顶温TE0603 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP3 {get;set;}

        /// <summary>
        /// Desc:顶温TE0604 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP4 {get;set;}

        /// <summary>
        /// Desc:软水补水泵出水管流量m3/h 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ADDWATER_FT {get;set;}

        /// <summary>
        /// Desc:煤气利用率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ETACO {get;set;}

        /// <summary>
        /// Desc:Z值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_Z {get;set;}

        /// <summary>
        /// Desc:W值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_W {get;set;}

        /// <summary>
        /// Desc:Z/W值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_Z_W {get;set;}

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
        /// Desc:理燃温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TF {get;set;}

        /// <summary>
        /// Desc:鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_E {get;set;}

        /// <summary>
        /// Desc:软水池流量累计 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double RSZGLJ {get;set;}

        /// <summary>
        /// Desc:1点顶温5min最高 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP1_MAX {get;set;}

        /// <summary>
        /// Desc:1点顶温5min最低 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP1_MIN {get;set;}

        /// <summary>
        /// Desc:1点顶温5min最高 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP2_MAX {get;set;}

        /// <summary>
        /// Desc:1点顶温5min最低 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP2_MIN {get;set;}

        /// <summary>
        /// Desc:1点顶温5min最高 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP3_MAX {get;set;}

        /// <summary>
        /// Desc:1点顶温5min最低 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP3_MIN {get;set;}

        /// <summary>
        /// Desc:1点顶温5min最高 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP4_MAX {get;set;}

        /// <summary>
        /// Desc:1点顶温5min最低 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP4_MIN {get;set;}

        /// <summary>
        /// Desc:喷吹载气N2量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_N2INJ {get;set;}

        /// <summary>
        /// Desc:气密箱氮气总管流量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_N2GEAR {get;set;}

        /// <summary>
        /// Desc:喷吹载气空气量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BVINJ {get;set;}

        /// <summary>
        /// Desc:炉顶煤气中的N2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_N2 {get;set;}

        /// <summary>
        /// Desc:次中心极差值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_LESSCENTRE_DIFF {get;set;}

        /// <summary>
        /// Desc:边缘温度均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SIDE_AVG {get;set;}

        /// <summary>
        /// Desc:边缘温度极差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SIDE_DIFF {get;set;}

        /// <summary>
        /// Desc:软水箱流量累计 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double RSZGLJ2 {get;set;}

        /// <summary>
        /// Desc:机前富氧 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BVO_WMH {get;set;}

        /// <summary>
        /// Desc:工业水给水压力1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_IWP1_IN {get;set;}

        /// <summary>
        /// Desc:工业水给水压力2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_IWP2_IN {get;set;}

        /// <summary>
        /// Desc:软水给水压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SWP1_IN {get;set;}

        /// <summary>
        /// Desc:软水补水压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SWP2_IN {get;set;}

        /// <summary>
        /// Desc:工业水给水压力均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_IWP_IN_AVG {get;set;}

        /// <summary>
        /// Desc:高压富氧 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BVO_HP {get;set;}

    }
}
