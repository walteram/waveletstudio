using System;
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
        /// <param name="points"></param>
        /// <returns></returns>
        public static double GetAccumulatedEnergy(ILArray<double> points)
        {
            if (points.IsEmpty)
            {
                return 0;
            }
            var energy = 0d;
            for (var i = 0; i < points.Length; i++)
            {
                energy += Math.Pow(Math.Abs(points.GetValue(i)), 2);
            }
            return energy;
        }
    }
}
