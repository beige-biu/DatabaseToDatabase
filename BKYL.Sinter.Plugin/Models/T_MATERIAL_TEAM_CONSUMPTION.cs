using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class T_MATERIAL_TEAM_CONSUMPTION
    {
        
        /// <summary>
        /// Desc:记录日期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? RECORD_DATE {get;set;}

        /// <summary>
        /// Desc:班次 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SHIFTS {get;set;}

        /// <summary>
        /// Desc:班组 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TAEM {get;set;}

        /// <summary>
        /// Desc:开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? WORK_BEGIN_TIME {get;set;}

        /// <summary>
        /// Desc:结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? WORK_END_TIME {get;set;}

        /// <summary>
        /// Desc:物料代码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MATERIAL_CODE {get;set;}

        /// <summary>
        /// Desc:物料名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MATERIAL_NAME {get;set;}

        /// <summary>
        /// Desc:物料消耗 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? CONSUMPTION_SUM {get;set;}

        /// <summary>
        /// Desc:物料单耗 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? SPECIFIC_CONSUMPTION {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int ID {get;set;}

    }
}
