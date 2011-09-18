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

        private string _name = "Scalar";

        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return _name; }
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

        private WaveMath.OperationEnum _operation;

        /// <summary>
        /// Math operation to be used
        /// </summary>
        [Parameter]
        public WaveMath.OperationEnum Operation
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
            SetOperationDescription();
            var connectingNode = InputNodes[0].ConnectingNode as BlockOutputNode;
            if (connectingNode == null || connectingNode.Object == null)
                return;

            OutputNodes[0].Object.Clear();
            foreach (var signal in connectingNode.Object)
            {
                var output = signal.Copy();
                output.Samples = WaveMath.GetScalarOperationFunction(Operation)(signal.Samples, Value);
                OutputNodes[0].Object.Add(output);   
            }            
            if (Cascade && OutputNodes[0].ConnectingNode != null)
                OutputNodes[0].ConnectingNode.Root.Execute();
        }

        private void SetOperationDescription()
        {
            _name = "Scalar " + Enum.GetName(typeof(WaveMath.OperationEnum), Operation);
            _description = "y(x) = y(x) " + WaveMath.GetOperationSymbol(Operation) + " " + string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.####}", Value);
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
