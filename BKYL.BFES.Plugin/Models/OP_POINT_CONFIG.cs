using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class OP_POINT_CONFIG
    {
        
        /// <summary>
        /// Desc:段位编号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int SEG_ID {get;set;}

        /// <summary>
        /// Desc:监测点编号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int POINT_ID {get;set;}

        /// <summary>
        /// Desc:监测点角度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? POINT_ANGLE {get;set;}

        /// <summary>
        /// Desc:扇形角度，用于用户点击横剖面选点 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SECTOR_ANGLE {get;set;}

        /// <summary>
        /// Desc:段位描述，中文 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SEG_DESC_CN {get;set;}

        /// <summary>
        /// Desc:监测点描述，中文 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string POINT_DESC_CN {get;set;}

        /// <summary>
        /// Desc:段位描述，英文 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SEG_DESC_EN {get;set;}

        /// <summary>
        /// Desc:监测点描述，英文 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string POINT_DESC_EN {get;set;}

        public DateTime? UPDATETIME { get; set; }

    }
}
