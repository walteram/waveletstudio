using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.SignalGeneration;

namespace WaveletStudio.Tests.SignalGeneration
{
    [TestClass]
    public class BinaryTest
    {
        [TestMethod]
        public void TestBinary()
        {
            var sampler = new Binary()
            {
                Amplitude = 2,
                Offset = 1,
                Start = 0,
                Finish = 1,
                SamplingRate = 120,
                IgnoreLastSample = true
            };
            var output = sampler.ExecuteSampler();
            var outputText = output.ToString();

            Assert.IsTrue(sampler.Name != null);
            Assert.AreEqual("1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000 1.000 3.000",
                            outputText);

        }
    }
}
