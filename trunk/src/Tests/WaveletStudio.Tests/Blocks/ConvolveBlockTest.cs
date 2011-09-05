using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;

namespace WaveletStudio.Tests.Blocks
{
    [TestClass]
    [DeploymentItem("libfftw3-3.dll")]
    [DeploymentItem("libfftw3f-3.dll")]
    public class ConvolveBlockTest
    {
        [TestMethod]        
        public void CompareConvolveMethods()
        {
            const int signalLength = 32000;
            const int filterLength = 128;

            var signalBlock = new GenerateSignalBlock { TemplateName = "Sine", Start = 0, Finish = 1, SamplingRate = signalLength, IgnoreLastSample = true };
            signalBlock.Execute();
            var samples = ((Signal) signalBlock.OutputNodes[0].Object).Samples;
            var filter = ((Signal)signalBlock.OutputNodes[0].Object).Samples["1:1:" + filterLength];
            Console.WriteLine(samples.Length);

            var stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();
            var convolvedNormal = WaveMath.ConvolveNormal(samples, filter);
            stopWatch.Stop();
            Console.WriteLine("Normal Convolve {0} samples - {1} ms", convolvedNormal.Length, stopWatch.ElapsedMilliseconds);

            WaveMath.ConvolveFft(samples, filter);
            stopWatch.Reset();
            stopWatch.Start();
            var convolvedFft = WaveMath.ConvolveFft(samples, filter);
            stopWatch.Stop();
            Console.WriteLine("FFT Convolve {0} samples - {1} ms", convolvedFft.Length, stopWatch.ElapsedMilliseconds);

        }
    }
}
