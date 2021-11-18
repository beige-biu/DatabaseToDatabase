using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models
{
    public class AV05M_LININGTC_ALARMINFO
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
        public string POINTNAME {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ALARMINFO {get;set;}
        public double DATAVALUE { get; set; }
        public int FLAG { get; set; }
        /// <summary>
        /// 热震开始时间
        /// </summary>
        public DateTime STARTTIME { get; set; }
        /// <summary>
        /// 持续时间
        /// </summary>
        public double REMAINTIME { get; set; }
        public double HEIGHT { get; set; }
        public double ANGLE { get; set; }
    }
   
}
