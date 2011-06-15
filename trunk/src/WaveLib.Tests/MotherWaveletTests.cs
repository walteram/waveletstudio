using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WaveLib.Tests
{
    [TestClass]
    public class MotherWaveletTests
    {
        [TestMethod]
        public void TestGetFilters()
        {
            var motherWavelet = new MotherWavelet(new[] {0.341506350946110, 0.591506350946110, 0.158493649053890, -0.091506350946110});
            var filters = motherWavelet.Filters;

            Assert.IsTrue(filters.DecompositionHighPassFilter.SequenceEqual(new[] { -0.48296291314453454, 0.83651630373780839, -0.22414386804201286, -0.12940952255126084 }));
            Assert.IsTrue(filters.DecompositionLowPassFilter.SequenceEqual(new[] { -0.12940952255126084, 0.22414386804201286, 0.83651630373780839, 0.48296291314453454 }));
            Assert.IsTrue(filters.ReconstructionHighPassFilter.SequenceEqual(new[] { -0.12940952255126084, -0.22414386804201286, 0.83651630373780839, -0.48296291314453454 }));
            Assert.IsTrue(filters.ReconstructionLowPassFilter.SequenceEqual(new[] { 0.48296291314453454, 0.83651630373780839, 0.22414386804201286, -0.12940952255126084 }));            
        }

        [TestMethod]
        public void TestLoadFromName()
        {
            var db4 = MotherWavelet.LoadFromName("db4");
            Assert.AreEqual("db4", db4.Name);
            Assert.IsTrue(db4.ScalingFilter.SequenceEqual(new[] { 0.16290171, 0.50547286, 0.44610007, -0.019787513, -0.13225358, 0.02180815, 0.023251801, -0.0074934947 }));
        }
    }
}
 