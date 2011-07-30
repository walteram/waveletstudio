using System;
using System.Linq;
using ILNumerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WaveletStudio.Tests
{
    [TestClass]
    public class WaveMathTests
    {
        [TestMethod]
        public void TestGetAccumulatedEnergy()
        {
            var input = new ILArray<double>(new double[] {4, 2, 1, 5, 1, 5, 8, 9, 3});
            var expected = 226d;
            Assert.AreEqual(expected, WaveMath.GetAccumulatedEnergy(input));

            input = new ILArray<double>(new double[] { });
            expected = 0d;
            Assert.AreEqual(expected, WaveMath.GetAccumulatedEnergy(input));
        }

        [TestMethod]
        public void TestMode()
        {
            var input = new ILArray<double>(new[] { 1.1, 1.1, 2, 3, 1, 2.5, 1.1, 459, 3.5, 1.1 });
            var expected = 1.1;
            Assert.AreEqual(expected, WaveMath.Mode(input));

            input = new ILArray<double>(new double[] { });
            expected = 0d;
            Assert.AreEqual(expected, WaveMath.Mode(input));

            input = new ILArray<double>(new[] { 1.1, 1.1, 2.2, 2.2 });
            expected = 1.1;
            Assert.AreEqual(expected, WaveMath.Mode(input));

            input = new ILArray<double>(new[] { 1.1, 1.1, 2.2, 2.2, 3.3, 3.3, 3.3 });
            expected = 3.3;
            Assert.AreEqual(expected, WaveMath.Mode(input));

            input = new ILArray<double>(new[] { 1.1, 1.1, 2.2, 2.2, 3.3, 3.3, 3.3, 1.1, 1.1, 2.2, 2.2, 1.1, 1.1, 2.2, 2.2, 3.3, 3.3, 3.3, 3.3, 3.3, 3.3, 1.1, 1.1, 2.2, 1.1, 1.1, 2.2, 2.2, 3.3, 3.3, 3.3, 2.2, 3.3, 3.3, 3.3, 1.1, 1.1, 2.2, 2.2, 3.3, 3.3, 3.3 });
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
    }
}
