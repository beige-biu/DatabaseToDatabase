using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.BGBFES7
{
    public class CONFIG_L3ANA_ITEMMAP
    {
        
        /// <summary>
        /// Desc:1：焦炭、球团、烧结、块矿、熔剂、除尘灰；2：铁水；3：炉渣 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? M_TYPE {get;set;}

        /// <summary>
        /// Desc:二级系统字段名 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string L2_ITEMFIELD {get;set;}

        /// <summary>
        /// Desc:三级系统字段名或者化验项目ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string L3_ITEMFIELD {get;set;}

        /// <summary>
        /// Desc:化验项目描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ITEM_DESC {get;set;}

    }
}
