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

namespace WaveletStudio.Functions
{
    public static partial class WaveMath
    {
        /// <summary>
        /// Switch output between first input A and B based on threshold value.
        /// </summary>
        public static double[] Switch(double[] a, double[] b, double[] threshold, SwitchCriteriaEnum switchCriteria)
        {
            var size = Math.Max(a.Length, b.Length);
            var result = MemoryPool.Pool.New<double>(size);
            for (var i = 0; i < size; i++)
            {
                if (i < a.Length && i < b.Length && i < threshold.Length) 
                    result[i] = Switch(a[i], b[i], threshold[i], switchCriteria);
                else if (i < a.Length)
                    result[i] = a[i];
                else if (i < b.Length)
                    result[i] = b[i];                
            }
            return result;
        }

        /// <summary>
        /// Switch output between first input A and B based on threshold value.
        /// </summary>
        public static double[] Switch(double[] a, double[] b, double threshold, SwitchCriteriaEnum switchCriteria)
        {
            var size = Math.Max(a.Length, b.Length);
            var result = MemoryPool.Pool.New<double>(size);
            for (var i = 0; i < size; i++)
            {
                if (i < a.Length && i < b.Length)
                    result[i] = Switch(a[i], b[i], threshold, switchCriteria);
                else if (i < a.Length)
                    result[i] = a[i];
                else
                    result[i] = b[i];
            }
            return result;
        }

        /// <summary>
        /// Switch output between first input A and B based on threshold value.
        /// </summary>
        public static double Switch(double a, double b, double threshold, SwitchCriteriaEnum switchCriteria)
        {
            if (switchCriteria == SwitchCriteriaEnum.BIsGreaterThanThreshold && b > threshold ||
                switchCriteria == SwitchCriteriaEnum.BIsLessThanThreshold && b < threshold ||
                switchCriteria == SwitchCriteriaEnum.BIsGreaterOrEqualsThanThreshold && b >= threshold ||
                switchCriteria == SwitchCriteriaEnum.BIsLessOrEqualsThanThreshold && b <= threshold)
                return b;
            return a;
        }

        /// <summary>
        /// Switch operation criteria enum
        /// </summary>
        public enum SwitchCriteriaEnum
        {
            /// <summary>
            /// Select B when B &gt; threshold, otherwise select A
            /// </summary>
            BIsGreaterThanThreshold,

            /// <summary>
            /// Select B when B &lt; threshold, otherwise select A
            /// </summary>
            BIsLessThanThreshold,

            /// <summary>
            /// Select B when B &gt;= threshold, otherwise select A
            /// </summary>
            BIsGreaterOrEqualsThanThreshold,

            /// <summary>
            /// Select B when B &lt;= threshold, otherwise select A
            /// </summary>
            BIsLessOrEqualsThanThreshold
        }
    }
}
