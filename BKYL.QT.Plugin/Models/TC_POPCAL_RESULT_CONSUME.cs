using System;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Models
{
    public class TC_POPCAL_RESULT_CONSUME
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:原料编号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int DT_CODE {get;set;}

        /// <summary>
        /// Desc:原料消耗 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? DT_VALUE {get;set;}

        /// <summary>
        /// Desc:占比 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double DT_RATIO {get;set;}

        /// <summary>
        /// Desc:0：夜班，1：白班 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string DT_CLASS_TYPE {get;set;}            
    }
}

