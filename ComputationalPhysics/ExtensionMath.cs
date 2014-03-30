using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalPhysics {
    public static class ExtensionMath {
        public static double Sqrd(this double v1) {
            return v1 * v1;
        }

        public static double Pow(this double b, int p) {
            return Math.Pow(b, p);
        }

        public static double DomainStart(this Func<double, double> function, double min, double max, out int counter, double eps = .001) {
            double lowerBound = min, upperBound = max;
            counter = 0;
            double range = double.MaxValue;
            double tryIndex = double.MinValue, tryEval = double.MinValue;
            while (range > eps) {
                counter++;
                range = upperBound - lowerBound;
                double maxEval = function(upperBound);
                double minEval = function(lowerBound);
                if (!double.IsNaN(minEval) || double.IsNaN(maxEval)) {
                    return double.NaN;
                }
                tryIndex = lowerBound + range / 2;
                tryEval = function(tryIndex);
                if (!double.IsNaN(tryEval)) {
                    upperBound = tryIndex;
                } else lowerBound = tryIndex;
            }
            return tryIndex;
        }

        public static double Zero(this Func<double, double> function, double min, double max, out int counter, double eps = .001) {
            double lowerBound = min,
                upperBound = max;
            counter = 0;
            double range = double.MaxValue;
            double tryIndex = double.MinValue, tryEval = double.MinValue;
            while (Math.Abs(tryEval) > eps && range > eps) {
                counter++;
                range = upperBound - lowerBound;
                double maxEval = function(upperBound);
                double minEval = function(lowerBound);
                if (Math.Abs(minEval) < eps) {
                    return minEval;
                }
                if (double.IsNaN(minEval) || double.IsNaN(maxEval)) {
                    return double.NaN;
                }
                bool signMax = (maxEval > 0);
                bool signMin = (minEval > 0);
                if (signMax == signMin) {
                    return double.NaN;
                }
                tryIndex = lowerBound + range / 2;
                tryEval = function(tryIndex);
                if (Math.Abs(tryEval) < eps) {
                    return tryIndex;
                }
                bool trySign = tryEval > 0;
                if (trySign == signMax) {
                    upperBound = tryIndex;
                } else lowerBound = tryIndex;
            }
            return tryIndex;
        }
    }
}
