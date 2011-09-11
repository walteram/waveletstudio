using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Functions
{
    [TestClass]
    public class WaveMathTests
    {
        [TestMethod]
        public void TestGetAccumulatedEnergy()
        {
            var input = new double[] {4, 2, 1, 5, 1, 5, 8, 9, 3};
            var expected = 226d;
            Assert.AreEqual(expected, WaveMath.GetAccumulatedEnergy(input));

            input = new double[] { };
            expected = 0d;
            Assert.AreEqual(expected, WaveMath.GetAccumulatedEnergy(input));
        }

        [TestMethod]
        public void TestMode()
        {
            var input = new[] { 1.1, 1.1, 2, 3, 1, 2.5, 1.1, 459, 3.5, 1.1 };
            var expected = 1.1;
            Assert.AreEqual(expected, WaveMath.Mode(input));

            input = new double[] { };
            expected = 0d;
            Assert.AreEqual(expected, WaveMath.Mode(input));

            input = new[] { 1.1, 1.1, 2.2, 2.2 };
            expected = 1.1;
            Assert.AreEqual(expected, WaveMath.Mode(input));

            input = new[] { 1.1, 1.1, 2.2, 2.2, 3.3, 3.3, 3.3 };
            expected = 3.3;
            Assert.AreEqual(expected, WaveMath.Mode(input));

            input = new[] { 1.1, 1.1, 2.2, 2.2, 3.3, 3.3, 3.3, 1.1, 1.1, 2.2, 2.2, 1.1, 1.1, 2.2, 2.2, 3.3, 3.3, 3.3, 3.3, 3.3, 3.3, 1.1, 1.1, 2.2, 1.1, 1.1, 2.2, 2.2, 3.3, 3.3, 3.3, 2.2, 3.3, 3.3, 3.3, 1.1, 1.1, 2.2, 2.2, 3.3, 3.3, 3.3 };
            expected = 3.3;
            Assert.AreEqual(expected, WaveMath.Mode(input));
        }

        [TestMethod]
        public void TestNormalDistribution()
        {
            var input = new double[] { 1, 2, 3, 4, 1, 0, 1, 2 };
            var expected = new[] { 0.24197072451914337, 0.3989422804014327, 0.24197072451914337, 0.053990966513188063, 0.24197072451914337, 0.053990966513188063, 0.24197072451914337, 0.3989422804014327 };
            var result = WaveMath.NormalDistribution(input, 2, 1);
            Assert.IsTrue(result.SequenceEqual(expected));

            input = new double[] { 0 };
            expected = new[] { 0.053990966513188063 };
            result = WaveMath.NormalDistribution(input, 2, 1);
            Assert.IsTrue(result.SequenceEqual(expected));

            input = new double[] { };
            expected = new double[] { };
            result = WaveMath.NormalDistribution(input, 2, 1);
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestNormalDensity()
        {
            var input = 5.1;
            var result = WaveMath.NormalDensity(input, 2, 1);
            Assert.AreEqual(0.0032668190561999247, result);

            input = 0;
            result = WaveMath.NormalDensity(input, 2, 0);
            Assert.IsTrue(double.IsNaN(result));

            input = -10;
            result = WaveMath.NormalDensity(input, 2, 0);
            Assert.IsTrue(double.IsNaN(result));
        }
        
        [TestMethod]
        public void TestConvolve()
        {
            var signal = new double[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var filter = new double[] { 1, 2, 3 };
            var convolved = WaveMath.Convolve(ConvolutionModeEnum.Normal, signal, filter);
            var expected = new double[] { 10, 16, 22, 28, 34, 40 };
            Assert.IsTrue(convolved.SequenceEqual(expected));

            signal = new double[] { 1, 2, 3 };
            filter = new double[] { 1, 2, 3, 4, 5 };
            convolved = WaveMath.ConvolveNormal(signal, filter);
            expected = new double[] { 10, 16, 22 };
            Assert.IsTrue(convolved.SequenceEqual(expected));

            signal = new double[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            filter = new double[] { 1, 2, 3, 4 };
            convolved = WaveMath.ConvolveNormal(signal, filter, false);
            expected = new double[] { 1, 4, 10, 20, 30, 40, 50, 60, 61, 52, 32 };
            Assert.IsTrue(convolved.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestConvolveManagedFft()
        {

            var signal = new double[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var filter = new double[] { 1, 2, 3 };
            var convolved = WaveMath.Convolve(ConvolutionModeEnum.ManagedFft, signal, filter);
            var expected = new double[] { 10, 16, 22, 28, 34, 40 };
            Assert.IsTrue(TestUtils.SequenceEquals(convolved, expected));

            signal = new double[] { 1, 2, 3 };
            filter = new double[] { 1, 2, 3, 4, 5 };
            convolved = WaveMath.ConvolveManagedFft(signal, filter);
            expected = new double[] { 10, 16, 22 };
            Assert.IsTrue(TestUtils.SequenceEquals(convolved, expected));

            signal = new double[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            filter = new double[] { 1, 2, 3, 4 };
            convolved = WaveMath.ConvolveManagedFft(signal, filter, false);
            expected = new double[] { 1, 4, 10, 20, 30, 40, 50, 60, 61, 52, 32 };
            Assert.IsTrue(TestUtils.SequenceEquals(convolved, expected));
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

            input = new double[] { };
            upSampled = WaveMath.UpSample(input);
            expected = new double[] { };
            Assert.IsTrue(upSampled.SequenceEqual(expected));
        }        
    }
}
