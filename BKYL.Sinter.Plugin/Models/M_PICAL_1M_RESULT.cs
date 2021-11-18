using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models

{
    public class M_PICAL_1M_RESULT
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:一混采样时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? PICAL_1M_TIME {get;set;}

        /// <summary>
        /// Desc:一混后粒度＜1mm的比例 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_GRIT_1 {get;set;}

        /// <summary>
        /// Desc:一混后粒度1-3mm的比例 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_GRIT_1_3 {get;set;}

        /// <summary>
        /// Desc:一混后粒度大于3mm的比例 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_GRIT_3 {get;set;}

        /// <summary>
        /// Desc:一混料对应的填充率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_FILL_RATE {get;set;}

        /// <summary>
        /// Desc:一混目标水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_TARGET_WATER {get;set;}

        /// <summary>
        /// Desc:一混加水量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_FT_PV {get;set;}

        /// <summary>
        /// Desc:一混转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_MIXER_RATE {get;set;}

        /// <summary>
        /// Desc:一混对应溶剂配比1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_SOL_BILL_1 {get;set;}

        /// <summary>
        /// Desc:一混对应溶剂配比2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_SOL_BILL_2 {get;set;}

        /// <summary>
        /// Desc:一混对应溶剂配比3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_SOL_BILL_3 {get;set;}

        /// <summary>
        /// Desc:一混料对应的混匀矿粒度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_IRON_GRIT {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_SPARE1 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_SPARE2 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_SPARE3 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_SPARE4 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_SPARE5 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_SPARE6 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_SPARE7 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_SPARE8 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_SPARE9 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_1M_SPARE10 {get;set;}

        /// <summary>
        /// Desc:烧返配比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_RE_BILL_DRY {get;set;}

    }
}
