using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class M_SINTER_ANA
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:取样时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime SAMPLTIME {get;set;}

        /// <summary>
        /// Desc:报样时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime REOPTTIME {get;set;}

        /// <summary>
        /// Desc:试样编码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string SAMPLE_CODE {get;set;}

        /// <summary>
        /// Desc:三级原料代码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int L2_CODE {get;set;}

        /// <summary>
        /// Desc:批号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BATCH_NUM {get;set;}

        /// <summary>
        /// Desc:TFe 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_TFE {get;set;}

        /// <summary>
        /// Desc:FeO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_FEO {get;set;}

        /// <summary>
        /// Desc:CaO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_CAO {get;set;}

        /// <summary>
        /// Desc:SiO2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_SIO2 {get;set;}

        /// <summary>
        /// Desc:Al2O3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_AL2O3 {get;set;}

        /// <summary>
        /// Desc:MgO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_MGO {get;set;}

        /// <summary>
        /// Desc:S 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_S {get;set;}

        /// <summary>
        /// Desc:P 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_P {get;set;}

        /// <summary>
        /// Desc:C 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_C {get;set;}

        /// <summary>
        /// Desc:Mn 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_MN {get;set;}

        /// <summary>
        /// Desc:烧损 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_LOT {get;set;}

        /// <summary>
        /// Desc:碱度（R） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_R {get;set;}

        /// <summary>
        /// Desc:H2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_H2O {get;set;}

        /// <summary>
        /// Desc:灰分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_ASH {get;set;}

        /// <summary>
        /// Desc:挥发分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_VOLATILES {get;set;}

        /// <summary>
        /// Desc:TiO2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_TIO2 {get;set;}

        /// <summary>
        /// Desc:K2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_K2O {get;set;}

        /// <summary>
        /// Desc:Na2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_NA2O {get;set;}

        /// <summary>
        /// Desc:PbO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_PBO {get;set;}

        /// <summary>
        /// Desc:ZnO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_ZNO {get;set;}

        /// <summary>
        /// Desc:F 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_TI { get;set;}

        /// <summary>
        /// Desc:As 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_AS {get;set;}

        /// <summary>
        /// Desc:Cu 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_CU {get;set;}

        /// <summary>
        /// Desc:Pb 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_PB {get;set;}

        /// <summary>
        /// Desc:Zn 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_ZN {get;set;}

        /// <summary>
        /// Desc:K 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_K {get;set;}

        /// <summary>
        /// Desc:Na 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_NA {get;set;}

        /// <summary>
        /// Desc:Cr 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_CR {get;set;}

        /// <summary>
        /// Desc:Ni 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_NI {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double C_MNO { get;set;}

        /// <summary>
        /// Desc:转鼓 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double EQUITY {get;set;}

        /// <summary>
        /// Desc:筛分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double SCRENING {get;set;}

        /// <summary>
        /// Desc:RDI-0.5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double RDI1 {get;set;}

        /// <summary>
        /// Desc:RDI+3.15 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double RDI2 {get;set;}

        /// <summary>
        /// Desc:RDI+6.3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double RDI3 {get;set;}

        /// <summary>
        /// Desc:<5mm（烧结矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double GRIT_SIN_1 {get;set;}

        /// <summary>
        /// Desc:5-10mm（烧结矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double GRIT_SIN_2 {get;set;}

        /// <summary>
        /// Desc:10-16mm（烧结矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double GRIT_SIN_3 {get;set;}

        /// <summary>
        /// Desc:16-25mm（烧结矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double GRIT_SIN_4 {get;set;}

        /// <summary>
        /// Desc:25-40mm（烧结矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double GRIT_SIN_5 {get;set;}

        /// <summary>
        /// Desc:>40mm（烧结矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double GRIT_SIN_6 {get;set;}

        /// <summary>
        /// Desc:还原度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double RI {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double SPARE2 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double SPARE3 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double SPARE4 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double SPARE5 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double SPARE6 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double SPARE7 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double SPARE8 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double SPARE9 {get;set;}

        /// <summary>
        /// Desc:备用(后台程序周期调用，别改)新插入置1，处理过置0 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int SPARE10 {get;set;}

    }
}
