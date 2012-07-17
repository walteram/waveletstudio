using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class LogicOperationBlockTest
    {
        [TestMethod]
        public void TestSampleBasedOperationBlockExecute()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
            var signalBlock1 = new GenerateSignalBlock { TemplateName = "Binary", Start = 0, Finish = 5, SamplingRate = 1, IgnoreLastSample = true };
            var signalBlock2 = new GenerateSignalBlock { TemplateName = "Binary", Start = 0, Finish = 5, SamplingRate = 1, IgnoreLastSample = true };
            var block = new LogicOperationBlock { Operation = WaveMath.LogicalOperationEnum.Or };
            block.Execute();
            
            signalBlock1.OutputNodes[0].ConnectTo(block.InputNodes[0]);
            signalBlock2.OutputNodes[0].ConnectTo(block.InputNodes[1]);
            Assert.IsNotNull(block.Name);
            Assert.IsNotNull(block.Description);
            Assert.IsNotNull(block.ProcessingType);
            Assert.AreEqual("Or", block.GetAssemblyClassName());

            signalBlock1.Execute();
            signalBlock2.Execute();
            Assert.AreEqual("0 1 0 1 0", block.OutputNodes[0].Object.ToString(0));

            var block2 = (LogicOperationBlock)block.Clone();
            block2.Operation = WaveMath.LogicalOperationEnum.NotOr;
            block.OutputNodes[0].ConnectTo(block2.InputNodes[0]);
            signalBlock2.OutputNodes[0].ConnectTo(block2.InputNodes[1]);
            signalBlock1.Execute();
            signalBlock2.Execute();
            Assert.AreEqual("1 0 1 0 1", block2.OutputNodes[0].Object[0].ToString(0));

            block.Cascade = false;
            block2 = (LogicOperationBlock)block.Clone();
            block.OutputNodes[0].ConnectTo(block2.InputNodes[0]);
            signalBlock1.Execute();
            Assert.AreEqual(0, block2.OutputNodes[0].Object.Count);
            Assert.AreEqual("", block2.OutputNodes[0].Object.ToString(0));

            signalBlock1 = new GenerateSignalBlock { TemplateName = "Binary", Start = 0, Finish = 3, SamplingRate = 1, IgnoreLastSample = true };
            signalBlock2 = new GenerateSignalBlock { TemplateName = "Binary", Start = 0, Finish = 5, SamplingRate = 1, IgnoreLastSample = true };
            signalBlock1.OutputNodes[0].ConnectTo(block.InputNodes[0]);
            signalBlock2.OutputNodes[0].ConnectTo(block.InputNodes[1]);
            signalBlock1.Execute();
            signalBlock2.Execute();
            signalBlock2.OutputNodes[0].Object.Add(new Signal(new double[] { 1, 2, 3, 4 }));
            block.Execute();
            Assert.AreEqual("0 1 0 0 0", block.OutputNodes[0].Object[0].ToString(0));
            Assert.AreEqual("1 1 1 0", block.OutputNodes[0].Object[1].ToString(0));

            signalBlock2.OutputNodes[0].Object.Clear();
            block.Execute();
            Assert.AreEqual(1, block.OutputNodes[0].Object.Count);
            Assert.AreEqual("0 1 0", block.OutputNodes[0].Object[0].ToString(0));
        }
    }
}
