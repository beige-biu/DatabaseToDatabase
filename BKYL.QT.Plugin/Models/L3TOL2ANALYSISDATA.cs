using System;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Models
{
    public class L3TOL2ANALYSISDATA
    {
        
        /// <summary>
        /// Desc:3级id主键 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ID {get;set;}

        /// <summary>
        /// Desc:试样编码用于增加新料编号和名称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string SAMPLE_MTRL_CODE {get;set;}

        /// <summary>
        /// Desc:试样名称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string SAMPLE_MTRL_NAME {get;set;}

        /// <summary>
        /// Desc:检验时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? TEST_TIME {get;set;}

        /// <summary>
        /// Desc:取样时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SAMPLE_TIME {get;set;}

        /// <summary>
        /// Desc:球团班次 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string QTCLASS_NAME {get;set;}

        /// <summary>
        /// Desc:球团班次1234 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? QTCLASS_NAME_ID {get;set;}

        /// <summary>
        /// Desc:这个表存的检验数据是data字符串，需要解析过后存到另外的表，这个字段作为是否解析的标志 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE3 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? P {get;set;}

        /// <summary>
        /// Desc:S                         
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? S {get;set;}

        /// <summary>
        /// Desc:Pb                                
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PB {get;set;}

        /// <summary>
        /// Desc:Sn                                
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SN {get;set;}

        /// <summary>
        /// Desc:Bi                                
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BI {get;set;}

        /// <summary>
        /// Desc:Sb                                
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SB {get;set;}

        /// <summary>
        /// Desc:As                                
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ASS {get;set;}

        /// <summary>
        /// Desc:Al2O3                             
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? AL2O3 {get;set;}

        /// <summary>
        /// Desc:TiO2                              
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TIO2 {get;set;}

        /// <summary>
        /// Desc:CaF2                              
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? CAF2 {get;set;}

        /// <summary>
        /// Desc:MnO                               
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MNO {get;set;}

        /// <summary>
        /// Desc:TFe                
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TFE {get;set;}

        /// <summary>
        /// Desc:FeO                               
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FEO {get;set;}

        /// <summary>
        /// Desc:SiO2                
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SIO2 {get;set;}

        /// <summary>
        /// Desc:CaO                               
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? CAO {get;set;}

        /// <summary>
        /// Desc:MgO                               
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MGO {get;set;}

        /// <summary>
        /// Desc:F                    
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? F {get;set;}

        /// <summary>
        /// Desc:K2O                               
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? K2O {get;set;}

        /// <summary>
        /// Desc:Na2O                              
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? NA2O {get;set;}

        /// <summary>
        /// Desc:Ig                                
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? IG {get;set;}

        /// <summary>
        /// Desc:碱度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ALKALINITY {get;set;}

        /// <summary>
        /// Desc:TFe/FeO                           
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TFE_FEO {get;set;}

        /// <summary>
        /// Desc:Mt                                
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MT {get;set;}

        /// <summary>
        /// Desc:粒度                              
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRANULARITY {get;set;}

        /// <summary>
        /// Desc:转鼓指数                          
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? EQUITY_INDEX {get;set;}

        /// <summary>
        /// Desc:抗磨指数                          
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? WEAR_RESISTANCE_INDEX {get;set;}

        /// <summary>
        /// Desc:筛分指数                          
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SCREENING_INDEX {get;set;}

        /// <summary>
        /// Desc:RDI+6.3                           
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string RDI_6_3 {get;set;}

        /// <summary>
        /// Desc:RI                                
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string RI {get;set;}

        /// <summary>
        /// Desc:熔融滴落试验                      
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MELT_DROP_TEST {get;set;}

        /// <summary>
        /// Desc:胶质价                            
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string COLLOID_PRICE {get;set;}

        /// <summary>
        /// Desc:膨胀倍数                          
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string EXPANSION_RATIO {get;set;}

        /// <summary>
        /// Desc:抗压强度                          
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string COMPRESSIVE_STRENGTH {get;set;}

        /// <summary>
        /// Desc:ZnO                               
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZNO {get;set;}

        /// <summary>
        /// Desc:-200目    
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NEGATIVE200ORDER {get;set;}

        /// <summary>
        /// Desc:水份      
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? WATER_CONTENT {get;set;}

        /// <summary>
        /// Desc:保留字段1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string RESERVED_FIELD1 {get;set;}

        /// <summary>
        /// Desc:保留字段2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string RESERVED_FIELD2 {get;set;}

        /// <summary>
        /// Desc:保留字段3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? RESERVED_FIELD3 {get;set;}

        /// <summary>
        /// Desc:保留字段4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? RESERVED_FIELD4 {get;set;}

        /// <summary>
        /// Desc:1 球团矿 0 其他原料矿 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string QXQT {get;set;}

        /// <summary>
        /// Desc:插入时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? TIMESTAMP {get;set;}

    }
}
