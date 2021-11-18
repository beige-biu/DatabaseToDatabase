using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class L3MATERIAL_CONFIG
    {
        
        /// <summary>
        /// Desc:原料范围归属编码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int M_TYPE {get;set;}

        /// <summary>
        /// Desc:原料范围归属编码名称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string M_DESC {get;set;}

        /// <summary>
        /// Desc:原料最小值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int CODE_MIN {get;set;}

        /// <summary>
        /// Desc:原料最大值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int CODE_MAX {get;set;}

        /// <summary>
        /// Desc:范围中去除特殊值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CODE_EXTRA {get;set;}

    }
}
