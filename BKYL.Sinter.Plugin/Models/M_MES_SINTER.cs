using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
  public  class M_MES_SINTER
    {
        /// <summary>
        /// Desc:l2_code 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int l2_CODE { get; set; }

        /// <summary>
        /// Desc:二级原料名称
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string l2_NAME { get; set; }
        public string MAT_PROD_CODE { get; set; }

        public string MAT_SAMPLE_NO { get; set; }
        public string MAT_SAMPLE_TIME { get; set; }

        public int SEQUENCE_NO { get; set; }
    }
}
