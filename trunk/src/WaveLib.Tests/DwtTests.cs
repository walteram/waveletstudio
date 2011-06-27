using System;
using System.Collections.Generic;
using System.Linq;
using ILNumerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WaveLib.Tests
{
    [TestClass]
    public class DwtTests
    {
        
        [TestMethod]
        public void TestDwt()
        {
            var signal = new Signal(new double[] {5, 6, 7, 8, 1, 2, 3, 4,}, 1);
            var wavelet = MotherWavelet.LoadFromName("haar");
            var output = Dwt.ExecuteDwt(signal, wavelet, 1, SignalExtension.ExtensionMode.SymmetricHalfPoint);
            var expectedApproximation = new ILArray<double>(new[] { 7.7781745930520172, 10.606601717798206, 2.1213203435596411, 4.9497474683058291 });
            var expectedDetails = new ILArray<double>(new[] { -0.707106781186547, -0.707106781186547, -0.707106781186547, -0.707106781186547 });

            Assert.IsTrue(SequenceEquals(output[0].Approximation, expectedApproximation));
            Assert.IsTrue(SequenceEquals(output[0].Details, expectedDetails));
        }

        [TestMethod]
        public void TestDwtTwoLevelsZeroPadding()
        {
            var signal = new Signal(new double[] { 5, 6, 7, 8, 1, 2, 3, 4 }, 1);
            var wavelet = MotherWavelet.LoadFromName("db4");
            var output = Dwt.ExecuteDwt(signal, wavelet, 2, SignalExtension.ExtensionMode.ZeroPadding);

            var expectedA1 = new ILArray<double>(new[] { 0.10083065061313592, -0.60472387194825949, 1.9356768454450775, 9.564171098531471, 7.4685915826435121, 3.4407781861706259, 3.5505197189413709 });
            var expectedA2 = new ILArray<double>(new[] { 0.0097241173965646223, -0.075213839644193592, 0.22259435703234348, -0.62953234901526167, 11.234286021898358, 6.4201808643834335, 0.81796094874626823 });
            var expectedD1 = new ILArray<double>(new[] { 2.1919660244229693, -0.76429990403022541, -3.712425832764648, 1.3951662234727382, -2.53980930134257, 0.76429994405246893, -0.16332425479813378 });
            var expectedD2 = new ILArray<double>(new[] { 0.21139340497972126, -0.44097376814889744, -1.6517956385039554, 2.0257051634726118, 0.439518138572961, -0.082787884460846284, -0.0376262841822663 });

            Assert.IsTrue(SequenceEquals(output[0].Approximation, expectedA1));
            Assert.IsTrue(SequenceEquals(output[0].Details, expectedD1));
            Assert.IsTrue(SequenceEquals(output[1].Approximation, expectedA2));
            Assert.IsTrue(SequenceEquals(output[1].Details, expectedD2));          
        }

        [TestMethod]
        public void TestDwtTwoLevelsPeriodicPadding()
        {
            var signal = new Signal(new double[] { 5, 6, 7, 8, 1, 2, 3, 4 }, 1);
            var wavelet = MotherWavelet.LoadFromName("db4");
            var output = Dwt.ExecuteDwt(signal, wavelet, 2, SignalExtension.ExtensionMode.PeriodicPadding); //ppd

            var expectedA1 = new ILArray<double>(new[] { 7.5694222332566481, 2.8360543142223662, 5.4861965643864492, 9.564171098531471, 7.5694222332566481, 2.8360543142223662, 5.4861965643864492 });
            var expectedA2 = new ILArray<double>(new[] { 10.254795056347346, 8.000805703744021, 8.7589053589221, 5.7365738310078322, 12.03931261636461, 6.8935291108651908, 6.79029828331375 });
            var expectedD1 = new ILArray<double>(new[] { -0.34784327691960071, 0.000000040022243075199526, -3.8757500875627819, 1.3951662234727382, -0.34784327691960071, 0.000000040022243075199526, -3.8757500875627819 });
            var expectedD2 = new ILArray<double>(new[] { 2.7173723736570405, 0.48069973385706888, -0.87632966992803074, 1.9614893811889251, -3.7742274826859972, 2.9640617322585006, -3.4730659923310214 });

            Assert.IsTrue(SequenceEquals(output[0].Approximation, expectedA1));
            Assert.IsTrue(SequenceEquals(output[0].Details, expectedD1));
            Assert.IsTrue(SequenceEquals(output[1].Approximation, expectedA2));
            Assert.IsTrue(SequenceEquals(output[1].Details, expectedD2));
        }

        [TestMethod]
        public void TestIDwt()
        {
            var points = new double[] { 5, 6, 7, 8, 1, 2, 3, 4, 1.1, 2.4444451 };
            var signal = new Signal(points, 1);
            var wavelet = MotherWavelet.LoadFromName("haar");
            var levels = Dwt.ExecuteDwt(signal, wavelet, 2);
            var output = Dwt.ExecuteIDwt(levels, wavelet);                        
            Assert.IsTrue(SequenceEquals(output, signal.Points));

            levels = Dwt.ExecuteDwt(signal, wavelet, 3, SignalExtension.ExtensionMode.SymmetricWholePoint);
            output = Dwt.ExecuteIDwt(levels, wavelet, 10);
            Assert.IsTrue(SequenceEquals(output, signal.Points));
        }

        private static bool SequenceEquals(ILArray<double> double1, ILArray<double> double2)
        {
            for (var i = 0; i < double1.Count(); i++)
            {
                if (!AlmostEquals(double1.GetValue(i), double2.GetValue(i), 0.0000001))
                    return false;
            }
            return true;
        }
        
        private static bool AlmostEquals(double double1, double double2, double precision)
        {
            return (Math.Abs(double1 - double2) <= precision);
        }


        [TestMethod]
        public void TestConvolve()
        {
            var signal = new ILArray<double>(new double[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            var filter = new ILArray<double>(new double[] {1, 2, 3});
            var convolved = Dwt.Convolve(signal, filter);
            var expected = new ILArray<double>(new double[] { 10, 16, 22, 28, 34, 40 });
            Assert.IsTrue(convolved.SequenceEqual(expected));

            signal = new ILArray<double>(new double[] { 1, 2, 3});
            filter = new ILArray<double>(new double[] { 1, 2, 3, 4, 5 });
            convolved = Dwt.Convolve(signal, filter);
            expected = new ILArray<double>(new double[] { 10, 16, 22 });
            Assert.IsTrue(convolved.SequenceEqual(expected));

            signal = new ILArray<double>(new double[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            filter = new ILArray<double>(new double[] { 1, 2, 3, 4 });
            convolved = Dwt.Convolve(signal, filter, false);
            expected = new ILArray<double>(new double[] { 1, 4, 10, 20, 30, 40, 50, 60, 61, 52, 32  });
            Assert.IsTrue(convolved.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestDownsample()
        {
            var input = new ILArray<double>(new double[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            var downSampled = Dwt.DownSample(input);
            var expected = new ILArray<double>(new double[] { 2, 4, 6, 8 });
            Assert.IsTrue(downSampled.SequenceEqual(expected));

            input = new ILArray<double>(new double[] { 1, 2, 3 });
            downSampled = Dwt.DownSample(input);
            expected = new ILArray<double>(new double[] { 2 });
            Assert.IsTrue(downSampled.SequenceEqual(expected));
            
            input = new ILArray<double>(new double[] { 1 });
            downSampled = Dwt.DownSample(input);
            expected = new ILArray<double>(new double[] { });
            Assert.IsTrue(downSampled.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestUpsample()
        {
            var input = new ILArray<double>(new double[] { 1 });
            var upSampled = Dwt.UpSample(input);
            var expected = new ILArray<double>(new double[] { 1 });
            Assert.IsTrue(upSampled.SequenceEqual(expected));

            input = new ILArray<double>(new double[] { 1, 2 });
            upSampled = Dwt.UpSample(input);
            expected = new ILArray<double>(new double[] { 1, 0, 2 });
            Assert.IsTrue(upSampled.SequenceEqual(expected));

            input = new ILArray<double>(new double[] { 1, 2, 3, 4, 5 });
            upSampled = Dwt.UpSample(input);
            expected = new ILArray<double>(new double[] { 1, 0, 2, 0, 3, 0, 4, 0, 5 });
            Assert.IsTrue(upSampled.SequenceEqual(expected));

            input = new ILArray<double>(new double[] { });
            upSampled = Dwt.UpSample(input);
            expected = new ILArray<double>(new double[] { });
            Assert.IsTrue(upSampled.SequenceEqual(expected));
        }
    }
}
