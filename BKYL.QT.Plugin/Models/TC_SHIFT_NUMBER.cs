using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Models
{
    public class TC_SHIFT_NUMBER
    {

        /// <summary>
        /// Desc:日期时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:班次号(甲、乙、丙、丁) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string TC_SHIT_NUMBER { get; set; }

        /// <summary>
        /// Desc:班名称(白班、夜班) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string TC_SHIT_NAME { get; set; }
    }
}
