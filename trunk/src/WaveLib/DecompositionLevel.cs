using ILNumerics;

namespace WaveletStudio.WaveLib
{
    /// <summary>
    /// Used as the return of a DWT operation and as the input of a IDWT operation
    /// </summary>
    public class DecompositionLevel
    {
        /// <summary>
        /// Approximation coefficients
        /// </summary>
        public ILArray<double> Approximation { get; set; }
        
        /// <summary>
        /// Detais coefficients
        /// </summary>
        public ILArray<double> Details { get; set; }        
    }
}
