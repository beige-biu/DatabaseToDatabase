using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class AV30S
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:风压 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BP30S {get;set;}

        /// <summary>
        /// Desc:顶压 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TP30S {get;set;}

        /// <summary>
        /// Desc:风量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BV30S {get;set;}

        /// <summary>
        /// Desc:东北顶温TE1101->_A_TTOP1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP130S {get;set;}

        /// <summary>
        /// Desc:东南顶温TE1102->_A_TTOP2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP230S {get;set;}

        /// <summary>
        /// Desc:西南顶温TE1103->_A_TTOP3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP330S {get;set;}

        /// <summary>
        /// Desc:西北顶温TE1104->_A_TTOP4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOP430S {get;set;}

        /// <summary>
        /// Desc:炉顶煤气CO含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_CO30S {get;set;}

        /// <summary>
        /// Desc:炉顶煤气CO2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_CO230S {get;set;}

        /// <summary>
        /// Desc:炉顶煤气H2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_N230S {get;set;}

        /// <summary>
        /// Desc:炉顶煤气N2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_H230S {get;set;}

        /// <summary>
        /// Desc:炉顶煤气CH4含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_CH430S {get;set;}

        /// <summary>
        /// Desc:料尺1实时值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SLE130S {get;set;}

        /// <summary>
        /// Desc:料尺2实时值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SLE230S {get;set;}

        /// <summary>
        /// Desc:雷达探尺 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_RADER {get;set;}

        /// <summary>
        /// Desc:料尺4实时值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SLE430S {get;set;}

        /// <summary>
        /// Desc:料尺5实时值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SLE530S {get;set;}

        /// <summary>
        /// Desc:料尺1降尺信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool A_SLE1STATUS {get;set;}

        /// <summary>
        /// Desc:料尺2降尺信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool A_SLE2STATUS {get;set;}

        /// <summary>
        /// Desc:料尺3工作状态 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool A_SLE3STATUS {get;set;}

        /// <summary>
        /// Desc:鼓风湿度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_MOI {get;set;}

        /// <summary>
        /// Desc:1东铁口铁水温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_HMT1 {get;set;}

        /// <summary>
        /// Desc:2西铁口铁水温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_HMT2 {get;set;}

        /// <summary>
        /// Desc:料尺1提尺信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool A_SLE1_UP {get;set;}

        /// <summary>
        /// Desc:料尺2提尺信号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool A_SLE2_UP {get;set;}

        /// <summary>
        /// Desc:顶温最大值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TTOPMM30S {get;set;}

        /// <summary>
        /// Desc:矿石1仓料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SORE1_CS {get;set;}

        /// <summary>
        /// Desc:矿石2仓料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SORE2_CS {get;set;}

        /// <summary>
        /// Desc:矿石3仓料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SORE3_CS {get;set;}

        /// <summary>
        /// Desc:矿石4仓料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SORE4_CS {get;set;}

        /// <summary>
        /// Desc:矿石5仓料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SORE5_CS {get;set;}

        /// <summary>
        /// Desc:矿石6仓料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SORE6_CS {get;set;}

        /// <summary>
        /// Desc:矿石7仓料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SORE7_CS {get;set;}

        /// <summary>
        /// Desc:矿石8仓料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SORE8_CS {get;set;}

        /// <summary>
        /// Desc:矿石9仓料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SORE9_CS {get;set;}

        /// <summary>
        /// Desc:矿石10仓料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SORE10_CS {get;set;}

        /// <summary>
        /// Desc:矿石11仓料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SORE11_CS {get;set;}

        /// <summary>
        /// Desc:矿石12仓料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SORE12_CS {get;set;}

        /// <summary>
        /// Desc:焦炭1仓料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SCKE1_CS {get;set;}

        /// <summary>
        /// Desc:焦炭2仓料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SCKE2_CS {get;set;}

        /// <summary>
        /// Desc:焦炭3仓料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SCKE3_CS {get;set;}

        /// <summary>
        /// Desc:焦炭4仓料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SCKE4_CS {get;set;}

        /// <summary>
        /// Desc:焦炭5仓料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_SCKE5_CS {get;set;}

    }
}
