using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TradeMaster.AI;
using TradeMaster.Entities;
using TradeMaster.Infrastructure;

namespace TradeMaster.UI
{
    public class MainWindowViewModel : ViewModelBase
    {
        ObservableCollection<Quote> quotes = new ObservableCollection<Quote>();
        ObservableCollection<TradeSignal> signals = new ObservableCollection<TradeSignal>();

        private MarketManager.MarketManager _marketManager = new MarketManager.MarketManager();
        MarketConnectionManager mgr;

        Timer timer = new Timer(30000);

        Dictionary<string, string> stockList = new Dictionary<string, string>();
        Dictionary<string, List<Quote>> quoteList = new Dictionary<string, List<Quote>>();
        Dictionary<string, List<double>> fiveEMAs = new Dictionary<string, List<double>>();
        Dictionary<string, List<double>> twentyEMAs = new Dictionary<string, List<double>>();

        public MainWindowViewModel(string requestToken)
        {
            mgr = AuthenticationManager.Authenticate(AuthenticationManager.APIKey, AuthenticationManager.Apisecret);
            //mgr.StoreInstruments(@"D:\Instruments.txt", null);

            Quote quote = mgr.GetQuote("NFO", "NIFTY17OCT9800CE");

            //_marketManager.Start();

            stockList.Add("NIFTY17OCT9800CE", "NFO");
            stockList.Add("NIFTY17OCT9850CE", "NFO");
            //stockList.Add("NIFTY17OCT9900CE", "NFO");
            //stockList.Add("NIFTY17OCT9950CE", "NFO");
            stockList.Add("MARUTI", "NSE");
            stockList.Add("INFY", "NSE");
            //stockList.Add("HDFCBANK", "NSE");
            //stockList.Add("NIFTY17OCT1000CE");

            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
       {
            //timer.Stop();
            foreach (KeyValuePair<string,string> stock in stockList)
            {
                Quote quote1 = mgr.GetQuote(stock.Value, stock.Key);
                              

                if (fiveEMAs.ContainsKey(stock.Key) == false) fiveEMAs.Add(stock.Key, new List<double>());
                if (twentyEMAs.ContainsKey(stock.Key) == false) twentyEMAs.Add(stock.Key, new List<double>());

                AnalyzeQuote(quote1);

                App.Current.Dispatcher.Invoke(() =>
                {
                    Quote existingQuote = quotes.FirstOrDefault(q => q.Symbol.Equals(stock.Key));
                    UpdateQuoteUI(quote1, existingQuote);
                });
            }
            //Quote quote = mgr.GetQuote("NFO", @"NIFTY17OCT9800CE");
            //AnalyzeQuote(quote);
        }
        
        private void AnalyzeQuote(Quote quote)
        {
            AddOrUpdateQuote(quote);

            ApplyMovingAverageCrossover(quote);
        }

        private void ApplyMovingAverageCrossover(Quote quote)
        {
            if (quoteList[quote.Symbol].Count > 21)
            {
                double previousFiveEMA = quoteList[quote.Symbol].Take(quoteList.Count - 6).Average(quot => quot.LTP);
                double previousTwentyEMA = quoteList[quote.Symbol].Take(quoteList.Count - 21).Average(quot => quot.LTP);

                double fiveEMA = quoteList[quote.Symbol].Take(quoteList.Count - 5).Average(quot => quot.LTP);
                double twentyEMA = quoteList[quote.Symbol].Take(quoteList.Count - 20).Average(quot => quot.LTP);

                if (fiveEMA > twentyEMA && fiveEMA < previousTwentyEMA)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        TradeSignals.Add(new TradeSignal() { Symbol = quote.Symbol, Price = quote.LTP, Type = Common.TransactionType.Buy });
                    });
                }
                if (fiveEMA > twentyEMA && fiveEMA >= previousTwentyEMA)
                {
                }
                if (fiveEMA < twentyEMA && fiveEMA > previousTwentyEMA)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        TradeSignals.Add(new TradeSignal() { Symbol = quote.Symbol, Price = quote.LTP, Type = Common.TransactionType.Sell });
                    });
                }
                if (fiveEMA < twentyEMA && fiveEMA <= previousTwentyEMA)
                {
                }

                //fiveEMAs[quote.Symbol].Add(fiveEMA);
                //twentyEMAs[quote.Symbol].Add(twentyEMA);

                //quote.Last5EMA = fiveEMA;
                //quote.Last20EMA = twentyEMA;
            }
        }    

        //private void ApplyMovingAverageCrossover(Quote quote)
        //{
        //    if (quoteList[quote.Symbol].Count > 20)
        //    {
        //        double fiveEMA = Calculate5PointEMA(quote).Value;
        //        double twentyEMA = Calculate20PointEMA(quote).Value;
        //        double? previous5EMA = fiveEMAs[quote.Symbol].LastOrDefault();
        //        double? previous20EMA = twentyEMAs[quote.Symbol].LastOrDefault();
        //        if (previous20EMA.HasValue && fiveEMA > twentyEMA && fiveEMA < previous20EMA)
        //        {
        //            App.Current.Dispatcher.Invoke(() =>
        //            {
        //                TradeSignals.Add(new TradeSignal() { Symbol = quote.Symbol, Price = quote.LTP, Type = Common.TransactionType.Buy });
        //            });
        //        }
        //        if (previous20EMA.HasValue && fiveEMA > twentyEMA && fiveEMA >= previous20EMA)
        //        {
        //        }
        //        if (previous20EMA.HasValue && fiveEMA < twentyEMA && fiveEMA > previous20EMA.Value)
        //        {
        //            App.Current.Dispatcher.Invoke(() =>
        //            {
        //                TradeSignals.Add(new TradeSignal() { Symbol = quote.Symbol, Price = quote.LTP, Type = Common.TransactionType.Sell });
        //            });
        //        }
        //        if (previous20EMA.HasValue && fiveEMA < twentyEMA && fiveEMA <= previous20EMA.Value)
        //        {
        //        }

        //        fiveEMAs[quote.Symbol].Add(fiveEMA);
        //        twentyEMAs[quote.Symbol].Add(twentyEMA);

        //        quote.Last5EMA = fiveEMA;
        //        quote.Last20EMA = twentyEMA;
        //    }
        //}

        private void AddOrUpdateQuote(Quote quote)
        {
            if (quoteList.ContainsKey(quote.Symbol))
            {
                quoteList[quote.Symbol].Add(quote);
            }
            else
            {
                quoteList.Add(quote.Symbol, new List<Quote>() { quote });
            }
        }
        
        private double? Calculate5PointEMA(Quote quote)
        {
            double? sma = TechnicalIndicators.SMA(quoteList[quote.Symbol].Select(q => q.LTP).ToList(), quote.LTP, 5);
            double previousEMA;
            if (fiveEMAs[quote.Symbol].Count > 0) previousEMA = fiveEMAs[quote.Symbol].Last();
            else previousEMA = sma.Value;
            double? ema = TechnicalIndicators.EMA(quoteList[quote.Symbol].Select(q => q.LTP).ToList(), quote.LTP, 5, previousEMA);

            return ema;
        }

        private double? Calculate20PointEMA(Quote quote)
        {
            double? sma = TechnicalIndicators.SMA(quoteList[quote.Symbol].Select(q => q.LTP).ToList(), quote.LTP, 20);
            double previousEMA;
            if (twentyEMAs[quote.Symbol].Count > 0) previousEMA = twentyEMAs[quote.Symbol].Last();
            else previousEMA = sma.Value;
            double? ema = TechnicalIndicators.EMA(quoteList[quote.Symbol].Select(q => q.LTP).ToList(), quote.LTP, 20, previousEMA);

            return ema;
        }

        private void UpdateQuoteUI(Quote quote, Quote existingQuote)
        {
            if (existingQuote != null)
            {
                int index = quotes.IndexOf(existingQuote);
                if (index >= 0)
                {
                    quotes.Insert(index, quote);
                    quotes.Remove(existingQuote);
                }
                else
                {
                    quotes.Add(quote);
                }
            }
            else
            {
                quotes.Add(quote);
            }
        }

        public ObservableCollection<TradeSignal> TradeSignals
        {
            get
            {
                return signals;
            }
        }
        public ObservableCollection<Quote> Quotes
        {
            get
            {
                return quotes;
            }
        }
    }
}
