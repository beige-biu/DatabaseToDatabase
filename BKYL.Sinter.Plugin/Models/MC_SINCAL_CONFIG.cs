using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin.Models
{
    public class MC_SINCAL_CONFIG
    {
        
        /// <summary>
        /// Desc:序号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int ID {get;set;}

        /// <summary>
        /// Desc:料仓号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? BIN_ID {get;set;}

        /// <summary>
        /// Desc:下料口号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BLANKING_ID {get;set;}

        /// <summary>
        /// Desc:料位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_W {get;set;}

        /// <summary>
        /// Desc:料种 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_COOD {get;set;}

        /// <summary>
        /// Desc:设备转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M_BELT_SPEED {get;set;}

    }
}
