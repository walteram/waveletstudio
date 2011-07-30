using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WaveletStudio.Tests
{
    [TestClass]
    public class DecompositionLevelTests
    {
        [TestMethod]
        public void TestGetDisturbances()
        {
            var signal = new Signal(1, 1, 1, 1, 1, 1, 1, 0.5, 2, 1.5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 2, 1, 1, 1, 4, 5, 4, 5, 1);
            var levels = Dwt.ExecuteDwt(signal, CommonMotherWavelets.GetWaveletFromName("haar"), 1);
            var disturbances = levels[0].GetDisturbances(0.01, 2);

            Assert.AreEqual(3, disturbances.Count);
        }

        [TestMethod]
        public void TestGetNoDisturbances()
        {
            var signal = new Signal(0, 0, 0, 0, 0, 0, 0, 0);
            var levels = Dwt.ExecuteDwt(signal, CommonMotherWavelets.GetWaveletFromName("db4"), 1);
            var disturbances = levels[0].GetDisturbances(0.01, 2);

            Assert.AreEqual(0, disturbances.Count);
        }

        [TestMethod]
        public void TestGetDisturbances2Levels()
        {
            var signal = new Signal(-10, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 5, 1, 1, 1, 1, 1, 1, 6, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1.1, -0.5, 2, -1.5, 1, 1, 1, 1, 1, 1, 1, 1, 1.5, 1.9, 0.2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 4, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 20, 10, -11, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 8, 2, 2, 2, -12, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 5, -1, 2, 3, 11, 12, -4, -1, -1, 1, 3, 2, 8, 7, 4, 6, 6, 7);
            var levels = Dwt.ExecuteDwt(signal, CommonMotherWavelets.GetWaveletFromName("haar"), 2);
            
            var disturbances = levels[0].GetDisturbances(0.05);
            Assert.AreEqual(10, disturbances.Count);

            disturbances = levels[1].GetDisturbances(0.05);
            Assert.AreEqual(7, disturbances.Count);
        }

        [TestMethod]
        public void TestGetDisturbances4Levels()
        {
            var signal = new Signal(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0.5, 2, 1.5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 2, 1, 1, 1, 4, 5, 4, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            var levels = Dwt.ExecuteDwt(signal, CommonMotherWavelets.GetWaveletFromName("haar"), 4);
            
            var disturbances = levels[3].GetDisturbances();
            Assert.AreEqual(1, disturbances.Count);
        }

        [TestMethod]
        public void TestGetDisturbancesSag()
        {
            var samples = ReadFile("sag.csv");
            var signal = new Signal(samples);
            var levels = Dwt.ExecuteDwt(signal, CommonMotherWavelets.GetWaveletFromName("db10"), 2);

            var disturbances = levels[1].GetDisturbances(0.001, 10);
            Assert.AreEqual(4, disturbances.Count);            
        }
        
        private double[] ReadFile(string filename)
        {
            return File.ReadAllText(@"C:\Wavelet\WaveletStudio\trunk\res\testdata\" + filename).Split(',').Select(it => double.Parse(it.Replace(".", ","))).ToArray();
        }
    }
}
