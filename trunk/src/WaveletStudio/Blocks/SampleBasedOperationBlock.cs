using System;
using System.Collections.Generic;
using System.Linq;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.Functions;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Sum two or more signals
    /// </summary>
    [Serializable]
    public class SampleBasedOperationBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SampleBasedOperationBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);
        }

        protected override sealed void CreateNodes(ref BlockBase root)
        {
            root.InputNodes = new List<BlockInputNode>
                                  {
                                      new BlockInputNode(ref root, "Signal1", "S1"),
                                      new BlockInputNode(ref root, "Signal2", "S2")
                                  };
            root.OutputNodes = new List<BlockOutputNode> {new BlockOutputNode(ref root, "Output", "Out")};
        }

        private string _name = "Operation";

        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Description of the block
        /// </summary>
        public override string Description
        {
            get { return "Sum, subtract, multiply or divide two or more blocks"; }
        }

        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Operation; } }

        private WaveMath.OperationEnum _operation = WaveMath.OperationEnum.Sum;

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

        /// <summary>
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            SetOperationDescription();
            var signals = InputNodes.Where(it => it.ConnectingNode != null).Select(it => ((BlockOutputNode)it.ConnectingNode).Object).ToArray();
            if(signals.Length == 0)
                return;
            OutputNodes[0].Object = WaveMath.ExecuteOperation(Operation, signals);
            if (Cascade && OutputNodes[0].ConnectingNode != null)
                OutputNodes[0].ConnectingNode.Root.Execute();
        }

        private void SetOperationDescription()
        {
            _name = Enum.GetName(typeof(WaveMath.OperationEnum), Operation);
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
