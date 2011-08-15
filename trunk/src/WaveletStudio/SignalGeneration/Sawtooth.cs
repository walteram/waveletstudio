using System;
using System.Collections.Generic;

namespace WaveletStudio.SignalGeneration
{
    ///<summary>
    /// Create a signal based on a sawtooth wave: y(x) = A * 2*((f*x+phi) - floor((f*x+phi) +0.5)) + D, where: 
    /// A   -> Amplitude
    /// f   -> Frequency
    /// phi -> Phase
    /// D   -> Offset
    ///</summary>
    public class Sawtooth : CommonSignalBase
    {
        /// <summary>
        /// Name
        /// </summary>
        public override string Name
        {
            get { return "Sawtooth"; }
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
                var value = Amplitude * 2 * ((Frequency * Convert.ToDouble(x) + Phase) - Math.Floor(Frequency * Convert.ToDouble(x) + Phase + .5) ) + Offset;
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
