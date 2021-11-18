using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models
{
    public class L3ANALYSI
    {

        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:料种代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int M_CODE { get; set; }

        /// <summary>
        /// Desc:3级料种代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string M_CODE3 { get; set; }
        /// <summary>
        /// 试样号
        /// </summary>
        public string MAT_SAMPLE_NO { get; set; }

        /// <summary>
        /// Desc:碳含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_C_CONT { get; set; }

        /// <summary>
        /// Desc:铁含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_TFE_CONT { get; set; }

        /// <summary>
        /// Desc:氧化铁含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_FE2O3_CONT { get; set; }

        /// <summary>
        /// Desc:氧化亚铁含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_FEO_CONT { get; set; }

        /// <summary>
        /// Desc:二氧化硅含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_SIO2_CONT { get; set; }

        /// <summary>
        /// Desc:三氧化二铝含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_AL2O3_CONT { get; set; }

        /// <summary>
        /// Desc:氧化钙含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_CAO_CONT { get; set; }

        /// <summary>
        /// Desc:氧化镁含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_MGO_CONT { get; set; }

        /// <summary>
        /// Desc:氧化钠含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_NA2O_CONT { get; set; }

        /// <summary>
        /// Desc:氧化钾含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_K2O_CONT { get; set; }

        /// <summary>
        /// Desc:氧化锰含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_MN_CONT { get; set; }

        /// <summary>
        /// Desc:硫含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S_CONT { get; set; }

        /// <summary>
        /// Desc:磷含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_P_CONT { get; set; }

        /// <summary>
        /// Desc:氧化钛含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_TIO2_CONT { get; set; }

        /// <summary>
        /// Desc:湿度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_MOISTURE { get; set; }

        /// <summary>
        /// Desc:碱度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_BASICITY { get; set; }

        /// <summary>
        /// Desc:锌含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_ZN_CONT { get; set; }

        /// <summary>
        /// Desc:灰份 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_COKEASH { get; set; }

        /// <summary>
        /// Desc:氟化钙含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_CAF2_CONT { get; set; }

        /// <summary>
        /// Desc:挥发份 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_VOLA_CONT { get; set; }

        /// <summary>
        /// Desc:氢含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_H_CONT { get; set; }

        /// <summary>
        /// Desc:平均粒径 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_PART_SIZE { get; set; }

        /// <summary>
        /// Desc:一氧化锰含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_MNO_CONT { get; set; }

        /// <summary>
        /// Desc:二氧化锰含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_MNO2_CONT { get; set; }

        /// <summary>
        /// Desc:硫化铁含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_FES_CONT { get; set; }

        /// <summary>
        /// Desc:硫化亚铁含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_FES2_CONT { get; set; }

        /// <summary>
        /// Desc:五氧化二钒含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_V2O5_CONT { get; set; }

        /// <summary>
        /// Desc:氧化铜含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_CUO_CONT { get; set; }

        /// <summary>
        /// Desc:氧化亚铜含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_CU2O_CONT { get; set; }

        /// <summary>
        /// Desc:二氧化碳含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_CO2_CONT { get; set; }

        /// <summary>
        /// Desc:二氧化硫含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_SO2_CONT { get; set; }

        /// <summary>
        /// Desc:五氧化二磷含量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_P2O5_CONT { get; set; }

        /// <summary>
        /// Desc:氧化铅 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_PB_CONT { get; set; }

        /// <summary>
        /// Desc:10－30mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_ADE2 { get; set; }

        /// <summary>
        /// Desc:>30mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_ADE3 { get; set; }

        /// <summary>
        /// Desc:As 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_AS { get; set; }

        /// <summary>
        /// Desc:Cu 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_CU { get; set; }

        /// <summary>
        /// Desc:烧损 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_LOI { get; set; }

        /// <summary>
        /// Desc:<10mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_ADE1 { get; set; }

        /// <summary>
        /// Desc:ZnO 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_ZNO { get; set; }

        /// <summary>
        /// Desc:转鼓强度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_TS { get; set; }

        /// <summary>
        /// Desc:抗磨强度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_AMS { get; set; }

        /// <summary>
        /// Desc:ADE4 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_ADE4 { get; set; }

        /// <summary>
        /// Desc:ADE5 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_ADE5 { get; set; }

        /// <summary>
        /// Desc:ADE6 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_ADE6 { get; set; }

        /// <summary>
        /// Desc:全硫 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_STD { get; set; }

        /// <summary>
        /// Desc:C 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_C { get; set; }

        /// <summary>
        /// Desc:抗碎强度M25 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_M25 { get; set; }

        /// <summary>
        /// Desc:抗碎强度M40 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_M40 { get; set; }

        /// <summary>
        /// Desc:内在水分 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_MAD { get; set; }

        /// <summary>
        /// Desc:反应性 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_CRI { get; set; }

        /// <summary>
        /// Desc:反应后强度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_CSR { get; set; }

        /// <summary>
        /// Desc:>60mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_SZ60 { get; set; }

        /// <summary>
        /// Desc:40mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_SZ40 { get; set; }

        /// <summary>
        /// Desc:40－25mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S40_25 { get; set; }

        /// <summary>
        /// Desc:<25mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S_25 { get; set; }

        /// <summary>
        /// Desc:<30mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S_30 { get; set; }

        /// <summary>
        /// Desc:<40-60mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S40_60 { get; set; }

        /// <summary>
        /// Desc:抗碎强度M10 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_M10 { get; set; }

        /// <summary>
        /// Desc:Ti 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_TI { get; set; }

        /// <summary>
        /// Desc:>31.5mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_SZ31Z5 { get; set; }

        /// <summary>
        /// Desc:31.5－25mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_SZ25 { get; set; }

        /// <summary>
        /// Desc:25－10mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_SZ10 { get; set; }

        /// <summary>
        /// Desc:10－6.3mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_SZ6Z3 { get; set; }

        /// <summary>
        /// Desc:<6.3mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S_6Z3 { get; set; }

        /// <summary>
        /// Desc:低温还原强度(RDI+6.3) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_RDIZ6Z3 { get; set; }

        /// <summary>
        /// Desc:低温还原粉化(RDI+3.15) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_RDIZ3Z15 { get; set; }

        /// <summary>
        /// Desc:低温还原抗磨(RDI－0.5) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_RDI_0Z5 { get; set; }

        /// <summary>
        /// Desc:还原度(RI) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_RI { get; set; }

        /// <summary>
        /// Desc:软化开始 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_RUANHKS { get; set; }

        /// <summary>
        /// Desc:软化终了 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_RUANHZL { get; set; }

        /// <summary>
        /// Desc:软化区间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_RUANHQJ { get; set; }

        /// <summary>
        /// Desc:熔融开始 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_RONGHKS { get; set; }

        /// <summary>
        /// Desc:熔融终了 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_RONGHZL { get; set; }

        /// <summary>
        /// Desc:熔融区间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_RONGHQJ { get; set; }

        /// <summary>
        /// Desc:压差 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_YC { get; set; }

        /// <summary>
        /// Desc:软容层厚度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_RRCHD { get; set; }

        /// <summary>
        /// Desc:抗裂指数(DI-0.5) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_DI_0Z5 { get; set; }

        /// <summary>
        /// Desc:抗裂指数(DI-3.15) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_D_3Z15 { get; set; }

        /// <summary>
        /// Desc:抗裂指数(DI-6.3) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_DI_6Z3 { get; set; }

        /// <summary>
        /// Desc:熔滴指数 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_RDZS { get; set; }

        /// <summary>
        /// Desc:硫分 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_STAR { get; set; }

        /// <summary>
        /// Desc:K+NA 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_KZNA { get; set; }

        /// <summary>
        /// Desc:高位热值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_GWR { get; set; }

        /// <summary>
        /// Desc:发热量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_HEATQTY { get; set; }

        /// <summary>
        /// Desc:水分(分析基) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_MAR { get; set; }

        /// <summary>
        /// Desc:>180目 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_SMZ180 { get; set; }

        /// <summary>
        /// Desc:-200目 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_SM_200 { get; set; }

        /// <summary>
        /// Desc:180-200 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_SMZ200 { get; set; }

        /// <summary>
        /// Desc:>16mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_SZ16 { get; set; }

        /// <summary>
        /// Desc:16－10mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S16_10 { get; set; }

        /// <summary>
        /// Desc:10－5mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S10_5 { get; set; }

        /// <summary>
        /// Desc:<5mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S_5 { get; set; }

        /// <summary>
        /// Desc:20-6.3mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S20_6Z3 { get; set; }

        /// <summary>
        /// Desc:8－5mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S8_5 { get; set; }

        /// <summary>
        /// Desc:转鼓指数(T) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_DI { get; set; }

        /// <summary>
        /// Desc:抗压强度(N) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_KYN { get; set; }

        /// <summary>
        /// Desc:还原膨胀(RSI) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_RSI { get; set; }

        /// <summary>
        /// Desc:铬 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_CR { get; set; }

        /// <summary>
        /// Desc:16-8mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S8_16 { get; set; }

        /// <summary>
        /// Desc:转鼓指数T 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_TI6Z3 { get; set; }

        /// <summary>
        /// Desc:抗压强度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_CCS { get; set; }

        /// <summary>
        /// Desc:抗磨强度 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_KMZS0Z5 { get; set; }

        /// <summary>
        /// Desc:25－16mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S25_16 { get; set; }

        /// <summary>
        /// Desc:低温还原粉化(RDI||3.15) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_RDI_3Z15 { get; set; }

        /// <summary>
        /// Desc:<10mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S_10 { get; set; }

        /// <summary>
        /// Desc:10－30mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S10_30 { get; set; }

        /// <summary>
        /// Desc:>30mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_SZ30 { get; set; }

        /// <summary>
        /// Desc:<8mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S8 { get; set; }

        /// <summary>
        /// Desc:25-31.5mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S25_31D5 { get; set; }

        /// <summary>
        /// Desc:6.3-25mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S6D3_25 { get; set; }

        /// <summary>
        /// Desc:>31.5mm

        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_SZ31D5 { get; set; }

        /// <summary>
        /// Desc:全水分

        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_MT { get; set; }

        /// <summary>
        /// Desc:25-40mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S25_40 { get; set; }

        /// <summary>
        /// Desc:发热量2(干基高位)

        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_HEATQTY_2 { get; set; }

        /// <summary>
        /// Desc:<45mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S0_45 { get; set; }

        /// <summary>
        /// Desc:>75mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S_Z75 { get; set; }

        /// <summary>
        /// Desc:哈氏可磨性系数

        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_HSKMXS { get; set; }

        /// <summary>
        /// Desc:0-30mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S0_30 { get; set; }

        /// <summary>
        /// Desc:20-90mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S20_90 { get; set; }

        /// <summary>
        /// Desc:>90mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_SZ90 { get; set; }

        /// <summary>
        /// Desc:<20mm 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_S20 { get; set; }
        /// <summary>
        /// 取样时间
        /// </summary>
        public DateTime? MAT_SAMPLE_TIME { get; set; }

    }
}
namespace BKYL.BFES.Plugin.Models.NG
{
    public class L3ANALYSI
    {
        /// <summary>
        /// Desc:取样时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:1级编码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int M_CODE { get; set; }
        public Double? M_SIAL_CONT { get; set; }
        public Double? M_SIZEUNDER5 { get; set; }
        public Double? M_SIZEUNDER8 { get; set; }
        public Double? M_SIZEUP8 { get; set; }
        public Double? M_SIZEUNDER10 { get; set; }
        public Double? M_SIZEUP10 { get; set; }
        public Double? M_SIZEUNDER10_50 { get; set; }
        public Double? M_SIZEUNDER20_80 { get; set; }
        public Double? M_SIZEUP40 { get; set; }
        public Double? M_ZASHI { get; set; }
        public Double? M_RDI3POINT15 { get; set; }

