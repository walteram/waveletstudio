using System;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Executes a scalar operation in a signal
    /// </summary>
    [Serializable]
    public class DownSampleBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DownSampleBlock()
        {
            BlockBase root = this;
            InputNodes.Add(new BlockInputNode(ref root, "Signal", "In"));            
            OutputNodes.Add(new BlockOutputNode(ref root, "Output", "Out"));
        }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return "DownSample"; }
        }

        /// <summary>
        /// Description of the block
        /// </summary>
        public override string Description
        {
            get { return "Downsample in base 2"; }
        }

        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Operation; } }

        /// <summary>
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            var inputNode = InputNodes[0].ConnectingNode as BlockOutputNode;
            if (inputNode == null || inputNode.Object == null)
                return;

            Signal signal = inputNode.Object;

            var output = signal.Copy();
            output.Samples = WaveMath.DownSample(signal.Samples);
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
