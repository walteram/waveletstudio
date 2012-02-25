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
using System.Collections.Generic;
using System.Resources;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.Functions;
using WaveletStudio.Properties;

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

        /// <summary>
        /// Creates the input and output nodes
        /// </summary>
        /// <param name="root"></param>
        protected override sealed void CreateNodes(ref BlockBase root)
        {
            root.InputNodes = new List<BlockInputNode> { new BlockInputNode(ref root, Resources.Signal, Resources.In) };
            root.OutputNodes = new List<BlockOutputNode> { new BlockOutputNode(ref root, Resources.Output, Resources.Out) };
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
            return Enum.GetName(typeof(WaveMath.OperationEnum), Operation);
        }
    }
}
