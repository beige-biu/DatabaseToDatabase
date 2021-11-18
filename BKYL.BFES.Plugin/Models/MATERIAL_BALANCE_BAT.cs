using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models
{
    public class MATERIAL_BALANCE_BAT
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
        public int I_CHARGES {get;set;}

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
        /// Desc:块矿带入的K2O t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double K2O_CRUDEORE {get;set;}

        /// <summary>
        /// Desc:球团带入的k2o t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double K2O_PELLET {get;set;}

        /// <summary>
        /// Desc:烧结矿带入的K2O t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double K2O_SINTER {get;set;}

        /// <summary>
        /// Desc:焦炭带入的K2O t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double K2O_COKE {get;set;}

        /// <summary>
        /// Desc:煤粉带入的K2O t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double K2O_COAL {get;set;}

        /// <summary>
        /// Desc:总的带入的K2O t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double K2O_TOTAL {get;set;}

        /// <summary>
        /// Desc:炉渣带出的K2O t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double K2O_SLAG {get;set;}

        /// <summary>
        /// Desc:炉尘带出的K2O t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double K2O_DUST {get;set;}

        /// <summary>
        /// Desc:高炉中剩余的K2O t 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double K2O_REST {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double K2O_TOXICOUT_SLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double K2O_TOXICOUT_DUST {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double K2O_TOXICKEEP {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double NA2O_CRUDEORE {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double NA2O_PELLET {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double NA2O_SINTER {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double NA2O_COKE {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double NA2O_COAL {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double NA2O_TOTAL {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double NA2O_SLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double NA2O_DUST {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double NA2O_REST {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double NA2O_TOXICOUT_SLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double NA2O_TOXICOUT_DUST {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double NA2O_TOXICKEEP {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double PBO_CRUDEORE {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double PBO_PELLET {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double PBO_SINTER {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double PBO_COKE {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double PBO_COAL {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double PBO_TOTAL {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double PBO_SLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double PBO_DUST {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double PBO_REST {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double PBO_TOXICOUT_SLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double PBO_TOXICOUT_DUST {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double PBO_TOXICKEEP {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double ZNO_CRUDEORE {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double ZNO_PELLET {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double ZNO_SINTER {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double ZNO_COKE {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double ZNO_COAL {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double ZNO_TOTAL {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double ZNO_SLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double ZNO_DUST {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double ZNO_REST {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double ZNO_TOXICOUT_SLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double ZNO_TOXICOUT_DUST {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double ZNO_TOXICKEEP {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_BVC1 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_BVO {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_COKE_NUT_WEIGHT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_CHARGE_NOZERO { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_SINTER_WEIGHT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_PELLET_WEIGHT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double I_CRUDEORE_WEIGHT { get; set; }
        /// <summary>
        /// Desc:- 矿石(块矿球团和烧结矿)带入P总量
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double I_P_ORE { get; set; }
        /// <summary>
        /// Desc:- 矿石(块矿球团和烧结矿)带入TIO2总量
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double I_TIO2_ORE { get; set; }
        /// <summary>
        /// Desc:- 矿石(块矿球团和烧结矿)带入MNO总量
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double I_MNO_ORE { get; set; }
    }
}
