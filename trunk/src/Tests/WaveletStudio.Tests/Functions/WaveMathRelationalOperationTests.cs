using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Functions
{
    [TestClass]
    public class WaveMathRelationalOperationTests
    {
        [TestMethod]
        public void TestGetRelationalOperationFunctionEqualsTo()
        {
            var func = WaveMath.GetRelationalOperationFunction(WaveMath.RelationalOperatorEnum.EqualsTo);
            Assert.AreEqual(1, func(0, 0));
            Assert.AreEqual(0, func(0, 1));
            Assert.AreEqual(0, func(1, 0));
            Assert.AreEqual(1, func(1, 1));
            Assert.AreEqual(0, func(0, -1));
            Assert.AreEqual(0, func(-1, 0));
            Assert.AreEqual(1, func(-1, -1));
        }
        
        [TestMethod]
        public void TestGetRelationalOperationFunctionOrNotEqualsTo()
        {
            var func = WaveMath.GetRelationalOperationFunction(WaveMath.RelationalOperatorEnum.NotEqualsTo);
            Assert.AreEqual(0, func(0, 0));
            Assert.AreEqual(1, func(0, 1));
            Assert.AreEqual(1, func(1, 0));
            Assert.AreEqual(0, func(1, 1));
            Assert.AreEqual(1, func(0, -1));
            Assert.AreEqual(1, func(-1, 0));
            Assert.AreEqual(0, func(-1, -1));
        }

        [TestMethod]
        public void TestGetRelationalOperationFunctionGreaterThan()
        {
            var func = WaveMath.GetRelationalOperationFunction(WaveMath.RelationalOperatorEnum.GreaterThan);
            Assert.AreEqual(0, func(0, 0));
            Assert.AreEqual(0, func(0, 1));
            Assert.AreEqual(1, func(1, 0));
            Assert.AreEqual(0, func(1, 1));
            Assert.AreEqual(1, func(0, -1));
            Assert.AreEqual(0, func(-1, 0));
            Assert.AreEqual(0, func(-1, -1));
        }

        [TestMethod]
        public void TestGetRelationalOperationFunctionLessThan()
        {
            var func = WaveMath.GetRelationalOperationFunction(WaveMath.RelationalOperatorEnum.LessThan);
            Assert.AreEqual(0, func(0, 0));
            Assert.AreEqual(1, func(0, 1));
            Assert.AreEqual(0, func(1, 0));
            Assert.AreEqual(0, func(1, 1));
            Assert.AreEqual(0, func(0, -1));
            Assert.AreEqual(1, func(-1, 0));
            Assert.AreEqual(0, func(-1, -1));
        }
        
        [TestMethod]
        public void TestGetRelationalOperationFunctionGreaterOrEqualsThan()
        {
            var func = WaveMath.GetRelationalOperationFunction(WaveMath.RelationalOperatorEnum.GreaterOrEqualsThan);
            Assert.AreEqual(1, func(0, 0));
            Assert.AreEqual(0, func(0, 1));
            Assert.AreEqual(1, func(1, 0));
            Assert.AreEqual(1, func(1, 1));
            Assert.AreEqual(1, func(0, -1));
            Assert.AreEqual(0, func(-1, 0));
            Assert.AreEqual(1, func(-1, -1));
        }

        [TestMethod]
        public void TestGetRelationalOperationFunctionLessOrEqualsThan()
        {
            var func = WaveMath.GetRelationalOperationFunction(WaveMath.RelationalOperatorEnum.LessOrEqualsThan);
            Assert.AreEqual(1, func(0, 0));
            Assert.AreEqual(1, func(0, 1));
            Assert.AreEqual(0, func(1, 0));
            Assert.AreEqual(1, func(1, 1));
            Assert.AreEqual(0, func(0, -1));
            Assert.AreEqual(1, func(-1, 0));
            Assert.AreEqual(1, func(-1, -1));
        }

        [TestMethod]
        public void TestExecuteRelationalOperationWithTwoArrays()
        {
            var array1 = new[] { 1.0, -3.1, 4.4, 8.5 };
            var array2 = new[] {1.0, 2.1, 3.4, 4.5};
            var result = WaveMath.ExecuteRelationalOperation(WaveMath.RelationalOperatorEnum.GreaterThan, array1, array2);
            Assert.IsTrue(TestUtils.SequenceEquals(result, new[] {0.0, 0.0, 1.0, 1.0}));

            array1 = new[] { 1.0, -3.1, 4.4 };
            array2 = new[] { 1.0, 2.1, 3.4, 4.5 };
            result = WaveMath.ExecuteRelationalOperation(WaveMath.RelationalOperatorEnum.GreaterThan, array1, array2);
            Assert.IsTrue(TestUtils.SequenceEquals(result, new[] { 0.0, 0.0, 1.0, 0.0 }));

            array1 = new[] { 1.0, -3.1, 4.4, 8.5 };
            array2 = new[] { 1.0, 2.1, 3.4 };
            result = WaveMath.ExecuteRelationalOperation(WaveMath.RelationalOperatorEnum.GreaterThan, array1, array2);
            Assert.IsTrue(TestUtils.SequenceEquals(result, new[] { 0.0, 0.0, 1.0, 0.0 }));
        }

        [TestMethod]
        public void TestExecuteRelationalOperationWithNextSample()
        {
            var array1 = new[] { 1.0, 3.1, 2.4, 2.5 };
            var result = WaveMath.ExecuteRelationalOperationWithNextSample(WaveMath.RelationalOperatorEnum.LessThan, array1);
            Assert.IsTrue(TestUtils.SequenceEquals(result, new[] { 1.0, 0.0, 1.0, 0.0 }));
        }

        [TestMethod]
        public void TestExecuteRelationalOperationWithPreviousSample()
        {
            var array1 = new[] { 1.0, 3.1, 2.4, 2.1 };
            var result = WaveMath.ExecuteRelationalOperationWithPreviousSample(WaveMath.RelationalOperatorEnum.LessThan, array1);
            Assert.IsTrue(TestUtils.SequenceEquals(result, new[] { 0.0, 0.0, 1.0, 1.0 }));
        }

        [TestMethod]
        public void TestExecuteRelationalOperationWithStaticValue()
        {
            var array1 = new[] { 1.0, 3.1, 2.4, 2.0 };
            var result = WaveMath.ExecuteRelationalOperation(WaveMath.RelationalOperatorEnum.LessOrEqualsThan, array1, 2);
            Assert.IsTrue(TestUtils.SequenceEquals(result, new[] { 1.0, 0.0, 0.0, 1.0 }));
        }

        [TestMethod]
        public void TestExecuteRelationalOperationWithSignals()
        {
            var signal1 = new Signal(new[] { 0.0, 0.0, 45.1, 10.2 });
            var signal2 = new Signal(new[] { 0.0, 20.0, 0.1, 15.1 });
            var signal3 = new Signal(new double[] { });
            var signal4 = new Signal(null);

            Assert.IsTrue(TestUtils.SequenceEquals(new[] { 1.0, 1.0, 0.0, 1.0 }, WaveMath.ExecuteRelationalOperation(WaveMath.RelationalOperatorEnum.LessOrEqualsThan, signal1, signal2).Samples));
            Assert.IsTrue(TestUtils.SequenceEquals(new[] { 1.0, 1.0, 0.0, 1.0 }, WaveMath.ExecuteRelationalOperation(WaveMath.RelationalOperatorEnum.LessOrEqualsThan, signal1, signal2, signal3, signal4).Samples));
            Assert.IsNull(WaveMath.ExecuteRelationalOperation(WaveMath.RelationalOperatorEnum.GreaterThan, null, signal3, signal4));
        }

        [TestMethod]
        public void TestExecuteRelationalOperationWithSignalAndStaticValue()
        {
            var signal1 = new Signal(new[] { 0.0, 20.0, 10.0, 10.2 });
            Assert.IsTrue(TestUtils.SequenceEquals(new[] { 1.0, 0.0, 1.0, 0.0 }, WaveMath.ExecuteRelationalOperation(WaveMath.RelationalOperatorEnum.LessOrEqualsThan, signal1, 10).Samples));            
        }

        [TestMethod]
        public void TestExecuteRelationalOperationNextSampleWithSignal()
        {
            var signal1 = new Signal(new[] { 1.0, 3.1, 2.4, 2.5 });
            Assert.IsTrue(TestUtils.SequenceEquals(new[] { 1.0, 0.0, 1.0, 0.0 }, WaveMath.ExecuteRelationalOperationWithNextSample(WaveMath.RelationalOperatorEnum.LessOrEqualsThan, signal1).Samples));
        }

        [TestMethod]
        public void TestExecuteRelationalOperationPreviousSampleWithSignal()
        {
            var signal1 = new Signal(new[] { 1.0, 3.1, 2.4, 2.5 });
            Assert.IsTrue(TestUtils.SequenceEquals(new[] { 0.0, 0.0, 1.0, 0.0 }, WaveMath.ExecuteRelationalOperationWithPreviousSample(WaveMath.RelationalOperatorEnum.LessOrEqualsThan, signal1).Samples));
        }
    }
}
