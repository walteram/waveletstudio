using System.Linq;
using ILNumerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WaveLib.Tests
{
    [TestClass]
    public class MathUtilsTests
    {

        [TestMethod]
        public void TestConvolve()
        {
            var signal = new ILArray<double>(new double[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            var filter = new ILArray<double>(new double[] { 1, 2, 3 });
            var convolved = MathUtils.Convolve(signal, filter);
            var expected = new ILArray<double>(new double[] { 10, 16, 22, 28, 34, 40 });
            Assert.IsTrue(convolved.SequenceEqual(expected));

            signal = new ILArray<double>(new double[] { 1, 2, 3 });
            filter = new ILArray<double>(new double[] { 1, 2, 3, 4, 5 });
            convolved = MathUtils.Convolve(signal, filter);
            expected = new ILArray<double>(new double[] { 10, 16, 22 });
            Assert.IsTrue(convolved.SequenceEqual(expected));

            convolved = MathUtils.Convolve(signal, filter, false);
            expected = new ILArray<double>(new double[] { 1, 4, 10, 16, 22, 22, 15 });
            Assert.IsTrue(convolved.SequenceEqual(expected));

            convolved = MathUtils.Convolve(signal, filter, true, 1);
            expected = new ILArray<double>(new double[] { 16 });
            Assert.IsTrue(convolved.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestDownsample()
        {
            var input = new ILArray<double>(new double[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            var downSampled = MathUtils.DownSample(input);
            var expected = new ILArray<double>(new double[] { 2, 4, 6, 8 });
            Assert.IsTrue(downSampled.SequenceEqual(expected));

            input = new ILArray<double>(new double[] { 1, 2, 3 });
            downSampled = MathUtils.DownSample(input);
            expected = new ILArray<double>(new double[] { 2 });
            Assert.IsTrue(downSampled.SequenceEqual(expected));

            input = new ILArray<double>(new double[] { 1 });
            downSampled = MathUtils.DownSample(input);
            expected = new ILArray<double>(new double[] { });
            Assert.IsTrue(downSampled.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestUpsample()
        {
            var input = new ILArray<double>(new double[] { 1 });
            var upSampled = MathUtils.UpSample(input);
            var expected = new ILArray<double>(new double[] { 1 });
            Assert.IsTrue(upSampled.SequenceEqual(expected));

            input = new ILArray<double>(new double[] { 1, 2 });
            upSampled = MathUtils.UpSample(input);
            expected = new ILArray<double>(new double[] { 1, 0, 2 });
            Assert.IsTrue(upSampled.SequenceEqual(expected));

            input = new ILArray<double>(new double[] { 1, 2, 3, 4, 5 });
            upSampled = MathUtils.UpSample(input);
            expected = new ILArray<double>(new double[] { 1, 0, 2, 0, 3, 0, 4, 0, 5 });
            Assert.IsTrue(upSampled.SequenceEqual(expected));

            input = new ILArray<double>(new double[] { });
            upSampled = MathUtils.UpSample(input);
            expected = new ILArray<double>(new double[] { });
            Assert.IsTrue(upSampled.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestCalculateEnergy()
        {
            var input = new ILArray<double>(new double[] {4, 8, 15, 23, 42, 8});
            Assert.AreEqual(2662, MathUtils.CalculateEnergy(input));
        }
    }
}
