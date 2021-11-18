using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models
{
    public class TC_YIELD_01H
    {

        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:产量(吨) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_VALUE { get; set; }

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
