using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepParameters {
    public static class Statistics {
        /// <summary>
        /// Get reliabity interval for vibration signal
        /// </summary>
        /// <param name="values">List of values of vibration signal</param>
        /// <param name="numberOfValuesForNormalLevel">Number of values that will be used to calc the maximum level of normal work</param>
        /// <param name="numberOfStds">Number of standard deviations that will be used to calc the maximum level of normal work</param>
        /// <returns>Reliability Interval that contains 100 values (each value is 1 percent of relilability)</returns>
        public static List<double> GetReliabilityInterval(List<double> values, int numberOfValuesForNormalLevel, double numberOfStds) {
            // List that contains reliability sections
            List<double> reliabilityInterval = new List<double>();

            // List of values for calculating max values for normal work
            List<double> valuesForNormalLevel = values.GetRange(0, numberOfValuesForNormalLevel);

            double maxValue = values.Max();

            // Calc value of max level for normal work
            double maxNormalLevel = GetMaxNormalVibrSignal(valuesForNormalLevel, numberOfStds);

            // If max value of signal for normal work is more than max signal return
            // list of values that equals to max value
            if (maxNormalLevel >= maxValue) {
                return Enumerable.Repeat(maxValue, 100).ToList();
            }

            double oneDivision = (maxValue - maxNormalLevel) / 99;

            // Add values to reliability interval
            reliabilityInterval.Add(maxNormalLevel);
            for (int i = 1; i < 99; i++) {
                reliabilityInterval.Add(maxNormalLevel + i * oneDivision);
            }
            reliabilityInterval.Add(maxValue);

            return reliabilityInterval;
        }

        /// <summary>
        /// Calc value of max level for normal work
        /// </summary>
        /// <param name="values">Values of vibration signal</param>
        /// <param name="stdCount">Number of stds for calc max level for normal work</param>
        /// <returns>Value of max normal level for normal work</returns>
        private static double GetMaxNormalVibrSignal(List<double> values, double stdCount) {
            return values.Average() + stdCount * StandardDeviation(values);
        }

        /// <summary>
        /// Get standard deviation
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Value of standard deviation</returns>
        public static double StandardDeviation(IEnumerable<double> values) {
            double avg = values.Average();
            return Math.Sqrt(values.Average(v => Math.Pow(v - avg, 2)));
        }
    }
}
