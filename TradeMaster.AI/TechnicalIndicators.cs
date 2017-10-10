using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMaster.Common;
using TradeMaster.Entities;

namespace TradeMaster.AI
{
    public static class TechnicalIndicators
    {
        public static double? SMA(List<double> values, double newValue, int period)
        {
            double? sma = null;

            values.Add(newValue);
            if (values.Count >= period)
            {
                sma = values.Skip(values.Count - period).Average();
            }

            return sma;
        }

        public static double? EMA(List<double> values, double newValue, int period, double? previousEMA)
        {
            double? ema = null;

            values.Add(newValue);
            if (values.Count >= period)
            {
                double multiplier = 2.0 / (period + 1);
                if (previousEMA.HasValue == false)
                {
                    previousEMA = SMA(values, newValue, period);
                }
                if (previousEMA.HasValue == true)
                {
                    ema = (newValue - previousEMA.Value) * multiplier + previousEMA.Value;
                }
                previousEMA = ema;
            }

            return ema;
        }

            //public static List<double> CalculateMovingAverage(IEnumerable<double> quotes, int duration, MovingAverage movingAverage = MovingAverage.Simple)
            //{
            //    List<double> ma = new List<double>();

            //    switch (movingAverage)
            //    {
            //        case MovingAverage.Simple:
            //            ma = CalculateSimpleMovingAverage(quotes, duration);
            //            break;
            //        case MovingAverage.Exponential:
            //            ma = CalculateExponentialMovingAverage(quotes.ToList(), duration);
            //            break;
            //    }

            //    return ma;
            //}

            //private static List<double> CalculateSimpleMovingAverage(IEnumerable<double> quotes, int duration)
            //{
            //    List<double> sma = new List<double>();

            //    for (int i = 0; i <= quotes.Count() - duration; ++i)
            //    {
            //        sma.Add(quotes.Skip(i).Take(duration).Average());
            //    }

            //    return sma;
            //}

            //private static List<double> CalculateExponentialMovingAverage(List<double> quotes, int duration)
            //{
            //    List<double> ema = new List<double>();
            //    double? previousDayEMA = null;
            //    double multiplier = 2.0 / (duration + 1);

            //    for (int i = 0; i <= quotes.Count() - duration; ++i)
            //    {
            //        if (previousDayEMA == null)
            //        {
            //            previousDayEMA = CalculateSimpleMovingAverage(quotes, duration).FirstOrDefault();
            //        }
            //        else
            //        {
            //            previousDayEMA = (quotes[duration + i - 1] - previousDayEMA.Value) * multiplier + previousDayEMA.Value;
            //        }
            //        ema.Add(previousDayEMA.Value);
            //    }

            //    return ema;
            //}

            //public static double CalculateExponentialMovingAverage(double previousEMA, double quote, int duration)
            //{
            //    double multiplier = 2.0 / (duration + 1);
            //    double previousDayEMA = previousEMA;

            //    double ema = (quote - previousDayEMA) * multiplier + previousDayEMA;

            //    return ema;
            //}

            //public static List<double> EMA(List<double> input, int range)
            //{
            //    int out1, count;
            //    double[] emaValues = new double[input.Count];
            //    TicTacTec.TA.Library.Core.Ema(0, input.Count - 1, input.ToArray(), range, out out1, out count, emaValues);

            //    return new List<double>(emaValues);            
            //}

            //public static double EMA(double input, double previousDayEMA, int range)
            //{
            //    double multiplier = 2.0 / (range + 1);
            //    double ema = (input - previousDayEMA) * multiplier + previousDayEMA;
            //    return ema;
            //}

            //public static List<double> SMA(List<double> input, int range)
            //{
            //    int out1, count;
            //    double[] smaValues = new double[input.Count];
            //    TicTacTec.TA.Library.Core.Sma(0, input.Count - 1, input.ToArray(), range, out out1, out count, smaValues);

            //    return new List<double>(smaValues);
            //}

            //public static Trend GetTrend(List<double> input)
            //{
            //    Trend trend = Trend.Unknown;
            //    int upTrend = 0;
            //    int downTrend = 0;

            //    for (int i = 0; i < input.Count - 1; ++i)
            //    {
            //        if (input[i + 1] > input[i])
            //        {
            //            upTrend ++;
            //        }
            //        if (input[i + 1] < input[i])
            //        {
            //            downTrend ++;
            //        }
            //    }

            //    if (upTrend > downTrend) trend = Trend.Up;
            //    if (upTrend < downTrend) trend = Trend.Down;
            //    if (upTrend == downTrend) trend = Trend.Sideways;

            //    return trend;
            //}
        }
}
