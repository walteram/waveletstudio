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
using WaveletStudio.Properties;
using WaveletStudio.Wavelet;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Wavelet decomposition block
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
        /// Wavelet function to be used
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
        /// Number of levels
        /// </summary>
        [Parameter]
        public int Level { get; set; }

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
                    var appSignal = signal.Copy();
                    appSignal.Name = name + Resources.ApproximationLevel +" " + (level.Index + 1);
                    appSignal.Samples = level.Approximation;
                    OutputNodes[0].Object.Add(appSignal);
                    OutputNodes[3].Object.Add(appSignal);

                    var detSignal = signal.Copy();
                    detSignal.Name = name + Resources.DetailsLevel + " " + (level.Index + 1);
                    detSignal.Samples = level.Details;
                    OutputNodes[1].Object.Add(detSignal);
                    OutputNodes[3].Object.Add(detSignal);
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

        /// <summary>
        /// Creates the input and output nodes
        /// </summary>
        /// <param name="root"></param>
        protected override sealed void CreateNodes(ref BlockBase root)
        {
            root.InputNodes = new List<BlockInputNode> { new BlockInputNode(ref root, Resources.Signal, Resources.In) };
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
