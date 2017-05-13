using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMaster.Common;

namespace TradeMaster.Entities
{
    public abstract class Order
    {
        #region Properties

        public int Id { get; set; }

        public string Symbol { get; set; }

        public Exchange Exchange { get; set; }

        public TransactionType TransactionType { get; set; }

        public abstract OrderType OrderType { get; }

        public int Quantity { get; set; }

        public ProductType ProductType { get; set; }

        public double LimitPrice { get; set; }

        public double TriggerPrice { get; set; }

        public OrderValidity Validity { get; set; }

        public string Tag { get; set; }

        #endregion
    }
}
