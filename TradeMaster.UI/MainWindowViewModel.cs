using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMaster.Entities;
using TradeMaster.Infrastructure;

namespace TradeMaster.UI
{
    public class MainWindowViewModel : ViewModelBase
    {
        private MarketManager.MarketManager _marketManager;
        public MainWindowViewModel(string requestToken)
        {            
            MarketConnectionManager mgr = AuthenticationManager.Authenticate(AuthenticationManager.APIKey, AuthenticationManager.Apisecret);
            //mgr.StoreInstruments(@"D:\Instruments.txt", null);

            Quote quote = mgr.GetQuote("NSE", "INFY");
        }        
    }
}
