﻿using System;
using System.Collections.Generic;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Output node of an block
    /// </summary>
    [Serializable]
    public class BlockOutputNode : BlockNodeBase
    {
        /// <summary>
        /// Object generated by the block
        /// </summary>
        public List<Signal> Object { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="root">The block object thar contains this node</param>
        /// <param name="name">Name of the output</param>
        /// <param name="shortName">Short name</param>
        public BlockOutputNode(ref BlockBase root, string name, string shortName) : base(ref root, name, shortName)
        {
            Object = new List<Signal>();
        }

        /// <summary>
        /// Connect this output node to an input node in other block
        /// </summary>
        /// <param name="node"></param>
        public void ConnectTo(BlockInputNode node)
        {
            ConnectingNode = node;
            node.ConnectingNode = this;
        }
        
        /// <summary>
        /// Connect this output node to an input node in other block
        /// </summary>
        /// <param name="node"></param>
        public void ConnectTo(ref BlockInputNode node)
        {
            ConnectingNode = node;
            node.ConnectingNode = this;
        }

        /// <summary>
        /// Clones this object
        /// </summary>
        /// <returns></returns>
        public BlockOutputNode Clone()
        {
            return (BlockOutputNode)MemberwiseClone();
        }        
    }
}
