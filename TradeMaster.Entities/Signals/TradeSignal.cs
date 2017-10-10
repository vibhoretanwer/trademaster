using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TradeMaster.Common;

namespace TradeMaster.Entities
{
    [DataContract]
    [DebuggerDisplay("{Type} @ {Price}")]
    public class TradeSignal
    {
        #region Properties

        [DataMember]
        public string Symbol { get; set; }

        [DataMember]
        public double Price { get; set; }

        [DataMember]
        public TransactionType Type { get; set; }

        #endregion
    }
}
