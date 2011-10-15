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
    public partial class BlockSetupBaseForm : QRibbonForm
    {
        protected readonly BlockBase TempBlock;
        public BlockBase Block { get; set; }
        protected bool HasFields;

        public BlockSetupBaseForm()
        {
            InitializeComponent();
        }

        protected delegate void EventHandler();

        protected event EventHandler OnBeforeInitializing;

        protected event EventHandler OnAfterInitializing;

        protected event EventHandler OnFieldValueChanged;

        public BlockSetupBaseForm(string title, ref BlockBase block)
        {
            InitializeComponent();
            FormCaption.Text = title;
            if (OnBeforeInitializing != null)
                OnBeforeInitializing();
            TempBlock = block.CloneWithLinks();
            TempBlock.Cascade = false;
            Block = block;
            CreateFields();
            if (OnAfterInitializing != null)
                OnAfterInitializing();
        }

        private void CreateFields()
        {
            var type = TempBlock.GetType();
            var topLocation = 41;
            var properties = GetProperties(type);
            HasFields = properties.Count() > 0;
            foreach (var property in properties)
            {
                var height = 21;
                var left = 115;
                var labelValue = ApplicationUtils.GetResourceString(property.Name);
                var defaultValue = (property.GetValue(TempBlock, null) ?? "").ToString();

                if (property.PropertyType != typeof(bool))
                {
                    var label = new Label { Name = "Label" + property.Name, Text = labelValue + @":", Location = new Point(6, topLocation + 3), Width = 108, Height = height+5, Padding = new Padding(0)};
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
                else if (property.PropertyType == typeof(uint) || property.PropertyType == typeof(int) || property.PropertyType == typeof(decimal) || property.PropertyType == typeof(double))
                {
                    field = new NumericUpDown { Minimum = uint.MinValue, Maximum = uint.MaxValue };

                    ((NumericUpDown)field).Scroll += FieldValueChanged;
                    ((NumericUpDown)field).ValueChanged += FieldValueChanged;
                    if (property.PropertyType == typeof(uint))
                    {
                        ((NumericUpDown)field).DecimalPlaces = 0;
                        ((NumericUpDown)field).Increment = 1;
                        ((NumericUpDown)field).Minimum = 1;
                        ((NumericUpDown)field).Maximum = uint.MaxValue;
                    }
                    else if (property.PropertyType != typeof(int))
                    {
                        ((NumericUpDown)field).DecimalPlaces = 3;
                        ((NumericUpDown)field).Increment = 0.1m;
                    }
                }
                else if (property.PropertyType == typeof(bool))
                {
                    field = new CheckBox { AutoSize = true, Text = labelValue, Checked = bool.Parse(defaultValue)};
                    ((CheckBox)field).CheckedChanged += FieldValueChanged;
                    if(properties.Count() == 1)
                        left = 6;
                }
                else if (property.PropertyType == typeof(string) && type.GetProperty(property.Name + "List") != null)
                {
                    var list = (List<string>)type.GetProperty(property.Name + "List").GetValue(TempBlock, null);
                    field = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
                    ((ComboBox)field).SelectedIndexChanged += FieldValueChanged;
                    foreach (var item in list)
                    {
                        var value = item.Contains("|") ? item.Split('|')[0] : item;
                        var key = item.Contains("|") ? item.Split('|')[1] : item;
                        ((ComboBox)field).Items.Add(new ListItem { Text = ApplicationUtils.GetResourceString(key), Value = value });
                        if (defaultValue == item || defaultValue == value)
                            ((ComboBox)field).SelectedIndex = ((ComboBox)field).Items.Count-1;
                    }                    
                }
                else
                {                                       
                    if (property.Name == "Text")
                    {
                        field = new TextBox{Multiline = true, ScrollBars = ScrollBars.Vertical };
                        height = 100;
                    }
                    else
                    {
                        field = new TextBox();
                    }
                    field.LostFocus += FieldValueChanged;
                }
                field.Name = "ClassField" + property.Name;
                field.Size = new Size(180, height);
                field.Tag = property;

                field.Location = new Point(left, topLocation);
                if (property.PropertyType != typeof(bool))
                    field.Text = defaultValue;

                Controls.Add(field);
                topLocation += height + 5;
            }
        }

        private static IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            var validPropertyTypes = new List<Type> { typeof(uint), typeof(int), typeof(decimal), typeof(double), typeof(string), typeof(bool) };
            return type.GetProperties().Where(p => p.GetCustomAttributes(typeof(Parameter), true).Length > 0 && p.CanWrite && (p.PropertyType.IsEnum || validPropertyTypes.Contains(p.PropertyType)));
        }

        protected void FieldValueChanged(object sender, EventArgs e)
        {
            var control = (Control) sender;
            if (control.Tag == null)
                return;
            object value;
            var property = (PropertyInfo)control.Tag;
            if (property.PropertyType == typeof(double))
                value = Convert.ToDouble(((NumericUpDown)control).Value);
            else if (property.PropertyType == typeof(uint))
                value = Convert.ToUInt32(((NumericUpDown)control).Value);
            else if (property.PropertyType == typeof(int))
                value = Convert.ToInt32(((NumericUpDown)control).Value);
            else if (property.PropertyType == typeof(decimal))
                value = Convert.ToDecimal(((NumericUpDown)control).Value);
            else if (property.PropertyType.IsEnum)            
                value = Enum.Parse(property.PropertyType, ((ListItem)((ComboBox)control).SelectedItem).Value);
            else if (sender.GetType() == typeof(ComboBox) || property.PropertyType.IsEnum)
                value = ((ListItem)((ComboBox)control).SelectedItem).Value;
            else if (property.PropertyType == typeof(bool))
                value = ((CheckBox)control).Checked;
            else
                value = control.Text;
            property.SetValue(TempBlock, value, null);
            if (OnFieldValueChanged != null)
                OnFieldValueChanged();
        }
        
        private void UseSignalButtonClick(object sender, EventArgs e)
        {
            var outputNodes = Block.OutputNodes;            
            var inputNodes = Block.InputNodes;
            Block = TempBlock.Clone();
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
            Block.Cascade = true;
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
