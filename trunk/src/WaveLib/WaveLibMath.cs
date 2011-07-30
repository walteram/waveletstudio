﻿using System;
using System.Threading.Tasks;
using ILNumerics;

namespace WaveletStudio.WaveLib
{
    /// <summary>
    /// Common math and statistical operations
    /// </summary>
    public static class WaveLibMath
    {
        /// <summary>
        /// Gets the accumulated energy of a signal
        /// </summary>
        /// <param name="samples"></param>
        /// <returns></returns>
        public static double GetAccumulatedEnergy(ILArray<double> samples)
        {
            if (samples.IsEmpty)
            {
                return 0;
            }
            var energy = 0d;
            for (var i = 0; i < samples.Length; i++)
            {
                energy += GetEnergy(samples.GetValue(i));
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
        /// Calculates the mode of an array, using the Paralleling
        /// </summary>
        /// <param name="samples"></param>
        /// <returns></returns>
        public static double Mode(ILArray<double> samples)
        {
            if (samples.IsEmpty)
            {
                return 0;
            }
            var sortedSamples = ILNumerics.BuiltInFunctions.ILMath.unique(samples);
            var maxFreq = sortedSamples.GetValue(0);
            var maxOccurrences = 0;
            Parallel.For(0, sortedSamples.Length, i =>
            {
                var occurrences = 0;
                Parallel.For(0, samples.Length, j =>
                {
                    if (samples.GetValue(j) == sortedSamples.GetValue(i))
                    {
                        occurrences++;
                    }
                });
                if (occurrences <= maxOccurrences)
                {
                    return;
                }
                maxOccurrences = occurrences;
                maxFreq = sortedSamples.GetValue(i);
            });
            return (maxFreq);
        }

        /// <summary>
        /// Scales a number based in its minimun and maximum
        /// </summary>
        /// <param name="x"></param>
        /// <param name="currentMin"></param>
        /// <param name="currentMax"></param>
        /// <param name="newMin"></param>
        /// <param name="newMax"></param>
        /// <returns></returns>
        public static double Scale(double x, double currentMin, double currentMax, double newMin, double newMax)
        {
            return newMin + (x - currentMin) / (currentMax - currentMin) * (newMax - newMin);
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
            Parallel.For(0, x.Length, i => x[i] = NormalDistribution(x[i], mean, deviation));
            return x;
        }

        /// <summary>
        /// Calculates the normal (or Gaussian) distribution of a sample
        /// </summary>
        /// <param name="x"></param>
        /// <param name="mean"></param>
        /// <param name="deviation"></param>
        /// <returns></returns>
        public static double NormalDistribution(double x, double mean, double deviation)
        {
            return (1/Math.Sqrt(2*Math.PI*deviation)) * Math.Exp(-1*(Math.Pow(x - mean, 2)/(2*deviation)));
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
            Parallel.For(0, x.Length, i =>
                                          {
                                              sum += x[i];
                                              sumOfSqrs += Math.Pow(x[i], 2);
                                          });
            var topSum = (x.Length * sumOfSqrs) - (Math.Pow(sum, 2));
            return Math.Sqrt(topSum / (x.Length * (x.Length - 1)));
        }
    }
}