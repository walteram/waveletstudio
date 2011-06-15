using System;
using System.Collections.Generic;
using ILNumerics;
using ILNumerics.BuiltInFunctions;

namespace WaveLib
{
    /// <summary>
    /// Provides DWT (Discreete Wavelet Transform) functions
    /// </summary>
    public class Dwt
    {
        /// <summary>
        /// Performs the DWT in a signal
        /// </summary>
        /// <param name="signal">The signal. Example: new Signal(1, 2, 3, 4, 5, 6, 7, 8)</param>
        /// <param name="motherWavelet">The mother wavelet. Example: MotherWavelet.LoadFromName("db4")</param>
        /// <param name="level">Level of decomposition.</param>
        /// <param name="extensionMode">Extension mode for treatment of border distortion</param>
        /// <returns></returns>
        public static List<DecompositionLevel> ExecuteDwt(Signal signal, MotherWavelet motherWavelet, int level, SignalExtension.ExtensionMode extensionMode = SignalExtension.ExtensionMode.SymmetricHalfPoint)
        {
            var levels = new List<DecompositionLevel>();
            
            var approximation = signal.Points.C;
            var details = signal.Points.C;
            
            for (var i = 1; i <= level; i++)
            {
                var extensionSize = motherWavelet.Filters.DecompositionLowPassFilter.Length - 1;
                var finalSize = (int)Math.Floor((approximation.Length - 1) / 2d) + (motherWavelet.Filters.DecompositionLowPassFilter.Length / 2);

                approximation = SignalExtension.Extend(approximation, extensionMode, extensionSize);
                details = SignalExtension.Extend(details, extensionMode, extensionSize);

                approximation = Convolve(approximation, motherWavelet.Filters.DecompositionLowPassFilter);
                approximation = DownSample(approximation);

                details = Convolve(details, motherWavelet.Filters.DecompositionHighPassFilter);
                details = DownSample(details);
                
                
                levels.Add(new DecompositionLevel
                               {
                                   Approximation = approximation,
                                   Detail = details
                               });
                details = approximation.C;
            }
            return levels;
        }
        
        /// <summary>
        /// Performs the Inverse-DWT in a signal
        /// </summary>
        /// <param name="decompositionLevels">Decomposition levels to reconstruct signal</param>
        /// <param name="motherWavelet">The mother wavelet. Example: MotherWavelet.LoadFromName("db4")</param>
        /// <param name="level">Level of decomposition.</param>
        /// <returns></returns>
        public static ILArray<double> ExecuteIDwt(List<DecompositionLevel> decompositionLevels, MotherWavelet motherWavelet, int level = 0)
        {
            if (level == 0 || level > decompositionLevels.Count)
            {
                level = decompositionLevels.Count;
            }
            var approximation = decompositionLevels[level-1].Approximation.C;
            var details = decompositionLevels[level - 1].Detail.C;

            for (var i = level - 1; i >= 0; i--)
            {
                approximation = UpSample(approximation);
                approximation = Convolve(approximation, motherWavelet.Filters.ReconstructionLowPassFilter, true, -1);

                details = UpSample(details);
                details = Convolve(details, motherWavelet.Filters.ReconstructionHighPassFilter, true, -1);

                //sum approximation with details
                approximation = ILMath.add(approximation, details);

                if (i <= 0) 
                    continue;
                if (approximation.Length > decompositionLevels[i-1].Detail.Length)
                {
                    approximation = SignalExtension.Deextend(approximation, decompositionLevels[i - 1].Detail.Length);
                }
                details = decompositionLevels[i - 1].Detail;                
            }
            return approximation;
        }

        public static ILArray<double> Convolve(ILArray<double> input, ILArray<double> filter, bool returnOnlyValid = true, int margin = 0)
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
                return new ILArray<double>(result)[string.Format("{0}:1:{1}", padding + margin, padding + size - 1 - margin)];
            }
            return new ILArray<double>(result);
        }
        
        public static ILArray<double> DownSample(ILArray<double> input)
        {
            var size = input.Length/2;
            var result = new double[size];
            var j = 0;
            for (var i = 0; i < input.Length; i++)
            {
                if (i%2 == 0) 
                    continue;
                result[j] = input.GetValue(i);
                j++;
            }
            return new ILArray<double>(result);
        }

        public static ILArray<double> UpSample(ILArray<double> input)
        {
            if (input.IsEmpty)
            {
                return ILMath.empty();
            }
            var size = input.Length * 2;
            var result = new double[size-1];
            for (var i = 0; i < input.Length; i++)
            {
                result[i*2] = input.GetValue(i);
            }
            return new ILArray<double>(result);
        }

        
    }
}
