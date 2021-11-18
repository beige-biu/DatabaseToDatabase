using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class CONFIG_SYS_VALUE
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:模型编码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string MODLE_ID {get;set;}

        /// <summary>
        /// Desc:序号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int ID {get;set;}

        /// <summary>
        /// Desc:参数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? VALUE {get;set;}

        /// <summary>
        /// Desc:参数描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string DESCRIBE {get;set;}

    }
}
