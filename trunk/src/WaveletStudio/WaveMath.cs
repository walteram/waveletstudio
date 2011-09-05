using System;
using System.Linq;
using ILNumerics;
using ILNumerics.BuiltInFunctions;
using ILNumerics.Misc;

namespace WaveletStudio
{
    /// <summary>
    /// Common math and statistical operations
    /// </summary>
    public static class WaveMath
    {
        /// <summary>
        /// Gets the accumulated energy of a signal
        /// </summary>
        /// <param name="samples"></param>
        /// <returns></returns>
        public static double GetAccumulatedEnergy(ILArray<double> samples)
        {
            if (samples.IsEmpty)
            {
                return 0;
            }
            var energy = 0d;
            for (var i = 0; i < samples.Length; i++)
            {
                energy += GetEnergy(samples.GetValue(i));
            }
            return energy;
        }

        /// <summary>
        /// Gets the energy of a sample
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        public static double GetEnergy(double sample)
        {
            return Math.Pow(Math.Abs(sample), 2);
        }

        /// <summary>
        /// Calculates the mode of an array
        /// </summary>
        /// <param name="samples"></param>
        /// <returns></returns>
        public static double Mode(ILArray<double> samples)
        {
            if (samples.IsEmpty)
            {
                return 0;
            }
            var sortedSamples = Unique(samples);
            var maxFreq = sortedSamples.GetValue(0);
            var maxOccurrences = 0;
            for (var i = 0; i < sortedSamples.Length; i++)
            {
                var occurrences = 0;
                for (var j = 0; j < samples.Length; j++)
                {
                    if (Math.Abs(samples.GetValue(j) - sortedSamples.GetValue(i)) < double.Epsilon)
                    {
                        occurrences++;
                    }
                }                
                if (occurrences <= maxOccurrences)
                {
                    continue;
                }
                maxOccurrences = occurrences;
                maxFreq = sortedSamples.GetValue(i);
            }
            return (maxFreq);
        }

        private static ILArray<double> Unique(ILArray<double> x)
        {
            // 1. Handle empty and singleton cases
            /*
            if (x.IsEmpty)
                return new ILArray<double>();

            if (x.IsScalar)
                return x.C;
            */
            // 2. Sort
            var x1 = x.Values.ToArray();
            Array.Sort(x1);

            // 3. Declarations
            var unqCount = 1;
            for (var i = 1; i < x1.Length; i++)
                if (Math.Abs(x1[i - 1] - x1[i]) > double.Epsilon)
                    unqCount++;

            var unq = ILMemoryPool.Pool.New<double>(unqCount);

            // 4. Unique
            unq[0] = x1[0];
            for (int i = 1, j = 1; i < x1.Length; i++)
            {
                if (Math.Abs(x1[i - 1] - x1[i]) <= double.Epsilon) 
                    continue;
                unq[j] = x1[i];
                j++;
            }

            return new ILArray<double>(unq);
        }

        /// <summary>
        /// Scales a number based in its minimun and maximum
        /// </summary>
        /// <param name="x"></param>
        /// <param name="currentMin"></param>
        /// <param name="currentMax"></param>
        /// <param name="newMin"></param>
        /// <param name="newMax"></param>
        /// <returns></returns>
        public static double Scale(double x, double currentMin, double currentMax, double newMin, double newMax)
        {
            return newMin + (x - currentMin) / (currentMax - currentMin) * (newMax - newMin);
        }

        /// <summary>
        /// Calculates the normal (or Gaussian) distribution of an array
        /// </summary>
        /// <param name="x"></param>
        /// <param name="mean"></param>
        /// <param name="deviation"></param>
        /// <returns></returns>
        public static double[] NormalDistribution(double[] x, double mean, double deviation)
        {
            var y = new double[x.Length];
            for (var i = 0; i < x.Length; i++)
            {
                y[i] = NormalDistribution(x[i], mean, deviation);
            }
            return y;
        }

        /// <summary>
        /// Calculates the normal (or Gaussian) distribution of a sample
        /// </summary>
        /// <param name="x"></param>
        /// <param name="mean"></param>
        /// <param name="deviation"></param>
        /// <returns></returns>
        public static double NormalDistribution(double x, double mean, double deviation)
        {
            return (1/Math.Sqrt(2*Math.PI*deviation)) * Math.Exp(-1*(Math.Pow(x - mean, 2)/(2*deviation)));
        }

        /// <summary>
        /// Calculates the normal density of a sample
        /// </summary>
        /// <param name="x"></param>
        /// <param name="mean"></param>
        /// <param name="deviation"></param>
        /// <returns></returns>
        public static double NormalDensity(double x, double mean, double deviation)
        {
            return Math.Exp(-(Math.Pow((x - mean) / deviation, 2) / 2)) / Math.Sqrt(2 * Math.PI) / deviation;
        }

