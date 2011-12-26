using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class DemuxBlockTest
    {
        [TestMethod]
        public void TestDemuxBlockExecute2()
        {
            var signalBlock = new ImportFromTextBlock {Text = "1, 2, 3, 4 \r\n, 4, 5, 6, 7"};
            var demuxBlock = new DemuxBlock();
            
            signalBlock.ConnectTo(demuxBlock);
            signalBlock.Execute();

            Console.WriteLine("Signal 1 = " + demuxBlock.OutputNodes[0].Object.ToString(0));
            Console.WriteLine("Signal 2 = " + demuxBlock.OutputNodes[1].Object.ToString(0));
            
            //Output:
            //Signal 1 = 1 2 3 4
            //Signal 2 = 4 5 6 7

            Assert.AreEqual("1 2 3 4", demuxBlock.OutputNodes[0].Object.ToString(0));
            Assert.AreEqual("4 5 6 7", demuxBlock.OutputNodes[1].Object.ToString(0));
        }

        [TestMethod]
        public void TestDemuxBlockExecute()
        {
            var signalBlock = new ImportFromTextBlock{ColumnSeparator = " ", SignalStart = 3, SamplingInterval = 1};
            var block = new DemuxBlock { OutputCount = 4};
            block.Execute();
            
            signalBlock.ConnectTo(block);            
            signalBlock.Text = "1 1 1 1\r\n2 2 2 2\r\n3 3 3 3\r\n4 4 4 4";
            signalBlock.Execute();

            Assert.AreEqual((uint)4, block.OutputCount);
            Assert.AreEqual("1 1 1 1", block.OutputNodes[0].Object[0].ToString(0));
            Assert.AreEqual("2 2 2 2", block.OutputNodes[1].Object[0].ToString(0));
            Assert.AreEqual("3 3 3 3", block.OutputNodes[2].Object[0].ToString(0));
            Assert.AreEqual("4 4 4 4", block.OutputNodes[3].Object[0].ToString(0));

            block.SignalIndexes = "0, 1\r\n2, 3\r\n99, -33";
            block.Execute();
            Assert.AreEqual((uint)4, block.OutputCount);
            Assert.AreEqual("1 1 1 1", block.OutputNodes[0].Object[0].ToString(0));
            Assert.AreEqual("2 2 2 2", block.OutputNodes[0].Object[1].ToString(0));
            Assert.AreEqual("3 3 3 3", block.OutputNodes[1].Object[0].ToString(0));
            Assert.AreEqual("4 4 4 4", block.OutputNodes[1].Object[1].ToString(0));
            Assert.AreEqual(0, block.OutputNodes[2].Object.Count);
            Assert.AreEqual(0, block.OutputNodes[3].Object.Count);

            var block2 = (DemuxBlock)block.Clone();
            block.ConnectTo(block2);
            signalBlock.Execute();
            Assert.AreEqual(2, block2.OutputNodes[0].Object.Count);

            block.Cascade = false;
            block2 = (DemuxBlock)block.Clone();
            block.ConnectTo(block2);
            signalBlock.Execute();
            Assert.AreEqual("", block2.OutputNodes[0].Object.ToString(0, " "));
            Assert.AreEqual(0, block2.OutputNodes[0].Object.Count);

            block.OutputCount = 0;
            block.SignalIndexes = "";
            signalBlock.ConnectTo(block);
            signalBlock.Execute();
            Assert.AreEqual((uint)4, block.OutputCount);
            Assert.AreEqual("1 1 1 1", block.OutputNodes[0].Object[0].ToString(0));
            Assert.AreEqual("2 2 2 2", block.OutputNodes[1].Object[0].ToString(0));
            Assert.AreEqual("3 3 3 3", block.OutputNodes[2].Object[0].ToString(0));
            Assert.AreEqual("4 4 4 4", block.OutputNodes[3].Object[0].ToString(0));
        }
    }
}
