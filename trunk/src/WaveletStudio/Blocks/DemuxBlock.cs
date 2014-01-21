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
using WaveletStudio.Properties;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// <para>Extracts the components of an input signal and outputs the components as separate signals.</para>
    /// <para>Image: http://i.imgur.com/pKW0rXZ.png </para>
    /// <para>InOutGraph: http://i.imgur.com/cHMpTVx.png </para>
    /// <example>
    ///     <code>
    ///         var signal = new ImportFromTextBlock {Text = "0, 3, -1, 2, 0, \r\n 0, -1, 2, 3, 0"};
    ///         var block = new DemuxBlock { OutputCount = 2 };
    ///         signal.ConnectTo(block);
    ///         signal.Execute();
    ///         
    ///         Console.WriteLine("Signal 1 = " + block.OutputNodes[0].Object.ToString(0));
    ///         Console.WriteLine("Signal 2 = " + block.OutputNodes[1].Object.ToString(0));
    ///         
    ///         //Console Output:
    ///         //Signal 1 = 0 3 -1 2 0
    ///         //Signal 2 = 0 -1 2 3 0
    ///     </code>
    /// </example>
    /// </summary>
    [Serializable]
    public class DemuxBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DemuxBlock()
        {
            OutputCount = 3;
        }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return "Demux"; }
        }

        private uint _outputCount;
        /// <summary>
        /// Number of output ports
        /// </summary>
        [Parameter]        
        public uint OutputCount
        {
            get { return _outputCount; }
            set
            {
                _outputCount = value;
                BlockBase root = this;
                CreateNodes(ref root);
            }
        }

        /// <summary>
        /// Indexes of the signals to be copied in the output. One line per output, separated with commas (,).
        /// </summary>
        [TextParameter]
        public string SignalIndexes { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public override string Description
        {
            get { return Resources.DemuxDescription; }
        }

        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Routing; } }

        /// <summary>
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            foreach (var outputNode in OutputNodes)
            {
                outputNode.Object.Clear();
            }
            var inputNode = InputNodes[0].ConnectingNode as BlockOutputNode;
            if (inputNode == null || inputNode.Object.Count == 0)
                return;
            if (OutputCount == 0)
                OutputCount = Convert.ToUInt32(inputNode.Object.Count);
            
            var signalIndexes = (SignalIndexes ?? "").Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < OutputNodes.Count; i++)
            {
                if (string.IsNullOrEmpty(SignalIndexes))
                {
                    if(i < inputNode.Object.Count)
                        OutputNodes[i].Object.Add(inputNode.Object[i]);
                }
                else if (i < signalIndexes.Length)
                {
                    foreach (var item in signalIndexes[i].Split(new []{','}, StringSplitOptions.RemoveEmptyEntries))
                    {
                        int index;
                        if(int.TryParse(item.Trim(), out index) && index >= 0 && index < inputNode.Object.Count)
                            OutputNodes[i].Object.Add(inputNode.Object[index]);
                    }
                }                
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
            root.InputNodes = BlockInputNode.CreateSingleInput(ref root);            
            root.OutputNodes = new List<BlockOutputNode>();
            for (var i = 1; i <= _outputCount; i++)
            {
                root.OutputNodes.Add(new BlockOutputNode(ref root, Resources.Output + " " + i, Resources.Out + i));
            }                       
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
