using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MES_TQMIR21_IOOP
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
        /// Desc:取样类别代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string SAMPLE_TYPE_CODE {get;set;}

        /// <summary>
        /// Desc:进厂批次号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string IN_BATCH_NO {get;set;}

        /// <summary>
        /// Desc:船名（中文） 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string SHIP_CNAME {get;set;}

        /// <summary>
        /// Desc:物料品名批号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string MAT_BATCH_NO {get;set;}

        /// <summary>
        /// Desc:物料品名代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string MAT_PROD_CODE {get;set;}

        /// <summary>
        /// Desc:供货单位中文名称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string SUPPLY_DEPT_CNAME {get;set;}

        /// <summary>
        /// Desc:合同号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ORDER_NO {get;set;}

        /// <summary>
        /// Desc:物料到达时刻 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string MAT_ARRI_TIME {get;set;}

        /// <summary>
        /// Desc:物料取样时刻 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string MAT_SAMPLE_TIME {get;set;}

        /// <summary>
        /// Desc:物料取样日期 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string MAT_SAMPLE_DATE {get;set;}

        /// <summary>
        /// Desc:物料取样班次 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string MAT_SAMPLE_SHIFT {get;set;}

        /// <summary>
        /// Desc:物料取样班别 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string MAT_SAMPLE_TEAM {get;set;}

        /// <summary>
        /// Desc:卸料开始时刻 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string UNLOAD_TIME_START {get;set;}

        /// <summary>
        /// Desc:卸料结束时刻 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string UNLOAD_TIME_END {get;set;}

        /// <summary>
        /// Desc:样序号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int SAMPLE_SEQ_NO {get;set;}

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
        /// Desc:物料取样位置 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string MAT_SAMPLE_POS {get;set;}

        /// <summary>
        /// Desc:罐次号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string TRE_TPC_NO {get;set;}

        /// <summary>
        /// Desc:铁口号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string IRON_MOUTH_NO {get;set;}

        /// <summary>
        /// Desc:炉次号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int IR_TAP_NO {get;set;}

        /// <summary>
        /// Desc:TPC号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string TPC_NO {get;set;}

        /// <summary>
        /// Desc:试样委托号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string SAMPLE_ENTR_NO {get;set;}

        /// <summary>
        /// Desc:试样说明 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string SAMPLE_DESC {get;set;}

        /// <summary>
        /// Desc:加密等级 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ENCR_LEVEL {get;set;}

        /// <summary>
        /// Desc:加密后代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ENCR_CODE_END {get;set;}

        /// <summary>
        /// Desc:九位码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string CODE_9 {get;set;}

        /// <summary>
        /// Desc:卸货量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double UNLOAD_QTY {get;set;}

        /// <summary>
        /// Desc:委托类别代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string WT_TYPE_CODE {get;set;}

        /// <summary>
        /// Desc:取样地点代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string AREA_TYPE_CODE {get;set;}

        /// <summary>
        /// Desc:分析完成标志 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ANALYSE_FLAG {get;set;}

        /// <summary>
        /// Desc:判定结束标志 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string END_FLAG {get;set;}

        /// <summary>
        /// Desc:堆号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string PILE_NO {get;set;}

        /// <summary>
        /// Desc:订购批号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ORDER_LOT_NO {get;set;}

        /// <summary>
        /// Desc:项次 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ITEMS_SEQ {get;set;}

        /// <summary>
        /// Desc:装运批次号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string SHIPMENT_BATCH_NO {get;set;}

        /// <summary>
        /// Desc:船只ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string SHIP_ID {get;set;}

        /// <summary>
        /// Desc:库区代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string STOCK_CODE {get;set;}

        /// <summary>
        /// Desc:库位/料条代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string STOCK_LOC_CODE {get;set;}

        /// <summary>
        /// Desc:物料取样地点 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string MAT_SAMPLE_LOCATION {get;set;}

        /// <summary>
        /// Desc:分析类别代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ANALYSIS_TYPE_CODE {get;set;}

        /// <summary>
        /// Desc:质检员 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string QLY_CHECKER {get;set;}

        /// <summary>
        /// Desc:物料分析日期 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string MAT_ANALYSE_DATE {get;set;}

        /// <summary>
        /// Desc:物料分析时刻 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string MAT_ANALYSE_TIME {get;set;}

        /// <summary>
        /// Desc:物料分析班次 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string MAT_ANALYSE_SHIFT {get;set;}

        /// <summary>
        /// Desc:物料分析班别 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string MAT_ANALYSE_TEAM {get;set;}

        /// <summary>
        /// Desc:分析报出时刻 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ANALYSE_CENTER_TIME {get;set;}

        /// <summary>
        /// Desc:组批号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string BATCH_NO1 {get;set;}

        /// <summary>
        /// Desc:铁次 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string IRON_DEGR {get;set;}

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
