using System;
using System.Collections.Generic;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.Functions;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Shift signal
    /// </summary>
    [Serializable]
    public class ShiftBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ShiftBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);
        }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return "Shift"; }
        }

        /// <summary>
        /// Description
        /// </summary>
        public override string Description
        {
            get { return "Shift signal."; }
        }

        /// <summary>
        /// Delay in time
        /// </summary>
        [Parameter]
        public double Delay { get; set; }

        /// <summary>
        /// Increment for the delay value
        /// </summary>
        public decimal DelayIncrement
        {
            get
            {
                var inputNode = InputNodes[0].ConnectingNode as BlockOutputNode;
                if (inputNode == null || inputNode.Object == null || inputNode.Object.Count == 0)
                    return 0.1m;
                return Convert.ToDecimal(inputNode.Object[0].SamplingInterval);
            }
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

            OutputNodes[0].Object.Clear();
            foreach (var signal in inputNode.Object)
            {
                var output = signal.Clone();
                WaveMath.Shift(ref output, Delay);
                OutputNodes[0].Object.Add(output);
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
            root.InputNodes = new List<BlockInputNode>();
            root.OutputNodes = new List<BlockOutputNode>();
            root.InputNodes.Add(new BlockInputNode(ref root, "Signal", "In"));
            root.OutputNodes.Add(new BlockOutputNode(ref root, "Output", "Out"));
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