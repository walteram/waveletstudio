using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class ScalarOperationBlockTest
    {
        [TestMethod]
        public void TestScalarOperationBlockExecute()
        {
            var signalBlock = new GenerateSignalBlock { TemplateName = "Binary", Start = 0, Finish = 5, SamplingRate = 1, IgnoreLastSample = true };
            var scalarBlock = new ScalarOperationBlock { Operation = ScalarOperationBlock.OperationEnum.Sum, Value = 1.5 };            
            signalBlock.OutputNodes[0].ConnectTo(scalarBlock.InputNodes[0]);
            
            signalBlock.Execute();
            Assert.AreEqual("1.5 2.5 1.5 2.5 1.5", scalarBlock.OutputNodes[0].Object.ToString(1));

            scalarBlock.Operation = ScalarOperationBlock.OperationEnum.Multiply;
            signalBlock.Execute();
            Assert.AreEqual("0.0 1.5 0.0 1.5 0.0", scalarBlock.OutputNodes[0].Object.ToString(1));

        }
    }
}
