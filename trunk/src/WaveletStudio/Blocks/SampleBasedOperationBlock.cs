using System;
using System.Collections.Generic;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.Functions;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Sum, subtract, multiply or divided two or more signals
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

        /// <summary>
        /// Creates the input and output nodes
        /// </summary>
        /// <param name="root"></param>
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
            get { return "Sum, subtract, multiply or divide two or more signals"; }
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

            var inputNode1 = InputNodes[0].ConnectingNode as BlockOutputNode;
            var inputNode2 = InputNodes[1].ConnectingNode as BlockOutputNode;
            if (inputNode1 == null || inputNode1.Object.Count == 0 || inputNode2 == null)
                return;
            if(inputNode2.Object.Count > inputNode1.Object.Count)
            {
                inputNode1 = InputNodes[1].ConnectingNode as BlockOutputNode;
                inputNode2 = InputNodes[0].ConnectingNode as BlockOutputNode;
            }

            OutputNodes[0].Object.Clear();
            for (var i = 0; i < inputNode1.Object.Count; i++)
            {
                var signal1 = inputNode1.Object[i];
                Signal signal2;
                if (i < inputNode2.Object.Count)
                {
                    signal2 = inputNode2.Object[i];                    
                }
                else if (inputNode2.Object.Count > 0)
                {
                    signal2 = inputNode2.Object[0];                    
                }
                else
                {
                    OutputNodes[0].Object.Add(signal1.Clone());
                    continue;
                }
                OutputNodes[0].Object.Add(WaveMath.ExecuteOperation(Operation, signal1, signal2));
            }
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
