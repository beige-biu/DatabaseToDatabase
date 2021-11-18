using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class BATCHHIS_DUMP
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:料批号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CHARGE {get;set;}

        /// <summary>
        /// Desc:料种 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int TYPE {get;set;}

        /// <summary>
        /// Desc:焦丁仓重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT1 {get;set;}

        /// <summary>
        /// Desc:烧结仓1重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT2 {get;set;}

        /// <summary>
        /// Desc:烧结仓2重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT3 {get;set;}

        /// <summary>
        /// Desc:烧结仓3重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT4 {get;set;}

        /// <summary>
        /// Desc:烧结仓4重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT5 {get;set;}

        /// <summary>
        /// Desc:烧结仓5重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT6 {get;set;}

        /// <summary>
        /// Desc:球团仓1重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT7 {get;set;}

        /// <summary>
        /// Desc:球团仓2重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT8 {get;set;}

        /// <summary>
        /// Desc:块矿仓1重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT9 {get;set;}

        /// <summary>
        /// Desc:块矿仓2重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT10 {get;set;}

        /// <summary>
        /// Desc:杂矿仓1重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT11 {get;set;}

        /// <summary>
        /// Desc:杂矿仓2重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT12 {get;set;}

        /// <summary>
        /// Desc:焦仓1重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT13 {get;set;}

        /// <summary>
        /// Desc:焦仓2重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT14 {get;set;}

        /// <summary>
        /// Desc:焦仓3重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT15 {get;set;}

        /// <summary>
        /// Desc:焦仓4重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT16 {get;set;}

        /// <summary>
        /// Desc:焦仓5重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT17 {get;set;}

        /// <summary>
        /// Desc:焦丁仓物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CODE1 {get;set;}

        /// <summary>
        /// Desc:烧结仓1物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CODE2 {get;set;}

        /// <summary>
        /// Desc:烧结仓2物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CODE3 {get;set;}

        /// <summary>
        /// Desc:烧结仓3物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CODE4 {get;set;}

        /// <summary>
        /// Desc:烧结仓4物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CODE5 {get;set;}

        /// <summary>
        /// Desc:烧结仓5物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CODE6 {get;set;}

        /// <summary>
        /// Desc:球团仓1物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CODE7 {get;set;}

        /// <summary>
        /// Desc:球团仓2物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CODE8 {get;set;}

        /// <summary>
        /// Desc:块矿仓1物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CODE9 {get;set;}

        /// <summary>
        /// Desc:块矿仓2物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CODE10 {get;set;}

        /// <summary>
        /// Desc:杂矿仓1物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CODE11 {get;set;}

        /// <summary>
        /// Desc:杂矿仓2物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CODE12 {get;set;}

        /// <summary>
        /// Desc:焦仓1物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CODE13 {get;set;}

        /// <summary>
        /// Desc:焦仓2物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CODE14 {get;set;}

        /// <summary>
        /// Desc:焦仓3物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CODE15 {get;set;}

        /// <summary>
        /// Desc:焦仓4物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CODE16 {get;set;}

        /// <summary>
        /// Desc:焦仓5物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CODE17 {get;set;}

        /// <summary>
        /// Desc:烧1仓设定水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WATER1 {get;set;}

        /// <summary>
        /// Desc:烧2仓设定水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WATER2 {get;set;}

        /// <summary>
        /// Desc:烧3仓设定水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WATER3 {get;set;}

        /// <summary>
        /// Desc:烧4仓设定水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WATER4 {get;set;}

        /// <summary>
        /// Desc:烧5仓设定水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WATER5 {get;set;}

        /// <summary>
        /// Desc:烧6仓设定水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WATER6 {get;set;}

        /// <summary>
        /// Desc:烧7仓设定水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WATER7 {get;set;}

        /// <summary>
        /// Desc:烧8仓设定水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WATER8 {get;set;}

        /// <summary>
        /// Desc:烧9仓设定水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WATER9 {get;set;}

        /// <summary>
        /// Desc:烧10仓设定水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WATER10 {get;set;}

        /// <summary>
        /// Desc:烧11仓设定水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WATER11 {get;set;}

        /// <summary>
        /// Desc:烧12仓设定水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WATER12 {get;set;}

        /// <summary>
        /// Desc:焦1仓设定水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WATER13 {get;set;}

        /// <summary>
        /// Desc:焦2仓设定水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WATER14 {get;set;}

        /// <summary>
        /// Desc:焦3仓设定水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WATER15 {get;set;}

        /// <summary>
        /// Desc:焦4仓设定水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WATER16 {get;set;}

        /// <summary>
        /// Desc:焦5仓设定水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WATER17 {get;set;}

        /// <summary>
        /// Desc:烧1仓设定重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT1_SET {get;set;}

        /// <summary>
        /// Desc:烧2仓设定重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT2_SET {get;set;}

        /// <summary>
        /// Desc:烧3仓设定重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT3_SET {get;set;}

        /// <summary>
        /// Desc:烧4仓设定重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT4_SET {get;set;}

        /// <summary>
        /// Desc:烧5仓设定重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT5_SET {get;set;}

        /// <summary>
        /// Desc:烧6仓设定重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT6_SET {get;set;}

        /// <summary>
        /// Desc:烧7仓设定重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT7_SET {get;set;}

        /// <summary>
        /// Desc:烧8仓设定重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT8_SET {get;set;}

        /// <summary>
        /// Desc:烧9仓设定重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT9_SET {get;set;}

        /// <summary>
        /// Desc:烧10仓设定重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT10_SET {get;set;}

        /// <summary>
        /// Desc:烧11仓设定重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT11_SET {get;set;}

        /// <summary>
        /// Desc:烧12仓设定重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT12_SET {get;set;}

        /// <summary>
        /// Desc:焦1仓设定重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT13_SET {get;set;}

        /// <summary>
        /// Desc:焦2仓设定重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT14_SET {get;set;}

        /// <summary>
        /// Desc:焦3仓设定重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT15_SET {get;set;}

        /// <summary>
        /// Desc:焦4仓设定重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT16_SET {get;set;}

        /// <summary>
        /// Desc:焦5仓设定重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double WEIGHT17_SET {get;set;}

        /// <summary>
        /// Desc:不清零料批 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CHARGE_NOZERO {get;set;}

    }
}
