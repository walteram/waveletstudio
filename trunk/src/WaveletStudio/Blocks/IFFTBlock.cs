using System;
using System.Collections.Generic;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.FFT;
using WaveletStudio.Functions;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// IFFT
    /// </summary>
    [Serializable]
    public class IFFTBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public IFFTBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);
        }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return "IFFT"; }
        }

        /// <summary>
        /// Description of the block
        /// </summary>
        public override string Description
        {
            get { return "Compute inverse fast Fourier transform (IFFT) of input"; }
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
            foreach (var inputSignal in inputNode.Object)
            {
                var ifft = (double[])inputSignal.Samples.Clone();
                ManagedFFT.FFT(ref ifft, false, Mode);
                ifft = WaveMath.DownSample(ifft, 2, true);

                var signal = new Signal(ifft)
                {
                    Start = 0,
                    Finish = ifft.Length - 1,
                    SamplingInterval = 0.5 / (Convert.ToDouble(ifft.Length) / Convert.ToDouble(inputSignal.SamplingRate))
                };
                OutputNodes[0].Object.Add(signal);
            }

            if (Cascade && OutputNodes[0].ConnectingNode != null)
                OutputNodes[0].ConnectingNode.Root.Execute();
        }

        /// <summary>
        /// Creates the input and output nodes
        /// </summary>
        /// <param name="root"></param>
        protected override sealed void CreateNodes(ref BlockBase root)
        {
            root.InputNodes = new List<BlockInputNode> { new BlockInputNode(ref root, "Signal", "In") };
            root.OutputNodes = new List<BlockOutputNode>
                                   {
                                       new BlockOutputNode(ref root, "Output", "IFFT")
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
