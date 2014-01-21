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
using System.ComponentModel;
using System.Linq;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.Functions;
using WaveletStudio.Properties;
using WaveletStudio.Wavelet;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// <para>The DWT block decomposes a signal using the specified wavelet function.</para>
    /// <para>Image: http://i.imgur.com/eB1KiuV.png </para>
    /// <para>InOutGraph: http://i.imgur.com/4vDa0Ta.png </para>
    /// <para>Inputs: This block has only one input: the signal to perform the wavelet decomposition.</para>
    /// <para>Outputs: This block has four outputs:</para>
    /// <para>Outputs:  0 – Approximation (Apx): Decomposition approximation levels</para>
    /// <para>Outputs:  1 – Details (Det): Decomposition details levels</para>
    /// <para>Outputs:  2 – Reconstruction (Rc): Reconstruction of the signal</para>
    /// <para>Outputs:  3 – All: List with all the previous outputs.</para>
    /// 
    /// <example>
    ///     <code>
    ///         var signal = new ImportFromTextBlock { Text = "0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1.1, 0.9, 1, 0, 0, 0, 0, 1, 1, 1, 1" };
    ///         var block = new DWTBlock
    ///         {
    ///             WaveletName = "haar",
    ///             Level = 1,
    ///             Rescale = true
    ///         };
    ///         
    ///         signal.ConnectTo(block);
    ///         signal.Execute();
    ///         
    ///         Console.WriteLine("APX: " + block.OutputNodes[0].Object.ToString(1));
    ///         Console.WriteLine("DET: " + block.OutputNodes[1].Object.ToString(1));
    ///         Console.WriteLine("RC:  " + block.OutputNodes[2].Object.ToString(1));            
    ///     </code>
    /// </example>
    /// </summary>
    [Serializable]
    public class DWTBlock : BlockBase
    {
        private MotherWavelet _motherWavelet;

        /// <summary>
        /// Constructor
        /// </summary>
        public DWTBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);

            Level = 1;
            ExtensionMode = SignalExtension.ExtensionMode.SymmetricHalfPoint;
            WaveletNameList = CommonMotherWavelets.Wavelets.Values.Select(it => ((string)it[0]).Split('|')[1] + " (" + ((string)it[0]).Split('|')[0] + ")").ToList();
            WaveletName = WaveletNameList.ElementAt(0);
        }

        /// <summary>
        /// Name
        /// </summary>
        public override string Name { get { return "DWT"; } }

        /// <summary>
        /// Description
        /// </summary>
        public override string Description { get { return Resources.DWTDescription; } }

        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Transform; } }

        /// <summary>
        /// Available wavelet functions
        /// </summary>
        public List<string> WaveletNameList { get; set; }

        private string _waveletName;
        /// <summary>
        /// <para>One of following wavelet functions name:</para>
        /// <para>- Coiflet wavelets: coif1, coif2, coif3, coif4, coif5</para>
        /// <para>- Daubechies wavelets: db2, db3, db4, db5, db6, db7, db8, db9, db10</para>
        /// <para>- Haar wavelet (db1): haar</para>
        /// <para>- Discreete Meyer wavelet: dmeyer</para>
        /// <para>- Symlet wavelet: sym2, sym3, sym4, sym5, sym6, sym7, sym8</para>
        /// </summary>
        [Parameter]
        [TypeConverter(typeof(WaveletNamesTypeConverter))]
        public string WaveletName
        {
            get
            {
                return _waveletName;
            }
            set
            {
                if (!LoadWavelets(value))
                {
                    throw new Exception(string.Format(Resources.WaveletNameNotFound, value));
                }
                _waveletName = value;
            }
        }

        private bool LoadWavelets(string waveletName)
        {
            if (waveletName.Contains("|"))
                waveletName = waveletName.Split('|')[0];
            else if (waveletName.Contains("("))
                waveletName = waveletName.Split('(')[1].Replace(")", "");

            if (_motherWavelet == null || waveletName != WaveletName)
            {
                var motherWavelet = CommonMotherWavelets.GetWaveletFromName(waveletName);
                if (motherWavelet == null)
                    return false;
                _motherWavelet = motherWavelet;
            }
            return true;
        }

        /// <summary>
        /// Number of levels of decomposition.
        /// </summary>
        [Parameter]
        public int Level { get; set; }
        
        /// <summary>
        /// Rescale outputs in the "ALL" output to the original signal size, reverting the downsample and extension effects. Warning: the "ALL" output will be modified, and the signal in this output cannot be reconstructed. Default value is false.
        /// </summary>
        [Parameter]
        public bool Rescale { get; set; }
        
        /// <summary>
        /// Extension mode
        /// </summary>
        [Parameter]
        public SignalExtension.ExtensionMode ExtensionMode { get; set; }

        /// <summary>
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            var connectingNode = InputNodes[0].ConnectingNode as BlockOutputNode;
            if (connectingNode == null || connectingNode.Object == null)
                return;
            var signalIndex = 0;
            OutputNodes[0].Object.Clear();
            OutputNodes[1].Object.Clear();
            OutputNodes[2].Object.Clear();
            OutputNodes[3].Object.Clear();
            foreach (var signal in connectingNode.Object)
            {
                signalIndex++;
                var name = signal.Name;
                if (!string.IsNullOrEmpty(name))
                    name += " - ";
                else
                    name = Resources.Signal + " " + signalIndex + " - ";
                var decompositionLevels = DWT.ExecuteDWT(signal, _motherWavelet, Level, ExtensionMode);
                foreach (var level in decompositionLevels)
                {
                    var apxSignal = signal.Copy();
                    apxSignal.Name = name + Resources.ApproximationLevel +" " + (level.Index + 1);
                    apxSignal.Samples = level.Approximation;
                    OutputNodes[0].Object.Add(apxSignal);
                    if (Rescale)
                    {
                        var rescaledApx = RescaleSignal(apxSignal, signal, level);
                        OutputNodes[3].Object.Add(rescaledApx);
                    }
                    else
                    {
                        OutputNodes[3].Object.Add(apxSignal);   
                    }                    

                    var detSignal = signal.Copy();
                    detSignal.Name = name + Resources.DetailsLevel + " " + (level.Index + 1);
                    detSignal.Samples = level.Details;
                    OutputNodes[1].Object.Add(detSignal);
                    if (Rescale)
                    {
                        var rescaledDet = RescaleSignal(detSignal, signal, level);
                        OutputNodes[3].Object.Add(rescaledDet);
                    }
                    else
                    {
                        OutputNodes[3].Object.Add(detSignal);   
                    }                    
                }
                var reconstruction = DWT.ExecuteIDWT(decompositionLevels, _motherWavelet, Level);
                var recSignal = signal.Copy();
                recSignal.Name = name + Resources.Reconstruction;
                recSignal.Samples = reconstruction;
                OutputNodes[2].Object.Add(recSignal);
                OutputNodes[3].Object.Add(recSignal);
            }
            if(!Cascade)
                return;
            foreach (var output in OutputNodes.Where(output => output.ConnectingNode != null))
            {
                output.ConnectingNode.Root.Execute();
            }            
        }

        private static Signal RescaleSignal(Signal decomposition, Signal signal, DecompositionLevel level)
        {
            var rescaled = WaveMath.InterpolateCubic(decomposition, (uint)Math.Pow(2, level.Index + 1));
            if (rescaled.SamplesCount < signal.SamplesCount)
            {
                var extensionSize = signal.SamplesCount - rescaled.SamplesCount;
                int right;
                var left = right = extensionSize/2;
                if (left + right != extensionSize)
                {
                    left++;
                }

                SignalExtension.Extend(ref rescaled, SignalExtension.ExtensionMode.SmoothPadding0, left, right);
            }
            else
            {
                rescaled.Samples = SignalExtension.Deextend(rescaled.Samples, signal.SamplesCount);
            }
            rescaled.CustomPlot = null;
            rescaled.Finish = signal.Finish;
            rescaled.SamplingRate = signal.SamplingRate;
            rescaled.SamplingInterval = signal.SamplingInterval;
            return rescaled;
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
                                       new BlockOutputNode(ref root, Resources.Approximation, "Apx"),
                                       new BlockOutputNode(ref root, Resources.Details, "Det"),
                                       new BlockOutputNode(ref root, Resources.Reconstruction, "Rc"),
                                       new BlockOutputNode(ref root, Resources.All, Resources.All),
                                   };
        }

        /// <summary>
        /// Clone the block, including the template
        /// </summary>
        /// <returns></returns>
        public override BlockBase Clone()
        {
            var block = (DWTBlock)MemberwiseClone();
            block.Execute();            
            return block;
        }

        /// <summary>
        /// Clones this block but mantains the links
        /// </summary>
        /// <returns></returns>
        public override BlockBase CloneWithLinks()
        {
            var block = (DWTBlock)MemberwiseCloneWithLinks();
            block.Execute();
            return block;
        }
    }
}
