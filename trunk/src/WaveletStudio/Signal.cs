using System;
using System.Collections.Generic;
using System.Text;
using ILNumerics;

namespace WaveletStudio
{
    /// <summary>
    /// 1-D Signal
    /// </summary>    
    public class Signal
    {
        /// <summary>
        /// Samples of the signal
        /// </summary>
        public ILArray<double> Samples { get; set; }

        /// <summary>
        /// Sampling Rate
        /// </summary>
        public int SamplingRate { get; set; }

        /// <summary>
        /// Start of the signal in the time
        /// </summary>
        public double Start { get; set; }

        /// <summary>
        /// Finish od the signal in the time
        /// </summary>
        public double Finish { get; set; }

        /// <summary>
        /// Interval in time between samples
        /// </summary>
        public double SamplingInterval { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Signal()
        {
            Samples = new ILArray<double>();
            SamplingRate = 1;
        }

        /// <summary>
        /// Aditional constructor, passing an array of double with the samples of the signal
        /// </summary>
        /// <param name="samples">The samples of the signal</param>
        public Signal(params double[] samples)
        {
            Samples = new ILArray<double>(samples);
            SamplingRate = 1;
        }

        /// <summary>
        /// Aditional constructor, passing an array of double with the samples of the signal and the number of samples per second
        /// </summary>
        /// <param name="samples">The samples of the signal</param>
        /// <param name="samplesPerSecond">Quantity of samples per second</param>
        public Signal(double[] samples, int samplesPerSecond)
        {
            Samples = new ILArray<double>(samples);
            SamplingRate = samplesPerSecond;
        }

        /// <summary>
        /// Aditional constructor, passing an array of double with the samples of the signal and the number of samples per second
        /// </summary>
        /// <param name="samples">The samples of the signal</param>
        /// <param name="samplesPerSecond">Quantity of samples per second</param>
        public Signal(ILArray<double> samples, int samplesPerSecond)
        {
            Samples = samples;
            SamplingRate = samplesPerSecond;
        }

        /// <summary>
        /// Informs that size of signal is a power of 2.
        /// </summary>
        /// <returns></returns>
        public bool LengthIsPowerOf2()
        {
            return IsPowerOf2(Samples.Length);
        }

        /// <summary>
        /// Resizes the signal until its length be a power of 2.
        /// </summary>
        /// <returns></returns>
        public void MakeLengthPowerOfTwo()
        {
            if (LengthIsPowerOf2() || Samples.Length == 0)
            {
                return;
            }
            var length = Samples.Length;
            while (!(IsPowerOf2(length)) && length > 0)
            {
                length--;
            }
            
            Samples = Samples[string.Format("0:1:{0}", length-1)];
        }

        private bool IsPowerOf2(int x)
        {
            return (x != 0) && ((x & (x - 1)) == 0);
        }

        /// <summary>
        /// Gets all the samples of the signal separated with a space
        /// </summary>
        /// <returns></returns>
        public new string ToString()
        {
            return ToString(3);
        }

        /// <summary>
        /// Gets all the samples of the signal separated with a space
        /// </summary>
        /// <param name="precision"></param>
        /// <returns></returns>
        public string ToString(int precision)
        {
            var format = "{0:0.";
            for (var i = 0; i < precision; i++)
            {
                format += "0";
            }
            format += "} ";
            var str = new StringBuilder();
            for (var i = 0; i < Samples.Length; i++)
            {
                str.Append(string.Format(System.Globalization.CultureInfo.InvariantCulture, format, Samples.GetValue(i)));
            }
            return str.ToString().Trim();
        }

        /// <summary>
        /// Get all the samples in a key value array pair
        /// </summary>
        /// <returns></returns>
        public IEnumerable<double[]> GetSamplesPair()
        {
            if (Samples.IsEmpty)
            {
                return new List<double[]>();
            }
            var samples = new List<double[]>();
            var x = Start;
            var interval = SamplingInterval;
            if (interval == 0)
            {
                interval = 1;
            }
            for (var i = 0; i < Samples.Length; i++)
            {
                samples.Add(new []{Samples.GetValue(i), x});
                x = Convert.ToDouble(Convert.ToDecimal(x) + Convert.ToDecimal(interval));
            }
            return samples;
        }

        public Signal Clone()
        {
            var newSignal = (Signal) MemberwiseClone();
            newSignal.Samples = (ILArray<double>) Samples.Clone();
            return newSignal;
        }
    }
}
