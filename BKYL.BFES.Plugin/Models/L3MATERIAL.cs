using System;
using System.Linq;
using System.Text;


namespace BKYL.BFES.Plugin.Models
{
    public class L3MATERIAL
    {
        /// <summary>
        /// Desc:记录时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:料种编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int M_TYPE { get; set; }

        /// <summary>
        /// Desc:一级物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CODEL1 { get; set; }

        /// <summary>
        /// Desc:二级系统内部物料编码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int CODE { get; set; }

        /// <summary>
        /// Desc:三级物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CODEL3 { get; set; }

        /// <summary>
        /// Desc:物料名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// Desc:物料堆比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double DENSITY { get; set; }

        /// <summary>
        /// Desc:物料水分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double MOISTURE { get; set; }

        /// <summary>
        /// Desc:物料单价 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double PERVALUE { get; set; }

        /// <summary>
        /// Desc:备用编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ITEMCD { get; set; }

        /// <summary>
        /// Desc:取原检化验数据求平均时的取数条数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CALAVG_NUM { get; set; }

        /// <summary>
        /// Desc:取原检化验数据求平均时的延迟时间，单位：小时 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int CAL_DELAYTIME { get; set; }

        /// <summary>
        /// Desc:查询类型：若没有满足查询，是否取最新，1是，0否 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool QUERY_TYPE { get; set; }

        /// <summary>
        /// Desc:与标准成分允许的最大偏离量，超过该偏量即剔除，不参与计算，单位：% 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int DEVIATION_PER { get; set; }
        /// <summary>
        /// Desc:参与偏离剔除的目标化验项，字段必须与原检化验表中化验项字段一致 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FIELDNAMES { get; set; }

        /// <summary>
        /// Desc:计算标准成分时，取模型检化验数据求平均的取数条数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int MODELDATA_NUM { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool FLAG { get; set; }

        /// <summary>
        /// Desc:是否启用偏离条件过滤状态：0否，1是 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool DEVIATION_STATE { get; set; }
    }
}

namespace BKYL.BFES.Plugin.Models.AG
{
    public class L3MATERIAL
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:原料品种 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int M_TYPE {get;set;}

        /// <summary>
        /// Desc:编码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int CODE {get;set;}

        /// <summary>
        /// Desc:三级编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CODEL3 {get;set;}

        /// <summary>
        /// Desc:原料名称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string NAME {get;set;}

        /// <summary>
        /// Desc:密度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double DENSITY {get;set;}

        /// <summary>
        /// Desc:水分 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double MOISTURE {get;set;}

        /// <summary>
        /// Desc:一级编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CODEL1 {get;set;}

        /// <summary>
        /// Desc:价格 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PERVALUE {get;set;}

        /// <summary>
        /// Desc:类型详解 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ITEMCD {get;set;}

    }
}
