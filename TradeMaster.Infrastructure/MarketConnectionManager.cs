﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMaster.Common;
using TradeMaster.Entities;

namespace TradeMaster.Infrastructure
{
    /// <summary>
    /// A Class to manage connection to market.
    /// </summary>
    public class MarketConnectionManager : ITrade
    {
        #region Constants

        /// <summary>
        /// The default root entry
        /// </summary>
        private const string Root = "https://api.kite.trade";

        /// <summary>
        /// The login url
        /// </summary>
        private const string Login = "https://kite.trade/connect/login";

        #endregion

        #region Private Data Members

        private readonly string _apiKey;
        private readonly bool _microCache;
        private string _accessToken;
        private HttpOperations _httpOperations;

        #endregion

        #region Constructor

        public MarketConnectionManager(string apiKey, int timeout = 100, string accessToken = null, bool debug = false, bool microCache = true)
        {
            this._apiKey = apiKey;
            this._accessToken = accessToken;
            this._microCache = microCache;
            this._httpOperations = new HttpOperations(apiKey, timeout, accessToken, debug, microCache);
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        #region Public Methods

        #region Trading API

        public string PlaceOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public string ModifyOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public string CancelOrder(Order order)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Authentication API

        /// <summary>
        ///To set access token
        /// </summary>
        /// <param name="accessToken">the string of access token</param>
        public void SetAccessToken(string accessToken)
        {
            //Set the `access_token` received after a successful authentication."""
            this._accessToken = accessToken;
        }

        /// <summary>
        /// Get login url
        /// </summary>
        /// <returns>returns login url</returns>
        public string LoginUrl()
        {
            //Get the remote login url to which a user should be redirected
            //to initiate the login flow.
            return Login + "?api_key=" + this._apiKey;
        }

        /// <summary>
        ///     curl https://api.kite.trade/session/token
        ///     -d "api_key=xxx"
        ///     -d "request_token=yyy"
        ///     -d "checksum=zzz"
        ///     Authentication involves redirecting a user
        ///     to the public Kite login endpoint
        ///     https://kite.trade/connect/login?api_key=xxx.
        ///     A successful login comes
        ///     back with a request_token
        ///     as a URL query parameter to
        ///     the registered redirect url.
        /// Gets the accesstoken
        /// response contains not just the `access_token`, but metadata for
        /// the user who has authenticated.
        /// </summary>
        /// <param name="requestToken">The request token you got after login</param>
        /// <param name="secret">your API secret</param>        
        /// </returns>
        public dynamic RequestAccessToken(string requestToken, string secret)
        {
            //Do the token exchange with the `request_token` obtained after the login flow,
            //and retrieve the `access_token` required for all subsequent requests.The
            //response contains not just the `access_token`, but metadata for
            //the user who has authenticated.
            //- `request_token` is the token obtained from the GET paramers after a successful login redirect.
            //- `secret` is the API secret issued with the API key.
            // h = hashlib.sha256(self.api_key.encode("utf-8") + request_token.encode("utf-8") + secret.encode("utf-8"))
            string checksum = HttpOperations.Sha256(this._apiKey + requestToken + secret);
            var values = new Dictionary<string, string>
            {
                {"request_token", requestToken},
                {"checksum", checksum}
            };
            dynamic resp = _httpOperations.Post("api.validate", values);
            if ((resp != null) && resp.ContainsKey("access_token"))
            {
                SetAccessToken(resp["access_token"]);
            }
            return resp;
        }

        /// <summary>
        ///     curl --request DELETE \
        ///     "https://api.kite.trade/session/token?api_key=xxx&access_token=yyy"
        ///     This call invalidates the access_token
        ///     and destroys the API session. After this,
        ///     the user should be sent through a new login
        ///     flow before further interactions.
        /// </summary>
        /// <param name="accessToken">The access token receieved during login</param>
        public void InvalidateToken(string accessToken = null)
        {
            var values = new Dictionary<string, string>();

            if (accessToken != null)
            {
                values.Add("access_token", accessToken);
            }
            _httpOperations.Delete("api.invalidate", values);
        }

        /// <summary>
        /// stores instruments in  csv file
        /// </summary>
        /// <param name="path">The path where to store file e.g
        /// Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "InstrumentList.csv")
        /// </param>
        /// <param name="exchange">Type of exchange "BSE" or "NSE" </param>
        public void StoreInstruments(string path, string exchange = null)
        {
            using (TextWriter writer = File.CreateText(path))
            {
                string data;
                if (exchange != null)
                {
                    var param = new Dictionary<string, string> { ["exchange"] = exchange };
                    data = _httpOperations.Get("market.instruments", param);
                }
                else
                {
                    data = _httpOperations.Get("market.instruments.all");
                }
                writer.Write(data);
            }
        }        

        #endregion

        #endregion

        #region Private Methods



        #endregion

        #endregion
    }
}
