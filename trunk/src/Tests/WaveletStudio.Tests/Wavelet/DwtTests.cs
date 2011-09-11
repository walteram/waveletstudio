using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Functions;
using WaveletStudio.Wavelet;

namespace WaveletStudio.Tests.Wavelet
{
    [TestClass]
    public class DwtTests
    {        
        [TestMethod]
        public void TestDwt()
        {
            var signal = new Signal(5, 6, 7, 8, 1, 2, 3, 4);
            var wavelet = MotherWavelet.LoadFromName("haar");
            var output = Dwt.ExecuteDwt(signal, wavelet, 1);
            var expectedApproximation = new[] { 7.7781745930520172, 10.606601717798206, 2.1213203435596411, 4.9497474683058291 };
            var expectedDetails = new[] { -0.707106781186547, -0.707106781186547, -0.707106781186547, -0.707106781186547 };

            foreach (var level in output)
            {
                Console.WriteLine(level.Approximation.ToString());
                Console.WriteLine(level.Details.ToString());
            }

            Assert.IsTrue(TestUtils.SequenceEquals(output[0].Approximation, expectedApproximation));
            Assert.IsTrue(TestUtils.SequenceEquals(output[0].Details, expectedDetails));
        }

        [TestMethod]
        public void TestDwtTwoLevelsZeroPadding()
        {
            var signal = new Signal(new double[] { 5, 6, 7, 8, 1, 2, 3, 4 }, 1);
            var wavelet = MotherWavelet.LoadFromName("db4");
            var output = Dwt.ExecuteDwt(signal, wavelet, 2, SignalExtension.ExtensionMode.ZeroPadding);

            var expectedA1 = new[] { 0.10083065061313592, -0.60472387194825949, 1.9356768454450775, 9.564171098531471, 7.4685915826435121, 3.4407781861706259, 3.5505197189413709 };
            var expectedA2 = new[] { 0.0097241173965646223, -0.075213839644193592, 0.22259435703234348, -0.62953234901526167, 11.234286021898358, 6.4201808643834335, 0.81796094874626823 };
            var expectedD1 = new[] { 2.1919660244229693, -0.76429990403022541, -3.712425832764648, 1.3951662234727382, -2.53980930134257, 0.76429994405246893, -0.16332425479813378 };
            var expectedD2 = new[] { 0.21139340497972126, -0.44097376814889744, -1.6517956385039554, 2.0257051634726118, 0.439518138572961, -0.082787884460846284, -0.0376262841822663 };

            Assert.IsTrue(TestUtils.SequenceEquals(output[0].Approximation, expectedA1));
            Assert.IsTrue(TestUtils.SequenceEquals(output[0].Details, expectedD1));
            Assert.IsTrue(TestUtils.SequenceEquals(output[1].Approximation, expectedA2));
            Assert.IsTrue(TestUtils.SequenceEquals(output[1].Details, expectedD2));          
        }

        [TestMethod]
        public void TestDwtTwoLevelsPeriodicPadding()
        {
            var signal = new Signal(new double[] { 5, 6, 7, 8, 1, 2, 3, 4 }, 1);
            var wavelet = MotherWavelet.LoadFromName("db4");
            var output = Dwt.ExecuteDwt(signal, wavelet, 2, SignalExtension.ExtensionMode.PeriodicPadding); //ppd

            var expectedA1 = new[] { 7.5694222332566481, 2.8360543142223662, 5.4861965643864492, 9.564171098531471, 7.5694222332566481, 2.8360543142223662, 5.4861965643864492 };
            var expectedA2 = new[] { 10.254795056347346, 8.000805703744021, 8.7589053589221, 5.7365738310078322, 12.03931261636461, 6.8935291108651908, 6.79029828331375 };
            var expectedD1 = new[] { -0.34784327691960071, 0.000000040022243075199526, -3.8757500875627819, 1.3951662234727382, -0.34784327691960071, 0.000000040022243075199526, -3.8757500875627819 };
            var expectedD2 = new[] { 2.7173723736570405, 0.48069973385706888, -0.87632966992803074, 1.9614893811889251, -3.7742274826859972, 2.9640617322585006, -3.4730659923310214 };

            Assert.IsTrue(TestUtils.SequenceEquals(output[0].Approximation, expectedA1));
            Assert.IsTrue(TestUtils.SequenceEquals(output[0].Details, expectedD1));
            Assert.IsTrue(TestUtils.SequenceEquals(output[1].Approximation, expectedA2));
            Assert.IsTrue(TestUtils.SequenceEquals(output[1].Details, expectedD2));
        }

        [TestMethod]
        public void TestIDwtWithNormalConvolution()
        {
            var points = new[] { 5, 6, 7, 8, 1, 2, 3, 4, 2.444, 1.1234 };
            var signal = new Signal(points, 1);
            var wavelet = MotherWavelet.LoadFromName("haar");
            var levels = Dwt.ExecuteDwt(signal, wavelet, 2, SignalExtension.ExtensionMode.SymmetricWholePoint, ConvolutionModeEnum.Normal);
            var output = Dwt.ExecuteIDwt(levels, wavelet);
            Assert.IsTrue(TestUtils.SequenceEquals(output, signal.Samples));

            levels = Dwt.ExecuteDwt(signal, wavelet, 3, SignalExtension.ExtensionMode.SymmetricWholePoint, ConvolutionModeEnum.Normal);
            output = Dwt.ExecuteIDwt(levels, wavelet, 10);
            Assert.IsTrue(TestUtils.SequenceEquals(output, signal.Samples));
        }

        [TestMethod]
        public void TestIDwtWithManagedFft()
        {
            var points = new[] { 5, 6, 7, 8, 1, 2, 3, 4, 2.444, 1.1234 };
            var signal = new Signal(points, 1);
            var wavelet = MotherWavelet.LoadFromName("haar");
            var levels = Dwt.ExecuteDwt(signal, wavelet, 2, SignalExtension.ExtensionMode.SymmetricWholePoint);
            var output = Dwt.ExecuteIDwt(levels, wavelet);
            Assert.IsTrue(TestUtils.SequenceEquals(output, signal.Samples));

            levels = Dwt.ExecuteDwt(signal, wavelet, 3, SignalExtension.ExtensionMode.SymmetricWholePoint);
            output = Dwt.ExecuteIDwt(levels, wavelet, 10);
            Assert.IsTrue(TestUtils.SequenceEquals(output, signal.Samples));
        }
    }
}
