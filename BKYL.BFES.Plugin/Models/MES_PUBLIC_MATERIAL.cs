using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models
{
    public class MES_PUBLIC_MATERIAL
    {
        
        /// <summary>
        /// Desc:原料代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int MAT_CODE {get;set;}

        /// <summary>
        /// Desc:英文名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string E_NAME {get;set;}

        /// <summary>
        /// Desc:中文名称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string C_NAME {get;set;}

        /// <summary>
        /// Desc:原料代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int C_TYPE {get;set;}

        /// <summary>
        /// Desc:密度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double DENSITY {get;set;}

        /// <summary>
        /// Desc:原料说明 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MAT_DESC {get;set;}

        /// <summary>
        /// Desc:MES代码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MES_CODE {get;set;}

    }
    public class MES_PUBLIC_MATERIAL_2
    {

        /// <summary>
        /// Desc:原料代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int MAT_CODE { get; set; }

        /// <summary>
        /// Desc:英文名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string E_NAME { get; set; }

        /// <summary>
        /// Desc:中文名称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string C_NAME { get; set; }

        /// <summary>
        /// Desc:原料代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int C_TYPE { get; set; }

        /// <summary>
        /// Desc:密度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double DENSITY { get; set; }

        /// <summary>
        /// Desc:原料说明 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MAT_DESC { get; set; }

        /// <summary>
        /// Desc:MES代码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MES_CODE { get; set; }

    }
}
