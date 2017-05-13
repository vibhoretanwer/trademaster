using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TradeMaster.Common
{

    public class HttpOperations
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

        /// <summary>
        /// A dictionary of API Routes that can be accessed
        /// </summary>
        private readonly Dictionary<string, string> _routes = new Dictionary<string, string>
        {
            ["parameters"] = "/parameters",
            ["api.validate"] = "/session/token",
            ["api.invalidate"] = "/session/token",
            ["user.margins"] = "/user/margins/{segment}",
            ["orders"] = "/orders",
            ["trades"] = "/trades",
            ["orders.info"] = "/orders/{order_id}",
            ["orders.place"] = "/orders/{variety}",
            ["orders.modify"] = "/orders/{variety}/{order_id}",
            ["orders.cancel"] = "/orders/{variety}/{order_id}",
            ["orders.trades"] = "/orders/{order_id}/trades",
            ["portfolio.positions"] = "/portfolio/positions",
            ["portfolio.holdings"] = "/portfolio/holdings",
            ["portfolio.positions.modify"] = "/portfolio/positions",
            ["market.instruments.all"] = "/instruments",
            ["market.instruments"] = "/instruments/{exchange}",
            ["market.quote"] = "/instruments/{exchange}/{tradingsymbol}",
            ["market.historical"] = "/instruments/historical/{instrument_token}/{interval}",
            ["market.trigger_range"] = "/instruments/{exchange}/{tradingsymbol}/trigger_range"
        };

        #endregion

        #region Private Data Members

        private readonly string _apiKey;
        private readonly bool _microCache;
        private string _accessToken;

        #endregion

        #region Constructor

        public HttpOperations(string apiKey, int timeout = 100, string accessToken = null, bool debug = false, bool microCache = true)
        {
            this._apiKey = apiKey;
            this._accessToken = accessToken;
            this._microCache = microCache;
        }

        #endregion

        #region Methods

        public dynamic Get(string route, Dictionary<string, string> param = null)
        {
            //Alias for sending a GET request
            return Request(route, "GET", param);
        }

        public dynamic Post(string route, Dictionary<string, string> param = null)
        {
            //Alias for sending a POST request.
            return Request(route, "POST", param);
        }

        public dynamic Put(string route, Dictionary<string, string> param = null)
        {
            //Alias for sending a PUT request.
            return Request(route, "PUT", param);
        }

        public dynamic Delete(string route, Dictionary<string, string> param = null)
        {
            //Alias for sending a DELETE request.

            return Request(route, "DELETE", param);
        }

        private dynamic Request(string route, string method, Dictionary<string, string> param = null)
        {
            var client = new RestClient(Root);

            //Micro cache?
            if (this._microCache == false)
                if (param != null) param["no_micro_cache"] = "1";

            //Is there a token?.
            if ((this._accessToken != null) && (param != null))
                param["access_token"] = this._accessToken;

            if (param != null)
            {
                param["api_key"] = this._apiKey; //caution
            }
            string uri = this._routes[route];

            //setup of request and client
            //request.Timeout = timeout;
            client.FollowRedirects = true;

            if (uri.Contains("{")) //add url segments
            {
                if (param != null)
                    foreach (KeyValuePair<string, string> kvp in param)
                    {
                        if (uri.Contains(kvp.Key))
                        {
                            uri = uri.Replace("{" + kvp.Key + "}", kvp.Value);
                        }
                    }
            }

            var request = new RestRequest(uri);
            //if (param != null)
            //    foreach (KeyValuePair<string, string> kvp in param)
            //    {
            //        request.AddUrlSegment(kvp.Key, kvp.Value);
            //    }
            if (method == "POST")
            {
                request.Method = Method.POST;
            }
            else if (method == "GET")
            {
                request.Method = Method.GET;
            }
            else if (method == "PUT")
            {
                request.Method = Method.PUT;
            }
            else if (method == "DELETE")
            {
                request.Method = Method.DELETE;
            }
            if (param != null)
            {
                foreach (KeyValuePair<string, string> kvp in param)
                {
                    request.AddParameter(kvp.Key, kvp.Value);
                }
                request.AddJsonBody(param);
            }
            else
            {
                param = new Dictionary<string, string>
                {
                    ["access_token"] = this._accessToken,
                    ["api_key"] = this._apiKey
                };
                request.AddJsonBody(param);
            }

            IRestResponse response = client.Execute(request);
            string content = response.Content;

            if (response.ErrorException != null)
            {
                string message = response.ErrorMessage;
                var restExcept = new ApplicationException(message, response.ErrorException);
                throw restExcept;
            }

            if (response.ContentType.Contains("json"))
            {
                dynamic responseDict;
                try
                {
                    responseDict = GetDict(content);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("could not parse json", ex);
                }

                // api error
                if (responseDict["status"] == "error")
                {
                    if (response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        if (responseDict["error_type"] == "TwoFAException")
                            throw new ApplicationException("kite api error TwoFAException"
                                                           + response.StatusDescription);
                        throw new ApplicationException("kite api error " + responseDict["error_type"] +
                                                       "status" + response.StatusDescription);
                    }
                }
                return responseDict["data"];
            }
            if (response.ContentType.Contains("csv"))
            {
                return content;
            }
            throw new ApplicationException();
        }

        public static string Sha256(string password)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            if (password != null)
            {
                byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0,
                    Encoding.UTF8.GetByteCount(password));
                foreach (byte theByte in crypto)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }
            return hash.ToString();
        }

        private Dictionary<string, object> GetDict(string json)
        {
            dynamic obj = JsonConvert.DeserializeObject<IDictionary<string, object>>( json, new JsonConverter[] { new NestedJSONConverter() });
            return obj;
        }

        #endregion
    }
}
