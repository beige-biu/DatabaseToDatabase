using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class TPP_MATERIALOUT
    {
        
        /// <summary>
        /// Desc:收料顺序号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string C_RID {get;set;}

        /// <summary>
        /// Desc:工厂编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_FACTORY {get;set;}

        /// <summary>
        /// Desc:工厂描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_FACTORYESC {get;set;}

        /// <summary>
        /// Desc:工作中心编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_WORKCENTERID {get;set;}

        /// <summary>
        /// Desc:工作中心描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_WORKCENTERDESC {get;set;}

        /// <summary>
        /// Desc:工位编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_STATIONID {get;set;}

        /// <summary>
        /// Desc:工位描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_STATIONDESC {get;set;}

        /// <summary>
        /// Desc:库房编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_STORAGE {get;set;}

        /// <summary>
        /// Desc:库房描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_STORAGEDES {get;set;}

        /// <summary>
        /// Desc:物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_MATERIALID {get;set;}

        /// <summary>
        /// Desc:物料描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_MATERIALNAME {get;set;}

        /// <summary>
        /// Desc:移动类型 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_MOVETYPE {get;set;}

        /// <summary>
        /// Desc:作业计划号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_PLANID {get;set;}

        /// <summary>
        /// Desc:生产订单号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_OPERPLANID {get;set;}

        /// <summary>
        /// Desc:凭证单号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_ORDERNUMBRER {get;set;}

        /// <summary>
        /// Desc:收料批次(炉次) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_BATCHNUMBER {get;set;}

        /// <summary>
        /// Desc:罐号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_BATCHUNIT {get;set;}

        /// <summary>
        /// Desc:炉号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_STOVEID {get;set;}

        /// <summary>
        /// Desc:水份 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? N_H2O {get;set;}

        /// <summary>
        /// Desc:批次等级 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_BATCHRANK {get;set;}

        /// <summary>
        /// Desc:样本编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_SAMPLEID {get;set;}

        /// <summary>
        /// Desc:来料批次 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_COMEBATCH {get;set;}

        /// <summary>
        /// Desc:数据状态 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_STATUS {get;set;}

        /// <summary>
        /// Desc:是否冻结 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_ISFREE {get;set;}

        /// <summary>
        /// Desc:记账状态 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_ACCSTATE {get;set;}

        /// <summary>
        /// Desc:收料重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? N_QUANTITY {get;set;}

        /// <summary>
        /// Desc:湿基重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_WETQUALITY {get;set;}

        /// <summary>
        /// Desc:理论重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? N_THEORYQUANTITY {get;set;}

        /// <summary>
        /// Desc:重量单位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_UNIT {get;set;}

        /// <summary>
        /// Desc:收料日期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_ROWDATE {get;set;}

        /// <summary>
        /// Desc:记账日期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string D_ACCDATE {get;set;}

        /// <summary>
        /// Desc:班组 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_TEAM {get;set;}

        /// <summary>
        /// Desc:班次 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_SHIFT {get;set;}

        /// <summary>
        /// Desc:计量状态 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_MEASURESTATE {get;set;}

        /// <summary>
        /// Desc:出铁计划 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_TAPPINGPLANID {get;set;}

        /// <summary>
        /// Desc:出铁顺序 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? N_IRONSEQUENCE {get;set;}

        /// <summary>
        /// Desc:出铁口 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_TAPPINGHOLE {get;set;}

        /// <summary>
        /// Desc:单罐出铁开始 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_IRONTIMESTART {get;set;}

        /// <summary>
        /// Desc:单罐出铁结束 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_IRONTIMEEND {get;set;}

        /// <summary>
        /// Desc:出铁开口时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string D_ROWSTARTDATE {get;set;}

        /// <summary>
        /// Desc:出铁堵口时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string D_ROWENDDATE {get;set;}

        /// <summary>
        /// Desc:操作人 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_STAFF {get;set;}

        /// <summary>
        /// Desc:备注 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_REMARK {get;set;}

        /// <summary>
        /// Desc:时间戳 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? C_TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:扩展1(铁水移动类型占用：101) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_SW01 {get;set;}

        /// <summary>
        /// Desc:扩展2(收料皮带) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_SW02 {get;set;}

        /// <summary>
        /// Desc:扩展3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_SW03 {get;set;}

        /// <summary>
        /// Desc:扩展4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_SW04 {get;set;}

        /// <summary>
        /// Desc:扩展5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_SW05 {get;set;}

        /// <summary>
        /// Desc:收料方式 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_STYLE {get;set;}

        /// <summary>
        /// Desc:操作时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_OPDATETIME {get;set;}

        /// <summary>
        /// Desc:是否自动(0人工1采集) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_ISAUTO {get;set;}

    }
}
