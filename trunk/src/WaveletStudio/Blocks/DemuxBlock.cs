using System;
using System.Collections.Generic;
using WaveletStudio.Blocks.CustomAttributes;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Extract and output elements of vector signal
    /// </summary>
    [Serializable]
    public class DemuxBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DemuxBlock()
        {
            OutputCount = 3;
        }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return "Demux"; }
        }

        private uint _outputCount;
        /// <summary>
        /// Number of output ports
        /// </summary>
        [Parameter]        
        public uint OutputCount
        {
            get { return _outputCount; }
            set
            {
                _outputCount = value;
                BlockBase root = this;
                CreateNodes(ref root);
            }
        }

        /// <summary>
        /// Indexes of the signals to be copied in the output. One line per output, separated with commas (,).
        /// </summary>
        [TextParameter]
        public string SignalIndexes { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public override string Description
        {
            get { return "Extract and output elements of vector signal."; }
        }

        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Routing; } }

        /// <summary>
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            foreach (var outputNode in OutputNodes)
            {
                outputNode.Object.Clear();
            }
            var inputNode = InputNodes[0].ConnectingNode as BlockOutputNode;
            if (inputNode == null || inputNode.Object.Count == 0)
                return;
            if (OutputCount == 0)
                OutputCount = Convert.ToUInt32(inputNode.Object.Count);
            
            var signalIndexes = (SignalIndexes ?? "").Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < OutputNodes.Count; i++)
            {
                if (string.IsNullOrEmpty(SignalIndexes))
                {
                    if(i < inputNode.Object.Count)
                        OutputNodes[i].Object.Add(inputNode.Object[i]);
                }
                else if (i < signalIndexes.Length)
                {
                    foreach (var item in signalIndexes[i].Split(new []{','}, StringSplitOptions.RemoveEmptyEntries))
                    {
                        int index;
                        if(int.TryParse(item.Trim(), out index) && index >= 0 && index < inputNode.Object.Count)
                            OutputNodes[i].Object.Add(inputNode.Object[index]);
                    }
                }                
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
            root.InputNodes = new List<BlockInputNode> { new BlockInputNode(ref root, "Input", "In") };
            root.OutputNodes = new List<BlockOutputNode>();
            for (var i = 1; i <= _outputCount; i++)
            {
                root.OutputNodes.Add(new BlockOutputNode(ref root, "Output " + i, "Out" + i));
            }                       
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
