using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class M_PICAL_2M_RESULT
    {
        
        /// <summary>
        /// Desc:时间（粒度检测报出时间） 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:二混采样时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime PICAL_2M_TIME {get;set;}

        /// <summary>
        /// Desc:二混后粒度＜1mm的比例 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_GRIT_1 {get;set;}

        /// <summary>
        /// Desc:二混后粒度1-3mm的比例 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_GRIT_1_3 {get;set;}

        /// <summary>
        /// Desc:二混后粒度>3mm的比例 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_GRIT_3 {get;set;}

        /// <summary>
        /// Desc:二混料对应填充率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_FILL_RATE {get;set;}

        /// <summary>
        /// Desc:二混目标水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_TARGET_WATER {get;set;}

        /// <summary>
        /// Desc:二混加水量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_FT_PV {get;set;}

        /// <summary>
        /// Desc:二混转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_MIXER_RATE {get;set;}

        /// <summary>
        /// Desc:二混料对应一混加水量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_1M_FT_PV {get;set;}

        /// <summary>
        /// Desc:二混料对应一混转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_1M_MIXER_RATE {get;set;}

        /// <summary>
        /// Desc:二混对应溶剂配比1
 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_SOL_BILL_1 {get;set;}

        /// <summary>
        /// Desc:二混对应溶剂配比2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_SOL_BILL_2 {get;set;}

        /// <summary>
        /// Desc:二混对应溶剂配比3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_SOL_BILL_3 {get;set;}

        /// <summary>
        /// Desc:二混料对应混匀矿粒度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_IRON_GRIT {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_SPARE1 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_SPARE2 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_SPARE3 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_SPARE4 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_SPARE5 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_SPARE6 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_SPARE7 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_SPARE8 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_SPARE9 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_2M_SPARE10 {get;set;}

        /// <summary>
        /// Desc:烧返配比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PICAL_RE_BILL_DRY {get;set;}

    }
}
