using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class ExportToCSVBlockTest
    {
        [TestMethod]
        [DeploymentItem("example.csv")]
        public void TestExportToCSVBlockExecute()
        {
            var signalBlock = new GenerateSignalBlock { Offset = 1.2, TemplateName = "Binary", Start = 0, Finish = 5, SamplingRate = 1, IgnoreLastSample = true };
            var exportBlock = new ExportToCSVBlock {ColumnSeparator = "|", DecimalPlaces = 1, IncludeSignalNameInFirstColumn = true};
            exportBlock.Execute();

            Assert.IsNotNull(exportBlock.Name);
            Assert.IsNotNull(exportBlock.FilePath);
            Assert.IsNotNull(exportBlock.Description);
            Assert.IsNotNull(exportBlock.ProcessingType);            
            signalBlock.ConnectTo(exportBlock);
            signalBlock.Execute();
            var lines = File.ReadAllLines(Path.Combine(Utils.AssemblyDirectory, "output.csv"));
            Assert.AreEqual(1, lines.Length);
            Assert.AreEqual("|1.2|2.2|1.2|2.2|1.2", lines[0]);

            var exportBlock2 = (ExportToCSVBlock)exportBlock.Clone();
            signalBlock.Cascade = false;
            signalBlock.ConnectTo(exportBlock2);
            signalBlock.Execute();
            Assert.AreEqual(0, exportBlock2.OutputNodes.Count);

            signalBlock.Cascade = true;
            signalBlock.OutputNodes[0].Object = null;
            exportBlock2.Execute();
            Assert.AreEqual(0, exportBlock2.OutputNodes.Count);
        }
    }
}
