using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin
{
    public class BF_TEST
    {
        
        /// <summary>
        /// Desc:组ID：区分不同后台数据组号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int GROUP_ID {get;set;}

        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:属性ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ID {get;set;}

        /// <summary>
        /// Desc:属性描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ID_DESC {get;set;}

        /// <summary>
        /// Desc:属性值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? VALUE {get;set;}

    }
}
