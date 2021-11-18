using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models.SG
{
    public class BF_DAYDATE
    {

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:日料批数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_CHARGES {get;set;}

        /// <summary>
        /// Desc:矿批 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_OREWEIGHT {get;set;}

        /// <summary>
        /// Desc:日焦比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_K1 {get;set;}

        /// <summary>
        /// Desc:日操作燃料比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_F1 {get;set;}

        /// <summary>
        /// Desc:煤气利用率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_ETACO {get;set;}

        /// <summary>
        /// Desc:日均顶温 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_TTOP {get;set;}

        /// <summary>
        /// Desc:崩料次数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? D_SLIPNUM {get;set;}

        /// <summary>
        /// Desc:W值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_W {get;set;}

        /// <summary>
        /// Desc:Z值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_Z {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_CROSS_0 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_CROSS_1 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_CROSS_2 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_CROSS_3 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_CROSS_4 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_CROSS_5 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_CROSS_6 {get;set;}

        /// <summary>
        /// Desc:炉芯温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_CENTER {get;set;}

        /// <summary>
        /// Desc:钢砖温度均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_LININGTC19 {get;set;}

        /// <summary>
        /// Desc:18段壁体均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_LININGTC18 {get;set;}

        /// <summary>
        /// Desc:全炉水温差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_WTDIFFTOTAL {get;set;}

        /// <summary>
        /// Desc:炉体热负荷 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_HLTOTAL {get;set;}

        /// <summary>
        /// Desc:铁水温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_HMTP {get;set;}

        /// <summary>
        /// Desc:风温 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_BT {get;set;}

        /// <summary>
        /// Desc:湿度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_MOI {get;set;}

        /// <summary>
        /// Desc:富氧量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_BVO {get;set;}

        /// <summary>
        /// Desc:炉腹煤气量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_BVSG {get;set;}

        /// <summary>
        /// Desc:压差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_TPDIFF {get;set;}

        /// <summary>
        /// Desc:透气性指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_PINDEX {get;set;}

        /// <summary>
        /// Desc:抗阻指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_K {get;set;}

        /// <summary>
        /// Desc:标准风速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_STD_V {get;set;}

        /// <summary>
        /// Desc:实际风速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_REAL_V {get;set;}

        /// <summary>
        /// Desc:动能 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_E {get;set;}

        /// <summary>
        /// Desc:氢含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_H2 {get;set;}

        /// <summary>
        /// Desc:入炉品位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_FE {get;set;}

        /// <summary>
        /// Desc:返矿量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_RETURNORE {get;set;}

        /// <summary>
        /// Desc:返焦量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_RETURNCOKE {get;set;}

        /// <summary>
        /// Desc:灰铁比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_DUST_PER {get;set;}

        /// <summary>
        /// Desc:锌负荷 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_ZN_LOAD {get;set;}

        /// <summary>
        /// Desc:碱金属负荷 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_ALKA_LOAD {get;set;}

        /// <summary>
        /// Desc:补水量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? D_ADDWATER {get;set;}

    }
}
