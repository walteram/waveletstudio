﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Defines a processing block
    /// </summary>
    [Serializable]
    public abstract class BlockBase
    {
        public StringBuilder GeneratedData { get; set; }

        /// <summary>
        /// Inputs
        /// </summary>
        public List<BlockInputNode> InputNodes = new List<BlockInputNode>();

        /// <summary>
        /// Outputs
        /// </summary>
        public List<BlockOutputNode> OutputNodes = new List<BlockOutputNode>();

        /// <summary>
        /// Id of the block (changes for each instance)
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Description of the block
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Defines if the Execute method should be cascaded to the connected blocks
        /// </summary>
        public bool Cascade { get; set; }

        /// <summary>
        /// Type of action done by this block
        /// </summary>
        public abstract ProcessingTypeEnum ProcessingType { get; }

        /// <summary>
        /// Execute the block
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Creates the input and output nodes
        /// </summary>
        /// <param name="root"></param>
        protected abstract void CreateNodes(ref BlockBase root);

        /// <summary>
        /// Type of action done by the block
        /// </summary>
        public enum ProcessingTypeEnum
        {
            /// <summary>
            /// The block loads an external signal
            /// </summary>
            LoadSignal,
            /// <summary>
            /// The block creates a signal
            /// </summary>
            CreateSignal,
            /// <summary>
            /// The block makes some operation in a block
            /// </summary>
            Operation,
            /// <summary>
            /// The block insert disturbances or noise to the signal
            /// </summary>
            InsertDisturbance,
            /// <summary>
            /// The block exports the signal
            /// </summary>
            Export
        }

        /// <summary>
        /// Constructor
        /// </summary>
        protected BlockBase()
        {
            Id = Guid.NewGuid();
            Cascade = true;
        }

        /// <summary>
        /// Clone this block to a new one
        /// </summary>
        /// <returns></returns>
        public abstract BlockBase Clone();

        /// <summary>
        /// Clones this block but mantains the links
        /// </summary>
        /// <returns></returns>
        public abstract BlockBase CloneWithLinks();

        /// <summary>
        /// Clones this object
        /// </summary>
        /// <returns></returns>
        protected new BlockBase MemberwiseClone()
        {
            var block = (BlockBase)base.MemberwiseClone();
            CreateNodes(ref block);
            return block;
        }

        /// <summary>
        /// Clones this object
        /// </summary>
        /// <returns></returns>
        protected BlockBase MemberwiseCloneWithLinks()
        {
            var block = (BlockBase)base.MemberwiseClone();
            block.InputNodes = new List<BlockInputNode>();
            foreach (var node in InputNodes)
            {
                block.InputNodes.Add(node.Clone());
            }
            block.OutputNodes = new List<BlockOutputNode>();
            foreach (var blockOutputNode in OutputNodes)
            {
                block.OutputNodes.Add(blockOutputNode.Clone());
            }
            return block;
        }

        
    }
}
