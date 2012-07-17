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
        public void TestSignalIndexer()
        {
            var signal = new Signal(new double[] { 0, 10, 2 }, 1);
            Assert.AreEqual(0, signal[0]);
            Assert.AreEqual(10, signal[1]);
            Assert.AreEqual(2, signal[2]);

            signal[1] = 100;
            Assert.AreEqual(100, signal[1]);
        }

        [TestMethod]
        public void TestGetSampleIndexByTime()
        {
            var signal = new Signal(new double[] {10, 20, 30, 40}, 2) {Start = 10};
            Assert.AreEqual(0, signal.GetSampleIndexByTime(9));
            Assert.AreEqual(0, signal.GetSampleIndexByTime(10.0));
            Assert.AreEqual(1, signal.GetSampleIndexByTime(10.5));
            Assert.AreEqual(2, signal.GetSampleIndexByTime(11.0));
            Assert.AreEqual(3, signal.GetSampleIndexByTime(11.5));
            Assert.AreEqual(3, signal.GetSampleIndexByTime(12));
        }
        
        [TestMethod]
        public void TestSamplesCount()
        {
            var signal = new Signal(new double[] { 1, 2, 3, 4, 5 }, 1);
            Assert.AreEqual(5, signal.SamplesCount);

            signal.Samples = null;
            Assert.AreEqual(0, signal.SamplesCount);

            signal = new Signal();
            Assert.AreEqual(0, signal.SamplesCount);

            signal.Samples = null;
            Assert.AreEqual("", signal.ToString());
        }
        
        [TestMethod]
        public void TestGetSamplesPairAndTimeSeries()
        {
            var signal = new Signal(new double[] { 1, 2, 3, 4, 5 }, 1);            
            var pair = signal.GetSamplesPair().ToArray();
            var time = signal.GetTimeSeries().ToArray();

            for (var i = 0; i < time.Length; i++)
            {
                Assert.AreEqual(pair[i][1], i);
                Assert.AreEqual(pair[i][0], signal.Samples.GetValue(i));
                Assert.AreEqual(time[i], i);
            }

            signal = new Signal();
            Assert.AreEqual(0, signal.GetSamplesPair().ToList().Count);
            Assert.AreEqual(0, signal.GetTimeSeries().ToList().Count);

            signal.SamplingInterval = 1d/0d;
            Assert.AreEqual(0, signal.GetSamplesPair().Count());
            Assert.AreEqual(0, signal.GetTimeSeries().Count());

            signal.Samples = null;
            Assert.AreEqual(0, signal.GetSamplesPair().ToList().Count);
            Assert.AreEqual(0, signal.GetTimeSeries().ToList().Count);
        }

        [TestMethod]
        public void TestClone()
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
