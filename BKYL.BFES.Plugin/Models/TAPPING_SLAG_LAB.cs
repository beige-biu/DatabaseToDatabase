using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class TAPPING_SLAG_LAB
    {
        
        /// <summary>
        /// Desc:写入时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime SAMPLE_TAKETIME {get;set;}

        /// <summary>
        /// Desc:铁次号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int TAPPING_NUM {get;set;}

        /// <summary>
        /// Desc:氧化钙 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGCAO {get;set;}

        /// <summary>
        /// Desc:二氧化硅 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGSIO2 {get;set;}

        /// <summary>
        /// Desc:三氧化二铝 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGAL2O3 {get;set;}

        /// <summary>
        /// Desc:氧化镁 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGMGO {get;set;}

        /// <summary>
        /// Desc:氧化亚铁 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGFEO {get;set;}

        /// <summary>
        /// Desc:硫 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGS {get;set;}

        /// <summary>
        /// Desc:R2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGR2 {get;set;}

        /// <summary>
        /// Desc:二氧化钛 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGTIO2 {get;set;}

        /// <summary>
        /// Desc:流动性 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SLAG_FLOW {get;set;}

        /// <summary>
        /// Desc:渣色 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SLAGCOLOR {get;set;}

        /// <summary>
        /// Desc:更新时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime UPDATETIME {get;set;}

    }
}

namespace BKYL.BFES.Plugin.Models.TG
{
    public class TAPPING_SLAG_LAB
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
        public int TAPPING_NUM { get; set; }

        /// <summary>
        /// Desc:氧化钙 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGCAO { get; set; }

        /// <summary>
        /// Desc:二氧化硅 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGSIO2 { get; set; }

        /// <summary>
        /// Desc:三氧化二铝 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGAL2O3 { get; set; }

        /// <summary>
        /// Desc:氧化镁 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGMGO { get; set; }

        /// <summary>
        /// Desc:R2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGR2 { get; set; }

        /// <summary>
        /// Desc:二氧化钛 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGTIO2 { get; set; }

        /// <summary>
        /// Desc:更新时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? UPDATETIME { get; set; }

        /// <summary>
        /// Desc:写入时间 
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
        /// Desc:硫 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGS { get; set; }

        /// <summary>
        /// Desc:流动性 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SLAG_FLOW { get; set; }

        /// <summary>
        /// Desc:渣色 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SLAGCOLOR { get; set; }

        /// <summary>
        /// Desc:氧化亚铁 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGFEO { get; set; }

        /// <summary>
        /// Desc:铁 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGFE { get; set; }

        /// <summary>
        /// Desc:P 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGP { get; set; }

        /// <summary>
        /// Desc:MNO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGMNO { get; set; }

        /// <summary>
        /// Desc:MN 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGMN { get; set; }

        /// <summary>
        /// Desc:R3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGR3 { get; set; }

        /// <summary>
        /// Desc:R4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGR4 { get; set; }

        /// <summary>
        /// Desc:K2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGK2O { get; set; }

        /// <summary>
        /// Desc:Na2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGNA2O { get; set; }

        /// <summary>
        /// Desc:Pb 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGPB { get; set; }

        /// <summary>
        /// Desc:Zn 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGZN { get; set; }

        /// <summary>
        /// Desc:全量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAG_QUANLIANG { get; set; }

        /// <summary>
        /// Desc:班别 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CLASS { get; set; }

    }
}
