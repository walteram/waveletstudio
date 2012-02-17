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
using WaveletStudio.Blocks.CustomAttributes;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Signal extension
    /// </summary>
    [SingleInputOutputBlock]
    [Serializable]
    public class SignalExtensionBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SignalExtensionBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);

            ExtensionMode = SignalExtension.ExtensionMode.SymmetricHalfPoint;
            ExtensionSize = 0;
        }

        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return "Extend";  }
        }

        /// <summary>
        /// Description of the block
        /// </summary>
        public override string Description
        {
            get { return "Extends a signal using the specified mode"; }
        }

        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Operation; } }

        /// <summary>
        /// Extension mode
        /// </summary>
        [Parameter]
        public SignalExtension.ExtensionMode ExtensionMode { get; set; }

        /// <summary>
        /// Extension size. If zero, extents no next power of 2.
        /// </summary>
        [Parameter]
        public int ExtensionSize { get; set; }

        /// <summary>
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            var connectingNode = InputNodes[0].ConnectingNode as BlockOutputNode;
            if (connectingNode == null || connectingNode.Object == null)
                return;

            int beforeSize = 0, afterSize = 0;
            if (ExtensionSize > 0)
            {
                beforeSize = ExtensionSize;
                afterSize = ExtensionSize;
            }
            OutputNodes[0].Object.Clear();
            foreach (var signal in connectingNode.Object)
            {
                if (ExtensionSize <= 0)
                {
                    var size = SignalExtension.NextPowerOf2(signal.Samples.Length);
                    beforeSize = afterSize = (size - signal.Samples.Length) / 2;
                    while (beforeSize + afterSize + signal.Samples.Length < size)
                        afterSize++;
                }

                var output = signal.Clone();
                SignalExtension.Extend(ref output, ExtensionMode, beforeSize, afterSize);
                OutputNodes[0].Object.Add(output);
            }

            if (Cascade && OutputNodes[0].ConnectingNode != null)
                OutputNodes[0].ConnectingNode.Root.Execute();
        }

        /// <summary>
        /// Creates the input and output nodes
        /// </summary>
        /// <param name="root"></param>
        protected override sealed void CreateNodes(ref BlockBase root)
        {
            root.InputNodes = new List<BlockInputNode> { new BlockInputNode(ref root, "Signal", "In") };
            root.OutputNodes = new List<BlockOutputNode> {new BlockOutputNode(ref root, "Output", "Out")};
        }

        /// <summary>
        /// Clones this block
        /// </summary>
        /// <returns></returns>
        public override BlockBase Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Clones this block but mantains the links
        /// </summary>
        /// <returns></returns>
        public override BlockBase CloneWithLinks()
        {
            return MemberwiseCloneWithLinks();
        }
    }
}
