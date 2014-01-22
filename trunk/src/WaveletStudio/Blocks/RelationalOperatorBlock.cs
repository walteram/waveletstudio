/*  Wavelet Studio Signal Processing Library - www.waveletstudio.net
    Copyright (C) 2011, 2012 Walter V. S. de Amorim - The Wavelet Studio Initiative

    Wavelet Studio is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Wavelet Studio is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

using System;
using System.Resources;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.Functions;
using WaveletStudio.Properties;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// <para>Conversion to boolean based on &gt;, &lt;, &gt;=, &lt;=, &lt;&gt;, = an static value, the previous sample or the next sample.</para>
    /// <para>For each sample in the signal S1, the Relational Operation Block will compare the sample using the specified operation and returns 1 if true or 0 if false.</para>
    /// <para>Image: http://i.imgur.com/yJvRKtq.png </para>
    /// <para>InOutGraph: http://i.imgur.com/P56gqBS.png </para>
    /// <para>Title: Relational Operation </para>
    /// <example>
    ///     <code>
    ///         var signal1 = new ImportFromTextBlock { Text = "1, 3, -2, 9, 4, 2, 4, 0" };
    ///         var signal2 = new ImportFromTextBlock { Text = "0, 2, -1, 2, 3, 2, 4, 0" };
    ///         var block = new RelationalOperatorBlock
    ///         {
    ///             Operation = WaveMath.RelationalOperatorEnum.GreaterThan,
    ///             Operand = RelationalOperatorBlock.OperandEnum.Signal,
    ///         };
    ///         
    ///         signal1.ConnectTo(block);
    ///         signal2.ConnectTo(block);
    ///         signal1.Execute();
    ///         signal2.Execute();
    ///         
    ///         Console.WriteLine(block.Output[0].ToString(0));
    ///         //Console Output:
    ///         //1 1 0 1 1 0 0 0 
    ///         //This means that samples at index 0, 1, 3 and 4 of signal1 area greater than the respective samples of signal2
    ///     </code>
    /// </example>
    /// </summary>
    [Serializable]
    public class RelationalOperatorBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RelationalOperatorBlock()
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
            root.InputNodes =  BlockInputNode.CreateDoubledInput(ref root);
            root.OutputNodes = BlockOutputNode.CreateSingleOutput(ref root);
        }

        private string _name = Resources.RelationalOperator;

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
            get { return Resources.RelationalOperatorDescription; }
        }

        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Logic; } }

        private WaveMath.RelationalOperatorEnum _operation = WaveMath.RelationalOperatorEnum.EqualsTo;
        /// <summary>
        /// Relational operator to be used
        /// </summary>
        [Parameter(CausesRefresh = true)]
        public WaveMath.RelationalOperatorEnum Operation
        {
            get { return _operation; }
            set
            {
                _operation = value;
                SetOperationDescription();
            }
        }

        /// <summary>
        /// Operand type
        /// </summary>
        public enum OperandEnum
        {
            /// <summary>
            /// An static scalar value
            /// </summary>
            StaticValue,
            /// <summary>
            /// The previous sample of the same signal
            /// </summary>
            PreviousSample,
            /// <summary>
            /// The next sample of the same signal
            /// </summary>
            NextSample,
            /// <summary>
            /// Another signal (S2)
            /// </summary>
            Signal
        }

        /// <summary>
        /// Operand type
        /// </summary>
        [Parameter(CausesRefresh = true)]
        public OperandEnum Operand { get; set; }

        [Parameter]
        public double ScalarValue { get; set; }

        /// <summary>
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            SetOperationDescription();

            var inputNode1 = InputNodes[0].ConnectingNode as BlockOutputNode;
            var inputNode2 = InputNodes[1].ConnectingNode as BlockOutputNode;
            if (inputNode1 == null || inputNode1.Object.Count == 0)
                return;
            
            OutputNodes[0].Object.Clear();
            for (var i = 0; i < inputNode1.Object.Count; i++)
            {
                var signal1 = inputNode1.Object[i];

                if (Operand == OperandEnum.StaticValue)
                {
                    OutputNodes[0].Object.Add(WaveMath.ExecuteRelationalOperation(Operation, signal1, ScalarValue));
                }
                else if (Operand == OperandEnum.Signal)
                {
                    if(inputNode2 == null)
                        return;
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
                        OutputNodes[0].Object.Add(WaveMath.ExecuteRelationalOperation(Operation, signal1, 0));
                        continue;
                    }
                    OutputNodes[0].Object.Add(WaveMath.ExecuteRelationalOperation(Operation, signal1, signal2));
                }
                else if (Operand == OperandEnum.NextSample)
                {
                    OutputNodes[0].Object.Add(WaveMath.ExecuteRelationalOperationWithNextSample(Operation, signal1));
                }
                else if (Operand == OperandEnum.PreviousSample)
                {
                    OutputNodes[0].Object.Add(WaveMath.ExecuteRelationalOperationWithPreviousSample(Operation, signal1));
                }
            }
            if (Cascade && OutputNodes[0].ConnectingNode != null)
                OutputNodes[0].ConnectingNode.Root.Execute();
        }

        private void SetOperationDescription()
        {
            var resourceManager = new ResourceManager(typeof(Resources));
            _name = resourceManager.GetString(Enum.GetName(typeof(WaveMath.RelationalOperatorEnum), Operation));
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

        /// <summary>
        /// Gets the name of the class
        /// </summary>
        /// <returns></returns>
        public override string GetAssemblyClassName()
        {
            return Enum.GetName(typeof(WaveMath.RelationalOperatorEnum), Operation);
        }
    }
}
