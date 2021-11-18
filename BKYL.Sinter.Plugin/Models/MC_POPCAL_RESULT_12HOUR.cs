using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MC_POPCAL_RESULT_12HOUR
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:成品矿到配料的总倒推时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? POPCAL_H_FP_MAT_T_AV {get;set;}

        /// <summary>
        /// Desc:周期内成品矿重量累积值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? POPCAL_H_FP_ACCU {get;set;}

        /// <summary>
        /// Desc:周期内焦炭配料累积值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? POPCAL_H_COKE_ACCU {get;set;}

        /// <summary>
        /// Desc:固体燃耗湿基计算值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? POPCAL_H_TSC_CON {get;set;}

        /// <summary>
        /// Desc:固体燃耗湿基理论值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double POPCAL_H_TSC_CON_LL {get;set;}

        /// <summary>
        /// Desc:固体燃耗干基计算值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double POPCAL_H_TSC_CON_DRY {get;set;}

        /// <summary>
        /// Desc:固体燃耗干基理论值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double POPCAL_H_TSC_CON_LL_DRY {get;set;}

        /// <summary>
        /// Desc:周期内焦炭配料累积值倒推 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? POPCAL_H_COKE_ACC1 {get;set;}

        /// <summary>
        /// Desc:周期内理论产量累计值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? POPCAL_H_LL_ACCU {get;set;}

    }
}
