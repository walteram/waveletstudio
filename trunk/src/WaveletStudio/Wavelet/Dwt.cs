/*  Wavelet Studio Signal Processing Library - www.waveletstudio.net
    Copyright (C) 2011, 2012 Walter V. S. de Amorim - The Wavelet Studio Initiative

    Wavelet Studio is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Wavelet Studio is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

using System.Collections.Generic;
using WaveletStudio.Functions;

namespace WaveletStudio.Wavelet
{
    /// <summary>
    /// Discreet Wavelet Transform and its inverse
    /// </summary>
    public static class DWT
    {
        /// <summary>
        /// Multilevel 1-D Discreete Wavelet Transform
        /// </summary>
        /// <param name="signal">The signal. Example: new Signal(5, 6, 7, 8, 1, 2, 3, 4)</param>
        /// <param name="motherWavelet">The mother wavelet to be used. Example: CommonMotherWavelets.GetWaveletFromName("DB4")</param>
        /// <param name="level">The depth-level to perform the DWT</param>
        /// <param name="extensionMode">Signal extension mode</param>
        /// <param name="convolutionMode">Defines what convolution function should be used</param>
        /// <returns></returns>
        public static List<DecompositionLevel> ExecuteDWT(Signal signal, MotherWavelet motherWavelet, int level, SignalExtension.ExtensionMode extensionMode = SignalExtension.ExtensionMode.SymmetricHalfPoint, ConvolutionModeEnum convolutionMode = ConvolutionModeEnum.ManagedFFT)
        {
            var levels = new List<DecompositionLevel>();

            var approximation = (double[])signal.Samples.Clone();
            var details = (double[])signal.Samples.Clone();

            var realLength = signal.Samples.Length;
            for (var i = 1; i <= level; i++)
            {
                var extensionSize = motherWavelet.Filters.DecompositionLowPassFilter.Length - 1;
                
                approximation = SignalExtension.Extend(approximation, extensionMode, extensionSize);
                details = SignalExtension.Extend(details, extensionMode, extensionSize);

                approximation = WaveMath.Convolve(convolutionMode, approximation, motherWavelet.Filters.DecompositionLowPassFilter);
                approximation = WaveMath.DownSample(approximation);

                details = WaveMath.Convolve(convolutionMode, details, motherWavelet.Filters.DecompositionHighPassFilter);
                details = WaveMath.DownSample(details);

                realLength = realLength / 2;

                levels.Add(new DecompositionLevel
                               {
                                   Signal = signal,
                                   Index = i - 1,
                                   Approximation = approximation,
                                   Details = details
                               });
                details = (double[]) approximation.Clone();
            }
            return levels;
        }
        
        /// <summary>
        /// Multilevel inverse discrete 1-D wavelet transform
        /// </summary>
        /// <param name="decompositionLevels">The decomposition levels of the DWT</param>
        /// <param name="motherWavelet">The mother wavelet to be used. Example: CommonMotherWavelets.GetWaveletFromName("DB4") </param>
        /// <param name="level">The depth-level to perform the DWT</param>
        /// <param name="convolutionMode">Defines what convolution function should be used</param>
        /// <returns></returns>
        public static double[] ExecuteIDWT(List<DecompositionLevel> decompositionLevels, MotherWavelet motherWavelet, int level = 0, ConvolutionModeEnum convolutionMode = ConvolutionModeEnum.ManagedFFT)
        {
            if (level == 0 || level > decompositionLevels.Count)
            {
                level = decompositionLevels.Count;
            }
            if (level <= 0)
                return null;
            var approximation = (double[])decompositionLevels[level-1].Approximation.Clone();
            var details = (double[]) decompositionLevels[level - 1].Details.Clone();

            for (var i = level - 1; i >= 0; i--)
            {
                approximation = WaveMath.UpSample(approximation);
                approximation = WaveMath.Convolve(convolutionMode, approximation, motherWavelet.Filters.ReconstructionLowPassFilter, true, -1);

                details = WaveMath.UpSample(details);
                details = WaveMath.Convolve(convolutionMode, details, motherWavelet.Filters.ReconstructionHighPassFilter, true, -1);

                //sum approximation with details
                approximation = WaveMath.Add(approximation, details);

                if (i <= 0) 
                    continue;
                if (approximation.Length > decompositionLevels[i-1].Details.Length)
                {
                    approximation = SignalExtension.Deextend(approximation, decompositionLevels[i - 1].Details.Length);
                }

                details = (double[]) decompositionLevels[i - 1].Details.Clone();
            }

            return approximation;
        }        
    }
}
