using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.SignalGeneration;

namespace WaveletStudio.Tests.SignalGeneration
{
    [TestClass]
    public class SawtoothTest
    {
        [TestMethod]
        public void TestSawtooth()
        {
            var sampler = new Sawtooth
            {
                Amplitude = 2,
                Frequency = 5,
                Phase = 0.25,
                Offset = 1,
                Start = 0,
                Finish = 1,
                SamplingRate = 120,
                EndingOption = CommonSignalBase.EndingOptionEnum.ExcludeLast
            };
            var output = sampler.ExecuteSampler();
            var outputText = output.ToString();

            Assert.IsTrue(sampler.Name != null);
            Assert.AreEqual("2.000 2.167 2.333 2.500 2.667 2.833 3.000 -0.833 -0.667 -0.500 -0.333 -0.167 0.000 0.167 0.333 0.500 0.667 0.833 1.000 1.167 1.333 1.500 1.667 1.833 2.000 2.167 2.333 2.500 2.667 2.833 3.000 -0.833 -0.667 -0.500 -0.333 -0.167 0.000 0.167 0.333 0.500 0.667 0.833 1.000 1.167 1.333 1.500 1.667 1.833 2.000 2.167 2.333 2.500 2.667 2.833 3.000 -0.833 -0.667 -0.500 -0.333 -0.167 0.000 0.167 0.333 0.500 0.667 0.833 1.000 1.167 1.333 1.500 1.667 1.833 2.000 2.167 2.333 2.500 2.667 2.833 3.000 -0.833 -0.667 -0.500 -0.333 -0.167 0.000 0.167 0.333 0.500 0.667 0.833 1.000 1.167 1.333 1.500 1.667 1.833 2.000 2.167 2.333 2.500 2.667 2.833 3.000 -0.833 -0.667 -0.500 -0.333 -0.167 0.000 0.167 0.333 0.500 0.667 0.833 1.000 1.167 1.333 1.500 1.667 1.833 ",
                            outputText);

        }
    }
}
