using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class ConvolutionBlockTest
    {
        [TestMethod]
        public void TestConvolutionBlockExecute()
        {
            var signalBlock1 = new GenerateSignalBlock { Offset = 1.2, TemplateName = "Binary", Start = 0, Finish = 5, SamplingRate = 1, IgnoreLastSample = true };
            var signalBlock2 = new GenerateSignalBlock { Offset = 2.3, TemplateName = "Binary", Start = 0, Finish = 5, SamplingRate = 1, IgnoreLastSample = true };
            var convolutionBlock = new ConvolutionBlock {ReturnOnlyValid = true};
            convolutionBlock.Execute();

            signalBlock1.OutputNodes[0].ConnectTo(convolutionBlock.InputNodes[0]);
            signalBlock2.OutputNodes[0].ConnectTo(convolutionBlock.InputNodes[1]);
            Assert.IsNotNull(convolutionBlock.Name);
            Assert.IsNotNull(convolutionBlock.Description);
            Assert.IsNotNull(convolutionBlock.ProcessingType);

            signalBlock1.Execute();
            Assert.IsNull(convolutionBlock.OutputNodes[0].Object[0].Samples);

            signalBlock2.Execute();
            Assert.IsNotNull(convolutionBlock.OutputNodes[0].Object);
            Assert.AreEqual("22.80", convolutionBlock.OutputNodes[0].Object.ToString(2)); 

            convolutionBlock.ReturnOnlyValid = false;
            signalBlock1.Execute();
            Assert.AreEqual("2.76 9.02 12.78 18.04 22.80 18.04 12.78 9.02 2.76", convolutionBlock.OutputNodes[0].Object.ToString(2));

            var convolutionBlock2 = (ConvolutionBlock)convolutionBlock.Clone();
            convolutionBlock.OutputNodes[0].ConnectTo(convolutionBlock2.InputNodes[0]);
            signalBlock2.OutputNodes[0].ConnectTo(convolutionBlock2.InputNodes[1]);
            signalBlock2.Execute();
            Assert.AreEqual("6.348 29.854 65.508 113.520 177.480 221.144 230.292 221.144 177.480 113.520 65.508 29.854 6.348", convolutionBlock2.OutputNodes[0].Object.ToString(3));

            convolutionBlock2 = (ConvolutionBlock)convolutionBlock.Clone();
            convolutionBlock.Cascade = false;
            convolutionBlock.OutputNodes[0].ConnectTo(convolutionBlock2.InputNodes[0]);
            convolutionBlock.Execute();
            Assert.AreEqual(0, convolutionBlock2.OutputNodes[0].Object.Count);

            convolutionBlock.Cascade = true;
            convolutionBlock.OutputNodes[0].ConnectTo(convolutionBlock2.InputNodes[0]);
            signalBlock2.OutputNodes[0].ConnectTo(convolutionBlock2.InputNodes[1]);
            convolutionBlock.Execute();
            Assert.IsNotNull(convolutionBlock2.OutputNodes[0].Object);

            convolutionBlock.Cascade = true;
            convolutionBlock2.OutputNodes[0].Object = null;
            convolutionBlock.OutputNodes[0].ConnectingNode = null;
            signalBlock2.OutputNodes[0].ConnectingNode = null;
            convolutionBlock.Execute();
            Assert.IsNull(convolutionBlock2.OutputNodes[0].Object);
        }

        [TestMethod]        
        public void CompareConvolveMethods()
        {
            const int signalLength = 32000;
            const int filterLength = 128;

            //Creates signal and filter by sine wave
            var signalBlock = new GenerateSignalBlock { TemplateName = "Sine", Start = 0, Finish = 1, SamplingRate = signalLength, IgnoreLastSample = true };
            signalBlock.Execute();
            var signal = signalBlock.OutputNodes[0].Object[0].Samples;
            var filter = new double[filterLength];
            Array.Copy(signal, filter, filterLength);

            //Run Normal convolution and show time
            var stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();
            var convolvedNormal = WaveMath.ConvolveNormal(signal, filter);
            stopWatch.Stop();
            Console.WriteLine("Normal Convolve {0} samples - {1} ms", convolvedNormal.Length, stopWatch.ElapsedMilliseconds);

            //Run ManagedFFT convolution and show time
            WaveMath.ConvolveManagedFFT(signal, filter);
            stopWatch.Reset();
            stopWatch.Start();
            var convolvedFFT = WaveMath.ConvolveManagedFFT(signal, filter);
            stopWatch.Stop();
            Console.WriteLine("FFT Convolve {0} samples - {1} ms", convolvedFFT.Length, stopWatch.ElapsedMilliseconds);

        }
    }
}
