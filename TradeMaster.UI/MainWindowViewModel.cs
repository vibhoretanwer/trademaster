using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMaster.Entities;

namespace TradeMaster.UI
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private MarketManager.MarketManager _marketManager;
        public MainWindowViewModel()
        {
            Quotes = new ObservableCollection<OHLC>();
            TradeSignals = new ObservableCollection<TradeSignal>();

            _marketManager = new MarketManager.MarketManager();
            _marketManager.Start();

            _marketManager.GetQuotes().ToList().ForEach(x => Quotes.Add(x));
            _marketManager.GetTradeSignals().ToList().ForEach(x => TradeSignals.Add(x));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<OHLC> Quotes { get; set; }

        public ObservableCollection<TradeSignal> TradeSignals { get; set; }
    }
}
