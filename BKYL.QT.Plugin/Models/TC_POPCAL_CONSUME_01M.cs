using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Models
{
    public class TC_POPCAL_CONSUME_01M
    {

        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:原料编码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int TC_CODE { get; set; }

        /// <summary>
        /// Desc:原料消耗 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_VALUE { get; set; }

        /// <summary>
        /// Desc:仓号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int TC_BARNID { get; set; }

    }
}
