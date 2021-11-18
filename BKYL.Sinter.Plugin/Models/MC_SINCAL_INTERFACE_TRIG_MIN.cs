using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MC_SINCAL_INTERFACE_TRIG_MIN
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:序号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int ID {get;set;}

        /// <summary>
        /// Desc:料仓号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_ID {get;set;}

        /// <summary>
        /// Desc:料种 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SINCAL_COOD {get;set;}

        /// <summary>
        /// Desc:下料口 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? BLANKING_ID {get;set;}

        /// <summary>
        /// Desc:料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ML {get;set;}

        /// <summary>
        /// Desc:配矿配比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_RATIO {get;set;}

        /// <summary>
        /// Desc:矿配比设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_SP {get;set;}

        /// <summary>
        /// Desc:矿配比当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_PV {get;set;}

        /// <summary>
        /// Desc:矿配比百分比（干） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_DRY_BILL {get;set;}

        /// <summary>
        /// Desc:分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_TC {get;set;}

        /// <summary>
        /// Desc:水分设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_H2O_SP {get;set;}

        /// <summary>
        /// Desc:水分当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_H2O_PV {get;set;}

        /// <summary>
        /// Desc:下料设定值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_BK_SP {get;set;}

        /// <summary>
        /// Desc:下料当前值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_BK_PV {get;set;}

        /// <summary>
        /// Desc:下料偏差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_BK_DV {get;set;}

        /// <summary>
        /// Desc:下料百分比（湿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_WET_BILL {get;set;}

        /// <summary>
        /// Desc:设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_FRE {get;set;}

        /// <summary>
        /// Desc:混匀矿累计（t） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_ATIVE {get;set;}

        /// <summary>
        /// Desc:料口配比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_RATIO_BUNKER {get;set;}
        /// <summary>
        /// Desc:湿配比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SINCAL_ORE_WET { get; set; }

    }
}
