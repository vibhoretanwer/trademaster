﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TradeMaster.Entities
{
    [DataContract]
    [DebuggerDisplay("O={Open}, C={Close}, H={High}, L={Low}")]
    public class OHLC
    {
        #region Properties

        [DataMember]
        public double Open { get; set; }

        [DataMember]
        public double Close { get; set; }

        [DataMember]
        public double High { get; set; }

        [DataMember]
        public double Low { get; set; }

        #endregion
    }
}
