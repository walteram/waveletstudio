using System.IO;
using System.Linq;
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
            var signal = new Signal(1, 1, 1, 1, 1, 1, 1, 0.5, 2, 1.5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 2, 1, 1, 1, 4, 5, 4, 2, 1, 1);
            var levels = Dwt.ExecuteDwt(signal, CommonMotherWavelets.GetWaveletFromName("haar"), 1);
            var disturbances = levels[0].GetDisturbances();

            Assert.AreEqual(3, disturbances.Count);
            Assert.AreEqual(3, disturbances[0].Start);
            Assert.AreEqual(4, disturbances[0].Finish);
            Assert.AreEqual(2, disturbances[0].Length);

            Assert.AreEqual(10, disturbances[1].Start);
            Assert.AreEqual(11, disturbances[1].Finish);
            Assert.AreEqual(2, disturbances[1].Length);

            Assert.AreEqual(13, disturbances[2].Start);
            Assert.AreEqual(15, disturbances[2].Finish);
            Assert.AreEqual(3, disturbances[2].Length);

            Assert.AreEqual(27, disturbances[2].SignalStart);
            Assert.AreEqual(30, disturbances[2].SignalFinish);
            Assert.AreEqual(4, disturbances[2].SignalLength);
        }

        [TestMethod]
        public void TestGetDisturbances2Levels()
        {
            var signal = new Signal(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0.5, 2, 1.5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 2, 1, 1, 1, 4, 5, 4, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            var levels = Dwt.ExecuteDwt(signal, CommonMotherWavelets.GetWaveletFromName("haar"), 2);
            var disturbances = levels[0].GetDisturbances();
            Assert.AreEqual(3, disturbances.Count);
            Assert.AreEqual(55, disturbances[2].SignalStart);
            Assert.AreEqual(58, disturbances[2].SignalFinish);
            Assert.AreEqual(4, disturbances[2].SignalLength);

            disturbances = levels[1].GetDisturbances();
            Assert.AreEqual(2, disturbances.Count);
            Assert.AreEqual(49, disturbances[1].SignalStart);
            Assert.AreEqual(58, disturbances[1].SignalFinish);
            Assert.AreEqual(10, disturbances[1].SignalLength);
        }

        [TestMethod]
        public void TestGetDisturbances4Levels()
        {
            var signal = new Signal(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0.5, 2, 1.5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 2, 1, 1, 1, 4, 5, 4, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            var levels = Dwt.ExecuteDwt(signal, CommonMotherWavelets.GetWaveletFromName("haar"), 4);
            
            var disturbances = levels[3].GetDisturbances();
            Assert.AreEqual(1, disturbances.Count);
            Assert.AreEqual(17, disturbances[0].SignalStart);
            Assert.AreEqual(62, disturbances[0].SignalFinish);
            Assert.AreEqual(46, disturbances[0].SignalLength);
        }

        [TestMethod]
        public void TestGetDisturbancesSag()
        {
            var samples = ReadFile("sag.csv");
            var signal = new Signal(samples);
            var levels = Dwt.ExecuteDwt(signal, CommonMotherWavelets.GetWaveletFromName("db10"), 1);

            var disturbances = levels[0].GetDisturbances(0.001);
            Assert.AreEqual(1, disturbances.Count);
            Assert.AreEqual(17, disturbances[0].SignalStart);
            Assert.AreEqual(62, disturbances[0].SignalFinish);
            Assert.AreEqual(46, disturbances[0].SignalLength);
        }

        private double[] ReadFile(string filename)
        {
            return File.ReadAllText(@"C:\Wavelet\WaveletStudio\trunk\res\testdata\" + filename).Split(',').Select(it => double.Parse(it.Replace(".", ","))).ToArray();
        }
    }
}
