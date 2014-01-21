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
using WaveletStudio.FFT;
using WaveletStudio.Functions;
using WaveletStudio.Properties;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// <para>Executes the Backward Fast Fourier Transform (FFT) using the Managed FFT function.</para>
    /// <para>Image: http://i.imgur.com/AeAyClJ.png </para>
    /// <para>InOutGraph: http://i.imgur.com/qPxeOtO.png </para>
    /// <example>
    ///     <code>
    ///         var freqInput = new ImportFromTextBlock { Text = "12.3, 0.0, 4.5, 7.2, -5.8, 4.5, -7.5, -2.3, -2.8, 0.0, -7.5, 2.3, -5.8, -4.5, 4.5, -7.2" };
    ///         var block = new IFFTBlock
    ///         {
    ///             Mode = ManagedFFTModeEnum.UseLookupTable
    ///         };
    ///     
    ///         freqInput.ConnectTo(block);
    ///         freqInput.Execute();
    ///     
    ///         Console.WriteLine(block.OutputNodes[0].Object.ToString(1));            
    ///     
    ///         //Console output:
    ///         //-1.0, 6.0, 5.0, -0.5, 0.5, 0.0, 0.3, 2.0
    ///     </code>
    /// </example>
    /// </summary>
    [Serializable]
    public class IFFTBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public IFFTBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);
        }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return "IFFT"; }
        }

        /// <summary>
        /// Description of the block
        /// </summary>
        public override string Description
        {
            get { return Resources.IFFTDescription; }
        }
        
        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Transform; } }

        /// <summary>
        /// Computation mode
        /// </summary>
        [Parameter]
        public ManagedFFTModeEnum Mode { get; set; } 

        /// <summary>
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            var inputNode = InputNodes[0].ConnectingNode as BlockOutputNode;
            if (inputNode == null || inputNode.Object == null)
                return;

            OutputNodes[0].Object.Clear();
            foreach (var inputSignal in inputNode.Object)
            {
                var ifft = (double[])inputSignal.Samples.Clone();
                ManagedFFT.Instance.FFT(ref ifft, false, Mode);
                ifft = WaveMath.DownSample(ifft, 2, true);

                var signal = new Signal(ifft)
                {
                    Start = 0,
                    Finish = ifft.Length - 1,
                    SamplingInterval = 1
                };
                OutputNodes[0].Object.Add(signal);
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
            root.OutputNodes = new List<BlockOutputNode>
                                   {
                                       new BlockOutputNode(ref root, Resources.Output, "IFFT")
                                   };
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
