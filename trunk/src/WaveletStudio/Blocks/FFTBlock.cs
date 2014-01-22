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
    /// <para>Executes the Forward Fast Fourier Transform (FFT) using the Managed FFT function.</para>
    /// <para>Image: http://i.imgur.com/tpcUlFu.png </para>
    /// <para>InOutGraph: http://i.imgur.com/MRddB2k.png </para>
    /// <para>Title: FFT </para>
    /// <para>Inputs: This block has only one input: the signal or signal list to be computed.</para>
    /// <para>Outputs: This block has two outputs:</para>
    /// <para>Outputs:  0 – Absolute Value (abs): Absolute values of transform. Usefull to plot the transform.</para>
    /// <para>Outputs:  1 – Complex FFT (fft): Complex values of transform (real and imaginary part). You must use this output if you plan to reconstruct the original signal.</para>
    /// <example>
    ///     <code>
    ///         var signal = new ImportFromTextBlock { Text = "-1, 6, 5, -0.5, 0.5, 0, 0.25, 2, 0, -4, 4, 0, 1, -3, -1, 2, -3, 0, 0, 3, 0, -0.1, 1, 1.1, -3, 0, 0, 1, 5, -1, -0.5, -4.5, -4, 4, 0, -0.25, 3, 2" };
    ///         var block = new FFTBlock
    ///         {
    ///             Mode = ManagedFFTModeEnum.UseLookupTable
    ///         };
    ///         
    ///         signal.ConnectTo(block);
    ///         signal.Execute();
    ///         
    ///         Console.WriteLine("Abs: " + block.OutputNodes[0].Object.ToString(1));
    ///         Console.WriteLine("FFT: " + block.OutputNodes[1].Object.ToString(1));
    ///     </code>
    /// </example>
    /// </summary>
    [Serializable]
    public class FFTBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FFTBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);
        }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return "FFT"; }
        }

        /// <summary>
        /// Description of the block
        /// </summary>
        public override string Description
        {
            get { return Resources.FFTDescription; }
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
            OutputNodes[1].Object.Clear();
            foreach (var inputSignal in inputNode.Object)
            {
                var fft = WaveMath.UpSample(inputSignal.Samples);
                ManagedFFT.Instance.FFT(ref fft, true, Mode);
                var abs = WaveMath.AbsFromComplex(fft, 0, fft.Length/2);
                abs = WaveMath.Normalize(abs, abs.Length);

                var absSignal = new Signal(abs)
                {
                    Start = 0,
                    Finish = abs.Length - 1
                };
                var fftSignal = new Signal(fft)
                {
                    Start = 0,
                    Finish = fft.Length - 1,
                    IsComplex = true,
                    SamplingInterval = fft.Length
                };

                OutputNodes[0].Object.Add(absSignal);
                OutputNodes[1].Object.Add(fftSignal);
            }
            
            if (Cascade && OutputNodes[0].ConnectingNode != null)
                OutputNodes[0].ConnectingNode.Root.Execute();
            if (Cascade && OutputNodes[1].ConnectingNode != null)
                OutputNodes[1].ConnectingNode.Root.Execute();
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
                                       new BlockOutputNode(ref root, Resources.AbsoluteValue, "Abs"),
                                       new BlockOutputNode(ref root, Resources.FFT, "FFT")
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