using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class ImportFromTextBlockTest
    {
        [TestMethod]
        public void TestImportFromTextBlockExecute()
        {
            var textBlock = new ImportFromTextBlock { Text = "name,s1,s2,s3,s4\r\nSignal1, 1.1, 9.12355, 0.123456, 0\r\nSignal2, -1.1, asdf, 0.123456, 0\r\nSignal3, -1.1, 9.12355, 0.123456, 0\r\nSignal4\r\n\r\n", SignalNameInFirstColumn = true };
            textBlock.Execute();
            Assert.IsNotNull(textBlock.Name);
            Assert.IsNotNull(textBlock.Description);
            Assert.IsNotNull(textBlock.ProcessingType);
            Assert.AreEqual(3, textBlock.OutputNodes[0].Object.Count);
            
            Assert.AreEqual("Signal1", textBlock.OutputNodes[0].Object[0].Name);
            Assert.IsTrue(TestUtils.SequenceEquals(new[] { 1.1, 9.12355, 0.123456, 0 }, textBlock.OutputNodes[0].Object[0].Samples));

            Assert.AreEqual("Signal2", textBlock.OutputNodes[0].Object[1].Name);
            Assert.IsTrue(TestUtils.SequenceEquals(new[] { -1.1, 0.123456, 0 }, textBlock.OutputNodes[0].Object[1].Samples));

            Assert.AreEqual("Signal3", textBlock.OutputNodes[0].Object[2].Name);
            Assert.IsTrue(TestUtils.SequenceEquals(new[] { -1.1, 9.12355, 0.123456, 0 }, textBlock.OutputNodes[0].Object[2].Samples));


            textBlock.Cascade = false;
            var scalarBlock = new ScalarOperationBlock { Operation = WaveMath.OperationEnum.Sum, Value = 1.5 };
            textBlock.OutputNodes[0].ConnectTo(scalarBlock.InputNodes[0]);
            textBlock.Execute();
            Assert.AreEqual(0, scalarBlock.OutputNodes[0].Object.Count);

            var textBlock2 = (ImportFromTextBlock)textBlock.Clone();
            textBlock2.OutputNodes[0].ConnectTo(scalarBlock.InputNodes[0]);
            textBlock2.Cascade = true;
            textBlock2.Execute();
            Assert.AreEqual(3, textBlock2.OutputNodes[0].Object.Count);

            textBlock.Text = null;
            textBlock.Execute();
            Assert.AreEqual(0, textBlock.OutputNodes[0].Object.Count);  
        }
    }
}
