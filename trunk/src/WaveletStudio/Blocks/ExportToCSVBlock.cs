using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WaveletStudio.Blocks.CustomAttributes;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Export a signal to a CSV file
    /// </summary>
    [SingleInputOutputBlock]
    [Serializable]
    public class ExportToCSVBlock : BlockBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ExportToCSVBlock()
        {
            BlockBase root = this;
            CreateNodes(ref root);
            FilePath = "output.csv";
            ColumnSeparator = ",";
            IncludeSignalNameInFirstColumn = true;
            DecimalPlaces = 3;
        }

        /// <summary>
        /// Name
        /// </summary>
        public override string Name { get { return "Export CSV"; } }

        /// <summary>
        /// Description
        /// </summary>
        public override string Description { get { return "Exports a signal based to a CSV file"; } }

        /// <summary>
        /// Processing type
        /// </summary>
        public override ProcessingTypeEnum ProcessingType { get { return ProcessingTypeEnum.Export; } }

        [Parameter]
        public string FilePath { get; set; }

        [Parameter]
        public string ColumnSeparator { get; set; }

        [Parameter]
        public int DecimalPlaces { get; set; }

        [Parameter]
        public bool IncludeSignalNameInFirstColumn { get; set; }

        /// <summary>
        /// Executes the block
        /// </summary>
        public override void Execute()
        {
            var inputNode = InputNodes[0].ConnectingNode as BlockOutputNode;
            if (inputNode == null || inputNode.Object == null)
                return;
            var filePath = FilePath;
            if(!Path.IsPathRooted(filePath))
                filePath = Path.Combine(Utils.AssemblyDirectory, filePath);

            var stringBuilder = new StringBuilder();
            foreach (var inputSignal in inputNode.Object)
            {
                var line = inputSignal.ToString(DecimalPlaces, ColumnSeparator);
                if (IncludeSignalNameInFirstColumn)
                    line = inputSignal.Name + ColumnSeparator + line;
                stringBuilder.AppendLine(line);
            }
            GeneratedData = stringBuilder;
            File.WriteAllText(filePath, stringBuilder.ToString());                   
        }

        protected override sealed void CreateNodes(ref BlockBase root)
        {
            root.InputNodes = new List<BlockInputNode> { new BlockInputNode(ref root, "Signal", "S") };            
        }

        /// <summary>
        /// Clone the block, including the template
        /// </summary>
        /// <returns></returns>
        public override BlockBase Clone()
        {
            var block = (ExportToCSVBlock)MemberwiseClone();
            block.Execute();
            return block;
        }

        /// <summary>
        /// Clones this block but mantains the links
        /// </summary>
        /// <returns></returns>
        public override BlockBase CloneWithLinks()
        {
            var block = (ExportToCSVBlock)MemberwiseCloneWithLinks();
            block.Execute();
            return block;
        }
    }
}
