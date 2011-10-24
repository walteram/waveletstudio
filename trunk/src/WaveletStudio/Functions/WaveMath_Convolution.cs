using System;
using WaveletStudio.FFT;

namespace WaveletStudio.Functions
{
    public static partial class WaveMath
    {
        /// <summary>
        /// Convolves vectors input and filter.
        /// </summary>
        /// <param name="convolutionMode">Defines what convolution function should be used</param> 
        /// <param name="input">The input signal</param>
        /// <param name="filter">The filter</param>
        /// <param name="returnOnlyValid">True to return only the middle of the array</param>
        /// <param name="margin">Margin to be used if returnOnlyValid is set to true</param>
        /// <param name="mode">FFT mode</param>
        /// <returns></returns>
        public static double[] Convolve(ConvolutionModeEnum convolutionMode, double[] input, double[] filter, bool returnOnlyValid = true, int margin = 0, ManagedFFTModeEnum mode = ManagedFFTModeEnum.UseLookupTable)
        {
            return convolutionMode == ConvolutionModeEnum.Normal ? ConvolveNormal(input, filter, returnOnlyValid, margin) : ConvolveManagedFFT(input, filter, returnOnlyValid, margin, mode);
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
        /// <param name="mode">Mode</param>
        /// <returns></returns>
        public static double[] ConvolveManagedFFT(double[] input, double[] filter, bool returnOnlyValid = true, int margin = 0, ManagedFFTModeEnum mode = ManagedFFTModeEnum.UseLookupTable)
        {
            if (input == null || filter == null)
                return null;
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

            ManagedFFT.FFT(ref inputFFT, true, mode);
            ManagedFFT.FFT(ref filterFFT, true, mode);
            for (var i = 0; i < ifft.Length; i = i + 2)
            {
                ifft[i] = inputFFT[i] * filterFFT[i] - inputFFT[i + 1] * filterFFT[i + 1];
                ifft[i + 1] = (inputFFT[i] * filterFFT[i + 1] + inputFFT[i + 1] * filterFFT[i]) * -1;
            }
            ManagedFFT.FFT(ref ifft, false, mode);

            var ifft2 = MemoryPool.Pool.New<double>(size);
            ifft2[0] = ifft[0];
            for (var i = 0; i < ifft2.Length - 2; i = i + 1)
            {
                ifft2[i + 1] = ifft[ifft.Length - 2 - i * 2];
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
    }

    /// <summary>
    /// Convolution mode
    /// </summary>
    public enum ConvolutionModeEnum
    {
        /// <summary>
        /// Normal
        /// </summary>
        Normal,
        /// <summary>
        /// FFT with the managed library
        /// </summary>
        ManagedFFT
    }
}
