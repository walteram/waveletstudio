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

        [TestMethod]
        public void TestInterpolateCubic()
        {
            var signal = new Signal(new double[] { 1, 2, 3, 4 }) { Start = 1, Finish = 4, SamplingInterval = 1 };
            var newSignal = WaveMath.InterpolateCubic(signal, 2);
            Assert.AreEqual("1.0 1.5 2.0 2.5 3.0 3.5 4.0", newSignal.ToString(1));
            Assert.AreEqual(1, newSignal.Start);
            Assert.AreEqual(4, newSignal.Finish);
            Assert.AreEqual(0.5, newSignal.SamplingInterval);
            Assert.AreEqual(2, newSignal.SamplingRate);

            signal = new Signal(new double[] { -7, 4, 8, -2, 1, -55, 2 }) { Start = -1, Finish = 2, SamplingInterval = 4d / 7d };
            newSignal = WaveMath.InterpolateCubic(signal, 7);
            Assert.AreEqual("-7.0000 -6.0436 -4.7484 -3.1947 -1.4627 0.3672 2.2149 4.0000 5.6422 7.0613 8.1770 8.9090 9.1770 8.9008 8.0000 6.4543 4.4827 2.3642 0.3778 -1.1977 -2.0833 -2.0000 -0.8092 1.0661 3.0629 4.6179 5.1678 4.1496 1.0000 -4.6313 -12.2428 -21.1202 -30.5489 -39.8148 -48.2032 -55.0000 -59.4906 -60.9607 -58.6960 -51.9819 -40.1041 -22.3483 2.0000", newSignal.ToString(4));
            Assert.AreEqual(-1, newSignal.Start);
            Assert.AreEqual(2, newSignal.Finish);
            Assert.AreEqual(Convert.ToDecimal(4d / 7d / 7d), Convert.ToDecimal(newSignal.SamplingInterval));
            Assert.AreEqual(Convert.ToInt32(1 / (4d / 7d / 7d)), newSignal.SamplingRate);

            signal = new Signal(new double[] { -7, 4, 8, -2, 1, -55, 2 }) { Start = 1, Finish = 4, SamplingInterval = 4d / 7d };
            newSignal = WaveMath.InterpolateCubic(signal, 1);
            Assert.AreEqual("-7, 4, 8, -2, 1, -55, 2", newSignal.ToString(0, ", "));
            Assert.AreEqual(1, newSignal.Start);
            Assert.AreEqual(4, newSignal.Finish);
            Assert.AreEqual(4d / 7d, newSignal.SamplingInterval);
            Assert.AreEqual(Convert.ToInt32(1 / (4d / 7d)), newSignal.SamplingRate);
        }

        [TestMethod]
        public void TestInterpolateNeville()
        {
            var signal = new Signal(new double[] { 1, 2, 3, 4 }) { Start = 1, Finish = 4, SamplingInterval = 1 };
            var newSignal = WaveMath.InterpolateNeville(signal, 2);
            Assert.AreEqual("1.0 1.5 2.0 2.5 3.0 3.5 4.0", newSignal.ToString(1));
            Assert.AreEqual(1, newSignal.Start);
            Assert.AreEqual(4, newSignal.Finish);
            Assert.AreEqual(0.5, newSignal.SamplingInterval);
            Assert.AreEqual(2, newSignal.SamplingRate);

            signal = new Signal(new double[] { -7, 4, 8, -2, 1, -55, 2 }) { Start = -1, Finish = 2, SamplingInterval = 4d / 7d };
            newSignal = WaveMath.InterpolateNeville(signal, 7);
            Assert.AreEqual("-7.0000 -17.0107 -19.7983 -17.8778 -13.2265 -7.3518 -1.3568 4.0000 8.2479 11.1517 12.6632 12.8768 11.9897 10.2650 8.0000 5.4978 3.0434 0.8835 -0.7896 -1.8495 -2.2444 -2.0000 -1.2202 -0.0823 1.1709 2.2438 2.8040 2.5028 1.0000 -2.0082 -6.7562 -13.3726 -21.8395 -31.9478 -43.2485 -55.0000 -66.1111 -75.0803 -79.9307 -78.1415 -66.5742 -41.3964 2.0000", newSignal.ToString(4));
            Assert.AreEqual(-1, newSignal.Start);
            Assert.AreEqual(2, newSignal.Finish);
            Assert.AreEqual(Convert.ToDecimal(4d / 7d / 7d), Convert.ToDecimal(newSignal.SamplingInterval));
            Assert.AreEqual(Convert.ToInt32(1 / (4d / 7d / 7d)), newSignal.SamplingRate);

            signal = new Signal(new double[] { -7, 4, 8, -2, 1, -55, 2 }) { Start = 1, Finish = 4, SamplingInterval = 4d / 7d };
            newSignal = WaveMath.InterpolateNeville(signal, 1);
            Assert.AreEqual("-7, 4, 8, -2, 1, -55, 2", newSignal.ToString(0, ", "));
            Assert.AreEqual(1, newSignal.Start);
            Assert.AreEqual(4, newSignal.Finish);
            Assert.AreEqual(4d / 7d, newSignal.SamplingInterval);
            Assert.AreEqual(Convert.ToInt32(1 / (4d / 7d)), newSignal.SamplingRate);
        }

        [TestMethod]
        public void TestInterpolateNewton()
        {
            var signal = new Signal(new double[] { 1, 2, 3, 4 }) { Start = 1, Finish = 4, SamplingInterval = 1 };
            var newSignal = WaveMath.InterpolateNewton(signal, 2);
            Assert.AreEqual("1.0 1.5 2.0 2.5 3.0 3.5 4.0", newSignal.ToString(1));
            Assert.AreEqual(1, newSignal.Start);
            Assert.AreEqual(4, newSignal.Finish);
            Assert.AreEqual(0.5, newSignal.SamplingInterval);
            Assert.AreEqual(2, newSignal.SamplingRate);

            signal = new Signal(new double[] { -7, 4, 8, -2, 1, -55, 2 }) { Start = -1, Finish = 2, SamplingInterval = 4d / 7d };
            newSignal = WaveMath.InterpolateNewton(signal, 7);
            Assert.AreEqual("-7.0000 -17.0107 -19.7983 -17.8778 -13.2265 -7.3518 -1.3568 4.0000 8.2479 11.1517 12.6632 12.8768 11.9897 10.2650 8.0000 5.4978 3.0434 0.8835 -0.7896 -1.8495 -2.2444 -2.0000 -1.2202 -0.0823 1.1709 2.2438 2.8040 2.5028 1.0000 -2.0082 -6.7562 -13.3726 -21.8395 -31.9478 -43.2485 -55.0000 -66.1111 -75.0803 -79.9307 -78.1415 -66.5742 -41.3964 2.0000", newSignal.ToString(4));
            Assert.AreEqual(-1, newSignal.Start);
            Assert.AreEqual(2, newSignal.Finish);
            Assert.AreEqual(Convert.ToDecimal(4d / 7d / 7d), Convert.ToDecimal(newSignal.SamplingInterval));
            Assert.AreEqual(Convert.ToInt32(1 / (4d / 7d / 7d)), newSignal.SamplingRate);

            signal = new Signal(new double[] { -7, 4, 8, -2, 1, -55, 2 }) { Start = 1, Finish = 4, SamplingInterval = 4d / 7d };
            newSignal = WaveMath.InterpolateNewton(signal, 1);
            Assert.AreEqual("-7, 4, 8, -2, 1, -55, 2", newSignal.ToString(0, ", "));
            Assert.AreEqual(1, newSignal.Start);
            Assert.AreEqual(4, newSignal.Finish);
            Assert.AreEqual(4d / 7d, newSignal.SamplingInterval);
            Assert.AreEqual(Convert.ToInt32(1 / (4d / 7d)), newSignal.SamplingRate);
        }
    }
}
