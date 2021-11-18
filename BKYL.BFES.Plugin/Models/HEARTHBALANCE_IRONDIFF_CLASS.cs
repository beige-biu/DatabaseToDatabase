using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models
{
    public class HEARTHBALANCE_IRONDIFF_CLASS
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string BC {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BB {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? IRON_THEORY {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? IRON_REAL {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? IRONDIFF {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SI_AVG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SI_DIFF {get;set;}

        /// <summary>
        /// Desc:班产量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? YIELD {get;set;}

        /// <summary>
        /// Desc:焦炭量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? COKEWEIGHT {get;set;}

        /// <summary>
        /// Desc:焦丁量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? LITTLEWEIGHT {get;set;}

        /// <summary>
        /// Desc:烧结矿量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINTERWEIGHT {get;set;}

        /// <summary>
        /// Desc:球团矿量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PELLETWEIGHT {get;set;}

        /// <summary>
        /// Desc:熔剂量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FLUXWEIGHT {get;set;}

        /// <summary>
        /// Desc:块矿量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? CURDEWEIGHT {get;set;}
        /// <summary>
        /// 出铁率
        /// </summary>
        public double? IRONPRODRATIO { get; set; }
        /// <summary>
        /// 出渣率
        /// </summary>
        public double? SLAGPRODRATIO { get; set; }
    }
}
