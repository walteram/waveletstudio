using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILNumerics;

namespace WaveletStudio.WaveLib
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
        public ILArray<double> Approximation { get; set; }

        /// <summary>
        /// Detais coefficients
        /// </summary>
        public ILArray<double> Details { get; set; }

        /// <summary>
        /// Signal from which this level was created
        /// </summary>
        public Signal Signal { get; set; }

        /// <summary>
        /// TODO: In progress.
        /// </summary>
        public class Disturbance
        {
            public int Start;
            public int Finish;

            public int SignalStart;
            public int SignalFinish;

            internal Disturbance(int start, int finish, int detailsLength, int signalLength)
            {
                Start = start;
                Finish = finish;
                
                SignalStart = (int)WaveLibMath.Scale(start, 0, detailsLength, 0, signalLength);
                SignalFinish = (int)WaveLibMath.Scale(finish, 0, detailsLength, 0, signalLength);
                
            }
        }

        /// <summary>
        /// Gets the disturbances in the signal based on the normal density distribution of the details coefficients
        /// </summary>
        /// <param name="threshold">The higher the threshold, the higher the tolerance in flutuations on energy of the details</param>
        /// <param name="minimunDistance">Minimun distance between disturbances to consider a new one</param>
        /// <returns></returns>
        public List<Disturbance> GetDisturbances(double threshold = 0.1, int minimunDistance = 3)
        {
            threshold = 1 - threshold;
            var disturbances = new List<Disturbance>();
            var mean = ILNumerics.BuiltInFunctions.ILMath.mean(Details).GetValue(0);
            var deviation = WaveLibMath.StandardDeviation(Details.Values.ToArray());
            var samples = new List<Sample>();
            var min = double.MaxValue;
            var max = double.MinValue;
            for (var i = 0; i < Details.Length; i++)
            {
                var sample = Details.GetValue(i);
                var norm = WaveLibMath.NormalDistribution(sample, mean, deviation);
                if (norm < min)                
                    min = norm;                
                if (norm > max)                
                    max = norm;                
                samples.Add(new Sample { Index = i, Value = sample, NormalValue = norm });
            }
            //ajusta a escala da distribuição normal para 0..1, removendo os valores maiores que threshold
            for (var i = samples.Count - 1; i >= 0; i--)
            {
                samples[i].NormalValue = WaveLibMath.Scale(samples[i].NormalValue, min, max, 0, 1);
                if (samples[i].NormalValue > threshold)
                    samples.RemoveAt(i);   
            }
            int? start = null;
            var startIndex = 0;
            for (var i = 0; i < samples.Count; i++)
            {
                if (start == null)
                {
                    start = i;
                    startIndex = samples[i].Index;
                }
                else if (samples[i].Index - samples[i - 1].Index >= minimunDistance || i == samples.Count - 1)
                {
                    disturbances.Add(new Disturbance(startIndex, samples[i - 1].Index, Details.Length, Signal.Samples.Length));
                    if (i < samples.Count - 1)
                    {
                        start = i;
                        startIndex = samples[i].Index;
                    }
                    else
                    {
                        disturbances.Add(new Disturbance(samples[i].Index, samples[i].Index, Details.Length, Signal.Samples.Length));
                    }
                }
            }
            if (disturbances.Count > 1 && disturbances[disturbances.Count - 1].Finish - disturbances[disturbances.Count - 2].Finish < minimunDistance)
            {
                disturbances[disturbances.Count - 2].Finish = disturbances[disturbances.Count - 1].Finish;
                disturbances.RemoveAt(disturbances.Count - 1);
            }
            return disturbances;
        }

        private class Sample
        {
            public int Index { get; set; }
            public double Value { get; set; }
            public double NormalValue { get; set; }
        }
    }
}
