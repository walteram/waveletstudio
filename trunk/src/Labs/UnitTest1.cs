using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuronDotNet.Core;
using NeuronDotNet.Core.Backpropagation;

namespace Labs
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestNet()
        {
            const int inputSize = 3;
            const int outputSize = 8;

            var inputLayer = new LinearLayer(inputSize);
            var hiddenLayer = new SigmoidLayer(10);
            var outputLayer = new SigmoidLayer(outputSize);
            new BackpropagationConnector(inputLayer, hiddenLayer);
            new BackpropagationConnector(hiddenLayer, outputLayer);

            var trainingSet = new TrainingSet(inputSize, outputSize);
            trainingSet.Add(new TrainingSample(new double[] { 5, 2, 7 }, new double[] { 0, 1, 1, 0, 0, 0, 1, 0 }));
            trainingSet.Add(new TrainingSample(new double[] { 2, 9, 11 }, new double[] { 0, 1, 1, 0, 0, 0, 1, 1 }));
            trainingSet.Add(new TrainingSample(new double[] { 1, 2, 3 }, new double[] { 0, 1, 1, 0, 0, 0, 0, 0 }));
            trainingSet.Add(new TrainingSample(new double[] { 2, 3, 4 }, new double[] { 0, 0, 0, 1, 1, 0, 0, 0 }));
            trainingSet.Add(new TrainingSample(new double[] { 4, 5, 6 }, new double[] { 0, 0, 0, 1, 1, 1, 1, 0 }));


            var network = new BackpropagationNetwork(inputLayer, outputLayer);
            network.SetLearningRate(0.25);
            network.Learn(trainingSet, 30000);

            var testResult = network.Run(new double[] { 8, 7, 3 });
            Assert.AreEqual(198, testResult[0]);
        }
    }
}
