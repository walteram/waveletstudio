using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class DownSampleBlockTest
    {
        [TestMethod]
        public void TestDownSampleBlockExecute()
        {
            var signalBlock = new GenerateSignalBlock { Offset = 1.2, TemplateName = "Binary", Start = 0, Finish = 5, SamplingRate = 1, IgnoreLastSample = true };
            var downSampleBlock = new DownSampleBlock();
            downSampleBlock.Execute();
            
            signalBlock.OutputNodes[0].ConnectTo(downSampleBlock.InputNodes[0]);
            Assert.IsNotNull(downSampleBlock.Name);
            Assert.IsNotNull(downSampleBlock.Description);
            Assert.IsNotNull(downSampleBlock.ProcessingType);

            signalBlock.Execute();
            Assert.AreEqual("2.2 2.2", downSampleBlock.OutputNodes[0].Object.ToString(1));

            var downSampleBlock2 = (DownSampleBlock)downSampleBlock.Clone();
            downSampleBlock.OutputNodes[0].ConnectTo(downSampleBlock2.InputNodes[0]);
            signalBlock.Execute();
            Assert.AreEqual("2.2", downSampleBlock2.OutputNodes[0].Object.ToString(1));

            downSampleBlock.Cascade = false;
            downSampleBlock2 = (DownSampleBlock)downSampleBlock.Clone();
            downSampleBlock.OutputNodes[0].ConnectTo(downSampleBlock2.InputNodes[0]);
            signalBlock.Execute();
            Assert.AreEqual(0, downSampleBlock2.OutputNodes[0].Object.Count);
        }
    }
}
