/*  Wavelet Studio Signal Processing Library - www.waveletstudio.net
    Copyright (C) 2011, 2012 Walter V. S. de Amorim - The Wavelet Studio Initiative

    Wavelet Studio is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Wavelet Studio is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

using System;
using System.Windows.Forms;
using Qios.DevSuite.Components.Ribbon;
using WaveletStudio.Designer.Properties;

namespace WaveletStudio.Designer.Forms
{
    public partial class PrintPreviewForm : QRibbonForm
    {
        private readonly DiagramForm _diagramForm;

        public PrintPreviewForm()
        {
            InitializeComponent();
        }
        
        public PrintPreviewForm(DiagramForm diagramForm)
        {
            InitializeComponent();
            _diagramForm = diagramForm;            
            UpdateMarginFields();
            RefreshDocument();
        }

        public void RefreshDocument()
        {            
            PrintPreviewControl.Document = _diagramForm.GetPrintDocument();
        }

        private void DiagramFormLoad(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            Text = string.Format("{0} [{1}]", _diagramForm.Text, Resources.Preview);
            GridComposite.Checked = Settings.Default.PrintGrid;
            StretchComposite.Checked = Settings.Default.PrintAllowStretch;
            ZoomTrackBar.Value = 6;
        }
       
        private void PrintCompositeItemItemActivated(object sender, Qios.DevSuite.Components.QCompositeEventArgs e)
        {
            _diagramForm.Print(this, true);
            RefreshDocument();
        }

        private void SettingsCompositeItemActivated(object sender, Qios.DevSuite.Components.QCompositeEventArgs e)
        {
            _diagramForm.Print(this, false);
            RefreshDocument();
        }

        private void GridCompositeItemActivated(object sender, Qios.DevSuite.Components.QCompositeEventArgs e)
        {
            GridComposite.Checked = !GridComposite.Checked;
            Settings.Default.PrintGrid = GridComposite.Checked;
            Settings.Default.Save();
            RefreshDocument();
        }

        private void StretchCompositeItemActivated(object sender, Qios.DevSuite.Components.QCompositeEventArgs e)
        {
            StretchComposite.Checked = !StretchComposite.Checked;
            Settings.Default.PrintAllowStretch = StretchComposite.Checked;
            Settings.Default.Save();
            RefreshDocument();
        }

        private void MarginInputBoxTextChanged(object sender, EventArgs e)
        {
            var changed = false;
            Settings.Default.PrintMarginTop = ParseMargin(MarginTopField.InputBox.Text, Settings.Default.PrintMarginTop, ref changed);
            Settings.Default.PrintMarginLeft = ParseMargin(MarginLeftField.InputBox.Text, Settings.Default.PrintMarginLeft, ref changed);
            Settings.Default.PrintMarginRight = ParseMargin(MarginRightField.InputBox.Text, Settings.Default.PrintMarginRight, ref changed);
            Settings.Default.PrintMarginBottom = ParseMargin(MarginBottomField.InputBox.Text, Settings.Default.PrintMarginBottom, ref changed);
            UpdateMarginFields();
            Settings.Default.Save();
            if(changed)
                RefreshDocument();
        }

        private void UpdateMarginFields()
        {
            MarginTopField.InputBox.Text = Settings.Default.PrintMarginTop.ToString();
            MarginLeftField.InputBox.Text = Settings.Default.PrintMarginLeft.ToString();
            MarginRightField.InputBox.Text = Settings.Default.PrintMarginRight.ToString();
            MarginBottomField.InputBox.Text = Settings.Default.PrintMarginBottom.ToString();
        }

        private decimal ParseMargin(string newValue, decimal oldValue, ref bool changed)
        {
            decimal newValueDecimal;
            if (decimal.TryParse(newValue, out newValueDecimal))
            {
                if (newValueDecimal > 10)
                    newValueDecimal = 10;
                if (newValueDecimal < 0)
                    newValueDecimal = 0;                
                if (changed == false && newValueDecimal != oldValue)
                    changed = true;
                return newValueDecimal;   
            }
            return oldValue;
        }
        
        private void ZoomMinusButtonClick(object sender, EventArgs e)
        {
            if (ZoomTrackBar.Value>1)
                ZoomTrackBar.Value -= 1;
        }

        private void ZoomTrackBarValueChanged(object sender, EventArgs e)
        {
            PrintPreviewControl.Zoom = ZoomTrackBar.Value / 10f;
            ZoomLabel.Text = (ZoomTrackBar.Value*10) + @"%";
        }

        private void ZoomPlusButtonClick(object sender, EventArgs e)
        {
            if (ZoomTrackBar.Value < 30)
                ZoomTrackBar.Value += 1;
        }

        private void CloseCompositeItemActivated(object sender, Qios.DevSuite.Components.QCompositeEventArgs e)
        {
            Close();
        }            
    }
}

