using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Functions;
using WaveletStudio.Blocks;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class BlockListTest
    {
        [TestMethod]
        public void TestBlockList()
        {
            var signalBlock1 = new GenerateSignalBlock { Cascade = false, Offset  = 1, TemplateName = "Binary", Start = 0, Finish = 3, SamplingRate = 1};
            var signalBlock2 = new GenerateSignalBlock { Cascade = false, Offset = 2, TemplateName = "Binary", Start = 0, Finish = 3, SamplingRate = 1 };
            var signalBlock3 = new GenerateSignalBlock { Offset = 3, TemplateName = "Binary", Start = 0, Finish = 3, SamplingRate = 1 };
            var convolutionBlock1 = new ConvolutionBlock();
            var convolutionBlock2 = new ConvolutionBlock();
            var blockList = new BlockList { signalBlock1, signalBlock2, signalBlock3, convolutionBlock1, convolutionBlock2 };

            signalBlock1.ConnectTo(convolutionBlock1);
            signalBlock2.ConnectTo(convolutionBlock1);
            convolutionBlock1.ConnectTo(convolutionBlock2);
            signalBlock2.ConnectTo(convolutionBlock2);

            blockList.ExecuteAll();

            Assert.AreEqual("4 20 45 78 111 114 103 74 33 18", convolutionBlock2.OutputNodes[0].Object.ToString(0));
        }
    }
}
