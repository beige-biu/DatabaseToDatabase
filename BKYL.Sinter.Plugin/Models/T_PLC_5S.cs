using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class T_PLC_5S
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:总料量SP 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_TOTAL_SP_W_5S {get;set;}

        /// <summary>
        /// Desc:总料量PV 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_TOTAL_PV_W_5S {get;set;}

        /// <summary>
        /// Desc:1#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_W_1_5S {get;set;}

        /// <summary>
        /// Desc:2#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_W_2_5S {get;set;}

        /// <summary>
        /// Desc:3#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_W_3_5S {get;set;}

        /// <summary>
        /// Desc:4#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_W_4_5S {get;set;}

        /// <summary>
        /// Desc:5#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_W_5_5S {get;set;}

        /// <summary>
        /// Desc:6#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_W_6_5S {get;set;}

        /// <summary>
        /// Desc:7#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_W_7_5S {get;set;}

        /// <summary>
        /// Desc:8#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_W_8_5S {get;set;}

        /// <summary>
        /// Desc:9#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_W_9_5S {get;set;}

        /// <summary>
        /// Desc:10#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_W_10_5S {get;set;}

        /// <summary>
        /// Desc:11#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_W_11_5S {get;set;}

        /// <summary>
        /// Desc:12#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_W_12_5S {get;set;}

        /// <summary>
        /// Desc:13#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_W_13_5S {get;set;}

        /// <summary>
        /// Desc:14#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_W_14_5S {get;set;}

        /// <summary>
        /// Desc:15#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_W_15_5S {get;set;}

        /// <summary>
        /// Desc:1#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? T_CODE_1 {get;set;}

        /// <summary>
        /// Desc:2#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? T_CODE_2 {get;set;}

        /// <summary>
        /// Desc:3#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? T_CODE_3 {get;set;}

        /// <summary>
        /// Desc:4#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? T_CODE_4 {get;set;}

        /// <summary>
        /// Desc:5#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? T_CODE_5 {get;set;}

        /// <summary>
        /// Desc:6#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? T_CODE_6 {get;set;}

        /// <summary>
        /// Desc:7#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? T_CODE_7 {get;set;}

        /// <summary>
        /// Desc:8#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? T_CODE_8 {get;set;}

        /// <summary>
        /// Desc:9#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? T_CODE_9 {get;set;}

        /// <summary>
        /// Desc:10#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? T_CODE_10 {get;set;}

        /// <summary>
        /// Desc:11#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? T_CODE_11 {get;set;}

        /// <summary>
        /// Desc:12#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? T_CODE_12 {get;set;}

        /// <summary>
        /// Desc:13#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? T_CODE_13 {get;set;}

        /// <summary>
        /// Desc:14#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? T_CODE_14 {get;set;}

        /// <summary>
        /// Desc:15#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? T_CODE_15 {get;set;}

        /// <summary>
        /// Desc:1#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int T_SL_1_5S {get;set;}

        /// <summary>
        /// Desc:2#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int T_SL_2_5S {get;set;}

        /// <summary>
        /// Desc:3#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int T_SL_3_5S {get;set;}

        /// <summary>
        /// Desc:4#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int T_SL_4_5S {get;set;}

        /// <summary>
        /// Desc:5#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int T_SL_5_5S {get;set;}

        /// <summary>
        /// Desc:6#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int T_SL_6_5S {get;set;}

        /// <summary>
        /// Desc:7#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int T_SL_7_5S {get;set;}

        /// <summary>
        /// Desc:8#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int T_SL_8_5S {get;set;}

        /// <summary>
        /// Desc:9#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int T_SL_9_5S {get;set;}

        /// <summary>
        /// Desc:10#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int T_SL_10_5S {get;set;}

        /// <summary>
        /// Desc:11#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int T_SL_11_5S {get;set;}

        /// <summary>
        /// Desc:12#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int T_SL_12_5S {get;set;}

        /// <summary>
        /// Desc:13#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int T_SL_13_5S {get;set;}

        /// <summary>
        /// Desc:14#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int T_SL_14_5S {get;set;}

        /// <summary>
        /// Desc:15#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int T_SL_15_5S {get;set;}

        /// <summary>
        /// Desc:16#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int T_SL_16_5S {get;set;}

        /// <summary>
        /// Desc:17#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int T_SL_17_5S {get;set;}

        /// <summary>
        /// Desc:18#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int T_SL_18_5S {get;set;}

        /// <summary>
        /// Desc:19#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int T_SL_19_5S {get;set;}

        /// <summary>
        /// Desc:1#下料口皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELT_SL_1_5S {get;set;}

        /// <summary>
        /// Desc:2#下料口皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELT_SL_2_5S {get;set;}

        /// <summary>
        /// Desc:3#下料口皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELT_SL_3_5S {get;set;}

        /// <summary>
        /// Desc:4#下料口皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELT_SL_4_5S {get;set;}

        /// <summary>
        /// Desc:5#下料口皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELT_SL_5_5S {get;set;}

        /// <summary>
        /// Desc:6#下料口皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELT_SL_6_5S {get;set;}

        /// <summary>
        /// Desc:7#下料口皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELT_SL_7_5S {get;set;}

        /// <summary>
        /// Desc:8#下料口皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELT_SL_8_5S {get;set;}

        /// <summary>
        /// Desc:9#下料口皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELT_SL_9_5S {get;set;}

        /// <summary>
        /// Desc:10#下料口皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELT_SL_10_5S {get;set;}

        /// <summary>
        /// Desc:11#下料口皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELT_SL_11_5S {get;set;}

        /// <summary>
        /// Desc:12#下料口皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELT_SL_12_5S {get;set;}

        /// <summary>
        /// Desc:13#下料口皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELT_SL_13_5S {get;set;}

        /// <summary>
        /// Desc:14#下料口皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELT_SL_14_5S {get;set;}

        /// <summary>
        /// Desc:15#下料口皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELT_SL_15_5S {get;set;}

        /// <summary>
        /// Desc:16#下料口皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELT_SL_16_5S {get;set;}

        /// <summary>
        /// Desc:17#下料口皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELT_SL_17_5S {get;set;}

        /// <summary>
        /// Desc:18#下料口皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELT_SL_18_5S {get;set;}

        /// <summary>
        /// Desc:19#下料口皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELTSL_19_5S {get;set;}

        /// <summary>
        /// Desc:1#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SP_W_1_5S {get;set;}

        /// <summary>
        /// Desc:2#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SP_W_2_5S {get;set;}

        /// <summary>
        /// Desc:3#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SP_W_3_5S {get;set;}

        /// <summary>
        /// Desc:4#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SP_W_4_5S {get;set;}

        /// <summary>
        /// Desc:5#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SP_W_5_5S {get;set;}

        /// <summary>
        /// Desc:6#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SP_W_6_5S {get;set;}

        /// <summary>
        /// Desc:7#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SP_W_7_5S {get;set;}

        /// <summary>
        /// Desc:8#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SP_W_8_5S {get;set;}

        /// <summary>
        /// Desc:9#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SP_W_9_5S {get;set;}

        /// <summary>
        /// Desc:10#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SP_W_10_5S {get;set;}

        /// <summary>
        /// Desc:11#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SP_W_11_5S {get;set;}

        /// <summary>
        /// Desc:12#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SP_W_12_5S {get;set;}

        /// <summary>
        /// Desc:13#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SP_W_13_5S {get;set;}

        /// <summary>
        /// Desc:14#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SP_W_14_5S {get;set;}

        /// <summary>
        /// Desc:15#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SP_W_15_5S {get;set;}

        /// <summary>
        /// Desc:16#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SP_W_16_5S {get;set;}

        /// <summary>
        /// Desc:17#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SP_W_17_5S {get;set;}

        /// <summary>
        /// Desc:18#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SP_W_18_5S {get;set;}

        /// <summary>
        /// Desc:19#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SP_W_19_5S {get;set;}

        /// <summary>
        /// Desc:1#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_ACTUAL_W_1_5S {get;set;}

        /// <summary>
        /// Desc:2#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_ACTUAL_W_2_5S {get;set;}

        /// <summary>
        /// Desc:3#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_ACTUAL_W_3_5S {get;set;}

        /// <summary>
        /// Desc:4#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_ACTUAL_W_4_5S {get;set;}

        /// <summary>
        /// Desc:5#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_ACTUAL_W_5_5S {get;set;}

        /// <summary>
        /// Desc:6#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_ACTUAL_W_6_5S {get;set;}

        /// <summary>
        /// Desc:7#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_ACTUAL_W_7_5S {get;set;}

        /// <summary>
        /// Desc:8#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_ACTUAL_W_8_5S {get;set;}

        /// <summary>
        /// Desc:9#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_ACTUAL_W_9_5S {get;set;}

        /// <summary>
        /// Desc:10#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_ACTUAL_W_10_5S {get;set;}

        /// <summary>
        /// Desc:11#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_ACTUAL_W_11_5S {get;set;}

        /// <summary>
        /// Desc:12#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_ACTUAL_W_12_5S {get;set;}

        /// <summary>
        /// Desc:13#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_ACTUAL_W_13_5S {get;set;}

        /// <summary>
        /// Desc:14#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_ACTUAL_W_14_5S {get;set;}

        /// <summary>
        /// Desc:15#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_ACTUAL_W_15_5S {get;set;}

        /// <summary>
        /// Desc:16#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_ACTUAL_W_16_5S {get;set;}

        /// <summary>
        /// Desc:17#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_ACTUAL_W_17_5S {get;set;}

        /// <summary>
        /// Desc:18#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_ACTUAL_W_18_5S {get;set;}

        /// <summary>
        /// Desc:19#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_ACTUAL_W_19_5S {get;set;}

        /// <summary>
        /// Desc:P-7皮带（配料总皮带）启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BELT_SL_P_7_5S {get;set;}

        /// <summary>
        /// Desc:一混前皮带启停信号(皮带1H-1) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_1M_PRE_BELT_B_E_5S {get;set;}

        /// <summary>
        /// Desc:一混前转运皮带秤值（用于加水开停机判断）(皮带1H-1) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_1M_PRE_BELT_W_1H_1_5S {get;set;}

        /// <summary>
        /// Desc:一混设备启停信号（启-1；停-0） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_1M_SL_5S {get;set;}

        /// <summary>
        /// Desc:一混目标水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_1M_TARGET_WATER_5S {get;set;}

        /// <summary>
        /// Desc:一混加水流量设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_1M_FT_SP_5S {get;set;}

        /// <summary>
        /// Desc:一混加水流量反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_1M_FT_PV_5S {get;set;}

        /// <summary>
        /// Desc:一混混合机转速/频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_1M_MIXER_RATE_5S {get;set;}

        /// <summary>
        /// Desc:一混后混合料水分检测 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_1M_NEX_WATER_AVG_5S {get;set;}

        /// <summary>
        /// Desc:一混后皮带启停信号(皮带2H-1) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_1M_NEX_BELT_B_E_2H_1_5S {get;set;}

        /// <summary>
        /// Desc:一混后皮带秤值(皮带2H-1) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_1M_NEX_BELT_W_2H_1_5S {get;set;}

        /// <summary>
        /// Desc:二混设备启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_2M_SL_5S {get;set;}

        /// <summary>
        /// Desc:二混目标水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_2M_TARGET_WATER_5S {get;set;}

        /// <summary>
        /// Desc:二混加水流量设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_2M_FLOW_SP_5S {get;set;}

        /// <summary>
        /// Desc:二混加水流量反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_2M_FLOW_PV_5S {get;set;}

        /// <summary>
        /// Desc:二混混合机转速/频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_2M_MIXER_RATE_5S {get;set;}

        /// <summary>
        /// Desc:二混后转运皮带秤值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_2M_BELT_VALUE_5S {get;set;}

        /// <summary>
        /// Desc:二混后皮带启停信号(皮带Z2-1) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_2M_NEX_BELT_2H_1_S_5S {get;set;}

        /// <summary>
        /// Desc:二混后混合料水分检测 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_2M_NEX_WATER_AVG_5S {get;set;}

        /// <summary>
        /// Desc:进梭式布料器S-1皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_IN_SK_S_5S {get;set;}

        /// <summary>
        /// Desc:梭式布料器皮带启停信号(S-6皮带) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_SHUTTLE_S_S_5S {get;set;}

        /// <summary>
        /// Desc:混合料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_BLEND_LEVEL_5S {get;set;}

        /// <summary>
        /// Desc:圆辊给料机启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_STICK_SL_5S {get;set;}

        /// <summary>
        /// Desc:圆辊给料机设定频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_STICK_SP_5S {get;set;}

        /// <summary>
        /// Desc:圆辊给料机反馈频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_STICK_PV_5S {get;set;}

        /// <summary>
        /// Desc:九辊布料器A启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_N_STICK_A_SL_5S {get;set;}

        /// <summary>
        /// Desc:九辊布料器B启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_N_STICK_B_SL_5S {get;set;}

        /// <summary>
        /// Desc:目标点火温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_AIM_TE_5S {get;set;}

        /// <summary>
        /// Desc:点火段温度1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_IG_01_TE_5S {get;set;}

        /// <summary>
        /// Desc:点火段温度2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_IG_02_TE_5S {get;set;}

        /// <summary>
        /// Desc:点火段温度3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_IG_03_TE_5S {get;set;}

        /// <summary>
        /// Desc:点火段煤气流量反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_IG_GAS_PV_5S {get;set;}

        /// <summary>
        /// Desc:点火段空气流量反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_IG_AIR_PV_5S {get;set;}

        /// <summary>
        /// Desc:烧结机启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_SIN_SL_5S {get;set;}

        /// <summary>
        /// Desc:烧结机机速设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SIN_MS_SP_5S {get;set;}

        /// <summary>
        /// Desc:烧结机机速反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_SIN_MS_PV_5S {get;set;}

        /// <summary>
        /// Desc:主抽1风机启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_FAN_1_SL_5S {get;set;}

        /// <summary>
        /// Desc:主抽2风机启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_FAN_2_SL_5S {get;set;}

        /// <summary>
        /// Desc:主抽1大烟道废气流量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_MA_SB_1_FLUE_FT_5S {get;set;}

        /// <summary>
        /// Desc:主抽2大烟道废气流量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_MA_SB_2_FLUE_FT_5S {get;set;}

        /// <summary>
        /// Desc:主抽1大烟道压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_MA_SB_1_FLUE_PT_5S {get;set;}

        /// <summary>
        /// Desc:主抽2大烟道压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_MA_SB_2_FLUE_PT_5S {get;set;}

        /// <summary>
        /// Desc:主抽1大烟道温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_MA_SB_1_FLUE_TE_5S {get;set;}

        /// <summary>
        /// Desc:主抽2大烟道温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_MA_SB_2_FLUE_TE_5S {get;set;}

        /// <summary>
        /// Desc:单辊破碎机启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BM_SL_5S {get;set;}

        /// <summary>
        /// Desc:环冷机启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_RC_SL_5S {get;set;}

        /// <summary>
        /// Desc:环冷机机速设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_RC_SPEED_SP_5S {get;set;}

        /// <summary>
        /// Desc:环冷机机速反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_RC_SPEED_PV_5S {get;set;}

        /// <summary>
        /// Desc:环冷机入口温度() 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_RC_IN_TE_5S {get;set;}

        /// <summary>
        /// Desc:环冷机出口温度（或烧结矿冷却后温度） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_RC_OUT_TE_5S {get;set;}

        /// <summary>
        /// Desc:环冷机1号鼓风机启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_RC_B_S_1_5S {get;set;}

        /// <summary>
        /// Desc:环冷机2号鼓风机启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_RC_B_S_2_5S {get;set;}

        /// <summary>
        /// Desc:环冷机3号鼓风机启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_RC_B_S_3_5S {get;set;}

        /// <summary>
        /// Desc:环冷机4号鼓风机启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_RC_B_S_4_5S {get;set;}

        /// <summary>
        /// Desc:环冷机5号鼓风机启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_RC_B_S_5_5S {get;set;}

        /// <summary>
        /// Desc:板式给矿机启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_PF_SL_5S {get;set;}

        /// <summary>
        /// Desc:板式给矿机设定转速/皮带 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_PF_SPEED_SP_5S {get;set;}

        /// <summary>
        /// Desc:板式给矿机反馈转速/皮带 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_PF_SPEED_PV_5S {get;set;}

        /// <summary>
        /// Desc:进料筛SF-1皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_MS_IN_SF_1_SL_5S {get;set;}

        /// <summary>
        /// Desc:成品矿皮带启停信号（P-8皮带） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_FP_BELT_SL_5S {get;set;}

        /// <summary>
        /// Desc:冷返矿SF-3皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_COLD_AO_SF_3_SL_5S {get;set;}

        /// <summary>
        /// Desc:冷返矿Z1-2皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_COLD_AO_Z1_2_SL_5S {get;set;}

        /// <summary>
        /// Desc:冷返矿Z2-3皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_COLD_AO_Z1_3_SL_5S {get;set;}

        /// <summary>
        /// Desc:铺底料SF-2皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BED_MATERAL_SF_2_SL_5S {get;set;}

        /// <summary>
        /// Desc:铺底料Z1-1皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BED_MATERAL_Z1_1_SL_5S {get;set;}

        /// <summary>
        /// Desc:铺底料Z2-2皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BED_MATERAL_Z1_2_SL_5S {get;set;}

        /// <summary>
        /// Desc:铺底料S-2皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_BED_MATERAL_S_2_SL_5S {get;set;}

        /// <summary>
        /// Desc:直供高炉皮带1启停信号（SJK-1皮带） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_DS_BFES_DELT1_SL_5S {get;set;}

        /// <summary>
        /// Desc:铺底料仓称重 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_BED_MATERAL_W_5S {get;set;}

        /// <summary>
        /// Desc:筛一启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_SCREEN_SL_1_5S {get;set;}

        /// <summary>
        /// Desc:筛二启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_SCREEN_SL_2_5S {get;set;}

        /// <summary>
        /// Desc:筛三启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool T_SCREEN_SL_3_5S {get;set;}

        /// <summary>
        /// Desc:布料厚度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? T_THICK_SP_5S {get;set;}

    }
}
