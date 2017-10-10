using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMaster.Entities;

namespace TradeMaster.MarketManager
{
    public class MarketManager
    {
        #region Private Members

        QuotesServiceReference.QuotesServiceClient _quotesService = new QuotesServiceReference.QuotesServiceClient();

        #endregion

        #region Constructor

        public MarketManager()
        {

        }

        #endregion

        #region Methods

        public void Start()
        {
            _quotesService.Echo();   
        }

        public IEnumerable<OHLC> GetQuotes()
        {
            return _quotesService.GetQuotes();
        }

        public IEnumerable<TradeSignal> GetTradeSignals()
        {
            //return _quotesService.GetTradeSignals();
            return new List<TradeSignal>();
        }

        #endregion
    }
}
