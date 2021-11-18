using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MD_PHY_PARTICLE_INFO_IN
    {
        
        /// <summary>
        /// Desc:插入时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:采样时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SAMPLETIME {get;set;}

        /// <summary>
        /// Desc:报样时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? REOPTTIME {get;set;}

        /// <summary>
        /// Desc:试样号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SAMPLE_NUM {get;set;}

        /// <summary>
        /// Desc:品名代码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NAME_CODE {get;set;}

        /// <summary>
        /// Desc:二级代码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? L2_CODE {get;set;}

        /// <summary>
        /// Desc:物料名 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MATERIAL_NAME {get;set;}

        /// <summary>
        /// Desc:班次 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CLASSES {get;set;}

        /// <summary>
        /// Desc:班别 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CLASS_NUM {get;set;}

        /// <summary>
        /// Desc:料温（二次混合） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MATERIAL_TE {get;set;}

        /// <summary>
        /// Desc:H2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? H2O {get;set;}

        /// <summary>
        /// Desc:粒度>3mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_UP_3 {get;set;}

        /// <summary>
        /// Desc:粒度1-3mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_1_3 {get;set;}

        /// <summary>
        /// Desc:粒度<1mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_LOW_1 {get;set;}

        /// <summary>
        /// Desc:粒度>5mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_UP_5 {get;set;}

        /// <summary>
        /// Desc:(生石灰)粒度<3mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_LOW_3 {get;set;}

        /// <summary>
        /// Desc:配料程序碳调整中焦屑粒度变化调整用1：为铁料： 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? FLAG {get;set;}

        /// <summary>
        /// Desc:粒度相指数及相关性计算使用，1：二混；0计算完成 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? FLAG_GRIT {get;set;}

    }
}
