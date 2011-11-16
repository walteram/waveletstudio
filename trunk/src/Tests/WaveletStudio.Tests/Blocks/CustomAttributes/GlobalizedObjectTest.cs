using System;
using System.ComponentModel;
using System.Text;
using System.Collections.Generic;
using System.Linq;
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
            var block = new WaveletBlock();
            Assert.AreEqual(typeof(WaveletBlock).FullName, block.GetClassName());
            Assert.IsTrue(block.GetAttributes().Count > 0);
            Assert.IsNull(block.GetComponentName());
            Assert.IsNotNull(block.GetConverter());
            Assert.IsNull(block.GetDefaultEvent());
            Assert.IsNull(block.GetDefaultProperty());
            Assert.IsNull(block.GetEditor(typeof(WaveletBlock)));
            Assert.AreEqual(0, block.GetEvents().Count);
            Assert.IsTrue(block.GetProperties().Count > 0);            
            Assert.AreEqual(block, block.GetPropertyOwner(new PropertyDescriptorMock("WaveletName", new Attribute[] { })));

            block = new WaveletBlock();
            Assert.AreEqual(0, block.GetEvents(new Attribute[]{}).Count);
            Assert.IsTrue(block.GetProperties(new Attribute[] { }).Count > 0);
        }
    }
}
