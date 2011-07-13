using System;
using System.Threading.Tasks;
using ILNumerics;

namespace WaveletStudio.WaveLib
{
    /// <summary>
    /// Common operations
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
                energy += Math.Pow(Math.Abs(samples.GetValue(i)), 2);
            }            
            return energy;
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

    }
}
