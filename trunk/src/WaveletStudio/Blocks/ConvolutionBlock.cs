using System;
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
            InputNodes.Add(new BlockInputNode(ref root, "Signal1", "S1"));
            InputNodes.Add(new BlockInputNode(ref root, "Signal2", "S2"));
            OutputNodes.Add(new BlockOutputNode(ref root, "Output", "Out"));
            ConvolutionMode = ConvolutionModeEnum.ManagedFft;
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
        [Parameter]
        public ConvolutionModeEnum ConvolutionMode { get; set; }

        /// <summary>
        /// The 
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

            var signal = inputNode1.Object;
            var filter = inputNode2.Object;

            var output = signal.Copy();
            output.Samples = WaveMath.Convolve(ConvolutionMode, signal.Samples, filter.Samples, ReturnOnlyValid);
            OutputNodes[0].Object = output;
            if (Cascade && OutputNodes[0].ConnectingNode != null)
                OutputNodes[0].ConnectingNode.Root.Execute();
        }
        
        /// <summary>
        /// Clones this block
        /// </summary>
        /// <returns></returns>
        public override BlockBase Clone()
        {
            return MemberwiseClone();            
        }
    }
}
