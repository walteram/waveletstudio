/***********************************************************************************************
 COPYRIGHT 2008 Vijeth D

 This file is part of Function Approximation NeuronDotNet Sample.
 (Project Website : http://neurondotnet.freehostia.com)

 NeuronDotNet is a free software. You can redistribute it and/or modify it under the terms of
 the GNU General Public License as published by the Free Software Foundation, either version 3
 of the License, or (at your option) any later version.

 NeuronDotNet is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 See the GNU General Public License for more details.

 You should have received a copy of the GNU General Public License along with NeuronDotNet.
 If not, see <http://www.gnu.org/licenses/>.

 ***********************************************************************************************/

using System;
using System.Drawing;
using System.Windows.Forms;
using NeuronDotNet.Core;
using NeuronDotNet.Core.Backpropagation;
using NeuronDotNet.Core.Initializers;
using ZedGraph;

namespace NeuronDotNet.Samples.FunctionApproximation
{
    public partial class MainForm : Form
    {
        private static readonly Color enabledColor = Color.Tomato;
        private static readonly Color disabledColor = Color.Goldenrod;

        private LineItem curve = new LineItem("",
            new double[] { 1, 2, 3, 4, 5, 6 },
            new double[] { 0.01, 0.03, 0.07, 0.15, 0.31, 0.63},
            enabledColor,
            SymbolType.Diamond);

        private double learningRate = 0.3d;
        private int neuronCount = 10;
        private int cycles = 10000;
        private BackpropagationNetwork network;

        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadForm(object sender, EventArgs e)
        {
            GraphPane pane = functionGraph.GraphPane;
            pane.Chart.Fill = new Fill(Color.AntiqueWhite, Color.Honeydew, -45F);
            pane.Title.IsVisible = false;
            pane.YAxis.IsVisible = false;
            pane.YAxis.Scale.Max = 1d;
            pane.YAxis.Scale.Min = 0d;

            pane.Legend.IsVisible = false;

            pane.XAxis.Title.Text = "Drag the points to position them and define the function";
            pane.XAxis.Title.FontSpec.Size = 15;
            pane.XAxis.MajorGrid.IsVisible = true;
            pane.XAxis.Scale.IsVisible = false;

            curve.Line.IsVisible = false;
            curve.Symbol.Fill.Type = FillType.Solid;
            functionGraph.ContextMenuBuilder += new ZedGraphControl.ContextMenuBuilderEventHandler(
                delegate(ZedGraphControl graph, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
                {
                    for (int i = 0; i < menuStrip.Items.Count; i++)
                    {
                        string tag = (string)menuStrip.Items[i].Tag;
                        if (tag == "undo_all" || tag == "unzoom" || tag == "set_default")
                        {
                            menuStrip.Items.RemoveAt(i);
                            i--;
                        }
                    }
                });

            functionGraph.IsEnableVEdit = true;
            functionGraph.EditModifierKeys = Keys.None;
            functionGraph.EditButtons = MouseButtons.Left;
            functionGraph.IsEnableZoom = false;
            functionGraph.IsEnableSelection = false;
            functionGraph.IsEnableHPan = false;
            functionGraph.IsEnableVPan = false;
            functionGraph.GraphPane.CurveList.Capacity = 2;
            OnPointEdit(functionGraph, pane, curve, 0);
            pane.AxisChange();
            EnableControls(true);
            txtCycles.Text = cycles.ToString();
            txtLearningRate.Text = learningRate.ToString();
            txtNeuronCount.Text = neuronCount.ToString();
        }

        private string OnPointEdit(ZedGraphControl sender, GraphPane pane, CurveItem curve, int iPt)
        {
            listData.Items.Clear();
            for (int i = 0; i < this.curve.Points.Count; i++)
            {
                listData.Items.Add((i + 1).ToString());
                this.curve.Points[i].Y = Math.Max(0.01, Math.Min(0.99, this.curve.Points[i].Y));
                listData.Items[i].SubItems.Add(this.curve.Points[i].Y.ToString("0.00"));
            }
            CleanseGraph();
            return null;
        }

        private void EnableControls(bool enabled)
        {
            btnStart.Enabled = enabled;
            btnStop.Enabled = !enabled;
            txtCycles.Enabled = enabled;
            txtLearningRate.Enabled = enabled;
            txtNeuronCount.Enabled = enabled;
            trainingProgressBar.Value = 0;

            functionGraph.IsEnableVEdit = enabled;
            curve.Color = enabled ? enabledColor : disabledColor;
        }

        private void CleanseGraph()
        {
            functionGraph.GraphPane.CurveList.Clear();
            functionGraph.GraphPane.CurveList.Add(curve);
            functionGraph.Invalidate();
        }

        private void Start(object sender, EventArgs e)
        {
            CleanseGraph();
            EnableControls(false);
            curve.Color = enabledColor;

            if (!int.TryParse(txtCycles.Text, out cycles)) { cycles = 10000; }
            if (!double.TryParse(txtLearningRate.Text, out learningRate)) { learningRate = 0.25d; }
            if (!int.TryParse(txtNeuronCount.Text, out neuronCount)) { neuronCount = 10; }

            if (cycles <= 0) { cycles = 10000; }
            if (learningRate < 0 || learningRate > 1) { learningRate = 0.25d; }
            if (neuronCount <= 0) { neuronCount = 10; }

            txtCycles.Text = cycles.ToString();
            txtLearningRate.Text = learningRate.ToString(); 
            txtNeuronCount.Text = neuronCount.ToString();

            LinearLayer inputLayer = new LinearLayer(1);
            SigmoidLayer hiddenLayer = new SigmoidLayer(neuronCount);
            SigmoidLayer outputLayer = new SigmoidLayer(1);
            new BackpropagationConnector(inputLayer, hiddenLayer).Initializer = new RandomFunction(0d, 0.3d);
            new BackpropagationConnector(hiddenLayer, outputLayer).Initializer = new RandomFunction(0d, 0.3d);
            network = new BackpropagationNetwork(inputLayer, outputLayer);
            network.SetLearningRate(learningRate);

            TrainingSet trainingSet = new TrainingSet(1, 1);
            for (int i = 0; i < curve.Points.Count; i++)
            {
                double xVal = curve.Points[i].X;
                for (double input = xVal - 0.05; input < xVal + 0.06; input += 0.01)
                {
                    trainingSet.Add(new TrainingSample(new double[] { input }, new double[] { curve.Points[i].Y}));
                }
            }

            network.EndEpochEvent += new TrainingEpochEventHandler(
                delegate(object senderNetwork, TrainingEpochEventArgs args)
                {
                    trainingProgressBar.Value = (int)(args.TrainingIteration * 100d / cycles);
                    Application.DoEvents();
                });
            network.Learn(trainingSet, cycles);
            StopLearning(this, EventArgs.Empty);
        }

        private void StopLearning(object sender, EventArgs e)
        {
            if (network != null)
            {
                network.StopLearning();
                LineItem lineItem = new LineItem("Approximated Function");
                for (double xVal = 0; xVal < 10; xVal += 0.05d)
                {
                    lineItem.AddPoint(xVal, network.Run(new double[] { xVal })[0]);
                }
                lineItem.Symbol.Type = SymbolType.None;
                lineItem.Color = Color.DarkOrchid;
                functionGraph.GraphPane.CurveList.Add(lineItem);
                functionGraph.Refresh();
                functionGraph.GraphPane.CurveList.Remove(lineItem);
            }
            network = null;
            EnableControls(true);
        }

        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            StopLearning(this, EventArgs.Empty);
        }
    }
}
