using System;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Input node of an block
    /// </summary>
    [Serializable]
    public class BlockInputNode : BlockNodeBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="root">The block object thar contains this node</param>
        /// <param name="name">Name of the input</param>
        /// <param name="shortName">Short name</param>
        public BlockInputNode(ref BlockBase root, string name, string shortName) : base(ref root, name, shortName)
        {
        }

        /// <summary>
        /// Connect this input node to an output node in other block
        /// </summary>
        /// <param name="node"></param>
        public void ConnectTo(BlockOutputNode node)
        {
            ConnectingNode = node;
            node.ConnectingNode = this;
        }
        
        /// <summary>
        /// Connect this input node to an output node in other block
        /// </summary>
        /// <param name="node"></param>
        public void ConnectTo(ref BlockOutputNode node)
        {
            ConnectingNode = node;
            node.ConnectingNode = this;
        }

        /// <summary>
        /// Clones this object
        /// </summary>
        /// <returns></returns>
        public BlockInputNode Clone()
        {
            return (BlockInputNode)MemberwiseClone();
        }
    }
}
