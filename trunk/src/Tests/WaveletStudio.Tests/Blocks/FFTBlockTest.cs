using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class FFTBlockTest
    {
        [TestMethod]
        public void TestFFTBlockExecute()
        {
            //Test with periodic signal
            var signalBlock = new GenerateSignalBlock { TemplateName = "Binary", Start = 0, Finish = 7, SamplingRate = 1, Offset = 1.1};
            var fftBlock = new FFTBlock();
            fftBlock.Execute();

            signalBlock.OutputNodes[0].ConnectTo(fftBlock.InputNodes[0]);
            Assert.IsNotNull(fftBlock.Name);
            Assert.IsNotNull(fftBlock.Description);
            Assert.IsNotNull(fftBlock.ProcessingType);

            signalBlock.Execute();
            Assert.AreEqual("3.2 0.0 0.0 0.0", fftBlock.OutputNodes[0].Object.ToString(1));

            //Test cascade
            var scalarBlock = new ScalarOperationBlock{Value = 1, Operation = WaveMath.OperationEnum.Sum};
            fftBlock.OutputNodes[0].ConnectTo(scalarBlock.InputNodes[0]);
            signalBlock.Execute();
            Assert.AreEqual("4.2 1.0 1.0 1.0", scalarBlock.OutputNodes[0].Object.ToString(1));

            //Test when cascade is false
            fftBlock.Cascade = false;
            var fftBlock2 = (FFTBlock)fftBlock.Clone();
            fftBlock.OutputNodes[0].ConnectTo(fftBlock2.InputNodes[0]);
            signalBlock.Execute();
            Assert.AreEqual(0, fftBlock2.OutputNodes[0].Object.Count);
        }
    }
}
