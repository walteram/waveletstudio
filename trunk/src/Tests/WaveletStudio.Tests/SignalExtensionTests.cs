using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WaveletStudio.Tests
{
    [TestClass]
    public class SignalExtensionTests
    {
        [TestMethod]
        public void TestDeextend()
        {
            var points = new double[] { 2, 6, 1, 6, 5, 0, 4, 5, 3, 4 };
            var extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.SymmetricHalfPoint, 100);
            var deextended = SignalExtension.Deextend(extended, points.Length);
            Assert.IsTrue(deextended.SequenceEqual(points));

            points = new double[] { 1, 2 };
            deextended = SignalExtension.Deextend(points, 1);
            Assert.IsTrue(deextended.SequenceEqual(points));
        }

        [TestMethod]
        public void TestExtendSymmetricHalfPoint()
        {
            var points = new double[] { 2, 6, 1, 6, 5, 0, 4, 5, 3, 4 };
            var extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.SymmetricHalfPoint,3);
            var expected = new double[] { 1, 6, 2, 2, 6, 1, 6, 5, 0, 4, 5, 3, 4, 4, 3, 5 };
            Assert.IsTrue(extended.SequenceEqual(expected));

            points = new double[] { 2, 6, 1 };
            extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.SymmetricHalfPoint, 1);
            expected = new double[] { 2, 2, 6, 1, 1 };
            Assert.IsTrue(extended.SequenceEqual(expected));

            points = new double[] { 2, 6, 1, 6, 5, 0 };
            extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.SymmetricHalfPoint, 1);
            expected = new double[] { 2, 2, 6, 1, 6, 5, 0, 0 };
            Assert.IsTrue(extended.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestExtendSymmetricWholePoint()
        {
            var points = new double[] { 2, 6, 1, 6, 5, 0, 4, 5, 3, 4 };
            var extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.SymmetricWholePoint, 3);
            var expected = new double[] { 6, 1, 6, 2, 6, 1, 6, 5, 0, 4, 5, 3, 4, 3, 5, 4 };
            Assert.IsTrue(extended.SequenceEqual(expected));

            points = new double[] { 2, 6, 1 };
            extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.SymmetricWholePoint, 1);
            expected = new double[] { 6, 2, 6, 1, 6 };
            Assert.IsTrue(extended.SequenceEqual(expected));

            points = new double[] { 2, 6, 1, 6, 5, 0 };
            extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.SymmetricWholePoint, 1);
            expected = new double[] { 6, 2, 6, 1, 6, 5, 0, 5 };
            Assert.IsTrue(extended.SequenceEqual(expected));

            points = new double[] { 2, 6, 1, 6, 5, 0 };
            SignalExtension.Extend(points, SignalExtension.ExtensionMode.SymmetricWholePoint, 8);
        }

        [TestMethod]
        public void TestExtendAntisymmetricHalfPoint()
        {
            var points = new double[] { 2, 6, 1, 6, 5, 0, 4, 5, 3, 4 };
            var extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.AntisymmetricHalfPoint, 3);
            var expected = new double[] { -1, -6, -2, 2, 6, 1, 6, 5, 0, 4, 5, 3, 4, -4, -3, -5 };
            Assert.IsTrue(extended.SequenceEqual(expected));

            points = new double[] { 2, 6, 1 };
            extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.AntisymmetricHalfPoint, 1);
            expected = new double[] { -2, 2, 6, 1, -1 };
            Assert.IsTrue(extended.SequenceEqual(expected));
        }
        
        [TestMethod]
        public void TestExtendAntisymmetricWholePoint()
        {
            var points = new double[] { 2, 6, 1, 6, 5, 4, 5, 3, 4 };
            var extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.AntisymmetricWholePoint, 3);
            var expected = new double[] {-2, 3, -2, 2, 6, 1, 6, 5, 4, 5, 3, 4, 5, 3, 4};
            Assert.IsTrue(extended.SequenceEqual(expected));

            points = new double[] { 2, 6, 1 };
            extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.AntisymmetricWholePoint, 1);
            expected = new double[] { -2, 2, 6, 1, -4 };
            Assert.IsTrue(extended.SequenceEqual(expected));

            points = new double[] { 2 };
            extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.AntisymmetricWholePoint, 1);
            expected = new double[] { -2, 2, -2 };
            Assert.IsTrue(extended.SequenceEqual(expected));

            points = new double[] { };
            extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.AntisymmetricWholePoint, 1);
            expected = new double[] { 0, 0 };
            Assert.IsTrue(extended.SequenceEqual(expected));

            points = new double[] { 2, 6, 1, 6, 5, 0 };
            SignalExtension.Extend(points, SignalExtension.ExtensionMode.AntisymmetricWholePoint, 8);
        }

        [TestMethod]
        public void TestExtendPeriodicPadding()
        {
            var points = new double[] { 2, 6, 1, 6, 5, 0, 4, 5, 3, 4 };
            var extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.PeriodicPadding, 3);
            var expected = new double[] { 5, 3, 4, 2, 6, 1, 6, 5, 0, 4, 5, 3, 4, 2, 6, 1 };
            Assert.IsTrue(extended.SequenceEqual(expected));

            points = new double[] { 2, 6, 1 };
            extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.PeriodicPadding, 1);
            expected = new double[] { 1, 2, 6, 1, 2 };
            Assert.IsTrue(extended.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestExtendZeroPadding()
        {
            var points = new double[] { 2, 6, 1, 6, 5, 0, 4, 5, 3, 4 };
            var extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.ZeroPadding, 3);
            var expected = new double[] { 0, 0, 0, 2, 6, 1, 6, 5, 0, 4, 5, 3, 4, 0, 0, 0 };
            Assert.IsTrue(extended.SequenceEqual(expected));

            points = new double[] { 2, 6, 1 };
            extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.ZeroPadding, 1);
            expected = new double[] { 0, 2, 6, 1, 0 };
            Assert.IsTrue(extended.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestExtendSmoothPadding0()
        {
            var points = new double[] { 2, 6, 1, 6, 5, 0, 4, 5, 3, 4 };
            var extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.SmoothPadding0, 3);
            var expected = new double[] { 2, 2, 2, 2, 6, 1, 6, 5, 0, 4, 5, 3, 4, 4, 4, 4 };
            Assert.IsTrue(extended.SequenceEqual(expected));

            points = new double[] { 2, 6, 1 };
            extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.SmoothPadding0, 1);
            expected = new double[] { 2, 2, 6, 1, 1 };
            Assert.IsTrue(extended.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestExtendSmoothPadding1()
        {
            var points = new double[] { 2, 6, 1, 6, 5, 0, 4, 5, 3, 4 };
            var extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.SmoothPadding1, 3);
            var expected = new double[] { -10, -6, -2, 2, 6, 1, 6, 5, 0, 4, 5, 3, 4, 5, 6, 7 };
            Assert.IsTrue(extended.SequenceEqual(expected));

            points = new double[] { 7, 6, 1, 6, 5, 0, 4, 5, 3, 1 };
            extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.SmoothPadding1, 3);
            expected = new double[] { 10, 9, 8, 7, 6, 1, 6, 5, 0, 4, 5, 3, 1, -1, -3, -5 };
            Assert.IsTrue(extended.SequenceEqual(expected));

            points = new double[] { 2, 6, 1 };
            extended = SignalExtension.Extend(points, SignalExtension.ExtensionMode.SmoothPadding1, 1);
            expected = new double[] { -2, 2, 6, 1, -4 };
            Assert.IsTrue(extended.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestNextPowerOf2()
        {
            Assert.AreEqual(1024, SignalExtension.NextPowerOf2(1000));
            Assert.AreEqual(2048, SignalExtension.NextPowerOf2(1024));
            Assert.AreEqual(1, SignalExtension.NextPowerOf2(0));
            Assert.AreEqual(0, SignalExtension.NextPowerOf2(-100));
        }
    }
}
