using System;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Models
{
    public class TC_RUN_RATE
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:辊压系统运行率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double? TC_ROLLING {get;set;}

        /// <summary>
        /// Desc:预配料系统运行率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double? TC_PRE_MATERIAL {get;set;}

        /// <summary>
        /// Desc:配料系统运行率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double? TC_MATERIAL {get;set;}

        /// <summary>
        /// Desc:干燥系统运行率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double? TC_DRY {get;set;}

        /// <summary>
        /// Desc:混合系统运行率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double? TC_BLEND {get;set;}

        /// <summary>
        /// Desc:造球系统运行率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double? TC_PELLET {get;set;}

        /// <summary>
        /// Desc:焙烧系统运行率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double? TC_ROAS {get;set;}

        /// <summary>
        /// Desc:成品系统运行率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public double? TC_PRODUCT {get;set;}

    }
}
