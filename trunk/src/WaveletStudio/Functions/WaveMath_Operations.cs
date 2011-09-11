using System;

namespace WaveletStudio.Functions
{
    public static partial class WaveMath
    {
        /// <summary>
        /// Add two arrays
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns></returns>
        public static double[] Add(double[] array1, double[] array2)
        {
            var a1 = (double[])array1.Clone();
            var a2 = (double[])array2.Clone();
            if (a1.Length > a2.Length)
            {
                for (var i = 0; i < a2.Length; i++)
                {
                    a1[i] += a2[i];
                }
                return a1;
            }

            for (var i = 0; i < a1.Length; i++)
            {
                a2[i] += a1[i];
            }
            return a2;
        }

        /// <summary>
        /// Sum all the samples in an array with a scalar value
        /// </summary>
        /// <param name="array"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static double[] Add(double[] array, double scalar)
        {
            var newArray = (double[])array.Clone();
            for (var i = 0; i < newArray.Length; i++)
            {
                newArray[i] += scalar;
            }
            return newArray;
        }

        /// <summary>
        /// Multiply two arrays
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns></returns>
        public static double[] Multiply(double[] array1, double[] array2)
        {
            var a1 = (double[])array1.Clone();
            var a2 = (double[])array2.Clone();
            if (a1.Length > a2.Length)
            {
                for (var i = 0; i < a2.Length; i++)
                {
                    a1[i] *= a2[i];
                }
                return a1;
            }

            for (var i = 0; i < a1.Length; i++)
            {
                a2[i] *= a1[i];
            }
            return a2;
        }

        /// <summary>
        /// Multiplies an array by a scalar value
        /// </summary>
        /// <param name="array"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static double[] Multiply(double[] array, double scalar)
        {
            var newArray = (double[])array.Clone();
            for (var i = 0; i < newArray.Length; i++)
            {
                newArray[i] *= scalar;
            }
            return newArray;
        }

        public static double[] SubArray(this double[] array, int length)
        {
            var newArray = MemoryPool.Pool.New<double>(length, true);
            Array.Copy(array, newArray, length > array.Length ? array.Length : length);
            return newArray;
        }
    }
}
