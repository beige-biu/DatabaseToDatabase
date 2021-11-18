using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MC_SRM_PAR
    {
        
        /// <summary>
        /// Desc:插入时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:烧返配比调整周期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_BILL_T {get;set;}

        /// <summary>
        /// Desc:仓重偏差变化ec计算时间段 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_EC_T {get;set;}

        /// <summary>
        /// Desc:目标仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_AIM {get;set;}

        /// <summary>
        /// Desc:烧返仓位偏差量1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_E_LEVEL_1 {get;set;}

        /// <summary>
        /// Desc:烧返仓位偏差量2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_E_LEVEL_2 {get;set;}

        /// <summary>
        /// Desc:烧返仓位偏差量3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_E_LEVEL_3 {get;set;}

        /// <summary>
        /// Desc:烧返仓位偏差量4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_E_LEVEL_4 {get;set;}

        /// <summary>
        /// Desc:烧返配比调整幅度低低值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_BILL_LOW_LOW {get;set;}

        /// <summary>
        /// Desc:烧返配比调整幅度低值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_BILL_LOW {get;set;}

        /// <summary>
        /// Desc:烧返配比调整幅度高值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_BILL_UP {get;set;}

        /// <summary>
        /// Desc:烧返配比调整幅度高高值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_BILL_UP_UP {get;set;}

        /// <summary>
        /// Desc:烧返仓位上限值报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_UP {get;set;}

        /// <summary>
        /// Desc:烧返仓位下限值报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_LOW {get;set;}

        /// <summary>
        /// Desc:烧返配比调整幅度修正量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_K {get;set;}

        public  int? STATE { get; set; }

    }
}
