using System;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Models
{
    public class ALARM_INFORMATION
    {
        
        /// <summary>
        /// Desc:第一次报警时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:报警ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ALARM_ID {get;set;}

        /// <summary>
        /// Desc:报警描述 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ALARM_DESC {get;set;}

        /// <summary>
        /// Desc:报警类型 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int ALARM_TYPE {get;set;}

        /// <summary>
        /// Desc:报警类型描述 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ALARM_TYPE_DESC {get;set;}

        /// <summary>
        /// Desc:报警值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Decimal ALARM_LASTVALUE {get;set;}

        /// <summary>
        /// Desc:更新时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime UPDATETIMESTAMP {get;set;}

        /// <summary>
        /// Desc:报警参数 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string PARAMS {get;set;}

        /// <summary>
        /// Desc:报警等级 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public bool ALARM_CHILDID {get;set;}

        /// <summary>
        /// Desc:工序ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? PROCESS_ID {get;set;}

        /// <summary>
        /// Desc:工序描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PROCESS_DESC {get;set;}

    }
}
