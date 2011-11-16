using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WaveletStudio.Blocks;

namespace WaveletStudio.MainApplication.Forms
{
    public partial class BlockPlot : UserControl
    {
        public BlockBase Block { get; set; }

        public BlockPlot()
        {
            InitializeComponent();
            ApplicationUtils.ConfigureGraph(GraphControl, "Output");
        }

        public BlockPlot(ref BlockBase block) 
        {
            InitializeComponent();
            ApplicationUtils.ConfigureGraph(GraphControl, block.Name);
            Block = block;
        }

        public new void Refresh()
        {
            UpdateGraph();
            LoadBlockOutputs();
            UpdateSignalList();
            base.Refresh();
        }

        private void LoadBlockOutputs()
        {
            ShowOutputList.Items.Clear();
            if (Block != null)
            {
                foreach (var output in Block.OutputNodes)
                {
                    if (output.Name != "All")
                        ShowOutputList.Items.Add(output.Name);
                }
            }            
            if (ShowOutputList.Items.Count > 0)
                ShowOutputList.SelectedIndex = 0;
            if (ShowOutputList.Items.Count > 1)
            {
                ShowOutputList.Visible = true;
                ShowOutputLabel.Visible = true;
            }
            else
            {
                ShowOutputList.Visible = false;
                ShowOutputLabel.Visible = false;
                ShowOutputSignal.Visible = false;
            }
        }

        private void UpdateGraph()
        {
            var pane = GraphControl.GraphPane;
            if (pane.CurveList.Count > 0)
                pane.CurveList.Clear();
            if (Block == null)
                return;
            Block.Execute();
            var outputNode = Block.OutputNodes.FirstOrDefault(it => it.Name == ShowOutputList.Text);
            if (outputNode == null || outputNode.Object == null || outputNode.Object.Count == 0)
            {
                NoDataLabel.Visible = true;
                GraphControl.Invalidate();
                GraphControl.Refresh();
                return;
            }
            NoDataLabel.Visible = false;
            var index = ShowOutputSignal.SelectedIndex;
            if (index == -1 || index > outputNode.Object.Count - 1)
                index = 0;
            var signal = outputNode.Object[index];
            var samples = signal.GetSamplesPair().ToList();

            var yAxys = new ZedGraph.PointPairList();
            yAxys.AddRange(samples.Select(it => new ZedGraph.PointPair(it[1], it[0])));
            pane.AddCurve(outputNode.Name, yAxys, Color.Red, ZedGraph.SymbolType.None);

            if (signal.CustomPlot != null && signal.CustomPlot.Length > 0)
            {
                if (signal.CustomPlot.Length == 2)
                {
                    var minValue = signal.Samples.Min() * 1.1;
                    var maxValue = signal.Samples.Max() * 1.1;

                    var area = new ZedGraph.PointPairList{
                                                            {signal.CustomPlot[0], minValue}, {signal.CustomPlot[0], maxValue},
                                                            {signal.CustomPlot[0], maxValue}, {signal.CustomPlot[1], maxValue},
                                                            {signal.CustomPlot[1], maxValue}, {signal.CustomPlot[1], minValue}, 
                                                            {signal.CustomPlot[1], minValue}, {signal.CustomPlot[0], minValue}
                                                         };
                    pane.AddCurve("Previous Size", area, Color.Orange, ZedGraph.SymbolType.None);
                }
            }

            pane.Legend.IsVisible = false;
            pane.Title.Text = ApplicationUtils.GetResourceString(outputNode.Name);
            pane.XAxis.Title.IsVisible = false;
            pane.YAxis.Title.IsVisible = false;
            if (!pane.IsZoomed && samples.Count() != 0)
            {
                pane.XAxis.Scale.Min = samples.ElementAt(0)[1];
                pane.XAxis.Scale.Max = samples.ElementAt(samples.Count() - 1)[1];
            }
            GraphControl.AxisChange();
            GraphControl.Invalidate();
            GraphControl.Refresh();
        }

        private void GraphControlMouseDoubleClick(object sender, MouseEventArgs e)
        {
            GraphControl.ZoomOutAll(GraphControl.GraphPane);
        }

        private void UpdateSignalList()
        {
            var currentIndex = ShowOutputSignal.SelectedIndex;
            ShowOutputSignal.Items.Clear();
            if (Block == null)
            {
                ShowOutputSignal.Visible = false;
                return;
            }
            var outputNode = Block.OutputNodes.FirstOrDefault(it => it.Name == ShowOutputList.Text);
            if (outputNode == null || outputNode.Object == null || outputNode.Object.Count == 0)
            {
                ShowOutputSignal.Visible = false;
            }
            else
            {
                foreach (var signal in outputNode.Object)
                {
                    ShowOutputSignal.Items.Add(string.IsNullOrEmpty(signal.Name) ? "Signal" : signal.Name);
                }
                if (ShowOutputSignal.Items.Count > 0)
                    ShowOutputSignal.SelectedIndex = ShowOutputSignal.Items.Count > currentIndex ? currentIndex : 0;
                ShowOutputSignal.Visible = true;
            }
        }

        private void ShowOutputListSelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGraph();
            UpdateSignalList();
        }

        private void ShowOutputSignalSelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }
    }
}
