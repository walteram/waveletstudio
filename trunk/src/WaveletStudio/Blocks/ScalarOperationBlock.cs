using System;
using WaveletStudio.Blocks.CustomAttributes;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Executes a scalar operation in a signal
    /// </summary>
    [SingleInputOutputBlock]
    [Serializable]
    public class ScalarOperationBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ScalarOperationBlock()
        {
            BlockBase root = this;
            InputNodes.Add(new BlockInputNode(ref root, "Signal", "In"));
            OutputNodes.Add(new BlockOutputNode(ref root, "Output", "Out"));
        }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return "Scalar Operation"; }
        }

        private string _description = "Multiply, add or subtract or divide the samples of a signal by a scalar number";

        /// <summary>
        /// Description of the block
        /// </summary>
        public override string Description
        {
            get { return _description; }
        }

        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Operation; } }

        private OperationEnum _operation;

        /// <summary>
        /// Math operation to be used
        /// </summary>
        [Parameter]
        public OperationEnum Operation
        {
            get { return _operation; }
            set
            {
                SetOperationDescription();
                _operation = value;
            }
        }

        private double _scalar = 1;

        /// <summary>
        /// Scalar value
        /// </summary>
        [Parameter]
        public double Value
        {
            get { return _scalar; }
            set
            {
                SetOperationDescription();
                _scalar = value;
            }
        }        

        /// <summary>
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            var connectingNode = InputNodes[0].ConnectingNode as BlockOutputNode;
            if (connectingNode == null || connectingNode.Object == null)
                return;
            var input = connectingNode.Object;
            var output = input.Copy();
            switch (Operation)
            {
                case OperationEnum.Multiply:
                    output.Samples = input.Samples * Value;
                    break;
                case OperationEnum.Sum:
                    output.Samples = input.Samples + Value;
                    break;
                case OperationEnum.Subtract:
                    output.Samples = input.Samples - Value;
                    break;
                case OperationEnum.Divide:
                    output.Samples = input.Samples / Value;
                    break;
            }
            OutputNodes[0].Object = output;
            if (Cascade && OutputNodes[0].ConnectingNode != null)
                OutputNodes[0].ConnectingNode.Root.Execute();
        }

        private void SetOperationDescription()
        {
            string operation;
            switch (Operation)
            {
                case OperationEnum.Sum:
                    operation = "+";
                    break;
                case OperationEnum.Multiply:
                    operation = "*";
                    break;
                case OperationEnum.Subtract:
                    operation = "-";
                    break;
                default:
                    operation = "/";
                    break;
            }
            _description = "y(x) = y(x) " + operation + " " + string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.####}", Value);
        }

        /// <summary>
        /// Operation
        /// </summary>
        public enum OperationEnum
        {
            /// <summary>
            /// Multiply
            /// </summary>
            Multiply,
            /// <summary>
            /// Sum
            /// </summary>
            Sum,
            /// <summary>
            /// Subtract
            /// </summary>
            Subtract,
            /// <summary>
            /// Divide
            /// </summary>
            Divide
        }

        /// <summary>
        /// Clones this block
        /// </summary>
        /// <returns></returns>
        public override BlockBase Clone()
        {
            return MemberwiseClone();            
        }
    }
}
