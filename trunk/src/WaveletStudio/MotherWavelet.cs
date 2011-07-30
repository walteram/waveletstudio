using System;

namespace WaveletStudio
{
    /// <summary>
    /// Mother wavelet base class
    /// </summary>
    public class MotherWavelet
    {
        /// <summary>
        /// Name of the mother wavelet
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The scaling function of the mother wavelet, used to calculate the filters
        /// </summary>
        public double[] ScalingFilter { get; private set; }

        private FiltersStruct _filters;

        /// <summary>
        /// Constructior using the name and the scaling filter
        /// </summary>
        /// <param name="name">Name of the mother wavelet</param>
        /// <param name="scalingFilter">The scaling function of the mother wavelet</param>
        public MotherWavelet(string name, double[] scalingFilter)
        {
            Name = name;
            ScalingFilter = scalingFilter;
        }

        /// <summary>
        /// Constructior using the name and the scaling filter
        /// </summary>
        /// <param name="scalingFilter">The scaling function of the mother wavelet</param>
        public MotherWavelet(double[] scalingFilter)
        {
            ScalingFilter = scalingFilter; 
        }
        
        /// <summary>
        /// Decomposition and Reconstruction filters
        /// </summary>
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

        /// <summary>
        /// Decomposition and Reconstruction filters base-type
        /// </summary>
        public struct FiltersStruct
        {
            /// <summary>
            /// Decomposition Low-pass Filter
            /// </summary>
            public double[] DecompositionLowPassFilter;
            /// <summary>
            /// Decomposition High-pass Filter
            /// </summary>
            public double[] DecompositionHighPassFilter;
            /// <summary>
            /// Reconstruction Low-pass Filter
            /// </summary>
            public double[] ReconstructionLowPassFilter;
            /// <summary>
            /// Reconstruction High-pass Filter
            /// </summary>
            public double[] ReconstructionHighPassFilter;
        }

        /// <summary>
        /// Calculates the reconstruction and decomposition filters 
        /// </summary>
        public void CalculateFilters()
        {
            var filterLength = ScalingFilter.Length;
            const double sqrt2 = 1.4142135623730951; //Math.Sqrt(2)
           

            //Calculating Lo_R
            _filters.ReconstructionLowPassFilter = new double[filterLength];
            for (var i = 0; i < filterLength; i++)
            {
                _filters.ReconstructionLowPassFilter[i] = ScalingFilter[i] * sqrt2;
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

        /// <summary>
        /// Loads the mother-wavelet by its name. Just a link to CommonMotherWavelets.GetWaveletFromName.
        /// </summary>
        /// <param name="name">Mother-wavelet name</param>
        /// <returns></returns>
        public static MotherWavelet LoadFromName(string name)
        {
            return CommonMotherWavelets.GetWaveletFromName(name);
        }
    }
}
