using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class MATERIAL_BALANCE_CLASS
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:料批号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_CHARGES {get;set;}

        /// <summary>
        /// Desc:吨铁耗风量(m3) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_BVC {get;set;}

        /// <summary>
        /// Desc:带入矿石重量 t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_ORE_IN {get;set;}

        /// <summary>
        /// Desc:带入焦炭重量 t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_COKE_IN {get;set;}

        /// <summary>
        /// Desc:带入煤粉重量 t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_COAL_IN {get;set;}

        /// <summary>
        /// Desc:带入熔剂重量 t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_FLUX_IN {get;set;}

        /// <summary>
        /// Desc:鼓风重量 t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_BVC1_IN {get;set;}

        /// <summary>
        /// Desc:出铁量 t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_MHMT_OUT {get;set;}

        /// <summary>
        /// Desc:出渣量 t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_MSLAG_OUT {get;set;}

        /// <summary>
        /// Desc:炉顶煤气重量 t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_MVTOP_OUT {get;set;}

        /// <summary>
        /// Desc:炉顶水蒸气重量 t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_MH2O_OUT {get;set;}

        /// <summary>
        /// Desc:炉尘重量 t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_DUST_OUT {get;set;}

        /// <summary>
        /// Desc:总的带入重量 t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_SUM_IN {get;set;}

        /// <summary>
        /// Desc:总的带出重量 t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_SUM_OUT {get;set;}

        /// <summary>
        /// Desc:带入风的总量(m3) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_BVC1 {get;set;}

        /// <summary>
        /// Desc:没有计算这个数，考虑删除 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_BVO {get;set;}

        /// <summary>
        /// Desc:带入焦丁量 t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_COKE_NUT_WEIGHT {get;set;}

        /// <summary>
        /// Desc:大焦比 kg/t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_COKE_RATIO {get;set;}

        /// <summary>
        /// Desc:煤比 kg/t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_COAL_RATIO {get;set;}

        /// <summary>
        /// Desc:焦丁比 kg/t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_NUTCOKE_RATIO {get;set;}

        /// <summary>
        /// Desc:渣比 kg/t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_SLAGRATIO {get;set;}

        /// <summary>
        /// Desc:全焦比 kg/t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_TOTAL_COKE_RATIO {get;set;}

        /// <summary>
        /// Desc:燃料比 kg/t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_FULE_RATIO {get;set;}

        /// <summary>
        /// Desc:矿耗 kg/t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_ORE_RATIO {get;set;}

        /// <summary>
        /// Desc:大焦负荷 kg/t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_BURDENRATIO {get;set;}

        /// <summary>
        /// Desc:全焦负荷 kg/t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_TOTAL_BURDENRATIO {get;set;}

        /// <summary>
        /// Desc:烧结矿重量 t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_SINTER_WEIGHT {get;set;}

        /// <summary>
        /// Desc:球团重量 t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_PELLET_WEIGHT {get;set;}

        /// <summary>
        /// Desc:块矿重量 t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_CRUDEORE_WEIGHT {get;set;}

    }
}
