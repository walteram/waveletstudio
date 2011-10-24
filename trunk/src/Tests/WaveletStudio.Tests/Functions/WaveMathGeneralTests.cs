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
    }
}
