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
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.Functions;
using WaveletStudio.Properties;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// <para>Dilate or contract a signal in time and/or amplitude.</para>
    /// <para>Image: http://i.imgur.com/quStjHO.png </para>
    /// <para>InOutGraph: http://i.imgur.com/Loo4dme.png </para>
    /// <example>
    ///     <code>
    ///         var signal = new ImportFromTextBlock { Text = "1, 3, -2, 9, 4.5, -2, 4, 0" };
    ///         var block = new ScaleBlock
    ///         {
    ///             TimeScalingFactor = 2,
    ///             AmplitudeScalingFactor = 1.5
    ///         };
    ///     
    ///         signal.ConnectTo(block);
    ///         signal.Execute();
    ///     
    ///         Console.WriteLine(block.Output[0].ToString(0, ", "));    
    ///     </code>
    /// </example>
    /// </summary>
    [Serializable]
    public class ScaleBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ScaleBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);
            TimeScalingFactor = 1;
            AmplitudeScalingFactor = 1;
        }
        
        /// <summary>
        /// Name of the block
        /// </summary>
        public override string Name
        {
            get { return Resources.Scale; }
        }

        /// <summary>
        /// Description
        /// </summary>
        public override string Description
        {
            get { return Resources.ScaleDescription; }
        }

        private double _timeScalingFactor;
        /// <summary>
        /// Factor to be used in time scaling. If the value is setted to 1, no scaling will be applied to the time variable.
        /// </summary>
        [Parameter]
        public double TimeScalingFactor
        {
            get { return _timeScalingFactor; }
            set { _timeScalingFactor = Math.Max(0, value); }
        }

        private double _amplitudeScalingFactor;
        /// <summary>
        /// Factor to be used in amplitude scaling. If the value is setted to 1, no scaling will be applied to the amplitude variable.
        /// </summary>
        [Parameter]
        public double AmplitudeScalingFactor
        {
            get { return _amplitudeScalingFactor; }
            set { _amplitudeScalingFactor = Math.Max(0, value); }
        }

        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Operation; } }

        /// <summary>
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            var inputNode = InputNodes[0].ConnectingNode as BlockOutputNode;
            if (inputNode == null || inputNode.Object == null)
                return;

            OutputNodes[0].Object.Clear();
            foreach (var signal in inputNode.Object)
            {
                var output = WaveMath.Scale(signal, _amplitudeScalingFactor, _timeScalingFactor);                
                OutputNodes[0].Object.Add(output);
            }            
            if (Cascade && OutputNodes[0].ConnectingNode != null)
            {
                OutputNodes[0].ConnectingNode.Root.Execute();
            }
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