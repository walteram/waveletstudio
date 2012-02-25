﻿/*  Wavelet Studio Signal Processing Library - www.waveletstudio.net
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
using System.Linq;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.Properties;
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
            SamplingRate = 32768;
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
        public List<string> TemplateNameList { get; set; }

        private string _templateName;
        /// <summary>
        /// Template to be used
        /// </summary>
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
                    throw new Exception(string.Format(Resources.TemplateNotFound, value));
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
            OutputNodes[0].Object = new List<Signal> { _template.ExecuteSampler() };
            if (Cascade && OutputNodes[0].ConnectingNode != null)
                OutputNodes[0].ConnectingNode.Root.Execute();            
        }

        /// <summary>
        /// Creates the input and output nodes
        /// </summary>
        /// <param name="root"></param>
        protected override sealed void CreateNodes(ref BlockBase root)
        {
            root.OutputNodes = new List<BlockOutputNode> {new BlockOutputNode(ref root, Resources.Signal, "S")};
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
        /// Gets the name of the class
        /// </summary>
        /// <returns></returns>
        public override string GetAssemblyClassName()
        {
            return _template.GetAssemblyClassName();
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
