using ILNumerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WaveletStudio.WaveLib.Tests
{
    [TestClass]
    public class WaveLibMath
    {
        [TestMethod]
        public void TestGetAccumulatedEnergy()
        {
            var input = new ILArray<double>(new double[] {4, 2, 1, 5, 1, 5, 8, 9, 3});
            var expected = 226d;
            Assert.AreEqual(expected, WaveLib.WaveLibMath.GetAccumulatedEnergy(input));

            input = new ILArray<double>(new double[] { });
            expected = 0d;
            Assert.AreEqual(expected, WaveLib.WaveLibMath.GetAccumulatedEnergy(input));
        }

        [TestMethod]
        public void TestMode()
        {
            var input = new ILArray<double>(new[] { 1.1, 1.1, 2, 3, 1, 2.5, 1.1, 459, 3.5, 1.1 });
            var expected = 1.1;
            Assert.AreEqual(expected, WaveLib.WaveLibMath.Mode(input));

            input = new ILArray<double>(new double[] { });
            expected = 0d;
            Assert.AreEqual(expected, WaveLib.WaveLibMath.Mode(input));

            input = new ILArray<double>(new[] { 1.1, 1.1, 2.2, 2.2 });
            expected = 1.1;
            Assert.AreEqual(expected, WaveLib.WaveLibMath.Mode(input));

            input = new ILArray<double>(new[] { 1.1, 1.1, 2.2, 2.2, 3.3, 3.3, 3.3 });
            expected = 3.3;
            Assert.AreEqual(expected, WaveLib.WaveLibMath.Mode(input));

            input = new ILArray<double>(new[] { 1.1, 1.1, 2.2, 2.2, 3.3, 3.3, 3.3, 1.1, 1.1, 2.2, 2.2, 1.1, 1.1, 2.2, 2.2, 3.3, 3.3, 3.3, 3.3, 3.3, 3.3, 1.1, 1.1, 2.2, 1.1, 1.1, 2.2, 2.2, 3.3, 3.3, 3.3, 2.2, 3.3, 3.3, 3.3, 1.1, 1.1, 2.2, 2.2, 3.3, 3.3, 3.3 });
            expected = 3.3;
            Assert.AreEqual(expected, WaveLib.WaveLibMath.Mode(input));
        }
    }
}
