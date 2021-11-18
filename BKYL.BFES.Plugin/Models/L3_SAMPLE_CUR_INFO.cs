using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models
{
    public class L3_SAMPLE_CUR_INFO
    {

        /// <summary>
        /// Desc:分析时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime ANALYSE_TIME { get; set; }
        /// <summary>
        /// Desc:试样ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string SAMPLE_ID {get;set;}
        public string SAMPLE { get { return SAMPLE_ID.Substring(0, 5); } } //矿石取前5位作为标识编码
        public string SAMPLEIRON { get { return SAMPLE_ID.Substring(0, 4); } }//渣铁取前4位作为标识编码

        /// <summary>
        /// Desc:试样代码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SAMPLE_CODE {get;set;}

        /// <summary>
        /// Desc:试样类型代码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SAMPLE_TYPE_CODE {get;set;}

        /// <summary>
        /// Desc:试样类型名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SAMPLE_TYPE_NAME {get;set;}

        /// <summary>
        /// Desc:料种代码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TYPE_PART_CODE {get;set;}

        /// <summary>
        /// Desc:料种名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TYPE_PART_NAME {get;set;}

        /// <summary>
        /// Desc:试样位置编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SAMPLEING_POS_CODE {get;set;}

        /// <summary>
        /// Desc:试样位置名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SAMPLEING_POS_NAME {get;set;}

        /// <summary>
        /// Desc:试样状态编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SAMPLEING_STATUS_CODE {get;set;}

        /// <summary>
        /// Desc:试样状态名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SAMPLEING_STATUS_NAME {get;set;}

        /// <summary>
        /// Desc:分类料种 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TYPE_DISTINCT {get;set;}

        /// <summary>
        /// Desc:接收时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? REC_DT {get;set;}

        /// <summary>
        /// Desc:高炉ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BF_ID {get;set;}

        /// <summary>
        /// Desc:铁次号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TAP_NO {get;set;}

        /// <summary>
        /// Desc:铁次小时时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TAP_TIME_NO {get;set;}

        /// <summary>
        /// Desc:铁次月时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string IRON_MOUTH_NO {get;set;}

        /// <summary>
        /// Desc:班组名字 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TEAM_NAME {get;set;}

        /// <summary>
        /// Desc:班次名字 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SHIFT_NAME {get;set;}

        /// <summary>
        /// Desc:元素1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_1 {get;set;}

        /// <summary>
        /// Desc:元素2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_2 {get;set;}

        /// <summary>
        /// Desc:元素3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_3 {get;set;}

        /// <summary>
        /// Desc:元素4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_4 {get;set;}

        /// <summary>
        /// Desc:元素5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_5 {get;set;}

        /// <summary>
        /// Desc:元素6 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_6 {get;set;}

        /// <summary>
        /// Desc:元素7 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_7 {get;set;}

        /// <summary>
        /// Desc:元素8 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_8 {get;set;}

        /// <summary>
        /// Desc:元素9 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_9 {get;set;}

        /// <summary>
        /// Desc:元素10 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_10 {get;set;}

        /// <summary>
        /// Desc:元素11 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_11 {get;set;}

        /// <summary>
        /// Desc:元素12 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_12 {get;set;}

        /// <summary>
        /// Desc:元素13 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_13 {get;set;}

        /// <summary>
        /// Desc:元素14 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_14 {get;set;}

        /// <summary>
        /// Desc:元素15 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_15 {get;set;}

        /// <summary>
        /// Desc:元素16 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_16 {get;set;}

        /// <summary>
        /// Desc:元素17 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_17 {get;set;}

        /// <summary>
        /// Desc:元素18 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_18 {get;set;}

        /// <summary>
        /// Desc:元素19 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_19 {get;set;}

        /// <summary>
        /// Desc:元素20 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_20 {get;set;}

        /// <summary>
        /// Desc:元素21 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_21 {get;set;}

        /// <summary>
        /// Desc:元素22 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_22 {get;set;}

        /// <summary>
        /// Desc:元素23 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_23 {get;set;}

        /// <summary>
        /// Desc:元素24 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_24 {get;set;}

        /// <summary>
        /// Desc:元素25 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_25 {get;set;}

        /// <summary>
        /// Desc:元素26 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_26 {get;set;}

        /// <summary>
        /// Desc:元素27 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_27 {get;set;}

        /// <summary>
        /// Desc:元素28 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_28 {get;set;}

        /// <summary>
        /// Desc:元素29 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_29 {get;set;}

        /// <summary>
        /// Desc:元素30 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_30 {get;set;}

        /// <summary>
        /// Desc:元素31 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_31 {get;set;}

        /// <summary>
        /// Desc:元素32 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_32 {get;set;}

        /// <summary>
        /// Desc:元素33 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_33 {get;set;}

        /// <summary>
        /// Desc:元素34 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_34 {get;set;}

        /// <summary>
        /// Desc:元素35 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_35 {get;set;}

        /// <summary>
        /// Desc:元素36 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_36 {get;set;}

        /// <summary>
        /// Desc:元素37 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_37 {get;set;}

        /// <summary>
        /// Desc:元素38 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_38 {get;set;}

        /// <summary>
        /// Desc:元素39 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_39 {get;set;}

        /// <summary>
        /// Desc:元素40 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_40 {get;set;}

        /// <summary>
        /// Desc:元素41 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_41 {get;set;}

        /// <summary>
        /// Desc:元素42 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_42 {get;set;}

        /// <summary>
        /// Desc:元素43 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_43 {get;set;}

        /// <summary>
        /// Desc:元素44 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_44 {get;set;}

        /// <summary>
        /// Desc:元素45 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_45 {get;set;}

        /// <summary>
        /// Desc:元素46 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_46 {get;set;}

        /// <summary>
        /// Desc:元素47 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_47 {get;set;}

        /// <summary>
        /// Desc:元素48 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_48 {get;set;}

        /// <summary>
        /// Desc:元素49 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_49 {get;set;}

        /// <summary>
        /// Desc:元素50 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? ELE_50 {get;set;}

        /// <summary>
        /// Desc:入库时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime SAMPLETIME {get;set;}

    }
}
