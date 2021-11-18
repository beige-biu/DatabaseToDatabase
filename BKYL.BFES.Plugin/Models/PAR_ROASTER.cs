using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models
{
    public class PAR_ROASTER
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
        public Double? M_EE_LOWER { get; set; }

        /// <summary>
        /// Desc:有效范围上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_EE_UP { get; set; }

        /// <summary>
        /// Desc:理想范围下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_IL_LOWER { get; set; }

        /// <summary>
        /// Desc:理想范围上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_IL_UP { get; set; }

        /// <summary>
        /// Desc:单位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_UNIT { get; set; }

        /// <summary>
        /// Desc:亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_FEO_BRA { get; set; }

        /// <summary>
        /// Desc:抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CS_BRA { get; set; }

        /// <summary>
        /// Desc:转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_DRIX_BRA { get; set; }

        /// <summary>
        /// Desc:料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_B_S_BRA { get; set; }

        /// <summary>
        /// Desc:其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_OTHER_BRA { get; set; }

    }
}
