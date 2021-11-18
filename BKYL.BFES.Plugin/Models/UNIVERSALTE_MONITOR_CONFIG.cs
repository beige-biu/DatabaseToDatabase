using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models
{
    public class UNIVERSALTE_MONITOR_CONFIG
    {
        
        /// <summary>
        /// Desc:组号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int GROUPNO {get;set;}

        /// <summary>
        /// Desc:点号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int POINTNO {get;set;}

        /// <summary>
        /// Desc:点类型 0 炉壳 1壁体 2 铁沟 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int POINTTYPE {get;set;}

        /// <summary>
        /// Desc:组描述 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string GROUPDESC {get;set;}

        /// <summary>
        /// Desc:组英文描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string GROUPDESC_EN {get;set;}

        /// <summary>
        /// Desc:点描述 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string POINTDESC {get;set;}

        /// <summary>
        /// Desc:点英文描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string POINTDESC_EN {get;set;}

        /// <summary>
        /// Desc:点地址 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ADDRESS {get;set;}

        /// <summary>
        /// Desc:高度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Single HEIGHT {get;set;}

        /// <summary>
        /// Desc:角度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Single ANGLE {get;set;}

        /// <summary>
        /// Desc:温度最低值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int TELOW {get;set;}

        /// <summary>
        /// Desc:温度最高值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int TEHEIGHT {get;set;}

        /// <summary>
        /// Desc:报警类型 0 不报警 1报警 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public bool ALARMTYPE {get;set;}

        /// <summary>
        /// Desc:报警低限 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double ALARMLOW {get;set;}

        /// <summary>
        /// Desc:报警高限 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double ALARMHEIGHT {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Single DEPTH {get;set;}

    }
}
