using System;
using ILNumerics;

namespace WaveLib
{
    public class DecompositionLevel
    {
        /// <summary>
        /// Approximation coefficients of this decomposition level
        /// </summary>
        public ILArray<double> Approximation { get; set; }

        /// <summary>
        /// Detail coefficients of this decomposition level
        /// </summary>
        public ILArray<double> Detail { get; set; }        
    }
}
