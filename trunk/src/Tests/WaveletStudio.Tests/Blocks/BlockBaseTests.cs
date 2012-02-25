using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class BlockBaseTests
    {
        [TestMethod]
        public void TestGetName()
        {
            var block = new ConvolutionBlock();
            Assert.AreEqual(block.Name, BlockBase.GetName(block.GetType()));
            Assert.AreEqual("ConvolutionBlock", block.GetAssemblyClassName());

            Assert.AreEqual("", BlockBase.GetName(typeof(Object)));
            Assert.AreEqual("", BlockBase.GetName(typeof(BlockBase)));
            Assert.AreEqual(true, block.HasParameters());

            var block2 = new InvertBlock();
            Assert.AreEqual(false, block2.HasParameters());
        }

        [TestMethod]
        public void TestCurrentDirectory()
        {
            var block = new ConvolutionBlock();
            Assert.AreEqual(Utils.AssemblyDirectory, block.CurrentDirectory);

            block.CurrentDirectory = "xxx";
            Assert.AreEqual("xxx", block.CurrentDirectory);
        }
    }
}
