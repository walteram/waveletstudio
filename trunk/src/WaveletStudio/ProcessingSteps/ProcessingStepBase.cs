using System;

namespace WaveletStudio.ProcessingSteps
{
    public abstract class ProcessingStepBase
    {
        public int Index { get; set; }

        public Guid Id { get; private set; }
        
        public abstract int Key { get; }

        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract ProcessingTypeEnum ProcessingType { get; }

        public abstract int PreferredOrder { get; }

        public abstract Signal Signal { get; set; }

        public abstract void Process(ProcessingStepBase previousProcess);

        public object Tag { get; set; }

        public enum ProcessingTypeEnum
        {
            CreateSignal,
            Operation,
            InsertDisturbance,
            Process,
            Export
        }

        protected ProcessingStepBase()
        {
            Id = Guid.NewGuid();
        }

        public abstract ProcessingStepBase Clone();
    }
}
