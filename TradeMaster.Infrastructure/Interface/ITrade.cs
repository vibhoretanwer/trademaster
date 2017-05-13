using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMaster.Entities;

namespace TradeMaster.Infrastructure
{
    public interface ITrade
    {
        #region Properties

        /// <summary>
        /// Place Order.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        string PlaceOrder(Order order);

        /// <summary>
        /// Modify Order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        string ModifyOrder(Order order);

        /// <summary>
        /// Cancel Order.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        string CancelOrder(Order order);

        #endregion
    }
}
