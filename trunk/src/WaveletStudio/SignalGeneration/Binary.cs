using System;
using System.Collections.Generic;

namespace WaveletStudio.SignalGeneration
{
    ///<summary>
    /// Create a binary wave: y(x) = 0, 1, 0, 1, 0, 1....
    ///</summary>
    internal class Binary : CommonSignalBase
    {
        /// <summary>
        /// Not used in this signal
        /// </summary>
        protected new double Frequency { get; set; }

        /// <summary>
        /// Not used in this signal
        /// </summary>
        protected new double Phase { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public override string Name
        {
            get { return "Binary"; }
        }

        /// <summary>
        /// Generates the signal
        /// </summary>
        /// <returns></returns>
        public override Signal ExecuteSampler()
        {
            var samples = new List<double>();
            var finish = Convert.ToDecimal(GetFinish());
            var lastValue = 1;
            for (var x = Convert.ToDecimal(Start); x <= finish; x += Convert.ToDecimal(SamplingInterval))
            {
                lastValue = lastValue == 1 ? 0 : 1;
                var value = Amplitude*lastValue + Offset;
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
