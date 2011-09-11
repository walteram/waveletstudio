using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.FFT;

namespace WaveletStudio.Tests.FFT
{
    [TestClass]
    public class ManagedFFTTests
    {
        [TestMethod]
        public void TestManagedFFT()
        {
            // some tests of various lengths
            double[] t4 = { 1, 1, 1, 1 }; // input
            double[] a4R = { 4, 0, 0, 0 }; // real FFT
            double[] a4C = { 2, 2, 0, 0 };    // complex FFT, ...
            double[] t4A = { 1, 2, 3, 4 };
            double[] a4Ar = { 10, -2, -2, -2 };
            double[] a4Ac = { 4, 6, -2, -2 };
            double[] t8 = { 0.100652, -0.442825, -0.457954, -0.00624455, 0.19978, -0.267328, -0.47192, -0.235878 };
            double[] a8R = { -1.58172, 0.322834, -0.385598, 0.0522465, 1.23031, -0.468031, 0.187343, 0.0243147 };
            double[] a8C = { -0.629442, -0.952276, -0.328761, -0.161531, 1.23031, -0.46803, 0.130505, -0.189463 };
            double[] t32 = { -0.333615, 0.468917, 0.884538, 0.0276625, 0.979812, 0.91061, -0.175599, 0.1756, -0.695263, 0.557298, 0.112251, -0.285586, -0.73988, -0.0750604, -0.332421, 0.391004, 0.0588164, -0.18941, -0.416513, -0.596507, 0.659257, -0.654753, -0.472673, 0.875249, -0.00712734, -0.12367, -0.357211, -0.152413, 0.0130609, -0.0342799, 0.818388, 0.671986 };
            double[] a32R = { 1.96247, -1.97083, 4.71435, 1.34203, 1.41278, 2.2209, -0.301542, 1.30462, 0.717877, -1.42063, -3.19595, -1.52441, -0.474644, -2.90705, 0.747585, 2.44391, -0.125698, -0.247344, -4.4128, -1.07521, -1.28254, 2.42047, -1.30217, -0.450559, -4.49676, -2.19137, 0.193633, 0.848902, 2.05478, -1.91513, 0.417439, 1.79843 };
            double[] a32C = { -0.00417904, 1.96665, 1.44498, 2.18532, 1.46969, 1.82996, -1.0868, 0.620212, 1.23124, 0.951988, -2.48776, -1.88406, -0.412289, -2.73394, 0.564497, 2.93413, -0.125699, -0.247344, -4.22971, -0.584989, -1.3449, 2.59358, -2.01036, -0.810202, -5.01012, 0.181248, 0.97889, 0.164497, 1.99787, -2.30607, 3.68681, 2.64171 };

            double[][] tests = { t4, a4R, a4C, t4A, a4Ar, a4Ac, t8, a8R, a8C, t32, a32R, a32C };//, t32, a32 };

            var ret = true;
            for (var testIndex = 0; testIndex < tests.Length; testIndex += 3)
            {
                var test = tests[testIndex];
                var answerReal = tests[testIndex + 1];
                var answerComplex = tests[testIndex + 2];

                ret &= Test(ManagedFFT.RealFFT, test, answerReal);
                ret &= Test(ManagedFFT.FFT, test, answerComplex);
                ret &= Test(ManagedFFT.TableFFT, test, answerComplex);
            }
            Assert.IsTrue(ret);
        }

        /// <summary>
        /// Test the given function on the given data and see if the result is the given answer.
        /// </summary>
        /// <returns>true if matches</returns>
        static bool Test(Action<double[], bool> fftFunction, double[] test, double[] answer)
        {
            var returnValue = true;
            var copy = test.ToArray(); // make a copy
            fftFunction(copy, true); // forward transform
            returnValue &= Compare(copy, answer); // check it
            fftFunction(copy, false); // backward transform
            returnValue &= Compare(copy, test); // check it
            return returnValue;
        }

        /// <summary>
        /// Compare two arrays of doubles for "equality"
        /// up to a small tolerance
        /// </summary>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        /// <returns></returns>
        static bool Compare(double[] arr1, double[] arr2)
        {
            if (arr1.Length != arr2.Length)
                return false;
            for (var i = 0; i < arr1.Length; ++i)
                if ((Math.Abs(arr1[i] - arr2[i]) > 0.0001))
                    return false;
            return true;
        }
    }
}
