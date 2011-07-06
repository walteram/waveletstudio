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
    }
}
