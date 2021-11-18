using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models
{
    public class MES_TMMIRF9_IOOP
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
        /// Desc:作业日期 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string WORK_DATE {get;set;}

        /// <summary>
        /// Desc:出铁开始时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string OUT_START_TIME {get;set;}

        /// <summary>
        /// Desc:单元号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string UNIT_NO {get;set;}

        /// <summary>
        /// Desc:铁次 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string IRON_DEGR {get;set;}

        /// <summary>
        /// Desc:TPC号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string TPC_NO {get;set;}

        /// <summary>
        /// Desc:磅单号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string WEIGH_NO {get;set;}

        /// <summary>
        /// Desc:计量申请号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string WEIGH_APP_NO {get;set;}

        /// <summary>
        /// Desc:车号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string CAR_NO {get;set;}

        /// <summary>
        /// Desc:毛重 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double GROSS_WT {get;set;}

        /// <summary>
        /// Desc:皮重 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double TARE_WT {get;set;}

        /// <summary>
        /// Desc:重量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double NET_WEIGHT {get;set;}

        /// <summary>
        /// Desc:重罐过磅时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string PONDER_TIME {get;set;}

        /// <summary>
        /// Desc:皮重时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string TWTIME {get;set;}

        /// <summary>
        /// Desc:司磅员 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string WEIGH_BY {get;set;}

        /// <summary>
        /// Desc:磅站 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string WEIGH_SITE {get;set;}

        /// <summary>
        /// Desc:去向 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string DEST_GF {get;set;}

        /// <summary>
        /// Desc:备注 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string REMARK {get;set;}

        /// <summary>
        /// Desc:发送人 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string SND_PERSON {get;set;}

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
