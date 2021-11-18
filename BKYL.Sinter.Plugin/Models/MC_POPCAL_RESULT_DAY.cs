using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MC_POPCAL_RESULT_DAY
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:历史烧结返矿百分比平均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double POPCAL_D_SIN_RM {get;set;}

        /// <summary>
        /// Desc:历史烧损平均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double POPCAL_D_LOT {get;set;}

        /// <summary>
        /// Desc:历史理论烧结矿产量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double POPCAL_D_OUT_THR {get;set;}

        /// <summary>
        /// Desc:历史实际烧结矿产量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double POPCAL_D_OUT_REA {get;set;}

        /// <summary>
        /// Desc:实时设定总料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? POPCAL_D_BW_ACT {get;set;}

        /// <summary>
        /// Desc:修改产量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? POPCAL_D_OUT_MOD {get;set;}

        /// <summary>
        /// Desc:计划产量（全天） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? POPCAL_D_ALL_OUT_PL {get;set;}

        /// <summary>
        /// Desc:每天20:01计算下一天的理论混合料总料量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? POPCAL_D_BW_THR {get;set;}

        /// <summary>
        /// Desc:白班计划产量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double POPCAL_D_OUT_PL {get;set;}

        /// <summary>
        /// Desc:夜班计划产量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? POPCAL_D_N_OUT_PL {get;set;}

    }
}
