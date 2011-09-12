using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class NodesTest
    {
        [TestMethod]
        public void TestNodesConnection()
        {
            BlockBase root1 = new ConvolutionBlock();
            BlockBase root2 = new ConvolutionBlock();
            var out1 = new BlockOutputNode(ref root1, "Out1", "O");
            var in1 = new BlockInputNode(ref root2, "In1", "I");

            in1.ConnectTo(out1);            
            Assert.AreSame(out1.ConnectingNode, in1);
            Assert.AreSame(in1.ConnectingNode, out1);

            out1.ConnectingNode = null;
            in1.ConnectingNode = null;
            in1.ConnectTo(ref out1);
            Assert.AreSame(out1.ConnectingNode, in1);
            Assert.AreSame(in1.ConnectingNode, out1);

            out1.ConnectingNode = null;
            in1.ConnectingNode = null;
            out1.ConnectTo(in1);
            Assert.AreSame(out1.ConnectingNode, in1);
            Assert.AreSame(in1.ConnectingNode, out1);

            out1.ConnectingNode = null;
            in1.ConnectingNode = null;
            out1.ConnectTo(ref in1);
            Assert.AreSame(out1.ConnectingNode, in1);
            Assert.AreSame(in1.ConnectingNode, out1);

            out1.ConnectingNode = null;
            in1.ConnectingNode = null;
            BlockNodeBase outBase = out1;
            BlockNodeBase inBase = in1;
            outBase.ConnectTo(ref inBase);
            Assert.AreSame(out1.ConnectingNode, in1);
            Assert.AreSame(in1.ConnectingNode, out1);
        }
    }
}
