using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class T_MATERIAL_METERING_MAP
    {
        
        /// <summary>
        /// Desc:料仓序号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int MAP_ID {get;set;}

        /// <summary>
        /// Desc:料仓仓号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string DEVICE_NO {get;set;}

        /// <summary>
        /// Desc:料仓名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string DEVICE_NAME {get;set;}

        /// <summary>
        /// Desc:料仓描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string DEVICE_DESCR {get;set;}

        /// <summary>
        /// Desc:料仓料种 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MAP_MAT_CODE {get;set;}

        /// <summary>
        /// Desc:料仓物料名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MAP_MAT_NAME {get;set;}

        /// <summary>
        /// Desc:开始使用时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? MAP_BEGIN_TIME {get;set;}

        /// <summary>
        /// Desc:结束使用时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? MAP_END_TIME {get;set;}

        /// <summary>
        /// Desc:修改日期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? CREATE_DATE {get;set;}

        /// <summary>
        /// Desc:修改人 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CREATER {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MAP_STAT {get;set;}

    }
}
