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
using WaveletStudio.Functions;
using WaveletStudio.Properties;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// <para>The Convolution block convolves the signal of the first input with the signal of second input (filter). Both the signal and filter can be lists. Each signal in the first list will be convolved with the correspondent filter in the second. If the number of filters in the filter list is lesser than the number of filters, the first filter will be used.</para>
    /// <para>Inputs: This block has two inputs: the signal list and the filter list.</para>
    /// <para>Outputs: This block has only one output: the result of convolution between the inputs.</para>
    /// <para>Image: http://i.imgur.com/kUEo44z.png </para>
    /// <para>InOutGraph: http://i.imgur.com/rvyncoK.png </para>
    /// <example>
    ///     <code>
    ///         var signal = new ImportFromTextBlock { Text = "0, 0, 1, 1, 1, 1, 0, 0" };
    ///         var filter = new ImportFromTextBlock { Text = "0, 0, 1, 0.75, 0.5, 0.25, 0, 0" };
    ///         var block = new ConvolutionBlock
    ///         {
    ///             FFTMode = ManagedFFTModeEnum.UseLookupTable,
    ///             ReturnOnlyValid = false
    ///         };
    ///
    ///         signal.OutputNodes[0].ConnectTo(block.InputNodes[0]);
    ///         filter.OutputNodes[0].ConnectTo(block.InputNodes[1]);
    ///         
    ///         var blockList = new BlockList { signal, filter, block };
    ///         blockList.ExecuteAll();
    ///         Console.WriteLine(block.Output[0].ToString(1));
    ///         
    ///         //Console Output:
    ///         //0.0 0.0 0.0 0.0 1.0 1.8 2.3 2.5 1.5 0.8 0.3 0.0 0.0 0.0 0.0
    ///     </code>
    /// </example>
    /// </summary>
    [Serializable]
    public class ConvolutionBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ConvolutionBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);
            ConvolutionMode = ConvolutionModeEnum.ManagedFFT;
            ReturnOnlyValid = false;
        }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return Resources.Convolution; }
        }

        /// <summary>
        /// Description of the block
        /// </summary>
        public override string Description
        {
            get { return Resources.ConvolutionDescription; }
        }

        /// <summary>
        /// The convolution mode to be used.
        /// </summary>
        //[Parameter] - Normal mode is too slow! So i removed from user interface
        public ConvolutionModeEnum ConvolutionMode { get; set; }

        /// <summary>
        /// The FFT mode to be used on convolution.
        /// </summary>
        [Parameter]
        public ManagedFFTModeEnum FFTMode { get; set; }

        /// <summary>
        /// If true, the block will return only the central part of the convolution resulting in a new signal with (S – F + 1) samples (S = size of signal, F = size of filter). If false, all the convolution will returned and resulting in a new signal with (S + F – 1) samples. Default value is false.
        /// </summary>
        [Parameter]
        public bool ReturnOnlyValid { get; set; }

        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Operation; } }

        /// <summary>
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            var inputNode1 = InputNodes[0].ConnectingNode as BlockOutputNode;
            var inputNode2 = InputNodes[1].ConnectingNode as BlockOutputNode;
            if (inputNode1 == null || inputNode1.Object == null || inputNode2 == null || inputNode2.Object == null)
                return;

            var outputs = new List<Signal>();
            var signals = inputNode1.Object;
            var filters = inputNode2.Object;
            for (var i = 0; i < signals.Count; i++)
            {
                var signal = signals[i];
                if (filters.Count == 0)
                {
                    outputs.Add(signal.Clone());
                    continue;
                }
                var output = signal.Copy();
                var filterIndex = i < filters.Count ? i : 0;                
                output.Samples = WaveMath.Convolve(ConvolutionMode, signal.Samples, filters[filterIndex].Samples, ReturnOnlyValid, 0, FFTMode);
                outputs.Add(output);
            }
            OutputNodes[0].Object = outputs;
            if (Cascade && OutputNodes[0].ConnectingNode != null)
                OutputNodes[0].ConnectingNode.Root.Execute();
        }

        /// <summary>
        /// Creates the input and output nodes
        /// </summary>
        /// <param name="root"></param>
        protected override sealed void CreateNodes(ref BlockBase root)
        {
            root.InputNodes = BlockInputNode.CreateDoubledInput(ref root);
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