        /// <summary>
        /// Desc:3级编码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string M_CODE3 { get; set; }

        /// <summary>
        /// Desc:C含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_C_CONT { get; set; }

        /// <summary>
        /// Desc:Fe含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_TFE_CONT { get; set; }

        /// <summary>
        /// Desc:Fe2O3含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_FE2O3_CONT { get; set; }

        /// <summary>
        /// Desc:FeO含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_FEO_CONT { get; set; }

        /// <summary>
        /// Desc:SiO2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIO2_CONT { get; set; }

        /// <summary>
        /// Desc:AL2O3含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_AL2O3_CONT { get; set; }

        /// <summary>
        /// Desc:CaO含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CAO_CONT { get; set; }

        /// <summary>
        /// Desc:MgO含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MGO_CONT { get; set; }

        /// <summary>
        /// Desc:Na2O含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_NA2O_CONT { get; set; }

        /// <summary>
        /// Desc:K2O含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_K2O_CONT { get; set; }

        /// <summary>
        /// Desc:Mn含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MN_CONT { get; set; }

        /// <summary>
        /// Desc:S含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_S_CONT { get; set; }

        /// <summary>
        /// Desc:P含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_P_CONT { get; set; }

        /// <summary>
        /// Desc:TiO2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_TIO2_CONT { get; set; }

