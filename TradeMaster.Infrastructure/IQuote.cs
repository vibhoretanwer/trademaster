using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMaster.Entities;

namespace TradeMaster.Infrastructure
{
    interface IQuote
    {
        Quote GetQuote(string exchange, string symbol);
    }
}
