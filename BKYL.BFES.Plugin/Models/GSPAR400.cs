using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class GSPAR400
    {
        
        /// <summary>
        /// Desc:总压差变化1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL_DP1 {get;set;}

        /// <summary>
        /// Desc:总压差变化2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL_DP2 {get;set;}

        /// <summary>
        /// Desc:总压差变化3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL_DP3 {get;set;}

        /// <summary>
        /// Desc:总压差变化4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL_DP4 {get;set;}

        /// <summary>
        /// Desc:风量变化1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_BV1 {get;set;}

        /// <summary>
        /// Desc:风量变化2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_BV2 {get;set;}

        /// <summary>
        /// Desc:风量变化3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_BV3 {get;set;}

        /// <summary>
        /// Desc:风量变化4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_BV4 {get;set;}

        /// <summary>
        /// Desc:料速变化1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL_VB1 {get;set;}

        /// <summary>
        /// Desc:料速变化2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL_VB2 {get;set;}

        /// <summary>
        /// Desc:顶温变化1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_TTOP1 {get;set;}

        /// <summary>
        /// Desc:顶温变化2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_TTOP2 {get;set;}

        /// <summary>
        /// Desc:顶温变化3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_TTOP3 {get;set;}

        /// <summary>
        /// Desc:煤气利用率1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL_COR1 {get;set;}

        /// <summary>
        /// Desc:煤气利用率2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL_COR2 {get;set;}

        /// <summary>
        /// Desc:铁水温度界限值1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_HMTH_1 {get;set;}

        /// <summary>
        /// Desc:铁水温度界限值2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_HMTH_2 {get;set;}

        /// <summary>
        /// Desc:铁水温度界限值3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_HMTH_3 {get;set;}

        /// <summary>
        /// Desc:铁水温度界限值4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_HMTH_4 {get;set;}

        /// <summary>
        /// Desc:铁水温度界限值5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_HMTH_5 {get;set;}

        /// <summary>
        /// Desc:铁水温度界限值6 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_HMTH_6 {get;set;}

        /// <summary>
        /// Desc:风温动作1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_BT403C01 {get;set;}

        /// <summary>
        /// Desc:风温动作2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_BT403C02 {get;set;}

        /// <summary>
        /// Desc:风温动作3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_BT403C03 {get;set;}

        /// <summary>
        /// Desc:风温动作4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_BT403C04 {get;set;}

        /// <summary>
        /// Desc:附加焦动作1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_BC403C01 {get;set;}

        /// <summary>
        /// Desc:风量动作1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_BV403C01 {get;set;}

        /// <summary>
        /// Desc:风量动作2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_BV403C02 {get;set;}

        /// <summary>
        /// Desc:喷煤动作1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL_PCI403C01 {get;set;}

        /// <summary>
        /// Desc:喷煤动作2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL_PCI403C02 {get;set;}

        /// <summary>
        /// Desc:喷煤动作3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL_PCI403C03 {get;set;}

        /// <summary>
        /// Desc:喷煤动作4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL_PCI403C04 {get;set;}

        /// <summary>
        /// Desc:焦比动作1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_CR403C01 {get;set;}

        /// <summary>
        /// Desc:风口前理论燃烧温度界限1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_FKLR1 {get;set;}

        /// <summary>
        /// Desc:风口前理论燃烧温度界限2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_FKLR2 {get;set;}

        /// <summary>
        /// Desc:风口前理论燃烧温度界限3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_FKLR3 {get;set;}

        /// <summary>
        /// Desc:出铁开始后不动作时间界限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_ACT_EXPIRY_TIME {get;set;}

        /// <summary>
        /// Desc:矿石动作1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL_OR403C01 {get;set;}

        /// <summary>
        /// Desc:风口前理论燃烧温度30分钟平均值界限1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL1_AVGT1 {get;set;}

        /// <summary>
        /// Desc:风口前理论燃烧温度30分钟平均值界限2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL1_AVGT2 {get;set;}

        /// <summary>
        /// Desc:透气性指数变化1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL_ITZ1 {get;set;}

        /// <summary>
        /// Desc:透气性指数变化2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL_ITZ2 {get;set;}

        /// <summary>
        /// Desc:透气性指数变化3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL_ITZ3 {get;set;}

        /// <summary>
        /// Desc:透气性指数变化4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL_ITZ4 {get;set;}

        /// <summary>
        /// Desc:CO/CO2上升的界限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_H2_RISE_CC2D3 {get;set;}

        /// <summary>
        /// Desc:煤气H2利用率变化的界限值1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_H2_RISE_ETAH2D1_1 {get;set;}

        /// <summary>
        /// Desc:煤气H2利用率变化的界限值2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_H2_RISE_ETAH2D2_1 {get;set;}

        /// <summary>
        /// Desc:插入或更新表的时标 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:预报不同炉温趋势的间隔时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL1_XH {get;set;}

        /// <summary>
        /// Desc:透气性指数变化参数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL1_ITZ_PAR {get;set;}

        /// <summary>
        /// Desc:料速变化参数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL1_VB_PAR {get;set;}

        /// <summary>
        /// Desc:顶温变化参数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL1_TTOP_PAR {get;set;}

        /// <summary>
        /// Desc:喷煤动作1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL1_PCI404C01 {get;set;}

        /// <summary>
        /// Desc:风量动作1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL1_BV404C01 {get;set;}

        /// <summary>
        /// Desc:风温动作1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_THER_MODEL1_BT404C01 {get;set;}

        /// <summary>
        /// Desc:炉料燃料比偏差参数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_THER_MODEL1_DF1 {get;set;}

        /// <summary>
        /// Desc:氢含量上升湿度变化阈值1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_H2_RISE_MOI15D1 {get;set;}

        /// <summary>
        /// Desc:氢含量上升氢含量偏差阈值1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_H2_RISE_H2D1_1 {get;set;}

        /// <summary>
        /// Desc:上一条CO/CO2上升的界限值1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_H2_RISE_CC2D3B1 {get;set;}

        /// <summary>
        /// Desc:氮气含量阈值1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_N2_RISE_MOI15 {get;set;}

    }
}
