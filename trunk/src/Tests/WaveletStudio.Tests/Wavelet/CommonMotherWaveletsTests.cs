using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Wavelet;

namespace WaveletStudio.Tests.Wavelet
{
    [TestClass]
    public class CommonMotherWaveletsTests
    {
        [TestMethod]
        public void TestGetWaveletFromName()
        {
            Assert.AreEqual("db4", CommonMotherWavelets.GetWaveletFromName("DB4").Name);
            Assert.AreEqual("db4", CommonMotherWavelets.GetWaveletFromName("Daub4").Name);
            Assert.AreEqual("db4", CommonMotherWavelets.GetWaveletFromName("D4").Name);
            Assert.IsNull(CommonMotherWavelets.GetWaveletFromName("abcd"));
        }
    }
}
