using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models
{
    public class MES_TQMIR23_IOOP
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
        /// Desc:归档标记 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ARCHIVE_FLAG {get;set;}

        /// <summary>
        /// Desc:修改/删除责任者 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string DU_MAKER {get;set;}

        /// <summary>
        /// Desc:修改/删除时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string DU_TIME {get;set;}

        /// <summary>
        /// Desc:版次 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int VERSION {get;set;}

        /// <summary>
        /// Desc:归档邮戳号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ARCH_STAMP_NO {get;set;}

        /// <summary>
        /// Desc:流水序号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public long SEQUENCE_NO {get;set;}

        /// <summary>
        /// Desc:物料试样号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string MAT_SAMPLE_NO {get;set;}

        /// <summary>
        /// Desc:分析类别代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ANALYSIS_TYPE_CODE {get;set;}

        /// <summary>
        /// Desc:分析项目代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ANALYSE_ITEM_CODE {get;set;}

        /// <summary>
        /// Desc:分析值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double ANALYSIS_VALUE {get;set;}

        /// <summary>
        /// Desc:标志位1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FLAG1 {get;set;}

        /// <summary>
        /// Desc:标志位2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FLAG2 {get;set;}

        /// <summary>
        /// Desc:标志位3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FLAG3 {get;set;}

        /// <summary>
        /// Desc:标志位4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FLAG4 {get;set;}

        /// <summary>
        /// Desc:标志位5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FLAG5 {get;set;}

        /// <summary>
        /// Desc:标志位6 
        /// Default:- 
        /// Nullable:True 
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
