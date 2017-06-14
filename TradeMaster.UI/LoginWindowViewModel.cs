using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMaster.Infrastructure;

namespace TradeMaster.UI
{
    public class LoginWindowViewModel : ViewModelBase
    {
        #region Properties

        public Uri AuthenticationUri
        {
            get
            {
                return new Uri(@"https://kite.trade/connect/login?api_key=" + AuthenticationManager.LoginURL);
            }
        }

        #endregion
    }
}
