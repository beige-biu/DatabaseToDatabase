using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MC_STOP_START_Time
    {
        /// <summary>
        /// Desc:插入时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// 设备编码
        /// </summary>
        public string INSTA_CODE { get; set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        public int FLAG { get; set; }

        /// <summary>
        /// 停机开始时间
        /// </summary>
        public DateTime STOP_BEGINTIME { get; set; }
        /// <summary>
        /// 停机结束时间
        /// </summary>
        public DateTime STOP_ENDTIME { get; set; }

        /// <summary>
        /// 时间间隔
        /// </summary>
        public double INTERVAL_TIME { get; set; }
    }


    public class MC_STOP_START_RECORDER
    {
        
        /// <summary>
        /// Desc:ID号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int ID {get;set;}

        /// <summary>
        /// Desc:P_7皮带停机开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? START_TIME_P_7 {get;set;}

        /// <summary>
        /// Desc:P_7皮带停机结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? STOP_TIME_P_7 {get;set;}

        /// <summary>
        /// Desc:P_7皮带停机时间间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? INTERVAL_TIME_P_7 {get;set;}

        /// <summary>
        /// Desc:1H_1皮带停机开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? START_TIME_1H_1 {get;set;}

        /// <summary>
        /// Desc:1H_1皮带停机结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? STOP_TIME_1H_1 {get;set;}

        /// <summary>
        /// Desc:1H_1皮带停机时间间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? INTERVAL_TIME_1H_1 {get;set;}

        /// <summary>
        /// Desc:一次混合机停机开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? START_TIME_1M {get;set;}

        /// <summary>
        /// Desc:一次混合机停机结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? STOP_TIME_1M {get;set;}

        /// <summary>
        /// Desc:一次混合机停机时间间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? INTERVAL_TIME_1M {get;set;}

        /// <summary>
        /// Desc:2H_1皮带停机开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? START_TIME_2H_1 {get;set;}

        /// <summary>
        /// Desc:2H_1皮带停机结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? STOP_TIME_2H_1 {get;set;}

        /// <summary>
        /// Desc:2H_1皮带停机时间间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? INTERVAL_TIME_2H_1 {get;set;}

        /// <summary>
        /// Desc:二次混合机停机开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? START_TIME_2M {get;set;}

        /// <summary>
        /// Desc:二次混合机停机结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? STOP_TIME_2M {get;set;}

        /// <summary>
        /// Desc:二次混合机停机时间间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? INTERVAL_TIME_2M {get;set;}

        /// <summary>
        /// Desc:Z2_1皮带停机开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? START_TIME_Z2_1 {get;set;}

        /// <summary>
        /// Desc:Z2_1皮带停机结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? STOP_TIME_Z2_1 {get;set;}

        /// <summary>
        /// Desc:Z2_1皮带停机时间间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? INTERVAL_TIME_Z2_1 {get;set;}

        /// <summary>
        /// Desc:S_1皮带停机开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? START_TIME_S_1 {get;set;}

        /// <summary>
        /// Desc:S_1皮带停机结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? STOP_TIME_S_1 {get;set;}

        /// <summary>
        /// Desc:S_1皮带停机时间间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? INTERVAL_TIME_S_1 {get;set;}

        /// <summary>
        /// Desc:S_6皮带停机开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? START_TIME_S_6 {get;set;}

        /// <summary>
        /// Desc:S_6皮带停机结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? STOP_TIME_S_6 {get;set;}

        /// <summary>
        /// Desc:S_6皮带停机时间间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? INTERVAL_TIME_S_6 {get;set;}

        /// <summary>
        /// Desc:圆辊给料机停机开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? START_TIME_STICK {get;set;}

        /// <summary>
        /// Desc:圆辊给料机停机结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? STOP_TIME_STICK {get;set;}

        /// <summary>
        /// Desc:圆辊给料机停机时间间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? INTERVAL_TIME_STICK {get;set;}

        /// <summary>
        /// Desc:烧结机停机开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? START_TIME_SIN {get;set;}

        /// <summary>
        /// Desc:烧结机停机结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? STOP_TIME_SIN {get;set;}

        /// <summary>
        /// Desc:烧结机停机时间间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? INTERVAL_TIME_SIN {get;set;}

        /// <summary>
        /// Desc:单辊破碎机停机开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? START_TIME_BM {get;set;}

        /// <summary>
        /// Desc:单辊破碎机停机结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? STOP_TIME_BM {get;set;}

        /// <summary>
        /// Desc:单辊破碎机停机时间间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? INTERVAL_TIME_BM {get;set;}

        /// <summary>
        /// Desc:环冷机停机开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? START_TIME_RC {get;set;}

        /// <summary>
        /// Desc:环冷机停机结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? STOP_TIME_RC {get;set;}

        /// <summary>
        /// Desc:环冷机停机时间间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? INTERVAL_TIME_RC {get;set;}

        /// <summary>
        /// Desc:板式给矿机停机开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? START_TIME_PF {get;set;}

        /// <summary>
        /// Desc:板式给矿机停机结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? STOP_TIME_PF {get;set;}

        /// <summary>
        /// Desc:板式给矿机停机时间间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? INTERVAL_TIME_PF {get;set;}

        /// <summary>
        /// Desc:SF_1皮带停机开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? START_TIME_SF_1 {get;set;}

        /// <summary>
        /// Desc:SF_1皮带停机结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? STOP_TIME_SF_1 {get;set;}

        /// <summary>
        /// Desc:SF_1皮带停机时间间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? INTERVAL_TIME_SF_1 {get;set;}

        /// <summary>
        /// Desc:冷筛机停机开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? START_TIME_SCREEN {get;set;}

        /// <summary>
        /// Desc:冷筛机停机结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? STOP_TIME_SCREEN {get;set;}

        /// <summary>
        /// Desc:冷筛机停机时间间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? INTERVAL_TIME_SCREEN {get;set;}

        /// <summary>
        /// Desc:P_8皮带停机开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? START_TIME_P_8 {get;set;}

        /// <summary>
        /// Desc:P_8皮带停机结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? STOP_TIME_P_8 {get;set;}

        /// <summary>
        /// Desc:P_8皮带停机时间间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? INTERVAL_TIME_P_8 {get;set;}

        /// <summary>
        /// Desc:SJK_1皮带停机开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? START_TIME_SJK_1 {get;set;}

        /// <summary>
        /// Desc:SJK_1皮带停机结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? STOP_TIME_SJK_1 {get;set;}

        /// <summary>
        /// Desc:SJK_1皮带停机时间间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? INTERVAL_TIME_SJK_1 {get;set;}

    }
}
