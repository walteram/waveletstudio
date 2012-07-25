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
            foreach (var block in this.Where(block => block.InputNodes.Count == 0 && !block.Cascade))
            {
                ExecuteBlock(block);
            }
        }

        private void ExecuteBlock(BlockBase block)
        {
            block.Execute();
            if (block.Cascade)
            {
                return;
            }
            foreach (var node in block.OutputNodes.Where(node => node.ConnectingNode != null))
            {
                ExecuteBlock(node.ConnectingNode.Root);
            }
        }
    }
}
