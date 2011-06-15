using System;
using ILNumerics;

namespace WaveLib
{
    public class DecompositionLevel
    {
        public ILArray<double> Approximation { get; set; }
        public ILArray<double> Details { get; set; }

        public double GetDetailsEnergy()
        {
            var energy = 0d;
            for (var i = 0; i < Details.Length; i++)
            {
                energy += Math.Pow(Math.Abs(Details.GetValue(i)), 2);
            }
            return energy;
        }
    }
}
