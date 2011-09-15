using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class IFFTBlockTest
    {
        [TestMethod]
        public void TestIFFTBlockExecute()
        {
            var signalBlock = new GenerateSignalBlock { TemplateName = "Binary", Start = 0, Finish = 31, SamplingRate = 1, Offset = 1.1 };
            var fftBlock = new FFTBlock();
            var ifftBlock = new IFFTBlock();
            ifftBlock.Execute();

            signalBlock.OutputNodes[0].ConnectTo(fftBlock.InputNodes[0]);
            fftBlock.OutputNodes[1].ConnectTo(ifftBlock.InputNodes[0]);
            Assert.IsNotNull(ifftBlock.Name);
            Assert.IsNotNull(ifftBlock.Description);
            Assert.IsNotNull(ifftBlock.ProcessingType);

            signalBlock.Execute();
            Assert.AreEqual("1.1 2.1 1.1 2.1 1.1 2.1 1.1 2.1 1.1 2.1 1.1 2.1 1.1 2.1 1.1 2.1 1.1 2.1 1.1 2.1 1.1 2.1 1.1 2.1 1.1 2.1 1.1 2.1 1.1 2.1 1.1 2.1", ifftBlock.OutputNodes[0].Object.ToString(1));

            //Test cascade
            var scalarBlock = new ScalarOperationBlock { Value = 1, Operation = ScalarOperationBlock.OperationEnum.Sum };
            ifftBlock.OutputNodes[0].ConnectTo(scalarBlock.InputNodes[0]);
            signalBlock.Execute();
            Assert.AreEqual("2.1 3.1 2.1 3.1 2.1 3.1 2.1 3.1 2.1 3.1 2.1 3.1 2.1 3.1 2.1 3.1 2.1 3.1 2.1 3.1 2.1 3.1 2.1 3.1 2.1 3.1 2.1 3.1 2.1 3.1 2.1 3.1", scalarBlock.OutputNodes[0].Object.ToString(1));

            //Test when cascade is false
            fftBlock.Cascade = false;
            var ifftBlock2 = (IFFTBlock)ifftBlock.Clone();
            ifftBlock.OutputNodes[0].ConnectTo(ifftBlock2.InputNodes[0]);
            signalBlock.Execute();
            Assert.IsNull(ifftBlock2.OutputNodes[0].Object);

            ifftBlock2.Cascade = false;
            ifftBlock2.Execute();
            Assert.IsNotNull(ifftBlock2.OutputNodes[0].Object);
        }
    }
}
