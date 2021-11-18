using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class TPS_MEASUREMAIN
    {
        
        /// <summary>
        /// Desc:计量码单 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string C_MEASUREDOCID {get;set;}

        /// <summary>
        /// Desc:计量起始日期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_MEASUREBEGINDATE {get;set;}

        /// <summary>
        /// Desc:主运载工具车号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_CARRYTOOLMAINID {get;set;}

        /// <summary>
        /// Desc:计量性质1:进厂2:调拨3:出厂4:退货5.回皮6:秤斗重8:其他 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_MEASUREPROPERTIES {get;set;}

        /// <summary>
        /// Desc:容器编号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_CONTAINERID {get;set;}

        /// <summary>
        /// Desc:计量方式编码(一毛一皮/定期皮) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string I_MEASUREFASHION {get;set;}

        /// <summary>
        /// Desc:运载工具类型编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_CARRYTOOLSTYPEID {get;set;}

        /// <summary>
        /// Desc:移动类型 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_GOODSMOVETYPE {get;set;}

        /// <summary>
        /// Desc:计量完成标示(0:未完成;1:已完成;3计质量作废;7智能物流作废;88:铁水改派;99铁水退罐) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? I_MSRFINISHFLAG {get;set;}

        /// <summary>
        /// Desc:订单ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_ORDERID {get;set;}

        /// <summary>
        /// Desc:毛重计量时间(yyyy-MM-dd HH:mm:ss) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_GROSSDATETIME {get;set;}

        /// <summary>
        /// Desc:皮重计量时间(yyyy-MM-dd HH:mm:ss) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_TAREDATETIME {get;set;}

        /// <summary>
        /// Desc:净重T 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? N_NETWEIGHT {get;set;}

        /// <summary>
        /// Desc:净重计量时间(yyyy-MM-dd HH:mm:ss) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string D_NETDATETIME {get;set;}

        /// <summary>
        /// Desc:发方储位ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_SENDSTORAGEID {get;set;}

        /// <summary>
        /// Desc:收方储位ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_RECEIVESTORAGEID {get;set;}

        /// <summary>
        /// Desc:库房卸货标识(0:未卸1已卸2已装) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_AFFIRMFLAG {get;set;}

        /// <summary>
        /// Desc:卸货班次 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_UNLOADCLASS {get;set;}

        /// <summary>
        /// Desc:卸货人 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_UNLOADMAN {get;set;}

        /// <summary>
        /// Desc:卸货日期(进铁时间) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string D_UNLOADDATE {get;set;}

        /// <summary>
        /// Desc:原带重量(预重) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? N_INTRINSICWEIGHT {get;set;}

        /// <summary>
        /// Desc:参考重量(估重) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? N_THEORYWEIGHT {get;set;}

        /// <summary>
        /// Desc:到货日期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string D_ARRIVALDATE {get;set;}

        /// <summary>
        /// Desc:发货工厂ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_SENDFACTORY {get;set;}

        /// <summary>
        /// Desc:接收工厂ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_RECEIVEFACTORY {get;set;}

        /// <summary>
        /// Desc:物料编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_MATERIELID {get;set;}

        /// <summary>
        /// Desc:物料描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_MATERIELDES {get;set;}

        /// <summary>
        /// Desc:发货方批次 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_SENDBATCHID {get;set;}

        /// <summary>
        /// Desc:高炉号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_STOVESITE {get;set;}

        /// <summary>
        /// Desc:铁次号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_STOVELOT {get;set;}

        /// <summary>
        /// Desc:收货方批次 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_RECEIVEBATCHID {get;set;}

        /// <summary>
        /// Desc:发站 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_STARTSTATION {get;set;}

        /// <summary>
        /// Desc:质检批次 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_CHECKLOT {get;set;}

        /// <summary>
        /// Desc:成分是否产生(0:未产生;1:产生;2:变更) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_BALANCENO {get;set;}

        /// <summary>
        /// Desc:创建计量委托工厂 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_ENTRUSTFACTORY {get;set;}

        /// <summary>
        /// Desc:维护人 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_MAINTAINER {get;set;}

        /// <summary>
        /// Desc:发货日期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string D_SENDDATE {get;set;}

        /// <summary>
        /// Desc:计量备注 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_REMARK {get;set;}

        /// <summary>
        /// Desc:入库标识(0未入库1湿基入库2干基入库3财务结算9不需入库) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_INSTORAGEFLAG {get;set;}

        /// <summary>
        /// Desc:供应商编号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_SUPPLIERID {get;set;}

        /// <summary>
        /// Desc:供应商描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_SUPPLYIERDES {get;set;}

        /// <summary>
        /// Desc:客户编号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_CLIENTID {get;set;}

        /// <summary>
        /// Desc:客户描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_CLIENTDES {get;set;}

        /// <summary>
        /// Desc:收货物料号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_REFMATERIELID {get;set;}

        /// <summary>
        /// Desc:删除原因 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_DELREASON {get;set;}

        /// <summary>
        /// Desc:作废标识(0:正常;1:删除2:废钢拆分) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_STATE {get;set;}

        /// <summary>
        /// Desc:时间戳 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? C_TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:扩展1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_SW01 {get;set;}

        /// <summary>
        /// Desc:扩展2 
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
        /// Desc:创建时间，为计量单的创建时间，为质检模块查询没有发送取样委托的计量单用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string D_CREATETIME {get;set;}

        /// <summary>
        /// Desc:卸空时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_UNLOADEMPTYTIME {get;set;}

        /// <summary>
        /// Desc:扣重 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? C_DEDUCTEDWEIGHT {get;set;}

        /// <summary>
        /// Desc:内部控制批号(铁水收料主键) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_INNERBATCH {get;set;}

        /// <summary>
        /// Desc:物料组 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_MATERIELGROUP {get;set;}

        /// <summary>
        /// Desc:毛重顺序号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public long N_GROSSSORT {get;set;}

        /// <summary>
        /// Desc:铁水任务单(过磅计划) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_IRONWAYID {get;set;}

        /// <summary>
        /// Desc:出铁开口时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string D_TIMEOUTSTART {get;set;}

        /// <summary>
        /// Desc:出铁堵口时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string D_TIMEOUTEND {get;set;}

        /// <summary>
        /// Desc:单罐出铁开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string D_TIMEIRONSTART {get;set;}

        /// <summary>
        /// Desc:单罐出铁结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string D_TIMEIRONEND {get;set;}

        /// <summary>
        /// Desc:历史皮重时间(yyyy-MM-dd HH:mm:ss) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string D_HISTARTTIME {get;set;}

        /// <summary>
        /// Desc:去向-A板卷厂B炼钢场C电炉厂D#1生铁站E#2生铁站 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_TOCODE {get;set;}

        /// <summary>
        /// Desc:多次过磅标志，默认1，需要回抛 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_MULWEIKEY {get;set;}

        /// <summary>
        /// Desc:毛重T 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? N_GROSSWGT {get;set;}

        /// <summary>
        /// Desc:毛重计量点 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_GROSSWGTSITE {get;set;}

        /// <summary>
        /// Desc:毛重计量班次1大夜班2白班3小夜班 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_GROSSWGTSHIFT {get;set;}

        /// <summary>
        /// Desc:毛重计量班別A甲班B乙班C丙班D丁班 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_GROSSWGTCREW {get;set;}

        /// <summary>
        /// Desc:毛重计量人員 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_GROSSWGTMAN {get;set;}

        /// <summary>
        /// Desc:皮重T 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? N_TARTWGT {get;set;}

        /// <summary>
        /// Desc:皮重计量点 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_TARTWGTSITE {get;set;}

        /// <summary>
        /// Desc:皮重计量班次1大夜班2白班3小夜班 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_TARTWGTSHIFT {get;set;}

        /// <summary>
        /// Desc:皮重计量班別A甲班B乙班C丙班D丁班 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_TARTWGTCREW {get;set;}

        /// <summary>
        /// Desc:皮重计量人員 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_TARTWGTMAN {get;set;}

        /// <summary>
        /// Desc:净重计量点 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_NETWGTSITE {get;set;}

        /// <summary>
        /// Desc:净重计量班次1大夜班2白班3小夜班 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_NETWGTSHIFT {get;set;}

        /// <summary>
        /// Desc:净重计量班別A甲班B乙班C丙班D丁班 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_NETWGTCREW {get;set;}

        /// <summary>
        /// Desc:净重计量人員 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_NETWGTMAN {get;set;}

        /// <summary>
        /// Desc:出铁温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? C_TEMPERATUREOUT {get;set;}

        /// <summary>
        /// Desc:接收温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? C_TEMPERATURERECEIVE {get;set;}

        /// <summary>
        /// Desc:预估硅 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? C_PREDICTSI {get;set;}

        /// <summary>
        /// Desc:预估硫 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? C_PREDICTS {get;set;}

        /// <summary>
        /// Desc:历史皮重T 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? N_HISTAREWGT {get;set;}

        /// <summary>
        /// Desc:出铁口 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_TAPHOLE {get;set;}

        /// <summary>
        /// Desc:卸空标志(0未卸空1已卸空) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_UNLOADEMPTYFLAG {get;set;}

        /// <summary>
        /// Desc:卸空人 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_UNLOADEMPTYMAN {get;set;}

        /// <summary>
        /// Desc:卸空班次 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_UNLOADEMPTYSHIFT {get;set;}

        /// <summary>
        /// Desc:数据来源(1计量2铁区补) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_DATASOURCE {get;set;}

        /// <summary>
        /// Desc:来源描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_FROMDESC {get;set;}

        /// <summary>
        /// Desc:去向描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_TODESC {get;set;}

    }
}
