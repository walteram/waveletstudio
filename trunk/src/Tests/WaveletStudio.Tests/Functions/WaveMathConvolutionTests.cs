using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Functions
{
    [TestClass]
    public class WaveMathConvolutionTests
    {
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
        public void TestConvolveManagedFFT()
        {

            var signal = new double[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var filter = new double[] { 1, 2, 3 };
            var convolved = WaveMath.Convolve(ConvolutionModeEnum.ManagedFFT, signal, filter);
            var expected = new double[] { 10, 16, 22, 28, 34, 40 };
            Assert.IsTrue(TestUtils.SequenceEquals(convolved, expected));

            signal = new double[] { 1, 2, 3 };
            filter = new double[] { 1, 2, 3, 4, 5 };
            convolved = WaveMath.ConvolveManagedFFT(signal, filter);
            expected = new double[] { 10, 16, 22 };
            Assert.IsTrue(TestUtils.SequenceEquals(convolved, expected));

            signal = new double[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            filter = new double[] { 1, 2, 3, 4 };
            convolved = WaveMath.ConvolveManagedFFT(signal, filter, false);
            expected = new double[] { 1, 4, 10, 20, 30, 40, 50, 60, 61, 52, 32 };
            Assert.IsTrue(TestUtils.SequenceEquals(convolved, expected));

            Assert.IsNull(WaveMath.ConvolveManagedFFT(null, null, false));
        }
    }
}
