using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMaster.Common;

namespace TradeMaster.Entities
{
    public class CoverOrder : Order
    {
        #region Properties

        public override OrderType OrderType
        {
            get
            {
                return OrderType.Cover;
            }
        }

        #endregion
    }
}
