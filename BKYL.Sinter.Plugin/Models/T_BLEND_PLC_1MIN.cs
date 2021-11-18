using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class T_BLEND_PLC_1MIN
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:一混前皮带启停信号(皮带1H-1) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool B_1M_PRE_BELT_SL {get;set;}

        /// <summary>
        /// Desc:一混前转运皮带秤值（用于加水开停机判断）(皮带1H-1) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_PRE_BELT_W_1H_1 {get;set;}

        /// <summary>
        /// Desc:一混前皮带电机转速(皮带1H-1) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_PRE_BELT_SPEED {get;set;}

        /// <summary>
        /// Desc:除尘灰仓加水点1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_DUST_WARTER_1 {get;set;}

        /// <summary>
        /// Desc:除尘灰仓加水点2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_DUST_WARTER_2 {get;set;}

        /// <summary>
        /// Desc:脱硫灰仓加水点1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_TAKE_S_WARTER_1 {get;set;}

        /// <summary>
        /// Desc:脱硫灰仓加水点2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_TAKE_S_WARTER_2 {get;set;}

        /// <summary>
        /// Desc:一混设备启停信号（启-1；停-0） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool B_1M_SL {get;set;}

        /// <summary>
        /// Desc:一混加水自动投入信号（1-自动，0-手动） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool B_1M_AUT_WATER {get;set;}

        /// <summary>
        /// Desc:一混目标水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_TARGET_WATER {get;set;}

        /// <summary>
        /// Desc:一混加水流量设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_FT_SP {get;set;}

        /// <summary>
        /// Desc:一混加水流量反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_FT_PV {get;set;}

        /// <summary>
        /// Desc:一混加水阀开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_SP {get;set;}

        /// <summary>
        /// Desc:一混加水阀开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_PV {get;set;}

        /// <summary>
        /// Desc:一混水管压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_PT {get;set;}

        /// <summary>
        /// Desc:一混混合机转速/频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_MIXER_RATE {get;set;}

        /// <summary>
        /// Desc:一混1#热水泵运行信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool B_1M_WORK_1 {get;set;}

        /// <summary>
        /// Desc:一混1#热水泵设定频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_SP_FQ_1 {get;set;}

        /// <summary>
        /// Desc:一混1#热水泵反馈频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_PV_FQ_1 {get;set;}

        /// <summary>
        /// Desc:一混2#热水泵运行信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool B_1M_WORK_2 {get;set;}

        /// <summary>
        /// Desc:一混2#热水泵设定频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_SP_FQ_2 {get;set;}

        /// <summary>
        /// Desc:一混2#热水泵反馈频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_PV_FQ_2 {get;set;}

        /// <summary>
        /// Desc:一混热水供水泵1入口热水温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_TE_1 {get;set;}

        /// <summary>
        /// Desc:一混热水供水泵2入口热水温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_TE_2 {get;set;}

        /// <summary>
        /// Desc:一混热水供水泵3入口热水温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_TE_3 {get;set;}

        /// <summary>
        /// Desc:一混后皮带启停信号(皮带2H-1) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool B_1M_NEX_BELT_B_E_2H_1 {get;set;}

        /// <summary>
        /// Desc:一混后皮带电机转速(皮带2H-1) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_NEX_BELT_SPEED_2H_1 {get;set;}

        /// <summary>
        /// Desc:一混后皮带秤值(皮带2H-1) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_NEX_BELT_W_2H_1 {get;set;}

        /// <summary>
        /// Desc:二混设备启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool B_2M_SL {get;set;}

        /// <summary>
        /// Desc:二混加水自动投入信号（1-自动，0-手动） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool B_2M_AUT_WATER {get;set;}

        /// <summary>
        /// Desc:二混后转运皮带秤值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_2M_BELT_VALUE {get;set;}

        /// <summary>
        /// Desc:二混目标水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_2M_TARGET_WATER {get;set;}

        /// <summary>
        /// Desc:二混加水流量设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_2M_FLOW_SP {get;set;}

        /// <summary>
        /// Desc:二混加水流量反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_2M_FLOW_PV {get;set;}

        /// <summary>
        /// Desc:二混加水阀开度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_2M_SP {get;set;}

        /// <summary>
        /// Desc:二混加水阀开度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_2M_PV {get;set;}

        /// <summary>
        /// Desc:二混水管压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_2M_PT {get;set;}

        /// <summary>
        /// Desc:二混混合机转速/频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_2M_MIXER_RATE {get;set;}

        /// <summary>
        /// Desc:二混1#热水泵运行信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool B_2M_WORK_1 {get;set;}

        /// <summary>
        /// Desc:二混1#热水泵设定频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_2M_SP_FQ_1 {get;set;}

        /// <summary>
        /// Desc:二混1#热水泵反馈频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_2M_PV_FQ_1 {get;set;}

        /// <summary>
        /// Desc:二混2#热水泵运行信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool B_2M_WORK_2 {get;set;}

        /// <summary>
        /// Desc:二混2#热水泵设定频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_2M_SP_FQ_2 {get;set;}

        /// <summary>
        /// Desc:二混2#热水泵反馈频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_2M_PV_FQ_2 {get;set;}

        /// <summary>
        /// Desc:二混热水供水泵1入口热水温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_2M_TE_1 {get;set;}

        /// <summary>
        /// Desc:二混热水供水泵2入口热水温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_2M_TE_2 {get;set;}

        /// <summary>
        /// Desc:二混热水供水泵3入口热水温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_2M_TE_3 {get;set;}

        /// <summary>
        /// Desc:二混后皮带启停信号(皮带Z2-1) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool B_2M_NEX_BELT_2H_1_S {get;set;}

        /// <summary>
        /// Desc:二混后皮带电机转速(皮带Z2-1) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_2M_NEX_BELT_2H_1_SPEED {get;set;}

        /// <summary>
        /// Desc:煤粉仓在线水分仪检测值1（取平均值） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_WATER_VALUE_1_AVG {get;set;}

        /// <summary>
        /// Desc:煤粉仓在线水分仪检测值2（取平均值） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_WATER_VALUE_2_AVG {get;set;}

        /// <summary>
        /// Desc:一混前水分仪检测值（取平均值） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_PRE_WATER_AVG {get;set;}

        /// <summary>
        /// Desc:一混后混合料水分检测（取平均值） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_1M_NEX_WATER_AVG {get;set;}

        /// <summary>
        /// Desc:二混后混合料水分检测（取平均值） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? B_2M_NEX_WATER_AVG {get;set;}

    }
}
