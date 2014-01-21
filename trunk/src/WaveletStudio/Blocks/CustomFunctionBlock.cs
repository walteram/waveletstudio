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
using System.Diagnostics.CodeAnalysis;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.Properties;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Generates a custom function
    /// </summary>
    [SingleInputOutputBlock]
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class CustomFunctionBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CustomFunctionBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);

            Start = 0;
            Finish = 1;
            SamplingRate = 32768;
            IgnoreLastSample = false;            
        }

        /// <summary>
        /// Name
        /// </summary>
        public override string Name { get { return Resources.Ramp; } } //TODO: ajustar

        /// <summary>
        /// Description
        /// </summary>
        public override string Description { get { return Resources.RampDescription; } } //TODO: ajustar

        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Operation; } }

        /// <summary>
        /// Start of the signal in time
        /// </summary>
        [Parameter]
        public double Start { get; set; }

        /// <summary>
        /// Finish of the signal in time
        /// </summary>
        [Parameter]
        public double Finish { get; set; }

        private int _samplingRate;
        /// <summary>
        /// Sampling rate used on sampling
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
        /// Text of the function
        /// </summary>
        [Parameter]
        public string FunctionText { get; set; }

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
        /// Defines it the last sample will be included in signal
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
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            var finish = GetFinish();
            var samples = MemoryPool.Pool.New<double>(Convert.ToInt32(Math.Ceiling((finish - Start) / SamplingInterval + SamplingInterval)));
            var i = 0;
            for (var x = Start; x <= finish; x += SamplingInterval)
            {
                samples[i] = Math.Sin(3.75*2*Math.PI*x) + Math.Cos(7*2*Math.PI*x) + Math.Cos(11*2*Math.PI*x);
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
            var block = (CustomFunctionBlock)MemberwiseClone();
            block.Execute();            
            return block;
        }

        /// <summary>
        /// Clones this block but mantains the links
        /// </summary>
        /// <returns></returns>
        public override BlockBase CloneWithLinks()
        {
            var block = (CustomFunctionBlock)MemberwiseCloneWithLinks();
            block.Execute();
            return block;
        }
    }
}
