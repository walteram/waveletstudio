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
