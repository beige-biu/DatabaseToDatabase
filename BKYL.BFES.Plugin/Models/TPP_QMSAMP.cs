using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class TPP_QMSAMP
    {
        /// <summary>
        /// 铁次号
        /// </summary>
        public string TAPNUMB { get; set; }
        /// <summary>
        /// 罐号
        /// </summary>
        public string LADLE1_NR { get; set; }
        
        /// <summary>
        /// Desc:样本主键 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string C_SMPID {get;set;}

        /// <summary>
        /// Desc:样品编号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_SMP_NO {get;set;}

        /// <summary>
        /// Desc:物料代码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_MTRL_NO {get;set;}

        /// <summary>
        /// Desc:物料名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_MTRL_NAME {get;set;}

        /// <summary>
        /// Desc:MR料号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_MRMTRL {get;set;}

        /// <summary>
        /// Desc:MR品名 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_MRMTRL_NAME {get;set;}

        /// <summary>
        /// Desc:炉号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_HEAT_NO {get;set;}

        /// <summary>
        /// Desc:检验站 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_INSPEMPL_NO {get;set;}

        /// <summary>
        /// Desc:委托单号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_TRUSTDEED_NO {get;set;}

        /// <summary>
        /// Desc:取样地点 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_SMP_SITE_NO {get;set;}

        /// <summary>
        /// Desc:取样时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_GET_SMP_TIME {get;set;}

        /// <summary>
        /// Desc:送样时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_SMP_SEND_TIME {get;set;}

        /// <summary>
        /// Desc:制样时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_SMP_MK_TIME {get;set;}

        /// <summary>
        /// Desc:委托部门 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_USE_DEPT_NO {get;set;}

        /// <summary>
        /// Desc:试样类型 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_SMP_TYPE {get;set;}

        /// <summary>
        /// Desc:供应商代码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_PROVID_NO {get;set;}

        /// <summary>
        /// Desc:产地 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_PRD_AREA {get;set;}

        /// <summary>
        /// Desc:等级描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_JUDG_DESC {get;set;}

        /// <summary>
        /// Desc:判定结果 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_JUDG_RST {get;set;}

        /// <summary>
        /// Desc:检验批号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_CHECKNO {get;set;}

        /// <summary>
        /// Desc:凭证类型 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_ORDER_TYPE {get;set;}

        /// <summary>
        /// Desc:凭证单号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_ORDER_NUMBER {get;set;}

        /// <summary>
        /// Desc:备注 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_NOTE {get;set;}

        /// <summary>
        /// Desc:时间戳 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? C_TIMESTAMP {get;set;}

    }
}
