using System;
using System.Collections.Generic;
using WaveletStudio.FFT;
using WaveletStudio.Functions;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Executes a scalar operation in a dy
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
        public ManagedFFTModeEnum Mode { get; set; } 

        /// <summary>
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            var inputNode = InputNodes[0].ConnectingNode as BlockOutputNode;
            if (inputNode == null || inputNode.Object == null)
                return;

            var inputSignal = inputNode.Object;
            var fft = WaveMath.UpSample(inputSignal.Samples, false);
            ManagedFFT.FFT(ref fft, true, Mode);
            fft = WaveMath.AbsFromComplex(fft);
            fft = WaveMath.Normalize(fft.SubArray(fft.Length / 2), fft.Length/4);

            var signal = new Signal(fft)
                             {
                                 Start = 0,
                                 Finish = fft.Length,
                                 SamplingInterval = 0.5/(Convert.ToDouble(fft.Length)/Convert.ToDouble(inputSignal.SamplingRate))
                             };
            OutputNodes[0].Object = signal;
            if (Cascade && OutputNodes[0].ConnectingNode != null)
                OutputNodes[0].ConnectingNode.Root.Execute();
        }

        protected override sealed void CreateNodes(ref BlockBase root)
        {
            root.InputNodes = new List<BlockInputNode> { new BlockInputNode(ref root, "Signal", "In") };
            root.OutputNodes = new List<BlockOutputNode> {new BlockOutputNode(ref root, "Output", "Out")};
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
