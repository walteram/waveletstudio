using System;
using System.Collections.Generic;
using System.Linq;
using WaveletStudio.Blocks.CustomAttributes;
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
            WaveletNameList = CommonMotherWavelets.Wavelets.Values.Select(it => (string)it[0]).ToList();
            WaveletName = WaveletNameList.ElementAt(0);
        }

        /// <summary>
        /// Name
        /// </summary>
        public override string Name { get { return "DWT"; } }

        /// <summary>
        /// Description
        /// </summary>
        public override string Description { get { return "Wavelet decomposition block"; } }

        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Operation; } }

        /// <summary>
        /// Available wavelet functions
        /// </summary>
        public List<string> WaveletNameList { get; set; }

        private string _waveletName;
        /// <summary>
        /// Wavelet function to be used
        /// </summary>
        [Parameter]
        public string WaveletName
        {
            get
            {
                return _waveletName;
            }
            set
            {
                if(value.Contains("|")) 
                    value = value.Split('|')[0];
                if (!LoadWavelets(value))
                {
                    throw new Exception("The wavelet " + value + " does not exist.");
                }
                _waveletName = value;
            }
        }

        private bool LoadWavelets(string waveletName)
        {
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
                    name = "Signal " + signalIndex + " - ";
                var decompositionLevels = DWT.ExecuteDWT(signal, _motherWavelet, Level, ExtensionMode);
                foreach (var level in decompositionLevels)
                {
                    var appSignal = signal.Copy();
                    appSignal.Name = name + "Approximation Level " + (level.Index + 1);
                    appSignal.Samples = level.Approximation;
                    OutputNodes[0].Object.Add(appSignal);
                    OutputNodes[3].Object.Add(appSignal);

                    var detSignal = signal.Copy();
                    detSignal.Name = name + "Details Level " + (level.Index + 1);
                    detSignal.Samples = level.Details;
                    OutputNodes[1].Object.Add(detSignal);
                    OutputNodes[3].Object.Add(detSignal);
                }
                var reconstruction = DWT.ExecuteIDWT(decompositionLevels, _motherWavelet, Level);
                var recSignal = signal.Copy();
                recSignal.Name = name + "Reconstruction";
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
            root.InputNodes = new List<BlockInputNode> { new BlockInputNode(ref root, "Signal", "In") };
            root.OutputNodes = new List<BlockOutputNode>
                                   {
                                       new BlockOutputNode(ref root, "Aproximation", "Apx"),
                                       new BlockOutputNode(ref root, "Details", "Det"),
                                       new BlockOutputNode(ref root, "Reconstruction", "Rc"),
                                       new BlockOutputNode(ref root, "All", "All"),
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
