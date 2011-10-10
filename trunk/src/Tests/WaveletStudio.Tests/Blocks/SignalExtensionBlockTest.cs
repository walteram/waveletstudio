using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class SignalExtensionBlockTest
    {
        [TestMethod]
        public void TestSignalExtensionBlockTestExecute()
        {
            var signalBlock = new ImportFromTextBlock();
            var extensionBlock = new SignalExtensionBlock { ExtensionMode = SignalExtension.ExtensionMode.SymmetricHalfPoint };
            extensionBlock.Execute();
            
            signalBlock.ConnectTo(extensionBlock);
            Assert.IsNotNull(extensionBlock.Name);
            Assert.IsNotNull(extensionBlock.Description);
            Assert.IsNotNull(extensionBlock.ProcessingType);

            extensionBlock.ExtensionSize = 0;
            signalBlock.Text = "1, 2, 3, 4, 5";
            signalBlock.Execute();
            Assert.AreEqual("1, 1, 2, 3, 4, 5, 5, 4", extensionBlock.OutputNodes[0].Object.ToString(0, ", "));

            extensionBlock.ExtensionSize = 3;
            signalBlock.Text = "2, 6, 1, 6, 5, 0, 4, 5, 3, 4";            
            signalBlock.Execute();
            Assert.AreEqual("1, 6, 2, 2, 6, 1, 6, 5, 0, 4, 5, 3, 4, 4, 3, 5", extensionBlock.OutputNodes[0].Object.ToString(0, ", "));

            var extensionBlock2 = (SignalExtensionBlock)extensionBlock.Clone();
            extensionBlock2.ExtensionMode = SignalExtension.ExtensionMode.PeriodicPadding;            
            extensionBlock.ConnectTo(extensionBlock2);
            signalBlock.Execute();
            Assert.AreEqual("4, 3, 5, 1, 6, 2, 2, 6, 1, 6, 5, 0, 4, 5, 3, 4, 4, 3, 5, 1, 6, 2", extensionBlock2.OutputNodes[0].Object.ToString(0, ", "));

            extensionBlock.Cascade = false;
            extensionBlock2 = (SignalExtensionBlock)extensionBlock.Clone();
            extensionBlock.ConnectTo(extensionBlock2);
            signalBlock.Execute();
            Assert.AreEqual("", extensionBlock2.OutputNodes[0].Object.ToString(0, " "));
            Assert.AreEqual(0, extensionBlock2.OutputNodes[0].Object.Count);
        }
    }
}
