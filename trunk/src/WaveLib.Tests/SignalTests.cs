using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using ILNumerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WaveLib.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class SignalTests
    {
        [TestMethod]
        public void TestLengthIsPowerOf2()
        {
            var signal = new Signal(new ILArray<double>(new double[] { 1, 2, 3, 4, 5 }), 1);
            Assert.IsFalse(signal.LengthIsPowerOf2());

            signal = new Signal(new double[] { 1, 2, 3, 4 }, 1);
            Assert.IsTrue(signal.LengthIsPowerOf2());

            signal = new Signal();
            Assert.IsFalse(signal.LengthIsPowerOf2());
        }

        [TestMethod]
        public void TestMakeLengthPowerOfTwo()
        {
            var signal = new Signal(new double[] { 1, 2, 3, 4, 5 }, 1);
            signal.MakeLengthPowerOfTwo();
            Assert.AreEqual(4, signal.Points.Length);
            Assert.IsTrue(signal.Points.Equals(new ILArray<double>(new double[] { 1, 2, 3, 4 })));

            signal = new Signal(new double[] { 1, 2, 3, 4 }, 1);
            signal.MakeLengthPowerOfTwo();
            Assert.AreEqual(4, signal.Points.Length);
            Assert.IsTrue(signal.Points.Equals(new ILArray<double>(new double[] { 1, 2, 3, 4 })));

            signal = new Signal();
            signal.MakeLengthPowerOfTwo();
            Assert.AreEqual(0, signal.Points.Length);
        }
    }
}
