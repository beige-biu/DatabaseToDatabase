using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class AV01H_TENCROSS_07DAVG
    {
        
        /// <summary>
        /// Desc:数据时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:十字测温 次中心 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_CROSS1 {get;set;}

        /// <summary>
        /// Desc:十字测温 次次中心 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_CROSS2 {get;set;}

        /// <summary>
        /// Desc:十字测温 次边缘 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_CROSS3 {get;set;}

        /// <summary>
        /// Desc:十字测温 边缘 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? A_CROSS4 {get;set;}

    }
}
