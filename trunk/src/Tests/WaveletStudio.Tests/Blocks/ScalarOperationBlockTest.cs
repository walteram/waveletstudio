using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class ScalarOperationBlockTest
    {
        [TestMethod]
        public void TestScalarOperationBlockExecute()
        {
            var signalBlock = new GenerateSignalBlock { TemplateName = "Binary", Start = 0, Finish = 5, SamplingRate = 1, IgnoreLastSample = true };
            var scalarBlock = new ScalarOperationBlock { Operation = WaveMath.OperationEnum.Sum, Value = 1.5 };
            scalarBlock.Execute();
            
            signalBlock.OutputNodes[0].ConnectTo(scalarBlock.InputNodes[0]);
            Assert.IsNotNull(scalarBlock.Name);
            Assert.IsNotNull(scalarBlock.Description);
            Assert.IsNotNull(scalarBlock.ProcessingType);

            signalBlock.Execute();
            Assert.AreEqual("1.5 2.5 1.5 2.5 1.5", scalarBlock.OutputNodes[0].Object.ToString(1));

            scalarBlock.Operation = WaveMath.OperationEnum.Multiply;
            signalBlock.Execute();
            Assert.AreEqual("0.0 1.5 0.0 1.5 0.0", scalarBlock.OutputNodes[0].Object.ToString(1));

            scalarBlock.Operation = WaveMath.OperationEnum.Subtract;
            signalBlock.Execute();
            Assert.AreEqual("-1.5 -0.5 -1.5 -0.5 -1.5", scalarBlock.OutputNodes[0].Object.ToString(1));

            scalarBlock.Operation = WaveMath.OperationEnum.Divide;
            signalBlock.Execute();
            Assert.AreEqual("0.00 0.67 0.00 0.67 0.00", scalarBlock.OutputNodes[0].Object.ToString(2));
            
            var scalarBlock2 = (ScalarOperationBlock)scalarBlock.Clone();
            scalarBlock2.Operation = WaveMath.OperationEnum.Sum;
            scalarBlock2.Value = 3.1;
            scalarBlock.OutputNodes[0].ConnectTo(scalarBlock2.InputNodes[0]);
            signalBlock.Execute();
            Assert.AreEqual("3.10 3.77 3.10 3.77 3.10", scalarBlock2.OutputNodes[0].Object.ToString(2));

            scalarBlock.Cascade = false;
            scalarBlock2 = (ScalarOperationBlock)scalarBlock.Clone();
            scalarBlock.OutputNodes[0].ConnectTo(scalarBlock2.InputNodes[0]);
            signalBlock.Execute();
            Assert.IsNull(scalarBlock2.OutputNodes[0].Object);
        }
    }
}
