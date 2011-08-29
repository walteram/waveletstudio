using System;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Base of the nodes
    /// </summary>
    [Serializable]
    public abstract class BlockNodeBase
    {
        /// <summary>
        /// The block that contains this node
        /// </summary>
        public BlockBase Root { get; set; }

        /// <summary>
        /// Name of the node
        /// </summary>
        public string Name { get; set; }
    
        /// <summary>
        /// Short name
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// The another node connected to this
        /// </summary>
        public BlockNodeBase ConnectingNode { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="root">The block object thar contains this node</param>
        /// <param name="name">Name</param>
        /// <param name="shortName">Short name</param>
        protected BlockNodeBase(ref BlockBase root, string name, string shortName)
        {
            Root = root;
            Name = name;
            ShortName = shortName;
            ConnectingNode = null;            
        }


        /// <summary>
        /// Connect this input node to another node in other block
        /// </summary>
        /// <param name="node"></param>
        public void ConnectTo(ref BlockNodeBase node)
        {
            ConnectingNode = node;
            node.ConnectingNode = this;
        }
    }
}
