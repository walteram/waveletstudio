using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class RampBlockTest
    {
        [TestMethod]
        public void TestRampBlockExecute()
        {
            var signalBlock = new RampFunctionBlock { Start = 0, Finish = 5, RampStart = 1, RampFinish = 3, SamplingInterval = 1, SamplingRate = 1, Amplitude = 2, Offset = 1, ReturnToZero = true };
            signalBlock.Execute();
            
            var newSignal = (RampFunctionBlock)signalBlock.Clone();
            newSignal.Finish = 6;
            newSignal.ReturnToZero = false;
            newSignal.Execute();

            Assert.IsNotNull(signalBlock.Name);
            Assert.IsNotNull(signalBlock.Description);
            Assert.IsNotNull(signalBlock.ProcessingType);

            Assert.AreNotSame(signalBlock.OutputNodes[0], newSignal.OutputNodes[0]);
            Assert.AreNotSame(signalBlock.OutputNodes[0].Object, newSignal.OutputNodes[0].Object);

            Assert.AreEqual("1 1 3 5 5 5 5", newSignal.OutputNodes[0].Object.ToString(0));
            Assert.AreEqual("1 1 3 5 1 1", signalBlock.OutputNodes[0].Object.ToString(0));

            signalBlock.Cascade = false;
            var scalarBlock = new ScalarOperationBlock { Operation = WaveMath.OperationEnum.Sum, Value = 1.5 };
            signalBlock.OutputNodes[0].ConnectTo(scalarBlock.InputNodes[0]);
            signalBlock.Execute();
            Assert.AreEqual(0, scalarBlock.OutputNodes[0].Object.Count);

            signalBlock.Cascade = true;
            signalBlock.Execute();
            Assert.IsNotNull(scalarBlock.OutputNodes[0].Object[0]);
            Assert.AreEqual("2.5 2.5 4.5 6.5 2.5 2.5", scalarBlock.OutputNodes[0].Object.ToString(1));

            signalBlock.IgnoreLastSample = true;
            signalBlock.SamplingRate = 0;
            signalBlock.Execute();
            Assert.AreEqual("1 1 3 5 1", signalBlock.OutputNodes[0].Object.ToString(0));
        }
    }
}
