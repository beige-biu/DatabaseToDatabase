using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models
{
    public class CONFIG_BFANADATA
    {
        
        /// <summary>
        /// Desc:序号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ID {get;set;}

        /// <summary>
        /// Desc:描述 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string D_DESC {get;set;}

        /// <summary>
        /// Desc:表名 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TABLENAME {get;set;}

        /// <summary>
        /// Desc:字段名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FIELDNAME {get;set;}

        /// <summary>
        /// Desc:数据源时间范围 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TIMESQL {get;set;}

        /// <summary>
        /// Desc:过滤条件 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FILTERSQL {get;set;}

        /// <summary>
        /// Desc:时间字段 ，如果按1方式获取数据源，需要配置该字段 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TIMEFIELD {get;set;}

        /// <summary>
        /// Desc:计算方式 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CALTYPE {get;set;}

        /// <summary>
        /// Desc:0:默认(单字段处理方式)，1:先取当日的，取不到获取昨天的，如果再取不到，取满足条件的最新数据 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int DEALTYPE {get;set;}

        /// <summary>
        /// Desc:目标字段名 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TFIELDNAME {get;set;}

    }
}
