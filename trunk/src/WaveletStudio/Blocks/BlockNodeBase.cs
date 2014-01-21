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
using System.Collections.Generic;

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
        /// Returns the signals of the node
        /// </summary>
        public abstract List<Signal> SignalList();

        /// <summary>
        /// Constructor
        /// </summary>
        protected BlockNodeBase()
        {

        }

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
