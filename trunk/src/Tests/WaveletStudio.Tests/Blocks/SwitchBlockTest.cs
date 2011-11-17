using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class SwitchBlockTest
    {
        [TestMethod]
        public void TestMuxBlockExecute()
        {
            var signalBlock1 = new ImportFromTextBlock { ColumnSeparator = " ", SignalStart = 3, SamplingInterval = 1 };
            var signalBlock2 = new ImportFromTextBlock { ColumnSeparator = " ", SignalStart = 3, SamplingInterval = 1 };
            var thresholdBlock = new ImportFromTextBlock { Text = "1 5 2", ColumnSeparator = " ", SignalStart = 3, SamplingInterval = 1 };
            var block = new SwitchBlock {StaticThreshold = 10};
            
            block.Execute();
            signalBlock1.ConnectTo(block);
            signalBlock1.Text = "1 2 3 4 \r\n 1 2 3 \r\n 1 2 3 4 5";
            signalBlock1.Execute();            
            Assert.AreEqual("1 2 3 4", block.OutputNodes[0].Object[0].ToString(0));
            Assert.AreEqual("1 2 3", block.OutputNodes[0].Object[1].ToString(0));
            Assert.AreEqual("1 2 3 4 5", block.OutputNodes[0].Object[2].ToString(0));
            Assert.AreEqual(3, block.OutputNodes[0].Object.Count);

            block = new SwitchBlock { StaticThreshold = 10 };
            signalBlock1.OutputNodes[0].ConnectTo(block.InputNodes[2]);
            signalBlock1.Execute();
            Assert.AreEqual("1 2 3 4", block.OutputNodes[0].Object[0].ToString(0));
            Assert.AreEqual("1 2 3", block.OutputNodes[0].Object[1].ToString(0));
            Assert.AreEqual("1 2 3 4 5", block.OutputNodes[0].Object[2].ToString(0));
            Assert.AreEqual(3, block.OutputNodes[0].Object.Count);

            signalBlock1.OutputNodes[0].ConnectTo(block.InputNodes[0]);            
            signalBlock2.OutputNodes[0].ConnectTo(block.InputNodes[2]);            
            signalBlock1.Execute();
            signalBlock2.Execute();
            Assert.AreEqual("1 2 3 4", block.OutputNodes[0].Object[0].ToString(0));
            Assert.AreEqual("1 2 3", block.OutputNodes[0].Object[1].ToString(0));
            Assert.AreEqual("1 2 3 4 5", block.OutputNodes[0].Object[2].ToString(0));
            Assert.AreEqual(3, block.OutputNodes[0].Object.Count);

            signalBlock2.Text = "2 3 4 5 \r\n 3 4 5";
            signalBlock1.OutputNodes[0].ConnectTo(block.InputNodes[0]);
            signalBlock2.OutputNodes[0].ConnectTo(block.InputNodes[2]);
            signalBlock1.Execute();
            signalBlock2.Execute();
            Assert.AreEqual("1 2 3 4", block.OutputNodes[0].Object[0].ToString(0));
            Assert.AreEqual("1 2 3", block.OutputNodes[0].Object[1].ToString(0));
            Assert.AreEqual("1 2 3 4 5", block.OutputNodes[0].Object[2].ToString(0));
            Assert.AreEqual(3, block.OutputNodes[0].Object.Count);

            signalBlock2.OutputNodes[0].ConnectTo(block.InputNodes[0]);
            signalBlock1.OutputNodes[0].ConnectTo(block.InputNodes[2]);
            signalBlock1.Execute();
            signalBlock2.Execute();
            Assert.AreEqual("2 3 4 5", block.OutputNodes[0].Object[0].ToString(0));
            Assert.AreEqual("3 4 5", block.OutputNodes[0].Object[1].ToString(0));
            Assert.AreEqual("1 2 3 4 5", block.OutputNodes[0].Object[2].ToString(0));
            Assert.AreEqual(3, block.OutputNodes[0].Object.Count);

            signalBlock1.OutputNodes[0].ConnectTo(block.InputNodes[0]);
            thresholdBlock.OutputNodes[0].ConnectTo(block.InputNodes[1]);
            signalBlock2.OutputNodes[0].ConnectTo(block.InputNodes[2]);                        
            signalBlock1.Execute();
            signalBlock2.Execute();
            thresholdBlock.Execute();
            Assert.AreEqual("2 2 4 4", block.OutputNodes[0].Object[0].ToString(0));
            Assert.AreEqual("3 2 5", block.OutputNodes[0].Object[1].ToString(0));
            Assert.AreEqual("1 2 3 4 5", block.OutputNodes[0].Object[2].ToString(0));
            Assert.AreEqual(3, block.OutputNodes[0].Object.Count);

            thresholdBlock.Text = "3";
            signalBlock1.OutputNodes[0].ConnectTo(block.InputNodes[0]);
            thresholdBlock.OutputNodes[0].ConnectTo(block.InputNodes[1]);
            signalBlock2.OutputNodes[0].ConnectTo(block.InputNodes[2]);
            signalBlock1.Execute();
            signalBlock2.Execute();
            thresholdBlock.Execute();
            Assert.AreEqual("1 3 4 5", block.OutputNodes[0].Object[0].ToString(0));
            Assert.AreEqual("3 4 5", block.OutputNodes[0].Object[1].ToString(0));
            Assert.AreEqual("1 2 3 4 5", block.OutputNodes[0].Object[2].ToString(0));
            Assert.AreEqual(3, block.OutputNodes[0].Object.Count);

            thresholdBlock.Text = "3\r\n4 5";
            signalBlock1.OutputNodes[0].ConnectTo(block.InputNodes[0]);
            thresholdBlock.OutputNodes[0].ConnectTo(block.InputNodes[1]);
            signalBlock2.OutputNodes[0].ConnectTo(block.InputNodes[2]);
            signalBlock1.Execute();
            signalBlock2.Execute();
            thresholdBlock.Execute();
            Assert.AreEqual("1 3 4 5", block.OutputNodes[0].Object[0].ToString(0));
            Assert.AreEqual("1 2 3", block.OutputNodes[0].Object[1].ToString(0));
            Assert.AreEqual("1 2 3 4 5", block.OutputNodes[0].Object[2].ToString(0));
            Assert.AreEqual(3, block.OutputNodes[0].Object.Count);

            var block2 = (SwitchBlock)block.Clone();
            block.ConnectTo(block2);
            signalBlock1.Execute();
            Assert.AreEqual(3, block2.OutputNodes[0].Object.Count);

            block.Cascade = false;
            block2 = (SwitchBlock)block.Clone();
            block.ConnectTo(block2);
            signalBlock1.Execute();
            Assert.AreEqual("", block2.OutputNodes[0].Object.ToString(0, " "));
            Assert.AreEqual(0, block2.OutputNodes[0].Object.Count);
        }
    }
}
