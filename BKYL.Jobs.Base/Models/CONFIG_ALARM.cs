using System;
using System.Linq;
using System.Text;

namespace BKYL.Jobs.Base.Models
{
    public class CONFIG_ALARM
    {
        
        /// <summary>
        /// Desc:更新时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:报警点ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int ALARM_ID {get;set;}

        /// <summary>
        /// Desc:报警点描述 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ALARM_DESC {get;set;}

        /// <summary>
        /// Desc:报警类型ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int ALARM_TYPE {get;set;}

        /// <summary>
        /// Desc:报警类型描述 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ALARM_TYPE_DESC {get;set;}

        /// <summary>
        /// Desc:低值报警值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Decimal ALARM_RED_L { get; set; }
        /// <summary>
        /// Desc:低值预警值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Decimal ALARM_YELLOW_L { get; set; }

       
        /// <summary>
        /// Desc:高值预警值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Decimal ALARM_YELLOW_H {get;set;}

        /// <summary>
        /// Desc:高值报警值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Decimal ALARM_RED_H {get;set;}

        /// <summary>
        /// Desc:时间字段 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string TIME_FIELD { get; set; }
        /// <summary>
        /// Desc:所属表名 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string TABLE_NAME {get;set;}

        /// <summary>
        /// Desc:所属表内字段名 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string COLUMN_NAME {get;set;}

        /// <summary>
        /// Desc:表频率 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int FRE {get;set;}

        /// <summary>
        /// Desc:是否启用报警 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public bool ISENABLE {get;set;}

        /// <summary>
        /// Desc:上限值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Decimal UP_LIMIT { get; set; }

        /// <summary>
        /// Desc:下限值
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Decimal DOWN_LIMIT { get; set; }

    }
}
