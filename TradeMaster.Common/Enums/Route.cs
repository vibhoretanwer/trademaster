using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMaster.Common
{
    public enum Route
    {
        PARAMETERS,
        API_VALIDATE,
        API_INVALIDATE,
        USER_MARGINS,
        ORDERS,
        TRADES,
        ORDERS_INFO,
        ORDERS_PLACE,
        ORDERS_MODIFY,
        ORDERS_CANCEL,
        ORDERS_TRADES,
        PORTFOLIO_POSITIONS,
        PORTFOLIO_HOLDINGS,
        PORTFOLIO_POSITIONS_MODIFY,
        MARKET_INSTRUMENTS_ALL,
        MARKET_INSTRUMENTS,
        MARKET_QUOTE,
        MARKET_HISTORICAL,
        MARKET_TRIGGER_RANGE,
    }
}
