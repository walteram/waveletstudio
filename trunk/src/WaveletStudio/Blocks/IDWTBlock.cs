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
    /// Inverse Wavelet decomposition block
    /// </summary>
    [Serializable]
    public class IDWTBlock : BlockBase
    {
        private MotherWavelet _motherWavelet;

        /// <summary>
        /// Constructor
        /// </summary>
        public IDWTBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);

            Level = 1;
            WaveletNameList = CommonMotherWavelets.Wavelets.Values.Select(it => ((string)it[0]).Split('|')[1] + " (" + ((string)it[0]).Split('|')[0] + ")").ToList();
            WaveletName = WaveletNameList.ElementAt(0);
        }

        /// <summary>
        /// Name
        /// </summary>
        public override string Name { get { return "IDWT"; } }

        /// <summary>
        /// Description
        /// </summary>
        public override string Description { get { return Resources.IDWTDescription; } }

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
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            var inputNode1 = InputNodes[0].ConnectingNode as BlockOutputNode;
            var inputNode2 = InputNodes[1].ConnectingNode as BlockOutputNode;
            if (inputNode1 == null || inputNode1.Object == null || inputNode2 == null || inputNode2.Object == null)
                return;

            OutputNodes[0].Object.Clear();
            var approximations = inputNode1.Object;
            var details = inputNode2.Object;

            var tempLevels = new List<DecompositionLevel>();
            var outputs = new List<Signal>();

            var currentName = "";
            for (var i = 0; i < approximations.Count; i++)
            {
                var name = approximations[i].Name != null ? approximations[i].Name.Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries)[0] : Resources.Signal;
                if (name != currentName && currentName != "")
                {
                    outputs.Add(new Signal(DWT.ExecuteIDWT(tempLevels, _motherWavelet, Level)){Name = name});
                    tempLevels = new List<DecompositionLevel>();
                }               
                currentName = name;
                if (approximations[i].Samples != null && i < details.Count && details[i].Samples != null)
                {
                    var level = new DecompositionLevel
                    {
                        Approximation = approximations[i].Samples,
                        Details = details[i].Samples,
                        Index = i
                    };
                    tempLevels.Add(level);
                }                
                if (i != approximations.Count - 1) 
                    continue;
                outputs.Add(new Signal(DWT.ExecuteIDWT(tempLevels, _motherWavelet, Level)) { Name = name });
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
            root.InputNodes = new List<BlockInputNode>
                                  {
                                      new BlockInputNode(ref root, Resources.Approximation, "Apx"),
                                      new BlockInputNode(ref root, Resources.Details, "Det")
                                  };
            root.OutputNodes = new List<BlockOutputNode>
                                   {
                                       new BlockOutputNode(ref root, Resources.Signal, Resources.Out)
                                   };
        }

        /// <summary>
        /// Clone the block, including the template
        /// </summary>
        /// <returns></returns>
        public override BlockBase Clone()
        {
            var block = (IDWTBlock)MemberwiseClone();
            block.Execute();            
            return block;
        }

        /// <summary>
        /// Clones this block but mantains the links
        /// </summary>
        /// <returns></returns>
        public override BlockBase CloneWithLinks()
        {
            var block = (IDWTBlock)MemberwiseCloneWithLinks();
            block.Execute();
            return block;
        }
    }
}
