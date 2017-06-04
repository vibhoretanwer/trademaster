using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMaster.Common;

namespace TradeMaster.AI
{
    public static class TechnicalIndicators
    {
        public static List<double> EMA(List<double> input, int range)
        {
            int out1, count;
            double[] emaValues = new double[input.Count];
            TicTacTec.TA.Library.Core.Ema(0, input.Count - 1, input.ToArray(), range, out out1, out count, emaValues);

            return new List<double>(emaValues);            
        }

        public static double EMA(double input, double previousDayEMA, int range)
        {
            double multiplier = 2.0 / (range + 1);
            double ema = (input - previousDayEMA) * multiplier + previousDayEMA;
            return ema;
        }

        public static List<double> SMA(List<double> input, int range)
        {
            int out1, count;
            double[] smaValues = new double[input.Count];
            TicTacTec.TA.Library.Core.Sma(0, input.Count - 1, input.ToArray(), range, out out1, out count, smaValues);

            return new List<double>(smaValues);
        }

        public static Trend GetTrend(List<double> input)
        {
            Trend trend = Trend.Unknown;
            int upTrend = 0;
            int downTrend = 0;

            for (int i = 0; i < input.Count - 1; ++i)
            {
                if (input[i + 1] > input[i])
                {
                    upTrend ++;
                }
                if (input[i + 1] < input[i])
                {
                    downTrend ++;
                }
            }

            if (upTrend > downTrend) trend = Trend.Up;
            if (upTrend < downTrend) trend = Trend.Down;
            if (upTrend == downTrend) trend = Trend.Sideways;

            return trend;
        }
    }
}
