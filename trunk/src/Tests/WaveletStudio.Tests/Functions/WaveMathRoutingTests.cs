using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Functions
{
    [TestClass]
    public class WaveMathRoutingTests
    {
        [TestMethod]
        public void TestSwitch()
        {
            Assert.AreEqual(20, WaveMath.Switch(10, 20, 12, WaveMath.SwitchCriteriaEnum.BIsGreaterThanThreshold));
            Assert.AreEqual(10, WaveMath.Switch(10, 20, 30, WaveMath.SwitchCriteriaEnum.BIsGreaterThanThreshold));

            Assert.AreEqual(20, WaveMath.Switch(10, 20, 30, WaveMath.SwitchCriteriaEnum.BIsLessThanThreshold));
            Assert.AreEqual(10, WaveMath.Switch(10, 20, 12, WaveMath.SwitchCriteriaEnum.BIsLessThanThreshold));

            Assert.AreEqual(20, WaveMath.Switch(10, 20, 12, WaveMath.SwitchCriteriaEnum.BIsGreaterOrEqualsThanThreshold));
            Assert.AreEqual(20, WaveMath.Switch(10, 20, 20, WaveMath.SwitchCriteriaEnum.BIsGreaterOrEqualsThanThreshold));
            Assert.AreEqual(10, WaveMath.Switch(10, 20, 30, WaveMath.SwitchCriteriaEnum.BIsGreaterOrEqualsThanThreshold));

            Assert.AreEqual(20, WaveMath.Switch(10, 20, 30, WaveMath.SwitchCriteriaEnum.BIsLessOrEqualsThanThreshold));
            Assert.AreEqual(20, WaveMath.Switch(10, 20, 20, WaveMath.SwitchCriteriaEnum.BIsLessOrEqualsThanThreshold));
            Assert.AreEqual(10, WaveMath.Switch(10, 20, 12, WaveMath.SwitchCriteriaEnum.BIsLessOrEqualsThanThreshold));            
        }

        [TestMethod]
        public void TestSwitch2()
        {
            Assert.IsTrue(new double[] { 1, 2, 20 }.SequenceEqual(WaveMath.Switch(new double[] { 1, 2, 3 }, new double[] { 10, 12, 20 }, 12, WaveMath.SwitchCriteriaEnum.BIsGreaterThanThreshold)));
            Assert.IsTrue(new double[] { 1, 2, 20, 4 }.SequenceEqual(WaveMath.Switch(new double[] { 1, 2, 3, 4 }, new double[] { 10, 12, 20 }, 12, WaveMath.SwitchCriteriaEnum.BIsGreaterThanThreshold)));
            Assert.IsTrue(new double[] { 1, 2, 20, 8 }.SequenceEqual(WaveMath.Switch(new double[] { 1, 2, 3 }, new double[] { 10, 12, 20, 8 }, 12, WaveMath.SwitchCriteriaEnum.BIsGreaterThanThreshold)));
        }

        [TestMethod]
        public void TestSwitch3()
        {
            Assert.IsTrue(new double[] { 1, 2, 20 }.SequenceEqual(WaveMath.Switch(new double[] { 1, 2, 3 }, new double[] { 10, 12, 20 }, new double[] {12, 20, 8}, WaveMath.SwitchCriteriaEnum.BIsGreaterThanThreshold)));
            Assert.IsTrue(new double[] { 1, 2, 20, 4 }.SequenceEqual(WaveMath.Switch(new double[] { 1, 2, 3, 4 }, new double[] { 10, 12, 20 }, new double[] { 12, 20, 8 }, WaveMath.SwitchCriteriaEnum.BIsGreaterThanThreshold)));
            Assert.IsTrue(new double[] { 1, 2, 20, 8 }.SequenceEqual(WaveMath.Switch(new double[] { 1, 2, 3 }, new double[] { 10, 12, 20, 8 }, new double[] { 12, 20, 8 }, WaveMath.SwitchCriteriaEnum.BIsGreaterThanThreshold)));
            Assert.IsTrue(new double[] { 10, 2, 3, 8 }.SequenceEqual(WaveMath.Switch(new double[] { 1, 2, 3 }, new double[] { 10, 12, 20, 8 }, new double[] { 8 }, WaveMath.SwitchCriteriaEnum.BIsGreaterThanThreshold)));
        }
    }
}
