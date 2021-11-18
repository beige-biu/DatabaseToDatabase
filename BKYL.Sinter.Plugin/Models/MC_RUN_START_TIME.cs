using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MC_RUN_START_TIME
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string INSTA_CODE {get;set;}

        /// <summary>
        /// Desc:运行开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? RUN_BEGINTIME {get;set;}

        /// <summary>
        /// Desc:运行结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? RUN_ENDTIME {get;set;}

        /// <summary>
        /// Desc:时间间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? INTERVAL_TIME {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

    }
}
