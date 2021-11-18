using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class STUFF_MATRIXDATA_CHANGE_M
    {

        /// <summary>
        /// Desc:开始使用时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIME_BEGIN { get; set; }

        /// <summary>
        /// Desc:结束使用时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? TIME_END { get; set; }

        /// <summary>
        /// Desc:矩阵使用时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TIMESPAN { get; set; }

        /// <summary>
        /// Desc:开始使用批次 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CHARGE_BEGIN { get; set; }

        /// <summary>
        /// Desc:结束使用批次 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? CHARGE_END { get; set; }

        /// <summary>
        /// Desc:产铁量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? THERY_FE { get; set; }

        /// <summary>
        /// Desc:折合日产铁量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? THERY_FE_DAY { get; set; }

        /// <summary>
        /// Desc:平均矿石批重 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? WEIGHT_ORE { get; set; }

        /// <summary>
        /// Desc:平均焦炭批重 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? WEIGHT_COKE { get; set; }

        /// <summary>
        /// Desc:装料制度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CHARGE_MATRIX { get; set; }
        /// <summary>
        /// Desc:装料制度2
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CHARGE_MATRIX_C_O { get; set; }

        /// <summary>
        /// Desc:中心焦比例 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? CENTRAL_COKE_RATIO { get; set; }

        /// <summary>
        /// Desc:矿焦角差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double ANGLEDIFF_ORE_COKE { get; set; }

        /// <summary>
        /// Desc:矿石角差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double ANGLEDIFF_ORE { get; set; }

        /// <summary>
        /// Desc:矿石最大角 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double MAXANGLE_ORE { get; set; }

        /// <summary>
        /// Desc:焦炭最大角 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double MAXANGLE_COKE { get; set; }

        /// <summary>
        /// Desc:焦炭角差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double ANGLEDIFF_COKE { get; set; }

        /// <summary>
        /// Desc:矿石最小角 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double MINANGLE_ORE { get; set; }

        /// <summary>
        /// Desc:焦炭最小角 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double MINANGLE_COKE { get; set; }

        /// <summary>
        /// Desc:中心负荷 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? CENTER_FH { get; set; }

        /// <summary>
        /// Desc:平台负荷 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MID_FH { get; set; }

        /// <summary>
        /// Desc:边缘负荷 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MARGIN_FH { get; set; }

        /// <summary>
        /// Desc:理论燃料比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? THERY_RLB { get; set; }

        /// <summary>
        /// Desc:理论焦比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? THERY_GJB { get; set; }

        /// <summary>
        /// Desc:理论煤比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? THERY_MB { get; set; }

        /// <summary>
        /// Desc:风量(混风后热风流量) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_BV5M {get;set;}

        /// <summary>
        /// Desc:风压(混风后热风压力) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_BP5M {get;set;}

        /// <summary>
        /// Desc:平均热风温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_BT5M {get;set;}

        /// <summary>
        /// Desc:平均顶压 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_TP5M {get;set;}

        /// <summary>
        /// Desc:富氧量(m3/h) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_BVO {get;set;}

        /// <summary>
        /// Desc:小时喷煤量(t/h) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_WINJ {get;set;}

        /// <summary>
        /// Desc:质量鼓风湿度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_MOI {get;set;}

        /// <summary>
        /// Desc:炉顶煤气CO含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_CO {get;set;}

        /// <summary>
        /// Desc:炉顶煤气CO2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_CO2 {get;set;}

        /// <summary>
        /// Desc:炉顶煤气H2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_H2 {get;set;}

        /// <summary>
        /// Desc:标高21.6米270度方向静压力PE03018 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_SPL11 {get;set;}

        /// <summary>
        /// Desc:标高21.6米0度方向静压力PE03019 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_SPL12 {get;set;}

        /// <summary>
        /// Desc:标高21.6米90度方向静压力PE03020 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_SPL13 {get;set;}

        /// <summary>
        /// Desc:标高21.6米180度方向静压力PE03021 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_SPL14 {get;set;}

        /// <summary>
        /// Desc:标高25.52米270度方向静压力PE03022 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_SPL21 {get;set;}

        /// <summary>
        /// Desc:标高25.52米0度方向静压力PE03023 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_SPL22 {get;set;}

        /// <summary>
        /// Desc:标高25.52米90度方向静压力PE03024 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_SPL23 {get;set;}

        /// <summary>
        /// Desc:标高25.52米180度方向静压力PE03025 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_SPL24 {get;set;}

        /// <summary>
        /// Desc:十字测温中心 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TE1301 {get;set;}

        /// <summary>
        /// Desc:十字测温东北次中心 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TE1302 {get;set;}

        /// <summary>
        /// Desc:十字测温东北 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TE1303 {get;set;}

        /// <summary>
        /// Desc:十字测温东北 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TE1304 {get;set;}

        /// <summary>
        /// Desc:十字测温东北边缘 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TE1305 {get;set;}

        /// <summary>
        /// Desc:十字测温东南次中心 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TE1306 {get;set;}

        /// <summary>
        /// Desc:十字测温东南 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TE1307 {get;set;}

        /// <summary>
        /// Desc:十字测温东南 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TE1308 {get;set;}

        /// <summary>
        /// Desc:十字测温东南边缘 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TE1309 {get;set;}

        /// <summary>
        /// Desc:十字测温西南次中心 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TE1310 {get;set;}

        /// <summary>
        /// Desc:十字测温西南 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TE1311 {get;set;}

        /// <summary>
        /// Desc:十字测温西南 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TE1312 {get;set;}

        /// <summary>
        /// Desc:十字测温西南边缘 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TE1313 {get;set;}

        /// <summary>
        /// Desc:十字测温西北次中心 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TE1314 {get;set;}

        /// <summary>
        /// Desc:十字测温西北 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TE1315 {get;set;}

        /// <summary>
        /// Desc:十字测温西北 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TE1316 {get;set;}

        /// <summary>
        /// Desc:十字测温西北边缘 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TE1317 {get;set;}

        /// <summary>
        /// Desc:平均顶温 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_TTOPAVG {get;set;}

        /// <summary>
        /// Desc:顶温TE0601 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_TTOP1 {get;set;}

        /// <summary>
        /// Desc:顶温TE0602 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_TTOP2 {get;set;}

        /// <summary>
        /// Desc:顶温TE0603 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_TTOP3 {get;set;}

        /// <summary>
        /// Desc:顶温TE0604 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_TTOP4 {get;set;}

        /// <summary>
        /// Desc:煤气利用率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_ETACO {get;set;}

        /// <summary>
        /// Desc:Z值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_Z {get;set;}

        /// <summary>
        /// Desc:W值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_W {get;set;}

        /// <summary>
        /// Desc:Z/W值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_Z_W {get;set;}

        /// <summary>
        /// Desc:炉腹煤气量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_BVSG {get;set;}

        /// <summary>
        /// Desc:炉腹煤气指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_XBVSG {get;set;}

        /// <summary>
        /// Desc:理燃温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_TF {get;set;}

        /// <summary>
        /// Desc:鼓风动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_E {get;set;}

        /// <summary>
        /// Desc:炉顶煤气中的N2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_N2 {get;set;}

        /// <summary>
        /// Desc:次中心极差值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_LESSCENTRE_DIFF {get;set;}

        /// <summary>
        /// Desc:边缘温度均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_SIDE_AVG {get;set;}

        /// <summary>
        /// Desc:边缘温度极差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_SIDE_DIFF {get;set;}

        /// <summary>
        /// Desc:炉缸热负荷 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HEARTH_HL {get;set;}

        /// <summary>
        /// Desc:炉腹热负荷 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BOSH_HL {get;set;}

        /// <summary>
        /// Desc:炉腰热负荷 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? WAIST_HL {get;set;}

        /// <summary>
        /// Desc:炉身下部热负荷 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BOTTOMBODY_HL {get;set;}

        /// <summary>
        /// Desc:炉身中部热负荷 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MIDBODY_HL {get;set;}

        /// <summary>
        /// Desc:炉身上部热负荷 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TOPBODY_HL {get;set;}

        /// <summary>
        /// Desc:风口小套热负荷 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TUYERE_HL {get;set;}

        /// <summary>
        /// Desc:炉缸水温差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HEARTH_DIFFTE {get;set;}

        /// <summary>
        /// Desc:炉腹水温差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BOSH_DIFFTE {get;set;}

        /// <summary>
        /// Desc:炉腰水温差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? WAIST_DIFFTE {get;set;}

        /// <summary>
        /// Desc:炉身下部水温差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BOTTOMBODY_DIFFTE {get;set;}

        /// <summary>
        /// Desc:炉身中部温差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MIDBODY_DIFFTE {get;set;}

        /// <summary>
        /// Desc:炉身上部温差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TOPBODY_DIFFTE {get;set;}

        /// <summary>
        /// Desc:风口小套水温差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TUYERE_DIFFTE {get;set;}

        /// <summary>
        /// Desc:总热负荷 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TOTAL_HL {get;set;}

        /// <summary>
        /// Desc:总流量差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TOTAL_DIFFTE {get;set;}

        /// <summary>
        /// Desc:总流量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TOTAL_FT {get;set;}

        /// <summary>
        /// Desc:进水温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TE_IN {get;set;}

        /// <summary>
        /// Desc:出水温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TE_OUT {get;set;}

        /// <summary>
        /// Desc:前12小时热负荷均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TOTAL_HL_AVG {get;set;}

        /// <summary>
        /// Desc:前12小时水温差均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TOTAL_DIFFTE_AVG {get;set;}

        /// <summary>
        /// Desc:富氧率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_BVO_RATE {get;set;}

    }
}
