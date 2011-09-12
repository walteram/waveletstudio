using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class FFTBlockTest
    {
        [TestMethod]
        public void TestFFTBlockExecute()
        {
            var signalBlock = new GenerateSignalBlock { TemplateName = "Binary", Start = 0, Finish = 5, SamplingRate = 1, IgnoreLastSample = true };
            var fftBlock = new FFTBlock();
            fftBlock.Execute();

            //todo: implement test

            /*
            signalBlock.OutputNodes[0].ConnectTo(fftBlock.InputNodes[0]);
            Assert.IsNotNull(fftBlock.Name);
            Assert.IsNotNull(fftBlock.Description);
            Assert.IsNotNull(fftBlock.ProcessingType);

            signalBlock.Execute();
            Assert.AreEqual("2.2 2.2", fftBlock.OutputNodes[0].Object.ToString(1));

            var downSampleBlock2 = (DownSampleBlock)fftBlock.Clone();
            fftBlock.OutputNodes[0].ConnectTo(downSampleBlock2.InputNodes[0]);
            signalBlock.Execute();
            Assert.AreEqual("2.2", downSampleBlock2.OutputNodes[0].Object.ToString(1));

            fftBlock.Cascade = false;
            downSampleBlock2 = (DownSampleBlock)fftBlock.Clone();
            fftBlock.OutputNodes[0].ConnectTo(downSampleBlock2.InputNodes[0]);
            signalBlock.Execute();
            Assert.IsNull(downSampleBlock2.OutputNodes[0].Object);*/
        }
    }
}
