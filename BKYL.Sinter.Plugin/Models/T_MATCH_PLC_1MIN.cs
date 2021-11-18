using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class T_MATCH_PLC_1MIN
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:1#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_W_1 {get;set;}

        /// <summary>
        /// Desc:2#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_W_2 {get;set;}

        /// <summary>
        /// Desc:3#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_W_3 {get;set;}

        /// <summary>
        /// Desc:4#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_W_4 {get;set;}

        /// <summary>
        /// Desc:5#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_W_5 {get;set;}

        /// <summary>
        /// Desc:6#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_W_6 {get;set;}

        /// <summary>
        /// Desc:7#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_W_7 {get;set;}

        /// <summary>
        /// Desc:8#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_W_8 {get;set;}

        /// <summary>
        /// Desc:9#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_W_9 {get;set;}

        /// <summary>
        /// Desc:10#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_W_10 {get;set;}

        /// <summary>
        /// Desc:11#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_W_11 {get;set;}

        /// <summary>
        /// Desc:12#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_W_12 {get;set;}

        /// <summary>
        /// Desc:13#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_W_13 {get;set;}

        /// <summary>
        /// Desc:14#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_W_14 {get;set;}

        /// <summary>
        /// Desc:15#配料仓仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_W_15 {get;set;}

        /// <summary>
        /// Desc:1#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_CODE_1 {get;set;}

        /// <summary>
        /// Desc:2#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_CODE_2 {get;set;}

        /// <summary>
        /// Desc:3#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_CODE_3 {get;set;}

        /// <summary>
        /// Desc:4#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_CODE_4 {get;set;}

        /// <summary>
        /// Desc:5#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_CODE_5 {get;set;}

        /// <summary>
        /// Desc:6#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_CODE_6 {get;set;}

        /// <summary>
        /// Desc:7#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_CODE_7 {get;set;}

        /// <summary>
        /// Desc:8#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_CODE_8 {get;set;}

        /// <summary>
        /// Desc:9#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_CODE_9 {get;set;}

        /// <summary>
        /// Desc:10#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_CODE_10 {get;set;}

        /// <summary>
        /// Desc:11#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_CODE_11 {get;set;}

        /// <summary>
        /// Desc:12#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_CODE_12 {get;set;}

        /// <summary>
        /// Desc:13#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_CODE_13 {get;set;}

        /// <summary>
        /// Desc:14#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_CODE_14 {get;set;}

        /// <summary>
        /// Desc:15#配料仓料种(编码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_CODE_15 {get;set;}

        /// <summary>
        /// Desc:1#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SP_W_1 {get;set;}

        /// <summary>
        /// Desc:2#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SP_W_2 {get;set;}

        /// <summary>
        /// Desc:3#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SP_W_3 {get;set;}

        /// <summary>
        /// Desc:4#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SP_W_4 {get;set;}

        /// <summary>
        /// Desc:5#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SP_W_5 {get;set;}

        /// <summary>
        /// Desc:6#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SP_W_6 {get;set;}

        /// <summary>
        /// Desc:7#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SP_W_7 {get;set;}

        /// <summary>
        /// Desc:8#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SP_W_8 {get;set;}

        /// <summary>
        /// Desc:9#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SP_W_9 {get;set;}

        /// <summary>
        /// Desc:10#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SP_W_10 {get;set;}

        /// <summary>
        /// Desc:11#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SP_W_11 {get;set;}

        /// <summary>
        /// Desc:12#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SP_W_12 {get;set;}

        /// <summary>
        /// Desc:13#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SP_W_13 {get;set;}

        /// <summary>
        /// Desc:14#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SP_W_14 {get;set;}

        /// <summary>
        /// Desc:15#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SP_W_15 {get;set;}

        /// <summary>
        /// Desc:16#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SP_W_16 {get;set;}

        /// <summary>
        /// Desc:17#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SP_W_17 {get;set;}

        /// <summary>
        /// Desc:18#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SP_W_18 {get;set;}

        /// <summary>
        /// Desc:19#下料口设定下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SP_W_19 {get;set;}

        /// <summary>
        /// Desc:1#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ACTUAL_W_1 {get;set;}

        /// <summary>
        /// Desc:2#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ACTUAL_W_2 {get;set;}

        /// <summary>
        /// Desc:3#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ACTUAL_W_3 {get;set;}

        /// <summary>
        /// Desc:4#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ACTUAL_W_4 {get;set;}

        /// <summary>
        /// Desc:5#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ACTUAL_W_5 {get;set;}

        /// <summary>
        /// Desc:6#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ACTUAL_W_6 {get;set;}

        /// <summary>
        /// Desc:7#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ACTUAL_W_7 {get;set;}

        /// <summary>
        /// Desc:8#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ACTUAL_W_8 {get;set;}

        /// <summary>
        /// Desc:9#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ACTUAL_W_9 {get;set;}

        /// <summary>
        /// Desc:10#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ACTUAL_W_10 {get;set;}

        /// <summary>
        /// Desc:11#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ACTUAL_W_11 {get;set;}

        /// <summary>
        /// Desc:12#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ACTUAL_W_12 {get;set;}

        /// <summary>
        /// Desc:13#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ACTUAL_W_13 {get;set;}

        /// <summary>
        /// Desc:14#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ACTUAL_W_14 {get;set;}

        /// <summary>
        /// Desc:15#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ACTUAL_W_15 {get;set;}

        /// <summary>
        /// Desc:16#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ACTUAL_W_16 {get;set;}

        /// <summary>
        /// Desc:17#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ACTUAL_W_17 {get;set;}

        /// <summary>
        /// Desc:18#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ACTUAL_W_18 {get;set;}

        /// <summary>
        /// Desc:19#下料口实际下料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ACTUAL_W_19 {get;set;}

        /// <summary>
        /// Desc:1#下料口给料盘频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DISC_FQ_1 {get;set;}

        /// <summary>
        /// Desc:2#下料口给料盘频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DISC_FQ_2 {get;set;}

        /// <summary>
        /// Desc:3#下料口给料盘频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DISC_FQ_3 {get;set;}

        /// <summary>
        /// Desc:4#下料口给料盘频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DISC_FQ_4 {get;set;}

        /// <summary>
        /// Desc:5#下料口给料盘频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DISC_FQ_5 {get;set;}

        /// <summary>
        /// Desc:6#下料口给料盘频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DISC_FQ_6 {get;set;}

        /// <summary>
        /// Desc:7#下料口给料盘频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DISC_FQ_7 {get;set;}

        /// <summary>
        /// Desc:8#下料口给料盘频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DISC_FQ_8 {get;set;}

        /// <summary>
        /// Desc:9#下料口给料盘频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DISC_FQ_9 {get;set;}

        /// <summary>
        /// Desc:10#下料口给料盘频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DISC_FQ_10 {get;set;}

        /// <summary>
        /// Desc:11#下料口给料盘频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DISC_FQ_11 {get;set;}

        /// <summary>
        /// Desc:12#下料口给料盘频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DISC_FQ_12 {get;set;}

        /// <summary>
        /// Desc:13#下料口给料盘频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DISC_FQ_13 {get;set;}

        /// <summary>
        /// Desc:14#下料口给料盘频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DISC_FQ_14 {get;set;}

        /// <summary>
        /// Desc:15#下料口给料盘频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DISC_FQ_15 {get;set;}

        /// <summary>
        /// Desc:16#下料口给料盘频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DISC_FQ_16 {get;set;}

        /// <summary>
        /// Desc:17#下料口给料盘频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DISC_FQ_17 {get;set;}

        /// <summary>
        /// Desc:18#下料口给料盘频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DISC_FQ_18 {get;set;}

        /// <summary>
        /// Desc:19#下料口给料盘频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_DISC_FQ_19 {get;set;}

        /// <summary>
        /// Desc:1#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_SL_1 {get;set;}

        /// <summary>
        /// Desc:2#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_SL_2 {get;set;}

        /// <summary>
        /// Desc:3#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_SL_3 {get;set;}

        /// <summary>
        /// Desc:4#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_SL_4 {get;set;}

        /// <summary>
        /// Desc:5#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_SL_5 {get;set;}

        /// <summary>
        /// Desc:6#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_SL_6 {get;set;}

        /// <summary>
        /// Desc:7#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_SL_7 {get;set;}

        /// <summary>
        /// Desc:8#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_SL_8 {get;set;}

        /// <summary>
        /// Desc:9#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_SL_9 {get;set;}

        /// <summary>
        /// Desc:10#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_SL_10 {get;set;}

        /// <summary>
        /// Desc:11#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_SL_11 {get;set;}

        /// <summary>
        /// Desc:12#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_SL_12 {get;set;}

        /// <summary>
        /// Desc:13#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_SL_13 {get;set;}

        /// <summary>
        /// Desc:14#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_SL_14 {get;set;}

        /// <summary>
        /// Desc:15#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_SL_15 {get;set;}

        /// <summary>
        /// Desc:16#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_SL_16 {get;set;}

        /// <summary>
        /// Desc:17#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_SL_17 {get;set;}

        /// <summary>
        /// Desc:18#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_SL_18 {get;set;}

        /// <summary>
        /// Desc:19#下料口启停信号（1-启用；0-禁用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_SL_19 {get;set;}

        /// <summary>
        /// Desc:总料量设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_TOTAL_SP_W {get;set;}

        /// <summary>
        /// Desc:总料量实际值(平均值) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_TOTAL_PV_W {get;set;}

        /// <summary>
        /// Desc:1#配料仓下料口皮带秤累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_ACC_1 {get;set;}

        /// <summary>
        /// Desc:2#配料仓下料口皮带秤累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_ACC_2 {get;set;}

        /// <summary>
        /// Desc:3#配料仓下料口皮带秤累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_ACC_3 {get;set;}

        /// <summary>
        /// Desc:4#配料仓下料口皮带秤累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_ACC_4 {get;set;}

        /// <summary>
        /// Desc:5#配料仓下料口皮带秤累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_ACC_5 {get;set;}

        /// <summary>
        /// Desc:6#配料仓下料口皮带秤累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_ACC_6 {get;set;}

        /// <summary>
        /// Desc:7#配料仓下料口皮带秤累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_ACC_7 {get;set;}

        /// <summary>
        /// Desc:8#配料仓下料口皮带秤累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_ACC_8 {get;set;}

        /// <summary>
        /// Desc:9#配料仓下料口皮带秤累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_ACC_9 {get;set;}

        /// <summary>
        /// Desc:10#配料仓下料口皮带秤累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_ACC_10 {get;set;}

        /// <summary>
        /// Desc:11#配料仓下料口皮带秤累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_ACC_11 {get;set;}

        /// <summary>
        /// Desc:12#配料仓下料口皮带秤累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_ACC_12 {get;set;}

        /// <summary>
        /// Desc:13#配料仓下料口皮带秤累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_ACC_13 {get;set;}

        /// <summary>
        /// Desc:14#配料仓下料口皮带秤累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_ACC_14 {get;set;}

        /// <summary>
        /// Desc:15#配料仓下料口皮带秤累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_ACC_15 {get;set;}

        /// <summary>
        /// Desc:16#配料仓下料口皮带秤累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_ACC_16 {get;set;}

        /// <summary>
        /// Desc:17#配料仓下料口皮带秤累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_ACC_17 {get;set;}

        /// <summary>
        /// Desc:18#配料仓下料口皮带秤累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_ACC_18 {get;set;}

        /// <summary>
        /// Desc:19#配料仓下料口皮带秤累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_ACC_19 {get;set;}

        /// <summary>
        /// Desc:P-7皮带（配料总皮带）运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool M_BELT_SL_P_7 {get;set;}

        /// <summary>
        /// Desc:P-7皮带（配料总皮带）电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SP_P_7 {get;set;}

        /// <summary>
        /// Desc:1#下料口皮带运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_BELT_SL_1 {get;set;}

        /// <summary>
        /// Desc:1#下料口皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SPEED_1 {get;set;}

        /// <summary>
        /// Desc:2#下料口皮带运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_BELT_SL_2 {get;set;}

        /// <summary>
        /// Desc:2#下料口皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SPEED_2 {get;set;}

        /// <summary>
        /// Desc:3#下料口皮带运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_BELT_SL_3 {get;set;}

        /// <summary>
        /// Desc:3#下料口皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SPEED_3 {get;set;}

        /// <summary>
        /// Desc:4#下料口皮带运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_BELT_SL_4 {get;set;}

        /// <summary>
        /// Desc:4#下料口皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SPEED_4 {get;set;}

        /// <summary>
        /// Desc:5#下料口皮带运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_BELT_SL_5 {get;set;}

        /// <summary>
        /// Desc:5#下料口皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SPEED_5 {get;set;}

        /// <summary>
        /// Desc:6#下料口皮带运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_BELT_SL_6 {get;set;}

        /// <summary>
        /// Desc:6#下料口皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SPEED_6 {get;set;}

        /// <summary>
        /// Desc:7#下料口皮带运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_BELT_SL_7 {get;set;}

        /// <summary>
        /// Desc:7#下料口皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SPEED_7 {get;set;}

        /// <summary>
        /// Desc:8#下料口皮带运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_BELT_SL_8 {get;set;}

        /// <summary>
        /// Desc:8#下料口皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SPEED_8 {get;set;}

        /// <summary>
        /// Desc:9#下料口皮带运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_BELT_SL_9 {get;set;}

        /// <summary>
        /// Desc:9#下料口皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SPEED_9 {get;set;}

        /// <summary>
        /// Desc:10#下料口皮带运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_BELT_SL_10 {get;set;}

        /// <summary>
        /// Desc:10#下料口皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SPEED_10 {get;set;}

        /// <summary>
        /// Desc:11#下料口皮带运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_BELT_SL_11 {get;set;}

        /// <summary>
        /// Desc:11#下料口皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SPEED_11 {get;set;}

        /// <summary>
        /// Desc:12#下料口皮带运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_BELT_SL_12 {get;set;}

        /// <summary>
        /// Desc:12#下料口皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SPEED_12 {get;set;}

        /// <summary>
        /// Desc:13#下料口皮带运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_BELT_SL_13 {get;set;}

        /// <summary>
        /// Desc:13#下料口皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SPEED_13 {get;set;}

        /// <summary>
        /// Desc:14#下料口皮带运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_BELT_SL_14 {get;set;}

        /// <summary>
        /// Desc:14#下料口皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SPEED_14 {get;set;}

        /// <summary>
        /// Desc:15#下料口皮带运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_BELT_SL_15 {get;set;}

        /// <summary>
        /// Desc:15#下料口皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SPEED_15 {get;set;}

        /// <summary>
        /// Desc:13#下料口皮带运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_BELT_SL_16 {get;set;}

        /// <summary>
        /// Desc:16#下料口皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SPEED_16 {get;set;}

        /// <summary>
        /// Desc:17#下料口皮带运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_BELT_SL_17 {get;set;}

        /// <summary>
        /// Desc:17#下料口皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SPEED_17 {get;set;}

        /// <summary>
        /// Desc:18#下料口皮带运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_BELT_SL_18 {get;set;}

        /// <summary>
        /// Desc:18#下料口皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SPEED_18 {get;set;}

        /// <summary>
        /// Desc:19#下料口皮带运转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_BELT_SL_19 {get;set;}

        /// <summary>
        /// Desc:19#下料口皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_BELT_SPEED_19 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double SINCAL_MIX_SP_LOT {get;set;}
        public int ISQY { get; set; }
        ///////// <summary>
        ///////// 判断下料口是否启用
        ///////// 
        ///////// 
        //


        ///// <summary>
        ///// 1#下料口实际下料量(t/h)
        ///// </summary>
        //public Double M_ACTUAL_W_1 { get; set; }
        //public Double M_ACTUAL_W_T_2 { get; set; }
        //public Double M_ACTUAL_W_T_3 { get; set; }
        //public Double M_ACTUAL_W_T_4 { get; set; }
        //public Double M_ACTUAL_W_T_5 { get; set; }
        //public Double M_ACTUAL_W_T_6 { get; set; }
        //public Double M_ACTUAL_W_T_7 { get; set; }
        //public Double M_ACTUAL_W_T_8 { get; set; }
        //public Double M_ACTUAL_W_T_9 { get; set; }

        //public Double M_ACTUAL_W_10 { get; set; }
        //public Double M_ACTUAL_W_T_11 { get; set; }
        //public Double M_ACTUAL_W_T_12 { get; set; }
        //public Double M_ACTUAL_W_T_13 { get; set; }
        //public Double M_ACTUAL_W_T_14 { get; set; }
        //public Double M_ACTUAL_W_T_15 { get; set; }
        //public Double M_ACTUAL_W_T_16 { get; set; }
        //public Double M_ACTUAL_W_T_17 { get; set; }
        //public Double M_ACTUAL_W_T_18 { get; set; }
        //public Double M_ACTUAL_W_T_19 { get; set; }
    }
}
