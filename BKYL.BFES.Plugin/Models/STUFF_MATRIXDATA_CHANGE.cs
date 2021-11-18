using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class STUFF_MATRIXDATA_CHANGE
    {
        /// <summary>
        /// Desc:变料时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime CHANGETIME { get; set; }
        /// <summary>
        /// Desc:记录时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:矿石批重 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double WEIGHT_ORE { get; set; }

        /// <summary>
        /// Desc:焦炭批重 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double WEIGHT_COKE { get; set; }

        /// <summary>
        /// Desc:设定矿料线 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double SET_LX_ORE { get; set; }

        /// <summary>
        /// Desc:设定焦料线 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double SET_LX_COKE { get; set; }

        /// <summary>
        /// Desc:装料制度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string CHARGE_MATRIX { get; set; }

        /// <summary>
        /// Desc:中心焦比例 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double CENTRAL_COKE_RATIO { get; set; }

        /// <summary>
        /// Desc:矿焦角差 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double ANGLEDIFF_ORE_COKE { get; set; }

        /// <summary>
        /// Desc:矿石角差 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double ANGLEDIFF_ORE { get; set; }

        /// <summary>
        /// Desc:矿石最大角 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double MAXANGLE_ORE { get; set; }

        /// <summary>
        /// Desc:焦炭最大角 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double MAXANGLE_COKE { get; set; }

        /// <summary>
        /// Desc:焦丁重量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double COKE_NUT_WEIGHT { get; set; }

        /// <summary>
        /// Desc:烧结矿重量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double SINTER_WEIGHT { get; set; }

        /// <summary>
        /// Desc:球团矿重量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double PELLET_WEIGHT { get; set; }

        /// <summary>
        /// Desc:块矿重量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double CRUDEORE_WEIGHT { get; set; }

        /// <summary>
        /// Desc:熔剂重量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double FLUX_WEIGHT { get; set; }

        /// <summary>
        /// Desc:烧结矿比例 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double SINTER_RATIO { get; set; }

        /// <summary>
        /// Desc:球团矿比例 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double PELLET_RATIO { get; set; }

        /// <summary>
        /// Desc:块矿比例 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double CRUDEORE_RATIO { get; set; }

        /// <summary>
        /// Desc:入炉品位 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double GRADE_IN { get; set; }

        /// <summary>
        /// Desc:喷煤量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double WINJ { get; set; }

        /// <summary>
        /// Desc:大焦负荷 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double BURDENRATIO { get; set; }

        /// <summary>
        /// Desc:全焦负荷 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double TOTAL_BURDENRATIO { get; set; }

        /// <summary>
        /// Desc:理论煤比 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double WINJ_RATIO { get; set; }

        /// <summary>
        /// Desc:理论焦丁比 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double COKE_NUT_RATIO { get; set; }

        /// <summary>
        /// Desc:批铁量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double THERY_FE { get; set; }

        /// <summary>
        /// Desc:批体积 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double THERY_TJ { get; set; }

        /// <summary>
        /// Desc:渣中SiO2 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double SLAG_SIO2 { get; set; }

        /// <summary>
        /// Desc:渣中Al2O3 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double SLAG_AL2O3 { get; set; }

        /// <summary>
        /// Desc:渣中CaO 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double SLAG_CAO { get; set; }

        /// <summary>
        /// Desc:渣中MgO 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double SLAG_MGO { get; set; }

        /// <summary>
        /// Desc:R2 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double R2 { get; set; }

        /// <summary>
        /// Desc:理论渣量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double THERY_SLAG { get; set; }

        /// <summary>
        /// Desc:冶炼周期 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double THERY_YLZQ { get; set; }

        /// <summary>
        /// Desc:理论焦比 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double THERY_GJB { get; set; }

        /// <summary>
        /// Desc:理论矿耗 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double THERY_KH { get; set; }

        /// <summary>
        /// Desc:理论渣比 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double THERY_ZB { get; set; }
        /// <summary>
        /// 布料制度使用时间
        /// </summary>
        public double TIMESPAN { get; set; }
        /// <summary>
        /// 焦炭角度差
        /// </summary>
        public double ANGLEDIFF_COKE { get; set; }
        /// <summary>
        /// 矿石最小角度
        /// </summary>
        public double MINANGLE_ORE { get; set; }
        /// <summary>
        /// 焦炭最小角度
        /// </summary>
        public double MINANGLE_COKE { get; set; }
    }
}
