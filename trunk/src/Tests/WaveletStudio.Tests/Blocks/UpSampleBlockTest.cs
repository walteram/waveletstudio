using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class UpSampleBlockTest
    {
        [TestMethod]
        public void TestUpSampleBlockExecute()
        {
            var signalBlock = new ImportFromTextBlock{ColumnSeparator = " "};
            var block = new UpSampleBlock();
            block.Execute();
            
            signalBlock.ConnectTo(block);
            Assert.IsNotNull(block.Name);
            Assert.IsNotNull(block.Description);
            Assert.IsNotNull(block.ProcessingType);

            block.Factor = 2;
            signalBlock.Text = "1 2 3 4 5";
            signalBlock.Execute();
            Assert.AreEqual("1 0 2 0 3 0 4 0 5", block.OutputNodes[0].Object.ToString(0));

            block.Factor = 4;
            signalBlock.Text = "1 2 3 4 5 6 7 8 9 0";
            signalBlock.Execute();
            Assert.AreEqual("1 0 0 0 2 0 0 0 3 0 0 0 4 0 0 0 5 0 0 0 6 0 0 0 7 0 0 0 8 0 0 0 9 0 0 0 0", block.OutputNodes[0].Object.ToString(0));

            var block2 = (UpSampleBlock)block.Clone();
            block2.Factor = 3;
            signalBlock.Text = "1 2";
            block.ConnectTo(block2);
            signalBlock.Execute();
            Assert.AreEqual("1 0 0 0 2", block.OutputNodes[0].Object.ToString(0));
            Assert.AreEqual("1 0 0 0 0 0 0 0 0 0 0 0 2", block2.OutputNodes[0].Object.ToString(0));

            block.Cascade = false;
            block2 = (UpSampleBlock)block.Clone();
            block.ConnectTo(block2);
            signalBlock.Execute();
            Assert.AreEqual("", block2.OutputNodes[0].Object.ToString(0, " "));
            Assert.AreEqual(0, block2.OutputNodes[0].Object.Count);
        }

        [TestMethod]
        public void TestUpSampleDocumentationSample()
        {
            //Creates a sample signal with 4 samples
            var signalBlock = new ImportFromTextBlock { Text = "14, 20, 11, 41" };

            //UpSample block to add 2 samples between each other
            var block = new UpSampleBlock { Factor = 3 };

            //Connect and execute blocks
            signalBlock.ConnectTo(block);
            signalBlock.Execute();

            Console.WriteLine(block.OutputNodes[0].Object[0].ToString(0));

            //Output: 14 0 0 20 0 0 11 0 0 41

            Assert.AreEqual("14 0 0 20 0 0 11 0 0 41", block.OutputNodes[0].Object[0].ToString(0));
        }
    }
}
