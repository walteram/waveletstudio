// Code to implement decently performing FFT for complex and real valued
// signals. See www.lomont.org for a derivation of the relevant algorithms 
// from first principles. Copyright Chris Lomont 2010. 
// This code and any ports are free for all to use for any reason as long 
// as this header is left in place.
using System;
using System.Collections.Generic;
using WaveletStudio.Functions;

namespace WaveletStudio.FFT
{
    /// <summary>
    /// Represent a class that performs real or complex valued Fast Fourier 
    /// Transforms. Instantiate it and use the FFT or TableFFT methods to 
    /// compute complex to complex FFTs. Use FFTReal for real to complex 
    /// FFTs which are much faster than standard FFTs.
    /// </summary>
    public static class ManagedFFT
    {
        /// <summary>
        /// Compute the forward or inverse Fourier Transform of data using the specified mode
        /// </summary>
        /// <param name="data">The complex data stored as alternating real and imaginary parts</param>
        /// <param name="forward">true for a forward transform, false for inverse transform</param>
        /// <param name="mode">Mode to be used</param>
        public static void FFT(ref double[] data, bool forward, ManagedFFTModeEnum mode)
        {
            if(mode == ManagedFFTModeEnum.DynamicTrigonometricValues)
                DynamicFFT(ref data, forward);
            else
                TableFFT(ref data, forward);
        }

        /// <summary>
        /// Compute the forward or inverse Fourier Transform of data, with 
        /// data containing complex valued data as alternating real and 
        /// imaginary parts. The length must be a power of 2.
        /// </summary>
        /// <param name="data">The complex data stored as alternating real 
        /// and imaginary parts</param>
        /// <param name="forward">true for a forward transform, false for 
        /// inverse transform</param>
        public static void DynamicFFT(ref double[] data, bool forward)
        {
            var n = data.Length;
            // checks n is a power of 2 in 2's complement format
            if ((n & (n - 1)) != 0)
            {
                data = data.SubArray(SignalExtension.NextPowerOf2(n));
                n = data.Length;
            }            
            n /= 2;    // n is the number of samples

            Reverse(ref data, n); // bit index data reversal

            // do transform: so single point transforms, then doubles, etc.
            double sign = forward ? 1 : -1;
            var mmax = 1;
            while (n > mmax)
            {
                var istep = 2 * mmax;
                var theta = sign * Math.PI / mmax;
                double wr = 1, wi = 0;
                var wpr = Math.Cos(theta);
                var wpi = Math.Sin(theta);
                for (var m = 0; m < istep; m += 2)
                {
                    for (var k = m; k < 2 * n; k += 2 * istep)
                    {
                        var j = k + istep;
                        var tempr = wr * data[j] - wi * data[j + 1];
                        var tempi = wi * data[j] + wr * data[j + 1];
                        data[j] = data[k] - tempr;
                        data[j + 1] = data[k + 1] - tempi;
                        data[k] = data[k] + tempr;
                        data[k + 1] = data[k + 1] + tempi;
                    }
                    var t = wr; // trig recurrence
                    wr = wr * wpr - wi * wpi;
                    wi = wi * wpr + t * wpi;
                }
                mmax = istep;
            }

            // inverse scaling in the backward case
            if (forward) 
                return;
            var scale = 1.0 / n;
            for (var i = 0; i < 2 * n; ++i)
                data[i] *= scale;
        }


        /// <summary>
        /// Compute the forward or inverse Fourier Transform of data, with data
        /// containing complex valued data as alternating real and imaginary 
        /// parts. The length must be a power of 2. This method caches values 
        /// and should be slightly faster on repeated uses than then FFT method. 
        /// It is also slightly more accurate.
        /// </summary>
        /// <param name="data">The complex data stored as alternating real 
        /// and imaginary parts</param>
        /// <param name="forward">true for a forward transform, false for 
        /// inverse transform</param>
        public static void TableFFT(ref double[] data, bool forward)
        {
            var n = data.Length;
            // checks n is a power of 2 in 2's complement format
            if ((n & (n - 1)) != 0)
            {
                data = data.SubArray(SignalExtension.NextPowerOf2(n));
                n = data.Length;
            }            
            n /= 2;    // n is the number of samples

            Reverse(ref data, n); // bit index data reversal

            // make table if needed
            if (CosTable.Count != n)
                Initialize(n);

            // do transform: so single point transforms, then doubles, etc.
            double sign = forward ? 1 : -1;
            var mmax = 1;
            var tptr = 0;
            while (n > mmax)
            {
                var istep = 2 * mmax;
                for (var m = 0; m < istep; m += 2)
                {
                    var wr = CosTable[tptr];
                    var wi = sign * SinTable[tptr++];
                    for (var k = m; k < 2 * n; k += 2 * istep)
                    {
                        var j = k + istep;
                        var tempr = wr * data[j] - wi * data[j + 1];
                        var tempi = wi * data[j] + wr * data[j + 1];
                        data[j] = data[k] - tempr;
                        data[j + 1] = data[k + 1] - tempi;
                        data[k] = data[k] + tempr;
                        data[k + 1] = data[k + 1] + tempi;
                    }
                }
                mmax = istep;
            }

            // copy out with optional scaling
            if (forward) 
                return;

            var scale = 1.0 / n;
            for (var i = 0; i < 2 * n; ++i)
                data[i] *= scale;
        }

