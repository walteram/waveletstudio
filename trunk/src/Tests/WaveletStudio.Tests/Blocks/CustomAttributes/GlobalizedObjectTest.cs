using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;

namespace WaveletStudio.Tests.Blocks.CustomAttributes
{
    [TestClass]
    public class GlobalizedObjectTest
    {
        [TestMethod]
        public void TestGlobalizedObject()
        {
            var block = new DWTBlock();
            Assert.AreEqual(typeof(DWTBlock).FullName, block.GetClassName());
            Assert.IsTrue(block.GetAttributes().Count > 0);
            Assert.IsNull(block.GetComponentName());
            Assert.IsNotNull(block.GetConverter());
            Assert.IsNull(block.GetDefaultEvent());
            Assert.IsNull(block.GetDefaultProperty());
            Assert.IsNull(block.GetEditor(typeof(DWTBlock)));
            Assert.AreEqual(0, block.GetEvents().Count);
            Assert.IsTrue(block.GetProperties().Count > 0);
            Assert.IsFalse(block.CausesRefresh);
            Assert.AreEqual(block, block.GetPropertyOwner(new PropertyDescriptorMock("WaveletName", new Attribute[] { })));

            block = new DWTBlock();
            Assert.AreEqual(0, block.GetEvents(new Attribute[]{}).Count);
            Assert.IsTrue(block.GetProperties(new Attribute[] { }).Count > 0);

            var block2 = new ScalarOperationBlock();
            Assert.IsTrue(block2.CausesRefresh);
        }
    }
}
