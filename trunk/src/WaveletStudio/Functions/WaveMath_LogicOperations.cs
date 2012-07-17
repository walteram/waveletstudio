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
        /// Executes a logical operation between two arrays
        /// </summary>
        public static double[] ExecuteLogicOperation(LogicalOperationEnum operation, double[] array1, double[] array2)
        {
            var output = MemoryPool.Pool.New<double>(Math.Max(array1.Length, array2.Length));
            var function = GetLogicalOperationFunction(operation);
            for (var i = 0; i < Math.Min(array1.Length, array2.Length); i++)
            {
                output[i] = function(array1[i], array2[i]);
            }
            return output;
        }


        /// <summary>
        /// Executes a logical operation between two or more signals
        /// </summary>
        public static Signal ExecuteLogicOperation(LogicalOperationEnum operation, params Signal[] signals)
        {
            signals = signals.Where(it => it != null && it.Samples != null && it.Samples.Length > 0).OrderByDescending(it => it.Samples.Length).ToArray();
            if (signals.Length == 0)
                return null;

            var newSignal = signals[0].Clone();
            for (var i = 1; i < signals.Length; i++)
            {
                newSignal.Samples = ExecuteLogicOperation(operation, newSignal.Samples, signals[i].Samples);
            }
            return newSignal;
        }

        /// <summary>
        /// Gets the function of specified logical operation
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public static Func<double, double, double> GetLogicalOperationFunction(LogicalOperationEnum operation)
        {
            if(operation == LogicalOperationEnum.And)
            {
                return (x1, x2) => (x1 != 0d && x2 != 0d) ? 1d : 0d;
            }
            if (operation == LogicalOperationEnum.Or)
            {
                return (x1, x2) => (x1 != 0d || x2 != 0d) ? 1d : 0d;
            }
            if (operation == LogicalOperationEnum.Xor)
            {
                return (x1, x2) => (x1 != 0d ^ x2 != 0d) ? 1d : 0d;
            }
            if (operation == LogicalOperationEnum.NotAnd)
            {
                return (x1, x2) => !(x1 != 0d && x2 != 0d) ? 1d : 0d;
            }
            if (operation == LogicalOperationEnum.NotOr)
            {
                return (x1, x2) => !(x1 != 0d || x2 != 0d) ? 1d : 0d;
            }
            if (operation == LogicalOperationEnum.NotXor)
            {
                return (x1, x2) => !(x1 != 0d ^ x2 != 0d) ? 1d : 0d;
            }            
            return (x1, x2) => (x1 == 0d) ? 1d : 0d; //Not
        }

        /// <summary>
        /// Logical Operation
        /// </summary>
        public enum LogicalOperationEnum
        {
            /// <summary>
            /// And (&&)
            /// </summary>
            And,
            /// <summary>
            /// Or (||)
            /// </summary>
            Or,
            /// <summary>
            /// Xor (^)
            /// </summary>
            Xor,
            /// <summary>
            /// Not And (!&&)
            /// </summary>
            NotAnd,
            /// <summary>
            /// Not Or (!||)
            /// </summary>
            NotOr,
            /// <summary>
            /// Not Xor (!^)
            /// </summary>
            NotXor,
            /// <summary>
            /// Not (!)
            /// </summary>
            Not
        }
    }
}
