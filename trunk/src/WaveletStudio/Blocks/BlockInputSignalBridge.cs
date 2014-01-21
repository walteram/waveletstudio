using System;
using System.Collections.Generic;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// A shortcut to get a signal of the input
    /// </summary>
    [Serializable]
    public sealed class BlockInputSignalBridge : BlockInOutSignalBridgeBase<BlockInputNode>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BlockInputSignalBridge(BlockBase root) : base(root)
        {
                
        }

        protected override List<BlockInputNode> GetNodeList(BlockBase root)
        {
            return root.InputNodes;
        }
    }
}