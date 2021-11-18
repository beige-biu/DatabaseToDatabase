using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.BGBFES7
{
    public class L3ANALYSI
    {
        
        /// <summary>
        /// Desc:检化验时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:料种代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int M_CODE {get;set;}

        /// <summary>
        /// Desc:3级料种代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string M_CODE3 {get;set;}

        /// <summary>
        /// Desc:碳含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_C_CONT {get;set;}

        /// <summary>
        /// Desc:铁含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_TFE_CONT {get;set;}

        /// <summary>
        /// Desc:氧化铁含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_FE2O3_CONT {get;set;}

        /// <summary>
        /// Desc:氧化亚铁含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_FEO_CONT {get;set;}

        /// <summary>
        /// Desc:二氧化硅含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_SIO2_CONT {get;set;}

        /// <summary>
        /// Desc:三氧化二铝含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_AL2O3_CONT {get;set;}

        /// <summary>
        /// Desc:氧化钙含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_CAO_CONT {get;set;}

        /// <summary>
        /// Desc:氧化镁含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_MGO_CONT {get;set;}

        /// <summary>
        /// Desc:氧化钠含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_NA2O_CONT {get;set;}

        /// <summary>
        /// Desc:氧化钾含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_K2O_CONT {get;set;}

        /// <summary>
        /// Desc:氧化锰含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_MN_CONT {get;set;}

        /// <summary>
        /// Desc:硫含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S_CONT {get;set;}

        /// <summary>
        /// Desc:磷含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_P_CONT {get;set;}

        /// <summary>
        /// Desc:氧化钛含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_TIO2_CONT {get;set;}

        /// <summary>
        /// Desc:湿度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_MOISTURE {get;set;}

        /// <summary>
        /// Desc:碱度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_BASICITY {get;set;}

        /// <summary>
        /// Desc:锌含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_ZN_CONT {get;set;}

        /// <summary>
        /// Desc:灰份 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_COKEASH {get;set;}

        /// <summary>
        /// Desc:氟化钙含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_CAF2_CONT {get;set;}

        /// <summary>
        /// Desc:挥发份 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_VOLA_CONT {get;set;}

        /// <summary>
        /// Desc:氢含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_H_CONT {get;set;}

        /// <summary>
        /// Desc:平均粒径 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_PART_SIZE {get;set;}

        /// <summary>
        /// Desc:一氧化锰含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_MNO_CONT {get;set;}

        /// <summary>
        /// Desc:二氧化锰含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_MNO2_CONT {get;set;}

        /// <summary>
        /// Desc:硫化铁含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_FES_CONT {get;set;}

        /// <summary>
        /// Desc:硫化亚铁含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_FES2_CONT {get;set;}

        /// <summary>
        /// Desc:五氧化二钒含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_V2O5_CONT {get;set;}

        /// <summary>
        /// Desc:氧化铜含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_CUO_CONT {get;set;}

        /// <summary>
        /// Desc:氧化亚铜含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_CU2O_CONT {get;set;}

        /// <summary>
        /// Desc:二氧化碳含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_CO2_CONT {get;set;}

        /// <summary>
        /// Desc:二氧化硫含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_SO2_CONT {get;set;}

        /// <summary>
        /// Desc:五氧化二磷含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_P2O5_CONT {get;set;}

        /// <summary>
        /// Desc:氧化铅 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_PB_CONT {get;set;}

        /// <summary>
        /// Desc:转鼓指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ZGZS_VAL {get;set;}

        /// <summary>
        /// Desc:筛分指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SFZS_VAL {get;set;}

        /// <summary>
        /// Desc:硫分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ST_D_VAL {get;set;}

        /// <summary>
        /// Desc:M10 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_M10_VAL {get;set;}

        /// <summary>
        /// Desc:M40 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_M40_VAL {get;set;}

        /// <summary>
        /// Desc:CRI 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CRI_VAL {get;set;}

        /// <summary>
        /// Desc:CSR 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CSR_VAL {get;set;}

        /// <summary>
        /// Desc:氟含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_F_VAL {get;set;}

        /// <summary>
        /// Desc:筛分>40 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ATTRIBUTE1 {get;set;}

        /// <summary>
        /// Desc:筛分40-25 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ATTRIBUTE2 {get;set;}

        /// <summary>
        /// Desc:筛分25-16 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ATTRIBUTE3 {get;set;}

        /// <summary>
        /// Desc:筛分16-10 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ATTRIBUTE4 {get;set;}

        /// <summary>
        /// Desc:筛分10-5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ATTRIBUTE5 {get;set;}

        /// <summary>
        /// Desc:总量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ATTRIBUTE6 {get;set;}

        /// <summary>
        /// Desc:平均粒径 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ATTRIBUTE7 {get;set;}

        /// <summary>
        /// Desc:配鼓40-25 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ATTRIBUTE8 {get;set;}

        /// <summary>
        /// Desc:配鼓25-16 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ATTRIBUTE9 {get;set;}

        /// <summary>
        /// Desc:配鼓16-10 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ATTRIBUTE10 {get;set;}

        /// <summary>
        /// Desc:入鼓量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ATTRIBUTE11 {get;set;}

        /// <summary>
        /// Desc:出鼓量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ATTRIBUTE12 {get;set;}

        /// <summary>
        /// Desc:>6.3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ATTRIBUTE13 {get;set;}

        /// <summary>
        /// Desc:筛分8-16 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ATTRIBUTE14 {get;set;}

        /// <summary>
        /// Desc:配鼓10-40 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ATTRIBUTE15 {get;set;}

        /// <summary>
        /// Desc:抗压强度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_KYQD_VAL {get;set;}

        /// <summary>
        /// Desc:还原度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_RI_VAL {get;set;}

        /// <summary>
        /// Desc:氧化锌 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ZNO_VAL {get;set;}

        /// <summary>
        /// Desc:RDI+3.15 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_RDI315_VAL {get;set;}

        /// <summary>
        /// Desc:压差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_YACHA_VAL {get;set;}

        /// <summary>
        /// Desc:膨胀率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_PZL_VAL {get;set;}

        /// <summary>
        /// Desc:粒度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_LIDU_VAL {get;set;}

        /// <summary>
        /// Desc:化验试样号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_SAMPLE_NO {get;set;}

        /// <summary>
        /// Desc:送样时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? M_SAMPLE_TIME {get;set;}

    }
}
