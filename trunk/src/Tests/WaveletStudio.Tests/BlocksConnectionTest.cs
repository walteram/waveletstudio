using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;

namespace WaveletStudio.Tests
{
    [TestClass]
    public class BlocksConnectionTest
    {
        [TestMethod]
        public void TestConnection()
        {
            var signalBlock = new GenerateSignalBlock{TemplateName = "Binary", Start = 0, Finish = 5, SamplingRate = 1, IgnoreLastSample = true};
            var sumBlock = new ScalarOperationBlock { Operation = ScalarOperationBlock.OperationEnum.Sum, Scalar = 1.5 };
            var multBlock = new ScalarOperationBlock { Operation = ScalarOperationBlock.OperationEnum.Multiply, Scalar = 2 };

            var signalOutputNode = signalBlock.OutputNodes[0];
            var sumInputNode = sumBlock.InputNodes[0];
            var sumOutputNode = sumBlock.OutputNodes[0];
            var multInputNode = multBlock.InputNodes[0];
            var multOutputNode = multBlock.OutputNodes[0];
            
            signalOutputNode.ConnectTo(ref sumInputNode);
            sumOutputNode.ConnectTo(ref multInputNode);

            signalBlock.Process();

            var outputText = multOutputNode.Object.ToString(0);

            Assert.AreEqual("3 5 3 5 3",
                            outputText);
        }
    }
}
