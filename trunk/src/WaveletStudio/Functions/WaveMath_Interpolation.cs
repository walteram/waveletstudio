using System;

namespace WaveletStudio.Functions
{
    public static partial class WaveMath
    {
        /// <summary>
        /// Linear interpolation. Factor must be >= 2
        /// </summary>
        /// <returns></returns>
        public static Signal InterpolateLinear(Signal signal, uint factor)
        {
            if (factor < 2)
                return signal.Clone();

            var newSignal = signal.Copy();
            var newSamples = MemoryPool.Pool.New<double>(Convert.ToInt32(signal.Samples.Length*factor-factor+1));
            var currentIndex = 0;
            var newInterval = signal.SamplingInterval/factor;
            var currentX = signal.Start;
            var newX = currentX;
            for (var i = 0; i < signal.SamplesCount; i++)
            {
                if (i == signal.SamplesCount - 1)
                {
                    newSamples[currentIndex] = signal.Samples[i];
                    break;
                }
                var y0 = signal.Samples[i];
                var y1 = signal.Samples[i+1];
                var x0 = currentX;
                var x1 = currentX + signal.SamplingInterval;
                var div = ((y1 - y0)/(x1 - x0));
                for (var j = 0; j < factor; j++)
                {
                    newSamples[currentIndex] = y0 + (newX - x0)*div;
                    currentIndex++;
                    newX += newInterval;                    
                }
                currentX = x1;
            }
            newSignal.Samples = newSamples;
            newSignal.SamplingInterval = newInterval;            
            return newSignal;
        }

        /// <summary>
        /// Nearest interpolation. Factor must be >= 2
        /// </summary>
        /// <returns></returns>
        public static Signal InterpolateNearest(Signal signal, uint factor)
        {
            if (factor < 2)
                return signal.Clone();

            var newSignal = signal.Copy();
            var newSamples = MemoryPool.Pool.New<double>(Convert.ToInt32(signal.Samples.Length * factor - factor + 1));
            var currentIndex = 0;
            var newInterval = signal.SamplingInterval / factor;
            var currentX = signal.Start;
            var newX = currentX;
            for (var i = 0; i < signal.SamplesCount; i++)
            {
                if (i == signal.SamplesCount - 1)
                {
                    newSamples[currentIndex] = signal.Samples[i];
                    break;
                }
                var y0 = signal.Samples[i];
                var y1 = signal.Samples[i + 1];
                var x0 = currentX;
                var x1 = currentX + signal.SamplingInterval;
                for (var j = 0; j < factor; j++)
                {
                    if(newX - x0 < x1 - newX)
                        newSamples[currentIndex] = y0;
                    else
                        newSamples[currentIndex] = y1;
                    currentIndex++;
                    newX += newInterval;
                }
                currentX = x1;
            }
            newSignal.Samples = newSamples;
            newSignal.SamplingInterval = newInterval;
            return newSignal;
        }

        /// <summary>
        /// Cubic interpolation. Factor must be >= 2
        /// </summary>
        /// <returns></returns>
        public static Signal InterpolateCubic(Signal signal, uint factor)
        {
            if (factor < 2)
                return signal.Clone();

            var n = signal.Samples.Length;
            var newSignal = signal.Copy();
            var newSamples = MemoryPool.Pool.New<double>(Convert.ToInt32(signal.Samples.Length * factor - factor + 1));
            var time = signal.GetTimeSeries();
            var b = MemoryPool.Pool.New<double>(n);
            var c = MemoryPool.Pool.New<double>(n);
            var d = MemoryPool.Pool.New<double>(n);
            Interp.cubic_nak(n, time, signal.Samples, ref b, ref c, ref d);

            var newInterval = Convert.ToDecimal(signal.SamplingInterval / factor);
            var currentX = Convert.ToDecimal(signal.Start);
            for (var i = 0; i < newSamples.Length; i++)
            {
                newSamples[i] = Interp.spline_eval(n, time, signal.Samples, b, c, d, Convert.ToDouble(currentX));
                currentX += newInterval;                
            }
            newSignal.Samples = newSamples;
            newSignal.SamplingInterval = Convert.ToDouble(newInterval);

            MemoryPool.Pool.RegisterObject(b);
            MemoryPool.Pool.RegisterObject(c);
            MemoryPool.Pool.RegisterObject(d);

            return newSignal;
        }
    }
}
