using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Functions
{
    [TestClass]
    public class WaveMathInterpolationTests
    {
        [TestMethod]
        public void TestInterpolateLinear()
        {
            var signal = new Signal(new double[] {1, 2, 3, 4}){Start = 1, Finish = 4, SamplingInterval = 1};            
            var newSignal = WaveMath.InterpolateLinear(signal, 2);
            Assert.AreEqual("1.0 1.5 2.0 2.5 3.0 3.5 4.0", newSignal.ToString(1));
            Assert.AreEqual(1, newSignal.Start);
            Assert.AreEqual(4, newSignal.Finish);
            Assert.AreEqual(0.5, newSignal.SamplingInterval);
            Assert.AreEqual(2, newSignal.SamplingRate);

            signal = new Signal(new double[] { -7, 4, 8, -2, 1, -55, 2 }) { Start = -1, Finish = 2, SamplingInterval = 4d/7d };
            newSignal = WaveMath.InterpolateLinear(signal, 7);
            Assert.AreEqual("-7.0000 -5.4286 -3.8571 -2.2857 -0.7143 0.8571 2.4286 4.0000 4.5714 5.1429 5.7143 6.2857 6.8571 7.4286 8.0000 6.5714 5.1429 3.7143 2.2857 0.8571 -0.5714 -2.0000 -1.5714 -1.1429 -0.7143 -0.2857 0.1429 0.5714 1.0000 -7.0000 -15.0000 -23.0000 -31.0000 -39.0000 -47.0000 -55.0000 -46.8571 -38.7143 -30.5714 -22.4286 -14.2857 -6.1429 2.0000", newSignal.ToString(4));
            Assert.AreEqual(-1, newSignal.Start);
            Assert.AreEqual(2, newSignal.Finish);
            Assert.AreEqual(4d / 7d / 7d, newSignal.SamplingInterval);
            Assert.AreEqual(Convert.ToInt32(1/(4d / 7d / 7d)), newSignal.SamplingRate);

            signal = new Signal(new double[] { -7, 4, 8, -2, 1, -55, 2 }) { Start = 1, Finish = 4, SamplingInterval = 4d / 7d };
            newSignal = WaveMath.InterpolateLinear(signal, 1);
            Assert.AreEqual("-7, 4, 8, -2, 1, -55, 2", newSignal.ToString(0, ", "));
            Assert.AreEqual(1, newSignal.Start);
            Assert.AreEqual(4, newSignal.Finish);
            Assert.AreEqual(4d / 7d, newSignal.SamplingInterval);
            Assert.AreEqual(Convert.ToInt32(1 / (4d / 7d)), newSignal.SamplingRate);
        }

        [TestMethod]
        public void TestInterpolateNearest()
        {
            var signal = new Signal(new double[] { 1, 2, 3, 4 }) { Start = 1, Finish = 4, SamplingInterval = 1 };
            var newSignal = WaveMath.InterpolateNearest(signal, 2);
            Assert.AreEqual("1 2 2 3 3 4 4", newSignal.ToString(0));
            Assert.AreEqual(1, newSignal.Start);
            Assert.AreEqual(4, newSignal.Finish);
            Assert.AreEqual(0.5, newSignal.SamplingInterval);
            Assert.AreEqual(2, newSignal.SamplingRate);

            signal = new Signal(new double[] { -7, 4, 8, -2, 1, -55, 2 }) { Start = -1, Finish = 2, SamplingInterval = 4d / 7d };
            newSignal = WaveMath.InterpolateNearest(signal, 7);
            Assert.AreEqual("-7 -7 -7 -7 4 4 4 4 4 4 4 8 8 8 8 8 8 8 -2 -2 -2 -2 -2 -2 -2 1 1 1 1 1 1 1 -55 -55 -55 -55 -55 -55 -55 2 2 2 2", newSignal.ToString(0));
            Assert.AreEqual(-1, newSignal.Start);
            Assert.AreEqual(2, newSignal.Finish);
            Assert.AreEqual(4d / 7d / 7d, newSignal.SamplingInterval);
            Assert.AreEqual(Convert.ToInt32(1 / (4d / 7d / 7d)), newSignal.SamplingRate);

            signal = new Signal(new double[] { -7, 4, 8, -2, 1, -55, 2 }) { Start = 1, Finish = 4, SamplingInterval = 4d / 7d };
            newSignal = WaveMath.InterpolateNearest(signal, 1);
            Assert.AreEqual("-7, 4, 8, -2, 1, -55, 2", newSignal.ToString(0, ", "));
            Assert.AreEqual(1, newSignal.Start);
            Assert.AreEqual(4, newSignal.Finish);
            Assert.AreEqual(4d / 7d, newSignal.SamplingInterval);
            Assert.AreEqual(Convert.ToInt32(1 / (4d / 7d)), newSignal.SamplingRate);
        }
    }
}
