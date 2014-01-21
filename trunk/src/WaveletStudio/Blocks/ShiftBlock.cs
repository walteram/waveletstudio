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
    /// <para>Shifts a signal in time modifying the Start property.</para>
    /// <para>Image: http://i.imgur.com/3D9pbJc.png </para>
    /// <para>InOutGraph: http://i.imgur.com/CZ9mozs.png </para>
    /// <example>
    ///     <code>
    ///         var block = new ShiftBlock { Delay = 1.7 };
    ///         var signal = new ImportFromTextBlock { Text = "0, 1, -1, 3, 0", SignalStart = 1 };
    ///         signal.ConnectTo(block);
    ///         signal.Execute();
    /// 
    ///         Console.WriteLine(block.Output[0].Start);
    ///         //Output: 2.7
    ///     </code>
    /// </example>
    /// </summary>
    [Serializable]
    public class ShiftBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ShiftBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);
        }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return Resources.Shift; }
        }

        /// <summary>
        /// Description
        /// </summary>
        public override string Description
        {
            get { return Resources.ShiftDescription; }
        }

        /// <summary>
        /// Delay in time
        /// </summary>
        [Parameter]
        public double Delay { get; set; }

        /// <summary>
        /// Increment for the delay value
        /// </summary>
        public decimal DelayIncrement
        {
            get
            {
                var inputNode = InputNodes[0].ConnectingNode as BlockOutputNode;
                if (inputNode == null || inputNode.Object == null || inputNode.Object.Count == 0)
                    return 0.1m;
                return Convert.ToDecimal(inputNode.Object[0].SamplingInterval);
            }
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
                var output = signal.Clone();
                WaveMath.Shift(ref output, Delay);
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