using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Qios.DevSuite.Components.Ribbon;
using WaveletStudio.Blocks;
using WaveletStudio.Blocks.CustomAttributes;

namespace WaveletStudio.MainApplication.Forms
{
    public partial class BlockSetupForm : QRibbonForm
    {
        private readonly BlockBase _tempBlock;
        public BlockBase Block { get; private set; }

        public BlockSetupForm(string title, ref BlockBase block)
        {
            InitializeComponent();
            FormCaption.Text = title;
            ApplicationUtils.ConfigureGraph(GraphControl, title);
            _tempBlock = block.CloneWithLinks();
            Block = block;
            CreateFields();
            LoadBlockOutputs();
        }

        private void CreateFields()
        {
            var type = _tempBlock.GetType();
            var validPropertyTypes = new List<Type> { typeof(int), typeof(decimal), typeof(double), typeof(string), typeof(bool) };

            var topLocation = 41;
            foreach (var property in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(Parameter), true).Length > 0 && p.CanWrite && (p.PropertyType.IsEnum || validPropertyTypes.Contains(p.PropertyType))))
            {
                var labelValue = ApplicationUtils.GetResourceString(property.Name);
                var defaultValue = (property.GetValue(_tempBlock, null) ?? "").ToString();

                if (property.PropertyType != typeof(bool))
                {
                    var label = new Label { Name = "Label" + property.Name, Text = labelValue + @":", Location = new Point(6, topLocation + 3), Width = 108, Padding = new Padding(0)};
                    Controls.Add(label);
                }

                Control field;
                if (property.PropertyType.IsEnum)
                {
                    field = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
                    ((ComboBox)field).SelectedIndexChanged += FieldValueChanged;
                    foreach (var item in Enum.GetNames(property.PropertyType))
                    {
                        ((ComboBox)field).Items.Add(new ListItem{Text = ApplicationUtils.GetResourceString(item), Value = item});
                        if (defaultValue == item)
                            ((ComboBox) field).SelectedIndex = ((ComboBox) field).Items.Count -1;    
                    }
                        
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
                    var list = (List<string>)type.GetProperty(property.Name + "List").GetValue(_tempBlock, null);
                    field = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
                    ((ComboBox)field).SelectedIndexChanged += FieldValueChanged;
                    foreach (var item in list)
                    {
                        ((ComboBox)field).Items.Add(new ListItem { Text = ApplicationUtils.GetResourceString(item), Value = item });
                        if (defaultValue == item)
                            ((ComboBox)field).SelectedIndex = ((ComboBox)field).Items.Count-1;
                    }                    
                }
                else
                {
                    field = new TextBox();
                    field.TextChanged += FieldValueChanged;
                }
                field.Name = "ClassField" + property.Name;
                field.Size = new Size(180, 21);
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
                value = Enum.Parse(property.PropertyType, ((ListItem)((ComboBox)control).SelectedItem).Value);
            else if (property.PropertyType == typeof(bool))
                value = ((CheckBox)control).Checked;
            else
                value = control.Text;
            property.SetValue(_tempBlock, value, null);
            
            UpdateGraph();
        }
        
        private void LoadBlockOutputs()
        {
            ShowOutputList.Items.Clear();
            foreach (var output in Block.OutputNodes)
            {
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
            }
        }

        private void UpdateGraph()
        {
            var pane = GraphControl.GraphPane;
            if (pane.CurveList.Count > 0)
                pane.CurveList.RemoveAt(0);
            _tempBlock.Execute();
            var outputNode = _tempBlock.OutputNodes.FirstOrDefault(it => it.Name == ShowOutputList.Text);
            if (outputNode == null || outputNode.Object == null || outputNode.Object.Count == 0)
            {
                NoDataLabel.Visible = true;
                return;
            }
            NoDataLabel.Visible = false;
            var samples = outputNode.Object[0].GetSamplesPair();
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
            UpdateGraph();
        }

        private void UseSignalButtonClick(object sender, EventArgs e)
        {
            var outputNodes = Block.OutputNodes;            
            var inputNodes = Block.InputNodes;
            Block = _tempBlock.Clone();
            Block.OutputNodes = outputNodes;
            Block.InputNodes = inputNodes;
            foreach (var node in inputNodes)
            {
                node.Root = Block;
            }
            foreach (var node in outputNodes)
            {
                node.Root = Block;
            }

            Block.Execute();
        }

        internal class ListItem
        {
            public string Text { get; set; }
            public string Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
    }
}
