using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.SignalGeneration;

namespace WaveletStudio.Tests.SignalGeneration
{
    [TestClass]
    public class SineTest
    {
        [TestMethod]
        public void TestSine()
        {
            var sampler = new Sine
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
            Assert.AreEqual("3.000 2.932 2.732 2.414 2.000 1.518 1.000 0.482 0.000 -0.414 -0.732 -0.932 -1.000 -0.932 -0.732 -0.414 0.000 0.482 1.000 1.518 2.000 2.414 2.732 2.932 3.000 2.932 2.732 2.414 2.000 1.518 1.000 0.482 0.000 -0.414 -0.732 -0.932 -1.000 -0.932 -0.732 -0.414 0.000 0.482 1.000 1.518 2.000 2.414 2.732 2.932 3.000 2.932 2.732 2.414 2.000 1.518 1.000 0.482 0.000 -0.414 -0.732 -0.932 -1.000 -0.932 -0.732 -0.414 0.000 0.482 1.000 1.518 2.000 2.414 2.732 2.932 3.000 2.932 2.732 2.414 2.000 1.518 1.000 0.482 0.000 -0.414 -0.732 -0.932 -1.000 -0.932 -0.732 -0.414 0.000 0.482 1.000 1.518 2.000 2.414 2.732 2.932 3.000 2.932 2.732 2.414 2.000 1.518 1.000 0.482 0.000 -0.414 -0.732 -0.932 -1.000 -0.932 -0.732 -0.414 0.000 0.482 1.000 1.518 2.000 2.414 2.732 2.932 ",
                            outputText);

        }
    }
}
