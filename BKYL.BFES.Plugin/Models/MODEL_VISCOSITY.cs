using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class MODEL_VISCOSITY
    {
        
        /// <summary>
        /// Desc:数据时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:粘度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double VISCOSITY {get;set;}

        /// <summary>
        /// Desc:SIO2质量百分比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double SIO2 {get;set;}

        /// <summary>
        /// Desc:CAO质量百分比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double CAO {get;set;}

        /// <summary>
        /// Desc:MGO质量百分比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double MGO {get;set;}

        /// <summary>
        /// Desc:AL2O3质量百分比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double AL2O3 {get;set;}

        /// <summary>
        /// Desc:MNO质量百分比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double MNO {get;set;}

        /// <summary>
        /// Desc:FEO质量百分比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double FEO {get;set;}

        /// <summary>
        /// Desc:TIO2质量百分比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TIO2 {get;set;}

        /// <summary>
        /// Desc:CAF2质量百分比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double CAF2 {get;set;}

        /// <summary>
        /// Desc:二元碱度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double R2 {get;set;}

        /// <summary>
        /// Desc:三元碱度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double R3 {get;set;}

        /// <summary>
        /// Desc:四元碱度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double R4 {get;set;}

        /// <summary>
        /// Desc:四元mol碱度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double R4MOL {get;set;}

        /// <summary>
        /// Desc:镁铝比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double MG_AL_RATIO {get;set;}

        /// <summary>
        /// Desc:炉渣温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE_SLAG {get;set;}

        /// <summary>
        /// Desc:粘度骤变温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE_LIMIT {get;set;}

        /// <summary>
        /// Desc:炉渣熔化性温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TE_SLAG_MELT {get;set;}

        /// <summary>
        /// Desc:1400温度下液相占比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double LIRATIO {get;set;}

        /// <summary>
        /// Desc:批数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CHARGE {get;set;}

        /// <summary>
        /// Desc:修正后的SIO2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SIO2 {get;set;}

        /// <summary>
        /// Desc:修正后的CaO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_CAO {get;set;}

        /// <summary>
        /// Desc:修正后的MgO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_MGO {get;set;}

        /// <summary>
        /// Desc:修正后的Al2O3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_AL2O3 {get;set;}

        /// <summary>
        /// Desc:修正后的R2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_R2 {get;set;}

        /// <summary>
        /// Desc:修正后的FeO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_FEO {get;set;}

        /// <summary>
        /// Desc:修正后的MnO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_MNO {get;set;}

    }
}
