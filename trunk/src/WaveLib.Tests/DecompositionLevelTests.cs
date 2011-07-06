using ILNumerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WaveletStudio.WaveLib.Tests
{
    [TestClass]
    public class DecompositionLevelTests
    {
        [TestMethod]
        public void TestGetDisturbances()
        {
            var decompositionLevel = new DecompositionLevel { Details = new ILArray<double>(new[] { 3, 0, 0, 0, 0, 0, 0.35, -0.35, 0, 0, 0, 0, 0, 0.1, 0, 0, 0, 0, 0, 0.1, 0.2, -0.1, 0, 0, 0, 0, 2 }) };
            var disturbances = decompositionLevel.GetDisturbances();

            Assert.AreEqual(5, disturbances.Count);
            Assert.AreEqual(0, disturbances[0].Start);
            Assert.AreEqual(0, disturbances[0].Finish);
            Assert.AreEqual(6, disturbances[1].Start);
            Assert.AreEqual(7, disturbances[1].Finish);
            Assert.AreEqual(13, disturbances[2].Start);
            Assert.AreEqual(13, disturbances[2].Finish);
            Assert.AreEqual(19, disturbances[3].Start);
            Assert.AreEqual(21, disturbances[3].Finish);
            Assert.AreEqual(3, disturbances[3].Length);
            Assert.AreEqual(26, disturbances[4].Start);
            Assert.AreEqual(26, disturbances[4].Finish);
        }
    }
}
