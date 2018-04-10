﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.GateOut.Console.DAL
{
    public class SecureGateLog
    {
        public long SecureGateLogID { get; set; }
        public DateTime Dtm1 { get; set; }
        public string Loc1 { get; set; }

        public string LogCat { get; set; }

        public string LogRemark { get; set; }

        public long RefID { get; set; }

        public SecureGateLog()
        {
            SecureGateLogID = 0;
            Dtm1 = DateTime.MinValue;
            Loc1 = string.Empty;
            LogCat = string.Empty;
            LogRemark = string.Empty;
            RefID = 0;
        }
    }
}
