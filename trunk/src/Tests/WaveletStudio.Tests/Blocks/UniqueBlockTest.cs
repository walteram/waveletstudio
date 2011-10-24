using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class UniqueBlockTest
    {
        [TestMethod]
        public void TestUniqueBlockExecute()
        {
            var signalBlock = new ImportFromTextBlock{ColumnSeparator = " "};
            var block = new UniqueBlock();
            block.Execute();
            
            signalBlock.ConnectTo(block);
            Assert.IsNotNull(block.Name);
            Assert.IsNotNull(block.Description);
            Assert.IsNotNull(block.ProcessingType);

            block.SortSamples = false;
            signalBlock.Text = "3 5 3 1 2 2";
            signalBlock.Execute();
            Assert.AreEqual("3 5 1 2", block.OutputNodes[0].Object.ToString(0));

            block.SortSamples = true;
            signalBlock.Text = "3 5 3 1 2 2";
            signalBlock.Execute();
            Assert.AreEqual("1 2 3 5", block.OutputNodes[0].Object.ToString(0));

            var block2 = (UniqueBlock)block.Clone();
            signalBlock.Text = "1 2 3 3 4 4 5";
            block.ConnectTo(block2);
            signalBlock.Execute();
            Assert.AreEqual("1 2 3 4 5", block.OutputNodes[0].Object.ToString(0));
            Assert.AreEqual("1 2 3 4 5", block2.OutputNodes[0].Object.ToString(0));

            block.Cascade = false;
            block2 = (UniqueBlock)block.Clone();
            block.ConnectTo(block2);
            signalBlock.Execute();
            Assert.AreEqual("", block2.OutputNodes[0].Object.ToString(0, " "));
            Assert.AreEqual(0, block2.OutputNodes[0].Object.Count);
        }
    }
}
