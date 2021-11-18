using System;
using System.Linq;
using System.Text;

namespace BKYL.WTD.Plugin.Models
{
    public class BF1_WTD_HFDAY_01D
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int GROUPNO { get; set; }

        /// <summary>
        /// Desc:平均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double HF_AVG_VALUE {get;set;}

        /// <summary>
        /// Desc:最大值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double HF_MAX_VALUE {get;set;}

        /// <summary>
        /// Desc:最大点号名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string HF_MAX_NAME {get;set;}

    }
}
