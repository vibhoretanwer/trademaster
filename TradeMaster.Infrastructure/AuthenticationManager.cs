using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMaster.Infrastructure
{
    public static class AuthenticationManager
    {
        #region Private Members

        private static readonly string rootUrl = "";
        private static string apiKey = "icgbzpgqrkyhjfq2";
        private static string apiSecret = "xo9ow8edaa3wgtfqu18tf2n5npnovjt6";

        #endregion

        static AuthenticationManager()
        {
            AppConstants.ApiKey = apiKey;
            AppConstants.Apisecret = apiSecret;
        }

        #region Properties

        public static string LoginURL
        {
            get
            {
                return "https://kite.trade/connect/login?api_key=" + apiKey;
            }
        }

        public static string APIKey
        {
            get
            {
                return apiKey;
            }
        }

        public static string Apisecret
        {
            get
            {
                return apiSecret;
            }
        }

        public static string AccessToken { get; set; }

        #endregion

        #region Methods

        public static MarketConnectionManager Authenticate(string apiKey, string apisecret)
        {
            //AppConstants.ApiKey = apiKey;
            //AppConstants.Apisecret = apisecret;
            MarketConnectionManager connection = new MarketConnectionManager(apiKey);
            //string token = AccessToken;
            dynamic data = DataHelper.Saveaccesstoken(ref connection, AccessToken);
            AppConstants.AccessToken = data["access_token"];
            AppConstants.PublicToken = data["public_token"];
            AppConstants.UserId = data["user_id"];
            connection.SetAccessToken(AppConstants.AccessToken);
            return connection;
        }

        #endregion
    }
}
