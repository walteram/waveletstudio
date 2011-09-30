using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class WaveletBlockTest
    {
        // TODO: Finish this test
        [TestMethod]
        public void TestScalarOperationBlockExecute()
        {
            var signalBlock = new GenerateSignalBlock { TemplateName = "Binary", Start = 0, Finish = 10, SamplingRate = 1, IgnoreLastSample = true };
            var waveletBlock = new WaveletBlock
                                   {
                                       WaveletName = "db4",
                                       Level = 2,
                                       ExtensionMode = SignalExtension.ExtensionMode.SymmetricHalfPoint
                                   };
            waveletBlock.Execute();

            signalBlock.OutputNodes[0].ConnectTo(waveletBlock.InputNodes[0]);
            Assert.IsNotNull(waveletBlock.Name);
            Assert.IsNotNull(waveletBlock.Description);
            Assert.IsNotNull(waveletBlock.ProcessingType);

            signalBlock.Execute();
            Assert.AreEqual("0.7 0.9 0.2 0.7 0.7 0.8 0.5 1.2", waveletBlock.OutputNodes[0].Object[0].ToString(1));
            Assert.AreEqual("1.0 0.8 1.1 0.8 1.0 0.8 1.6", waveletBlock.OutputNodes[0].Object[1].ToString(1));
            Assert.AreEqual("-0.2 -0.8 -0.7 -0.7 -0.7 0.2 0.8 0.7", waveletBlock.OutputNodes[1].Object[0].ToString(1));
            Assert.AreEqual("-0.1 -0.4 0.0 -0.3 0.1 0.2 0.1", waveletBlock.OutputNodes[1].Object[1].ToString(1));
            Assert.AreEqual(signalBlock.OutputNodes[0].Object.ToString(1), waveletBlock.OutputNodes[2].Object[0].ToString(1));
            Assert.AreEqual(5, waveletBlock.OutputNodes[3].Object.Count);
            Assert.AreEqual("0.7 0.9 0.2 0.7 0.7 0.8 0.5 1.2", waveletBlock.OutputNodes[3].Object[0].ToString(1));
            Assert.AreEqual("-0.2 -0.8 -0.7 -0.7 -0.7 0.2 0.8 0.7", waveletBlock.OutputNodes[3].Object[1].ToString(1));
            Assert.AreEqual("1.0 0.8 1.1 0.8 1.0 0.8 1.6", waveletBlock.OutputNodes[3].Object[2].ToString(1));
            Assert.AreEqual("-0.1 -0.4 0.0 -0.3 0.1 0.2 0.1", waveletBlock.OutputNodes[3].Object[3].ToString(1));
            Assert.AreEqual(signalBlock.OutputNodes[0].Object.ToString(1), waveletBlock.OutputNodes[3].Object[4].ToString(1));

            var block2 = (WaveletBlock)waveletBlock.Clone();
            signalBlock.OutputNodes[0].ConnectTo(block2.InputNodes[0]);
            signalBlock.Execute();
            Assert.AreEqual("0.7 0.9 0.2 0.7 0.7 0.8 0.5 1.2", block2.OutputNodes[0].Object[0].ToString(1));

            waveletBlock.Cascade = false;
            block2 = (WaveletBlock)waveletBlock.Clone();
            waveletBlock.OutputNodes[0].ConnectTo(block2.InputNodes[0]);
            signalBlock.Execute();
            Assert.AreEqual(0, block2.OutputNodes[0].Object.Count);

        }
    }
}
