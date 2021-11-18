using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class TX_IO_LTXC_SINTER_SUB
    {
        
        /// <summary>
        /// Desc:样号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SMP_NO {get;set;}

        /// <summary>
        /// Desc:炉号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string HEAT_NO {get;set;}

        /// <summary>
        /// Desc:试样类型‘000a’ 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SMP_TYPE {get;set;}

        /// <summary>
        /// Desc:检验项目 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string SMP_ITEM {get;set;}

        /// <summary>
        /// Desc:检验结果 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TEST_RST {get;set;}

        /// <summary>
        /// Desc:修约值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TEST_RST_RD {get;set;}

        /// <summary>
        /// Desc:序列号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string SERIAL_NO {get;set;}

    }
}
