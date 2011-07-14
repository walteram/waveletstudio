using System;
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
        /// Level index in DWT
        /// </summary>
        public int Index { get; set; }
        
        /// <summary>
        /// Approximation coefficients
        /// </summary>
        public ILArray<double> Approximation { get; set; }
        
        /// <summary>
        /// Detais coefficients
        /// </summary>
        public ILArray<double> Details { get; set; }

        /// <summary>
        /// Signal from which this level was created
        /// </summary>
        public Signal Signal { get; set; }

        /// <summary>
        /// TODO: In progress.
        /// </summary>
        public class Disturbance
        {
            public int Start;
            public int Finish;
            public int Length;

            public int SignalStart;
            public int SignalFinish;
            public int SignalLength;
        }

        /// <summary>
        /// TODO: In progress. This method is not full tested...
        /// </summary>
        /// <returns></returns>
        public List<Disturbance> GetDisturbances(double threshold = 0.1)
        {
            int? start = null;
            int? finish = null;
            var existsZero = false;
            var details = Details;
            var disturbances = new List<Disturbance>();
            var mode = WaveLibMath.Mode(details);
            for (var i = 0; i < details.Length; i++)
            {
                if (i==86)
                {
                    var a = 1;
                }
                var sampleValue = details.GetValue(i);
                var abs = Math.Abs(Math.Abs(sampleValue) - Math.Abs(mode));
                if (abs > Math.Abs(sampleValue) * threshold && start == null )
                {
                    start = i;
                }
                else if (abs <= Math.Abs(sampleValue) * threshold && start != null)
                {
                    if (i == details.Length - 1 || Index == 0)
                    {
                        finish = i - 1;
                    }
                    else
                    {
                        var nextSampleValue = details.GetValue(i + 1);
                        var nextAbs = Math.Abs(nextSampleValue - mode);
                        if (nextAbs <= nextSampleValue * threshold)
                        {
                            finish = i - 1;
                        }
                    }
                }
                else if (abs <= sampleValue * threshold && start == null)
                {
                    existsZero = true;
                }
                if (start != null && finish != null)
                {
                    var length = finish.Value - start.Value + 1;
                    var signalStart = start.Value * Math.Pow(2, Index + 1) + 1;
                    var signalFinish = finish.Value * Math.Pow(2, Index + 1) + (Math.Pow(2, Index+1) - 2);
                    var signalLength = signalFinish - signalStart + 1;
                    disturbances.Add(new Disturbance
                                         {
                                             Start = start.Value,
                                             Finish = finish.Value,
                                             Length = length,

                                             SignalStart = (int) signalStart,
                                             SignalFinish = (int) signalFinish,
                                             SignalLength = (int) signalLength
                                         });
                    start = null;
                    finish = null;
                }
            }
            return disturbances;
        }
    }
}
