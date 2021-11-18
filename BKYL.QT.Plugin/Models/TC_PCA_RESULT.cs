using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Models
{
    public class TC_PCA_RESULT
    {

        /// <summary>
        /// Desc:插入时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_SPE { get; set; }

        /// <summary>
        /// Desc:SPE控制值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_SPE_CT_VALUE { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_T2 { get; set; }

        /// <summary>
        /// Desc:T2控制值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TC_T2_CT_VALUE { get; set; }

    }
}
