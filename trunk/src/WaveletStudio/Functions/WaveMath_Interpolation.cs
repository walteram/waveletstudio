/*  Wavelet Studio Signal Processing Library - www.waveletstudio.net
    Copyright (C) 2011, 2012 Walter V. S. de Amorim - The Wavelet Studio Initiative

    Wavelet Studio is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Wavelet Studio is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

using System;

namespace WaveletStudio.Functions
{
    public static partial class WaveMath
    {
        /// <summary>
        /// Performs an interpolation in the specified signal
        /// </summary>
        /// <param name="signal"></param>
        /// <param name="factor"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static Signal Interpolate(Signal signal, uint factor, InterpolationModeEnum mode)
        {
            if (mode == InterpolationModeEnum.Linear)
                return InterpolateLinear(signal, factor);
            if (mode == InterpolationModeEnum.Nearest)
                return InterpolateNearest(signal, factor);
            if (mode == InterpolationModeEnum.Polynomial)
                return InterpolatePolynomial(signal, factor);
            if (mode == InterpolationModeEnum.NewtonForm)
                return InterpolateNewtonForm(signal, factor);
            return InterpolateCubic(signal, factor);
        }

        /// <summary>
        /// Linear interpolation. Factor must be >= 2
        /// </summary>
        /// <returns></returns>
        public static Signal InterpolateLinear(Signal signal, uint factor)
        {
            if (factor < 2)
                return signal.Clone();

            var newSignal = signal.Copy();
            var newSamples = MemoryPool.Pool.New<double>(Convert.ToInt32(signal.Samples.Length*factor-factor+1));
            var currentIndex = 0;
            var newInterval = signal.SamplingInterval/factor;
            var currentX = signal.Start;
            var newX = currentX;
            for (var i = 0; i < signal.SamplesCount; i++)
            {
                if (i == signal.SamplesCount - 1)
                {
                    newSamples[currentIndex] = signal.Samples[i];
                    break;
                }
                var y0 = signal.Samples[i];
                var y1 = signal.Samples[i+1];
                var x0 = currentX;
                var x1 = currentX + signal.SamplingInterval;
                var div = ((y1 - y0)/(x1 - x0));
                for (var j = 0; j < factor; j++)
                {
                    newSamples[currentIndex] = y0 + (newX - x0)*div;
                    currentIndex++;
                    newX += newInterval;                    
                }
                currentX = x1;
            }
            newSignal.Samples = newSamples;
            newSignal.SamplingInterval = newInterval;            
            return newSignal;
        }

        /// <summary>
        /// Nearest interpolation. Factor must be >= 2
        /// </summary>
        /// <returns></returns>
        public static Signal InterpolateNearest(Signal signal, uint factor)
        {
            if (factor < 2)
                return signal.Clone();

            var newSignal = signal.Copy();
            var newSamples = MemoryPool.Pool.New<double>(Convert.ToInt32(signal.Samples.Length * factor - factor + 1));
            var currentIndex = 0;
            var newInterval = signal.SamplingInterval / factor;
            var currentX = signal.Start;
            var newX = currentX;
            for (var i = 0; i < signal.SamplesCount; i++)
            {
                if (i == signal.SamplesCount - 1)
                {
                    newSamples[currentIndex] = signal.Samples[i];
                    break;
                }
                var y0 = signal.Samples[i];
                var y1 = signal.Samples[i + 1];
                var x0 = currentX;
                var x1 = currentX + signal.SamplingInterval;
                for (var j = 0; j < factor; j++)
                {
                    if(newX - x0 < x1 - newX)
                        newSamples[currentIndex] = y0;
                    else
                        newSamples[currentIndex] = y1;
                    currentIndex++;
                    newX += newInterval;
                }
                currentX = x1;
            }
            newSignal.Samples = newSamples;
            newSignal.SamplingInterval = newInterval;
            return newSignal;
        }

        /// <summary>
        /// Cubic interpolation. Factor must be >= 2
        /// </summary>
        /// <returns></returns>
        public static Signal InterpolateCubic(Signal signal, uint factor)
        {
            if (factor < 2)
                return signal.Clone();

            var n = signal.Samples.Length;
            var newSignal = signal.Copy();
            var newSamples = MemoryPool.Pool.New<double>(Convert.ToInt32(signal.Samples.Length * factor - factor + 1));
            var time = signal.GetTimeSeries();
            var b = MemoryPool.Pool.New<double>(n);
            var c = MemoryPool.Pool.New<double>(n);
            var d = MemoryPool.Pool.New<double>(n);
            CubicNak(n, time, signal.Samples, ref b, ref c, ref d);

            var newInterval = Convert.ToDecimal(signal.SamplingInterval / factor);
            var currentX = Convert.ToDecimal(signal.Start);
            for (var i = 0; i < newSamples.Length; i++)
            {
                newSamples[i] = SplineEval(n, time, signal.Samples, b, c, d, Convert.ToDouble(currentX));
                currentX += newInterval;                
            }
            newSignal.Samples = newSamples;
            newSignal.SamplingInterval = Convert.ToDouble(newInterval);

            MemoryPool.Pool.RegisterObject(b);
            MemoryPool.Pool.RegisterObject(c);
            MemoryPool.Pool.RegisterObject(d);

            return newSignal;
        }

        /// <summary>
        /// Interpolation from the Neville's Algorthm. Factor must be >= 2
        /// </summary>
        /// <returns></returns>
        public static Signal InterpolatePolynomial(Signal signal, uint factor)
        {
            if (factor < 2)
                return signal.Clone();

            var n = signal.Samples.Length;
            var newSignal = signal.Copy();
            var newSamples = MemoryPool.Pool.New<double>(Convert.ToInt32(signal.Samples.Length * factor - factor + 1));
            var time = signal.GetTimeSeries();
            
            var newInterval = Convert.ToDecimal(signal.SamplingInterval / factor);
            var currentX = Convert.ToDecimal(signal.Start);
            for (var i = 0; i < newSamples.Length; i++)
            {
                newSamples[i] = Neville(n, time, signal.Samples, Convert.ToDouble(currentX));
                currentX += newInterval;
            }
            newSignal.Samples = newSamples;
            newSignal.SamplingInterval = Convert.ToDouble(newInterval);

            return newSignal;
        }

        /// <summary>
        /// Interpolation by the Newton Interpolation Polynomial. Factor must be >= 2
        /// </summary>
        /// <returns></returns>
        public static Signal InterpolateNewtonForm(Signal signal, uint factor)
        {
            if (factor < 2)
                return signal.Clone();

            var n = signal.Samples.Length;
            var newSignal = signal.Copy();
            var newSamples = MemoryPool.Pool.New<double>(Convert.ToInt32(signal.Samples.Length * factor - factor + 1));
            var time = signal.GetTimeSeries();            
            var coeffs = NewtonDivDiff(n, time, signal.Samples);

            var newInterval = Convert.ToDecimal(signal.SamplingInterval / factor);
            var currentX = Convert.ToDecimal(signal.Start);
            for (var i = 0; i < newSamples.Length; i++)
            {
                newSamples[i] = NewtonEval(n, time, coeffs, Convert.ToDouble(currentX));
                currentX += newInterval;
            }
            newSignal.Samples = newSamples;
            newSignal.SamplingInterval = Convert.ToDouble(newInterval);

            return newSignal;
        }

        /// <summary>
        /// Evaluate the polynomial which interpolates a given set of data at a single value of the independent variable using the Neville's algorithm. Based on work by Brian Bradie at tiny.cc/6nbwf
        /// </summary>
        /// <param name="n">number of interpolating points</param>
        /// <param name="x">array containing interpolating points</param>
        /// <param name="y">array containing function values to be interpolated</param>
        /// <param name="t">value of independent variable at which the interpolating polynomial is to be evaluated</param>
        /// <returns>y	value of interpolating polynomial defined by the data in the arrays x and y at the specified value of the independent variable</returns>
        private static double Neville(int n, double[] x, double[] y, double t)
        {
            int i, j;
            var f = MemoryPool.Pool.New<double>(n);
            for (i = 0; i < n; i++)
                f[i] = y[i];

            for (j = 1; j < n; j++)
                for (i = n - 1; i >= j; i--)
                {
                    f[i] = ((t - x[i - j]) * f[i] - (t - x[i]) * f[i - 1]) / (x[i] - x[i - j]);
                }

            var fxbar = f[n - 1];
            MemoryPool.Pool.RegisterObject(f);
            return (fxbar);
        }

        /// <summary>
        /// Compute the coefficients of the Newton form of the polynomial which interpolates a given set of data. Based on work by Brian Bradie at tiny.cc/6nbwf       
        /// </summary>
        /// <param name="n">number of interpolating points</param>
        /// <param name="x">array containing interpolating points</param>
        /// <param name="y">array containing function values to be interpolated</param>
        /// <returns></returns>
        private static double[] NewtonDivDiff(int n, double[] x, double[] y)
        {
            int i, j;

            var f = MemoryPool.Pool.New<double>(n);
            for (i = 0; i < n; i++)
                f[i] = y[i];

            for (j = 1; j < n; j++)
                for (i = n - 1; i >= j; i--)
                {
                    f[i] = (f[i] - f[i - 1]) / (x[i] - x[i - j]);
                }

            return (f);
        }

        /// <summary>
        /// Evaluate the Newton form of the polynomial which interpolates a given set of data at a single value of the independent variable given the coefficients of the Newton form (obtained from 'NewtonDivDiff'). Based on work by Brian Bradie at tiny.cc/6nbwf
        /// </summary>
        /// <param name="n">number of interpolating points</param>
        /// <param name="x">array containing interpolating points</param>
        /// <param name="nf">array containing the coefficients of the Newton form of the interpolating polynomial</param>
        /// <param name="t">value of independent variable at which the interpolating polynomial is to be evaluated</param>
        /// <returns></returns>
        private static double NewtonEval(int n, double[] x, double[] nf, double t)
        {
            int j;

            var temp = nf[n - 1];
            for (j = n - 2; j >= 0; j--)
                temp = temp * (t - x[j]) + nf[j];

            return (temp);
        }

        /// <summary>
        /// Used in interpolation functions
        /// </summary>
        /// <param name="n"></param>
        /// <param name="c"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="r"></param>
        private static void Tridiagonal(int n, double[] c, ref double[] a, ref double[] b, ref double[] r)
        {
            int i;

            for (i = 0; i < n - 1; i++)
            {
                b[i] /= a[i];
                a[i + 1] -= c[i] * b[i];
            }

            r[0] /= a[0];
            for (i = 1; i < n; i++)
                r[i] = (r[i] - c[i - 1] * r[i - 1]) / a[i];

            for (i = n - 2; i >= 0; i--)
                r[i] -= r[i + 1] * b[i];
        }

        /// <summary> 
        /// Determine the coefficients for the 'not-a-knot' cubic spline for a given set of data
        /// </summary>
        /// <param name="n">number of interpolating points</param>
        /// <param name="x">array containing interpolating points</param>
        /// <param name="f">array containing function values to be interpolated</param>
        /// <param name="b">array of size at least n</param>
        /// <param name="c">array of size at least n</param>
        /// <param name="d">array of size at least n</param>
        private static void CubicNak(int n, double[] x, double[] f, ref double[] b, ref double[] c, ref double[] d)
        {
            int i;
            var h = MemoryPool.Pool.New<double>(n);
            var dl = MemoryPool.Pool.New<double>(n);
            var dd = MemoryPool.Pool.New<double>(n);
            var du = MemoryPool.Pool.New<double>(n);

            for (i = 0; i < n - 1; i++)
                h[i] = x[i + 1] - x[i];
            for (i = 0; i < n - 3; i++)
                dl[i] = du[i] = h[i + 1];

            for (i = 0; i < n - 2; i++)
            {
                dd[i] = 2.0 * (h[i] + h[i + 1]);
                c[i] = (3.0 / h[i + 1]) * (f[i + 2] - f[i + 1]) -
                       (3.0 / h[i]) * (f[i + 1] - f[i]);
            }
            dd[0] += (h[0] + h[0] * h[0] / h[1]);
            dd[n - 3] += (h[n - 2] + h[n - 2] * h[n - 2] / h[n - 3]);
            du[0] -= (h[0] * h[0] / h[1]);
            dl[n - 4] -= (h[n - 2] * h[n - 2] / h[n - 3]);

            Tridiagonal(n - 2, dl, ref dd, ref du, ref c);

            for (i = n - 3; i >= 0; i--)
                c[i + 1] = c[i];
            c[0] = (1.0 + h[0] / h[1]) * c[1] - h[0] / h[1] * c[2];
            c[n - 1] = (1.0 + h[n - 2] / h[n - 3]) * c[n - 2] - h[n - 2] / h[n - 3] * c[n - 3];
            for (i = 0; i < n - 1; i++)
            {
                d[i] = (c[i + 1] - c[i]) / (3.0 * h[i]);
                b[i] = (f[i + 1] - f[i]) / h[i] - h[i] * (c[i + 1] + 2.0 * c[i]) / 3.0;
            }

            MemoryPool.Pool.RegisterObject(h);
            MemoryPool.Pool.RegisterObject(du);
            MemoryPool.Pool.RegisterObject(dd);
            MemoryPool.Pool.RegisterObject(dl);
        }

        /// <summary>
        /// Evaluate a cubic spline at a single value of the independent variable given the coefficients of the cubic spline interpolant (obtained from 'CubicNak' or 'CubicClamped')
        /// </summary>
        /// <param name="n">number of interpolating points</param>
        /// <param name="x">array containing interpolating points</param>
        /// <param name="f">array containing the constant terms from the cubic spline</param>
        /// <param name="b">array containing the coefficients of the linear terms from the cubic spline</param>
        /// <param name="c">array containing the coefficients of the quadratic terms from the cubic spline</param>
        /// <param name="d">array containing the coefficients of the cubic terms from the cubic spline</param>
        /// <param name="t">value of independent variable at which the interpolating polynomial is to be evaluated</param>
        /// <returns></returns>
        private static double SplineEval(int n, double[] x, double[] f, double[] b, double[] c, double[] d, double t)
        {
            var i = 1;
            var found = false;
            while (!found && (i < n - 1))
            {
                if (t < x[i])
                    found = true;
                else
                    i++;
            }
            t = f[i - 1] + (t - x[i - 1]) * (b[i - 1] + (t - x[i - 1]) * (c[i - 1] + (t - x[i - 1]) * d[i - 1]));
            return (t);
        }
    }

    /// <summary>
    /// Interpolation mode
    /// </summary>
    public enum InterpolationModeEnum
    {
        /// <summary>
        /// Linear interpolation
        /// </summary>
        Linear,
        /// <summary>
        /// Nearest neighbour interpolation
        /// </summary>
        Nearest,
        /// <summary>
        /// Cubic interpolation (spline)
        /// </summary>
        Cubic,
        /// <summary>
        /// Polynomial interpolation using the Neville's Algorithm
        /// </summary>
        Polynomial,
        /// <summary>
        /// Newton form of polynomial interpolation
        /// </summary>
        NewtonForm
    }
}