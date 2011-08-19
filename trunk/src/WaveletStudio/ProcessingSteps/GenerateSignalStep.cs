using System;
using System.Collections.Generic;
using System.Linq;
using WaveletStudio.SignalGeneration;

namespace WaveletStudio.ProcessingSteps
{
    public class GenerateSignalStep : ProcessingStepBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GenerateSignalStep()
        {
            TemplateNameList = new List<string>();
            TemplateNameList.AddRange(Utils.GetTypes("WaveletStudio.SignalGeneration").Select(it => it.Name).ToArray());
            TemplateName = TemplateNameList.ElementAt(0);

            Amplitude = 1;
            Frequency = 60;
            Phase = 0;
            Offset = 0;
            Start = 0;
            Finish = 1;
            SamplingRate = 44100;
            IgnoreLastSample = false;

            LoadTemplate();
        }

        public static int StepKey { get { return 1; } }

        public override int Key { get { return StepKey; } }

        public override string Name
        {
            get { return Template.Name; }
        }

        public override string Description
        {
            get { return Template.Description; }
        }

        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.CreateSignal; } }

        public override int PreferredOrder { get { return 0; } }

        public override Signal Signal { get; set; }

        public CommonSignalBase Template { get; set; }

        /// <summary>
        /// Available templates
        /// </summary>
        public List<string> TemplateNameList { get; private set; }

        /// <summary>
        /// Template to be used
        /// </summary>
        public string TemplateName { get; set; }        

        /// <summary>
        /// Amplitude of the signal
        /// </summary>
        public double Amplitude { get; set; }

        /// <summary>
        /// Frequency of the signal
        /// </summary>
        public double Frequency { get; set; }

        /// <summary>
        /// The initial angle of function at its origin
        /// </summary>
        public double Phase { get; set; }

        /// <summary>
        /// Distance from the origin
        /// </summary>
        public double Offset { get; set; }

        /// <summary>
        /// Start of the signal in time
        /// </summary>
        public double Start { get; set; }

        /// <summary>
        /// Finish of the signal in time
        /// </summary>
        public double Finish { get; set; }

        /// <summary>
        /// Sampling rate used on sampling
        /// </summary>
        public int SamplingRate { get; set; }

        /// <summary>
        /// Defines it the last sample will be included in signal
        /// </summary>
        public bool IgnoreLastSample { get; set; }

        public override void Process(ProcessingStepBase previousProcess)
        {
            LoadTemplate();
            Signal = Template.ExecuteSampler();
        }

        public void LoadTemplate()
        {
            var signalType = Utils.GetType("WaveletStudio.SignalGeneration." + TemplateName);
            Template = (CommonSignalBase)Activator.CreateInstance(signalType);
            Template.Amplitude = Amplitude;
            Template.Frequency = Frequency;
            Template.Phase = Phase;
            Template.Offset = Offset;
            Template.Start = Start;
            Template.Finish = Finish;
            Template.SamplingRate = SamplingRate;
            Template.IgnoreLastSample = IgnoreLastSample;
        }

        public override ProcessingStepBase Clone()
        {
            var step = (GenerateSignalStep)MemberwiseClone();
            step.Template = Template.Clone();
            Signal = Template.ExecuteSampler();
            return step;
        }

        public static GenerateSignalStep Clone(ProcessingStepBase step)
        {
            var casted = (GenerateSignalStep) step;
            return (GenerateSignalStep)casted.Clone();
        }
    }
}
