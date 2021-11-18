using System;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Models
{
    public class L2_MATERIAL_ANA
    {
        
        /// <summary>
        /// Desc:插入时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:班次 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? M_SHIFT {get;set;}

        /// <summary>
        /// Desc:原料名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_L2_NAME {get;set;}

        /// <summary>
        /// Desc:原料二级编码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string M_L2_CODE {get;set;}

        /// <summary>
        /// Desc:原料三级编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_L3_CODE {get;set;}

        /// <summary>
        /// Desc:类型 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FLAG {get;set;}

        /// <summary>
        /// Desc:Tfe 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_TFE {get;set;}

        /// <summary>
        /// Desc:FeO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_FEO {get;set;}

        /// <summary>
        /// Desc:SiO2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SIO2 {get;set;}

        /// <summary>
        /// Desc:Al2O3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_AL2O3 {get;set;}

        /// <summary>
        /// Desc:CaO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_CAO {get;set;}

        /// <summary>
        /// Desc:MgO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_MGO {get;set;}

        /// <summary>
        /// Desc:S 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_S {get;set;}

        /// <summary>
        /// Desc:P 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_P {get;set;}

        /// <summary>
        /// Desc:TiO2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_TIO2 {get;set;}

        /// <summary>
        /// Desc:H2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_H2O {get;set;}

        /// <summary>
        /// Desc:修改标志 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool M_FLAG {get;set;}

        /// <summary>
        /// Desc:F 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_F {get;set;}

        /// <summary>
        /// Desc:Pb  
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_PB {get;set;}

        /// <summary>
        /// Desc:Sn  
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SN {get;set;}

        /// <summary>
        /// Desc:Bi 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_BI {get;set;}

        /// <summary>
        /// Desc:Sb 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SB {get;set;}

        /// <summary>
        /// Desc:As   
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_AS {get;set;}

        /// <summary>
        /// Desc:CaF2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CAF2 {get;set;}

        /// <summary>
        /// Desc:MnO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MNO {get;set;}

        /// <summary>
        /// Desc:K2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_K2O {get;set;}

        /// <summary>
        /// Desc:Na2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_NA2O {get;set;}

        /// <summary>
        /// Desc:Ig 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_IG {get;set;}

        /// <summary>
        /// Desc:碱度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ALKALINITY {get;set;}

        /// <summary>
        /// Desc:TFE_FEO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_TFE_FEO {get;set;}

        /// <summary>
        /// Desc:MT（全水） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MT {get;set;}

        /// <summary>
        /// Desc:ZnO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_ZNO {get;set;}

        /// <summary>
        /// Desc:-200目  
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_NEGATIVE200ORDER {get;set;}

        /// <summary>
        /// Desc:单价 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_PERVALUE {get;set;}

        /// <summary>
        /// Desc:粒度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_GRIT {get;set;}

        /// <summary>
        /// Desc:粒度当量值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_C1 {get;set;}

        /// <summary>
        /// Desc:烧损值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_LOI {get;set;}

        /// <summary>
        /// Desc:烧损当量值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_C2 {get;set;}

        /// <summary>
        /// Desc:粒度补充系数 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_LM {get;set;}

        /// <summary>
        /// Desc:铁矿粉综合品位价值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_COST {get;set;}

        /// <summary>
        /// Desc:铁矿粉综合价值指数 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_INDEX {get;set;}

        /// <summary>
        /// Desc:是否计算完成标志0为计算，1计算完成 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int M_CAL_FLAG {get;set;}

    }
}
