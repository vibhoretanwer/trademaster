using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMaster.Infrastructure
{
    public static class AuthenticationManager
    {
        public static MarketConnectionManager Authenticate(string apiKey, string apisecret)
        {
            AppConstants.ApiKey = apiKey;
            AppConstants.Apisecret = apisecret;
            MarketConnectionManager connection = new MarketConnectionManager(apiKey);
            //LoginForm loginForm = new LoginForm();
            //loginForm.Url = "https://kite.trade/connect/login?api_key=" + apiKey;
            //loginForm.ShowDialog();
            //string token = loginForm.RequestToken;
            string token = "";
            dynamic data = DataHelper.Saveaccesstoken(ref connection, token);
            AppConstants.AccessToken = data["access_token"];
            AppConstants.PublicToken = data["public_token"];
            AppConstants.UserId = data["user_id"];
            //kitecon.SetAccessToken(AppConstants.AccessToken);
            return connection;
        }
    }
}
