using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class MuxBlockTest
    {
        [TestMethod]
        public void TestMuxBlockExecute2()
        {
            var signal1 = new ImportFromTextBlock { Text = "1, 2, 3, 4" };
            var signal2 = new ImportFromTextBlock { Text = "5, 6, 7, 8" };
            var signal3 = new ImportFromTextBlock { Text = "9, 2, 4, 3" };
            var muxBlock = new MuxBlock { InputCount = 3 };
            signal1.ConnectTo(muxBlock);
            signal2.ConnectTo(muxBlock);
            signal3.ConnectTo(muxBlock);

            signal1.Execute();
            signal2.Execute();
            signal3.Execute();

            Console.WriteLine(muxBlock.OutputNodes[0].Object.Count);
            Console.WriteLine(muxBlock.OutputNodes[0].Object[0].ToString(0));
            Console.WriteLine(muxBlock.OutputNodes[0].Object[1].ToString(0));
            Console.WriteLine(muxBlock.OutputNodes[0].Object[2].ToString(0));
            //Output: 
            //3
            //1 2 3 4
            //5 6 7 8
            //9 2 4 3

            Assert.AreEqual(3, muxBlock.OutputNodes[0].Object.Count);
            Assert.AreEqual("1 2 3 4", muxBlock.OutputNodes[0].Object[0].ToString(0));
            Assert.AreEqual("5 6 7 8", muxBlock.OutputNodes[0].Object[1].ToString(0));
            Assert.AreEqual("9 2 4 3", muxBlock.OutputNodes[0].Object[2].ToString(0));            
        }

        [TestMethod]
        public void TestMuxBlockExecute()
        {
            var signalBlock1 = new ImportFromTextBlock{ColumnSeparator = " ", SignalStart = 3, SamplingInterval = 1};
            var signalBlock2 = new ImportFromTextBlock { ColumnSeparator = " ", SignalStart = 3, SamplingInterval = 1 };
            var signalBlock3 = new ImportFromTextBlock { ColumnSeparator = " ", SignalStart = 3, SamplingInterval = 1 };
            var signalBlock4 = new ImportFromTextBlock { ColumnSeparator = " ", SignalStart = 3, SamplingInterval = 1 };
            var block = new MuxBlock { InputCount = 4};
            block.Execute();
            
            signalBlock1.ConnectTo(block);            
            signalBlock1.Text = "1 1 1 1";
            signalBlock1.Execute();

            Assert.AreEqual((uint)4, block.InputCount);
            Assert.AreEqual("1 1 1 1", block.OutputNodes[0].Object[0].ToString(0));
            Assert.AreEqual(1, block.OutputNodes[0].Object.Count);

            block.SignalNames = "S1\r\nS2\r\nS3\r\nS4";

            signalBlock2.ConnectTo(block);
            signalBlock2.Text = "2 2 2 2";
            signalBlock2.Execute();
            signalBlock3.ConnectTo(block);
            signalBlock3.Text = "3 3 3 3";
            signalBlock3.Execute();
            signalBlock4.ConnectTo(block);
            signalBlock4.Text = "4 4 4 4";
            signalBlock4.Execute();

            Assert.AreEqual("1 1 1 1", block.OutputNodes[0].Object[0].ToString(0));
            Assert.AreEqual("2 2 2 2", block.OutputNodes[0].Object[1].ToString(0));
            Assert.AreEqual("3 3 3 3", block.OutputNodes[0].Object[2].ToString(0));
            Assert.AreEqual("4 4 4 4", block.OutputNodes[0].Object[3].ToString(0));
            Assert.AreEqual("S1", block.OutputNodes[0].Object[0].Name);
            Assert.AreEqual("S2", block.OutputNodes[0].Object[1].Name);
            Assert.AreEqual("S3", block.OutputNodes[0].Object[2].Name);
            Assert.AreEqual("S4", block.OutputNodes[0].Object[3].Name);
            Assert.AreEqual(4, block.OutputNodes[0].Object.Count);

            var block2 = (MuxBlock)block.Clone();
            block.ConnectTo(block2);
            signalBlock4.Execute();
            Assert.AreEqual(4, block2.OutputNodes[0].Object.Count);          

            block.Cascade = false;
            block2 = (MuxBlock)block.Clone();
            block.ConnectTo(block2);
            signalBlock4.Execute();
            Assert.AreEqual("", block2.OutputNodes[0].Object.ToString(0, " "));
            Assert.AreEqual(0, block2.OutputNodes[0].Object.Count);
        }
    }
}
