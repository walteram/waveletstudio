using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuronDotNet.Core;
using NeuronDotNet.Core.Backpropagation;

namespace WaveletStudio.Tests
{
    [TestClass]
    public class LabTests
    {
        private BackpropagationNetwork _xorNetwork;
        private double[] _errorList;
        private const int cycles = 20000;
        private const int neuronCount = 5;
        private const double learningRate = 0.25d;
        private double _max;

        [TestMethod]
        public void LabTest1()
        {
            var inputLayer = new LinearLayer(5);
            var hiddenLayer = new TanhLayer(neuronCount);
            var outputLayer = new TanhLayer(2);
            new BackpropagationConnector(inputLayer, hiddenLayer);
            new BackpropagationConnector(hiddenLayer, outputLayer);
            _xorNetwork = new BackpropagationNetwork(inputLayer, outputLayer);
            _xorNetwork.SetLearningRate(learningRate);

            var trainingSet = new TrainingSet(5, 2);
            trainingSet.Add(new TrainingSample(new double[] { 0, 0, 0, 0, 0 }, new double[] { 0, 0}));
            trainingSet.Add(new TrainingSample(new double[] { 0, 0, 0, 1, 0 }, new double[] { 3, 3 }));
            trainingSet.Add(new TrainingSample(new double[] { 0, 0, 1, 0, 0 }, new double[] { 2, 2 }));
            trainingSet.Add(new TrainingSample(new double[] { 0, 0, 1, 1, 0 }, new double[] { 2, 3 }));
            trainingSet.Add(new TrainingSample(new double[] { 0, 1, 0, 0, 0 }, new double[] { 1, 1 }));
            trainingSet.Add(new TrainingSample(new double[] { 0, 1, 0, 1, 0 }, new double[] { 1, 3 }));
            trainingSet.Add(new TrainingSample(new double[] { 0, 1, 1, 0, 0 }, new double[] { 1, 2 }));
            trainingSet.Add(new TrainingSample(new double[] { 0, 1, 1, 1, 0 }, new double[] { 1, 3 }));
            trainingSet.Add(new TrainingSample(new double[] { 22, 1, 1, 1, 22 }, new double[] { 1, 3 }));

            _errorList = new double[cycles];

            //_xorNetwork.EndEpochEvent += EndEpochEvent;
            _xorNetwork.Learn(trainingSet, cycles);

            var result = _xorNetwork.Run(new double[] { 0, 0, 1, 1, 0 });
            }

        private void EndEpochEvent(object network, TrainingEpochEventArgs args)
        {
            _errorList[args.TrainingIteration] = _xorNetwork.MeanSquaredError;
            _max = Math.Max(_max, _xorNetwork.MeanSquaredError);
        }
    }
}
