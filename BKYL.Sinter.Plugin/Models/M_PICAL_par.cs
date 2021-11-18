using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class M_PICAL_PAR
    {
        
        /// <summary>
        /// Desc:漏风率
 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PAR_K1 {get;set;}

        /// <summary>
        /// Desc:烧结机宽度
 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PAR_B {get;set;}

        /// <summary>
        /// Desc:根据过程和操作设定计算修正系数
 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PAR_N {get;set;}

        /// <summary>
        /// Desc:烧结面积有效利用系数
 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PAR_K2 {get;set;}

        /// <summary>
        /// Desc:一混二混匹配取平均时间段
 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PAR_T1 {get;set;}

        /// <summary>
        /// Desc:透气性匹配取平均时间段
 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PAR_T2 {get;set;}

        public DateTime TIMESTAMP { get; set; }

    }
}
