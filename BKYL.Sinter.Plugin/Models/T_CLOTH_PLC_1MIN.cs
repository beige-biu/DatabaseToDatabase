using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class T_CLOTH_PLC_1MIN
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:梭式布料器行走电机转速/频率(S-6皮带) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SHUTTLE_RATE {get;set;}

        /// <summary>
        /// Desc:梭式布料器皮带启停信号(S-6皮带) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool C_SHUTTLE_S_S {get;set;}

        /// <summary>
        /// Desc:梭式布料器皮带启动超时故障信号(S-6皮带) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool C_OVERTIME {get;set;}

        /// <summary>
        /// Desc:梭式布料行走电机急停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool C_E_STOP_S {get;set;}

        /// <summary>
        /// Desc:梭式布料行走电机正转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool C_FWD_S {get;set;}

        /// <summary>
        /// Desc:梭式布料行走电机反转信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool C_REV_S {get;set;}

        /// <summary>
        /// Desc:梭式布料器皮带跑偏信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool C_BELT_DEV_S {get;set;}

        /// <summary>
        /// Desc:梭式布料器皮带打滑信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool C_BELT_SLIP_S {get;set;}

        /// <summary>
        /// Desc:梭式布料器皮带热过载保护信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool C_BELT_OVER_S {get;set;}

        /// <summary>
        /// Desc:梭式布料器皮带撕裂信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool C_BELT_TEARING_S {get;set;}

        /// <summary>
        /// Desc:进梭式布料器S-1皮带启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool C_IN_SK_S {get;set;}

        /// <summary>
        /// Desc:进梭式布料器S-1皮带电机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_IN_SK_SPEED {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_1 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_2 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool SPARE_3 {get;set;}

        /// <summary>
        /// Desc:矿槽仓位自动控制投入退出按钮（1投入，0退出） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_LEVEL_AUTO {get;set;}

        /// <summary>
        /// Desc:混合料仓仓位(取平均) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_BLEND_LEVEL {get;set;}

        /// <summary>
        /// Desc:混合料仓目标仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_BLEND_TAR_LEVEL {get;set;}

        /// <summary>
        /// Desc:混合料温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_BLEND_TE {get;set;}

        /// <summary>
        /// Desc:混合料温度低限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_BLEND_TE_LOW {get;set;}

        /// <summary>
        /// Desc:混合料温度高限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_BLEND_TE_HIGH {get;set;}

        /// <summary>
        /// Desc:混合料仓仓位低限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_BLEND_W_LOW {get;set;}

        /// <summary>
        /// Desc:混合料仓仓位高限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_BLEND_W_HIGH {get;set;}

        /// <summary>
        /// Desc:蒸汽量(取平均) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_STEAM_AVG {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_4 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_5 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_6 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_7 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool SPARE_8 {get;set;}

        /// <summary>
        /// Desc:圆辊给料机启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_STICK_SL {get;set;}

        /// <summary>
        /// Desc:圆辊给料机设定频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_STICK_SP {get;set;}

        /// <summary>
        /// Desc:圆辊给料机反馈频率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_STICK_PV {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_9 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_10 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_11 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_12 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_13 {get;set;}

        /// <summary>
        /// Desc:九辊布料器A频率设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_N_STICK_A_SP {get;set;}

        /// <summary>
        /// Desc:九辊布料器B频率设定 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_N_STICK_B_SP {get;set;}

        /// <summary>
        /// Desc:九辊布料器A频率反馈 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_N_STICK_A_PV {get;set;}

        /// <summary>
        /// Desc:九辊布料器B频率反馈 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_N_STICK_B_PV {get;set;}

        /// <summary>
        /// Desc:九辊布料器A启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool C_N_STICK_A_SL {get;set;}

        /// <summary>
        /// Desc:九辊布料器B启停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool C_N_STICK_B_SL {get;set;}

        /// <summary>
        /// Desc:九辊布料器A启动超时故障信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool C_N_STICK_OVERTIME_A {get;set;}

        /// <summary>
        /// Desc:九辊布料器B启动超时故障信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool C_N_STICK_OVERTIME_B {get;set;}

        /// <summary>
        /// Desc:九辊布料器A急停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool C_N_STICK_E_STOP_A {get;set;}

        /// <summary>
        /// Desc:九辊布料器B急停信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool C_N_STICK_E_STOP_B {get;set;}

        /// <summary>
        /// Desc:九辊布料器A电机热过载信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_N_STICK_OVER_A {get;set;}

        /// <summary>
        /// Desc:九辊布料器B电机热过载信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_N_STICK_OVER_B {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_14 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_15 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_16 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_17 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_18 {get;set;}

        /// <summary>
        /// Desc:铺底料仓称重 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_BED_MATERAL_W {get;set;}

        /// <summary>
        /// Desc:铺底料厚度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_BED_MATERAL_TH {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_19 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_20 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_21 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_22 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_23 {get;set;}

        /// <summary>
        /// Desc:大闸门设定开度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_BIG_SG_O_SP {get;set;}

        /// <summary>
        /// Desc:大闸门反馈开度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_BIG_SG_O_PV {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_24 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_25 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_26 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_27 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_28 {get;set;}

        /// <summary>
        /// Desc:1#小闸门设定开度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_O_SP_1 {get;set;}

        /// <summary>
        /// Desc:2#小闸门设定开度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_O_SP_2 {get;set;}

        /// <summary>
        /// Desc:3#小闸门设定开度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_O_SP_3 {get;set;}

        /// <summary>
        /// Desc:4#小闸门设定开度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_O_SP_4 {get;set;}

        /// <summary>
        /// Desc:5#小闸门设定开度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_O_SP_5 {get;set;}

        /// <summary>
        /// Desc:6#小闸门设定开度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_O_SP_6 {get;set;}

        /// <summary>
        /// Desc:7#小闸门设定开度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_O_SP_7 {get;set;}

        /// <summary>
        /// Desc:1#小闸门反馈开度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_O_PV_1 {get;set;}

        /// <summary>
        /// Desc:2#小闸门反馈开度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_O_PV_2 {get;set;}

        /// <summary>
        /// Desc:3#小闸门反馈开度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_O_PV_3 {get;set;}

        /// <summary>
        /// Desc:4#小闸门反馈开度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_O_PV_4 {get;set;}

        /// <summary>
        /// Desc:5#小闸门反馈开度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_O_PV_5 {get;set;}

        /// <summary>
        /// Desc:6#小闸门反馈开度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_O_PV_6 {get;set;}

        /// <summary>
        /// Desc:7#小闸门反馈开度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_O_PV_7 {get;set;}

        /// <summary>
        /// Desc:1#小闸门设定布料厚度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_SP_1 {get;set;}

        /// <summary>
        /// Desc:2#小闸门设定布料厚度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_SP_2 {get;set;}

        /// <summary>
        /// Desc:3#小闸门设定布料厚度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_SP_3 {get;set;}

        /// <summary>
        /// Desc:4#小闸门设定布料厚度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_SP_4 {get;set;}

        /// <summary>
        /// Desc:5#小闸门设定布料厚度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_SP_5 {get;set;}

        /// <summary>
        /// Desc:6#小闸门设定布料厚度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_SP_6 {get;set;}

        /// <summary>
        /// Desc:7#小闸门设定布料厚度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_SP_7 {get;set;}

        /// <summary>
        /// Desc:1#小闸门实际布料厚度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_PV_1 {get;set;}

        /// <summary>
        /// Desc:2#小闸门实际布料厚度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_PV_2 {get;set;}

        /// <summary>
        /// Desc:3#小闸门实际布料厚度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_PV_3 {get;set;}

        /// <summary>
        /// Desc:4#小闸门实际布料厚度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_PV_4 {get;set;}

        /// <summary>
        /// Desc:5#小闸门实际布料厚度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_PV_5 {get;set;}

        /// <summary>
        /// Desc:6#小闸门实际布料厚度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_PV_6 {get;set;}

        /// <summary>
        /// Desc:7#小闸门实际布料厚度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_PV_7 {get;set;}

        /// <summary>
        /// Desc:1#小闸门自动控制投入开关（1-投入；0-退出） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? C_SMALL_SG_AUTO_1 {get;set;}

        /// <summary>
        /// Desc:2#小闸门自动控制投入开关（1-投入；0-退出） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? C_SMALL_SG_AUTO_2 {get;set;}

        /// <summary>
        /// Desc:3#小闸门自动控制投入开关（1-投入；0-退出） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? C_SMALL_SG_AUTO_3 {get;set;}

        /// <summary>
        /// Desc:4#小闸门自动控制投入开关（1-投入；0-退出） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? C_SMALL_SG_AUTO_4 {get;set;}

        /// <summary>
        /// Desc:5#小闸门自动控制投入开关（1-投入；0-退出） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? C_SMALL_SG_AUTO_5 {get;set;}

        /// <summary>
        /// Desc:6#小闸门自动控制投入开关（1-投入；0-退出） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? C_SMALL_SG_AUTO_6 {get;set;}

        /// <summary>
        /// Desc:7#小闸门自动控制投入开关（1-投入；1-退出） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? C_SMALL_SG_AUTO_7 {get;set;}

        /// <summary>
        /// Desc:1#小闸门反馈布料厚度(雷达料位计检测)取平均 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_PV_1_AVG {get;set;}

        /// <summary>
        /// Desc:2#小闸门反馈布料厚度(雷达料位计检测)取平均 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_PV_2_AVG {get;set;}

        /// <summary>
        /// Desc:3#小闸门反馈布料厚度(雷达料位计检测)取平均 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_PV_3_AVG {get;set;}

        /// <summary>
        /// Desc:4#小闸门反馈布料厚度(雷达料位计检测)取平均 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_PV_4_AVG {get;set;}

        /// <summary>
        /// Desc:5#小闸门反馈布料厚度(雷达料位计检测)取平均 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_PV_5_AVG {get;set;}

        /// <summary>
        /// Desc:6#小闸门反馈布料厚度(雷达料位计检测)取平均 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_PV_6_AVG {get;set;}

        /// <summary>
        /// Desc:7#小闸门反馈布料厚度(雷达料位计检测)取平均 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SMALL_SG_PV_7_AVG {get;set;}

        /// <summary>
        /// Desc:布料厚度反馈值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_THICK_PV {get;set;}

        /// <summary>
        /// Desc:布料厚度设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_THICK_SP {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_30 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_31 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_32 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SPARE_33 {get;set;}

    }
}
