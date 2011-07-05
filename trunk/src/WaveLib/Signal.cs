using ILNumerics;

namespace WaveletStudio.WaveLib
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
        /// Quantity of samples per second. It will be used in the future
        /// </summary>
        public int SamplesPerSecond { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Signal()
        {
            Samples = new ILArray<double>();
            SamplesPerSecond = 1;
        }

        /// <summary>
        /// Aditional constructor, passing an array of double with the samples of the signal
        /// </summary>
        /// <param name="samples">The samples of the signal</param>
        public Signal(params double[] samples)
        {
            Samples = new ILArray<double>(samples);
            SamplesPerSecond = 1;
        }

        /// <summary>
        /// Aditional constructor, passing an array of double with the samples of the signal and the number of samples per second
        /// </summary>
        /// <param name="samples">The samples of the signal</param>
        /// <param name="samplesPerSecond">Quantity of samples per second</param>
        public Signal(double[] samples, int samplesPerSecond)
        {
            Samples = new ILArray<double>(samples);
            SamplesPerSecond = samplesPerSecond;
        }

        /// <summary>
        /// Aditional constructor, passing an array of double with the samples of the signal and the number of samples per second
        /// </summary>
        /// <param name="samples">The samples of the signal</param>
        /// <param name="samplesPerSecond">Quantity of samples per second</param>
        public Signal(ILArray<double> samples, int samplesPerSecond)
        {
            Samples = samples;
            SamplesPerSecond = samplesPerSecond;
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
    }
}
