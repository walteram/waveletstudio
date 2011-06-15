using System;

namespace WaveLib
{
    public class MotherWavelet
    {
        public string Name { get; set; }

        public bool SupportCwt { get; set; }

        public bool SupportDwt { get; set; }

        public double[] ScalingFilter { get; private set; }

        public Func<int, double> Function {get; set; }

        private FiltersStruct _filters;

        public MotherWavelet(string name, double[] scalingFilter)
        {
            Name = name;
            ScalingFilter = scalingFilter;
        }
        
        public MotherWavelet(double[] scalingFilter)
        {
            ScalingFilter = scalingFilter; 
        }
        
        public FiltersStruct Filters
        {
            get 
            {
                if (_filters.DecompositionHighPassFilter == null)
                {
                    CalculateFilters();
                }
                return _filters; 
            }
        }

        public struct FiltersStruct
        {
            public double[] DecompositionLowPassFilter;
            public double[] DecompositionHighPassFilter;
            public double[] ReconstructionLowPassFilter;
            public double[] ReconstructionHighPassFilter;
        }

        private void CalculateFilters()
        {
            var filterLength = ScalingFilter.Length;

            //Calculating Lo_R
            _filters.ReconstructionLowPassFilter = new double[filterLength];
            for (var i = 0; i < filterLength; i++)
            {
                _filters.ReconstructionLowPassFilter[i] = ScalingFilter[i] / (Math.Sqrt(2) / 2F);
            }

            //Calculating Lo_D  (inverse of Lo_R)
            _filters.DecompositionLowPassFilter = new double[filterLength];
            _filters.ReconstructionLowPassFilter.CopyTo(_filters.DecompositionLowPassFilter, 0);
            Array.Reverse(_filters.DecompositionLowPassFilter);

            //Calculating Hi_R (qmf(Lo_R))
            var k = 0;
            _filters.ReconstructionHighPassFilter = new double[filterLength];
            for (var i = filterLength - 1; i >= 0; i--)
            {
                _filters.ReconstructionHighPassFilter[k] = _filters.ReconstructionLowPassFilter[i];
                if (k % 2 != 0)
                {
                    _filters.ReconstructionHighPassFilter[k] *= -1;
                }
                k++;
            }

            //Calculating Hi_D  (inverse of Hi_R)
            _filters.DecompositionHighPassFilter = new double[filterLength];
            _filters.ReconstructionHighPassFilter.CopyTo(_filters.DecompositionHighPassFilter, 0);
            Array.Reverse(_filters.DecompositionHighPassFilter);
        }

        public static MotherWavelet LoadFromName(string name)
        {
            return CommonMotherWavelets.GetWaveletFromName(name);
        }
    }
}
