using System;
using ILNumerics;

namespace WaveLib
{
    public class DecompositionLevel
    {
        /// <summary>
        /// Approximation coefficients of this decomposition level
        /// </summary>
        public ILArray<double> Approximation { get; set; }

        /// <summary>
        /// Detail coefficients of this decomposition level
        /// </summary>
        public ILArray<double> Detail { get; set; }

        public double GetDetailsEnergy()
        {
            var energy = 0d;
            for (var i = 0; i < Detail.Length; i++)
            {
                energy += Math.Pow(Math.Abs(Detail.GetValue(i)), 2);
            }
            return energy;
        }
    }
}
