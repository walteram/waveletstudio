using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.SignalGeneration;

namespace WaveletStudio.Tests.SignalGeneration
{
    [TestClass]
    public class TriangleTest
    {
        [TestMethod]
        public void TestSquare()
        {
            var sampler = new Triangle
                              {
                                  Amplitude = 2,
                                  Frequency = 5,
                                  Phase = 0.25,
                                  Offset = 1,
                                  Start = 0,
                                  Finish = 1,
                                  SamplingRate = 0,
                                  SamplingInterval = 1d/120d,
                                  EndingOption = CommonSignalBase.EndingOptionEnum.ExcludeLast
                              };
            sampler.SamplingRate = 120;
            var output = sampler.ExecuteSampler();
            var outputText = output.ToString();

            Assert.IsTrue(sampler.Name != null);
            Assert.AreEqual("3.000 2.667 2.333 2.000 1.667 1.333 1.000 0.667 0.333 0.000 -0.333 -0.667 -1.000 -0.667 -0.333 0.000 0.333 0.667 1.000 1.333 1.667 2.000 2.333 2.667 3.000 2.667 2.333 2.000 1.667 1.333 1.000 0.667 0.333 0.000 -0.333 -0.667 -1.000 -0.667 -0.333 0.000 0.333 0.667 1.000 1.333 1.667 2.000 2.333 2.667 3.000 2.667 2.333 2.000 1.667 1.333 1.000 0.667 0.333 0.000 -0.333 -0.667 -1.000 -0.667 -0.333 0.000 0.333 0.667 1.000 1.333 1.667 2.000 2.333 2.667 3.000 2.667 2.333 2.000 1.667 1.333 1.000 0.667 0.333 0.000 -0.333 -0.667 -1.000 -0.667 -0.333 0.000 0.333 0.667 1.000 1.333 1.667 2.000 2.333 2.667 3.000 2.667 2.333 2.000 1.667 1.333 1.000 0.667 0.333 0.000 -0.333 -0.667 -1.000 -0.667 -0.333 0.000 0.333 0.667 1.000 1.333 1.667 2.000 2.333 2.667 ",
                            outputText);

        }
    }
}
