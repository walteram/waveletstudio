using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class ScaleBlockTest
    {
        [TestMethod]
        public void TestScaleBlockExecute()
        {
            var signalBlock = new GenerateSignalBlock { TemplateName = "Binary", Start = 0, Finish = 5, SamplingRate = 1, IgnoreLastSample = true };
            var scaleBlock = new ScaleBlock
                                  {
                                      TimeScalingFactor = 2,
                                      AmplitudeScalingFactor = 0.5
                                  };
            scaleBlock.Execute();
            
            signalBlock.OutputNodes[0].ConnectTo(scaleBlock.InputNodes[0]);
            Assert.IsNotNull(scaleBlock.Name);
            Assert.IsNotNull(scaleBlock.Description);
            Assert.IsNotNull(scaleBlock.ProcessingType);

            signalBlock.Execute();
            Assert.AreEqual("0.0 0.5 0.0 0.5 0.0", scaleBlock.OutputNodes[0].Object.ToString(1));
            Assert.AreEqual(0, scaleBlock.OutputNodes[0].Object[0].Start);
            Assert.AreEqual(10, scaleBlock.OutputNodes[0].Object[0].Finish);

            scaleBlock.TimeScalingFactor = 1;
            signalBlock.Execute();
            Assert.AreEqual("0.0 0.5 0.0 0.5 0.0", scaleBlock.OutputNodes[0].Object.ToString(1));
            Assert.AreEqual("ScaleBlock", scaleBlock.GetAssemblyClassName());
            Assert.AreEqual(0, scaleBlock.OutputNodes[0].Object[0].Start);
            Assert.AreEqual(5, scaleBlock.OutputNodes[0].Object[0].Finish);

            scaleBlock.AmplitudeScalingFactor = 1;
            signalBlock.Execute();
            Assert.AreEqual("0.0 1.0 0.0 1.0 0.0", scaleBlock.OutputNodes[0].Object.ToString(1));

            scaleBlock.TimeScalingFactor = -1;
            Assert.AreEqual(0, scaleBlock.TimeScalingFactor);
            signalBlock.Execute();
            Assert.AreEqual("0.0 1.0 0.0 1.0 0.0", scaleBlock.OutputNodes[0].Object.ToString(1));

            scaleBlock.AmplitudeScalingFactor = -1;
            Assert.AreEqual(0, scaleBlock.AmplitudeScalingFactor);
            signalBlock.Execute();
            Assert.AreEqual("0.0 0.0 0.0 0.0 0.0", scaleBlock.OutputNodes[0].Object.ToString(1));
            Assert.AreEqual(0, scaleBlock.OutputNodes[0].Object[0].Start);
            Assert.AreEqual(5, scaleBlock.OutputNodes[0].Object[0].Finish);

            scaleBlock.AmplitudeScalingFactor = 5;
            scaleBlock.TimeScalingFactor = 2;
            signalBlock.Execute();
            Assert.AreEqual("0.0 5.0 0.0 5.0 0.0", scaleBlock.OutputNodes[0].Object.ToString(1));
            Assert.AreEqual(0, scaleBlock.OutputNodes[0].Object[0].Start);
            Assert.AreEqual(10, scaleBlock.OutputNodes[0].Object[0].Finish);

            var scaleBlock2 = (ScaleBlock)scaleBlock.Clone();
            scaleBlock2.AmplitudeScalingFactor = 3;
            scaleBlock2.TimeScalingFactor = 2;
            scaleBlock.OutputNodes[0].ConnectTo(scaleBlock2.InputNodes[0]);
            signalBlock.Execute();
            Assert.AreEqual("0.00 15.00 0.00 15.00 0.00", scaleBlock2.OutputNodes[0].Object.ToString(2));
            Assert.AreEqual(0, scaleBlock2.OutputNodes[0].Object[0].Start);
            Assert.AreEqual(20, scaleBlock2.OutputNodes[0].Object[0].Finish);

            scaleBlock.Cascade = false;
            scaleBlock2 = (ScaleBlock)scaleBlock.Clone();
            scaleBlock.OutputNodes[0].ConnectTo(scaleBlock2.InputNodes[0]);
            signalBlock.Execute();
            Assert.AreEqual(0, scaleBlock2.OutputNodes[0].Object.Count);
        }
    }
}
