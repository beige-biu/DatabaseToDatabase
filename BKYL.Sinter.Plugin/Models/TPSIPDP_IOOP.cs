using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class TPSIPDP_IOOP
    {
        
        /// <summary>
        /// Desc:记录创建责任者 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string REC_CREATOR {get;set;}

        /// <summary>
        /// Desc:记录创建时刻 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string REC_CREATE_TIME {get;set;}

        /// <summary>
        /// Desc:记录修改责任者 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string REC_REVISOR {get;set;}

        /// <summary>
        /// Desc:记录修改时刻 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string REC_REVISE_TIME {get;set;}

        /// <summary>
        /// Desc:生产月份 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string PROD_MON {get;set;}

        /// <summary>
        /// Desc:作业日期 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string WORK_DATE {get;set;}

        /// <summary>
        /// Desc:工艺单元 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string PROC_UNIT {get;set;}

        /// <summary>
        /// Desc:单元号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string UNIT_NO {get;set;}

        /// <summary>
        /// Desc:生产指标项目代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string PROD_ITEM_CODE {get;set;}

        /// <summary>
        /// Desc:生产指标项目值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double PROD_ITEM_VALUE {get;set;}

        /// <summary>
        /// Desc:标志位1 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string FLAG1 {get;set;}

        /// <summary>
        /// Desc:标志位2 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string FLAG2 {get;set;}

        /// <summary>
        /// Desc:标志位3 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string FLAG3 {get;set;}

        /// <summary>
        /// Desc:标志位4 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string FLAG4 {get;set;}

        /// <summary>
        /// Desc:标志位5 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string FLAG5 {get;set;}

        /// <summary>
        /// Desc:标志位6 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string FLAG6 {get;set;}

        /// <summary>
        /// Desc:通信标志 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string COMM_FLAG {get;set;}

        /// <summary>
        /// Desc:通信发送标志 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string COMM_SND_FLAG {get;set;}

        /// <summary>
        /// Desc:通信发送时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string COMM_SND_TIME {get;set;}

        /// <summary>
        /// Desc:通信发送备注 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string COMM_SND_REMARK {get;set;}

        /// <summary>
        /// Desc:通信接收标志 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string COMM_RECV_FLAG {get;set;}

        /// <summary>
        /// Desc:通信接收时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string COMM_RECV_TIME {get;set;}

        /// <summary>
        /// Desc:通信接收备注 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string COMM_RECV_REMARK {get;set;}

    }
}