        /// <summary>
        /// Compute the forward or inverse Fourier Transform of data, with 
        /// data containing real valued data only. The output is complex 
        /// valued after the first two entries, stored in alternating real 
        /// and imaginary parts. The first two returned entries are the real 
        /// parts of the first and last value from the conjugate symmetric 
        /// output, which are necessarily real. The length must be a power 
        /// of 2.
        /// </summary>
        /// <param name="data">The complex data stored as alternating real 
        /// and imaginary parts</param>
        /// <param name="forward">true for a forward transform, false for 
        /// inverse transform</param>
        public static void RealFFT(ref double[] data, bool forward)
        {
            var n = data.Length; // # of real inputs, 1/2 the complex length
            // checks n is a power of 2 in 2's complement format
            if ((n & (n - 1)) != 0)
            {
                data = data.SubArray(SignalExtension.NextPowerOf2(n));
                n = data.Length;
            }

            double sign = -1;
            if (forward)
            { 
                // do packed FFT. This can be changed to FFT to save memory
                TableFFT(ref data, true);
                sign = 1;
            }

            var theta = sign * 2 * Math.PI / n;
            var wpr = Math.Cos(theta);
            var wpi = Math.Sin(theta);
            var wjr = wpr;
            var wji = wpi;
            for (var j = 1; j <= n / 4; ++j)
            {
                var k = n / 2 - j;
                var tnr = data[2 * k];
                var tni = data[2 * k + 1];
                var tjr = data[2 * j];
                var tji = data[2 * j + 1];

                var e = (tjr + tnr);
                var f = (tji - tni);
                var a = (tjr - tnr) * wji;
                var d = (tji + tni) * wji;
                var b = (tji + tni) * wjr;
                var c = (tjr - tnr) * wjr;

                // compute entry y[j]
                data[2 * j] = 0.5 * (e + sign * (a + b));
                data[2 * j + 1] = 0.5 * (f - sign * (c - d));

                // compute entry y[k]
                data[2 * k] = 0.5 * (e - sign * (a + b));
                data[2 * k + 1] = 0.5 * (sign * (-c + d) - f);

                var temp = wjr;
                // todo - allow more accurate version here? make option?
                wjr = wjr * wpr - wji * wpi;
                wji = temp * wpi + wji * wpr;
            }

            if (forward)
            {
                // compute final y0 and y_{N/2}, store data[0], data[1]
                var temp = data[0];
                data[0] += data[1];
                data[1] = temp - data[1];
            }
            else
            {
                var temp = data[0]; // unpack the y[j], then invert FFT
                data[0] = 0.5 * (temp + data[1]);
                data[1] = 0.5 * (temp - data[1]);
                // do packed FFT. This can be changed to FFT to save memory
                TableFFT(ref data, false);
            }
        }

        /// <summary>
        /// Call this with the size before using the TableFFT version
        /// Fills in tables for speed. Done automatically in TableFFT
        /// </summary>
        /// <param name="size">The size of the FFT in samples</param>
        public static void Initialize(int size)
        {
            lock (CosTable)
            lock (SinTable)
            {
                CosTable.Clear();
                SinTable.Clear();

                // forward pass
                var n = size;
                var mmax = 1;
                while (n > mmax)
                {
                    var istep = 2 * mmax;
                    var theta = Math.PI / mmax;
                    double wr = 1, wi = 0;
                    var wpi = Math.Sin(theta);
                    // compute in a slightly slower yet more accurate manner
                    var wpr = Math.Sin(theta / 2);
                    wpr = -2 * wpr * wpr;
                    for (var m = 0; m < istep; m += 2)
                    {
                        CosTable.Add(wr);
                        SinTable.Add(wi);
                        var t = wr;
                        wr = wr * wpr - wi * wpi + wr;
                        wi = wi * wpr + t * wpi + wi;
                    }
                    mmax = istep;
                }
            }                       
        }

        /// <summary>
        /// Swap data indices whenever index i has binary 
        /// digits reversed from index j, where data is
        /// two doubles per index.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="n"></param>
        private static void Reverse(ref double[] data, int n)
        {
            // bit reverse the indices. This is exercise 5 in section 
            // 7.2.1.1 of Knuth's TAOCP the idea is a binary counter 
            // in k and one with bits reversed in j
            int j = 0, k = 0; // Knuth R1: initialize
            var top = n / 2;  // this is Knuth's 2^(n-1)
            while (true)
            {
                // Knuth R2: swap - swap j+1 and k+2^(n-1), 2 entries each
                var t = data[j + 2];
                data[j + 2] = data[k + n];
                data[k + n] = t;
                t = data[j + 3];
                data[j + 3] = data[k + n + 1];
                data[k + n + 1] = t;
                if (j > k)
                { // swap two more
                    // j and k
                    t = data[j];
                    data[j] = data[k];
                    data[k] = t;
                    t = data[j + 1];
                    data[j + 1] = data[k + 1];
                    data[k + 1] = t;
                    // j + top + 1 and k+top + 1
                    t = data[j + n + 2];
                    data[j + n + 2] = data[k + n + 2];
                    data[k + n + 2] = t;
                    t = data[j + n + 3];
                    data[j + n + 3] = data[k + n + 3];
                    data[k + n + 3] = t;
                }
                // Knuth R3: advance k
                k += 4;
                if (k >= n)
                    break;
                // Knuth R4: advance j
                var h = top;
                while (j >= h)
                {
                    j -= h;
                    h /= 2;
                }
                j += h;
            } // bit reverse loop
        }

        /// <summary>
        /// Precomputed sin/cos tables for speed
        /// </summary>
        private static readonly List<double> CosTable = new List<double>();

        private static readonly List<double> SinTable = new List<double>();
    }
}
