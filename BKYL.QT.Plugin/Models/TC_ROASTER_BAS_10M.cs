using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Models
{
    public class TC_ROASTER_BAS_10M
    {

        /// <summary>
        /// Desc:插入时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:成品矿平均粒度  1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_GRIT { get; set; }
        /// <summary>
        /// Desc:成品矿平均粒度氧化铁得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_GRIT { get; set; }
        /// <summary>
        /// Desc:成品矿平均粒度抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_GRIT { get; set; }

        /// <summary>
        /// Desc:成品矿平均粒度转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_GRIT { get; set; }

        /// <summary>
        /// Desc:成品矿平均粒度料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_GRIT { get; set; }

        /// <summary>
        /// Desc:成品矿平均粒度其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_GRIT { get; set; }

        /// <summary>
        /// Desc:入机小时生球量(焙烧机给料量)2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FQ_W { get; set; }

        /// <summary>
        /// Desc:入机小时生球量亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_FQ { get; set; }

        /// <summary>
        /// Desc:入机小时生球量抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_FQ { get; set; }

        /// <summary>
        /// Desc:入机小时生球量转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_FQ { get; set; }

        /// <summary>
        /// Desc:入机小时生球量料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_FQ { get; set; }

        /// <summary>
        /// Desc:入机小时生球量其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_FQ { get; set; }

        /// <summary>
        /// Desc:15#风箱温度     3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_TE_15 { get; set; }

        /// <summary>
        /// Desc:15#风箱温亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_15 { get; set; }

        /// <summary>
        /// Desc:15#风箱温抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_15 { get; set; }

        /// <summary>
        /// Desc:15#风箱温转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_15 { get; set; }

        /// <summary>
        /// Desc:15#风箱温料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_15 { get; set; }

        /// <summary>
        /// Desc:15#风箱温其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_15 { get; set; }

        /// <summary>
        /// Desc:焙烧段烟罩压力3      4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_PH_TP { get; set; }

        /// <summary>
        /// Desc:焙烧段烟罩压力3亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_PH_TP { get; set; }

        /// <summary>
        /// Desc:焙烧段烟罩压力3抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_PH_TP { get; set; }

        /// <summary>
        /// Desc:焙烧段烟罩压力3转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_PH_TP { get; set; }

        /// <summary>
        /// Desc:焙烧段烟罩压力3料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_PH_TP { get; set; }

        /// <summary>
        /// Desc:焙烧段烟罩压力3其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_PH_TP { get; set; }

        /// <summary>
        /// Desc:鼓干段烟罩温度    5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FT_BT_TP { get; set; }

        /// <summary>
        /// Desc:鼓干段烟罩温度亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_BT_TP { get; set; }

        /// <summary>
        /// Desc:鼓干段烟罩温度抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_BT_TP { get; set; }

        /// <summary>
        /// Desc:鼓干段烟罩温度转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_BT_TP { get; set; }

        /// <summary>
        /// Desc:鼓干段烟罩温度料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_BT_TP { get; set; }

        /// <summary>
        /// Desc:鼓干段烟罩温度其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_BT_TP { get; set; }

        /// <summary>
        /// Desc:铺底料料位(铺底料重)     6 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_UC_BL_W { get; set; }

        /// <summary>
        /// Desc:铺底料料位(铺底料重)亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_UC { get; set; }

        /// <summary>
        /// Desc:铺底料料位(铺底料重)抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_UC { get; set; }

        /// <summary>
        /// Desc:铺底料料位(铺底料重)转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_UC { get; set; }

        /// <summary>
        /// Desc:铺底料料位(铺底料重)料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_UC { get; set; }

        /// <summary>
        /// Desc:铺底料料位(铺底料重)其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_UC { get; set; }

        /// <summary>
        /// Desc:卸料仓料量    7 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_PH_BL_W { get; set; }

        /// <summary>
        /// Desc:卸料仓料量亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_BL { get; set; }

        /// <summary>
        /// Desc:卸料仓料量抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_BL { get; set; }

        /// <summary>
        /// Desc:卸料仓料量转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_BL { get; set; }

        /// <summary>
        /// Desc:卸料仓料量料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_BL { get; set; }

        /// <summary>
        /// Desc:卸料仓料量其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_BL { get; set; }

        /// <summary>
        /// Desc:带式机机速    8 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_PH_BL_EP { get; set; }

        /// <summary>
        /// Desc:带式机机速亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_EP { get; set; }

        /// <summary>
        /// Desc:带式机机速抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_EP { get; set; }

        /// <summary>
        /// Desc:带式机机速转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_EP { get; set; }

        /// <summary>
        /// Desc:带式机机速料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_EP { get; set; }

        /// <summary>
        /// Desc:带式机机速其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_EP { get; set; }

        /// <summary>
        /// Desc:台车料厚   9 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_T_H_BCD { get; set; }

        /// <summary>
        /// Desc:台车料厚亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_BCD { get; set; }

        /// <summary>
        /// Desc:台车料厚抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_BCD { get; set; }

        /// <summary>
        /// Desc:台车料厚转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_BCD { get; set; }

        /// <summary>
        /// Desc:台车料厚料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_BCD { get; set; }

        /// <summary>
        /// Desc:台车料厚其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_BCD { get; set; }

        /// <summary>
        /// Desc:台车料厚偏差    10 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_T_H_BCD_PD { get; set; }

        /// <summary>
        /// Desc:台车料厚偏差亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_PD { get; set; }

        /// <summary>
        /// Desc:台车料厚偏差抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_PD { get; set; }

        /// <summary>
        /// Desc:台车料厚偏差转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_PD { get; set; }

        /// <summary>
        /// Desc:台车料厚偏差料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_PD { get; set; }

        /// <summary>
        /// Desc:台车料厚偏差其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_PD { get; set; }

        /// <summary>
        /// Desc:二冷段烟罩温度2     11 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_PH_R_TE { get; set; }

        /// <summary>
        /// Desc:二冷段烟罩温度2亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_R { get; set; }

        /// <summary>
        /// Desc:二冷段烟罩温度2抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_R { get; set; }

        /// <summary>
        /// Desc:二冷段烟罩温度2转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_R { get; set; }

        /// <summary>
        /// Desc:二冷段烟罩温度2料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_R { get; set; }

        /// <summary>
        /// Desc:二冷段烟罩温度2其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_R { get; set; }

        /// <summary>
        /// Desc:鼓干鼓风机温度     12 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FT_DD_TE { get; set; }

        /// <summary>
        /// Desc:鼓干鼓风机温度亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_DD { get; set; }

        /// <summary>
        /// Desc:鼓干鼓风机温度抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_DD { get; set; }

        /// <summary>
        /// Desc:鼓干鼓风机温度转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_DD { get; set; }

        /// <summary>
        /// Desc:鼓干鼓风机温度料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_DD { get; set; }

        /// <summary>
        /// Desc:鼓干鼓风机温度其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_DD { get; set; }

        /// <summary>
        /// Desc:主引风机温度     13 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FT_ML_TE { get; set; }

        /// <summary>
        /// Desc:主引风机温度亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_ML { get; set; }

        /// <summary>
        /// Desc:主引风机温度抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_ML { get; set; }

        /// <summary>
        /// Desc:主引风机温度转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_ML { get; set; }

        /// <summary>
        /// Desc:主引风机温度料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_ML { get; set; }

        /// <summary>
        /// Desc:主引风机温度其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_ML { get; set; }

        /// <summary>
        /// Desc:回热风机温度     14 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FT_RH_TE { get; set; }

        /// <summary>
        /// Desc:回热风机温度亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_RH { get; set; }

        /// <summary>
        /// Desc:回热风机温度抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_RH { get; set; }

        /// <summary>
        /// Desc:回热风机温度转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_RH { get; set; }

        /// <summary>
        /// Desc:回热风机温度料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_RH { get; set; }

        /// <summary>
        /// Desc:回热风机温度其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_RH { get; set; }

        /// <summary>
        /// Desc:抽干段压力     15 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_PH_PI_TP { get; set; }

        /// <summary>
        /// Desc:抽干段压力亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_PI { get; set; }

        /// <summary>
        /// Desc:抽干段压力抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_PI { get; set; }

        /// <summary>
        /// Desc:抽干段压力转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_PI { get; set; }

        /// <summary>
        /// Desc:抽干段压力料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_PI { get; set; }

        /// <summary>
        /// Desc:抽干段压力其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_PI { get; set; }

        /// <summary>
        /// Desc:烧嘴3区温度     16 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_BURNERQ3_TE { get; set; }

        /// <summary>
        /// Desc:烧嘴3区温度亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_Q3 { get; set; }

        /// <summary>
        /// Desc:烧嘴3区温度抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_Q3 { get; set; }

        /// <summary>
        /// Desc:烧嘴3区温度转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_Q3 { get; set; }

        /// <summary>
        /// Desc:烧嘴3区温度料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_Q3 { get; set; }

        /// <summary>
        /// Desc:烧嘴3区温度其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_Q3 { get; set; }

        /// <summary>
        /// Desc:烧嘴4区温度     17 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_BURNERQ4_TE { get; set; }

        /// <summary>
        /// Desc:烧嘴4区温度亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_Q4 { get; set; }

        /// <summary>
        /// Desc:烧嘴4区温度抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_Q4 { get; set; }

        /// <summary>
        /// Desc:烧嘴4区温度转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_Q4 { get; set; }

        /// <summary>
        /// Desc:烧嘴4区温度料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_Q4 { get; set; }

        /// <summary>
        /// Desc:烧嘴4区温度其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_Q4 { get; set; }

        /// <summary>
        /// Desc:烧嘴5区温度    18 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_BURNERQ5_TE { get; set; }

        /// <summary>
        /// Desc:烧嘴5区温度亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_Q5 { get; set; }

        /// <summary>
        /// Desc:烧嘴5区温度抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_Q5 { get; set; }

        /// <summary>
        /// Desc:烧嘴5区温度转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_Q5 { get; set; }

        /// <summary>
        /// Desc:烧嘴5区温度料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_Q5 { get; set; }

        /// <summary>
        /// Desc:烧嘴5区温度其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_Q5 { get; set; }

        /// <summary>
        /// Desc:烧嘴6区温度    19 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_BURNERQ6_TE { get; set; }

        /// <summary>
        /// Desc:烧嘴6区温度亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_Q6 { get; set; }

        /// <summary>
        /// Desc:烧嘴6区温度抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_Q6 { get; set; }

        /// <summary>
        /// Desc:烧嘴6区温度转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_Q6 { get; set; }

        /// <summary>
        /// Desc:烧嘴6区温度料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_Q6 { get; set; }

        /// <summary>
        /// Desc:烧嘴6区温度其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_Q6 { get; set; }

        /// <summary>
        /// Desc:预热段烟罩1区温度    20 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_ROASTERQ1_TE { get; set; }

        /// <summary>
        /// Desc:预热段烟罩1区温度亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_RQ1 { get; set; }

        /// <summary>
        /// Desc:预热段烟罩1区温度抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_RQ1 { get; set; }

        /// <summary>
        /// Desc:预热段烟罩1区温度转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_RQ1 { get; set; }

        /// <summary>
        /// Desc:预热段烟罩1区温度料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_RQ1 { get; set; }

        /// <summary>
        /// Desc:预热段烟罩1区温度其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_RQ1 { get; set; }

        /// <summary>
        /// Desc:预热段烟罩4区温度    21 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_ROASTERQ4_TE { get; set; }

        /// <summary>
        /// Desc:预热段烟罩4区温度亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_RQ4 { get; set; }

        /// <summary>
        /// Desc:预热段烟罩4区温度抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_RQ4 { get; set; }

        /// <summary>
        /// Desc:预热段烟罩4区温度转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_RQ4 { get; set; }

        /// <summary>
        /// Desc:预热段烟罩4区温度料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_RQ4 { get; set; }

        /// <summary>
        /// Desc:预热段烟罩4区温度其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_RQ4 { get; set; }

        /// <summary>
        /// Desc:预热段烟罩5区温度   22 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_ROASTERQ5_TE { get; set; }

        /// <summary>
        /// Desc:预热段烟罩5区温度亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_RQ5 { get; set; }

        /// <summary>
        /// Desc:预热段烟罩5区温度抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_RQ5 { get; set; }

        /// <summary>
        /// Desc:预热段烟罩5区温度转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_RQ5 { get; set; }

        /// <summary>
        /// Desc:预热段烟罩5区温度料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_RQ5 { get; set; }

        /// <summary>
        /// Desc:预热段烟罩5区温度其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_RQ5 { get; set; }

        /// <summary>
        /// Desc:主引风机转速   23 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FT_ML_ST { get; set; }

        /// <summary>
        /// Desc:主引风机转速亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_MLST { get; set; }

        /// <summary>
        /// Desc:主引风机转速抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_MLST { get; set; }

        /// <summary>
        /// Desc:主引风机转速转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_MLST { get; set; }

        /// <summary>
        /// Desc:主引风机转速料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_MLST { get; set; }

        /// <summary>
        /// Desc:主引风机转速其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_MLST { get; set; }

        /// <summary>
        /// Desc:回热风机转速   24 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FT_RH_ST { get; set; }

        /// <summary>
        /// Desc:回热风机转速亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_RHST { get; set; }

        /// <summary>
        /// Desc:回热风机转速抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_RHST { get; set; }

        /// <summary>
        /// Desc:回热风机转速转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_RHST { get; set; }

        /// <summary>
        /// Desc:回热风机转速料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_RHST { get; set; }

        /// <summary>
        /// Desc:回热风机转速其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_RHST { get; set; }

        /// <summary>
        /// Desc:冷却风机转速   25 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FT_CL_ST { get; set; }

        /// <summary>
        /// Desc:冷却风机转速亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_CLST { get; set; }

        /// <summary>
        /// Desc:冷却风机转速抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_CLST { get; set; }

        /// <summary>
        /// Desc:冷却风机转速转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_CLST { get; set; }

        /// <summary>
        /// Desc:冷却风机转速料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_CLST { get; set; }

        /// <summary>
        /// Desc:冷却风机转速其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_CLST { get; set; }

        /// <summary>
        /// Desc:皂土量    26 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_BTE { get; set; }

        /// <summary>
        /// Desc:皂土量亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_BTE { get; set; }

        /// <summary>
        /// Desc:皂土量抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_BTE { get; set; }

        /// <summary>
        /// Desc:皂土量转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_BTE { get; set; }

        /// <summary>
        /// Desc:皂土量料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_BTE { get; set; }

        /// <summary>
        /// Desc:皂土量其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_BTE { get; set; }

        /// <summary>
        /// Desc:瞬时湿返率    27 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_WR { get; set; }

        /// <summary>
        /// Desc:瞬时湿返率亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_WR { get; set; }

        /// <summary>
        /// Desc:瞬时湿返率抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_WR { get; set; }

        /// <summary>
        /// Desc:瞬时湿返率转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_WR { get; set; }

        /// <summary>
        /// Desc:瞬时湿返率料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_WR { get; set; }

        /// <summary>
        /// Desc:瞬时湿返率其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_WR { get; set; }

        /// <summary>
        /// Desc:干燥后水分    28 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRY_A_WATER { get; set; }

        /// <summary>
        /// Desc:干燥后水分亚铁含量得分  
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_WATER { get; set; }

        /// <summary>
        /// Desc:干燥后水分抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_WATER { get; set; }

        /// <summary>
        /// Desc:干燥后水分转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_WATER { get; set; }

        /// <summary>
        /// Desc:干燥后水分料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_WATER { get; set; }

        /// <summary>
        /// Desc:干燥后水分其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_WATER { get; set; }

        /// <summary>
        /// Desc:混合后水分    29 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Double TC_A_MIX { get; set; }

        /// <summary>
        /// Desc:混合后水分亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_AMIX { get; set; }

        /// <summary>
        /// Desc:混合后水分抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_AMIX { get; set; }

        /// <summary>
        /// Desc:混合后水分转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_AMIX { get; set; }

        /// <summary>
        /// Desc:混合后水分料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_AMIX { get; set; }

        /// <summary>
        /// Desc:混合后水分其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_AMIX { get; set; }

        /// <summary>
        /// Desc:8#风箱压力     30 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_BELLOWS08_TP { get; set; }

        /// <summary>
        /// Desc:8#风箱压力亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_08 { get; set; }

        /// <summary>
        /// Desc:8#风箱压力抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_08 { get; set; }

        /// <summary>
        /// Desc:8#风箱压力转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_08 { get; set; }

        /// <summary>
        /// Desc:8#风箱压力料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_08 { get; set; }

        /// <summary>
        /// Desc:8#风箱压力其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_08 { get; set; }

        /// <summary>
        /// Desc:16#风箱压力    31 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_BELLOWS16_TP { get; set; }

        /// <summary>
        /// Desc:16#风箱压力亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_16 { get; set; }

        /// <summary>
        /// Desc:16#风箱压力抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_16 { get; set; }

        /// <summary>
        /// Desc:16#风箱压力转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_16 { get; set; }

        /// <summary>
        /// Desc:16#风箱压力料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_16 { get; set; }

        /// <summary>
        /// Desc:16#风箱压力其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_16 { get; set; }

        /// <summary>
        /// Desc:21#风箱压力      32 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_BELLOWS21_TP { get; set; }

        /// <summary>
        /// Desc:216#风箱压力亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_21 { get; set; }

        /// <summary>
        /// Desc:21#风箱压力抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_21 { get; set; }

        /// <summary>
        /// Desc:21#风箱压力转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_21 { get; set; }

        /// <summary>
        /// Desc:21#风箱压力料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_21 { get; set; }

        /// <summary>
        /// Desc:21#风箱压力其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_21 { get; set; }

        /// <summary>
        /// Desc:Tfe 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_TFE { get; set; }

        /// <summary>
        /// Desc:FeO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO { get; set; }

        /// <summary>
        /// Desc:抗压强度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS { get; set; }

        /// <summary>
        /// Desc:转鼓指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX { get; set; }

        /// <summary>
        /// Desc:焙烧综合评价分数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_COM_SCORE { get; set; }

        /// <summary>
        /// Desc:亚铁含量参数得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_SCORE { get; set; }

        /// <summary>
        /// Desc:抗压强度参数得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_SCORE { get; set; }

        /// <summary>
        /// Desc:转鼓强度参数得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_SCORE { get; set; }

        /// <summary>
        /// Desc:料面平整参数得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_SCORE { get; set; }

        /// <summary>
        /// Desc:其它参数得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_SCORE { get; set; }

        /// <summary>
        /// Desc:鼓干鼓风机压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FT_DD_TP { get; set; }

        /// <summary>
        /// Desc:鼓干鼓风机压力亚铁含量得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_FEO_TP { get; set; }

        /// <summary>
        /// Desc:鼓干鼓风机压力抗压强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_CS_TP { get; set; }

        /// <summary>
        /// Desc:鼓干鼓风机压力转鼓强度得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_DRIX_TP { get; set; }

        /// <summary>
        /// Desc:鼓干鼓风机压力料面平整得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_B_S_TP { get; set; }

        /// <summary>
        /// Desc:鼓干鼓风机压力其它得分 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double TC_OTHER_TP { get; set; }



    }
}
