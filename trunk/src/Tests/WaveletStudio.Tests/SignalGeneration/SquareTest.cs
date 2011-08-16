using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.SignalGeneration;

namespace WaveletStudio.Tests.SignalGeneration
{
    [TestClass]
    public class SquareTest
    {
        [TestMethod]
        public void TestSquare()
        {
            var sampler = new Square
                              {
                Amplitude = 2,
                Frequency = 5,
                Phase = 0.25,
                Offset = 1,
                Start = 0,
                Finish = 1,
                SamplingRate = 120,
                IgnoreLastSample = true
            };
            var output = sampler.ExecuteSampler();
            var outputText = output.ToString();

            Assert.IsTrue(sampler.Name != null);
            Assert.AreEqual("3.000 3.000 3.000 3.000 3.000 3.000 3.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 3.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 -1.000 3.000 3.000 3.000 3.000 3.000",
                            outputText);

        }
    }
}