        /// <summary>
        /// Desc:水份 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MOISTURE { get; set; }

        /// <summary>
        /// Desc:Zn含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ZN_CONT { get; set; }

        /// <summary>
        /// Desc:灰份 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_COKEASH { get; set; }

        /// <summary>
        /// Desc:CaF2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CAF2_CONT { get; set; }

        /// <summary>
        /// Desc:挥发份含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_VOLA_CONT { get; set; }

        /// <summary>
        /// Desc:H含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_H_CONT { get; set; }

        /// <summary>
        /// Desc:MnO含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MNO_CONT { get; set; }

        /// <summary>
        /// Desc:MnO2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MNO2_CONT { get; set; }

        /// <summary>
        /// Desc:FeS含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_FES_CONT { get; set; }

        /// <summary>
        /// Desc:FeS2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_FES2_CONT { get; set; }

        /// <summary>
        /// Desc:V2O5含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_V2O5_CONT { get; set; }

        /// <summary>
        /// Desc:CuO含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CUO_CONT { get; set; }

        /// <summary>
        /// Desc:Cu2O含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CU2O_CONT { get; set; }

        /// <summary>
        /// Desc:CO2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CO2_CONT { get; set; }

        /// <summary>
        /// Desc:SO2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SO2_CONT { get; set; }

