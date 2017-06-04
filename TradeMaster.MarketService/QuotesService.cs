using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using TradeMaster.AI;
using TradeMaster.Common;
using TradeMaster.Entities;

namespace TradeMaster.MarketService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class QuotesService : IQuotesService
    {
        #region Private Members

        //private Timer _timer;
        private List<OHLC> _sampleData;
        Analyzer _analyzer;
        private static int _index;

        #endregion

        #region Properties

        public QuotesService()
        {
            _sampleData = LoadData();
            _analyzer = new Analyzer();
            //_timer = new Timer(new TimerCallback(CheckQuotes), null, 0, 1);         
            int count = 0;   
            while(count < _sampleData.Count)
            {
                CheckQuotes(null);
                count++;
            }
        }        

        #endregion

        public bool Echo()
        {
            return true;
        }

        #region Private Methods

        private List<OHLC> LoadData()
        {
            var csvParser = new CsvParser.CsvParser(',');
            IEnumerable<string[]> stockData = csvParser.ParseFile(@"D:\StockData\Maruti.csv", Encoding.Default);
            //IEnumerable<string[]> stockData = csvParser.ParseFile(@"D:\StockData\Infy.csv", Encoding.Default);
            //IEnumerable<string[]> stockData = csvParser.ParseFile(@"D:\StockData\Icicibank.csv", Encoding.Default);
            //IEnumerable<string[]> stockData = csvParser.ParseFile(@"D:\StockData\sbin.csv", Encoding.Default);
            //IEnumerable<string[]> stockData = csvParser.ParseFile(@"D:\StockData\MRF.csv", Encoding.Default);

            List<OHLC> quotes = new List<OHLC>();
            foreach (string[] record in stockData.Skip(1))
            {
                if (record.Length == 7 && record[1] != "null" && record[2] != "null" && record[3] != "null" && record[4] != "null")
                {
                    OHLC quote = new OHLC()
                    {
                        //Date = Convert.ToDateTime(record[0]),
                        Open = Convert.ToDouble(record[1]),
                        Close = Convert.ToDouble(record[4]),
                        High = Convert.ToDouble(record[2]),
                        Low = Convert.ToDouble(record[3]),
                        //Volume = Convert.ToInt32(record[5]),
                        //AdjClose = Convert.ToDouble(record[6]),
                    };
                    quotes.Add(quote);
                }
            }
            return quotes;
        }

        private void CheckQuotes(object state)
        {
            if(_index < _sampleData.Count)
            {
                Analyze(_sampleData[_index++]);
            }
            else
            {
                double pl = 0;
                foreach (var signal in _analyzer.Signals)
                {
                    if (signal.Type == TransactionType.Buy)
                    {
                        pl -= signal.Price;
                    }
                    else
                    {
                        pl += signal.Price;
                    }
                }                
            }
        }

        private void Analyze(OHLC quote)
        {
            _analyzer.Analyze(quote);
        }

        public IEnumerable<OHLC> GetQuotes()
        {
            return _sampleData;
        }

        public IEnumerable<TradeSignal> GetTradeSignals()
        {
            return _analyzer.Signals;
        }

        #endregion
    }
}
