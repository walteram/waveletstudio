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
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.Properties;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// <para>Extends a signal using the specified mode.</para>
    /// <para>Image: http://i.imgur.com/WDWxwms.png </para>
    /// <para>InOutGraph: http://i.imgur.com/W9tQojd.png </para>
    /// <para>Title: Signal Extension </para>
    /// <example>
    ///     <code>
    ///         var signal = new ImportFromTextBlock{Text = "1, 2, 3, 4, 5, 6, 7, 8"};
    ///         var block = new SignalExtensionBlock
    ///         {
    ///             ExtensionMode = SignalExtension.ExtensionMode.SymmetricHalfPoint,
    ///             ExtensionSize = 2
    ///         };
    ///         signal.ConnectTo(block);
    ///         signal.Execute();
    /// 
    ///         Console.WriteLine(block.Output[0].ToString(0));
    ///         //Output: 2 1 1 2 3 4 5 6 7 8 8 7
    ///     </code>
    /// </example>
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
            get { return Resources.Extend; }
        }

        /// <summary>
        /// Description of the block
        /// </summary>
        public override string Description
        {
            get { return Resources.ExtendDescription; }
        }

        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Operation; } }

        /// <summary>
        /// One of the following extension modes:
        /// </summary>
        [Parameter]
        public SignalExtension.ExtensionMode ExtensionMode { get; set; }

        /// <summary>
        /// Extension size. If zero (default), extents no next power of 2.
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
            root.InputNodes = BlockInputNode.CreateSingleInputSignal(ref root);
            root.OutputNodes = BlockOutputNode.CreateSingleOutput(ref root);
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
