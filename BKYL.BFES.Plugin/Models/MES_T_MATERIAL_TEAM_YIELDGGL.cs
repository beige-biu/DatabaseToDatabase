using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.Models
{
    public class MES_T_MATERIAL_TEAM_YIELDGGL
    {
        public int RECORD_ID { get; set; }
        public DateTime RECORD_DATE { get; set; }
        public string WP_NO { get; set; }
        public string WP_NAME { get; set; }
        public DateTime TEAM_BEGIN_TIME { get; set; }
        public DateTime TEAM_END_TIME { get; set; }
        public float YIELD_SUM { get; set; }
        public string RECORD_NAME { get; set; }
        public string SHIFT { get; set; }
        public string TEAM { get; set; }
    }
}
