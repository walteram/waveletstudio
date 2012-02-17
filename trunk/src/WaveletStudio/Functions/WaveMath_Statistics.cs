/*  Wavelet Studio Signal Processing Library - www.waveletstudio.net
    Copyright (C) 2011, 2012 Walter V. S. de Amorim - The Wavelet Studio Initiative

    Wavelet Studio is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Wavelet Studio is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

using System;

namespace WaveletStudio.Functions
{
    public static partial class WaveMath
    {
        /// <summary>
        /// Gets the accumulated energy of a signal
        /// </summary>
        /// <param name="samples"></param>
        /// <returns></returns>
        public static double GetAccumulatedEnergy(double[] samples)
        {
            var energy = 0d;
            foreach (var t in samples)
            {
                energy += GetEnergy(t);
            }
            return energy;
        }

        /// <summary>
        /// Gets the energy of a sample
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        public static double GetEnergy(double sample)
        {
            return Math.Pow(Math.Abs(sample), 2);
        }

        /// <summary>
        /// Calculates the mode of an array
        /// </summary>
        /// <param name="samples"></param>
        /// <returns></returns>
        public static double Mode(double[] samples)
        {
            if (samples.Length == 0)
            {
                return 0;
            }
            var sortedSamples = UniqueSorted(samples);
            var maxFreq = sortedSamples[0];
            var maxOccurrences = 0;
            foreach (var t in sortedSamples)
            {
                var occurrences = 0;
                foreach (var t1 in samples)
                {
                    if (Math.Abs(t1 - t) < Double.Epsilon)
                    {
                        occurrences++;
                    }
                }
                if (occurrences <= maxOccurrences)
                {
                    continue;
                }
                maxOccurrences = occurrences;
                maxFreq = t;
            }
            return (maxFreq);
        }

        /// <summary>
        /// Calculates the mean of an array
        /// </summary>
        /// <param name="samples"></param>
        /// <returns></returns>
        public static double Mean(double[] samples)
        {
            var sum = 0d;
            foreach (var value in samples)
            {
                sum += value;
            }
            return sum / (samples.Length);
        }

        /// <summary>
        /// Calculates the normal (or Gaussian) distribution of an array
        /// </summary>
        /// <param name="x"></param>
        /// <param name="mean"></param>
        /// <param name="deviation"></param>
        /// <returns></returns>
        public static double[] NormalDistribution(double[] x, double mean, double deviation)
        {
            var result = MemoryPool.Pool.New<double>(x.Length);
            for (var i = 0; i < x.Length; i++)
            {
                result[i] = ProbabilityDensityFunction(x[i], mean, deviation);
            }
            return result;
        }

        /// <summary>
        /// Calculates the Probability Density Function value of a sample
        /// </summary>
        /// <param name="x"></param>
        /// <param name="mean"></param>
        /// <param name="variance"></param>
        /// <returns></returns>
        public static double ProbabilityDensityFunction(double x, double mean, double variance)
        {
            return (1 / Math.Sqrt(2 * Math.PI * variance)) * Math.Exp(-1 * (Math.Pow(x - mean, 2) / (2 * variance)));
        }

        /// <summary>
        /// Calculates the normal density of a sample
        /// </summary>
        /// <param name="x"></param>
        /// <param name="mean"></param>
        /// <param name="deviation"></param>
        /// <returns></returns>
        public static double NormalDensity(double x, double mean, double deviation)
        {
            return Math.Exp(-(Math.Pow((x - mean) / deviation, 2) / 2)) / Math.Sqrt(2 * Math.PI) / deviation;
        }

        /// <summary>
        /// Calculates the standard deviation of an array of samples
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double StandardDeviation(double[] x)
        {
            var sum = 0d;
            var sumOfSqrs = 0d;
            foreach (var sample in x)
            {
                sum += sample;
                sumOfSqrs += Math.Pow(sample, 2);
            }
            var topSum = (x.Length * sumOfSqrs) - (Math.Pow(sum, 2));
            return Math.Sqrt(topSum / (x.Length * (x.Length - 1)));
        }
    }
}
