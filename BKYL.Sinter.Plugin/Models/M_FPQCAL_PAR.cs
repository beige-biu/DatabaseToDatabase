using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class M_FPQCAL_PAR
    {

        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:合格要求碱度的最大波动范围 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_Q_B_RANGE { get; set; }

        /// <summary>
        /// Desc:FeO中线 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_FE_CENTER { get; set; }

        /// <summary>
        /// Desc:合格要求FeO的最大波动范围 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_Q_FE_RANGE { get; set; }

        /// <summary>
        /// Desc:合格要求转鼓的最低值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_Q_D_MIN { get; set; }

        /// <summary>
        /// Desc:TFe中线 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_TFE_CENTER { get; set; }

        /// <summary>
        /// Desc:合格要求TFe的最大波动范围 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_Q_TFE_RANGE { get; set; }

        /// <summary>
        /// Desc:一级品要求碱度最大波动范围 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_FQ_B_RANGE { get; set; }

        /// <summary>
        /// Desc:一级品要求FeO最大波动范围 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_FQ_FE_RANGE { get; set; }

        /// <summary>
        /// Desc:一级品要求转鼓最低值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_FQ_D_MIN { get; set; }

        /// <summary>
        /// Desc:一级品要求TFe最大波动范围 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_FQ_TFE_RANGE { get; set; }

        /// <summary>
        /// Desc:品味稳定要求TFe波动中线 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_TATFE_CENTER { get; set; }

        /// <summary>
        /// Desc:品味稳定要求TFe最大波动范围 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_TATFE_RANGE { get; set; }

        /// <summary>
        /// Desc:碱度稳定要求碱度最大波动范围 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_R_S_RANGE { get; set; }

        /// <summary>
        /// Desc:FeO稳定要求FeO最大波动范围 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_FEO_S_RANGE { get; set; }

        /// <summary>
        /// Desc:MgO稳定要求MgO波动中线 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_MGO_CENTER { get; set; }

        /// <summary>
        /// Desc:MgO稳定要求MgO最大波动范围 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_MGO_RANGE { get; set; }

        /// <summary>
        /// Desc:低温还原粉化合格要求其最低值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_Q_RDI_MIN { get; set; }

        /// <summary>
        /// Desc:还原度合格要求还原度最低值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_Q_RI_MIN { get; set; }

        /// <summary>
        /// Desc:R中线 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_DS_RANGE { get; set; }

        /// <summary>
        /// Desc:＜5mm粒度范围的平均粒径 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_LESS_5_AVG { get; set; }

        /// <summary>
        /// Desc:5-10mm粒度范围的平均粒径 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_5_10_AVG { get; set; }

        /// <summary>
        /// Desc:10-16mm粒度范围的平均粒径 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_10_16_AVG { get; set; }

        /// <summary>
        /// Desc:16-25mm粒度范围的平均粒径 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_16_25_AVG { get; set; }

        /// <summary>
        /// Desc:25-40mm粒度范围的平均粒径 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_25_40_AVG { get; set; }

        /// <summary>
        /// Desc:＞40粒度范围的平均粒径 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_BIGGER_40_AVG { get; set; }

        /// <summary>
        /// Desc:Al2O3中线 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_AL2O3_CENTER { get; set; }

        /// <summary>
        /// Desc:CaO中线 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_CAO_CENTER { get; set; }

        /// <summary>
        /// Desc:CaO波动范围 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_CAO_RANGE { get; set; }
    }
}
