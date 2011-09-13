using System;
using System.Collections.Generic;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.Functions;

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
            CreateNodes(ref root);
        }

        protected override sealed void CreateNodes(ref BlockBase root)
        {
            root.InputNodes = new List<BlockInputNode> {new BlockInputNode(ref root, "Signal", "In")};
            root.OutputNodes = new List<BlockOutputNode> {new BlockOutputNode(ref root, "Output", "Out")};
        }

        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return "Scalar"; }
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
                _operation = value;
                SetOperationDescription();
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
                _scalar = value;
                SetOperationDescription();
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
                    output.Samples = WaveMath.Multiply(input.Samples, Value);
                    break;
                case OperationEnum.Sum:
                    output.Samples = WaveMath.Add(input.Samples, Value);
                    break;
                case OperationEnum.Subtract:
                    output.Samples = WaveMath.Add(input.Samples, -Value);
                    break;
                case OperationEnum.Divide:
                    output.Samples = WaveMath.Multiply(input.Samples, 1/Value);
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

        /// <summary>
        /// Clones this block but mantains the links
        /// </summary>
        /// <returns></returns>
        public override BlockBase CloneWithLinks()
        {
            return MemberwiseCloneWithLinks();
        }
    }
}
