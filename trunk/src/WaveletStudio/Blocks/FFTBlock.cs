using System;
using System.Collections.Generic;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.FFT;
using WaveletStudio.Functions;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// FFT
    /// </summary>
    [Serializable]
    public class FFTBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FFTBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);
        }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return "FFT"; }
        }

        /// <summary>
        /// Description of the block
        /// </summary>
        public override string Description
        {
            get { return "Compute fast Fourier transform (FFT) of input"; }
        }
        
        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Operation; } }

        /// <summary>
        /// Computation mode
        /// </summary>
        [Parameter]
        public ManagedFFTModeEnum Mode { get; set; } 

        /// <summary>
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            var inputNode = InputNodes[0].ConnectingNode as BlockOutputNode;
            if (inputNode == null || inputNode.Object == null)
                return;

            OutputNodes[0].Object.Clear();
            OutputNodes[1].Object.Clear();
            foreach (var inputSignal in inputNode.Object)
            {
                var fft = WaveMath.UpSample(inputSignal.Samples, false);
                ManagedFFT.FFT(ref fft, true, Mode);
                var abs = WaveMath.AbsFromComplex(fft);
                abs = WaveMath.Normalize(fft.SubArray(abs.Length / 2), abs.Length);

                var absSignal = new Signal(abs)
                {
                    Start = 0,
                    Finish = abs.Length - 1,
                    SamplingInterval = 0.5 / (Convert.ToDouble(abs.Length) / Convert.ToDouble(inputSignal.SamplingRate))
                };
                var fftSignal = new Signal(fft)
                {
                    Start = 0,
                    Finish = fft.Length - 1,
                    SamplingInterval = (Convert.ToDouble(abs.Length) / Convert.ToDouble(inputSignal.SamplingRate))
                };

                OutputNodes[0].Object.Add(absSignal);
                OutputNodes[1].Object.Add(fftSignal);
            }
            
            if (Cascade && OutputNodes[0].ConnectingNode != null)
                OutputNodes[0].ConnectingNode.Root.Execute();
            if (Cascade && OutputNodes[1].ConnectingNode != null)
                OutputNodes[1].ConnectingNode.Root.Execute();
        }

        protected override sealed void CreateNodes(ref BlockBase root)
        {
            root.InputNodes = new List<BlockInputNode> { new BlockInputNode(ref root, "Signal", "In") };
            root.OutputNodes = new List<BlockOutputNode>
                                   {
                                       new BlockOutputNode(ref root, "Absolute Value", "Abs"),
                                       new BlockOutputNode(ref root, "Complex FFT", "FFT")
                                   };
        }

        /// <summary>
        /// Clones this block
        /// </summary>
        /// <returns></returns>
        public override BlockBase Clone()
        {
            return MemberwiseClone();            
        }

        /// <summary>
        /// Clones this block but mantains the links
        /// </summary>
        /// <returns></returns>
        public override BlockBase CloneWithLinks()
        {
            return MemberwiseCloneWithLinks();
        }
    }
}
