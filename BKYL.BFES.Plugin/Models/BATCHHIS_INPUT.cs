using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class BATCHHIS_INPUT
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:物料代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string MAT_CODE {get;set;}

        /// <summary>
        /// Desc:重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MAT_WEIGHT {get;set;}

        /// <summary>
        /// Desc:物料名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MAT_NAME {get;set;}

    }
}
