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
using System.Linq;

namespace WaveletStudio.Functions
{
    public static partial class WaveMath
    {
        
        /// <summary>
        /// Executes a relational operation between two arrays
        /// </summary>
        public static double[] ExecuteRelationalOperation(RelationalOperatorEnum operation, double[] array1, double[] array2)
        {
            var output = MemoryPool.Pool.New<double>(Math.Max(array1.Length, array2.Length));
            var function = GetRelationalOperationFunction(operation);
            for (var i = 0; i < Math.Min(array1.Length, array2.Length); i++)
            {  
                output[i] = function(array1[i], array2[i]);
            }
            return output;
        }

        /// <summary>
        /// Executes a relational in an array, comparing each sample with the next one
        /// </summary>
        public static double[] ExecuteRelationalOperationWithNextSample(RelationalOperatorEnum operation, double[] array)
        {
            var output = MemoryPool.Pool.New<double>(array.Length);
            var function = GetRelationalOperationFunction(operation);
            for (var i = 0; i < array.Length - 1; i++)
            {
                output[i] = function(array[i], array[i + 1]);
            }
            return output;
        }

        /// <summary>
        /// Executes a relational in an array, comparing each sample with the previous one
        /// </summary>
        public static double[] ExecuteRelationalOperationWithPreviousSample(RelationalOperatorEnum operation, double[] array)
        {
            var output = MemoryPool.Pool.New<double>(array.Length);
            var function = GetRelationalOperationFunction(operation);
            for (var i = 1; i < array.Length; i++)
            {
                output[i] = function(array[i], array[i - 1]);
            }
            return output;
        }

        /// <summary>
        /// Executes a relational in an array, comparing each sample with the provided static value
        /// </summary>
        public static double[] ExecuteRelationalOperation(RelationalOperatorEnum operation, double[] array, double staticValue)
        {
            var output = MemoryPool.Pool.New<double>(array.Length);
            var function = GetRelationalOperationFunction(operation);
            for (var i = 0; i < array.Length; i++)
            {
                output[i] = function(array[i], staticValue);
            }
            return output;
        }

        /// <summary>
        /// Executes a relational operation between two or more signals
        /// </summary>
        public static Signal ExecuteRelationalOperation(RelationalOperatorEnum operation, params Signal[] signals)
        {
            signals = signals.Where(it => it != null && it.Samples != null && it.Samples.Length > 0).OrderByDescending(it => it.Samples.Length).ToArray();
            if (signals.Length == 0)
                return null;

            var newSignal = signals[0].Clone();
            for (var i = 1; i < signals.Length; i++)
            {
                newSignal.Samples = ExecuteRelationalOperation(operation, newSignal.Samples, signals[i].Samples);
            }
            return newSignal;
        }

        /// <summary>
        /// Executes a relational operation between two or more signals
        /// </summary>
        public static Signal ExecuteRelationalOperation(RelationalOperatorEnum operation, Signal signal, double staticValue)
        {
            var newSignal = signal.Clone();
            newSignal.Samples = ExecuteRelationalOperation(operation, newSignal.Samples, staticValue);
            return newSignal;
        }

        /// <summary>
        /// Executes a relational in a signal, comparing each sample with the previous one
        /// </summary>
        public static Signal ExecuteRelationalOperationWithPreviousSample(RelationalOperatorEnum operation, Signal signal)
        {
            var newSignal = signal.Clone();
            newSignal.Samples = ExecuteRelationalOperationWithPreviousSample(operation, newSignal.Samples);
            return newSignal;
        }

        /// <summary>
        /// Executes a relational in a signal, comparing each sample with the next one
        /// </summary>
        public static Signal ExecuteRelationalOperationWithNextSample(RelationalOperatorEnum operation, Signal signal)
        {
            var newSignal = signal.Clone();
            newSignal.Samples = ExecuteRelationalOperationWithNextSample(operation, newSignal.Samples);
            return newSignal;
        }

        /// <summary>
        /// Gets the function of specified relational operator
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public static Func<double, double, double> GetRelationalOperationFunction(RelationalOperatorEnum operation)
        {
            if (operation == RelationalOperatorEnum.GreaterThan)
            {
                return (x1, x2) => (x1 > x2) ? 1d : 0d;
            }
            if (operation == RelationalOperatorEnum.LessThan)
            {
                return (x1, x2) => (x1 < x2) ? 1d : 0d;
            }
            if (operation == RelationalOperatorEnum.GreaterOrEqualsThan)
            {
                return (x1, x2) => (x1 >= x2) ? 1d : 0d;
            }
            if (operation == RelationalOperatorEnum.LessOrEqualsThan)
            {
                return (x1, x2) => (x1 <= x2) ? 1d : 0d;
            }
            if (operation == RelationalOperatorEnum.NotEqualsTo)
            {
                return (x1, x2) => (x1 != x2) ? 1d : 0d;
            }
            return (x1, x2) => (x1 == x2) ? 1d : 0d;    //EqualsTo
        }
        
        /// <summary>
        /// Relational Operators
        /// </summary>
        public enum RelationalOperatorEnum
        {
            /// <summary>
            /// &gt;
            /// </summary>
            GreaterThan,

            /// <summary>
            /// &lt;
            /// </summary>
            LessThan,

            /// <summary>
            /// &gt;=
            /// </summary>
            GreaterOrEqualsThan,

            /// <summary>
            /// &lt;=
            /// </summary>
            LessOrEqualsThan,

            /// <summary>
            /// =
            /// </summary>
            EqualsTo,

            /// <summary>
            /// !=
            /// </summary>
            NotEqualsTo,
        }
    }
}
