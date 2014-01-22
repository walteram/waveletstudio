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
    /// <para> </para>
    /// <para>Inverts a signal in time. </para>
    /// <para>For example, if we have a signal with 8 samples like this one:</para>
    /// <para> </para>
    /// <code>2, 3, -4, 8, 7, 1, 2, -3</code>
    /// <para> </para>
    /// <para>the block will output a new signal with the folowing samples:</para>
    /// <para> </para>
    /// <code>-3, 2, 1, 7, 8, -4, 3, 2</code>
    /// <para> </para>
    /// <para>This block has no inputs.</para>
    /// <para>Image: http://i.imgur.com/7PhV0G4.png </para>
    /// <para>InOutGraph: http://i.imgur.com/nXdS5DB.png </para>
    /// <para>Title: Invert </para>
    /// <example>
    ///     <code>
    ///         var signal = new ImportFromTextBlock { Text = "2, 3, -4, 8, 7, 1, 2, -3" };
    ///         var block = new InvertBlock();
    ///         signal.ConnectTo(block);
    ///         signal.Execute();
    ///         
    ///         Console.WriteLine(block.Output[0].ToString(0));
    ///         //Output: -3 2 1 7 8 -4 3 2
    ///     </code>
    /// </example>
    /// </summary>
    [Serializable]
    public class InvertBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public InvertBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);
        }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return Resources.Invert; }
        }

        /// <summary>
        /// Description
        /// </summary>
        public override string Description
        {
            get { return Resources.InvertDescription; }
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
                var samples = WaveMath.Invert(signal.Samples);
                output.Samples = samples;
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