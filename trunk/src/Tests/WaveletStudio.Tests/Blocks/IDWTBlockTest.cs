using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class IDWTBlockTest
    {
        [TestMethod]
        public void TestIDWTBlockExecute()
        {
            const string signal1 = "1 2 3 4 5 6 7 8 7 6 16 5 4 3 2 1";
            const string signal2 = "7 6 16 5 4 3 2 1 1 2 3 4 5 6 7 8";
            const string signal = "Signal1 " + signal1 + "\r\nSignal2 " + signal2;
            var signalBlock = new ImportFromTextBlock { Text = signal, ColumnSeparator = " " };
            var dwtBlock = new DWTBlock { WaveletName = "db4", Level = 2, ExtensionMode = SignalExtension.ExtensionMode.SymmetricHalfPoint };
            var idwtBlock = new IDWTBlock {WaveletName = "db4", Level = 2};
            idwtBlock.Execute();
            Assert.IsTrue(idwtBlock.WaveletNameList.Count > 0);
            Assert.AreEqual("db4", idwtBlock.WaveletName);

            signalBlock.OutputNodes[0].ConnectTo(dwtBlock.InputNodes[0]);
            dwtBlock.OutputNodes[0].ConnectTo(idwtBlock.InputNodes[0]);
            dwtBlock.OutputNodes[1].ConnectTo(idwtBlock.InputNodes[1]);
            Assert.IsNotNull(idwtBlock.Name);
            Assert.IsNotNull(idwtBlock.Description);
            Assert.IsNotNull(idwtBlock.ProcessingType);

            signalBlock.Execute();
            Assert.AreEqual(signal1, idwtBlock.OutputNodes[0].Object[0].ToString(0));
            Assert.AreEqual(signal2, idwtBlock.OutputNodes[0].Object[1].ToString(0));

            var block2 = (IDWTBlock)idwtBlock.Clone();
            signalBlock.OutputNodes[0].ConnectTo(block2.InputNodes[0]);
            idwtBlock.OutputNodes[0].ConnectTo(block2.InputNodes[1]);
            signalBlock.Execute();
            idwtBlock.Execute();
            Assert.AreEqual("3.2 1.8 4.6 1.8 6.0 1.8 7.4 1.8 8.8 1.8 9.8 0.4 9.5 -1.3 12.0 5.7 15.6 0.7 5.2 -7.5 14.2 -4.2 5.3 -1.8 3.9 -1.8", block2.OutputNodes[0].Object[0].ToString(1));

            idwtBlock.Cascade = false;
            block2 = (IDWTBlock)idwtBlock.Clone();
            idwtBlock.OutputNodes[0].ConnectTo(block2.InputNodes[0]);
            signalBlock.Execute();
            idwtBlock.Execute();
            Assert.AreEqual(0, block2.OutputNodes[0].Object.Count);

            try
            {
                idwtBlock.WaveletName = "nonono";
                Assert.Fail("Exception not thrown!");
            }
            catch (Exception)
            {
                Assert.IsTrue(true, "Exception thrown! Yeay!");
            }
        }

        [TestMethod]
        public void TestIDWTBlockExecute2()
        {
            var signalBlock = new ImportFromTextBlock { Text = "2.1,3.2,-1,-1.3,-100,-2,15,22" };
            var dwtBlock = new DWTBlock { WaveletName = "DB4", Level = 2 };
            var idwtBlock = new IDWTBlock { WaveletName = "DB4", Level = 2 };
            signalBlock.ConnectTo(dwtBlock);
            //Connect approximation
            dwtBlock.OutputNodes[0].ConnectTo(idwtBlock.InputNodes[0]);
            //Connect details
            dwtBlock.OutputNodes[1].ConnectTo(idwtBlock.InputNodes[1]);
            signalBlock.Execute();
            Console.WriteLine(idwtBlock.OutputNodes[0].Object.ToString(1));

            Assert.AreEqual("2.1 3.2 -1.0 -1.3 -100.0 -2.0 15.0 22.0", idwtBlock.OutputNodes[0].Object.ToString(1));
        }
    }
}
