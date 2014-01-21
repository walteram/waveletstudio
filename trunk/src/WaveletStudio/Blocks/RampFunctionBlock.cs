/*  Wavelet Studio Signal Processing Library - www.waveletstudio.net
    Copyright (C) 2011, 2012 Walter V. S. de Amorim - The Wavelet Studio Initiative

    Wavelet Studio is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Wavelet Studio is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

using System;
using System.Collections.Generic;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.Properties;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// <para>Generates a Ramp signal using the following function:</para>
    /// <para>
    ///     <code>
    ///         y(t) = A * (t - t0) + D     ∀ t &gt;= t0, t &lt;= t1
    ///         y(t) = D                    ∀ t &lt; t0
    ///         y(t) = D + x(t1)            ∀ t &gt; t1 | D  (if ReturnToZero parameter is true)
    ///     </code>
    /// </para>
    /// <para>Where:</para>
    /// <para>A := amplitude</para>
    /// <para>D := offset</para>
    /// <para>t0 := ramp start</para>
    /// <para>t1 := ramp finish</para>
    /// <para>This block has no inputs.</para>
    /// <para>Image: http://i.imgur.com/o0NVryg.png </para>
    /// <para>InOutGraph: http://i.imgur.com/Vr3VOtk.png </para>
    /// <example>
    ///     <para>In this example we create a signal in a ramp shape. The signal starts at 0s and ends at 16s. The ramp starts at 3s and ends at 8s. The sampling rate is 1 (1Hz). The amplitude of the ramp is 1 and the signal offset is 1. The signal will return to the offset value (1) after the end of the ramp (8s). The last sample of the signal is included.</para>
    ///     <code>
    ///         var block = new RampFunctionBlock
    ///         {
    ///             Start = 0,
    ///             Finish = 16,
    ///             RampStart = 3,
    ///             RampFinish = 8,
    ///             SamplingRate = 1,
    ///             Amplitude = 1,
    ///             Offset = 1,
    ///             ReturnToZero = true,
    ///             IgnoreLastSample = false
    ///         };
    ///         block.Execute();
    /// 
    ///         Console.WriteLine(block.Output[0].ToString(0));
    ///         //Console Output:
    ///         //1, 1, 1, 1, 2, 3, 4, 5, 6, 1, 1, 1, 1, 1, 1, 1, 1
    ///     </code>
    /// </example>
    /// </summary>
    [SingleInputOutputBlock]
    [Serializable]
    public class RampFunctionBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RampFunctionBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);

            Amplitude = 1;
            Offset = 0;
            Start = RampStart = 0;
            Finish = RampFinish = 1;
            SamplingRate = 32768;
            IgnoreLastSample = false;
            ReturnToZero = true;
        }

        /// <summary>
        /// Name
        /// </summary>
        public override string Name { get { return Resources.Ramp; } }

        /// <summary>
        /// Description
        /// </summary>
        public override string Description  { get { return Resources.RampDescription; } }

        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.CreateSignal; } }

        /// <summary>
        /// Amplitude of the ramp. Default value is 1.
        /// </summary>
        [Parameter]
        public double Amplitude { get; set; }

        /// <summary>
        /// Offset of the signal in the y axis. Default value is 0.
        /// </summary>
        [Parameter]
        public double Offset { get; set; }

        /// <summary>
        /// Start of the signal in the time. Default value is 0.
        /// </summary>
        [Parameter]
        public double Start { get; set; }

        /// <summary>
        /// Finish of the signal in the time. Default value is 1.
        /// </summary>
        [Parameter]
        public double Finish { get; set; }

        /// <summary>
        /// Start of the ramp in the time. Default value is 0.
        /// </summary>
        [Parameter]
        public double RampStart { get; set; }

        /// <summary>
        /// Finish of the ramp in the time. Default value is 1.
        /// </summary>
        [Parameter]
        public double RampFinish { get; set; }

        private int _samplingRate;
        /// <summary>
        /// Sampling rate used on signal generation. Default value is 32768 (32KHz).
        /// </summary>
        [Parameter]
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

        /// <summary>
        /// If true, the last sample is not included in the created signal. Default value is false.
        /// </summary>
        [Parameter]
        public bool IgnoreLastSample { get; set; }

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
                    SamplingRate = Convert.ToInt32(Math.Round(1 / value));
                }
            }
        }

        /// <summary>
        /// If true, the value will return to 0 at the finish of the ramp. Default value is true.
        /// </summary>
        [Parameter]
        public bool ReturnToZero { get; set; }

        /// <summary>
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            var finish = GetFinish();
            var lastValue = Offset;
            var samples = MemoryPool.Pool.New<double>(Convert.ToInt32(Math.Ceiling((finish - Start) / SamplingInterval + SamplingInterval)));
            var i = 0;
            for (var x = Start; x <= finish; x += SamplingInterval)
            {
                samples[i] = GetRampValue(x, ref lastValue);
                i++;
            }
            var signal = new Signal(samples)
            {
                SamplingRate = SamplingRate,
                Start = Start,
                Finish = Finish,
                SamplingInterval = SamplingInterval
            };
            OutputNodes[0].Object = new List<Signal> { signal };
            if (Cascade && OutputNodes[0].ConnectingNode != null)
                OutputNodes[0].ConnectingNode.Root.Execute();            
        }

        private double GetRampValue(double x, ref double lastValue)
        {
            double value;
            if (x >= RampStart && x <= RampFinish)
            {
                value = Amplitude*(x - RampStart) + Offset;
                if (!ReturnToZero && x <= RampFinish)
                    lastValue = value;
            }
            else if (x < RampStart)
            {
                value = Offset;
            }
            else
            {
                value = lastValue;
            }
            return value;
        }

        /// <summary>
        /// Creates the input and output nodes
        /// </summary>
        /// <param name="root"></param>
        protected override sealed void CreateNodes(ref BlockBase root)
        {
            root.OutputNodes = BlockOutputNode.CreateSingleOutputSignal(ref root);
        }

        /// <summary>
        /// Clone the block, including the template
        /// </summary>
        /// <returns></returns>
        public override BlockBase Clone()
        {
            var block = (RampFunctionBlock)MemberwiseClone();
            block.Execute();            
            return block;
        }

        /// <summary>
        /// Clones this block but mantains the links
        /// </summary>
        /// <returns></returns>
        public override BlockBase CloneWithLinks()
        {
            var block = (RampFunctionBlock)MemberwiseCloneWithLinks();
            block.Execute();
            return block;
        }
    }
}
