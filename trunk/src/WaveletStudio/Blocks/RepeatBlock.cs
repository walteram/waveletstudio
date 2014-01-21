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
    /// <para>Repeats samples of a signal.</para>
    /// <para> </para>
    /// <para>For example, if we have a signal with 8 samples like this one:</para>
    /// <code>1, 9, 0, 1, 2, 5, -4, 4</code>
    /// <para>and set the FrameSize parameter to 4, the block will output a new signal with the folowing samples:</para>
    /// <code>1, 9, 0, 1,   1, 9, 0, 1,   2, 5, -4, 4,   2, 5, -4, 4</code>
    /// <para>Image: http://i.imgur.com/LC6BDlw.png </para>
    /// <para>InOutGraph: http://i.imgur.com/Pkjnhp9.png </para>    
    /// <example>
    ///     <code>
    ///         var signal = new ImportFromTextBlock { Text = "0, 3, -3, 0, 2, 2, 2, 0" };
    ///         var block = new RepeatBlock
    ///         { 
    ///             FrameSize = 4, 
    ///             RepetitionCount = 1
    ///         };
    ///         signal.ConnectTo(block);
    ///         signal.Execute();
    ///         
    ///         Console.WriteLine(block.Output[0].ToString(0));
    ///         //Output: 0 3 -3 0 0 3 -3 0 2 2 2 0 2 2 2 0
    ///     </code>
    /// </example>
    /// </summary>
    [Serializable]
    public class RepeatBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RepeatBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);
            RepetitionCount = 1;
            FrameSize = 1;
        }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return Resources.Repeat; }
        }

        /// <summary>
        /// The number of times the block will repeat the frame. Default value is 1.
        /// </summary>
        [Parameter]
        public uint FrameSize { get; set; }

        /// <summary>
        /// The number of samples to be repeated per time. Default value is 1.
        /// </summary>
        [Parameter]
        public uint RepetitionCount { get; set; }

        /// <summary>
        /// If true, keeps the original sampling rate, changing the signal start and finish times
        /// </summary>
        [Parameter]
        public bool KeepSamplingRate { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public override string Description
        {
            get { return Resources.RepeatDescription; }
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
                OutputNodes[0].Object.Add(WaveMath.Repeat(signal, FrameSize, RepetitionCount, KeepSamplingRate));
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
