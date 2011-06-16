using System;
using ILNumerics;
using ILNumerics.BuiltInFunctions;

namespace WaveLib
{
    public static class MathUtils
    {
        /// <summary>
        /// Convolution
        /// </summary>
        /// <param name="matrixA"></param>
        /// <param name="matrixB"></param>
        /// <param name="returnOnlyValid"></param>
        /// <param name="margin"></param>
        /// <returns></returns>
        public static ILArray<double> Convolve(ILArray<double> matrixA, ILArray<double> matrixB, bool returnOnlyValid = true, int margin = 0)
        {
            if (matrixA.Length < matrixB.Length)
            {
                var auxSignal = matrixA.C;
                matrixA = matrixB.C;
                matrixB = auxSignal;
            }
            var result = new double[matrixA.Length + matrixB.Length - 1];
            for (var i = 0; i < matrixA.Length; i++)
            {
                for (var j = 0; j < matrixB.Length; j++)
                {
                    result[i + j] = result[i + j] + matrixA.GetValue(i) * matrixB.GetValue(j);
                }
            }

            if (returnOnlyValid)
            {
                var size = matrixA.Length - matrixB.Length + 1;
                var padding = (result.Length - size) / 2;
                return new ILArray<double>(result)[string.Format("{0}:1:{1}", padding + margin, padding + size - 1 - margin)];
            }
            return new ILArray<double>(result);
        }

        public static ILArray<double> DownSample(ILArray<double> input)
        {
            var size = input.Length / 2;
            var result = new double[size];
            var j = 0;
            for (var i = 0; i < input.Length; i++)
            {
                if (i % 2 == 0)
                    continue;
                result[j] = input.GetValue(i);
                j++;
            }
            return new ILArray<double>(result);
        }

        public static ILArray<double> UpSample(ILArray<double> input)
        {
            if (input.IsEmpty)
            {
                return ILMath.empty();
            }
            var size = input.Length * 2;
            var result = new double[size - 1];
            for (var i = 0; i < input.Length; i++)
            {
                result[i * 2] = input.GetValue(i);
            }
            return new ILArray<double>(result);
        }

        public static double CalculateEnergy(ILArray<double> input)
        {
            var energy = 0d;
            for (var i = 0; i < input.Length; i++)
            {
                energy += Math.Pow(Math.Abs(input.GetValue(i)), 2);
            }
            return energy;
        }
    }
}
