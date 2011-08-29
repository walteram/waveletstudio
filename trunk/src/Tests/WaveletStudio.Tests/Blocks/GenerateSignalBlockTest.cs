using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class GenerateSignalBlockTest
    {
        [TestMethod]
        public void TestGenerateSignalBlockExecute()
        {
            var signalBlock = new GenerateSignalBlock{TemplateName = "Binary", Start = 0, Finish = 5, SamplingRate = 1, IgnoreLastSample = true};
            signalBlock.Execute();
            
            var newSignal = (GenerateSignalBlock)signalBlock.Clone();
            newSignal.Finish = 6;
            newSignal.Execute();

            Assert.AreNotSame(signalBlock.OutputNodes[0], newSignal.OutputNodes[0]);
            Assert.AreNotSame(signalBlock.OutputNodes[0].Object, newSignal.OutputNodes[0].Object);

            Assert.AreEqual("0.0 1.0 0.0 1.0 0.0 1.0", newSignal.OutputNodes[0].Object.ToString(1));
            Assert.AreEqual("0.0 1.0 0.0 1.0 0.0", signalBlock.OutputNodes[0].Object.ToString(1));
        }
    }
}
