using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.Jobs.Base.Models
{
    public class ConfigClearLog
    {
        private string lOGLOCATION = "";
        private int sAVETIME = 9999;

        public string LOGLOCATION
        {
            get
            {
                return lOGLOCATION;
            }

            set
            {
                lOGLOCATION = value;
            }
        }

        public int SAVETIME
        {
            get
            {
                return sAVETIME;
            }

            set
            {
                sAVETIME = value;
            }
        }
    }
}