        /// <summary>
        /// Desc:P2O5含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_P2O5_CONT { get; set; }

        /// <summary>
        /// Desc:Pb含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_PB_CONT { get; set; }

        /// <summary>
        /// Desc:焦炭反应性 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_COKE_CRI { get; set; }

        /// <summary>
        /// Desc:焦炭反应后强度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_COKE_CSR { get; set; }

        /// <summary>
        /// Desc:转鼓指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ZHUANGU_INDEX { get; set; }

        /// <summary>
        /// Desc:筛分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SHAIFEN { get; set; }

        /// <summary>
        /// Desc:R2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_R2 { get; set; }

        /// <summary>
        /// Desc:R3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_R3 { get; set; }

        /// <summary>
        /// Desc:球径mm(球团) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_DIAMETER_PELLET { get; set; }

        /// <summary>
        /// Desc:抗压N/球
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_KANGYA_PELLET { get; set; }

        /// <summary>
        /// Desc:-6.3mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_LOW_6POINT3 { get; set; }

        /// <summary>
        /// Desc:+6.3mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_UP_6POINT3 { get; set; }

        /// <summary>
        /// Desc:烧损 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SHAOSUN { get; set; }

        /// <summary>
        /// Desc:膨胀率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_PENGZHANGRATIO { get; set; }

        /// <summary>
        /// Desc:RDL+3.15

        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_RDL3POINT15 { get; set; }

        /// <summary>
        /// Desc:As含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_AS_CONT { get; set; }

        /// <summary>
        /// Desc:Cr含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CR_CONT { get; set; }

        /// <summary>
        /// Desc:Cu含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CU_CONT { get; set; }

        /// <summary>
        /// Desc:Sn含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SN_CONT { get; set; }

        /// <summary>
        /// Desc:H20含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_H2O_CONT { get; set; }

        /// <summary>
        /// Desc:RI 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_RI { get; set; }

        /// <summary>
        /// Desc:△Pmax

        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_PMAX { get; set; }

        /// <summary>
        /// Desc:熔剂粒度-20mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUNDER20 { get; set; }

        /// <summary>
        /// Desc:熔剂粒度-30mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUNDER30 { get; set; }

        /// <summary>
        /// Desc:熔剂粒度<6mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUNDER6 { get; set; }

        /// <summary>
        /// Desc:熔剂粒度6-50mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUNDER6_50 { get; set; }

