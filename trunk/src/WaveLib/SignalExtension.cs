using ILNumerics;
using ILNumerics.BuiltInFunctions;

namespace WaveLib
{
    public static class SignalExtension
    {
        public enum ExtensionMode
        {
            SymmetricHalfPoint,
            SymmetricWholePoint,
            AntisymmetricHalfPoint,
            AntisymmetricWholePoint,
            PeriodicPadding,
            ZeroPadding,
            SmoothPadding0, //Continuous
            SmoothPadding1
        }

        public static ILArray<double> Extend(ILArray<double> points, ExtensionMode extendMode, int extensionSize)
        {
            if (points.IsEmpty)
            {
                return new ILArray<double>(2);
            }
            var pointsHalfLength = points.Length;
            while (extensionSize > points.Length)
            {
                points = Extend(points, extendMode, pointsHalfLength);
                return points;
            }
            var beforeSize = extensionSize;
            var afterSize = extensionSize;
            ILArray<double> beforeExtension = null;
            ILArray<double> afterExtension = null;

            if (extendMode == ExtensionMode.ZeroPadding)
            {
                beforeExtension = ILMath.zeros(beforeSize);
                afterExtension = ILMath.zeros(afterSize);
            }
            else
            {
                string afterExpression;
                string beforeExpression;
                if (extendMode == ExtensionMode.SymmetricHalfPoint || extendMode == ExtensionMode.AntisymmetricHalfPoint)
                {
                    if (beforeSize > 0)
                    {
                        beforeExpression = beforeSize == 1 ? "0:1:0" : string.Format("{0}:-1:0", beforeSize - 1);
                        beforeExtension = points[beforeExpression];
                    }
                    afterExpression = afterSize == 1 ? string.Format("{0}:1:end", points.Length - 1) : string.Format("end:-1:{0}", points.Length - afterSize);
                    afterExtension = points[afterExpression];
                    if (extendMode == ExtensionMode.AntisymmetricHalfPoint)
                    {
                        if (beforeSize > 0) 
                            ILMath.invert(beforeExtension, beforeExtension);
                        ILMath.invert(afterExtension, afterExtension);
                    }
                }
                else if (extendMode == ExtensionMode.SymmetricWholePoint)
                {
                    if (beforeSize > 0)
                    {
                        beforeExpression = beforeSize == 1 ? "1:1:1" : string.Format("{0}:-1:1", beforeSize);
                        beforeExtension = points[beforeExpression];
                    }
                    afterExpression = afterSize == 1 ? string.Format("{0}:1:{0}", points.Length - 2) : string.Format("{0}:-1:{1}", points.Length - 2, points.Length - afterSize - 1);                                       
                    afterExtension = points[afterExpression];
                }
                else if (extendMode == ExtensionMode.AntisymmetricWholePoint)
                {
                    if (beforeSize > 0)
                        beforeExtension = GetAntisymmetricWholePointBefore(points, beforeSize);
                    afterExtension = GetAntisymmetricWholePointAfter(points, afterSize);
                }
                else if(extendMode == ExtensionMode.PeriodicPadding)
                {
                    if (beforeSize > 0)
                        beforeExtension = points[string.Format("{0}:1:end", points.Length - beforeSize)];
                    afterExtension = points[string.Format("0:1:{0}", afterSize - 1)];
                }
                else if (extendMode == ExtensionMode.SmoothPadding0)
                {
                    if (beforeSize > 0)
                        beforeExtension = points["0:1:0"];
                    afterExtension = points[string.Format("{0}:1:{0}", points.Length-1)];
                }
                else if (extendMode == ExtensionMode.SmoothPadding1)
                {
                    double previous;
                    if (beforeSize > 0)
                    {
                        beforeExtension = new ILArray<double>(beforeSize);
                        var beforeDif = points.GetValue(0) - points.GetValue(1);
                        previous = points.GetValue(0);
                        for (var i = beforeSize - 1; i >= 0; i--)
                        {
                            beforeExtension[i] = previous + beforeDif;
                            previous = beforeExtension.GetValue(i);
                        }
                    }   
                    afterExtension = new ILArray<double>(afterSize);
                    var afterDif = points.GetValue(points.Length - 1) - points.GetValue(points.Length - 2);
                    previous = points.GetValue(points.Length - 1);
                    for (var i = 0; i < afterSize; i++)
                    {
                        afterExtension[i] = previous + afterDif;
                        previous = afterExtension.GetValue(i);
                    }                                     
                }
            }

            var newPoints = new ILArray<double>(beforeSize + points.Length + afterSize);
            if (beforeSize > 0)
                newPoints[string.Format("0:1:{0}", beforeSize - 1)] = beforeExtension;
            newPoints[string.Format("{0}:1:{1}", beforeSize, beforeSize + points.Length - 1)] = points;
            newPoints[string.Format("{0}:1:end", beforeSize + points.Length)] = afterExtension;

            return newPoints;
        }

        public static ILArray<double> Deextend(ILArray<double> points, int size)
        {
            if (points.Length <= 2)
            {
                return points;
            }
            var padding = (points.Length - size)/2;
            return points[string.Format("{0}:1:{1}", padding, padding + size - 1)];
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
