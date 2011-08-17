using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Qios.DevSuite.Components.Ribbon;
using WaveletStudio.MainApplication.Properties;
using WaveletStudio.ProcessingSteps;
using WaveletStudio.SignalGeneration;

namespace WaveletStudio.MainApplication.Forms
{
    public partial class SignalOperationForm : QRibbonForm
    {
        private ProcessingStepBase _previousStep;
        private ProcessingStepBase _step;

        public SignalOperationForm(ProcessingStepBase step, ProcessingStepBase previousStep)
        {
            InitializeComponent();

            _step = step;
            _previousStep = previousStep;

            //Monta campos
            var type = step.GetType();
            var validPropertyTypes = new List<Type> { typeof(int), typeof(decimal), typeof(double), typeof(string), typeof(bool) };

            var topLocation = 81;
            foreach (var property in type.GetProperties().Where(p => p.CanWrite && (p.PropertyType.IsEnum || validPropertyTypes.Contains(p.PropertyType))))
            {
                var labelValue = Resources.ResourceManager.GetString(property.Name) ?? property.Name;
                var defaultValue = property.GetValue(step, null).ToString();

                if (property.PropertyType != typeof(bool))
                {
                    var label = new Label { Name = "Label" + property.Name, Text = labelValue + @":", Location = new Point(11, topLocation + 3) };
                    Controls.Add(label);   
                }                

                Control field;
                if (property.PropertyType.IsEnum)
                {
                    field = new ComboBox {DropDownStyle = ComboBoxStyle.DropDownList };
                    ((ComboBox) field).SelectedIndexChanged += FieldValueChanged;
                    foreach (var item in Enum.GetNames(property.PropertyType))
                    {
                        ((ComboBox)field).Items.Add(Resources.ResourceManager.GetString(item.ToLower()) ?? item);   
                    }                                        
                }
                else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(decimal) || property.PropertyType == typeof(double))
                {                    
                    field = new NumericUpDown {Minimum = int.MinValue, Maximum = int.MaxValue};
                    ((NumericUpDown) field).Scroll += FieldValueChanged;
                    ((NumericUpDown) field).ValueChanged += FieldValueChanged;
                }
                else if (property.PropertyType == typeof(bool))
                {                    
                    field = new CheckBox { AutoSize = true, Text = labelValue, Checked = bool.Parse(defaultValue) };
                    ((CheckBox) field).CheckedChanged += FieldValueChanged;
                }                
                else
                {
                    field = new TextBox();
                    field.TextChanged += FieldValueChanged;
                }
                field.Name = "ClassField" + property.Name;
                field.Size = new Size(138, 21);
                field.Tag = property;
                field.Location = new Point(115, topLocation);
                if (property.PropertyType != typeof(bool))
                    field.Text = defaultValue;                

                Controls.Add(field);
                topLocation += 26;
            }
        }

        public bool RunInBackground;

        public Signal GeneratedSignal;

        private bool _raiseEvents;

        private bool _raiseListEvent = true;

        public Signal Run()
        {
            _raiseListEvent = false;
            _step.Process(_previousStep);
            _raiseListEvent = true;
            return GeneratedSignal;
        }

        private void FieldValueChanged(object sender, EventArgs e)
        {
            //if (!_raiseEvents)
            //    return;
            //UpdateSignalFromFields();            

            var control = (Control) sender;
            if (control.Tag == null)
                return;
            object value;
            var property = (PropertyInfo)control.Tag;
            if (property.PropertyType == typeof(double))
                value = Convert.ToDouble(((NumericUpDown)control).Value);
            else if (property.PropertyType == typeof(int))
                value = Convert.ToInt32(((NumericUpDown)control).Value);
            else if (property.PropertyType == typeof(decimal))
                value = Convert.ToDecimal(((NumericUpDown)control).Value);
            else if (property.PropertyType.IsEnum)
            {
                value = Enum.Parse(property.PropertyType, control.Text);
            }
            else
                value = control.Text;
            property.SetValue(_step, value, null);
            
            UpdateGraph();
        }
        
        private bool _isRunningUpdate;

        private void UpdateSignalFromFields()
        {
            if (_isRunningUpdate)
            {
                return;
            }
            _isRunningUpdate = true;
            foreach (Control control in Controls)
            {
                if (control.Tag == null)
                    continue;
                object value;
                var property = (PropertyInfo) control.Tag;
                if (property.PropertyType == typeof(double))
                    value = double.Parse(control.Text);
                else if (property.PropertyType == typeof(int))
                    value = int.Parse(control.Text);
                else if (property.PropertyType == typeof(decimal))
                    value = decimal.Parse(control.Text);
                else if (property.PropertyType.IsEnum)
                {
                    value = Enum.Parse(property.PropertyType, control.Text);
                }
                else
                    value = control.Text;
                property.SetValue(_step, value, null);
            }            
            UpdateGraph();
            _isRunningUpdate = false;
        }

        private void UpdateFieldsFromSignal()
        {
            /*
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
            UpdateGraph();*/
        }

        private void UpdateGraph()
        {
            _step.Process(_previousStep);
            GeneratedSignal = _step.Signal;
            var samples = GeneratedSignal.GetSamplesPair();
            var pane = GraphControl.GraphPane;
            
            if (pane.CurveList.Count > 0)
                pane.CurveList.RemoveAt(0);
            var yAxys = new ZedGraph.PointPairList();
            yAxys.AddRange(samples.Select(it => new ZedGraph.PointPair(it[1], it[0])));
            pane.AddCurve(_step.Name, yAxys, Color.Red, ZedGraph.SymbolType.None);
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

        private void GraphControlMouseDoubleClick(object sender, MouseEventArgs e)
        {
            GraphControl.ZoomOutAll(GraphControl.GraphPane);
        }

        private void UseSignalButtonClick(object sender, EventArgs e)
        {

        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            
        }        
    }
}
