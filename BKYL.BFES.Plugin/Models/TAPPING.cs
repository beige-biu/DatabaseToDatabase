using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class TAPPING
    {

        /// <summary>
        /// Desc:铁次号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int TAPNUMB { get; set; }

        /// <summary>
        /// Desc:铁口号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool HOLENUMB { get; set; }

        /// <summary>
        /// Desc:配罐时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? PEIGUAN { get; set; }

        /// <summary>
        /// Desc:出铁开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? TAPSTART { get; set; }

        /// <summary>
        /// Desc:出铁结束时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? TAPEND { get; set; }

        /// <summary>
        /// Desc:铁间隔 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TAPBETWEEN { get; set; }

        /// <summary>
        /// Desc:出铁时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TAPTIME { get; set; }

        /// <summary>
        /// Desc:出渣开始时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SLAGSTART { get; set; }

        /// <summary>
        /// Desc:出渣时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGTIME { get; set; }

        /// <summary>
        /// Desc:出渣铁时间比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGIRONTIME { get; set; }

        /// <summary>
        /// Desc:估产 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GUCHAN { get; set; }

        /// <summary>
        /// Desc:估Si 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GUSI { get; set; }

        /// <summary>
        /// Desc:估S 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GUS { get; set; }

        /// <summary>
        /// Desc:铁间批数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? JIANPI { get; set; }

        /// <summary>
        /// Desc:理论铁量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? IRON_WCAL { get; set; }

        /// <summary>
        /// Desc:出铁速度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TAPPINGSPEED { get; set; }

        /// <summary>
        /// Desc:出铁总重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TAPWREAL { get; set; }

        /// <summary>
        /// Desc:铁量差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TAPDIFFE { get; set; }

        /// <summary>
        /// Desc:铁水温度(渣前) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMTP_SLAGQ { get; set; }

        /// <summary>
        /// Desc:铁水温度(渣后) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMTP_SLAGH { get; set; }

        /// <summary>
        /// Desc:铁口深度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HOLELENGH { get; set; }

        /// <summary>
        /// Desc:铁口角度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HOLEANGLE { get; set; }

        /// <summary>
        /// Desc:跑泥否 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FLAGSLUDGE { get; set; }

        /// <summary>
        /// Desc:打泥量(格) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLUDGENUM { get; set; }

        /// <summary>
        /// Desc:喷否 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PENFOU { get; set; }

        /// <summary>
        /// Desc:铁水Si 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMSI { get; set; }

        /// <summary>
        /// Desc:铁水Mn 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMMN { get; set; }

        /// <summary>
        /// Desc:铁水S 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMS { get; set; }

        /// <summary>
        /// Desc:铁水P 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMP { get; set; }

        /// <summary>
        /// Desc:铁水C 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMC { get; set; }

        /// <summary>
        /// Desc:铁水TI 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMTI { get; set; }

        /// <summary>
        /// Desc:铁水As 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMAS { get; set; }

        /// <summary>
        /// Desc:铁水Cr 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMCR { get; set; }

        /// <summary>
        /// Desc:铁水Cu 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HMCU { get; set; }

        /// <summary>
        /// Desc:cao 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGCAO { get; set; }

        /// <summary>
        /// Desc:sio2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGSIO2 { get; set; }

        /// <summary>
        /// Desc:s 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGS { get; set; }

        /// <summary>
        /// Desc:r2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGR2 { get; set; }

        /// <summary>
        /// Desc:feo 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGFEO { get; set; }

        /// <summary>
        /// Desc:tio2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGTIO2 { get; set; }

        /// <summary>
        /// Desc:al2o3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGAL2O3 { get; set; }

        /// <summary>
        /// Desc:mgo 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SLAGMGO { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_ID { get; set; }

        /// <summary>
        /// Desc:铁次日期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? TIMESTAMP { get; set; }

    }
}
