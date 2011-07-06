using System.Collections.Generic;
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

        /// <summary>
        /// TODO: In progress.
        /// </summary>
        public class Disturbance
        {
            public double Start;
            public double Finish;
            public double Length;
            public double[] Samples;
        }

        /// <summary>
        /// TODO: In progress. This method is not full tested...
        /// </summary>
        /// <returns></returns>
        public List<Disturbance> GetDisturbances()
        {
            var disturbances = new List<Disturbance>();
            int? start = null;
            int? finish = null;

            for (var i = 0; i < Details.Length; i++)
            {
                if (Details.GetValue(i) != 0 && start == null)
                {
                    start = i;
                }
                if (Details.GetValue(i) == 0 && start != null && i < Details.Length - 1 && Details[i + 1] == 0 && i > 0 || Details[i] != 0 && i == Details.Length - 1 && start != null)
                {
                    if (i == Details.Length-1)
                    {
                        finish = i;   
                    }
                    else
                    {
                        finish = i - 1;
                    }
                }
                if (start != null && finish != null)
                {
                    disturbances.Add(new Disturbance
                                         {
                                             Start = start.Value,
                                             Finish = finish.Value,
                                             Length = finish.Value - start.Value + 1                                             
                                         });
                    start = finish = null;
                }
            }
            return disturbances;
        }
    }
}
