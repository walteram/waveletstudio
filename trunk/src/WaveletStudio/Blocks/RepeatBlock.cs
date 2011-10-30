using System;
using System.Collections.Generic;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.Functions;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Repeat samples of a signal
    /// </summary>
    [Serializable]
    public class RepeatBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RepeatBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);
            RepetitionCount = 1;
            FrameSize = 1;
        }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return "Repeat"; }
        }

        /// <summary>
        /// Repetition count
        /// </summary>
        [Parameter]
        public uint RepetitionCount { get; set; }

        /// <summary>
        /// Frame size
        /// </summary>
        [Parameter]
        public uint FrameSize { get; set; }

        /// <summary>
        /// If true, keeps the original sampling rate, changing the signal start and finish times
        /// </summary>
        [Parameter]
        public bool KeepSamplingRate { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public override string Description
        {
            get { return "Resample input at higher rate by inserting zeros."; }
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
                OutputNodes[0].Object.Add(WaveMath.Repeat(signal, FrameSize, RepetitionCount, KeepSamplingRate));
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
