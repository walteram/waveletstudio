using System;
using System.Collections.Generic;
using System.Text;

namespace WaveletStudio
{
    /// <summary>
    /// 1-D Signal
    /// </summary>   
    [Serializable] 
    public class Signal
    {
        /// <summary>
        /// Name of the signal
        /// </summary>
        public string Name { get; set; }

        private double[] _samples;
        /// <summary>
        /// Samples of the signal
        /// </summary>
        public double[] Samples
        {
            get
            {
                return _samples;
            }
            set
            {
                if (_samples != null && value != null)
                {
                    SamplingInterval *= Convert.ToDouble(_samples.Length) / value.Length;
                    if (IsComplex)
                        SamplingInterval *= 2;
                }                
                _samples = value;
            }
        }

        /// <summary>
        /// Gets the number of samples in the signal
        /// </summary>
        public int SamplesCount 
        {
            get { return _samples != null ? _samples.Length : 0; }
        }

        /// <summary>
        /// Complex samples of the signal
        /// </summary>
        public bool IsComplex { get; set; }

        /// <summary>
        /// Start of the signal in the time
        /// </summary>
        public double Start { get; set; }

        /// <summary>
        /// Finish od the signal in the time
        /// </summary>
        public double Finish { get; set; }

        private double _samplingInterval;
        /// <summary>
        /// Gets or sets  the interval of samples (1/SamplingRate)
        /// </summary>
        /// <returns></returns>
        public double SamplingInterval
        {
            get
            {
                return _samplingInterval;
            }
            set
            {
                _samplingInterval = value;
                if (Math.Abs(value - 0d) > double.Epsilon)
                {
                    _samplingRate = Convert.ToInt32(Math.Round(1 / value));
                }
            }
        }

        private int _samplingRate;
        /// <summary>
        /// Sampling rate used on sampling
        /// </summary>
        public int SamplingRate
        {
            get
            {
                return _samplingRate;
            }
            set
            {
                _samplingRate = value;
                if (value == 0)
                    _samplingInterval = 1;
                else
                    _samplingInterval = 1d / value;
            }
        }

        /// <summary>
        /// Custom plotting
        /// </summary>
        public double[] CustomPlot { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Signal()
        {
            Samples = new double[0];
            SamplingRate = 1;
        }

        /// <summary>
        /// Aditional constructor, passing an array of double with the samples of the signal
        /// </summary>
        /// <param name="samples">The samples of the signal</param>
        public Signal(params double[] samples)
        {
            Samples = samples;
            SamplingRate = 1;
        }

        /// <summary>
        /// Aditional constructor, passing an array of double with the samples of the signal and the number of samples per second
        /// </summary>
        /// <param name="samples">The samples of the signal</param>
        /// <param name="samplesPerSecond">Quantity of samples per second</param>
        public Signal(double[] samples, int samplesPerSecond)
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
            var newArray = MemoryPool.Pool.New<double>(length);
            Array.Copy(Samples, newArray, length);
            Samples = newArray;
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
            return ToString(precision, " ");
        }

        /// <summary>
        /// Gets all the samples of the signal separated with the separator parameter
        /// </summary>
        /// <param name="precision"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public string ToString(int precision, string separator)
        {
            if (Samples == null)
                return "";
            var format = "{0:0.";
            for (var i = 0; i < precision; i++)
            {
                format += "0";
            }
            format += "}" + separator;
            var str = new StringBuilder();
            foreach (var t in Samples)
            {
                str.Append(string.Format(System.Globalization.CultureInfo.InvariantCulture, format, t));
            }
            return str.ToString().TrimEnd(separator.ToCharArray());
        }

        /// <summary>
        /// Get all the samples in a key value array pair
        /// </summary>
        /// <returns></returns>
        public IEnumerable<double[]> GetSamplesPair()
        {
            if (Samples == null || Samples.Length == 0)
            {
                return new List<double[]>();
            }
            var samples = new List<double[]>();
            var x = Start;
            foreach (var t in Samples)
            {
                samples.Add(new []{t, x});
                x = Convert.ToDouble(Convert.ToDecimal(x) + Convert.ToDecimal(SamplingInterval));
            }
            return samples;
        }

        /// <summary>
        /// Clones the signal, including the samples
        /// </summary>
        /// <returns></returns>
        public Signal Clone()
        {
            var signal = (Signal)MemberwiseClone();
            signal.Samples = (double[]) Samples.Clone();
            return signal;
        }

        /// <summary>
        /// Clone the signal without cloning the samples
        /// </summary>
        /// <returns></returns>
        public Signal Copy()
        {
            var signal = (Signal)MemberwiseClone();
            signal.Samples = null;
            return signal;
        }
    }
}
