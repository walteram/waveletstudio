using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Qios.DevSuite.Components.Ribbon;
using WaveletStudio.Blocks;

namespace WaveletStudio.MainApplication.Forms
{
    public partial class BlockSetupForm : QRibbonForm
    {
        private readonly BlockBase _previousStep;
        public BlockBase Step;
        private readonly BlockBase _blockBackup;

        public BlockSetupForm(string title, ref BlockBase step, BlockBase previousStep)
        {
            InitializeComponent();
            FormCaption.Text = title;
            GraphControl.ContextMenuBuilder += (sender, strip, pt, state) => strip.Items.RemoveByKey("set_default");
            Step = step;
            _blockBackup = step.Clone();
            _previousStep = previousStep;
            CreateFields();
        }

        private void CreateFields()
        {
            var type = Step.GetType();
            var validPropertyTypes = new List<Type> { typeof(int), typeof(decimal), typeof(double), typeof(string), typeof(bool) };

            var topLocation = 41;
            foreach (var property in type.GetProperties().Where(p => p.CanWrite && (p.PropertyType.IsEnum || validPropertyTypes.Contains(p.PropertyType)) && p.Name != "Index" ))
            {
                var labelValue = ApplicationUtils.GetResourceString(property.Name);
                var defaultValue = (property.GetValue(Step, null) ?? "").ToString();

                if (property.PropertyType != typeof(bool))
                {
                    var label = new Label { Name = "Label" + property.Name, Text = labelValue + @":", Location = new Point(11, topLocation + 3) };
                    Controls.Add(label);
                }

                Control field;
                if (property.PropertyType.IsEnum)
                {
                    field = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
                    ((ComboBox)field).SelectedIndexChanged += FieldValueChanged;
                    foreach (var item in Enum.GetNames(property.PropertyType))
                    {
                        ((ComboBox)field).Items.Add(ApplicationUtils.GetResourceString(item));
                    }
                    if (!string.IsNullOrEmpty(defaultValue))
                        ((ComboBox) field).SelectedItem = defaultValue;
                }
                else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(decimal) || property.PropertyType == typeof(double))
                {
                    field = new NumericUpDown { Minimum = int.MinValue, Maximum = int.MaxValue };
                    ((NumericUpDown)field).Scroll += FieldValueChanged;
                    ((NumericUpDown)field).ValueChanged += FieldValueChanged;
                    if (property.PropertyType != typeof(int))
                    {
                        ((NumericUpDown)field).DecimalPlaces = 3;
                        ((NumericUpDown)field).Increment = 0.1m;
                    }
                }
                else if (property.PropertyType == typeof(bool))
                {
                    field = new CheckBox { AutoSize = true, Text = labelValue, Checked = bool.Parse(defaultValue) };
                    ((CheckBox)field).CheckedChanged += FieldValueChanged;
                }
                else if (property.PropertyType == typeof(string) && type.GetProperty(property.Name + "List") != null)
                {
                    var list = (List<string>)type.GetProperty(property.Name + "List").GetValue(Step, null);
                    field = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
                    ((ComboBox)field).SelectedIndexChanged += FieldValueChanged;
                    foreach (var item in list)
                    {
                        ((ComboBox)field).Items.Add(ApplicationUtils.GetResourceString(item));
                    }
                    if (!string.IsNullOrEmpty(defaultValue))
                        ((ComboBox)field).SelectedItem = defaultValue;
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
        
        private void FieldValueChanged(object sender, EventArgs e)
        {
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
                value = Enum.Parse(property.PropertyType, control.Text);
            else if (property.PropertyType == typeof(bool))
                value = ((CheckBox)control).Checked;
            else
                value = control.Text;
            property.SetValue(Step, value, null);
            
            UpdateGraph();
        }
        
        private void UpdateGraph()
        {/*
            Step.Process(_previousStep);
            var samples = Step.Signal.GetSamplesPair();
            var pane = GraphControl.GraphPane;
            if (pane.CurveList.Count > 0)
                pane.CurveList.RemoveAt(0);
            
            var yAxys = new ZedGraph.PointPairList();
            yAxys.AddRange(samples.Select(it => new ZedGraph.PointPair(it[1], it[0])));
            pane.AddCurve(Step.Name, yAxys, Color.Red, ZedGraph.SymbolType.None);
            pane.Legend.IsVisible = false;
            pane.Title.IsVisible = false;
            pane.XAxis.Title.IsVisible = false;
            pane.YAxis.Title.IsVisible = false;
            if (!pane.IsZoomed && samples.Count() != 0)
            {
                pane.XAxis.Scale.Min = samples.ElementAt(0)[1];
                pane.XAxis.Scale.Max = samples.ElementAt(samples.Count() - 1)[1];                
            }            
            GraphControl.AxisChange();
            GraphControl.Invalidate();
            GraphControl.Refresh();*/
        }

        private void GraphControlMouseDoubleClick(object sender, MouseEventArgs e)
        {
            GraphControl.ZoomOutAll(GraphControl.GraphPane);            
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            Step = _blockBackup.Clone();
            //Step.Process(_previousStep);
        }   
    }
}
