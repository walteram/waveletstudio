using System;
using System.Drawing;
using System.Linq;
using Qios.DevSuite.Components.Ribbon;
using WaveletStudio.SignalGeneration;
using ZedGraph;

namespace WaveletStudio.MainApplication.Forms
{
    public partial class SignalGenerationForm : QRibbonForm
    {
        public SignalGenerationForm()
        {
            InitializeComponent();
            TemplateField.Items.AddRange(Utils.GetTypes("WaveletStudio.SignalGeneration").Select(it => it.Name).ToArray());
        }

        public string SignalTemplateName = "Sine";

        public bool RunInBackground;

        public CommonSignalBase Template;

        public Signal GeneratedSignal;

        private bool _raiseEvents;

        private bool _raiseListEvent = true;

        private void SignalFormGenerationLoad(object sender, EventArgs e)
        {            
            TemplateField.SelectedItem = SignalTemplateName;
        }

        public Signal Run()
        {
            _raiseListEvent = false;
            TemplateField.SelectedItem = SignalTemplateName;
            var signalType = Utils.GetType("WaveletStudio.SignalGeneration." + SignalTemplateName);
            Template = (CommonSignalBase)Activator.CreateInstance(signalType);
            GeneratedSignal = Template.ExecuteSampler();
            _raiseListEvent = true;
            return GeneratedSignal;
        }

        private void TemplateFieldSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_raiseListEvent)
            {
                return;
            }
            var signalType = Utils.GetType("WaveletStudio.SignalGeneration." + TemplateField.SelectedItem);            
            Template = (CommonSignalBase) Activator.CreateInstance(signalType);
            if (_raiseEvents)
                UpdateSignalFromFields();
            else
                UpdateFieldsFromSignal();
            SignalTemplateName = TemplateField.SelectedItem.ToString();
        }

        private void FieldValueChanged(object sender, EventArgs e)
        {
            if (!_raiseEvents)
                return;
            UpdateSignalFromFields();            
        }
        
        private bool _isRunningUpdate;

        private void UpdateSignalFromFields()
        {
            if (_isRunningUpdate)
            {
                return;
            }
            _isRunningUpdate = true;
            Template.Amplitude = Convert.ToDouble(AmplitudeField.Value);
            Template.Frequency = Convert.ToDouble(FrequencyField.Value);
            Template.Phase = Convert.ToDouble(PhaseField.Value);
            Template.Offset = Convert.ToDouble(OffsetField.Value);
            Template.Start = Convert.ToDouble(StartField.Value);
            Template.Finish = Convert.ToDouble(FinishField.Value);
            Template.SamplingRate = Convert.ToInt32(SamplingRateField.Value);
            Template.IgnoreLastSample = IgnoreLastSampleField.Checked;
            UpdateGraph();
            _isRunningUpdate = false;
        }

        private void UpdateFieldsFromSignal()
        {
            _raiseEvents = false;
            AmplitudeField.Value = Convert.ToDecimal(Template.Amplitude);
            FrequencyField.Value = Convert.ToDecimal(Template.Frequency);
            PhaseField.Value = Convert.ToDecimal(Template.Phase);
            OffsetField.Value = Convert.ToDecimal(Template.Offset);
            StartField.Value = Convert.ToDecimal(Template.Start);
            FinishField.Value = Convert.ToDecimal(Template.Finish);
            SamplingRateField.Value = Convert.ToDecimal(Template.SamplingRate);
            IgnoreLastSampleField.Checked = Template.IgnoreLastSample;
            _raiseEvents = true;
            UpdateGraph();
        }

        private void UpdateGraph()
        {
            GeneratedSignal = Template.ExecuteSampler();
            var samples = GeneratedSignal.GetSamplesPair();
            var pane = GraphControl.GraphPane;
            
            if (pane.CurveList.Count > 0)
                pane.CurveList.RemoveAt(0);
            var yAxys = new PointPairList();
            yAxys.AddRange(samples.Select(it => new PointPair(it[1], it[0])));            
            pane.AddCurve(Template.Name, yAxys, Color.Red, SymbolType.None);
            pane.Legend.IsVisible = false;
            pane.Title.IsVisible = false;
            pane.XAxis.Title.IsVisible = false;
            pane.YAxis.Title.IsVisible = false;
            if (!pane.IsZoomed)
            {
                pane.XAxis.Scale.Min = samples.ElementAt(0)[1];
                pane.XAxis.Scale.Max = samples.ElementAt(samples.Count() - 1)[1];                
            }            
            GraphControl.AxisChange();
            GraphControl.Invalidate();
            GraphControl.Refresh();
        }

        private void GraphControlMouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GraphControl.ZoomOutAll(GraphControl.GraphPane);
        }

        private void UseSignalButtonClick(object sender, EventArgs e)
        {

        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            TemplateField.SelectedItem = SignalTemplateName;
        }        
    }
}
