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
using WaveletStudio.Functions;
using WaveletStudio.Properties;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// <para>Resample input at higher rate by inserting zeros.</para>
    /// <para> </para>
    /// <para>For example, if we have a signal with 8 samples like this one:</para>
    /// <code>1.1, 9.12, 0.123, 1, 1.1, 4.56, 0.123, -45</code>
    /// <para>the block will output a new signal with the folowing samples:</para>
    /// <code>1.1, 0, 9.12, 0, 0.123, 0, 1, 0, 1.1, 0, 4.56, 0, 0.123, 0, -45</code>
    /// <para>Image: http://i.imgur.com/zOOdBZS.png </para>
    /// <para>InOutGraph: http://i.imgur.com/SS0Uk7T.png </para>
    /// <para>Title: Upsample</para>
    /// <example>
    ///     <code>
    ///         var signal = new ImportFromTextBlock { Text = "2, 3, -1, 1" };
    ///         var block = new UpSampleBlock 
    ///         {
    ///             Factor = 3
    ///         };
    ///         
    ///         signal.ConnectTo(block);
    ///         signal.Execute();
    /// 
    ///         Console.WriteLine(block.Output[0].ToString(0));
    ///         //Output: 2 0 0 3 0 0 -1 0 0 1
    ///     </code>
    /// </example>
    /// </summary>
    [Serializable]
    public class UpSampleBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UpSampleBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);
            Factor = 2;
        }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return Resources.Upsample; }
        }

        /// <summary>
        /// Upsample factor. Default value is 2.
        /// </summary>
        [Parameter]
        public uint Factor { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public override string Description
        {
            get { return Resources.UpsampleDescription; }
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
                output.Samples = WaveMath.UpSample(signal.Samples, Convert.ToInt32(Factor));                
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
