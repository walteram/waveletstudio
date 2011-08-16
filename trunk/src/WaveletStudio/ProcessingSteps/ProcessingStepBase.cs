namespace WaveletStudio.ProcessingSteps
{
    public abstract class ProcessingStepBase
    {
        public abstract int Key { get; }

        public abstract string Name { get; }

        public abstract ProcessingTypeEnum ProcessingType { get; }

        public abstract int PreferredOrder { get; }

        public abstract Signal Signal { get; set; }

        public abstract void Process(ProcessingStepBase previousProcess);        

        public enum ProcessingTypeEnum
        {
            CreateSignal,
            InsertDisturbance,
            Process,
            Export
        }
    }
}
