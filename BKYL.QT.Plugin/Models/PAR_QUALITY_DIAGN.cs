using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Models
{
    public class PAR_QUALITY_DIAGN
    {

        /// <summary>
        /// Desc:日期 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:序号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int M_ID { get; set; }

        /// <summary>
        /// Desc:参数名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_NAME { get; set; }

        /// <summary>
        /// Desc:有效范围下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_LOWER { get; set; }

        /// <summary>
        /// Desc:有效范围上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_UP { get; set; }

        /// <summary>
        /// Desc:单位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_CMP { get; set; }

        /// <summary>
        /// Desc:开始时序 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_START_TIME { get; set; }

        /// <summary>
        /// Desc:结束时序 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_END_TIME { get; set; }

        /// <summary>
        /// Desc:更新时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? UPDATESTAMP { get; set; }

    }
}
