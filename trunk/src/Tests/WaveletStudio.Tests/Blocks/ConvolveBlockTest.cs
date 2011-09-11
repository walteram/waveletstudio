using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Functions;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    public class ConvolveBlockTest
    {
        [TestMethod]        
        public void CompareConvolveMethods()
        {
            const int signalLength = 32000;
            const int filterLength = 128;

            //Creates signal and filter by sine wave
            var signalBlock = new GenerateSignalBlock { TemplateName = "Sine", Start = 0, Finish = 1, SamplingRate = signalLength, IgnoreLastSample = true };
            signalBlock.Execute();
            var signal = signalBlock.OutputNodes[0].Object.Samples;
            var filter = new double[filterLength];
            Array.Copy(signal, filter, filterLength);

            //Run Normal convolution and show time
            var stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();
            var convolvedNormal = WaveMath.ConvolveNormal(signal, filter);
            stopWatch.Stop();
            Console.WriteLine("Normal Convolve {0} samples - {1} ms", convolvedNormal.Length, stopWatch.ElapsedMilliseconds);

            //Run ManagedFFT convolution and show time
            WaveMath.ConvolveManagedFft(signal, filter);
            stopWatch.Reset();
            stopWatch.Start();
            var convolvedFft = WaveMath.ConvolveManagedFft(signal, filter);
            stopWatch.Stop();
            Console.WriteLine("FFT Convolve {0} samples - {1} ms", convolvedFft.Length, stopWatch.ElapsedMilliseconds);

        }
    }
}
