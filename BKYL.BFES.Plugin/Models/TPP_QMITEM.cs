using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class TPP_QMITEM
    {
        
        /// <summary>
        /// Desc:主键 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string C_ID {get;set;}

        /// <summary>
        /// Desc:样号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string C_SMPID {get;set;}

        /// <summary>
        /// Desc:炉号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_HEAT_NO {get;set;}

        /// <summary>
        /// Desc:试样类型 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string C_SMP_TYPE {get;set;}

        /// <summary>
        /// Desc:顺序号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_INSLOTCHAR {get;set;}

        /// <summary>
        /// Desc:项目编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_ITEM_CODE {get;set;}

        /// <summary>
        /// Desc:检验项目 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_ITEM_NAME {get;set;}

        /// <summary>
        /// Desc:检验结果 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_TEST_RST {get;set;}

        /// <summary>
        /// Desc:单位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_UNIT {get;set;}

        /// <summary>
        /// Desc:修约值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_TEST_RST_RD {get;set;}

        /// <summary>
        /// Desc:时间戳 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? C_TIMESTAMP {get;set;}

    }
}
