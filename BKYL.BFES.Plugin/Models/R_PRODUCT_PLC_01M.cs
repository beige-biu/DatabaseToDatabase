using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models
{
    public class R_PRODUCT_PLC_01M
    {

        /// <summary>
        /// Desc:插入时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:成4秤累积产量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_W_C4_ADD { get; set; }

        /// <summary>
        /// Desc:成4秤分钟产量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_W_C4_ACC { get; set; }

        /// <summary>
        /// Desc:成品料仓位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_W_C4 { get; set; }

        /// <summary>
        /// Desc:1#振动筛分机电流 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_EM01_EC { get; set; }

        /// <summary>
        /// Desc:2#振动筛分机电流 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_EM02_EC { get; set; }

        /// <summary>
        /// Desc:铺底-2皮带秤

        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_B_P2 { get; set; }

    }
}
