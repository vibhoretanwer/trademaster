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

        public float LTP { get; set; }

        public float ChangePercentage { get; set; }

        public float Change { get; set; }

        public int Volume { get; set; }

        public int BuyQuantity { get; set; }

        public int SellQuantity { get; set; }

        public float OpenInterest { get; set; }

        public int LastTradedQuantity { get; set; }

        public OHLC OHLC { get; set; }

        #endregion
    }
}
