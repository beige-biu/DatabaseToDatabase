using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class TAPPING_IRON_LAB
    {
        
        /// <summary>
        /// Desc:写入时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime SAMPTIME {get;set;}
        /// <summary>
        /// 样品编号
        /// </summary>
        public long C_SMPID { get; set; }

        /// <summary>
        /// Desc:铁次号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int TAPNUMB {get;set;}

        /// <summary>
        /// Desc:钢包号、罐号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string LADLE1_NR {get;set;}

        /// <summary>
        /// Desc:单罐产量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TAPWREAL {get;set;}

        /// <summary>
        /// Desc:硅 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double HMSI {get;set;}

        /// <summary>
        /// Desc:硫 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double HMS {get;set;}

        /// <summary>
        /// Desc:磷 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double HMP {get;set;}

        /// <summary>
        /// Desc:锰 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double HMMN {get;set;}

        /// <summary>
        /// Desc:碳 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double HMC {get;set;}

        /// <summary>
        /// Desc:砷 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double HMAS {get;set;}

        /// <summary>
        /// Desc:铬 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double HMCR {get;set;}

        /// <summary>
        /// Desc:鈦 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double HMTI {get;set;}

        /// <summary>
        /// Desc:铜 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double HMCU {get;set;}

        /// <summary>
        /// Desc:更新时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? UPDATETIME {get;set;}

    }
}


namespace BKYL.BFES.Plugin.Models.TG
{
    public class TAPPING_IRON_LAB
    {

        /// <summary>
        /// Desc:分析时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime ANALYSIS_TIME { get; set; }

        /// <summary>
        /// Desc:铁次号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int TAPNUMB { get; set; }

        /// <summary>
        /// Desc:硅 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMSI { get; set; }

        /// <summary>
        /// Desc:硫 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMS { get; set; }

        /// <summary>
        /// Desc:磷 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMP { get; set; }

        /// <summary>
        /// Desc:锰 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMMN { get; set; }

        /// <summary>
        /// Desc:碳 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMC { get; set; }

        /// <summary>
        /// Desc:鈦 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMTI { get; set; }

        /// <summary>
        /// Desc:更新时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? UPDATETIME { get; set; }

        /// <summary>
        /// Desc:入库时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_ID { get; set; }

        /// <summary>
        /// Desc:试样号

        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SAMPLEID { get; set; }

        /// <summary>
        /// Desc:铜 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMCU { get; set; }

        /// <summary>
        /// Desc:砷 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMAS { get; set; }

        /// <summary>
        /// Desc:铬 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMCR { get; set; }

        /// <summary>
        /// Desc:镍 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMNI { get; set; }

        /// <summary>
        /// Desc:班别 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CLASS { get; set; }

    }
}
