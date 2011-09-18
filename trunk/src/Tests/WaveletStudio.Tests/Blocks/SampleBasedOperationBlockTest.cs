using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class SampleBasedOperationBlockTest
    {
        [TestMethod]
        public void TestSampleBasedOperationBlockExecute()
        {
            var signalBlock1 = new GenerateSignalBlock { TemplateName = "Binary", Start = 0, Finish = 5, SamplingRate = 1, IgnoreLastSample = true };
            var signalBlock2 = new GenerateSignalBlock { TemplateName = "Binary", Start = 0, Finish = 5, SamplingRate = 1, IgnoreLastSample = true };
            var scalarBlock = new SampleBasedOperationBlock { Operation = WaveMath.OperationEnum.Sum };
            scalarBlock.Execute();
            
            signalBlock1.OutputNodes[0].ConnectTo(scalarBlock.InputNodes[0]);
            signalBlock2.OutputNodes[0].ConnectTo(scalarBlock.InputNodes[1]);
            Assert.IsNotNull(scalarBlock.Name);
            Assert.IsNotNull(scalarBlock.Description);
            Assert.IsNotNull(scalarBlock.ProcessingType);

            signalBlock1.Execute();
            signalBlock2.Execute();
            Assert.AreEqual("0 2 0 2 0", scalarBlock.OutputNodes[0].Object.ToString(0));

            var scalarBlock2 = (SampleBasedOperationBlock)scalarBlock.Clone();
            scalarBlock2.Operation = WaveMath.OperationEnum.Subtract;
            scalarBlock.OutputNodes[0].ConnectTo(scalarBlock2.InputNodes[0]);
            signalBlock1.Execute();
            Assert.AreEqual("0 2 0 2 0", scalarBlock2.OutputNodes[0].Object.ToString(0));

            scalarBlock.Cascade = false;
            scalarBlock2 = (SampleBasedOperationBlock)scalarBlock.Clone();
            scalarBlock.OutputNodes[0].ConnectTo(scalarBlock2.InputNodes[0]);
            signalBlock1.Execute();
            Assert.IsNull(scalarBlock2.OutputNodes[0].Object);
        }
    }
}
