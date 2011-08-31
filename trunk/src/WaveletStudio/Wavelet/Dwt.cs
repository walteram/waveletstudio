using System.Collections.Generic;
using ILNumerics;
using ILNumerics.BuiltInFunctions;

namespace WaveletStudio.Wavelet
{
    /// <summary>
    /// Discreet Wavelet Transform and its inverse
    /// </summary>
    public static class Dwt
    {        
        /// <summary>
        /// Multilevel 1-D Discreete Wavelet Transform
        /// </summary>
        /// <param name="signal">The signal. Example: new Signal(5, 6, 7, 8, 1, 2, 3, 4)</param>
        /// <param name="motherWavelet">The mother wavelet to be used. Example: CommonMotherWavelets.GetWaveletFromName("DB4")</param>
        /// <param name="level">The depth-level to perform the DWT</param>
        /// <param name="extensionMode">Signal extension mode</param>
        /// <returns></returns>
        public static List<DecompositionLevel> ExecuteDwt(Signal signal, MotherWavelet motherWavelet, int level, SignalExtension.ExtensionMode extensionMode = SignalExtension.ExtensionMode.SymmetricHalfPoint)
        {
            var levels = new List<DecompositionLevel>();
            
            var approximation = signal.Samples.C;
            var details = signal.Samples.C;
            var realLength = signal.Samples.Length;
            for (var i = 1; i <= level; i++)
            {
                var extensionSize = motherWavelet.Filters.DecompositionLowPassFilter.Length - 1;
                
                approximation = SignalExtension.Extend(approximation, extensionMode, extensionSize);
                details = SignalExtension.Extend(details, extensionMode, extensionSize);

                approximation = WaveMath.Convolve(approximation, motherWavelet.Filters.DecompositionLowPassFilter);
                approximation = WaveMath.DownSample(approximation);

                details = WaveMath.Convolve(details, motherWavelet.Filters.DecompositionHighPassFilter);
                details = WaveMath.DownSample(details);

                realLength = realLength / 2;

                levels.Add(new DecompositionLevel
                               {
                                   Signal = signal,
                                   Index = i - 1,
                                   Approximation = approximation,
                                   Details = details
                               });
                details = approximation.C;
            }
            return levels;
        }

        /// <summary>
        /// Multilevel inverse discrete 1-D wavelet transform
        /// </summary>
        /// <param name="decompositionLevels">The decomposition levels of the DWT</param>
        /// <param name="motherWavelet">The mother wavelet to be used. Example: CommonMotherWavelets.GetWaveletFromName("DB4") </param>
        /// <param name="level">The depth-level to perform the DWT</param>
        /// <returns></returns>
        public static ILArray<double> ExecuteIDwt(List<DecompositionLevel> decompositionLevels, MotherWavelet motherWavelet, int level = 0)
        {
            if (level == 0 || level > decompositionLevels.Count)
            {
                level = decompositionLevels.Count;
            }
            var approximation = decompositionLevels[level-1].Approximation.C;
            var details = decompositionLevels[level - 1].Details.C;

            for (var i = level - 1; i >= 0; i--)
            {
                approximation = WaveMath.UpSample(approximation);
                approximation = WaveMath.Convolve(approximation, motherWavelet.Filters.ReconstructionLowPassFilter, true, -1);

                details = WaveMath.UpSample(details);
                details = WaveMath.Convolve(details, motherWavelet.Filters.ReconstructionHighPassFilter, true, -1);

                //sum approximation with details
                approximation = ILMath.add(approximation, details);

                if (i <= 0) 
                    continue;
                if (approximation.Length > decompositionLevels[i-1].Details.Length)
                {
                    approximation = SignalExtension.Deextend(approximation, decompositionLevels[i - 1].Details.Length);
                }
                details = decompositionLevels[i - 1].Details;
            }

            return approximation;
        }
    }
}
