using System;
using System.Linq;

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
            var a1 = (double[])array2.Clone();
            var a2 = (double[])array1.Clone();
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
        /// Subtract two arrays
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns></returns>
        public static double[] Subtract(double[] array1, double[] array2)
        {
            var a1 = (double[])array2.Clone();
            var a2 = (double[])array1.Clone();
            if (a1.Length > a2.Length)
            {
                for (var i = 0; i < a2.Length; i++)
                {
                    a1[i] -= a2[i];
                }
                return a1;
            }

            for (var i = 0; i < a1.Length; i++)
            {
                a2[i] -= a1[i];
            }
            return a2;
        }

        /// <summary>
        /// Subtract all the samples in an array by a scalar value
        /// </summary>
        /// <param name="array"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static double[] Subtract(double[] array, double scalar)
        {
            return Add(array, scalar * -1);
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

        /// <summary>
        /// Divide an array by other
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns></returns>
        public static double[] Divide(double[] array1, double[] array2)
        {
            var a1 = (double[])array2.Clone();
            var a2 = (double[])array1.Clone();
            if (a1.Length > a2.Length)
            {
                for (var i = 0; i < a2.Length; i++)
                {
                    if (Math.Abs(a2[i]) < Double.Epsilon)
                        a1[i] = 0;
                    else
                        a1[i] /= a2[i];
                }
                return a1;
            }

            for (var i = 0; i < a1.Length; i++)
            {
                if (Math.Abs(a1[i]) < Double.Epsilon)
                    a2[i] = 0;
                else
                    a2[i] /= a1[i];
            }
            return a2;
        }

        /// <summary>
        /// Divides an array by a scalar value
        /// </summary>
        /// <param name="array"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static double[] Divide(double[] array, double scalar)
        {
            return Multiply(array, Math.Abs(scalar) < Double.Epsilon ? 0 : 1 / scalar);
        }

        /// <summary>
        /// Gets a subarrar with the specified length
        /// </summary>
        /// <param name="array"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static double[] SubArray(this double[] array, int length)
        {
            var newArray = MemoryPool.Pool.New<double>(length, true);
            Array.Copy(array, newArray, length > array.Length ? array.Length : length);
            return newArray;
        }

        /// <summary>
        /// Executes an operation between two or more signals
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="signals"></param>
        /// <returns></returns>
        public static Signal ExecuteOperation(OperationEnum operation, params Signal[] signals)
        {
            signals = signals.Where(it => it != null && it.Samples != null && it.Samples.Length > 0).OrderByDescending(it => it.Samples.Length).ToArray();
            if(signals.Length == 0)
                return null;

            var function = GetOperationFunction(operation);
            var newSignal = signals[0].Clone();
            for (var i = 1; i < signals.Length; i++)
            {
                newSignal.Samples = function(newSignal.Samples, signals[i].Samples);
            }
            return newSignal;
        }

        /// <summary>
        /// Gets the function of specified operation
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public static Func<double[], double[], double[]> GetOperationFunction(OperationEnum operation)
        {
            if (operation == OperationEnum.Sum)
                return Add;
            if (operation == OperationEnum.Subtract)
                return Subtract;
            if (operation == OperationEnum.Divide)
                return Divide;
            return Multiply;
        }

        /// <summary>
        /// Gets the function of specified scalar operation
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public static Func<double[], double, double[]> GetScalarOperationFunction(OperationEnum operation)
        {
            if (operation == OperationEnum.Sum)
                return Add;
            if (operation == OperationEnum.Subtract)
                return Subtract;
            if (operation == OperationEnum.Divide)
                return Divide;
            return Multiply;
        }

        /// <summary>
        /// Gets the symbol of the specified operation
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public static string GetOperationSymbol(OperationEnum operation)
        {
            string signal;
            switch (operation)
            {
                case OperationEnum.Sum:
                    signal = "+";
                    break;
                case OperationEnum.Multiply:
                    signal = "*";
                    break;
                case OperationEnum.Subtract:
                    signal = "-";
                    break;
                default:
                    signal = "/";
                    break;
            }
            return signal;
        }
        
        /// <summary>
        /// Operation
        /// </summary>
        public enum OperationEnum
        {
            /// <summary>
            /// Multiply
            /// </summary>
            Multiply,
            /// <summary>
            /// Sum
            /// </summary>
            Sum,
            /// <summary>
            /// Subtract
            /// </summary>
            Subtract,
            /// <summary>
            /// Divide
            /// </summary>
            Divide
        }
    }
}
