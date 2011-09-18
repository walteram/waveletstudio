using System;
using System.Collections.Generic;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.Functions;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Executes a scalar operation in a dy
    /// </summary>
    [Serializable]
    public class ConvolutionBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ConvolutionBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);
            ConvolutionMode = ConvolutionModeEnum.ManagedFFT;
            ReturnOnlyValid = false;
        }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return "Convolution"; }
        }

        /// <summary>
        /// Description of the block
        /// </summary>
        public override string Description
        {
            get { return "Compute convolution of two inputs"; }
        }

        /// <summary>
        /// The convolution mode to be used.
        /// </summary>
        //[Parameter] - Normal mode is too slow! So i removed from user interface
        public ConvolutionModeEnum ConvolutionMode { get; set; }

        /// <summary>
        /// The FFT mode to be used.
        /// </summary>
        [Parameter]
        public ManagedFFTModeEnum Mode { get; set; }

        /// <summary>
        /// The block returns only the valid samples (central area)
        /// </summary>
        [Parameter]
        public bool ReturnOnlyValid { get; set; }

        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Operation; } }

        /// <summary>
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            var inputNode1 = InputNodes[0].ConnectingNode as BlockOutputNode;
            var inputNode2 = InputNodes[1].ConnectingNode as BlockOutputNode;
            if (inputNode1 == null || inputNode1.Object == null || inputNode2 == null || inputNode2.Object == null)
                return;

            var outputs = new List<Signal>();
            var signals = inputNode1.Object;
            var filters = inputNode2.Object;
            for (var i = 0; i < signals.Count; i++)
            {
                var signal = signals[i];
                var output = signal.Copy();
                if (i < filters.Count)
                {
                    output.Samples = WaveMath.Convolve(ConvolutionMode, signal.Samples, filters[i].Samples, ReturnOnlyValid, 0, Mode);                    
                }
                outputs.Add(output);
            }
            OutputNodes[0].Object = outputs;
            if (Cascade && OutputNodes[0].ConnectingNode != null)
                OutputNodes[0].ConnectingNode.Root.Execute();
        }

        protected override sealed void CreateNodes(ref BlockBase root)
        {
            root.InputNodes = new List<BlockInputNode>
                                  {
                                      new BlockInputNode(ref root, "Signal1", "S1"),
                                      new BlockInputNode(ref root, "Signal2", "S2")
                                  };
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
