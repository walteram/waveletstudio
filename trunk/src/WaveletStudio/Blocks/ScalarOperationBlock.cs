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
    /// <para>Executes a scalar operation in a signal (sum, subtraction, multiplication and division).</para>
    /// <para>The operation is made sample-by-sample.</para>
    /// <para>Image: http://i.imgur.com/gC28gvP.png</para>
    /// <para>InOutGraph: http://i.imgur.com/HE2eGMN.png </para>
    /// <para>Title: Scalar Operation </para>
    /// <example>
    ///     <code>
    ///         var signal = new ImportFromTextBlock { Text = "1, 3, -2, 9, 4.5, -2, 4, 0" };
    ///         var block = new ScalarOperationBlock
    ///         {
    ///             Operation = WaveMath.OperationEnum.Sum,
    ///             Value = 1.5
    ///         };
    ///     
    ///         signal.ConnectTo(block);
    ///         signal.Execute();
    ///     
    ///         Console.WriteLine(block.Output[0].ToString(0, ", "));
    ///         //Console Output:
    ///         //2.5, 4.5, -0.5, 10.5, 6.0, -0.5, 5.5, 1.5
    ///     </code>
    /// </example>
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

        /// <summary>
        /// Creates the input and output nodes
        /// </summary>
        /// <param name="root"></param>
        protected override sealed void CreateNodes(ref BlockBase root)
        {
            root.InputNodes = BlockInputNode.CreateSingleInputSignal(ref root);
            root.OutputNodes = BlockOutputNode.CreateSingleOutput(ref root);
        }

        private string _name = Resources.Scalar;

        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return _name; }
        }

        private string _description = Resources.ScalarDescription;

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
        [Parameter(CausesRefresh = true)]
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
            var resourceManager = new ResourceManager(typeof(Resources));
            _name = resourceManager.GetString(Enum.GetName(typeof(WaveMath.OperationEnum), Operation));
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

        /// <summary>
        /// Gets the name of the class
        /// </summary>
        /// <returns></returns>
        public override string GetAssemblyClassName()
        {
            return "Scalar" + Enum.GetName(typeof(WaveMath.OperationEnum), Operation);
        }
    }
}
