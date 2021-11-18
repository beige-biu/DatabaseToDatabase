using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class SINTER_MIXTURE_DATA
    {
        public SINTER_MIXTURE_DATA(DateTime time,int ID,string name,double val,string desc = "")
        {
            this.TIMESTAMP = time;
            this.S_ID = ID;
            this.S_NAME = name;
            this.S_VALUE = val;
            this.S_DESC = desc;
        }
        public SINTER_MIXTURE_DATA()
        {
           
        }
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int S_ID {get;set;}

        /// <summary>
        /// Desc:名称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string S_NAME {get;set;}

        /// <summary>
        /// Desc:值 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public double S_VALUE {get;set;}

        /// <summary>
        /// Desc:描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string S_DESC {get;set;}

    }
}
