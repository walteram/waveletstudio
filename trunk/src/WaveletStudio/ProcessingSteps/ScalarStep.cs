namespace WaveletStudio.ProcessingSteps
{
    public class ScalarStep : ProcessingStepBase
    {
        public static int StepKey { get { return 2; } }

        public override int Key { get { return StepKey; } }

        public override string Name
        {
            get { return "Multiply, add or subtract or divide the samples of a signal by a scalar number"; }
        }

        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Process; } }

        public override int PreferredOrder { get { return 1; } }

        public override Signal Signal { get; set; }

        public double Scalar { get; set; }

        public OperationEnum Operation { get; set; }

        public override void Process(ProcessingStepBase previousProcess)
        {
            Signal = previousProcess.Signal.Clone();
            for (var i = 0; i < Signal.Samples.Length; i++)
            {
                var sample = Signal.Samples.GetValue(i);
                if (Operation == OperationEnum.Multiply)
                    sample *= Scalar;
                else if (Operation == OperationEnum.Add)
                    sample += Scalar;
                else if (Operation == OperationEnum.Subtract)
                    sample -= Scalar;
                else if (Operation == OperationEnum.Divide)
                    sample /= Scalar;
                Signal.Samples.SetValue(sample, i);
            }            
        }

        public enum OperationEnum
        {
            Multiply,
            Add,
            Subtract,
            Divide
        }
    }
}
