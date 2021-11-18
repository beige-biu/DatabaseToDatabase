using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models
{
    public class TAPPING_STATIS
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:出铁时长 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? E_TAPTIMESPAN {get;set;}

        /// <summary>
        /// Desc:见渣时长 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? E_SLAGTIMESPAN {get;set;}

        /// <summary>
        /// Desc:打泥量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? E_SLADEWEIGHT {get;set;}

        /// <summary>
        /// Desc:断铁口个数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? E_IMBROKENCOUNT {get;set;}

        /// <summary>
        /// Desc:出铁总个数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? E_TAPCOUNT {get;set;}

        /// <summary>
        /// Desc:铁水温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? E_HMTE {get;set;}

        /// <summary>
        /// Desc:铁口深度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? E_HOLELENGTH {get;set;}

        /// <summary>
        /// Desc:漏铁口率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? E_IMBROKENRATE {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? W_TAPTIMESPAN {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? W_SLAGTIMESPAN {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? W_SLADEWEIGHT {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? W_IMBROKENCOUNT {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? W_TAPCOUNT {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? W_HMTE {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? W_HOLELENGTH {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? W_IMBROKENRATE {get;set;}

    }
}
