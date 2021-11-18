using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MC_SINCAL_RESULT_1MIN
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:1#料仓物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SINCAL_CODE_1 {get;set;}

        /// <summary>
        /// Desc:2#料仓物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SINCAL_CODE_2 {get;set;}

        /// <summary>
        /// Desc:3#料仓物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SINCAL_CODE_3 {get;set;}

        /// <summary>
        /// Desc:4#料仓物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SINCAL_CODE_4 {get;set;}

        /// <summary>
        /// Desc:5#料仓物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SINCAL_CODE_5 {get;set;}

        /// <summary>
        /// Desc:6#料仓物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SINCAL_CODE_6 {get;set;}

        /// <summary>
        /// Desc:7#料仓物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SINCAL_CODE_7 {get;set;}

        /// <summary>
        /// Desc:8#料仓物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SINCAL_CODE_8 {get;set;}

        /// <summary>
        /// Desc:9#料仓物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SINCAL_CODE_9 {get;set;}

        /// <summary>
        /// Desc:10#料仓物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SINCAL_CODE_10 {get;set;}

        /// <summary>
        /// Desc:11#料仓物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SINCAL_CODE_11 {get;set;}

        /// <summary>
        /// Desc:12#料仓物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SINCAL_CODE_12 {get;set;}

        /// <summary>
        /// Desc:13#料仓物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SINCAL_CODE_13 {get;set;}

        /// <summary>
        /// Desc:14#料仓物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SINCAL_CODE_14 {get;set;}

        /// <summary>
        /// Desc:15#料仓物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SINCAL_CODE_15 {get;set;}

        /// <summary>
        /// Desc:1#仓物料成分更新时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SINCAL_PB01_TIME {get;set;}

        /// <summary>
        /// Desc:2#仓物料成分更新时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SINCAL_PB02_TIME {get;set;}

        /// <summary>
        /// Desc:3#仓物料成分更新时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SINCAL_PB03_TIME {get;set;}

        /// <summary>
        /// Desc:4#仓物料成分更新时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SINCAL_PB04_TIME {get;set;}

        /// <summary>
        /// Desc:5#仓物料成分更新时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SINCAL_PB05_TIME {get;set;}

        /// <summary>
        /// Desc:6#仓物料成分更新时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SINCAL_PB06_TIME {get;set;}

        /// <summary>
        /// Desc:7#仓物料成分更新时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SINCAL_PB07_TIME {get;set;}

        /// <summary>
        /// Desc:8#仓物料成分更新时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SINCAL_PB08_TIME {get;set;}

        /// <summary>
        /// Desc:9#仓物料成分更新时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SINCAL_PB09_TIME {get;set;}

        /// <summary>
        /// Desc:10#仓物料成分更新时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SINCAL_PB10_TIME {get;set;}

        /// <summary>
        /// Desc:11#仓物料成分更新时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SINCAL_PB11_TIME {get;set;}

        /// <summary>
        /// Desc:12#仓物料成分更新时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SINCAL_PB12_TIME {get;set;}

        /// <summary>
        /// Desc:13#仓物料成分更新时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SINCAL_PB13_TIME {get;set;}

        /// <summary>
        /// Desc:14#仓物料成分更新时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SINCAL_PB14_TIME {get;set;}

        /// <summary>
        /// Desc:15#仓物料成分更新时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SINCAL_PB15_TIME {get;set;}

        /// <summary>
        /// Desc:1#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_SL_1 {get;set;}

        /// <summary>
        /// Desc:2#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_SL_2 {get;set;}

        /// <summary>
        /// Desc:3#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_SL_3 {get;set;}

        /// <summary>
        /// Desc:4#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_SL_4 {get;set;}

        /// <summary>
        /// Desc:5#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_SL_5 {get;set;}

        /// <summary>
        /// Desc:6#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_SL_6 {get;set;}

        /// <summary>
        /// Desc:7#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_SL_7 {get;set;}

        /// <summary>
        /// Desc:8#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_SL_8 {get;set;}

        /// <summary>
        /// Desc:9#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_SL_9 {get;set;}

        /// <summary>
        /// Desc:10#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_SL_10 {get;set;}

        /// <summary>
        /// Desc:11#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_SL_11 {get;set;}

        /// <summary>
        /// Desc:12#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_SL_12 {get;set;}

        /// <summary>
        /// Desc:13#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_SL_13 {get;set;}

        /// <summary>
        /// Desc:14#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_SL_14 {get;set;}

        /// <summary>
        /// Desc:15#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_SL_15 {get;set;}

        /// <summary>
        /// Desc:16#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_SL_16 {get;set;}

        /// <summary>
        /// Desc:17#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_SL_17 {get;set;}

        /// <summary>
        /// Desc:18#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_SL_18 {get;set;}

        /// <summary>
        /// Desc:19#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_SL_19 {get;set;}

        /// <summary>
        /// Desc:1#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SP_W_1 {get;set;}

        /// <summary>
        /// Desc:2#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SP_W_2 {get;set;}

        /// <summary>
        /// Desc:3#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SP_W_3 {get;set;}

        /// <summary>
        /// Desc:4#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SP_W_4 {get;set;}

        /// <summary>
        /// Desc:5#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SP_W_5 {get;set;}

        /// <summary>
        /// Desc:6#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SP_W_6 {get;set;}

        /// <summary>
        /// Desc:7#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SP_W_7 {get;set;}

        /// <summary>
        /// Desc:8#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SP_W_8 {get;set;}

        /// <summary>
        /// Desc:9#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SP_W_9 {get;set;}

        /// <summary>
        /// Desc:10#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SP_W_10 {get;set;}

        /// <summary>
        /// Desc:11#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SP_W_11 {get;set;}

        /// <summary>
        /// Desc:12#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SP_W_12 {get;set;}

        /// <summary>
        /// Desc:13#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SP_W_13 {get;set;}

        /// <summary>
        /// Desc:14#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SP_W_14 {get;set;}

        /// <summary>
        /// Desc:15#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SP_W_15 {get;set;}

        /// <summary>
        /// Desc:16#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SP_W_16 {get;set;}

        /// <summary>
        /// Desc:17#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SP_W_17 {get;set;}

        /// <summary>
        /// Desc:18#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SP_W_18 {get;set;}

        /// <summary>
        /// Desc:19#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SP_W_19 {get;set;}

        /// <summary>
        /// Desc:1#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ACTUAL_W_1 {get;set;}

        /// <summary>
        /// Desc:2#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ACTUAL_W_2 {get;set;}

        /// <summary>
        /// Desc:3#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ACTUAL_W_3 {get;set;}

        /// <summary>
        /// Desc:4#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ACTUAL_W_4 {get;set;}

        /// <summary>
        /// Desc:5#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ACTUAL_W_5 {get;set;}

        /// <summary>
        /// Desc:6#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ACTUAL_W_6 {get;set;}

        /// <summary>
        /// Desc:7#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ACTUAL_W_7 {get;set;}

        /// <summary>
        /// Desc:8#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ACTUAL_W_8 {get;set;}

        /// <summary>
        /// Desc:9#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ACTUAL_W_9 {get;set;}

        /// <summary>
        /// Desc:10#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ACTUAL_W_10 {get;set;}

        /// <summary>
        /// Desc:11#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ACTUAL_W_11 {get;set;}

        /// <summary>
        /// Desc:12#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ACTUAL_W_12 {get;set;}

        /// <summary>
        /// Desc:13#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ACTUAL_W_13 {get;set;}

        /// <summary>
        /// Desc:14#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ACTUAL_W_14 {get;set;}

        /// <summary>
        /// Desc:15#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ACTUAL_W_15 {get;set;}

        /// <summary>
        /// Desc:16#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ACTUAL_W_16 {get;set;}

        /// <summary>
        /// Desc:17#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ACTUAL_W_17 {get;set;}

        /// <summary>
        /// Desc:18#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ACTUAL_W_18 {get;set;}

        /// <summary>
        /// Desc:19#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ACTUAL_W_19 {get;set;}

        /// <summary>
        /// Desc:混匀矿配比设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BLEND_ORE_BILL_SP {get;set;}

        /// <summary>
        /// Desc:铁料1配比设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_ORE_SP_01 {get;set;}

        /// <summary>
        /// Desc:铁料2配比设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_ORE_SP_02 {get;set;}

        /// <summary>
        /// Desc:铁料3配比设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_ORE_SP_03 {get;set;}

        /// <summary>
        /// Desc:铁料4配比设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_ORE_SP_04 {get;set;}

        /// <summary>
        /// Desc:铁料5配比设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_ORE_SP_05 {get;set;}

        /// <summary>
        /// Desc:铁料6配比设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_ORE_SP_06 {get;set;}

        /// <summary>
        /// Desc:铁料7配比设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_ORE_SP_07 {get;set;}

        /// <summary>
        /// Desc:烧返配比设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_SP_08 {get;set;}

        /// <summary>
        /// Desc:石灰石配比设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_STONE_SP_09 {get;set;}

        /// <summary>
        /// Desc:自产生石灰配比设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_STONE_SP_10 {get;set;}

        /// <summary>
        /// Desc:除尘灰配比设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_DUST_SP_11 {get;set;}

        /// <summary>
        /// Desc:生石灰1配比设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_SP_12 {get;set;}

        /// <summary>
        /// Desc:生石灰2配比设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_SP_13 {get;set;}

        /// <summary>
        /// Desc:燃料1配比设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_BILL_SP_14 {get;set;}

        /// <summary>
        /// Desc:燃料2配比设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_BILL_SP_15 {get;set;}

        /// <summary>
        /// Desc:混匀矿配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BLEND_ORE_BILL_PV {get;set;}

        /// <summary>
        /// Desc:铁料1配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_ORE_PV_01 {get;set;}

        /// <summary>
        /// Desc:铁料2配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_ORE_PV_02 {get;set;}

        /// <summary>
        /// Desc:铁料3配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_ORE_PV_03 {get;set;}

        /// <summary>
        /// Desc:铁料4配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_ORE_PV_04 {get;set;}

        /// <summary>
        /// Desc:铁料5配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_ORE_PV_05 {get;set;}

        /// <summary>
        /// Desc:铁料6配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_ORE_PV_06 {get;set;}

        /// <summary>
        /// Desc:铁料7配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BILL_ORE_PV_07 {get;set;}

        /// <summary>
        /// Desc:烧返配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BRUN_PV_08 {get;set;}

        /// <summary>
        /// Desc:石灰石配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_STONE_PV_09 {get;set;}

        /// <summary>
        /// Desc:自产生石灰配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_STONE_PV_10 {get;set;}

        /// <summary>
        /// Desc:除尘灰配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ASH_DUST_PV_11 {get;set;}

        /// <summary>
        /// Desc:生石灰1配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_PV_12 {get;set;}

        /// <summary>
        /// Desc:生石灰2配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FLUX_PV_13 {get;set;}

        /// <summary>
        /// Desc:燃料1配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_BILL_PV_14 {get;set;}

        /// <summary>
        /// Desc:燃料2配比当前 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_BILL_PV_15 {get;set;}

        /// <summary>
        /// Desc:目标含碳 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_C_A {get;set;}

        /// <summary>
        /// Desc:目标碱度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_R_A {get;set;}

        /// <summary>
        /// Desc:碱度调整值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_R_C {get;set;}

        /// <summary>
        /// Desc:含碳调整值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_C_DC {get;set;}

        /// <summary>
        /// Desc:总料量SP 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SUM_MIX_SP {get;set;}

        /// <summary>
        /// Desc:总料量PV 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SUM_MIX_PV {get;set;}

        /// <summary>
        /// Desc:倒推分钟 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_RETRODICT_TIME {get;set;}

        /// <summary>
        /// Desc:TFe（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_TFE {get;set;}

        /// <summary>
        /// Desc:FeO（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_FEO {get;set;}

        /// <summary>
        /// Desc:CaO（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_CAO {get;set;}

        /// <summary>
        /// Desc:SiO2（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_SIO2 {get;set;}

        /// <summary>
        /// Desc:Al2O3（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_AL2O3 {get;set;}

        /// <summary>
        /// Desc:MgO（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_MGO {get;set;}

        /// <summary>
        /// Desc:S（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_S {get;set;}

        /// <summary>
        /// Desc:P（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_P {get;set;}

        /// <summary>
        /// Desc:Mn（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_MN {get;set;}

        /// <summary>
        /// Desc:碱度（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_R {get;set;}

        /// <summary>
        /// Desc:TIO2（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_TIO2 {get;set;}

        /// <summary>
        /// Desc:K2O（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_K2O {get;set;}

        /// <summary>
        /// Desc:NA2O（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_NA2O {get;set;}

        /// <summary>
        /// Desc:PBO（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_PBO {get;set;}

        /// <summary>
        /// Desc:ZNO（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_ZNO {get;set;}

        /// <summary>
        /// Desc:F（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_TI { get;set;}

        /// <summary>
        /// Desc:AS（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_AS {get;set;}

        /// <summary>
        /// Desc:CU（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_CU {get;set;}

        /// <summary>
        /// Desc:PB（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_PB {get;set;}

        /// <summary>
        /// Desc:ZN（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_ZN {get;set;}

        /// <summary>
        /// Desc:K（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_K {get;set;}

        /// <summary>
        /// Desc:NA（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_NA {get;set;}

        /// <summary>
        /// Desc:Cr（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_CR {get;set;}

        /// <summary>
        /// Desc:Ni（设定下料量预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_NI {get;set;}

        /// <summary>
        /// Desc:备用成分1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_SPARE1 {get;set;}

        /// <summary>
        /// Desc:备用成分2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_SPARE2 {get;set;}

        /// <summary>
        /// Desc:备用成分3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_SPARE3 {get;set;}

        /// <summary>
        /// Desc:备用成分4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_SPARE4 {get;set;}

        /// <summary>
        /// Desc:备用成分5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_SPARE5 {get;set;}

        /// <summary>
        /// Desc:备用成分6 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_SP_SPARE6 {get;set;}

        /// <summary>
        /// Desc:设定下料量计算混合料综合烧损 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_MIX_SP_LOT {get;set;}

        /// <summary>
        /// Desc:采集水分计算的混合料原始水分含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_MIX_SP_H2O_1 {get;set;}

        /// <summary>
        /// Desc:设定水分计算的混合料原始水分含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_MIX_SP_H2O_2 {get;set;}

        /// <summary>
        /// Desc:设定下料量计算混合料FeO含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_MIX_SP_FEO {get;set;}

        /// <summary>
        /// Desc:设定下料量计算混合料固定碳 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_MIX_SP_C {get;set;}

        /// <summary>
        /// Desc:设定下料量计算混合料中非燃料带入的固定碳 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_NON_FUEL_SP_C {get;set;}

        /// <summary>
        /// Desc:设定下料量计算混合料中燃料带入的固定碳 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_SP_C {get;set;}

        /// <summary>
        /// Desc:设定下料量计算混合料中非铁料带入的SIO2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_NON_FE_SP_SIO2 {get;set;}

        /// <summary>
        /// Desc:设定下料量计算混合料总干料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_DRY_MIX_SP {get;set;}

        /// <summary>
        /// Desc:设定下料量计算每分钟烧结矿理论产量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_OUTPUT_SP {get;set;}

        /// <summary>
        /// Desc:设定下料量计算理论燃耗 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_CON_SP {get;set;}

        /// <summary>
        /// Desc:TFe（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_TFE { get;set;}

        /// <summary>
        /// Desc:FeO（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_FEO {get;set;}

        /// <summary>
        /// Desc:CaO（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_CAO {get;set;}

        /// <summary>
        /// Desc:SiO2（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_SIO2 {get;set;}

        /// <summary>
        /// Desc:Al2O3（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_AL2O3 {get;set;}

        /// <summary>
        /// Desc:MgO（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_MGO {get;set;}

        /// <summary>
        /// Desc:S（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_S {get;set;}

        /// <summary>
        /// Desc:P（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_P {get;set;}

        /// <summary>
        /// Desc:Mn（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_MN {get;set;}

        /// <summary>
        /// Desc:碱度（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_R {get;set;}

        /// <summary>
        /// Desc:TIO2（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_TIO2 {get;set;}

        /// <summary>
        /// Desc:K2O（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_K2O {get;set;}

        /// <summary>
        /// Desc:NA2O（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_NA2O {get;set;}

        /// <summary>
        /// Desc:PBO（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_PBO {get;set;}

        /// <summary>
        /// Desc:ZNO（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_ZNO {get;set;}

        /// <summary>
        /// Desc:F（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_TI { get;set;}

        /// <summary>
        /// Desc:AS（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_AS {get;set;}

        /// <summary>
        /// Desc:CU（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_CU {get;set;}

        /// <summary>
        /// Desc:PB（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_PB {get;set;}

        /// <summary>
        /// Desc:ZN（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_ZN {get;set;}

        /// <summary>
        /// Desc:K（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_K {get;set;}

        /// <summary>
        /// Desc:NA（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_NA {get;set;}

        /// <summary>
        /// Desc:Cr（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_CR {get;set;}

        /// <summary>
        /// Desc:Ni（实际下料量5分钟平均值预测烧结矿成分） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_NI {get;set;}

        /// <summary>
        /// Desc:备用成分1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_SPARE1 {get;set;}

        /// <summary>
        /// Desc:备用成分2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_SPARE2 {get;set;}

        /// <summary>
        /// Desc:备用成分3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_SPARE3 {get;set;}

        /// <summary>
        /// Desc:备用成分4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_SPARE4 {get;set;}

        /// <summary>
        /// Desc:备用成分5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_SPARE5 {get;set;}

        /// <summary>
        /// Desc:备用成分6 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_SIN_PV_SPARE6 {get;set;}

        /// <summary>
        /// Desc:实际下料量计算混合料综合烧损 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_MIX_PV_LOT {get;set;}

        /// <summary>
        /// Desc:采集水分计算的混合料原始水分含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_MIX_PV_H2O_1 {get;set;}

        /// <summary>
        /// Desc:实际水分计算的混合料原始水分含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_MIX_PV_H2O_2 {get;set;}

        /// <summary>
        /// Desc:实际下料量计算混合料FeO含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_MIX_PV_FEO {get;set;}

        /// <summary>
        /// Desc:实际下料量计算混合料固定碳 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_MIX_PV_C {get;set;}

        /// <summary>
        /// Desc:实际下料量计算混合料中非燃料带入的固定碳 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_NON_FUEL_PV_C {get;set;}

        /// <summary>
        /// Desc:实际下料量计算混合料中燃料带入的固定碳 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_PV_C {get;set;}

        /// <summary>
        /// Desc:实际下料量计算混合料中非铁料带入的SIO2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_NON_FE_PV_SIO2 {get;set;}

        /// <summary>
        /// Desc:实际下料量计算混合料总干料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_DRY_MIX_PV {get;set;}

        /// <summary>
        /// Desc:实际下料量计算每分钟烧结矿理论产量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_OUTPUT_PV {get;set;}

        /// <summary>
        /// Desc:实际下料量计算理论燃耗 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_FUEL_CON_PV {get;set;}

        /// <summary>
        /// Desc:实际下料计算烧返矿百分比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_B_B_RATIO_PV {get;set;}

        /// <summary>
        /// Desc:实际下料计算综合烧损(%) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_BL_RATIO_PV {get;set;}

    }
}
