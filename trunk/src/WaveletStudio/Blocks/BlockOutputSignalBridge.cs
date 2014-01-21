using System;
using System.Collections.Generic;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// A shortcut to get a signal of the input
    /// </summary>
    [Serializable]
    public sealed class BlockOutputSignalBridge : BlockInOutSignalBridgeBase<BlockOutputNode>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BlockOutputSignalBridge(BlockBase root) : base(root)
        {

        }

        protected override List<BlockOutputNode> GetNodeList(BlockBase root)
        {
            return root.OutputNodes;
        }
    }
}