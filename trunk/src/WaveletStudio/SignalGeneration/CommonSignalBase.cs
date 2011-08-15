using System;

namespace WaveletStudio.SignalGeneration
{
    /// <summary>
    /// Common signal base
    /// </summary>
    public abstract class CommonSignalBase
    {        
        /// <summary>
        /// Name of the signal
        /// </summary>
        public abstract string Name { get; }
        
        /// <summary>
        /// Start of the signal in time
        /// </summary>
        public double Start { get; set; }

        /// <summary>
        /// Finish of the signal in time
        /// </summary>
        public double Finish { get; set; }

        /// <summary>
        /// Amplitude of the signal
        /// </summary>
        public double Amplitude { get; set; }

        /// <summary>
        /// Frequency of the signal
        /// </summary>
        public double Frequency { get; set; }

        /// <summary>
        /// The initial angle of function at its origin
        /// </summary>
        public double Phase { get; set; }

        /// <summary>
        /// Distance from the origin
        /// </summary>
        public double Offset { get; set; }
        
        /// <summary>
        /// Defines it the last sample will be included in signal
        /// </summary>
        public bool IgnoreLastSample;

        /// <summary>
        /// Executes the sampler
        /// </summary>
        /// <returns></returns>
        public abstract Signal ExecuteSampler();

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
                if (value != 0)
                {
                    SamplingRate = Convert.ToInt32(Math.Round(1 / value));   
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
        /// Get the finish of the signal considering the seleccted EndingOption
        /// </summary>
        /// <returns></returns>
        protected double GetFinish()
        {
            var finish = Finish;
            if (IgnoreLastSample)
            {
                finish = finish - SamplingInterval;
            }
            return finish;
        }

        protected CommonSignalBase()
        {
            Amplitude = 1;
            Frequency = 60;
            Phase = 0;
            Offset = 0;
            Start = 0;
            Finish = 1;
            SamplingRate = 44100;
            IgnoreLastSample = false;
        }

        public CommonSignalBase Clone()
        {
            return (CommonSignalBase) MemberwiseClone();
        }
    }
}
