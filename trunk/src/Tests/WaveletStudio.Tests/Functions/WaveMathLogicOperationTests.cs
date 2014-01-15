using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Functions
{
    [TestClass]
    public class WaveMathLogicOperationTests
    {
        [TestMethod]
        public void TestGetLogicalOperationFunctionAnd()
        {
            var logicalAnd = WaveMath.GetLogicalOperationFunction(WaveMath.LogicalOperationEnum.And);
            Assert.AreEqual(0, logicalAnd(0, 0));
            Assert.AreEqual(0, logicalAnd(0, 1));
            Assert.AreEqual(0, logicalAnd(1, 0));
            Assert.AreEqual(1, logicalAnd(1, 1));
            Assert.AreEqual(0, logicalAnd(0, -1));
            Assert.AreEqual(0, logicalAnd(-1, 0));
            Assert.AreEqual(1, logicalAnd(-1, -1));
        }

        [TestMethod]
        public void TestGetLogicalOperationFunctionOr()
        {
            var logicalOr = WaveMath.GetLogicalOperationFunction(WaveMath.LogicalOperationEnum.Or);
            Assert.AreEqual(0, logicalOr(0, 0));
            Assert.AreEqual(1, logicalOr(0, 1));
            Assert.AreEqual(1, logicalOr(1, 0));
            Assert.AreEqual(1, logicalOr(1, 1));
            Assert.AreEqual(1, logicalOr(0, -1));
            Assert.AreEqual(1, logicalOr(-1, 0));
            Assert.AreEqual(1, logicalOr(-1, -1));
        }

        [TestMethod]
        public void TestGetLogicalOperationFunctionXor()
        {
            var logicalXor = WaveMath.GetLogicalOperationFunction(WaveMath.LogicalOperationEnum.Xor);
            Assert.AreEqual(0, logicalXor(0, 0));
            Assert.AreEqual(1, logicalXor(0, 1));
            Assert.AreEqual(1, logicalXor(1, 0));
            Assert.AreEqual(0, logicalXor(1, 1));
            Assert.AreEqual(1, logicalXor(0, -1));
            Assert.AreEqual(1, logicalXor(-1, 0));
            Assert.AreEqual(0, logicalXor(-1, -1));
        }

        [TestMethod]
        public void TestGetLogicalOperationFunctionNotAnd()
        {
            var logicalNotAnd = WaveMath.GetLogicalOperationFunction(WaveMath.LogicalOperationEnum.NotAnd);
            Assert.AreEqual(1, logicalNotAnd(0, 0));
            Assert.AreEqual(1, logicalNotAnd(0, 1));
            Assert.AreEqual(1, logicalNotAnd(1, 0));
            Assert.AreEqual(0, logicalNotAnd(1, 1));
            Assert.AreEqual(1, logicalNotAnd(0, -1));
            Assert.AreEqual(1, logicalNotAnd(-1, 0));
            Assert.AreEqual(0, logicalNotAnd(-1, -1));
        }
        
        [TestMethod]
        public void TestGetLogicalOperationFunctionNotOr()
        {
            var logicalNotOr = WaveMath.GetLogicalOperationFunction(WaveMath.LogicalOperationEnum.NotOr);
            Assert.AreEqual(1, logicalNotOr(0, 0));
            Assert.AreEqual(0, logicalNotOr(0, 1));
            Assert.AreEqual(0, logicalNotOr(1, 0));
            Assert.AreEqual(0, logicalNotOr(1, 1));
            Assert.AreEqual(0, logicalNotOr(0, -1));
            Assert.AreEqual(0, logicalNotOr(-1, 0));
            Assert.AreEqual(0, logicalNotOr(-1, -1));
        }

        [TestMethod]
        public void TestGetLogicalOperationFunctionNotXor()
        {
            var logicalNotXor = WaveMath.GetLogicalOperationFunction(WaveMath.LogicalOperationEnum.NotXor);
            Assert.AreEqual(1, logicalNotXor(0, 0));
            Assert.AreEqual(0, logicalNotXor(0, 1));
            Assert.AreEqual(0, logicalNotXor(1, 0));
            Assert.AreEqual(1, logicalNotXor(1, 1));
            Assert.AreEqual(0, logicalNotXor(0, -1));
            Assert.AreEqual(0, logicalNotXor(-1, 0));
            Assert.AreEqual(1, logicalNotXor(-1, -1));
        }

        [TestMethod]
        public void TestGetLogicalOperationFunctionNot()
        {
            var logicalNot = WaveMath.GetLogicalOperationFunction(WaveMath.LogicalOperationEnum.Not);
            Assert.AreEqual(1, logicalNot(0, 0)); //The second parameter is not used...
            Assert.AreEqual(1, logicalNot(0, 1));
            Assert.AreEqual(0, logicalNot(1, 0));
            Assert.AreEqual(0, logicalNot(1, 1));
            Assert.AreEqual(1, logicalNot(0, -1));
            Assert.AreEqual(0, logicalNot(-1, 0));
            Assert.AreEqual(0, logicalNot(-1, -1));
        }

        [TestMethod]
        public void TestExecuteLogicOperation()
        {
            var x1 = new[] {0.0, 0.0, 45.1, 10.2};
            var x2 = new[] {0.0, 20.0, 0.1, 15.1};
            Assert.IsTrue(TestUtils.SequenceEquals(new[] { 0.0, 1.0, 1.0, 1.0 }, WaveMath.ExecuteLogicOperation(WaveMath.LogicalOperationEnum.Or, x1, x2)));

            x1 = new[] { 0.0, 0.0, 45.1, 10.2, 1.0 };
            x2 = new[] { 0.0, 20.0, 0.1, 15.1 };
            Assert.IsTrue(TestUtils.SequenceEquals(new[] { 0.0, 1.0, 1.0, 1.0, 0.0 }, WaveMath.ExecuteLogicOperation(WaveMath.LogicalOperationEnum.Or, x1, x2)));
            Assert.IsTrue(TestUtils.SequenceEquals(new[] { 0.0, 1.0, 1.0, 1.0, 0.0 }, WaveMath.ExecuteLogicOperation(WaveMath.LogicalOperationEnum.Or, x2, x1)));
        }

        [TestMethod]
        public void TestExecuteLogicOperationWithSignals()
        {
            var signal1 = new Signal(new[] { 0.0, 0.0, 45.1, 10.2 });
            var signal2 = new Signal(new[] { 0.0, 20.0, 0.1, 15.1 });
            var signal3 = new Signal(new double[] { });
            var signal4 = new Signal(null);

            Assert.IsTrue(TestUtils.SequenceEquals(new[] { 0.0, 1.0, 1.0, 1.0 }, WaveMath.ExecuteLogicOperation(WaveMath.LogicalOperationEnum.Or, signal1, signal2).Samples));
            Assert.IsTrue(TestUtils.SequenceEquals(new[] { 0.0, 1.0, 1.0, 1.0 }, WaveMath.ExecuteLogicOperation(WaveMath.LogicalOperationEnum.Or, signal1, signal2, signal3, signal4).Samples));
            Assert.IsNull(WaveMath.ExecuteLogicOperation(WaveMath.LogicalOperationEnum.Or, null, signal3, signal4));
        }
    }
}
