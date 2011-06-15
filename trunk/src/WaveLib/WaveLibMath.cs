using System;
using ILNumerics;

namespace WaveLib
{
    public static class WaveLibMath
    {
        public static double GetAccumulatedEnergy(ILArray<double> points)
        {
            var energy = 0d;
            for (var i = 0; i < points.Length; i++)
            {
                energy += Math.Pow(Math.Abs(points.GetValue(i)), 2);
            }
            return energy;
        }
    }
}
