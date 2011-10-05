using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WaveletStudio.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class SignalTests
    {
        [TestMethod]
        public void TestClone()
        {
            var signal = new Signal(new double[] { 1, 2, 3, 4, 5 }, 1);
            var pair = signal.GetSamplesPair();

            var i = 0;
            foreach (var item in pair)
            {
                Assert.AreEqual(item[1], i);
                Assert.AreEqual(item[0], signal.Samples.GetValue(i));                
                i++;
            }

            signal = new Signal();
            pair = signal.GetSamplesPair();
            Assert.AreEqual(0, pair.ToList().Count);

            signal = new Signal(new double[] { 1, 2, 3, 4, 5 }) { SamplingInterval = Double.PositiveInfinity };
            pair = signal.GetSamplesPair();
            Assert.AreEqual(5, pair.ToList().Count);
        }

        [TestMethod]
        public void TestGetSamplesPair()
        {
            var signal = new Signal(new double[] { 1, 2, 3, 4, 5 }, 1);
            var clone = signal.Clone();
            Assert.IsTrue(TestUtils.SequenceEquals(signal.Samples, clone.Samples));
            Assert.AreNotSame(signal, clone);
            Assert.AreNotSame(signal.Samples, clone.Samples);
        }
        
        [TestMethod]
        public void TestLengthIsPowerOf2()
        {
            var signal = new Signal(new double[] { 1, 2, 3, 4, 5 }, 1);
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
            Assert.AreEqual(4, signal.Samples.Length);
            Assert.IsTrue(TestUtils.SequenceEquals(signal.Samples, new double[] { 1, 2, 3, 4 }));

            signal = new Signal(new double[] { 1, 2, 3, 4 }, 1);
            signal.MakeLengthPowerOfTwo();
            Assert.AreEqual(4, signal.Samples.Length);
            Assert.IsTrue(TestUtils.SequenceEquals(signal.Samples, new double[] { 1, 2, 3, 4 }));

            signal = new Signal();
            signal.MakeLengthPowerOfTwo();
            Assert.AreEqual(0, signal.Samples.Length);
        }
    }
}
