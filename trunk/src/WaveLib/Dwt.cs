using System.Collections.Generic;
using ILNumerics;
using ILNumerics.BuiltInFunctions;

namespace WaveLib
{
    public static class Dwt
    {        
        public static List<DecompositionLevel> ExecuteDwt(Signal signal, MotherWavelet motherWavelet, int level, SignalExtension.ExtensionMode extensionMode = SignalExtension.ExtensionMode.SymmetricHalfPoint)
        {
            var levels = new List<DecompositionLevel>();
            
            var approximation = signal.Points.C;
            var details = signal.Points.C;
            
            for (var i = 1; i <= level; i++)
            {
                var extensionSize = motherWavelet.Filters.DecompositionLowPassFilter.Length - 1;
                
                approximation = SignalExtension.Extend(approximation, extensionMode, extensionSize);
                details = SignalExtension.Extend(details, extensionMode, extensionSize);

                approximation = Convolve(approximation, motherWavelet.Filters.DecompositionLowPassFilter);
                approximation = DownSample(approximation);

                details = Convolve(details, motherWavelet.Filters.DecompositionHighPassFilter);
                details = DownSample(details);
                
                
                levels.Add(new DecompositionLevel
                               {
                                   Approximation = approximation,
                                   Details = details
                               });
                details = approximation.C;
            }
            return levels;
        }

        public static ILArray<double> ExecuteIDwt(List<DecompositionLevel> decompositionLevels, MotherWavelet motherWavelet, int level = 0, SignalExtension.ExtensionMode extensionMode = SignalExtension.ExtensionMode.SymmetricHalfPoint)
        {
            if (level == 0 || level > decompositionLevels.Count)
            {
                level = decompositionLevels.Count;
            }
            var approximation = decompositionLevels[level-1].Approximation.C;
            var details = decompositionLevels[level - 1].Details.C;

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
                if (approximation.Length > decompositionLevels[i-1].Details.Length)
                {
                    approximation = SignalExtension.Deextend(approximation, decompositionLevels[i - 1].Details.Length);
                }
                details = decompositionLevels[i - 1].Details;
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