        /// <summary>
        /// Desc:熔剂粒度20~40mm

        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUNDER20_40 { get; set; }

        /// <summary>
        /// Desc:熔剂粒度20~60mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUNDER20_60 { get; set; }

        /// <summary>
        /// Desc:熔剂粒度30~70mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUNDER30_70 { get; set; }

        /// <summary>
        /// Desc:熔剂粒度50~60mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUNDER50_60 { get; set; }

        /// <summary>
        /// Desc:熔剂粒度60~90mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUNDER60_90 { get; set; }

        /// <summary>
        /// Desc:熔剂粒度90~100mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUNDER90_100 { get; set; }

        /// <summary>
        /// Desc:样品编号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string SMP_NO { get; set; }

        /// <summary>
        /// Desc:试样类型 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string SMP_TYPE { get; set; }

        /// <summary>
        /// Desc:序列号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SERIAL_NO { get; set; }
    }
}

namespace BKYL.BFES.Plugin.Models.AG
{
    public class L3ANALYSI
    {

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int M_CODE { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string M_CODE3 { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_C_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_TFE_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_FE2O3_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_FEO_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIO2_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_AL2O3_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CAO_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MGO_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_NA2O_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_K2O_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MN_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_S_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_P_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_TIO2_CONT { get; set; }

        /// <summary>
        /// Desc:水份 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MOISTURE { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_BASICITY { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ZN_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ASH { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CAF2_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_VOLA_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_H_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_PART_SIZE { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MNO_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MNO2_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_FES_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_FES2_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_V2O5_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CUO_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CU2O_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CO2_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SO2_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_P2O5_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_PB_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_STD_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEABOVE40_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUNDER10_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_COKE_CRI { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_COKE_CSR { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEABOVE75_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEABOVE25_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUNDER25_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEABOVE100_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ZHUANGU_INDEX { get; set; }

        /// <summary>
        /// Desc:RDI+3.15 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_RDI_315 { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SINTER_SIZEABOVE10 { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SINTER_SIZEABOVE5 { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SINTER_SIZEUNDER5 { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_O2 { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUNDER30_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE30_40_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE40_60_CONT { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE60_80_CONT { get; set; }

        /// <summary>
        /// Desc:大于80 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEABOVE80_CONT { get; set; }

        /// <summary>
        /// Desc:筛分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SHAIFEN { get; set; }

        /// <summary>
        /// Desc:烧结5-10 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SINTER_SIZE5_10 { get; set; }

        /// <summary>
        /// Desc:烧结10-16 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SINTER_SIZE10_16 { get; set; }

        /// <summary>
        /// Desc:烧结16-25 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SINTER_SIZE16_25 { get; set; }

        /// <summary>
        /// Desc:烧结25-40 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SINTER_SIZE25_40 { get; set; }

        /// <summary>
        /// Desc:烧结大于40 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SINTER_SIZEABOVE40 { get; set; }

        /// <summary>
        /// Desc:熔剂-硫磺    
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SULFUR { get; set; }

        /// <summary>
        /// Desc:熔剂-三氧化二铬  
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CR2O3 { get; set; }

        /// <summary>
        /// Desc:抗压强度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_KANGYA { get; set; }

        /// <summary>
        /// Desc:抗磨强度(球团) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_KANGMO { get; set; }

        /// <summary>
        /// Desc:熔剂-两小时吸水率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_WATER_ABSORPTION_2H { get; set; }

        /// <summary>
        /// Desc:熔剂-粒度(-325目) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_GRANULARITY_325 { get; set; }

        /// <summary>
        /// Desc:熔剂-膨胀倍数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_EXPANSION_RATIO { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_H2O { get; set; }

        /// <summary>
        /// Desc:蒙脱石 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MONTMORILLONITE { get; set; }

        /// <summary>
        /// Desc:熔剂-粒度小于5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUNDER5 { get; set; }

        /// <summary>
        /// Desc:烧结矿碱度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_R { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_COKE_M40 { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_COKE_M10 { get; set; }

        /// <summary>
        /// Desc:RDI-0.5 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_RDI_05 { get; set; }

        /// <summary>
        /// Desc:RDI+6.3 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_RDI_63 { get; set; }

        /// <summary>
        /// Desc:m_dischargeweigh
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double M_DISCHARGEWEIGH { get; set; }

    }
}

namespace BKYL.BFES.Plugin.Models.JG
{
    public class L3ANALYSI
    {

        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:料种代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int M_CODE { get; set; }

