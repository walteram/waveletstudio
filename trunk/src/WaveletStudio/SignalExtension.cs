using System;
using WaveletStudio.Functions;

namespace WaveletStudio
{
    /// <summary>
    /// Signal extension methods
    /// </summary>
    public static class SignalExtension
    {
        /// <summary>
        /// Extension modes
        /// </summary>
        public enum ExtensionMode
        {
            /// <summary>
            /// Symmetric-padding (half-point): boundary value symmetric replication
            /// </summary>
            SymmetricHalfPoint,
            /// <summary>
            /// Symmetric-padding (whole-point): boundary value symmetric replication
            /// </summary>
            SymmetricWholePoint,
            /// <summary>
            /// Antisymmetric-padding (half-point): boundary value antisymmetric replication
            /// </summary>
            AntisymmetricHalfPoint,
            /// <summary>
            /// Antisymmetric-padding (whole-point): boundary value antisymmetric replication
            /// </summary>
            AntisymmetricWholePoint,
            /// <summary>
            /// Periodized extension
            /// </summary>
            PeriodicPadding,
            /// <summary>
            /// Zero extension
            /// </summary>
            ZeroPadding,
            /// <summary>
            /// Smooth extension of order 0 (Continuous)
            /// </summary>
            SmoothPadding0, 
            /// <summary>
            /// Smooth extension of order 1
            /// </summary>
            SmoothPadding1
        }

        /// <summary>
        /// Extends a signal using the specified extension mode
        /// </summary>
        /// <param name="input">Signal to extend</param>
        /// <param name="extensionMode">The extension mode</param>
        /// <param name="beforeSize">The extension size of the left side</param>
        /// <param name="afterSize">The extension size of right left side</param>
        /// <returns></returns>
        public static void Extend(ref Signal input, ExtensionMode extensionMode, int beforeSize, int afterSize)
        {
            var samples = Extend(input.Samples, extensionMode, beforeSize, afterSize);
            input.Samples = samples;
            input.CustomPlot = new[]{input.Start, input.Finish};
            input.Start -= input.SamplingInterval * beforeSize;
            input.Finish += input.SamplingInterval * afterSize;
        }
        
        /// <summary>
        /// Extends an array using the specified extension mode
        /// </summary>
        /// <param name="input">Array to extend</param>
        /// <param name="extensionMode">The extension mode</param>
        /// <param name="extensionSize">The extension size of the left and right sides (each one)</param>
        /// <returns></returns>
        public static double[] Extend(double[] input, ExtensionMode extensionMode, int extensionSize)
        {
            return Extend(input, extensionMode, extensionSize, extensionSize);
        }

