using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMaster.Entities
{
    public class Quote
    {
        #region Properties

        public string Symbol { get; set; }

        public double LTP { get; set; }

        public double ChangePercentage { get; set; }

        public double Change { get; set; }

        public long Volume { get; set; }

        public long BuyQuantity { get; set; }

        public long SellQuantity { get; set; }

        public double OpenInterest { get; set; }

        public long LastTradedQuantity { get; set; }

        public OHLC OHLC { get; set; }

        #endregion
    }
}
