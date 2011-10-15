using System;
using System.Collections.Generic;
using System.Linq;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.Functions;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Removes the duplicated samples in a signal
    /// </summary>
    [Serializable]
    public class UniqueBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UniqueBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);
            SortSamples = true;
        }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return "Unique"; }
        }

        /// <summary>
        /// Description
        /// </summary>
        public override string Description
        {
            get { return "Removes the duplicated samples in a signal."; }
        }

        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Operation; } }

        /// <summary>
        /// If true, the block sort the samples 
        /// </summary>
        [Parameter]
        public bool SortSamples { get; set; }

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
                output.Samples = SortSamples ? WaveMath.UniqueSorted(signal.Samples) : signal.Samples.Distinct().ToArray();
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