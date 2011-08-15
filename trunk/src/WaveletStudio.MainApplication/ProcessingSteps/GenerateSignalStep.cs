using System;
using WaveletStudio.SignalGeneration;

namespace WaveletStudio.MainApplication.ProcessingSteps
{
    public class GenerateSignalStep : ProcessingStepBase
    {
        public static int StepKey { get { return 1; } }

        public override int Key { get { return StepKey; } }

        public override string Name
        {
            get { return "Generate Signal from Template";  }
        }

        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.CreateSignal; } }

        public override int PreferredOrder { get { return 0; } }

        public override Signal Signal { get; set; }

        public CommonSignalBase Template { get; set; }

        public override void Process(ProcessingStepBase previousProcess)
        {
            Signal = Template.ExecuteSampler();
        }
    }
}
