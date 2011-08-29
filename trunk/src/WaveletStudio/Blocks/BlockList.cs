using System;
using System.Collections.Generic;
using System.Linq;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Defines a list of blocks
    /// </summary>
    [Serializable]
    public class BlockList : List<BlockBase>
    {
        /// <summary>
        /// Executes all the connected blocks in the list
        /// </summary>
        /// <returns></returns>
        public void ExecuteAll()
        {
            foreach (var block in this.Where(block => block.InputNodes.Count == 0))
            {
                ExecuteBlock(block);
            }
        }

        private void ExecuteBlock(BlockBase block)
        {
            block.Execute();
            if (block.Cascade) 
                return;
            foreach (var node in block.OutputNodes.Where(node => node.ConnectingNode != null))
            {
                ExecuteBlock(node.ConnectingNode.Root);
            }
        }        
    }
}
