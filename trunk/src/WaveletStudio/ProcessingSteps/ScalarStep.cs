namespace WaveletStudio.ProcessingSteps
{
    public class ScalarStep : ProcessingStepBase
    {
        public static int StepKey { get { return 2; } }

        public override int Key { get { return StepKey; } }

        public override string Name
        {
            get { return "Scalar Operation"; }
        }

        private string _description = "Multiply, add or subtract or divide the samples of a signal by a scalar number";
        public override string Description
        {
            get { return _description; }
        }

        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Operation; } }

        public override int PreferredOrder { get { return 1; } }

        public override Signal Signal { get; set; }

        private double _scalar = 1;
        public double Scalar
        {
            get { return _scalar; }
            set
            {
                SetOperationDescription();
                _scalar = value;
            }
        }

        private OperationEnum _operation;
        public OperationEnum Operation
        {
            get { return _operation; }
            set 
            { 
                SetOperationDescription(); 
                _operation = value; 
            }
        }

        private void SetOperationDescription()
        {
            string operation;
            if (Operation == OperationEnum.Add)
                operation = "+";
            else if (Operation == OperationEnum.Multiply)
                operation = "*";
            else if (Operation == OperationEnum.Subtract)
                operation = "-";
            else
                operation = "/";
            _description = "y(x) = y(x) " + operation + " " + string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.####}", Scalar);
        }

        public override void Process(ProcessingStepBase previousProcess)
        {
            Signal = previousProcess.Signal.Clone();
            SetOperationDescription(); 
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

        public override ProcessingStepBase Clone()
        {
            var step = (ScalarStep)MemberwiseClone();
            return step;
        }
    }
}
