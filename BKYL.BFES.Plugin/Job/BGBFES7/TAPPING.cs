using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.BGBFES7
{
    public class TAPPING
    {
        
        /// <summary>
        /// Desc:铁次号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int TAPNUMB {get;set;}

        /// <summary>
        /// Desc:铁口号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string HOLENUMB {get;set;}

        /// <summary>
        /// Desc:配罐时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? PEIGUAN {get;set;}

        /// <summary>
        /// Desc:出铁开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? TAPSTART {get;set;}

        /// <summary>
        /// Desc:出铁结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? TAPEND {get;set;}

        /// <summary>
        /// Desc:铁间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TAPBETWEEN {get;set;}

        /// <summary>
        /// Desc:出铁时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TAPTIME {get;set;}

        /// <summary>
        /// Desc:见渣时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SLAGSTART {get;set;}

        /// <summary>
        /// Desc:出渣时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGTIME {get;set;}

        /// <summary>
        /// Desc:估Si 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GUSI {get;set;}

        /// <summary>
        /// Desc:估S 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GUS {get;set;}

        /// <summary>
        /// Desc:理论铁量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? IRON_WCAL {get;set;}

        /// <summary>
        /// Desc:整罐出铁量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TAPWREAL {get;set;}

        /// <summary>
        /// Desc:铁量差（自动算：实际铁量-理论铁量） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TAPDIFFE {get;set;}

        /// <summary>
        /// Desc:铁水温度(渣前) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMTP_SLAGQ {get;set;}

        /// <summary>
        /// Desc:铁水温度(渣后) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMTP_SLAGH {get;set;}

        /// <summary>
        /// Desc:铁口深度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HOLELENGH {get;set;}

        /// <summary>
        /// Desc:跑泥否 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FLAGSLUDGE {get;set;}

        /// <summary>
        /// Desc:打泥量（格） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLUDGENUM {get;set;}

        /// <summary>
        /// Desc:铁水Si 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMSI {get;set;}

        /// <summary>
        /// Desc:铁水Mn 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMMN {get;set;}

        /// <summary>
        /// Desc:铁水S 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMS {get;set;}

        /// <summary>
        /// Desc:铁水P 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMP {get;set;}

        /// <summary>
        /// Desc:铁水C 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMC {get;set;}

        /// <summary>
        /// Desc:铁水TI 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMTI {get;set;}

        /// <summary>
        /// Desc:铁水Fe 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMFE {get;set;}

        /// <summary>
        /// Desc:cao 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGCAO {get;set;}

        /// <summary>
        /// Desc:sio2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGSIO2 {get;set;}

        /// <summary>
        /// Desc:s 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGS {get;set;}

        /// <summary>
        /// Desc:r2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGR2 {get;set;}

        /// <summary>
        /// Desc:feo 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGFEO {get;set;}

        /// <summary>
        /// Desc:tio2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGTIO2 {get;set;}

        /// <summary>
        /// Desc:al2o3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGAL2O3 {get;set;}

        /// <summary>
        /// Desc:mgo 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGMGO {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_ID {get;set;}

        /// <summary>
        /// Desc:铁次日期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:铁次号内索引 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? TAPINDEX {get;set;}

        /// <summary>
        /// Desc:主键自增长 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? ID {get;set;}

        /// <summary>
        /// Desc:是否出净（默认是） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CHUJING {get;set;}

        /// <summary>
        /// Desc:石头渣比例 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SHITOU_PER {get;set;}

        /// <summary>
        /// Desc:晚点原因 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LATE_REASON {get;set;}

        /// <summary>
        /// Desc:铁水温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMTP {get;set;}

        /// <summary>
        /// Desc:罐号、钢包号 山西晋南自定义 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LADLE {get;set;}

        /// <summary>
        /// Desc:理论渣量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAG_WCAL {get;set;}

        /// <summary>
        /// Desc:实际渣量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGWREAL {get;set;}

        /// <summary>
        /// Desc:料批号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BATCHNUM {get;set;}

        /// <summary>
        /// Desc:重叠时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? OVERLAPTIME {get;set;}

        /// <summary>
        /// Desc:吹否标识 0 -否 1-是 2-迫 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BLOWIF {get;set;}

        /// <summary>
        /// Desc:水渣量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? WATERSLAG_WEIGHT {get;set;}

        /// <summary>
        /// Desc:干渣量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? DRYSLAG_WEIGHT {get;set;}

        /// <summary>
        /// Desc:水渣号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string WATERSLAG_NUM {get;set;}

        /// <summary>
        /// Desc:铁水罐总数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? IRONPOT_NUM {get;set;}

        /// <summary>
        /// Desc:渣罐数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SLAGPOT_NUM {get;set;}

        /// <summary>
        /// Desc:铁渣F 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGF {get;set;}

        /// <summary>
        /// Desc:铁渣R0 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGR0 {get;set;}

        /// <summary>
        /// Desc:亏铁量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? DIFF_IRON {get;set;}

        /// <summary>
        /// Desc:亏渣量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? DIFF_SLAG {get;set;}

        /// <summary>
        /// Desc:钻头 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZUANTOU {get;set;}

        /// <summary>
        /// Desc:出铁速度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? IRONSPEED {get;set;}

        /// <summary>
        /// Desc:本次小头罐 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? THISPOT {get;set;}

        /// <summary>
        /// Desc:上次小头罐 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? LASTPOT {get;set;}

        /// <summary>
        /// Desc:实际出铁量（整罐出铁量+本次小头罐-上次小头罐） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? IRON_FINAL {get;set;}

    }
}
