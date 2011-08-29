using System;
using System.Collections.Generic;

namespace WaveletStudio.SignalGeneration
{
    ///<summary>
    /// Create a signal based on a triangle wave: y(x) = A * (1-4*abs(round(t-0.25)-(t-0.25))) + D, where: 
    /// t   -> (f*x+Phi)
    /// A   -> Amplitude
    /// f   -> Frequency
    /// phi -> Phase
    /// D   -> Offset
    ///</summary>
    [Serializable]
    public class Triangle : CommonSignalBase
    {
        /// <summary>
        /// Name
        /// </summary>
        public override string Name
        {
            get { return "Triangle"; }
        }

        /// <summary>
        /// Generates the signal
        /// </summary>
        /// <returns></returns>
        public override Signal ExecuteSampler()
        {
            var samples = new List<double>();
            var finish = Convert.ToDecimal(GetFinish());
            for (var x = Convert.ToDecimal(Start); x <= finish; x += Convert.ToDecimal(SamplingInterval))
            {
                var t = Frequency * Convert.ToDouble(x) + Phase - 0.25;
                var value = Amplitude * (1 - 4*Math.Abs(Math.Round(t) - t)) + Offset;
                samples.Add(value);
            }
            return new Signal(samples.ToArray())
            {
                SamplingRate = SamplingRate,
                Start = Start,
                Finish = Finish,
                SamplingInterval = SamplingInterval
            };
        }
    }
}
