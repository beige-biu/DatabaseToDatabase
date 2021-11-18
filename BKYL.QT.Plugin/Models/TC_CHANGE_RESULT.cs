using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Models
{
    public class TC_CHANGE_RESULT
    {

        /// <summary>
        /// Desc:日期 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:SEQ数据顺序(总次数) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int M_ID { get; set; }

        /// <summary>
        /// Desc:前配方 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_PF_NAME { get; set; }

        /// <summary>
        /// Desc:单次次数默认1 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int M_NUMBER { get; set; }

        /// <summary>
        /// Desc:配方 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string M_NAME { get; set; }

    }

}