        /// <summary>
        /// Calculates the standard deviation of an array of samples
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double StandardDeviation(double[] x)
        {
            var sum = 0d;
            var sumOfSqrs = 0d;
            foreach (var sample in x)
            {
                sum += sample;
                sumOfSqrs += Math.Pow(sample, 2);
            }            
            var topSum = (x.Length * sumOfSqrs) - (Math.Pow(sum, 2));
            return Math.Sqrt(topSum / (x.Length * (x.Length - 1)));
        }

        /// <summary>
        /// Convolves vectors input and filter.
        /// </summary>
        /// <param name="convolutionMode">Defines what convolution function should be used</param> 
        /// <param name="input">The input signal</param>
        /// <param name="filter">The filter</param>
        /// <param name="returnOnlyValid">True to return only the middle of the array</param>
        /// <param name="margin">Margin to be used if returnOnlyValid is set to true</param>        
        /// <returns></returns>
        public static ILArray<double> Convolve(ConvolutionModeEnum convolutionMode, ILArray<double> input, ILArray<double> filter, bool returnOnlyValid = true, int margin = 0)
        {
            return convolutionMode == ConvolutionModeEnum.Normal ? 
                        ConvolveNormal(input, filter, returnOnlyValid, margin) : 
                        ConvolveFft(input, filter, returnOnlyValid, margin);
        }

        /// <summary>
        /// Convolves vectors input and filter.
        /// </summary>
        /// <param name="input">The input signal</param>
        /// <param name="filter">The filter</param>
        /// <param name="returnOnlyValid">True to return only the middle of the array</param>
        /// <param name="margin">Margin to be used if returnOnlyValid is set to true</param>
        /// <returns></returns>
        public static ILArray<double> ConvolveNormal(ILArray<double> input, ILArray<double> filter, bool returnOnlyValid = true, int margin = 0)
        {
            if (input.Length < filter.Length)
            {
                var auxSignal = input.C;
                input = filter.C;
                filter = auxSignal;
            }
            var result = new double[input.Length + filter.Length - 1];
            for (var i = 0; i < input.Length; i++)
            {
                for (var j = 0; j < filter.Length; j++)
                {
                    result[i + j] = result[i + j] + input.GetValue(i) * filter.GetValue(j);
                }
            }

            if (returnOnlyValid)
            {
                var size = input.Length - filter.Length + 1;
                var padding = (result.Length - size) / 2;
                return new ILArray<double>(result)[String.Format("{0}:1:{1}", padding + margin, padding + size - 1 - margin)];
            }
            return new ILArray<double>(result);
        }

        public static ILArray<double> ConvolveFft(ILArray<double> input, ILArray<double> filter, bool returnOnlyValid = true, int margin = 0)
        {
            ILMath.FFT = new ILNumerics.Native.ILFFTW3FFT();
            if (input.Length < filter.Length)
            {
                var auxSignal = input.C;
                input = filter.C;
                filter = auxSignal;
            }
            var dim = input.Length + filter.Length - 1;
            var inputNew = ILMath.zeros(1, dim);
            var filterNew = ILMath.zeros(1, dim);
            inputNew["0:1:" + (input.Length - 1)] = input;
            filterNew["0:1:" + (filter.Length - 1)] = filter;            
            var ifft = ILMath.ifft(ILMath.fft(inputNew) * ILMath.fft(filterNew));
            var result = ILMath.real(ifft);

            if (returnOnlyValid)
            {
                var size = input.Length - filter.Length + 1;
                var padding = (result.Length - size) / 2;
                var start = padding + margin;
                var end = padding + size - 1 - margin;
                var newResul = new ILArray<double>(1, end - start + 1);
                newResul["0:1:end"] = result[String.Format("{0}:1:{1}", padding + margin, padding + size - 1 - margin)];
                return newResul;
            }
            return result;
        }

        /// <summary>
        /// Decreases the sampling rate of the input by keeping every odd sample starting with the first sample.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static ILArray<double> DownSample(ILArray<double> input)
        {
            var size = input.Length / 2;
            var result = new double[size];
            var j = 0;
            for (var i = 0; i < input.Length; i++)
            {
                if (i % 2 == 0)
                    continue;
                result[j] = input.GetValue(i);
                j++;
            }
            return new ILArray<double>(result);
        }

        /// <summary>
        /// Increases the sampling rate of the input by inserting n-1 zeros between samples. 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static ILArray<double> UpSample(ILArray<double> input)
        {
            if (input.IsEmpty)
            {
                return ILMath.empty();
            }
            var size = input.Length * 2;
            var result = new double[size - 1];
            for (var i = 0; i < input.Length; i++)
            {
                result[i * 2] = input.GetValue(i);
            }
            return new ILArray<double>(result);
        }
    }

    public enum ConvolutionModeEnum
    {
        Normal,
        Fft
    }
}
