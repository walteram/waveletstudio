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

using System.Collections.Generic;
using WaveletStudio.Functions;

namespace WaveletStudio.Wavelet
{
    /// <summary>
    /// Used as the return of a DWT operation and as the input of a IDWT operation
    /// </summary>
    public class DecompositionLevel
    {
        /// <summary>
        /// Level index in DWT
        /// </summary>
        public int Index { get; set; }
        
        /// <summary>
        /// Approximation coefficients
        /// </summary>
        public double[] Approximation { get; set; }

        /// <summary>
        /// Detais coefficients
        /// </summary>
        public double[] Details { get; set; }

        /// <summary>
        /// Signal from which this level was created
        /// </summary>
        public Signal Signal { get; set; }

        /// <summary>
        /// Single disturbance identified in a signal
        /// </summary>
        public class Disturbance
        {
            /// <summary>
            /// Start of the disturbance in the details level (index of the sample)
            /// </summary>
            public int Start;

            /// <summary>
            /// Finish of the disturbance in the details level (index of the sample)
            /// </summary>
            public int Finish;

            /// <summary>
            /// Start of the disturbance in the signal (index of the sample)
            /// </summary>
            public int SignalStart;

            /// <summary>
            /// Finish of the disturbance in the signal (index of the sample)
            /// </summary>
            public int SignalFinish;

            internal Disturbance(int start, int finish, int detailsLength, int signalLength)
            {
                Start = start;
                Finish = finish;
                
                SignalStart = (int)WaveMath.Scale(start, 0, detailsLength, 0, signalLength) + 1;
                SignalFinish = (int)WaveMath.Scale(finish, 0, detailsLength, 0, signalLength) + 1;

                if (start % 2 != 0 && finish % 2 != 0)
                {
                    SignalFinish--;
                }

            }
        }

        /// <summary>
        /// Estimates the disturbances in the signal based on the normal distribution of the details coefficients
        /// </summary>
        /// <param name="threshold">The higher the threshold, the higher the tolerance in flutuations on energy of the details</param>
        /// <param name="minimunDistance">Minimun distance between disturbances to consider a new one</param>
        /// <returns></returns>
        public List<Disturbance> GetDisturbances(double threshold = 0.1, int minimunDistance = 3)
        {
            threshold = 1 - threshold;
            var disturbances = new List<Disturbance>();
            var mean = WaveMath.Mean(Details);
            var deviation = WaveMath.StandardDeviation(Details);
            var samples = new List<KeyValuePair<int, double>>();
            var min = double.MaxValue;
            var max = double.MinValue;
            for (var i = 0; i < Details.Length; i++)
            {
                var sample = Details[i];
                var norm = WaveMath.ProbabilityDensityFunction(sample, mean, deviation);
                if (double.IsNaN(norm))
                    continue;
                if (norm < min)                
                    min = norm;                
                if (norm > max)                
                    max = norm;                
                samples.Add(new KeyValuePair<int, double>(i, norm));
            }
            //ajusta a escala da distribuição normal para 0..1, removendo os valores maiores que threshold
            for (var i = samples.Count - 1; i >= 0; i--)
            {
                var scaledNorm = WaveMath.Scale(samples[i].Value, min, max, 0, 1);
                samples[i] = new KeyValuePair<int, double>(i, scaledNorm);
                if (scaledNorm > threshold)
                    samples.RemoveAt(i);
                else
                    samples[i] = new KeyValuePair<int, double>(i, scaledNorm);
            }
            int? start = null;
            var startIndex = 0;
            for (var i = 0; i < samples.Count; i++)
            {
                if (start == null)
                {
                    start = i;
                    startIndex = samples[i].Key;
                }
                else if (samples[i].Key - samples[i - 1].Key >= minimunDistance || i == samples.Count - 1)
                {
                    disturbances.Add(new Disturbance(startIndex, samples[i - 1].Key, Details.Length, Signal.Samples.Length));
                    if (i < samples.Count - 1)
                    {
                        start = i;
                        startIndex = samples[i].Key;
                    }
                    else
                    {
                        disturbances.Add(new Disturbance(samples[i].Key, samples[i].Key, Details.Length, Signal.Samples.Length));
                    }
                }
            }
            if (disturbances.Count > 1 && (disturbances[disturbances.Count - 1].Finish - disturbances[disturbances.Count - 2].Finish) < minimunDistance)
            {
                disturbances[disturbances.Count - 2] = new Disturbance(disturbances[disturbances.Count - 2].Start, disturbances[disturbances.Count - 1].Finish, Details.Length, Signal.Samples.Length);
                disturbances.RemoveAt(disturbances.Count - 1);
            }
            return disturbances;
        }
    }
}
