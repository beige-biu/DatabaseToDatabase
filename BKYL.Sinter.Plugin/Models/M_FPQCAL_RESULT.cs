using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class M_FPQCAL_RESULT
    {
        
        /// <summary>
        /// Desc:插入时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:合格率（日平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_Q_RATE_DAY {get;set;}

        /// <summary>
        /// Desc:合格率（月平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_Q_RATE_MON {get;set;}

        /// <summary>
        /// Desc:一级品率（日平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_FQ_RATE_DAY {get;set;}

        /// <summary>
        /// Desc:一级品率（月平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_FQ_RATE_MON {get;set;}

        /// <summary>
        /// Desc:品味稳定率（日平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_TF_S_R_DAY {get;set;}

        /// <summary>
        /// Desc:品味稳定率（月平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_TF_S_R_MON {get;set;}

        /// <summary>
        /// Desc:碱度稳定率（日平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_BA_S_R_DAY {get;set;}

        /// <summary>
        /// Desc:碱度稳定率（月平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_BA_S_R_MON {get;set;}

        /// <summary>
        /// Desc:FeO稳定率（日平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_FEO_S_R_DAY {get;set;}

        /// <summary>
        /// Desc:FeO稳定率（月平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_FEO_S_R_MON {get;set;}

        /// <summary>
        /// Desc:MgO稳定率（月平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_MG_S_R_MON {get;set;}

        /// <summary>
        /// Desc:低温还原粉化稳定率（日平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_RDI_S_R_DAY {get;set;}

        /// <summary>
        /// Desc:低温还原粉化稳定率（月平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_RDI_S_R_MON {get;set;}

        /// <summary>
        /// Desc:还原度稳定率（日平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_RI_S_R_DAY {get;set;}

        /// <summary>
        /// Desc:还原度稳定率（月平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_RI_S_R_MON {get;set;}

        /// <summary>
        /// Desc:转鼓强度稳定率（日平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_DRUM_S_R_DAY {get;set;}

        /// <summary>
        /// Desc:转鼓强度稳定率（月平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_DRUM_S_R_MON {get;set;}

        /// <summary>
        /// Desc:成品率（日平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_FP_S_R_DAY {get;set;}

        /// <summary>
        /// Desc:成品率（月平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_FP_S_R_MON {get;set;}

        /// <summary>
        /// Desc:成品矿重量累积值（日累计） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_FP_ACCU_W_DAY {get;set;}

        /// <summary>
        /// Desc:下料量设定累积值（日累计） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_TOTAL_ACCU_W_DAY {get;set;}

        /// <summary>
        /// Desc:成品矿重量累积值（月累计） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_FP_ACCU_W_MON {get;set;}

        /// <summary>
        /// Desc:下料量设定累积值（月累计） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_TOTAL_ACCU_W_MON {get;set;}

        /// <summary>
        /// Desc:粒度<5mm（月平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_GRA_LES_5_MON {get;set;}

        /// <summary>
        /// Desc:粒度<5mm（日平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_GRA_LES_5_DAY {get;set;}

        /// <summary>
        /// Desc:粒度5-10mm（月平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_GRA_5_10_MON {get;set;}

        /// <summary>
        /// Desc:粒度5-10mm（日平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_GRA_5_10_DAY {get;set;}

        /// <summary>
        /// Desc:粒度10-16mm（月平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_GRA_10_16_MON {get;set;}

        /// <summary>
        /// Desc:粒度10-16mm（日平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_GRA_10_16_DAY {get;set;}

        /// <summary>
        /// Desc:粒度16-25mm（月平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_GRA_16_25_MON {get;set;}

        /// <summary>
        /// Desc:粒度16-25mm（日平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_GRA_16_25_DAY {get;set;}

        /// <summary>
        /// Desc:粒度25-40mm（月平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_GRA_25_40_MON {get;set;}

        /// <summary>
        /// Desc:粒度25-40mm（日平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_GRA_25_40_DAY {get;set;}

        /// <summary>
        /// Desc:粒度>40mm（月平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_GRA_B_40_MON {get;set;}

        /// <summary>
        /// Desc:粒度>40mm（日平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_GRA_B_40_DAY {get;set;}

        /// <summary>
        /// Desc:平均粒径（月平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_GRA_AVG_MON {get;set;}

        /// <summary>
        /// Desc:平均粒径（日平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_GRA_AVG_DAY {get;set;}

        /// <summary>
        /// Desc:一天检化验的数量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_EXAMI_DAY {get;set;}

        /// <summary>
        /// Desc:一个月检化验的数量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_EXAMI_MON {get;set;}

        /// <summary>
        /// Desc:满足合格条件数量（日） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_S_Q_RATE_DAY {get;set;}

        /// <summary>
        /// Desc:满足合格条件数量（月） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_S_Q_RATE_MON {get;set;}

        /// <summary>
        /// Desc:满足一级品条件数量（日） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_S_FQ_RATE_DAY {get;set;}

        /// <summary>
        /// Desc:满足一级品条件数量（月） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_S_FQ_RATE_MON {get;set;}

        /// <summary>
        /// Desc:满足品味稳定数量（日） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_S_TF_S_R_DAY {get;set;}

        /// <summary>
        /// Desc:满足品味稳定数量（月） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_S_TF_S_R_MON {get;set;}

        /// <summary>
        /// Desc:满足碱度稳定数量（日） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_S_BA_S_R_DAY {get;set;}

        /// <summary>
        /// Desc:满足碱度稳定数量（月） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_S_BA_S_R_MON {get;set;}

        /// <summary>
        /// Desc:满足FeO稳定数量（日） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_S_FEO_S_R_DAY {get;set;}

        /// <summary>
        /// Desc:满足FeO稳定数量（月） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_S_FEO_S_R_MON {get;set;}

        /// <summary>
        /// Desc:满足MgO稳定数量（日） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_S_MG_S_R_DAY {get;set;}

        /// <summary>
        /// Desc:满足MgO稳定数量（月） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_S_MG_S_R_MON {get;set;}

        /// <summary>
        /// Desc:满足低温还原粉化稳定数量（日） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_S_RDI_S_R_DAY {get;set;}

        /// <summary>
        /// Desc:满足低温还原粉化稳定数量（月） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_S_RDI_S_R_MON {get;set;}

        /// <summary>
        /// Desc:满足还原度稳定数量（日） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_S_RI_S_R_DAY {get;set;}

        /// <summary>
        /// Desc:满足还原度稳定数量（月） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_S_RI_S_R_MON {get;set;}

        /// <summary>
        /// Desc:满足转鼓强度稳定数量（日） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_S_DRUM_S_R_DAY {get;set;}

        /// <summary>
        /// Desc:满足转鼓强度稳定数量（月） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_S_DRUM_S_R_MON {get;set;}

        /// <summary>
        /// Desc:成品矿到下料口倒推时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_FP_MAT_TE {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_SPARE2 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_SPARE3 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_SPARE4 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_SPARE5 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_SPARE6 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_SPARE7 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_SPARE8 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_SPARE9 {get;set;}

        /// <summary>
        /// Desc:备用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_SPARE10 {get;set;}

        /// <summary>
        /// Desc:MgO稳定率（日平均） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? FPQ_MG_S_R_DAY {get;set;}

    }
}