        /// <summary>
        /// Desc:3级料种代码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string M_CODE3 { get; set; }

        /// <summary>
        /// Desc:碳含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_C_CONT { get; set; }

        /// <summary>
        /// Desc:铁含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_TFE_CONT { get; set; }

        /// <summary>
        /// Desc:氧化铁含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_FE2O3_CONT { get; set; }

        /// <summary>
        /// Desc:氧化亚铁含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_FEO_CONT { get; set; }

        /// <summary>
        /// Desc:二氧化硅含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIO2_CONT { get; set; }

        /// <summary>
        /// Desc:三氧化二铝含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_AL2O3_CONT { get; set; }

        /// <summary>
        /// Desc:氧化钙含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CAO_CONT { get; set; }

        /// <summary>
        /// Desc:氧化镁含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MGO_CONT { get; set; }

        /// <summary>
        /// Desc:氧化钠含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_NA2O_CONT { get; set; }

        /// <summary>
        /// Desc:氧化钾含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_K2O_CONT { get; set; }

        /// <summary>
        /// Desc:氧化锰含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MN_CONT { get; set; }

        /// <summary>
        /// Desc:硫含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_S_CONT { get; set; }

        /// <summary>
        /// Desc:磷含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_P_CONT { get; set; }

        /// <summary>
        /// Desc:氧化钛含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_TIO2_CONT { get; set; }

        /// <summary>
        /// Desc:湿度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MOISTURE { get; set; }

        /// <summary>
        /// Desc:碱度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_BASICITY { get; set; }

        /// <summary>
        /// Desc:锌含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ZN_CONT { get; set; }

        /// <summary>
        /// Desc:灰份 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_COKEASH { get; set; }

        /// <summary>
        /// Desc:氟化钙含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CAF2_CONT { get; set; }

        /// <summary>
        /// Desc:挥发份 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_VOLA_CONT { get; set; }

        /// <summary>
        /// Desc:氢含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_H_CONT { get; set; }

        /// <summary>
        /// Desc:平均粒径 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_PART_SIZE { get; set; }

        /// <summary>
        /// Desc:一氧化锰含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MNO_CONT { get; set; }

        /// <summary>
        /// Desc:二氧化锰含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MNO2_CONT { get; set; }

        /// <summary>
        /// Desc:硫化铁含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_FES_CONT { get; set; }

        /// <summary>
        /// Desc:硫化亚铁含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_FES2_CONT { get; set; }

        /// <summary>
        /// Desc:五氧化二钒含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_V2O5_CONT { get; set; }

        /// <summary>
        /// Desc:氧化铜含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CUO_CONT { get; set; }

        /// <summary>
        /// Desc:氧化亚铜含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CU2O_CONT { get; set; }

        /// <summary>
        /// Desc:二氧化碳含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CO2_CONT { get; set; }

        /// <summary>
        /// Desc:二氧化硫含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SO2_CONT { get; set; }

        /// <summary>
        /// Desc:五氧化二磷含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_P2O5_CONT { get; set; }

        /// <summary>
        /// Desc:氧化铅 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_PB_CONT { get; set; }

        /// <summary>
        /// Desc:试样号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string USERC1 { get; set; }

        /// <summary>
        /// Desc:转鼓指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_DI { get; set; }

        /// <summary>
        /// Desc:抗压强度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_KYN { get; set; }

        /// <summary>
        /// Desc:焦炭M10 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_M10 { get; set; }

        /// <summary>
        /// Desc:焦炭M40 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_M40 { get; set; }

        /// <summary>
        /// Desc:焦炭CSR 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CSR { get; set; }

        /// <summary>
        /// Desc:焦炭CRI 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CRI { get; set; }
        /// <summary>
        /// Desc:报表系统中的时间
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime M_PASTTIME { get; set; }

    }
}

namespace BKYL.BFES.Plugin.Models.TG
{
    public class L3ANALYSI
    {

        /// <summary>
        /// Desc:取样时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:1级编码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int M_CODE { get; set; }

        /// <summary>
        /// Desc:3级编码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string M_CODE3 { get; set; }

