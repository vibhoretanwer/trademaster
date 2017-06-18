using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMaster.Common;
using TradeMaster.Entities;

namespace TradeMaster.AI
{
    public class Analyzer
    {
        private bool _needTrendConfirmation;

        public Analyzer()
        {
            Data = new List<OHLC>();

            EMAs = new Dictionary<int, List<double>>();

            //EMAs.Add(3, new List<double>());
            EMAs.Add(5, new List<double>());
            //EMAs.Add(8, new List<double>());
            //EMAs.Add(12, new List<double>());
            EMAs.Add(20, new List<double>());

            Signals = new List<TradeSignal>();
        }

        public List<OHLC> Data { get; set; }

        public Dictionary<int, List<double>> EMAs { get; set; }

        public List<TradeSignal> Signals { get; set; }

        public void Analyze(OHLC currentQuote)
        {            
            if(Data.Count < 20)
            {
                Data.Add(currentQuote);
                return;
            }
            OHLC lastQuote = null;
            if (Data.Count > 0)
            {
                lastQuote = Data.Last();
            }
            Data.Add(currentQuote);

            CalculateNewEMA(currentQuote, 5);
            CalculateNewEMA(currentQuote, 20);
            
            if (_needTrendConfirmation)
            {
                _needTrendConfirmation = false;
                if (Signals.Last().Type == TransactionType.Buy)
                {
                    if (currentQuote.Close < lastQuote.Close)
                    {
                        Signals.Remove(Signals.Last());
                    }
                }
                else if (Signals.Last().Type == TransactionType.Sell)
                {
                    if (currentQuote.Close > lastQuote.Close)
                    {
                        Signals.Remove(Signals.Last());
                    }
                }
            }
            if (EMAs[5].LastOrDefault() > EMAs[20].LastOrDefault())
            {
                if (Signals.Count > 0)
                {
                    if (Signals.Last().Type == TransactionType.Sell)
                    {
                        Signals.Add(new TradeSignal() { Price = currentQuote.Close, Type = TransactionType.Buy });
                        _needTrendConfirmation = true;
                    }
                }
                else
                {
                    Signals.Add(new TradeSignal() { Price = currentQuote.Close, Type = TransactionType.Buy });
                    _needTrendConfirmation = true;
                }
            }

            if (EMAs[5].LastOrDefault() < EMAs[20].LastOrDefault())
            {
                if (Signals.Count > 0)
                {
                    if (Signals.Last().Type == TransactionType.Buy)
                    {
                        Signals.Add(new TradeSignal() { Price = currentQuote.Close, Type = TransactionType.Sell });
                        _needTrendConfirmation = true;
                    }
                }
                else
                {
                    Signals.Add(new TradeSignal() { Price = currentQuote.Close, Type = TransactionType.Sell });
                    _needTrendConfirmation = true;
                }
            }
        }

        private void CalculateNewEMA(OHLC currentQuote, int duration)
        {
            if (EMAs[duration].Count == 0)
            {
                EMAs[duration].Add(TechnicalIndicators.CalculateMovingAverage(Data.Select(x => x.Close), duration, MovingAverage.Exponential).FirstOrDefault());
            }
            else
            {
                double previousEMA = EMAs[duration].LastOrDefault();
                EMAs[duration].Add(TechnicalIndicators.CalculateExponentialMovingAverage(previousEMA, currentQuote.Close, duration));
            }
        }
    }
}
