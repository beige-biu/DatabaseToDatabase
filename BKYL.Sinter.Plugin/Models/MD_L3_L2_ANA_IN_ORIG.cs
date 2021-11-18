using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MD_L3_L2_ANA_IN_ORIG
    {
        
        /// <summary>
        /// Desc:插入时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:采样时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SAMPLETIME {get;set;}

        /// <summary>
        /// Desc:报样时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? REPORTTIME {get;set;}

        /// <summary>
        /// Desc:试样号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SAMPLE_CODE {get;set;}

        /// <summary>
        /// Desc:品名代码（三级编码） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NAME_CODE {get;set;}

        /// <summary>
        /// Desc:二级代码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? L2_CODE {get;set;}

        /// <summary>
        /// Desc:物料名 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MATERIAL_NAME {get;set;}

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
        /// Desc:MnO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_MNO {get;set;}

        /// <summary>
        /// Desc:转鼓指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? TI6Z3 {get;set;}

        /// <summary>
        /// Desc:转鼓强度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? DI {get;set;}

        /// <summary>
        /// Desc:筛分指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? SCRENING {get;set;}

        /// <summary>
        /// Desc:RDI-0.5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? RDI_0Z5 {get;set;}

        /// <summary>
        /// Desc:RDI+3.15 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? RDI_3Z15 {get;set;}

        /// <summary>
        /// Desc:RDI+6.3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? RDI_Z6Z3 {get;set;}

        /// <summary>
        /// Desc:<5mm（烧结矿）、高返、球团、烧返 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_5 {get;set;}

        /// <summary>
        /// Desc:5-10mm（烧结矿）、球团、 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_10_5 {get;set;}

        /// <summary>
        /// Desc:10-16mm（烧结矿）、球团、 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_16_10 {get;set;}

        /// <summary>
        /// Desc:16-25mm（烧结矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_25_16 {get;set;}

        /// <summary>
        /// Desc:25-40mm（烧结矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_40_25 {get;set;}

        /// <summary>
        /// Desc:>40mm（烧结矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_Z40 {get;set;}

        /// <summary>
        /// Desc:<3mm（焦粉） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_3 {get;set;}

        /// <summary>
        /// Desc:>3mm（焦粉、生石灰粉） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_Z3 {get;set;}

        /// <summary>
        /// Desc:<1mm（生石灰粉、焦粉、混匀料） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_LOW_1 {get;set;}

        /// <summary>
        /// Desc:1－3mm（生石灰粉、焦粉） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_1_3 {get;set;}

        /// <summary>
        /// Desc:<20mm（生石灰粉）、硅石块、 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_LOW_20 {get;set;}

        /// <summary>
        /// Desc:20-90mm（生石灰粉）、硅石块、 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_20_90 {get;set;}

        /// <summary>
        /// Desc:>90mm（生石灰粉）、硅石块 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_UP_90 {get;set;}

        /// <summary>
        /// Desc:>180目(煤粉) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_M_UP_180 {get;set;}

        /// <summary>
        /// Desc:180-200目(煤粉) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_M_180_200 {get;set;}

        /// <summary>
        /// Desc:<200目(煤粉) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_M_LOW_200 {get;set;}

        /// <summary>
        /// Desc:10－30mm(白云石粉) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_10_30 {get;set;}

        /// <summary>
        /// Desc:<0.04mm（铁矿粉、外购铁精粉） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_0Z04 {get;set;}

        /// <summary>
        /// Desc:<0.074mm（铁矿粉、外购铁精粉） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_0Z075 {get;set;}

        /// <summary>
        /// Desc:<0.15mm（铁矿粉、外购铁精粉） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_0Z15 {get;set;}

        /// <summary>
        /// Desc:<0.5mm（铁矿粉） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_0Z5 {get;set;}

        /// <summary>
        /// Desc:0.25－1mm（铁矿粉、外购铁精粉） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_0Z25_1 {get;set;}

        /// <summary>
        /// Desc:>6.3mm（铁矿粉、外购铁精粉） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_6Z3 {get;set;}

        /// <summary>
        /// Desc:<10mm（反焦）、白云石块、高炉焦屑、石灰石块、活性石灰石、蛇纹石块 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_10 {get;set;}

        /// <summary>
        /// Desc:20-10mm（反焦） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_20_10 {get;set;}

        /// <summary>
        /// Desc:25-10mm（反焦） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_25_10 {get;set;}

        /// <summary>
        /// Desc:25-20mm（反焦） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_25_20 {get;set;}

        /// <summary>
        /// Desc:>25mm（反焦） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_UP_25 {get;set;}

        /// <summary>
        /// Desc:30-25mm（反焦） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_30_25 {get;set;}

        /// <summary>
        /// Desc:>30mm（反焦）、白云石块、石灰石块、活性石灰石、蛇纹石块、 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_Z30 {get;set;}

        /// <summary>
        /// Desc:0-15mm（氧化铁屑） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_0_15 {get;set;}

        /// <summary>
        /// Desc:>15mm（氧化铁屑） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_UP_15 {get;set;}

        /// <summary>
        /// Desc:<30mm（焦炭） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_LOW_30 {get;set;}

        /// <summary>
        /// Desc:<25mm（焦炭） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_LOW_25 {get;set;}

        /// <summary>
        /// Desc:>40mm（焦炭） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_UP_40 {get;set;}

        /// <summary>
        /// Desc:40-60mm（焦炭） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_40_60 {get;set;}

        /// <summary>
        /// Desc:>60mm（焦炭） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_UP_60 {get;set;}

        /// <summary>
        /// Desc:抗碎强度M10（焦炭） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M10 {get;set;}

        /// <summary>
        /// Desc:抗碎强度M25（焦炭） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M25 {get;set;}

        /// <summary>
        /// Desc:抗碎强度M40（焦炭） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M40 {get;set;}

        /// <summary>
        /// Desc:内在水分（焦炭） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? MAD {get;set;}

        /// <summary>
        /// Desc:反应性（焦炭） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? CRI {get;set;}

        /// <summary>
        /// Desc:反应性强度（焦炭） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? CSR {get;set;}

        /// <summary>
        /// Desc:抗磨强度（焦炭、球团） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? M_10 {get;set;}

        /// <summary>
        /// Desc:高位热值(煤粉、无烟煤、烟煤) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GWR {get;set;}

        /// <summary>
        /// Desc:发热量(煤粉、无烟煤) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? HEATQTY {get;set;}

        /// <summary>
        /// Desc:抗压强度(N)(球团) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? CCS {get;set;}

        /// <summary>
        /// Desc:还原度(RI)（球团、块矿、烧结矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? RI {get;set;}

        /// <summary>
        /// Desc:还原膨胀(RSI)（球团） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? RSI {get;set;}

        /// <summary>
        /// Desc:软化开始（球团、块矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? RUANHKS {get;set;}

        /// <summary>
        /// Desc:软化终了（球团、块矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? RUANHZL {get;set;}

        /// <summary>
        /// Desc:软化区间（球团、块矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? RUANHQJ {get;set;}

        /// <summary>
        /// Desc:熔融开始（球团、块矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? RONGHKS {get;set;}

        /// <summary>
        /// Desc:熔融终了（球团、块矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? RONGHZL {get;set;}

        /// <summary>
        /// Desc:熔融区间（球团、块矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? RONGHQJ {get;set;}

        /// <summary>
        /// Desc:压差（球团、块矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? YC {get;set;}

        /// <summary>
        /// Desc:软容层厚度（球团、块矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? RRCHD {get;set;}

        /// <summary>
        /// Desc:熔滴指数（球团、块矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? RDZS {get;set;}

        /// <summary>
        /// Desc:抗裂指数(DI-0.5)（块矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? DI_0Z5 {get;set;}

        /// <summary>
        /// Desc:抗裂指数(DI-3.15)（块矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? DI_3Z15 {get;set;}

        /// <summary>
        /// Desc:抗裂指数(DI-6.3)（块矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? DI_6Z3 {get;set;}

        /// <summary>
        /// Desc:10－6.3mm（块矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_10_6Z3 {get;set;}

        /// <summary>
        /// Desc:>31.5mm（块矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_UP_31Z5 {get;set;}

        /// <summary>
        /// Desc:31.5－25mm（块矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_31Z5_25 {get;set;}

        /// <summary>
        /// Desc:<6.3mm（块矿） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_LOW_6Z3 {get;set;}

        /// <summary>
        /// Desc:>10mm(高炉焦屑) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_UP_10 {get;set;}

        /// <summary>
        /// Desc:20-6.3mm(球团矿) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_20_6Z3 {get;set;}

        /// <summary>
        /// Desc:8－5mm(球团矿) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_8_5 {get;set;}

        /// <summary>
        /// Desc:粒度16-8mm(球团矿) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_16_8 {get;set;}

        /// <summary>
        /// Desc:>5mm(球团矿) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_UP_5 {get;set;}

        /// <summary>
        /// Desc:活性度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? AOL {get;set;}

        /// <summary>
        /// Desc:>16mm（球团） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_UP_16 {get;set;}

        /// <summary>
        /// Desc:ADE1（混匀料） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? GRIT_UP_1 {get;set;}

        /// <summary>
        /// Desc:Ti 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? C_TI {get;set;}
        /// <summary>
        /// Desc:Ti 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? CAOZMGO { get; set; }

    }
}
