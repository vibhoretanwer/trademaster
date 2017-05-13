using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMaster.Infrastructure
{
    public static class DataHelper
    {
        /// <summary>
        /// enum which is used to access results from kitecon.Historical
        /// </summary>
        public enum OHLCType
        {
            Time = 0,
            Open = 1,
            High,
            Low,
            Close,
        }

        /// <summary>
        /// Saves access token and other meta data to a file on desktop
        /// </summary>
        /// <param name="connection">The initialized instance of kite connect</param>
        /// <param name="reqtoken">The request token receieved after login</param>
        /// <returns></returns>
        public static dynamic Saveaccesstoken(ref MarketConnectionManager connection, string reqtoken)
        {
            dynamic data = connection.RequestAccessToken(reqtoken, AppConstants.Apisecret);
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "data.txt");
            using (StreamWriter file = File.CreateText(path))
            {
                foreach (dynamic item in data)
                {
                    file.WriteLine("{0}={1}", item.Key, item.Value);
                }
            }
            return data;
        }

        /// <summary>
        /// stores the market instruments to desktop
        /// </summary>
        /// <param name="kitecon">The initialized instance of kite connect</param>
        public static void Storeinstruments(ref MarketConnectionManager kitecon)
        {
            kitecon.StoreInstruments(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "InstrumentList.csv"));
        }
    }
}
