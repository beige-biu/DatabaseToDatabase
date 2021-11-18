using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MD_BLEND_BATCH_LIST_IN
    {

        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:计划号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BATCH_NUM {get;set;}

        /// <summary>
        /// Desc:试样编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SAMPLE_CODE {get;set;}

        /// <summary>
        /// Desc:物料名 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_NAME {get;set;}

        /// <summary>
        /// Desc:湿配比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? WET_BILL {get;set;}

        /// <summary>
        /// Desc:湿单耗 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? WET_U_CON {get;set;}

        /// <summary>
        /// Desc:干配比 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? DRY_BILL {get;set;}

        /// <summary>
        /// Desc:干单耗 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? DRY_U_CON {get;set;}

        /// <summary>
        /// Desc:TFe 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_TFE {get;set;}

        /// <summary>
        /// Desc:FeO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_FEO {get;set;}

        /// <summary>
        /// Desc:CaO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_CAO {get;set;}

        /// <summary>
        /// Desc:SiO2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_SIO2 {get;set;}

        /// <summary>
        /// Desc:Al2O3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_AL2O3 {get;set;}

        /// <summary>
        /// Desc:MgO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_MGO {get;set;}

        /// <summary>
        /// Desc:S 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_S {get;set;}

        /// <summary>
        /// Desc:P 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_P {get;set;}

        /// <summary>
        /// Desc:C 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_C {get;set;}

        /// <summary>
        /// Desc:Mn 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_MN {get;set;}

        /// <summary>
        /// Desc:烧损 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_LOT {get;set;}

        /// <summary>
        /// Desc:碱度（R） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_R {get;set;}

        /// <summary>
        /// Desc:H2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_H2O {get;set;}

        /// <summary>
        /// Desc:灰分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_ASH {get;set;}

        /// <summary>
        /// Desc:挥发分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_VOLATILES {get;set;}

        /// <summary>
        /// Desc:TiO2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_TIO2 {get;set;}

        /// <summary>
        /// Desc:K2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_K2O {get;set;}

        /// <summary>
        /// Desc:Na2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_NA2O {get;set;}

        /// <summary>
        /// Desc:PbO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_PBO {get;set;}

        /// <summary>
        /// Desc:ZnO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_ZNO {get;set;}

        /// <summary>
        /// Desc:F 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_TI { get;set;}

        /// <summary>
        /// Desc:As 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_AS {get;set;}

        /// <summary>
        /// Desc:Cu 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_CU {get;set;}

        /// <summary>
        /// Desc:Pb 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_PB {get;set;}

        /// <summary>
        /// Desc:Zn 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_ZN {get;set;}

        /// <summary>
        /// Desc:K 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_K {get;set;}

        /// <summary>
        /// Desc:Na 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_NA {get;set;}

        /// <summary>
        /// Desc:Cr 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_CR {get;set;}

        /// <summary>
        /// Desc:Ni 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_NI {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_MNO { get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? BLEND_SPARE1 {get;set;}

        public  int L2_CODE { get; set; }

    }
}
