using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MC_SINCAL_TRIG_1MIN
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:1号混匀矿仓配比料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_01_ML {get;set;}

        /// <summary>
        /// Desc:1号混匀矿仓配比分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_01_TC {get;set;}

        /// <summary>
        /// Desc:1号混匀矿仓水分设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_01_H2O_SP {get;set;}

        /// <summary>
        /// Desc:1号混匀矿仓水分当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_01_H2O_PV {get;set;}

        /// <summary>
        /// Desc:1号混匀矿仓下料设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_01_BK_SP {get;set;}

        /// <summary>
        /// Desc:1号混匀矿仓下料当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_01_BK_PV {get;set;}

        /// <summary>
        /// Desc:1号混匀矿仓下料偏差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_01_BK_DV {get;set;}

        /// <summary>
        /// Desc:1号混匀矿仓设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_01_FRE {get;set;}

        /// <summary>
        /// Desc:2号混匀矿仓配比料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_02_ML {get;set;}

        /// <summary>
        /// Desc:2号混匀矿仓配比分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_02_TC {get;set;}

        /// <summary>
        /// Desc:2号混匀矿仓水分设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_02_H2O_SP {get;set;}

        /// <summary>
        /// Desc:2号混匀矿仓水分当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_02_H2O_PV {get;set;}

        /// <summary>
        /// Desc:2号混匀矿仓下料设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_02_BK_SP {get;set;}

        /// <summary>
        /// Desc:2号混匀矿仓下料当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_02_BK_PV {get;set;}

        /// <summary>
        /// Desc:2号混匀矿仓下料偏差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_02_BK_DV {get;set;}

        /// <summary>
        /// Desc:2号混匀矿仓设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_02_FRE {get;set;}

        /// <summary>
        /// Desc:3号混匀矿仓配比料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_03_ML {get;set;}

        /// <summary>
        /// Desc:3号混匀矿仓配比分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_03_TC {get;set;}

        /// <summary>
        /// Desc:3号混匀矿仓水分设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_03_H2O_SP {get;set;}

        /// <summary>
        /// Desc:3号混匀矿仓水分当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_03_H2O_PV {get;set;}

        /// <summary>
        /// Desc:3号混匀矿仓下料设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_03_BK_SP {get;set;}

        /// <summary>
        /// Desc:3号混匀矿仓下料当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_03_BK_PV {get;set;}

        /// <summary>
        /// Desc:3号混匀矿仓下料偏差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_03_BK_DV {get;set;}

        /// <summary>
        /// Desc:3号混匀矿仓设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_03_FRE {get;set;}

        /// <summary>
        /// Desc:混匀矿配比设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_SP {get;set;}

        /// <summary>
        /// Desc:混匀矿配比当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_PV {get;set;}

        /// <summary>
        /// Desc:混匀矿配比百分比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_DRY_BILL {get;set;}

        /// <summary>
        /// Desc:混匀矿下料百分比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_WET_BILL {get;set;}

        /// <summary>
        /// Desc:混匀矿累计（t） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_ATIVE {get;set;}

        /// <summary>
        /// Desc:混匀矿配矿配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_RATIO {get;set;}

        /// <summary>
        /// Desc:4号口铁料配比仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_04_ML {get;set;}

        /// <summary>
        /// Desc:4号口配矿配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_04_ORE_RATIO {get;set;}

        /// <summary>
        /// Desc:4号口铁料配比设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_04_SP {get;set;}

        /// <summary>
        /// Desc:4号口铁料配比当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_04_PV {get;set;}

        /// <summary>
        /// Desc:4号口铁料配比百分比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_04_PER {get;set;}

        /// <summary>
        /// Desc:4号口铁料配比分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_04_TC {get;set;}

        /// <summary>
        /// Desc:4号口铁料水分设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_04_H2O_SP {get;set;}

        /// <summary>
        /// Desc:4号口铁料水分当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_04_H2O_PV {get;set;}

        /// <summary>
        /// Desc:4号口铁料下料设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_04_SP {get;set;}

        /// <summary>
        /// Desc:4号口铁料下料当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_04_PV {get;set;}

        /// <summary>
        /// Desc:4号口铁料下料偏差值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_04_DV {get;set;}

        /// <summary>
        /// Desc:4号口铁料下料百分比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_04_PER {get;set;}

        /// <summary>
        /// Desc:4号口铁料设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_04_FRE {get;set;}

        /// <summary>
        /// Desc:4号口铁料累计（t） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_04_ATIVE {get;set;}

        /// <summary>
        /// Desc:5号口铁料配比仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IBILL_05_ML {get;set;}

        /// <summary>
        /// Desc:5号口配矿配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_05_ORE_RATIO {get;set;}

        /// <summary>
        /// Desc:5号口铁料配比设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_05_SP {get;set;}

        /// <summary>
        /// Desc:5号口铁料配比当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_05_PV {get;set;}

        /// <summary>
        /// Desc:5号口铁料配比百分比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_05_PER {get;set;}

        /// <summary>
        /// Desc:5号口铁料配比分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_05_TC {get;set;}

        /// <summary>
        /// Desc:5号口铁料水分设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_05_H2O_SP {get;set;}

        /// <summary>
        /// Desc:5号口铁料水分当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_05_H2O_PV {get;set;}

        /// <summary>
        /// Desc:5号口铁料下料设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_05_SP {get;set;}

        /// <summary>
        /// Desc:5号口铁料下料当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_05_PV {get;set;}

        /// <summary>
        /// Desc:5号口铁料下料偏差值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_05_DV {get;set;}

        /// <summary>
        /// Desc:5号口铁料下料百分比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_05_PER {get;set;}

        /// <summary>
        /// Desc:5号口铁料设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_05_FRE {get;set;}

        /// <summary>
        /// Desc:5号口铁料累计（t） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_05_ATIVE {get;set;}

        /// <summary>
        /// Desc:6号口铁料配比仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_06_ML {get;set;}

        /// <summary>
        /// Desc:6号口配矿配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_06_ORE_RATIO {get;set;}

        /// <summary>
        /// Desc:6号口铁料配比设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_06_SP {get;set;}

        /// <summary>
        /// Desc:6号口铁料配比当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_06_PV {get;set;}

        /// <summary>
        /// Desc:6号口铁料配比百分比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_06_PER {get;set;}

        /// <summary>
        /// Desc:6号口铁料配比分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_06_TC {get;set;}

        /// <summary>
        /// Desc:6号口铁料水分设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_06_H2O_SP {get;set;}

        /// <summary>
        /// Desc:6号口铁料水分当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_06_H2O_PV {get;set;}

        /// <summary>
        /// Desc:6号口铁料下料设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_06_SP {get;set;}

        /// <summary>
        /// Desc:6号口铁料下料当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_06_PV {get;set;}

        /// <summary>
        /// Desc:6号口铁料下料偏差值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_06_DV {get;set;}

        /// <summary>
        /// Desc:6号口铁料下料百分比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_06_PER {get;set;}

        /// <summary>
        /// Desc:6号口铁料设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_06_FRE {get;set;}

        /// <summary>
        /// Desc:6号口铁料累计（t） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_06_ATIVE {get;set;}

        /// <summary>
        /// Desc:7号口铁料配比仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_07_ML {get;set;}

        /// <summary>
        /// Desc:7号口配矿配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_07_ORE_RATIO {get;set;}

        /// <summary>
        /// Desc:7号口铁料配比设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_07_SP {get;set;}

        /// <summary>
        /// Desc:7号口铁料配比当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_07_PV {get;set;}

        /// <summary>
        /// Desc:7号口铁料配比百分比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_07_PER {get;set;}

        /// <summary>
        /// Desc:7号口铁料配比分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_07_TC {get;set;}

        /// <summary>
        /// Desc:7号口铁料水分设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_07_H2O_SP {get;set;}

        /// <summary>
        /// Desc:7号口铁料水分当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_07_H2O_PV {get;set;}

        /// <summary>
        /// Desc:7号口铁料下料设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_07_SP {get;set;}

        /// <summary>
        /// Desc:7号口铁料下料当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_07_PV {get;set;}

        /// <summary>
        /// Desc:7号口铁料下料偏差值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_07_DV {get;set;}

        /// <summary>
        /// Desc:7号口铁料下料百分比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_07_PER {get;set;}

        /// <summary>
        /// Desc:7号口铁料设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_07_FRE {get;set;}

        /// <summary>
        /// Desc:7号口铁料累计（t） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_07_ATIVE {get;set;}

        /// <summary>
        /// Desc:8A号口烧返配比分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_08A_TC {get;set;}

        /// <summary>
        /// Desc:8A号口烧返水分设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_08A_H2O_SP {get;set;}

        /// <summary>
        /// Desc:8A号口烧返水分当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_08A_H2O_PV {get;set;}

        /// <summary>
        /// Desc:8A号口烧返下料设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_08A_BK_SP {get;set;}

        /// <summary>
        /// Desc:8A号口烧返下料当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_08A_BK_PV {get;set;}

        /// <summary>
        /// Desc:8A号口烧返下料偏差值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_08A_BK_DV {get;set;}

        /// <summary>
        /// Desc:8A号口烧返设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_08A_FRE {get;set;}

        /// <summary>
        /// Desc:8B号口烧返配比分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_08B_TC {get;set;}

        /// <summary>
        /// Desc:8B号口烧返水分设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_08B_H2O_SP {get;set;}

        /// <summary>
        /// Desc:8B号口烧返水分当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_08B_H2O_PV {get;set;}

        /// <summary>
        /// Desc:8B号口烧返下料设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_08B_BK_SP {get;set;}

        /// <summary>
        /// Desc:8B号口烧返下料当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_08B_BK_PV {get;set;}

        /// <summary>
        /// Desc:8B号口烧返下料偏差值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_08B_BK_DV {get;set;}

        /// <summary>
        /// Desc:8B号口烧返设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_08B_FRE {get;set;}

        /// <summary>
        /// Desc:烧返仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_POS {get;set;}

        /// <summary>
        /// Desc:烧返配比设定（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_SP {get;set;}

        /// <summary>
        /// Desc:烧返配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_PV {get;set;}

        /// <summary>
        /// Desc:烧返配比百分比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_DRY_TAGE {get;set;}

        /// <summary>
        /// Desc:烧返下料百分比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_WET_TAGE {get;set;}

        /// <summary>
        /// Desc:烧返累计（t） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_ATIVE {get;set;}

        /// <summary>
        /// Desc:配矿配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_IRON_ORE_RATIO {get;set;}

        /// <summary>
        /// Desc:9A号口石灰石配比分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_09A_TC {get;set;}

        /// <summary>
        /// Desc:9A号口石灰石水分设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_09A_H2O_SP {get;set;}

        /// <summary>
        /// Desc:9A号口石灰石水分当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_09A_H2O_PV {get;set;}

        /// <summary>
        /// Desc:9A号口石灰石下料设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_09A_BK_SP {get;set;}

        /// <summary>
        /// Desc:9A号口石灰石下料当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_09A_BK_PV {get;set;}

        /// <summary>
        /// Desc:9A号口石灰石下料偏差值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_09A_BK_DV {get;set;}

        /// <summary>
        /// Desc:9A号口石灰石设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_09A_FRE {get;set;}

        /// <summary>
        /// Desc:9B号口石灰石配比分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_09B_TC {get;set;}

        /// <summary>
        /// Desc:9B号口石灰石水分设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_09B_H2O_SP {get;set;}

        /// <summary>
        /// Desc:9B号口石灰石水分当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_09B_H2O_PV {get;set;}

        /// <summary>
        /// Desc:9B号口石灰石下料设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_09B_BK_SP {get;set;}

        /// <summary>
        /// Desc:9B号口石灰石下料当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_09B_BK_PV {get;set;}

        /// <summary>
        /// Desc:9B号口石灰石下料偏差值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_09B_BK_DV {get;set;}

        /// <summary>
        /// Desc:9B号口石灰石设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_09B_FRE {get;set;}

        /// <summary>
        /// Desc:石灰石仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_STONE_POS {get;set;}

        /// <summary>
        /// Desc:石灰石配比设定（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_STONE_SP {get;set;}

        /// <summary>
        /// Desc:石灰石配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_STONE_PV {get;set;}

        /// <summary>
        /// Desc:石灰石配比百分比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_STONE_DRY_TAGE {get;set;}

        /// <summary>
        /// Desc:石灰石下料百分比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_STONE_WET_TAGE {get;set;}

        /// <summary>
        /// Desc:石灰石累计（t） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_STONE_ATIVE {get;set;}

        /// <summary>
        /// Desc:配矿配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_ORE_RATIO {get;set;}

        /// <summary>
        /// Desc:10A号口自产生石灰配比分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_10A_TC {get;set;}

        /// <summary>
        /// Desc:10A号口自产生石灰水分设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_10A_H2O_SP {get;set;}

        /// <summary>
        /// Desc:10A号口自产生石灰水分当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_10A_H2O_PV {get;set;}

        /// <summary>
        /// Desc:10A号口自产生石灰下料设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_10A_BK_SP {get;set;}

        /// <summary>
        /// Desc:10A号口自产生石灰下料当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_10A_BK_PV {get;set;}

        /// <summary>
        /// Desc:10A号口自产生石灰下料偏差值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_10A_BK_DV {get;set;}

        /// <summary>
        /// Desc:10A号口自产生石灰设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_10A_FRE {get;set;}

        /// <summary>
        /// Desc:10B号口自产生石灰配比分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_10B_TC {get;set;}

        /// <summary>
        /// Desc:10B号口自产生石灰水分设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_10B_H2O_SP {get;set;}

        /// <summary>
        /// Desc:10B号口自产生石灰水分当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_10B_H2O_PV {get;set;}

        /// <summary>
        /// Desc:10B号口自产生石灰下料设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_10B_BK_SP {get;set;}

        /// <summary>
        /// Desc:10B号口自产生石灰下料当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_10B_BK_PV {get;set;}

        /// <summary>
        /// Desc:10B号口自产生石灰下料偏差值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_10B_BK_DV {get;set;}

        /// <summary>
        /// Desc:10B号口自产生石灰设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_10B_FRE {get;set;}

        /// <summary>
        /// Desc:自产生石灰仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_S_POS {get;set;}

        /// <summary>
        /// Desc:自产生石灰配比设定（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_S_SP {get;set;}

        /// <summary>
        /// Desc:自产生石灰配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_S_PV {get;set;}

        /// <summary>
        /// Desc:自产生石灰配比百分比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_S_DRY_TAGE {get;set;}

        /// <summary>
        /// Desc:自产生石灰下料百分比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_S_WET_TAGE {get;set;}

        /// <summary>
        /// Desc:自产生石灰累计（t） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_S_ATIVE {get;set;}

        /// <summary>
        /// Desc:自产生石灰配矿配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_S_ORE_RATIO {get;set;}

        /// <summary>
        /// Desc:11A号口除尘灰配比分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_11A_TC {get;set;}

        /// <summary>
        /// Desc:11A号口除尘灰水分设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_11A_H2O_SP {get;set;}

        /// <summary>
        /// Desc:11A号口除尘灰水分当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_11A_H2O_PV {get;set;}

        /// <summary>
        /// Desc:11A号口除尘灰下料设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_11A_BK_SP {get;set;}

        /// <summary>
        /// Desc:11A号口除尘灰下料当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_11A_BK_PV {get;set;}

        /// <summary>
        /// Desc:11A号口除尘灰下料偏差值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_11A_BK_DV {get;set;}

        /// <summary>
        /// Desc:11A号口除尘灰设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_11A_FRE {get;set;}

        /// <summary>
        /// Desc:11B号口除尘灰配比分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_11B_TC {get;set;}

        /// <summary>
        /// Desc:11B号口除尘灰水分设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_11B_H2O_SP {get;set;}

        /// <summary>
        /// Desc:11B号口除尘灰水分当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_11B_H2O_PV {get;set;}

        /// <summary>
        /// Desc:11B号口除尘灰下料设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_11B_BK_SP {get;set;}

        /// <summary>
        /// Desc:11B号口除尘灰下料当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_11B_BK_PV {get;set;}

        /// <summary>
        /// Desc:11B号口除尘灰下料偏差值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_11B_BK_DV {get;set;}

        /// <summary>
        /// Desc:11B号口除尘灰设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_11B_FRE {get;set;}

        /// <summary>
        /// Desc:除尘灰仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_DUST_POS {get;set;}

        /// <summary>
        /// Desc:除尘灰配比设定（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_DUST_SP {get;set;}

        /// <summary>
        /// Desc:除尘灰配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_DUST_PV {get;set;}

        /// <summary>
        /// Desc:除尘灰配比百分比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_DUST_DRY_TAGE {get;set;}

        /// <summary>
        /// Desc:除尘灰下料百分比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_DUST_WET_TAGE {get;set;}

        /// <summary>
        /// Desc:除尘灰累计（t） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_DUST_ATIVE {get;set;}

        /// <summary>
        /// Desc:除尘灰配矿配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_DUST_ORE_RATIO {get;set;}

        /// <summary>
        /// Desc:12号口生石灰配比仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_12_ML {get;set;}

        /// <summary>
        /// Desc:12号口生石灰配比设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_12_SP {get;set;}

        /// <summary>
        /// Desc:12号口生石灰配比当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_12_PV {get;set;}

        /// <summary>
        /// Desc:12号口生石灰配比百分比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_12_BILL {get;set;}

        /// <summary>
        /// Desc:12号口生石灰配分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_12_TC {get;set;}

        /// <summary>
        /// Desc:12号口生石灰水分设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_12_H2O_SP {get;set;}

        /// <summary>
        /// Desc:12号口生石灰水分当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_12_H2O_PV {get;set;}

        /// <summary>
        /// Desc:12号口生石灰下料设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_12_BK_SP {get;set;}

        /// <summary>
        /// Desc:12号口生石灰下料当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_12_BK_PV {get;set;}

        /// <summary>
        /// Desc:12号口生石灰下料偏差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_12_BK_DV {get;set;}

        /// <summary>
        /// Desc:12号口生石灰设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_12_FRE {get;set;}

        /// <summary>
        /// Desc:13号口生石灰配比仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_13_ML {get;set;}

        /// <summary>
        /// Desc:13号口生石灰配比设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_13_SP {get;set;}

        /// <summary>
        /// Desc:13号口生石灰配比当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_13_PV {get;set;}

        /// <summary>
        /// Desc:13号口生石灰配比百分比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_13_BILL {get;set;}

        /// <summary>
        /// Desc:13号口生石灰配分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_13_TC {get;set;}

        /// <summary>
        /// Desc:13号口生石灰水分设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_13_H2O_SP {get;set;}

        /// <summary>
        /// Desc:13号口生石灰水分当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_13_H2O_PV {get;set;}

        /// <summary>
        /// Desc:13号口生石灰下料设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_13_BK_SP {get;set;}

        /// <summary>
        /// Desc:13号口生石灰下料当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_13_BK_PV {get;set;}

        /// <summary>
        /// Desc:13号口生石灰下料偏差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_13_BK_DV {get;set;}

        /// <summary>
        /// Desc:13号口生石灰设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_13_FRE {get;set;}

        /// <summary>
        /// Desc:生石灰下料百分比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_LIV_WET_TAGE {get;set;}

        /// <summary>
        /// Desc:生石灰累计（t） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_LIV_ATIVE {get;set;}

        /// <summary>
        /// Desc:生石灰配矿配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_LIV_ORE_RATIO {get;set;}

        /// <summary>
        /// Desc:14号口燃料仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_14_ML {get;set;}

        /// <summary>
        /// Desc:14号口燃料配比设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_14_SP {get;set;}

        /// <summary>
        /// Desc:14号口燃料配比当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_14_PV {get;set;}

        /// <summary>
        /// Desc:14号口燃料配比百分比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_14_BILL {get;set;}

        /// <summary>
        /// Desc:14号口燃料配分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_14_TC {get;set;}

        /// <summary>
        /// Desc:14号口燃料水分设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_14_H2O_SP {get;set;}

        /// <summary>
        /// Desc:14号口燃料水分当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_14_H2O_PV {get;set;}

        /// <summary>
        /// Desc:14号口燃料下料设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_14_BK_SP {get;set;}

        /// <summary>
        /// Desc:14号口燃料下料当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_14_BK_PV {get;set;}

        /// <summary>
        /// Desc:14号口燃料下料偏差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_14_BK_DV {get;set;}

        /// <summary>
        /// Desc:14号口燃料下料百分比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_14_BK_BILL {get;set;}

        /// <summary>
        /// Desc:14号口燃料设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_14_FRE {get;set;}

        /// <summary>
        /// Desc:14号口燃料累计（t） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_14_ATIVE {get;set;}

        /// <summary>
        /// Desc:15号口燃料仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_15_ML {get;set;}

        /// <summary>
        /// Desc:15号口燃料配比设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_15_SP {get;set;}

        /// <summary>
        /// Desc:15号口燃料配比当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_15_PV {get;set;}

        /// <summary>
        /// Desc:15号口燃料配比百分比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_15_BILL {get;set;}

        /// <summary>
        /// Desc:15号口燃料配分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_15_TC {get;set;}

        /// <summary>
        /// Desc:15号口燃料水分设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_15_H2O_SP {get;set;}

        /// <summary>
        /// Desc:15号口燃料水分当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_15_H2O_PV {get;set;}

        /// <summary>
        /// Desc:15号口燃料下料设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_15_BK_SP {get;set;}

        /// <summary>
        /// Desc:15号口燃料下料当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_15_BK_PV {get;set;}

        /// <summary>
        /// Desc:15号口燃料下料偏差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_15_BK_DV {get;set;}

        /// <summary>
        /// Desc:15号口燃料下料百分比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_15_BK_BILL {get;set;}

        /// <summary>
        /// Desc:15号口燃料设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_15_FRE {get;set;}

        /// <summary>
        /// Desc:15号口燃料累计（t） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_15_ATIVE {get;set;}

        /// <summary>
        /// Desc:14、15配矿配比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_14_15_ORE_RATIO {get;set;}

        /// <summary>
        /// Desc:目标含碳 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_C_TARGET {get;set;}

        /// <summary>
        /// Desc:碳调整值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_C_ADJUST {get;set;}

        /// <summary>
        /// Desc:目标碱度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_R_TARGET {get;set;}

        /// <summary>
        /// Desc:R调整值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_R_ADJUST {get;set;}

        /// <summary>
        /// Desc:总干料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_TOTAL_DRY_MATERIAL {get;set;}

        /// <summary>
        /// Desc:总料量PV 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_TOTAL_MATERIAL_PV {get;set;}

        /// <summary>
        /// Desc:总料量SP 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_TOTAL_MATERIAL_SP {get;set;}

        /// <summary>
        /// Desc:采样时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SAMPLETIME {get;set;}

    }
}
