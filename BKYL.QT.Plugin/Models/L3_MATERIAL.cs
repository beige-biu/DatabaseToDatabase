using System;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Models
{
    public class L3_MATERIAL
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:一级代码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_L1_CODE {get;set;}

        /// <summary>
        /// Desc:二级代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int M_L2_CODE {get;set;}

        /// <summary>
        /// Desc:三级代码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_L3_CODE {get;set;}

        /// <summary>
        /// Desc:物料描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_MAT_DESC {get;set;}

        /// <summary>
        /// Desc:二级名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_L2_NAME {get;set;}

        /// <summary>
        /// Desc:单价 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_PERVALUE {get;set;}

        /// <summary>
        /// Desc:粒度当量值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_C1 {get;set;}

        /// <summary>
        /// Desc:烧损值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_LOI {get;set;}

        /// <summary>
        /// Desc:烧损当量值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_C2 {get;set;}

        /// <summary>
        /// Desc:粒度补充系数 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_LM {get;set;}

    }
}