        /// <summary>
        /// Desc:C含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_C_CONT { get; set; }

        /// <summary>
        /// Desc:Fe含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_TFE_CONT { get; set; }

        /// <summary>
        /// Desc:Fe2O3含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_FE2O3_CONT { get; set; }

        /// <summary>
        /// Desc:FeO含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_FEO_CONT { get; set; }

        /// <summary>
        /// Desc:SiO2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIO2_CONT { get; set; }

        /// <summary>
        /// Desc:AL2O3含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_AL2O3_CONT { get; set; }

        /// <summary>
        /// Desc:CaO含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CAO_CONT { get; set; }

        /// <summary>
        /// Desc:MgO含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MGO_CONT { get; set; }

        /// <summary>
        /// Desc:Na2O含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_NA2O_CONT { get; set; }

        /// <summary>
        /// Desc:K2O含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_K2O_CONT { get; set; }

        /// <summary>
        /// Desc:Mn含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MN_CONT { get; set; }

        /// <summary>
        /// Desc:S含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_S_CONT { get; set; }

        /// <summary>
        /// Desc:P含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_P_CONT { get; set; }

        /// <summary>
        /// Desc:TiO2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_TIO2_CONT { get; set; }

        /// <summary>
        /// Desc:水份 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MOISTURE { get; set; }

        /// <summary>
        /// Desc:Zn含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ZN_CONT { get; set; }

        /// <summary>
        /// Desc:灰份 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_COKEASH { get; set; }

        /// <summary>
        /// Desc:CaF2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CAF2_CONT { get; set; }

        /// <summary>
        /// Desc:挥发份含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_VOLA_CONT { get; set; }

        /// <summary>
        /// Desc:H含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_H_CONT { get; set; }

        /// <summary>
        /// Desc:MnO含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MNO_CONT { get; set; }

        /// <summary>
        /// Desc:MnO2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_MNO2_CONT { get; set; }

        /// <summary>
        /// Desc:FeS含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_FES_CONT { get; set; }

        /// <summary>
        /// Desc:FeS2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_FES2_CONT { get; set; }

        /// <summary>
        /// Desc:V2O5含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_V2O5_CONT { get; set; }

        /// <summary>
        /// Desc:CuO含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CUO_CONT { get; set; }

        /// <summary>
        /// Desc:Cu2O含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CU2O_CONT { get; set; }

        /// <summary>
        /// Desc:CO2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CO2_CONT { get; set; }

        /// <summary>
        /// Desc:SO2含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SO2_CONT { get; set; }

        /// <summary>
        /// Desc:P2O5含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_P2O5_CONT { get; set; }

        /// <summary>
        /// Desc:Pb含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_PB_CONT { get; set; }

        /// <summary>
        /// Desc:焦炭反应性 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_COKE_CRI { get; set; }

        /// <summary>
        /// Desc:焦炭反应后强度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_COKE_CSR { get; set; }

        /// <summary>
        /// Desc:转鼓指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_ZHUANGU_INDEX { get; set; }

        /// <summary>
        /// Desc:R2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_R2 { get; set; }

        /// <summary>
        /// Desc:R3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_R3 { get; set; }

        /// <summary>
        /// Desc:"抗压N/球(抗压强度)" 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_KANGYA_PELLET { get; set; }

        /// <summary>
        /// Desc:烧损 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SHAOSUN { get; set; }

        /// <summary>
        /// Desc:膨胀率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_PENGZHANGRATIO { get; set; }

        /// <summary>
        /// Desc:As含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_AS_CONT { get; set; }

        /// <summary>
        /// Desc:Cr含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CR_CONT { get; set; }

        /// <summary>
        /// Desc:Cu含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_CU_CONT { get; set; }

        /// <summary>
        /// Desc:Sn含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SN_CONT { get; set; }

        /// <summary>
        /// Desc:H20含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_H2O_CONT { get; set; }

        /// <summary>
        /// Desc:RI 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_RI { get; set; }

        /// <summary>
        /// Desc:样品编号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string SMP_NO { get; set; }

        /// <summary>
        /// Desc:试样类型 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string SMP_TYPE { get; set; }

        /// <summary>
        /// Desc:序列号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? SERIAL_NO { get; set; }

