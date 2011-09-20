using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WaveletStudio.Blocks;

namespace WaveletStudio.MainApplication.Forms
{
    public partial class BlockSetupForm : BlockSetupBaseForm
    {
        public BlockSetupForm(string title, ref BlockBase block) : base(title, ref block)
        {
            InitializeComponent();
            OnBeforeInitializing += BeforeInitializing;
            OnAfterInitializing += AfterInitializing;
            OnFieldValueChanged += FieldValueChanged;
            BeforeInitializing();
            AfterInitializing();
        }

        protected void BeforeInitializing()
        {
            ApplicationUtils.ConfigureGraph(GraphControl, Text);
        }

        protected void AfterInitializing()
        {
            LoadBlockOutputs();
        }

        protected void FieldValueChanged()
        {
            UpdateGraph();
            UpdateSignalList();
        }

        private void LoadBlockOutputs()
        {
            ShowOutputList.Items.Clear();
            foreach (var output in Block.OutputNodes)
            {
                if(output.Name != "All")
                    ShowOutputList.Items.Add(output.Name);
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
                pane.CurveList.RemoveAt(0);
            TempBlock.Execute();
            var outputNode = TempBlock.OutputNodes.FirstOrDefault(it => it.Name == ShowOutputList.Text);
            if (outputNode == null || outputNode.Object == null || outputNode.Object.Count == 0)
            {
                NoDataLabel.Visible = true;
                return;
            }
            NoDataLabel.Visible = false;
            var index = ShowOutputSignal.SelectedIndex;
            if (index == -1 || index > outputNode.Object.Count - 1)
                index = 0;
            var samples = outputNode.Object[index].GetSamplesPair();
            var yAxys = new ZedGraph.PointPairList();
            yAxys.AddRange(samples.Select(it => new ZedGraph.PointPair(it[1], it[0])));
            pane.AddCurve(outputNode.Name, yAxys, Color.Red, ZedGraph.SymbolType.None);
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

        private void ShowOutputListSelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSignalList();
            UpdateGraph();
        }

        private void UpdateSignalList()
        {
            var currentIndex = ShowOutputSignal.SelectedIndex;
            ShowOutputSignal.Items.Clear();
            var outputNode = TempBlock.OutputNodes.FirstOrDefault(it => it.Name == ShowOutputList.Text);
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

        private void ShowOutputSignalSelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }
    }
}
