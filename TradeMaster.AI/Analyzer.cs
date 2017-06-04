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

            EMAs.Add(3, new List<double>());
            EMAs.Add(5, new List<double>());
            EMAs.Add(8, new List<double>());
            EMAs.Add(12, new List<double>());

            Signals = new List<TradeSignal>();
        }

        public List<OHLC> Data { get; set; }

        public Dictionary<int, List<double>> EMAs { get; set; }

        public List<TradeSignal> Signals { get; set; }

        public void Analyze(OHLC currentQuote)
        {
            OHLC lastQuote = null;
            if(Data.Count > 0)
            {
                lastQuote = Data.Last();
            }
            Data.Add(currentQuote);

            //List<double> threePointEMAs = TechnicalIndicators.EMA(Data.Select(ohlc => ohlc.Close).ToList(), 3);
            //List<double> fivePointEMA = TechnicalIndicators.EMA(Data.Select(ohlc => ohlc.Close).ToList(), 5);
            //List<double> eightPointEMA = TechnicalIndicators.EMA(Data.Select(ohlc => ohlc.Close).ToList(), 8);
            //List<double> twelvePointEMAs = TechnicalIndicators.EMA(Data.Select(ohlc => ohlc.Close).ToList(), 12);            
            double previousEMA = 0;
            double fivePointEMA = 0;
            if (EMAs[5].Count == 0)
            {
                var emaList = TechnicalIndicators.EMA(Data.Select(ohlc => ohlc.Close).ToList(), 5);
                var sampleEMAs = emaList.Where(x => x != 0);
                if (sampleEMAs.Count() > 0)
                    fivePointEMA = sampleEMAs.Last();
                previousEMA = sampleEMAs.Count() > 1 ? sampleEMAs.Take(sampleEMAs.Count() - 1).Last() : fivePointEMA;
            }
            else
            {
                previousEMA = EMAs[5].Last();
                fivePointEMA = TechnicalIndicators.EMA(currentQuote.Close, previousEMA, 5);
            }
            
            var ema = fivePointEMA;
            if(_needTrendConfirmation)
            {
                _needTrendConfirmation = false;
                if(Signals.Last().Type == TransactionType.Buy)
                {
                    if(currentQuote.Close < lastQuote.Close)
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
            if (ema != 0 && previousEMA != 0)
            {
                if (currentQuote.Close > ema && Data.Count > 0 && lastQuote.Close < ema)
                {
                    if (ema > previousEMA)
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
                }
                EMAs[5].Add(ema);
                if (currentQuote.Close < ema && Data.Count > 0 && lastQuote.Close > ema)
                {
                    if (ema < previousEMA)
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
            }

            //if (fivePointEMA.Any(value => value != 0))
            //{
            //    var sampleEMAs = fivePointEMA.Where(value => value != 0);
            //    var ema = sampleEMAs.Last();
            //    var previousEMA = sampleEMAs.Count() > 1 ? sampleEMAs.Take(sampleEMAs.Count() - 1).Last() : ema;

            //    if (currentQuote.Close > ema && Data.Count > 0 && Data.Last().Close < ema)
            //    {
            //        if (ema > previousEMA)
            //        {
            //            Signals.Add(new TradeSignal() { Price = currentQuote.Close, Type = TransactionType.Buy });
            //        }
            //    }
            //    EMAs[5].Add(ema);
            //    if(currentQuote.Close < ema && Data.Count > 0 && Data.Last().Close > ema)
            //    {
            //        if (ema < previousEMA)
            //        {
            //            Signals.Add(new TradeSignal() { Price = currentQuote.Close, Type = TransactionType.Sell });
            //        }
            //    }
            //}
            //if (twelvePointEMAs.Any(value => value != 0))
            //{
            //    var ema = twelvePointEMAs.Last(value => value != 0);
            //    EMAs[12].Add(ema);
            //}            
        }
    }
}
