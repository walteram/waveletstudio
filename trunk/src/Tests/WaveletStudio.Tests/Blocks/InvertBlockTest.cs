using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class InvertBlockTest
    {
        [TestMethod]
        public void TestInvertBlockExecute()
        {
            var signalBlock = new ImportFromTextBlock{ColumnSeparator = " "};
            var block = new InvertBlock();
            block.Execute();
            
            signalBlock.ConnectTo(block);
            Assert.IsNotNull(block.Name);
            Assert.IsNotNull(block.Description);
            Assert.IsNotNull(block.ProcessingType);

            signalBlock.Text = "1 2 3 4 5";
            signalBlock.Execute();
            Assert.AreEqual("5 4 3 2 1", block.OutputNodes[0].Object.ToString(0));

            var block2 = (InvertBlock)block.Clone();
            signalBlock.Text = "1 2";
            block.ConnectTo(block2);
            signalBlock.Execute();
            Assert.AreEqual("2 1", block.OutputNodes[0].Object.ToString(0));
            Assert.AreEqual("1 2", block2.OutputNodes[0].Object.ToString(0));

            block.Cascade = false;
            block2 = (InvertBlock)block.Clone();
            block.ConnectTo(block2);
            signalBlock.Execute();
            Assert.AreEqual("", block2.OutputNodes[0].Object.ToString(0, " "));
            Assert.AreEqual(0, block2.OutputNodes[0].Object.Count);
        }

        [TestMethod]
        public void TestInvertBlockExecute2()
        {
            var block = new InvertBlock();
            var signalBlock = new ImportFromTextBlock { Text = "2, 3, -4, 8, 7, 1, 2, -3" };
            signalBlock.ConnectTo(block);
            signalBlock.Execute();

            Console.WriteLine(block.OutputNodes[0].Object.ToString(0));
            //Output: -3 2 1 7 8 -4 3 2

            Assert.AreEqual("-3 2 1 7 8 -4 3 2", block.OutputNodes[0].Object.ToString(0));
        }
    }
}
