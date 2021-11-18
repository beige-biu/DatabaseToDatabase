using System;
using System.Linq;
using System.Text;

namespace BKYL.Jobs.Base.Models
{
    public class ALARM_INFO
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:报警ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int ALARM_ID {get;set;}

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
        /// Desc:报警级别(1预警2报警) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int ALARM_LEVEL {get;set;} 


        /// <summary>
        /// Desc:备注 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string REMARK {get;set;}
        /// <summary>
        /// Desc:报警发生时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime ALARM_TIMESTAMP { get; set; }
        /// <summary>
        /// Desc:报警值
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal ALARM_VALUE { get; set; }

      

    }
}
