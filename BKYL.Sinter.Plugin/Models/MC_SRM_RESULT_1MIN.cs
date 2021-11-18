using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MC_SRM_RESULT_1MIN
    {
        
        /// <summary>
        /// Desc:插入时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:调整时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? A_TIME {get;set;}

        /// <summary>
        /// Desc:调用状态标志(1：周期调用；2：仓位超限调用；3：目标仓位变化调用) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SRMCAL_FLAG {get;set;}

        /// <summary>
        /// Desc:烧返仓在用标志（1：在用；0：停用） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SRMCAL_SIG {get;set;}

        /// <summary>
        /// Desc:8号烧返仓实时仓重 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SRMCAL_W {get;set;}

        /// <summary>
        /// Desc:烧返当前配比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SRMCAL_BILL_SP {get;set;}

        /// <summary>
        /// Desc:烧返目标仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SRMCAL_W_AIM {get;set;}

        /// <summary>
        /// Desc:当前时间倒推EC_T时刻的烧返仓实时仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SRMCAL_W_LAST {get;set;}

        /// <summary>
        /// Desc:烧返仓实时仓位与目标仓重偏差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SRMCAL_E {get;set;}

        /// <summary>
        /// Desc:周期内烧返仓实时仓位偏差变化 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SRMCAL_EC {get;set;}

        /// <summary>
        /// Desc:本次烧返配比调整量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SRMCAL_BILL_SP_A {get;set;}

        /// <summary>
        /// Desc:调整后烧返配比设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SRMCAL_BILL_SP_NEW {get;set;}

        /// <summary>
        /// Desc:调整前烧返配比设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SRMCAL_BILL_SP_OLD {get;set;}

        /// <summary>
        /// Desc:总料量SP 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SRMCAL_TOTAL_SP_W {get;set;}

        /// <summary>
        /// Desc:总料量PV 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SRMCAL_TOTAL_PV_W {get;set;}

        /// <summary>
        /// Desc:燃料配比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SRMCAL_FUEL_BILL_SP {get;set;}

        /// <summary>
        /// Desc:烧结机设定机速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SRMCAL_SIN_MS_SP {get;set;}

        /// <summary>
        /// Desc:烧结机实际机速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SRMCAL_SIN_MS_PV {get;set;}

        /// <summary>
        /// Desc:当前模型运行状态（1：既存库又下发；2：只存库不下发；3：不存库不下发） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SRMCAL_RS_FLAG {get;set;}

        /// <summary>
        /// Desc:烧返配比调整模型启停信号（1：模型按钮为投入状态；0：模型按钮为退出状态） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SRMCAL_SL_FLAG {get;set;}

    }
}
