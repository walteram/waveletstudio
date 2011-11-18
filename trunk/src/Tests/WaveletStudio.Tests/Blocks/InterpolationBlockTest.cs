using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class InterpolationBlockTest
    {
        [TestMethod]
        public void TestInterpolationBlockExecute()
        {
            var signalBlock = new ImportFromTextBlock{ColumnSeparator = " "};
            var block = new InterpolationBlock();
            block.Execute();
            
            signalBlock.ConnectTo(block);
            Assert.IsNotNull(block.Name);
            Assert.IsNotNull(block.Description);
            Assert.IsNotNull(block.ProcessingType);

            block.Factor = 7;
            block.Mode = InterpolationModeEnum.Cubic;
            signalBlock.Text = "-7 4 8 -2 1 -55 2";
            signalBlock.SignalStart = -1;
            signalBlock.SamplingInterval = 4d / 7d;
            signalBlock.Execute();
            Assert.AreEqual("-7.0000 -6.0436 -4.7484 -3.1947 -1.4627 0.3672 2.2149 4.0000 5.6422 7.0613 8.1770 8.9090 9.1770 8.9008 8.0000 6.4543 4.4827 2.3642 0.3778 -1.1977 -2.0833 -2.0000 -0.8092 1.0661 3.0629 4.6179 5.1678 4.1496 1.0000 -4.6313 -12.2428 -21.1202 -30.5489 -39.8148 -48.2032 -55.0000 -59.4906 -60.9607 -58.6960 -51.9819 -40.1041 -22.3483 2.0000", block.OutputNodes[0].Object.ToString(4));

            var block2 = (InterpolationBlock)block.Clone();
            signalBlock.Text = "1 2 3 4";
            block.ConnectTo(block2);
            block.Factor = 2;
            block2.Factor = 2;
            signalBlock.Execute();
            Assert.AreEqual("1.0 1.5 2.0 2.5 3.0 3.5 4.0", block.OutputNodes[0].Object.ToString(1));
            Assert.AreEqual("1.0 1.3 1.5 1.8 2.0 2.3 2.5 2.8 3.0 3.3 3.5 3.8 4.0", block2.OutputNodes[0].Object.ToString(1));

            block.Cascade = false;
            block2 = (InterpolationBlock)block.Clone();
            block.ConnectTo(block2);
            signalBlock.Execute();
            Assert.AreEqual("", block2.OutputNodes[0].Object.ToString(0, " "));
            Assert.AreEqual(0, block2.OutputNodes[0].Object.Count);
        }

        [TestMethod]
        public void TestInterpolationDocumentationSample()
        {
            //Creates a sample signal with 4 samples
            var signalBlock = new ImportFromTextBlock {Text="14, 20, 11, 41"};

            //Interpolation block to create 2 samples between each other, 
            //using Linear Interpolation method
            var block = new InterpolationBlock{Factor=3, Mode=InterpolationModeEnum.Linear};
            
            //Connect and execute blocks
            signalBlock.ConnectTo(block);
            signalBlock.Execute();

            Console.WriteLine(block.OutputNodes[0].Object[0].ToString(0));

            //Output: 14 16 18 20 17 14 11 21 31 41

            Assert.AreEqual("14 16 18 20 17 14 11 21 31 41", block.OutputNodes[0].Object[0].ToString(0));
        }
    }
}
