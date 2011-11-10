using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class AbsoluteValueBlockTest
    {
        [TestMethod]
        public void AbsoluteValueBlockTestExecute()
        {
            var signalBlock = new ImportFromTextBlock();
            var block = new AbsoluteValueBlock();
            block.Execute();
            
            signalBlock.ConnectTo(block);
            Assert.IsNotNull(block.Name);
            Assert.IsNotNull(block.Description);
            Assert.IsNotNull(block.ProcessingType);

            signalBlock.Text = "2.1, 3.2, -1, -1.3, -100, 145, -2";
            signalBlock.Execute();
            Assert.AreEqual("2.1, 3.2, 1.0, 1.3, 100.0, 145.0, 2.0", block.OutputNodes[0].Object.ToString(1, ", "));

            var block2 = (AbsoluteValueBlock)block.Clone();
            signalBlock.Text = "1, 2, -1";
            block.ConnectTo(block2);
            signalBlock.Execute();
            Assert.AreEqual("1 2 1", block.OutputNodes[0].Object.ToString(0));
            Assert.AreEqual("1 2 1", block2.OutputNodes[0].Object.ToString(0));

            block.Cascade = false;
            block2 = (AbsoluteValueBlock)block.Clone();
            block.ConnectTo(block2);
            signalBlock.Execute();
            Assert.AreEqual("", block2.OutputNodes[0].Object.ToString(0, " "));
            Assert.AreEqual(0, block2.OutputNodes[0].Object.Count);
        }

        [TestMethod]
        public void AbsoluteValueBlockTest2()
        {
            var block = new AbsoluteValueBlock();
            var signalBlock = new ImportFromTextBlock { Text = "2.1, 3.2, -1, -1.3, -100, -2" };            
            signalBlock.ConnectTo(block);            
            signalBlock.Execute();

            Console.WriteLine(block.OutputNodes[0].Object.ToString(1));
            Assert.AreEqual("2.1 3.2 1.0 1.3 100.0 2.0", block.OutputNodes[0].Object.ToString(1));
        }
    }
}