        /// <summary>
        /// Extends an array using the specified extension mode
        /// </summary>
        /// <param name="input">Array to extend</param>
        /// <param name="extensionMode">The extension mode</param>
        /// <param name="beforeSize">The extension size of the left side</param>
        /// <param name="afterSize">The extension size of right left side</param>
        /// <returns></returns>
        public static double[] Extend(double[] input, ExtensionMode extensionMode, int beforeSize, int afterSize)
        {
            if (input.Length == 0)
            {
                return MemoryPool.Pool.New<double>(beforeSize + afterSize, true);
            }
            var pointsHalfLength = input.Length;
            while (beforeSize > input.Length || afterSize > input.Length)
            {
                input = Extend(input, extensionMode, pointsHalfLength);
                return input;
            }
            var beforeExtension = MemoryPool.Pool.New<double>(beforeSize, true);
            var afterExtension = MemoryPool.Pool.New<double>(afterSize, true);

            if (extensionMode != ExtensionMode.ZeroPadding)
            {
                if (extensionMode == ExtensionMode.SymmetricHalfPoint || extensionMode == ExtensionMode.AntisymmetricHalfPoint)
                {
                    if (beforeSize > 0)
                    {
                        Array.Copy(input, beforeExtension, beforeSize);
                        Array.Reverse(beforeExtension);
                    }
                    Array.Copy(input, input.Length - afterSize, afterExtension, 0, afterSize);
                    Array.Reverse(afterExtension);
                    if (extensionMode == ExtensionMode.AntisymmetricHalfPoint)
                    {
                        if (beforeSize > 0)
                            beforeExtension = WaveMath.Multiply(beforeExtension, -1);
                        afterExtension = WaveMath.Multiply(afterExtension, -1);
                    }
                }
                else if (extensionMode == ExtensionMode.SymmetricWholePoint)
                {
                    if (beforeSize > 0)
                    {
                        Array.Copy(input, input.Length > beforeSize ? 1 : 0, beforeExtension, 0, beforeSize);
                        Array.Reverse(beforeExtension);
                    }
                    Array.Copy(input, input.Length - afterSize - 1 < 0 ? 0 : input.Length - afterSize - 1, afterExtension, 0, afterSize);
                    Array.Reverse(afterExtension);
                }
                else if (extensionMode == ExtensionMode.AntisymmetricWholePoint)
                {
                    if (beforeSize > 0)
                        beforeExtension = GetAntisymmetricWholePointBefore(input, beforeSize);
                    afterExtension = GetAntisymmetricWholePointAfter(input, afterSize);
                }
                else if(extensionMode == ExtensionMode.PeriodicPadding)
                {
                    if (beforeSize > 0)
                        Array.Copy(input, input.Length - beforeSize, beforeExtension, 0, beforeSize);
                    Array.Copy(input, 0, afterExtension, 0, afterSize);                    
                }
                else if (extensionMode == ExtensionMode.SmoothPadding0)
                {
                    if (beforeSize > 0)
                    {
                        for (var i = 0; i < beforeExtension.Length; i++)
                        {
                            beforeExtension[i] = input[0];
                        }
                    }
                    for (var i = 0; i < afterExtension.Length; i++)
                    {
                        afterExtension[i] = input[input.Length-1];
                    }
                }
                else if (extensionMode == ExtensionMode.SmoothPadding1)
                {
                    double previous;
                    if (beforeSize > 0)
                    {
                        var beforeDif = input[0] - input[1];
                        previous = input[0];
                        for (var i = beforeSize - 1; i >= 0; i--)
                        {
                            beforeExtension[i] = previous + beforeDif;
                            previous = beforeExtension[i];
                        }
                    }   
                    var afterDif = input[input.Length - 1] - input[input.Length - 2];
                    previous = input[input.Length - 1];
                    for (var i = 0; i < afterSize; i++)
                    {
                        afterExtension[i] = previous + afterDif;
                        previous = afterExtension[i];
                    }                                     
                }
            }

            var newPoints = MemoryPool.Pool.New<double>(beforeSize + input.Length + afterSize);            
            if (beforeSize > 0)
                beforeExtension.CopyTo(newPoints, 0);
            input.CopyTo(newPoints, beforeSize);    
            afterExtension.CopyTo(newPoints, beforeSize + input.Length);
            return newPoints;
        }

        /// <summary>
        /// Deextends an array, returning only its middle part
        /// </summary>
        /// <param name="input">Array to extend</param>
        /// <param name="size">The extension size of the left and right sides (each one)</param>
        /// <returns></returns>
        public static double[] Deextend(double[] input, int size)
        {
            if (input.Length <= 2)
            {
                return input;
            }
            var padding = (input.Length - size)/2;
            var result = MemoryPool.Pool.New<double>(size);
            Array.Copy(input, padding, result, 0, size);
            return result;
        }

        private static double[] GetAntisymmetricWholePointBefore(double[] points, int beforeSize)
        {
            var beforeExtension = MemoryPool.Pool.New<double>(beforeSize);
            if (points.Length == 1)
            {
                for (var i = 0; i < beforeSize; i++)
                {
                    beforeExtension[i] = points[0] * -1;    
                }
                return beforeExtension;
            }
            var k = beforeSize - 1;
            for (var i = 0; i < beforeSize; i++)
            {
                var dif = (i + 1 < points.Length) ? points[i] - points[i + 1] : 0;
                var previous = i == 0 ? points[0] : beforeExtension[k + 1];
                beforeExtension[k] = previous + dif;
                k--;
            }
            return beforeExtension;
        }

        private static double[] GetAntisymmetricWholePointAfter(double[] points, int afterSize)
        {
            var afterExtension = MemoryPool.Pool.New<double>(afterSize);
            if (points.Length == 1)
            {
                for (var i = 0; i < afterSize; i++)
                {
                    afterExtension[i] = points[0] * -1;
                }
                return afterExtension;
            }
            var k = 0;
            for (var i = points.Length - 1; i >= points.Length - afterSize; i--)
            {
                var dif = i >= 1 ? points[i] - points[i - 1] : 0;
                var previous = k == 0 ? points[i] : afterExtension[k - 1];
                afterExtension[k] = previous + dif;
                k++;
            }
            return afterExtension;
        }

        /// <summary>
        /// Gets the next power of 2 of an number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int NextPowerOf2(int number)
        {            
            number = (number >> 1) | number;
            number = (number >> 2) | number;
            number = (number >> 4) | number;
            number = (number >> 8) | number;
            number = (number >> 16) | number;
            number++;
            return number;
        }        
    }
}
