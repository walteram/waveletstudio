﻿using System.Linq;
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

            Assert.IsTrue(filters.DecompositionHighPassFilter.SequenceEqual(new[] { -0.48296291314453427, 0.83651630373780783, -0.22414386804201272, -0.12940952255126076 }));
            Assert.IsTrue(filters.DecompositionLowPassFilter.SequenceEqual(new[] { -0.12940952255126076, 0.22414386804201272, 0.83651630373780783, 0.48296291314453427 }));
            Assert.IsTrue(filters.ReconstructionHighPassFilter.SequenceEqual(new[] { -0.12940952255126076, -0.22414386804201272, 0.83651630373780783, -0.48296291314453427 }));
            Assert.IsTrue(filters.ReconstructionLowPassFilter.SequenceEqual(new[] { 0.48296291314453427, 0.83651630373780783, 0.22414386804201272, -0.12940952255126076 }));            
        }

        [TestMethod]
        public void TestLoadFromName()
        {
            var db4 = MotherWavelet.LoadFromName("db4");
            Assert.AreEqual("db4", db4.Name);
            Assert.IsTrue(db4.ScalingFilter.SequenceEqual(new[] { 0.162901714025618, 0.505472857545651, 0.446100069123194, -0.019787513117909, -0.132253583684369, 0.021808150237385, 0.023251800535557, -0.007493494665127 }));
        }
    }
}
 