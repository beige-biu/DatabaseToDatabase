using System;
using System.Linq;
using System.Text;

namespace BKYL.WTD.Plugin.Models
{
    public class WTD_AVG_ALARM
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:组号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int GROUPNO {get;set;}

        /// <summary>
        /// Desc:水管号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int PIPENO {get;set;}

        /// <summary>
        /// Desc:水管描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PIPEDESC {get;set;}

        /// <summary>
        /// Desc:报警信息 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ALARMDESC {get;set;}

        /// <summary>
        /// Desc:均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? AVG_VAL {get;set;}

        /// <summary>
        /// Desc:实时值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? CURRENT_VAL { get;set;}

        /// <summary>
        /// 数值标签，-1=小于标准，1=大于标准
        /// </summary>
        public int VAL_FLAG { get; set; }


    }
}
