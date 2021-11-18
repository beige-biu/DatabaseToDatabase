using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class CONFIG_SINCAL_VALUE
    {
        
        /// <summary>
        /// Desc:更新时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:下料自动标志0：退出；1:投入 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? AUTO_BLANK_FLAG {get;set;}

        /// <summary>
        /// Desc:碳自动调整标志0：退出；1:投入 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? AUTO_C_FLAG {get;set;}

        /// <summary>
        /// Desc:碱度自动调整标志0：退出；1:投入 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? AUTO_R_FLAG {get;set;}

        /// <summary>
        /// Desc:烧返配比自动标志0：退出；1:投入 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? MCR_FLAG {get;set;}

    }
}