        /// <summary>
        /// Desc:粒度-5mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUNDER5 { get; set; }

        /// <summary>
        /// Desc:小时标志 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? R_HOUR_MSG { get; set; }

        /// <summary>
        /// Desc:SO3含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SO3_CONT { get; set; }

        /// <summary>
        /// Desc:焦炭M10 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_COKE_M10 { get; set; }

        /// <summary>
        /// Desc:焦炭M40 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_COKE_M40 { get; set; }

        /// <summary>
        /// Desc:抗磨强度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_KANGMO_INDEX { get; set; }

        /// <summary>
        /// Desc:蜂焦 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_COKE_FENG { get; set; }

        /// <summary>
        /// Desc:合格率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_HEGE_RATIO { get; set; }

        /// <summary>
        /// Desc:黑头焦 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_COKE_HEITOU { get; set; }

        /// <summary>
        /// Desc:R4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_R4 { get; set; }

        /// <summary>
        /// Desc:热爆裂性 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_REBAOLIE { get; set; }

        /// <summary>
        /// Desc:爆炸性 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_BAOZHA { get; set; }

        /// <summary>
        /// Desc:收到基低位发热量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_STAND_HEAT { get; set; }

        /// <summary>
        /// Desc:着火点 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_FIREPOINT { get; set; }

        /// <summary>
        /// Desc:全量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_QUANLIANG { get; set; }

        /// <summary>
        /// Desc:"RDI" 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_RDI { get; set; }

        /// <summary>
        /// Desc:200目 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUNDER200 { get; set; }

        /// <summary>
        /// Desc:粒度-0.5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUNDER05 { get; set; }

        /// <summary>
        /// Desc:粒度0.5-1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE05_1 { get; set; }

        /// <summary>
        /// Desc:粒度1-3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE1_3 { get; set; }

        /// <summary>
        /// Desc:粒度3-5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE3_5 { get; set; }

        /// <summary>
        /// Desc:粒度+3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUP3 { get; set; }

        /// <summary>
        /// Desc:粒度+5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUP5 { get; set; }

        /// <summary>
        /// Desc:粒度5~8mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE5_8 { get; set; }

        /// <summary>
        /// Desc:粒度8~16mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE8_16 { get; set; }

        /// <summary>
        /// Desc:粒度5-10mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE5_10 { get; set; }

        /// <summary>
        /// Desc:粒度10-25mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE10_25 { get; set; }

        /// <summary>
        /// Desc:粒度25-50mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE25_50 { get; set; }

        /// <summary>
        /// Desc:粒度+50mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUP50 { get; set; }

        /// <summary>
        /// Desc:粒度-15mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUNDER15 { get; set; }

        /// <summary>
        /// Desc:粒度15-25mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE15_25 { get; set; }

        /// <summary>
        /// Desc:粒度25-36mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE25_36 { get; set; }

        /// <summary>
        /// Desc:粒度36-40mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE36_40 { get; set; }

        /// <summary>
        /// Desc:粒度25-40mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE25_40 { get; set; }

        /// <summary>
        /// Desc:粒度40-50mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE40_50 { get; set; }

        /// <summary>
        /// Desc:粒度50-75mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE50_75 { get; set; }

        /// <summary>
        /// Desc:粒度+75mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUP75 { get; set; }

        /// <summary>
        /// Desc:平均粒径 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_AVGSIZE { get; set; }

        /// <summary>
        /// Desc:粒度5-25mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE5_25 { get; set; }

        /// <summary>
        /// Desc:粒度+40mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUP40 { get; set; }

        /// <summary>
        /// Desc:粒度40-75mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE40_75 { get; set; }

        /// <summary>
        /// Desc:粒度+25mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUP25 { get; set; }

        /// <summary>
        /// Desc:粒度+16mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZEUP16 { get; set; }

        /// <summary>
        /// Desc:粒度10-16mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE10_16 { get; set; }

        /// <summary>
        /// Desc:粒度16-25mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE16_25 { get; set; }

        /// <summary>
        /// Desc:粒度8-25mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE8_25 { get; set;}

        /// <summary>
        /// Desc:粒度8-40mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_SIZE8_40 { get; set; }

        /// <summary>
        /// Desc:粒度-8mm 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double M_SIZEUNDER8 { get; set; }
    }
}


