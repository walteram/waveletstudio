using System;
using System.Collections.Generic;
using System.Linq;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.SignalGeneration;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Generates a signal based on a template
    /// </summary>
    [SingleInputOutputBlock]
    [Serializable]
    public class GenerateSignalBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GenerateSignalBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);

            TemplateNameList = new List<string>();
            TemplateNameList.AddRange(Utils.GetTypes("WaveletStudio.SignalGeneration").Select(it => it.Name).ToArray());
            Amplitude = 1;
            Frequency = 60;
            Phase = 0;
            Offset = 0;
            Start = 0;
            Finish = 1;
            SamplingRate = 44100;
            IgnoreLastSample = false;
            TemplateName = TemplateNameList.ElementAt(0);
        }

        /// <summary>
        /// Name
        /// </summary>
        public override string Name { get { return _template.Name; } }

        /// <summary>
        /// Description
        /// </summary>
        public override string Description  { get { return _template.Description; } }

        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.CreateSignal; } }

        /// <summary>
        /// Available templates
        /// </summary>
        public List<string> TemplateNameList { get; private set; }

        private string _templateName;
        /// <summary>
        /// Template to be used
        /// </summary>
        [Parameter]
        public string TemplateName
        {
            get
            {
                return _templateName;
            }
            set
            {
                if (!LoadTemplate(value))
                {
                    throw new Exception("The template " + value + " does not exist.");   
                }
                _templateName = value;                
            }
        }

        /// <summary>
        /// Amplitude of the signal
        /// </summary>
        [Parameter]
        public double Amplitude { get; set; }

        /// <summary>
        /// Frequency of the signal
        /// </summary>
        [Parameter]
        public double Frequency { get; set; }

        /// <summary>
        /// The initial angle of function at its origin
        /// </summary>
        [Parameter]
        public double Phase { get; set; }

        /// <summary>
        /// Distance from the origin
        /// </summary>
        [Parameter]
        public double Offset { get; set; }

        /// <summary>
        /// Start of the signal in time
        /// </summary>
        [Parameter]
        public double Start { get; set; }

        /// <summary>
        /// Finish of the signal in time
        /// </summary>
        [Parameter]
        public double Finish { get; set; }

        /// <summary>
        /// Sampling rate used on sampling
        /// </summary>
        [Parameter]
        public int SamplingRate { get; set; }

        /// <summary>
        /// Defines it the last sample will be included in signal
        /// </summary>
        [Parameter]
        public bool IgnoreLastSample { get; set; }

        private CommonSignalBase _template;

        /// <summary>
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            LoadTemplate(TemplateName);
            OutputNodes[0].Object = _template.ExecuteSampler();
            if (Cascade && OutputNodes[0].ConnectingNode != null)
                OutputNodes[0].ConnectingNode.Root.Execute();            
        }

        protected override sealed void CreateNodes(ref BlockBase root)
        {
            root.OutputNodes = new List<BlockOutputNode> {new BlockOutputNode(ref root, "Signal", "S")};
        }

        private bool LoadTemplate(string templateName)
        {
            if (_template == null || templateName != TemplateName)
            {
                var templateType = Utils.GetType("WaveletStudio.SignalGeneration." + templateName);
                if (templateType == null)                
                    return false;                
                _template = (CommonSignalBase)Activator.CreateInstance(templateType);   
            }            
            _template.Amplitude = Amplitude;
            _template.Frequency = Frequency;
            _template.Phase = Phase;
            _template.Offset = Offset;
            _template.Start = Start;
            _template.Finish = Finish;
            _template.SamplingRate = SamplingRate;
            _template.IgnoreLastSample = IgnoreLastSample;
            return true;
        }

        /// <summary>
        /// Clone the block, including the template
        /// </summary>
        /// <returns></returns>
        public override BlockBase Clone()
        {
            var block = (GenerateSignalBlock)MemberwiseClone();
            block._template = _template.Clone();            
            block.Execute();            
            return block;
        }

        /// <summary>
        /// Clones this block but mantains the links
        /// </summary>
        /// <returns></returns>
        public override BlockBase CloneWithLinks()
        {
            var block = (GenerateSignalBlock)MemberwiseCloneWithLinks();
            block._template = _template.Clone();
            block.Execute();
            return block;
        }
    }
}
