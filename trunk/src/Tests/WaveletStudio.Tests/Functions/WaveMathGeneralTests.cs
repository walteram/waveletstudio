using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Functions
{
    [TestClass]
    public class WaveMathGeneralTests
    {
        [TestMethod]
        public void TestAbs()
        {
            var input = new [] {2.1, 3.2, -1, -1.3, -100, 145, -2};
            var expected = new [] {2.1, 3.2, 1, 1.3, 100, 145, 2};
            Assert.IsTrue(TestUtils.SequenceEquals(expected, WaveMath.Abs(input)));

            var signal = new Signal(input);
            WaveMath.Abs(ref signal, input);
            Assert.IsTrue(TestUtils.SequenceEquals(expected, signal.Samples));

            input = new double[] { };
            expected = new double[] { };
            Assert.IsTrue(TestUtils.SequenceEquals(expected, WaveMath.Abs(input)));            
        }

        [TestMethod]
        public void TestAbsFromComplex()
        {
            var input = new[] { 2.1, 3.2, 1, -1.3, -100, 145, -2 };
            var expected = new[] { 3.8275318418009276, 1.6401219466856727, 176.13914953808538, 2 };
            Assert.IsTrue(TestUtils.SequenceEquals(expected, WaveMath.AbsFromComplex(input)));

            var signal = new Signal(input) { IsComplex = true };
            WaveMath.Abs(ref signal, input);
            Assert.IsTrue(TestUtils.SequenceEquals(expected, signal.Samples));

            input = new double[] { };
            expected = new double[] { };
            Assert.IsTrue(TestUtils.SequenceEquals(expected, WaveMath.AbsFromComplex(input)));            
        }

        [TestMethod]
        public void TestDownsample()
        {
            var input = new double[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var downSampled = WaveMath.DownSample(input);
            var expected = new double[] { 2, 4, 6, 8 };
            Assert.IsTrue(downSampled.SequenceEqual(expected));

            input = new double[] { 1, 2, 3 };
            downSampled = WaveMath.DownSample(input);
            expected = new double[] { 2 };
            Assert.IsTrue(downSampled.SequenceEqual(expected));

            input = new double[] { 1 };
            downSampled = WaveMath.DownSample(input);
            expected = new double[] { };
            Assert.IsTrue(downSampled.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestUpsample()
        {
            var input = new double[] { 1 };
            var upSampled = WaveMath.UpSample(input);
            var expected = new double[] { 1 };
            Assert.IsTrue(upSampled.SequenceEqual(expected));

            input = new double[] { 1, 2 };
            upSampled = WaveMath.UpSample(input);
            expected = new double[] { 1, 0, 2 };
            Assert.IsTrue(upSampled.SequenceEqual(expected));

            input = new double[] { 1, 2, 3, 4, 5 };
            upSampled = WaveMath.UpSample(input);
            expected = new double[] { 1, 0, 2, 0, 3, 0, 4, 0, 5 };
            Assert.IsTrue(upSampled.SequenceEqual(expected));

            input = new double[] { 1, 2, 3, 4 };
            upSampled = WaveMath.UpSample(input, 3);
            expected = new double[] { 1, 0, 0, 2, 0, 0, 3, 0, 0, 4 };
            Assert.IsTrue(upSampled.SequenceEqual(expected));

            input = new double[] { 1, 2, 3, 4 };
            upSampled = WaveMath.UpSample(input, 4, false);
            expected = new double[] { 1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 4, 0, 0, 0 };
            Assert.IsTrue(upSampled.SequenceEqual(expected));

            input = new double[] { };
            upSampled = WaveMath.UpSample(input);
            expected = new double[] { };
            Assert.IsTrue(upSampled.SequenceEqual(expected));

            upSampled = WaveMath.UpSample(null);
            expected = new double[] { };
            Assert.IsTrue(upSampled.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestShift()
        {
            var signal = new Signal {Start = 3, Finish = 10};
            WaveMath.Shift(ref signal, -2.1);

            Assert.AreEqual(0.9d, signal.Start);
            Assert.AreEqual(7.9d, signal.Finish);
        }

        [TestMethod]
        public void TestInvert()
        {
            var samples = new double[]{-1, 2, 3, -4};
            var output = WaveMath.Invert(samples);
            Assert.IsTrue(output.SequenceEqual(new double[]{ -4, 3, 2, -1 }));

            samples = new double[] { };
            output = WaveMath.Invert(samples);
            Assert.IsTrue(output.SequenceEqual(new double[] { }));

            output = WaveMath.Invert(null);
            Assert.IsNull(output);
        }

        [TestMethod]
        public void TestRepeat()
        {
            var samples = new double[] { -1, 2, 3, -4 };
            var output = WaveMath.Repeat(samples, 1, 1);
            Assert.IsTrue(output.SequenceEqual(new double[] { -1, -1, 2, 2, 3, 3, -4, -4 }));

            output = WaveMath.Repeat(samples, 1, 2);
            Assert.IsTrue(output.SequenceEqual(new double[] { -1, -1, -1, 2, 2, 2, 3, 3, 3, -4, -4, -4 }));

            output = WaveMath.Repeat(samples, 1, 3);
            Assert.IsTrue(output.SequenceEqual(new double[] { -1, -1, -1, -1, 2, 2, 2, 2, 3, 3, 3, 3, -4, -4, -4, -4 }));

            output = WaveMath.Repeat(samples, 1, 4);
            Assert.IsTrue(output.SequenceEqual(new double[] { -1, -1, -1, -1, -1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, -4, -4, -4, -4, -4 }));


            output = WaveMath.Repeat(samples, 2, 1);
            Assert.IsTrue(output.SequenceEqual(new double[] { -1, 2, -1, 2, 3, -4, 3, -4 }));

            output = WaveMath.Repeat(samples, 2, 2);
            Assert.IsTrue(output.SequenceEqual(new double[] { -1, 2, -1, 2, -1, 2, 3, -4, 3, -4, 3, -4 }));

            output = WaveMath.Repeat(samples, 2, 3);
            Assert.IsTrue(output.SequenceEqual(new double[] { -1, 2, -1, 2, -1, 2, -1, 2, 3, -4, 3, -4, 3, -4, 3, -4 }));

            output = WaveMath.Repeat(samples, 3, 4);
            Assert.IsTrue(output.SequenceEqual(new double[] { -1, 2, 3, -1, 2, 3, -1, 2, 3, -1, 2, 3, -1, 2, 3, -4, -4, -4, -4, -4}));

            output = WaveMath.Repeat(samples, 0, 1);
            Assert.IsTrue(output.SequenceEqual(new double[] { -1, 2, 3, -4 }));

            output = WaveMath.Repeat(samples, 1, 0);
            Assert.IsTrue(output.SequenceEqual(new double[] { -1, 2, 3, -4 }));

            samples = new double[] { };
            output = WaveMath.Repeat(samples, 2, 2);
            Assert.IsTrue(output.SequenceEqual(new double[] { }));

            samples = DateTime.Now.Second >= 0 ? null : new double[0];
            output = WaveMath.Repeat(samples, 2, 2);
            Assert.IsNull(output);
        }

        [TestMethod]
        public void TestRepeatSignal()
        {
            var samples = new Signal(-1, 2, 3, -4) {Start = -1, Finish = 0.5, SamplingInterval = 0.5};
            var output = WaveMath.Repeat(samples, 1, 1, true);
            Assert.IsTrue(output.Samples.SequenceEqual(new double[] {-1, -1, 2, 2, 3, 3, -4, -4}));
            Assert.AreEqual(0.5, output.SamplingInterval);
            Assert.AreEqual(-1, output.Start);
            Assert.AreEqual(2.5, output.Finish);

            output = WaveMath.Repeat(samples, 1, 1, false);
            Assert.IsTrue(output.Samples.SequenceEqual(new double[] { -1, -1, 2, 2, 3, 3, -4, -4 }));
            Assert.AreEqual(0.1875, output.SamplingInterval);
            Assert.AreEqual(-1, output.Start);
            Assert.AreEqual(0.5, output.Finish);
        }

        [TestMethod]
        public void TestLimitRange()
        {
            Assert.AreEqual(10, WaveMath.LimitRange(5, 10, 20));
            Assert.AreEqual(200, WaveMath.LimitRange(500, 100, 200));
            Assert.AreEqual(3000, WaveMath.LimitRange(3000, 1000, 3500));
            Assert.AreEqual(4000, WaveMath.LimitRange(4000, 4000, 4001));
            Assert.AreEqual(5000, WaveMath.LimitRange(5000, 4000, 5000));
            Assert.AreEqual(6000, WaveMath.LimitRange(6000, 6000, 6000));
        }
    }
}
