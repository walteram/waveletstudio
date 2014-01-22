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
using WaveletStudio.Functions;
using WaveletStudio.Properties;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// <para>Outputs the absolute value of a signal.</para>
    /// <para>Image: http://i.imgur.com/fzFnv6Z.png </para>
    /// <para>InOutGraph: http://i.imgur.com/HouxION.png </para>
    /// <para>Title: Absolute Value</para>
    /// <example>
    ///     <code>
    ///         var block = new AbsoluteValueBlock();
    ///         var signal = new ImportFromTextBlock {Text = "2.1, 3.2, -1, -1.3, -100, -2"};
    ///         signal.ConnectTo(block);
    ///         signal.Execute();
    ///         Console.WriteLine(block.OutputNodes[0].Object.ToString(1));
    ///         
    ///         //Console Output:
    ///         //2.1 3.2 1.0 1.3 100.0 2.0
    ///     </code>
    /// </example>
    /// </summary>
    [Serializable]
    public class AbsoluteValueBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AbsoluteValueBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);
        }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return Resources.Absolute; }
        }

        /// <summary>
        /// Description
        /// </summary>
        public override string Description
        {
            get { return Resources.AbsoluteDescription; }
        }

        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Operation; } }

        /// <summary>
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            var inputNode = InputNodes[0].ConnectingNode as BlockOutputNode;
            if (inputNode == null || inputNode.Object == null)
                return;

            OutputNodes[0].Object.Clear();
            foreach (var signal in inputNode.Object)
            {
                var output = signal.Copy();
                WaveMath.Abs(ref output, signal.Samples);
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