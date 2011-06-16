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

            var extensionSize = motherWavelet.Filters.DecompositionLowPassFilter.Length - 1;
            for (var i = 1; i <= level; i++)
            {
                approximation = SignalExtension.Extend(approximation, extensionMode, extensionSize);
                approximation = MathUtils.Convolve(approximation, motherWavelet.Filters.DecompositionLowPassFilter);
                approximation = MathUtils.DownSample(approximation);
                details = SignalExtension.Extend(details, extensionMode, extensionSize);
                details = MathUtils.Convolve(details, motherWavelet.Filters.DecompositionHighPassFilter);
                details = MathUtils.DownSample(details);                
                
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
                approximation = MathUtils.UpSample(approximation);
                approximation = MathUtils.Convolve(approximation, motherWavelet.Filters.ReconstructionLowPassFilter, true, -1);

                details = MathUtils.UpSample(details);
                details = MathUtils.Convolve(details, motherWavelet.Filters.ReconstructionHighPassFilter, true, -1);

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
    }
}
