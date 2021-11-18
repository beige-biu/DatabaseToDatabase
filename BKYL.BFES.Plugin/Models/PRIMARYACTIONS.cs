using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class PRIMARYACTIONS
    {
        
        /// <summary>
        /// Desc:时标 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int R_MSGID {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_BVACT {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_BTACT {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_CRACT {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_BCACT {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_PCIACT {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_O2ACT {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_SLK {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_HMTRNK {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_TQRNK {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_AZF {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_ACT {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_ACT_TM {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_PCI_STOP {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_AZF1 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? R_ORACT {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string R_MSGTEXT {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string R_JUDGE_STATE {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string R_MSGEXPLAIN {get;set;}

        /// <summary>
        /// Desc:操作建议 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string R_OPERATIONTEXT {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool EXIST {get;set;}

        /// <summary>
        /// Desc:备注信息 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string REMARKS {get;set;}

        /// <summary>
        /// Desc:滑料时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? HL_TIME {get;set;}

    }
}
