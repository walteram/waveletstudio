using System;
using WaveletStudio.FFT;

namespace WaveletStudio.Functions
{
    /// <summary>
    /// Common math and statistical operations
    /// </summary>
    public static partial class WaveMath
    {
        /// <summary>
        /// Gets the accumulated energy of a signal
        /// </summary>
        /// <param name="samples"></param>
        /// <returns></returns>
        public static double GetAccumulatedEnergy(double[] samples)
        {
            var energy = 0d;
            foreach (var t in samples)
            {
                energy += GetEnergy(t);
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
        public static double Mode(double[] samples)
        {
            if (samples.Length == 0)
            {
                return 0;
            }
            var sortedSamples = Unique(samples);
            var maxFreq = sortedSamples[0];
            var maxOccurrences = 0;
            foreach (var t in sortedSamples)
            {
                var occurrences = 0;
                foreach (var t1 in samples)
                {
                    if (Math.Abs(t1 - t) < double.Epsilon)
                    {
                        occurrences++;
                    }
                }
                if (occurrences <= maxOccurrences)
                {
                    continue;
                }
                maxOccurrences = occurrences;
                maxFreq = t;
            }
            return (maxFreq);
        }

        /// <summary>
        /// Calculates the mean of an array
        /// </summary>
        /// <param name="samples"></param>
        /// <returns></returns>
        public static double Mean(double[] samples)
        {
            var sum = 0d;
            foreach (var value in samples)
            {
                sum += value;
            }
            return sum / (samples.Length);
        }

        /// <summary>
        /// Removes the repeated values in an array
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private static double[] Unique(double[] array)
        {
            Array.Sort(array);

            var unqCount = 1;
            for (var i = 1; i < array.Length; i++)
                if (Math.Abs(array[i - 1] - array[i]) > double.Epsilon)
                    unqCount++;

            var unq = MemoryPool.Pool.New<double>(unqCount);

            unq[0] = array[0];
            for (int i = 1, j = 1; i < array.Length; i++)
            {
                if (Math.Abs(array[i - 1] - array[i]) <= double.Epsilon) 
                    continue;
                unq[j] = array[i];
                j++;
            }

            return unq;
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
            var result = MemoryPool.Pool.New<double>(x.Length);
            for (var i = 0; i < x.Length; i++)
            {
                result[i] = NormalDistribution(x[i], mean, deviation);
            }
            return result;
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
        public static double[] Convolve(ConvolutionModeEnum convolutionMode, double[] input, double[] filter, bool returnOnlyValid = true, int margin = 0)
        {
            return convolutionMode == ConvolutionModeEnum.Normal ? ConvolveNormal(input, filter, returnOnlyValid, margin) : ConvolveManagedFft(input, filter, returnOnlyValid, margin);
        }

        /// <summary>
        /// Convolves vectors input and filter.
        /// </summary>
        /// <param name="input">The input signal</param>
        /// <param name="filter">The filter</param>
        /// <param name="returnOnlyValid">True to return only the middle of the array</param>
        /// <param name="margin">Margin to be used if returnOnlyValid is set to true</param>
        /// <returns></returns>
        public static double[] ConvolveNormal(double[] input, double[] filter, bool returnOnlyValid = true, int margin = 0)
        {
            if (input.Length < filter.Length)
            {
                var auxSignal = input;
                input = filter;
                filter = auxSignal;
            }
            var result = MemoryPool.Pool.New<double>(input.Length + filter.Length - 1);
            for (var i = 0; i < input.Length; i++)
            {
                for (var j = 0; j < filter.Length; j++)
                {
                    result[i + j] = result[i + j] + input[i] * filter[j];
                }
            }

            if (returnOnlyValid)
            {
                var size = input.Length - filter.Length + 1;
                var padding = (result.Length - size) / 2;
                
                var arraySize = (padding + size - 1 - margin) - (padding + margin) + 1;
                var newResult = MemoryPool.Pool.New<double>(arraySize);
                Array.Copy(result, padding + margin, newResult, 0, arraySize);
                return newResult;
            }
            return result;
        }

        /// <summary>
        /// Convolves vectors input and filter using a managed FFT algorithm.
        /// </summary>
        /// <param name="input">The input signal</param>
        /// <param name="filter">The filter</param>
        /// <param name="returnOnlyValid">True to return only the middle of the array</param>
        /// <param name="margin">Margin to be used if returnOnlyValid is set to true</param>
        /// <returns></returns>
        public static double[] ConvolveManagedFft(double[] input, double[] filter, bool returnOnlyValid = true, int margin = 0)
        {
            if (input.Length < filter.Length)
            {
                var auxSignal = input;
                input = filter;
                filter = auxSignal;
            }
            var realSize = input.Length + filter.Length - 1;
            var size = ((realSize > 0) && ((realSize & (realSize - 1)) == 0) ? realSize : SignalExtension.NextPowerOf2(realSize));
            var inputFFT = MemoryPool.Pool.New<double>(size * 2);
            var filterFFT = MemoryPool.Pool.New<double>(size * 2);
            var ifft = MemoryPool.Pool.New<double>(size * 2);
            
            for (var i = 0; i < input.Length; i++)
            {
                inputFFT[i * 2] = input[i];
            }
            for (var i = 0; i < filter.Length; i++)
            {
                filterFFT[i * 2] = filter[i];
            }

            ManagedFFT.FFT(ref inputFFT, true);
            ManagedFFT.FFT(ref filterFFT, true);
            for (var i = 0; i < ifft.Length; i = i + 2)
            {
                ifft[i] = inputFFT[i]*filterFFT[i] - inputFFT[i + 1]*filterFFT[i + 1];
                ifft[i+1] = (inputFFT[i] * filterFFT[i+1] + inputFFT[i + 1] * filterFFT[i]) * -1;
            }
            ManagedFFT.FFT(ref ifft, false);

            var ifft2 = MemoryPool.Pool.New<double>(size);
            ifft2[0] = ifft[0];
            for (var i = 0; i < ifft2.Length-2; i = i + 1)
            {
                ifft2[i + 1] = ifft[ifft.Length - 2 - i*2];
            }
            int start;
            if (returnOnlyValid)
            {
                size = input.Length - filter.Length + 1;
                var padding = (realSize - size) / 2;
                start = padding + margin;               
                size = input.Length - filter.Length - margin * 2 + 1;
            }
            else
            {
                start = 0;
                size = realSize;
            }
            var result = MemoryPool.Pool.New<double>(size);
            Array.Copy(ifft2, start, result, 0, size);
            return result;
        }

        /// <summary>
        /// Decreases the sampling rate of the input by keeping every odd sample starting with the first sample.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double[] DownSample(double[] input)
        {
            var size = input.Length / 2;
            var result = MemoryPool.Pool.New<double>(size);
            var j = 0;
            for (var i = 0; i < input.Length; i++)
            {
                if (i % 2 == 0)
                    continue;
                result[j] = input[i];
                j++;
            }
            return result;
        }

        /// <summary>
        /// Increases the sampling rate of the input by inserting n-1 zeros between samples. 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double[] UpSample(double[] input)
        {
            if (input.Length == 0)
            {
                return new double[0];
            }
            var size = input.Length * 2;
            var result = MemoryPool.Pool.New<double>(size - 1);
            for (var i = 0; i < input.Length; i++)
            {
                result[i * 2] = input[i];
            }
            return result;
        }        
    }

    public enum ConvolutionModeEnum
    {
        Normal,
        ManagedFft
    }
}
