using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMaster.Common;

namespace TradeMaster.Entities
{
    public class BracketOrder : Order
    {
        #region Properties

        public double SquareOffValue { get; set; }

        public double StoplossValue { get; set; }

        public double TrailingStoplossValue { get; set; }

        public override OrderType OrderType
        {
            get
            {
                return OrderType.Bracket;
            }
        }

        #endregion
    }
}
