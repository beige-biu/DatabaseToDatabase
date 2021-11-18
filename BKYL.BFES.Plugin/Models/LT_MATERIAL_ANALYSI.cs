    using System;
    using System.Linq;
    using System.Text;

    namespace BKYL.BFES.Plugin.Models
    {
        public class LT_MATERIAL_ANALYSI
        {

            /// <summary>
            /// Desc:样品编码 
            /// Default:- 
            /// Nullable:False 
            /// </summary>
            public string SAMPLE_MUNB { get; set; }

            /// <summary>
            /// Desc:送样时间 
            /// Default:- 
            /// Nullable:False 
            /// </summary>
            public DateTime SENDSAMPLE_TIME { get; set; }

            /// <summary>
            /// Desc:委托单位 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string CLIENT { get; set; }

            /// <summary>
            /// Desc:样品名称 
            /// Default:- 
            /// Nullable:False 
            /// </summary>
            public string SAMPLE_NAME { get; set; }

            /// <summary>
            /// Desc:入库时间 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public DateTime UPDATE_TIME { get; set; }

            /// <summary>
            /// Desc:- 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string SBBM { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS1 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF1 { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS2 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF2 { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS3 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF3 { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS4 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF4 { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS5 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF5 { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS6 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF6 { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS7 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF7 { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS8 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF8 { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS9 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF9 { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS10 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF10 { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS11 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF11 { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS12 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF12 { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS13 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF13 { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS14 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF14 { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS15 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF15 { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS16 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF16 { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS17 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF17 { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS18 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF18 { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS19 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF19 { get; set; }

            /// <summary>
            /// Desc:化学元素 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public string HXYS20 { get; set; }

            /// <summary>
            /// Desc:化学成分 
            /// Default:- 
            /// Nullable:True 
            /// </summary>
            public Double HXCF20 { get; set; }

            /// <summary>
            /// Desc:序号 
            /// Default:- 
            /// Nullable:False 
            /// </summary>
            public int SERIAL_NUMB { get; set; }

        }
    }
