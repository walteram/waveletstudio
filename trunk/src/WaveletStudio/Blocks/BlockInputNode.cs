/*  Wavelet Studio Signal Processing Library - www.waveletstudio.net
    Copyright (C) 2011, 2012 Walter V. S. de Amorim - The Wavelet Studio Initiative

    Wavelet Studio is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Wavelet Studio is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

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
        /// Default constructor
        /// </summary>
        public BlockInputNode()
        {

        }

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
            ConnectingNode.Root.Execute();
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
