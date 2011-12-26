using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class ShiftBlockTest
    {
        [TestMethod]
        public void TestShiftBlockExecute()
        {
            var signalBlock = new ImportFromTextBlock{ColumnSeparator = " ", SignalStart = 3, SamplingInterval = 1};
            var block = new ShiftBlock();
            block.Execute();
            
            signalBlock.ConnectTo(block);
            Assert.AreEqual(0.1m, block.DelayIncrement);            

            block.Delay = -2.1;
            signalBlock.Text = "3 4 5 6 7 8 9 10";
            signalBlock.Execute();
            Assert.AreEqual(0.9d, block.OutputNodes[0].Object[0].Start);
            Assert.AreEqual(7.9d, block.OutputNodes[0].Object[0].Finish);
            Assert.AreEqual(1, block.DelayIncrement);

            block.Delay = 2;
            signalBlock.Execute();
            Assert.AreEqual(5, block.OutputNodes[0].Object[0].Start);
            Assert.AreEqual(12, block.OutputNodes[0].Object[0].Finish);

            var block2 = (ShiftBlock)block.Clone();
            block2.Delay = 3;
            block.ConnectTo(block2);
            signalBlock.Execute();
            Assert.AreEqual(5, block.OutputNodes[0].Object[0].Start);
            Assert.AreEqual(12, block.OutputNodes[0].Object[0].Finish);
            Assert.AreEqual(8, block2.OutputNodes[0].Object[0].Start);
            Assert.AreEqual(15, block2.OutputNodes[0].Object[0].Finish);            

            block.Cascade = false;
            block2 = (ShiftBlock)block.Clone();
            block.ConnectTo(block2);
            signalBlock.Execute();
            Assert.AreEqual("", block2.OutputNodes[0].Object.ToString(0, " "));
            Assert.AreEqual(0, block2.OutputNodes[0].Object.Count);

            ((BlockOutputNode)block.InputNodes[0].ConnectingNode).Object = null;
            Assert.AreEqual(0.1m, block.DelayIncrement);
            block.InputNodes[0].ConnectingNode = null;
            Assert.AreEqual(0.1m, block.DelayIncrement);  
        }        
    }
}
