using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models
{
    public class MES_DAY_IRON
    {
        
        /// <summary>
        /// Desc:记录时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime RECORD_TIME {get;set;}

        /// <summary>
        /// Desc:班次 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string BC {get;set;}

        /// <summary>
        /// Desc:班别 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string BB {get;set;}

        /// <summary>
        /// Desc:炉次 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? FRUN_DEGR {get;set;}

        /// <summary>
        /// Desc:铁次 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string IRON_DEGR {get;set;}

        /// <summary>
        /// Desc:铁口 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string IRON_MOUTH_NO {get;set;}

        /// <summary>
        /// Desc:兑罐时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime TIN_TIME {get;set;}

        /// <summary>
        /// Desc:出铁开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime OUT_START_TIME {get;set;}

        /// <summary>
        /// Desc:来渣时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime OUT_TIME {get;set;}

        /// <summary>
        /// Desc:出铁结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime OUT_END_TIME {get;set;}

        /// <summary>
        /// Desc:产量理论 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? YIEL_THEO {get;set;}

        /// <summary>
        /// Desc:产量估产 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? YIEL_AMOU {get;set;}

        /// <summary>
        /// Desc:产量过磅 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? YIEL_POUD {get;set;}

        /// <summary>
        /// Desc:理论渣量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? YIEL_SLAG {get;set;}

        /// <summary>
        /// Desc:渣色 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string DREG_COLO {get;set;}

        /// <summary>
        /// Desc:深度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? IRON_MOUTH_DEPTH {get;set;}

        /// <summary>
        /// Desc:角度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? IRON_MOUTH_ANGEL {get;set;}

        /// <summary>
        /// Desc:打泥量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HIT_MUD {get;set;}

        /// <summary>
        /// Desc:带流 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string DAI_LIU {get;set;}

        /// <summary>
        /// Desc:晚点 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LATE {get;set;}

        /// <summary>
        /// Desc:成分估计SI 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? YIEL_SI {get;set;}

        /// <summary>
        /// Desc:成分估计S 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? YIEL_S {get;set;}

        /// <summary>
        /// Desc:成分估计R2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? YIEL_R2 {get;set;}

        /// <summary>
        /// Desc:铁水测温1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? IRON_TEMP1 {get;set;}

        /// <summary>
        /// Desc:铁水测温2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? IRON_TEMP2 {get;set;}

        /// <summary>
        /// Desc:填写人 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string RECORDER {get;set;}

        /// <summary>
        /// Desc:备注 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string REMARK {get;set;}

    }
}
