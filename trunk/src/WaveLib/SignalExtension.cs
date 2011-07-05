using ILNumerics;
using ILNumerics.BuiltInFunctions;

namespace WaveletStudio.WaveLib
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
        /// Extends an array using the specified extension mode
        /// </summary>
        /// <param name="input">Array to extend</param>
        /// <param name="extensionMode">The extension mode</param>
        /// <param name="extensionSize">The extension size of the left and right sides (each one)</param>
        /// <returns></returns>
        public static ILArray<double> Extend(ILArray<double> input, ExtensionMode extensionMode, int extensionSize)
        {
            if (input.IsEmpty)
            {
                return new ILArray<double>(2);
            }
            var pointsHalfLength = input.Length;
            while (extensionSize > input.Length)
            {
                input = Extend(input, extensionMode, pointsHalfLength);
                return input;
            }
            var beforeSize = extensionSize;
            var afterSize = extensionSize;
            ILArray<double> beforeExtension = null;
            ILArray<double> afterExtension = null;

            if (extensionMode == ExtensionMode.ZeroPadding)
            {
                beforeExtension = ILMath.zeros(beforeSize);
                afterExtension = ILMath.zeros(afterSize);
            }
            else
            {
                string afterExpression;
                string beforeExpression;
                if (extensionMode == ExtensionMode.SymmetricHalfPoint || extensionMode == ExtensionMode.AntisymmetricHalfPoint)
                {
                    if (beforeSize > 0)
                    {
                        beforeExpression = beforeSize == 1 ? "0:1:0" : string.Format("{0}:-1:0", beforeSize - 1);
                        beforeExtension = input[beforeExpression];
                    }
                    afterExpression = afterSize == 1 ? string.Format("{0}:1:end", input.Length - 1) : string.Format("end:-1:{0}", input.Length - afterSize);
                    afterExtension = input[afterExpression];
                    if (extensionMode == ExtensionMode.AntisymmetricHalfPoint)
                    {
                        if (beforeSize > 0) 
                            ILMath.invert(beforeExtension, beforeExtension);
                        ILMath.invert(afterExtension, afterExtension);
                    }
                }
                else if (extensionMode == ExtensionMode.SymmetricWholePoint)
                {
                    if (beforeSize > 0)
                    {
                        beforeExpression = beforeSize == 1 ? "1:1:1" : string.Format("{0}:-1:1", beforeSize);
                        beforeExtension = input[beforeExpression];
                    }
                    afterExpression = afterSize == 1 ? string.Format("{0}:1:{0}", input.Length - 2) : string.Format("{0}:-1:{1}", input.Length - 2, input.Length - afterSize - 1);                                       
                    afterExtension = input[afterExpression];
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
                        beforeExtension = input[string.Format("{0}:1:end", input.Length - beforeSize)];
                    afterExtension = input[string.Format("0:1:{0}", afterSize - 1)];
                }
                else if (extensionMode == ExtensionMode.SmoothPadding0)
                {
                    if (beforeSize > 0)
                        beforeExtension = input["0:1:0"];
                    afterExtension = input[string.Format("{0}:1:{0}", input.Length-1)];
                }
                else if (extensionMode == ExtensionMode.SmoothPadding1)
                {
                    double previous;
                    if (beforeSize > 0)
                    {
                        beforeExtension = new ILArray<double>(beforeSize);
                        var beforeDif = input.GetValue(0) - input.GetValue(1);
                        previous = input.GetValue(0);
                        for (var i = beforeSize - 1; i >= 0; i--)
                        {
                            beforeExtension[i] = previous + beforeDif;
                            previous = beforeExtension.GetValue(i);
                        }
                    }   
                    afterExtension = new ILArray<double>(afterSize);
                    var afterDif = input.GetValue(input.Length - 1) - input.GetValue(input.Length - 2);
                    previous = input.GetValue(input.Length - 1);
                    for (var i = 0; i < afterSize; i++)
                    {
                        afterExtension[i] = previous + afterDif;
                        previous = afterExtension.GetValue(i);
                    }                                     
                }
            }

            var newPoints = new ILArray<double>(beforeSize + input.Length + afterSize);
            if (beforeSize > 0)
                newPoints[string.Format("0:1:{0}", beforeSize - 1)] = beforeExtension;
            newPoints[string.Format("{0}:1:{1}", beforeSize, beforeSize + input.Length - 1)] = input;
            newPoints[string.Format("{0}:1:end", beforeSize + input.Length)] = afterExtension;

            return newPoints;
        }

        /// <summary>
        /// Deextends an array, returning only its middle part
        /// </summary>
        /// <param name="input">Array to extend</param>
        /// <param name="size">The extension size of the left and right sides (each one)</param>
        /// <returns></returns>
        public static ILArray<double> Deextend(ILArray<double> input, int size)
        {
            if (input.Length <= 2)
            {
                return input;
            }
            var padding = (input.Length - size)/2;
            return input[string.Format("{0}:1:{1}", padding, padding + size - 1)];
        }

        private static ILArray<double> GetAntisymmetricWholePointBefore(ILArray<double> points, int beforeSize)
        {            
            var beforeExtension = new ILArray<double>(beforeSize);
            if (points.Length == 1)
            {
                beforeExtension["0:1:end"] = -points.GetValue(0);
                return beforeExtension;
            }
            var k = beforeSize - 1;
            for (var i = 0; i < beforeSize; i++)
            {
                var dif = points[i] - points[i + 1];
                var previous = i == 0 ? points[0] : beforeExtension[k + 1];
                beforeExtension[k] = previous + dif;
                k--;
            }
            return beforeExtension;
        }

        private static ILArray<double> GetAntisymmetricWholePointAfter(ILArray<double> points, int afterSize)
        {
            var afterExtension = new ILArray<double>(afterSize);
            if (points.Length == 1)
            {
                afterExtension["0:1:end"] = -points.GetValue(0);
                return afterExtension;
            }
            var k = 0;
            for (var i = points.Length - 1; i >= points.Length - afterSize; i--)
            {
                var dif = points[i] - points[i - 1];
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
