using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Functions
{
    [TestClass]
    public class WaveMathOperationTests
    {
        [TestMethod]
        public void TestExecuteOperation()
        {
            var signal1 = new Signal(new[] { 3, 2, 2, 1, 8d });
            var signal2 = new Signal(new[] { 5, 1, -3, 2d });
            var signal3 = new Signal(new[] { 1, 2, 3, 4, 5, 6d });
            var signal4 = new Signal(new double[] { });
            var signal5 = new Signal(null);

            Assert.IsTrue(TestUtils.SequenceEquals(new[] { 9, 5, 2, 7, 13, 6d }, WaveMath.ExecuteOperation(WaveMath.OperationEnum.Sum, signal1, signal2, signal3, signal4, signal5).Samples));
            Assert.IsTrue(TestUtils.SequenceEquals(new[] { -7, -1, 4, 1, -3, 6d }, WaveMath.ExecuteOperation(WaveMath.OperationEnum.Subtract, signal1, signal2, signal3, signal4, signal5).Samples));
            Assert.IsTrue(TestUtils.SequenceEquals(new[] { 15, 4, -18, 8, 40, 6d }, WaveMath.ExecuteOperation(WaveMath.OperationEnum.Multiply, signal1, signal2, signal3, signal4, signal5).Samples));
            Assert.IsTrue(TestUtils.SequenceEquals(new[] { 0.0666666, 1, -0.5, 2, 0.625, 6d }, WaveMath.ExecuteOperation(WaveMath.OperationEnum.Divide, signal1, signal2, signal3, signal4, signal5).Samples));
            Assert.IsTrue(TestUtils.SequenceEquals(new[] { 0.0666666, 1, -0.5, 2, 0.625, 6d }, WaveMath.ExecuteOperation(WaveMath.OperationEnum.Divide, signal1, signal2, signal3, signal4, signal5).Samples));
            Assert.IsNull(WaveMath.ExecuteOperation(WaveMath.OperationEnum.Sum, null, signal4, signal5));
        }

        [TestMethod]
        public void TestAddArrays()
        {
            var array1 = new[] { 1.2, 2.3, 3.4, 4.5 };
            var array2 = new[] { 1.1, 2.2, 3.3, 0 };
            var expected = new[] { 2.3, 4.5, 6.7, 4.5 };
            var expected2 = new[] { 2.3, 4.5, 6.7, 0 };
            Assert.IsTrue(TestUtils.SequenceEquals(expected, WaveMath.GetOperationFunction(WaveMath.OperationEnum.Sum)(array1, array2)));
            Assert.IsTrue(TestUtils.SequenceEquals(expected, WaveMath.Add(array2, array1)));
            Assert.IsTrue(TestUtils.SequenceEquals(expected, WaveMath.Add(array1, array2.SubArray(3))));
            Assert.IsTrue(TestUtils.SequenceEquals(expected2, WaveMath.Add(array1.SubArray(3), array2)));
        }

        [TestMethod]
        public void TestAddScalar()
        {
            var array1 = new[] { 1.2, 2.3, 3.4, 4.5 };
            const double scalar = 1.1;
            var expected = new[] { 2.3, 3.4, 4.5, 5.6 };
            Assert.IsTrue(TestUtils.SequenceEquals(expected, WaveMath.Add(array1, scalar)));
        }

        [TestMethod]
        public void TestMultiplyArrays()
        {
            var array1 = new[] { 1.2, 2.3, 3.4, 4.5 };
            var array2 = new[] { 1.1, 2.2, 3.3, 1 };
            var expected = new[] { 1.32, 5.06, 11.22, 4.5 };
            Assert.IsTrue(TestUtils.SequenceEquals(expected, WaveMath.GetOperationFunction(WaveMath.OperationEnum.Multiply)(array1, array2)));
            Assert.IsTrue(TestUtils.SequenceEquals(expected, WaveMath.Multiply(array2, array1)));
            Assert.IsTrue(TestUtils.SequenceEquals(expected, WaveMath.Multiply(array1, array2.SubArray(3))));
        }

        [TestMethod]
        public void TestMultiplyScalar()
        {
            var array1 = new[] { 1.2, 2.3, 3.4, 4.5 };
            const double scalar = 1.1;
            var expected = new[] { 1.32, 2.53, 3.74, 4.95 };
            Assert.IsTrue(TestUtils.SequenceEquals(expected, WaveMath.Multiply(array1, scalar)));
        }

        [TestMethod]
        public void TestDivideArrays()
        {
            var array1 = new[] { 2, 4, 0, 12.0 };
            var array2 = new[] { 1, 0, 3, 8.0 };
            var expected1 = new[] { 2, 0, 0, 1.5 };
            var expected2 = new[] { 0.5, 0, 0, 0.6666667 };
            var expected3 = new[] { 2, 0, 0, 12.0 };
            var expected4 = new[] { 0.5, 0, 0, 8 };
            Assert.IsTrue(TestUtils.SequenceEquals(expected1, WaveMath.GetOperationFunction(WaveMath.OperationEnum.Divide)(array1, array2)));
            Assert.IsTrue(TestUtils.SequenceEquals(expected2, WaveMath.Divide(array2, array1)));
            Assert.IsTrue(TestUtils.SequenceEquals(expected3, WaveMath.Divide(array1, array2.SubArray(3))));
            Assert.IsTrue(TestUtils.SequenceEquals(expected4, WaveMath.Divide(array1.SubArray(3), array2)));
        }

        [TestMethod]
        public void TestDivideScalar()
        {
            var array1 = new[] { 1.2, 2.3, 3.4, 4.5 };
            var scalar = 1.1;
            var expected = new[] { 1.09090909, 2.09090909, 3.09090909, 4.09090909 };
            Assert.IsTrue(TestUtils.SequenceEquals(expected, WaveMath.Divide(array1, scalar)));

            expected = new[] { 0.0, 0.0, 0.0, 0.0 };
            Assert.IsTrue(TestUtils.SequenceEquals(expected, WaveMath.Divide(array1, 0)));
        }

        [TestMethod]
        public void TestSubtractArrays()
        {
            var array1 = new[] { 1.2, 2.2, 3.4, 4.5 };
            var array2 = new[] { 1.1, 2.3, 3.3, 0 };
            var expected1 = new[] { 0.1, -0.1, 0.1, 4.5 };
            var expected2 = new[] { -0.1, 0.1, -0.1, -4.5 };
            var expected3 = new[] { -0.1, 0.1, -0.1, 0 };
            Assert.IsTrue(TestUtils.SequenceEquals(expected1, WaveMath.GetOperationFunction(WaveMath.OperationEnum.Subtract)(array1, array2)));
            Assert.IsTrue(TestUtils.SequenceEquals(expected2, WaveMath.Subtract(array2, array1)));
            Assert.IsTrue(TestUtils.SequenceEquals(expected1, WaveMath.Subtract(array1, array2.SubArray(3))));
            Assert.IsTrue(TestUtils.SequenceEquals(expected3, WaveMath.Subtract(array1.SubArray(3), array2)));
        }

        [TestMethod]
        public void TestSubtractScalar()
        {
            var array1 = new[] { 1.2, 2.3, 3.4, 4.5 };
            const double scalar = 1.1;
            var expected = new[] { 0.1, 1.2, 2.3, 3.4 };
            Assert.IsTrue(TestUtils.SequenceEquals(expected, WaveMath.Subtract(array1, scalar)));
        }
    }
}
