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
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Qios.DevSuite.Components;
using Qios.DevSuite.Components.Ribbon;
using WaveletStudio.Designer.Properties;
using WaveletStudio.Designer.Resources;
using WaveletStudio.Designer.Utils;

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
            CreateButtons();
            UpdateMarginFields();
            RefreshDocument();
        }

        private void CreateButtons()
        {
            var printGroup = PreviewOptionsPage.CreateCompositeGroup(null);
            CreateButton(printGroup, DesignerResources.Print, Images.iconprint1, PrintCompositeItemItemActivated, false);

            var settingsGroup = PreviewOptionsPage.CreateCompositeGroup(DesignerResources.Settings);
            CreateButton(settingsGroup, DesignerResources.Settings, Images.iconprintsettings1, SettingsCompositeItemActivated, false);
            CreateButton(settingsGroup, "GRID", Images.iconGrid, GridCompositeItemActivated, Settings.Default.PrintGrid);
            CreateButton(settingsGroup, "STRETCH", Images.iconstretch1, StretchCompositeItemActivated, Settings.Default.PrintAllowStretch);

            var closeGroup = PreviewOptionsPage.CreateCompositeGroup("");
            CreateButton(closeGroup, "CLOSE", Images.iconclose, CloseCompositeItemActivated, false);
        }

        private void CreateButton(QCompositeGroup group, string title, Image image, QCompositeEventHandler onClickAction, bool isChecked)
        {
            var item = QControlUtils.CreateCompositeItem(title, image);
            item.ItemActivated += onClickAction;
            item.Checked = isChecked;
            group.Items.Add(item);
        }

        public void RefreshDocument()
        {            
            PrintPreviewControl.Document = _diagramForm.GetPrintDocument();
        }

        private void DiagramFormLoad(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            Text = string.Format("{0} [{1}]", _diagramForm.Text, DesignerResources.Preview);            
            ZoomTrackBar.Value = 6;
        }
       
        private void PrintCompositeItemItemActivated(object sender, QCompositeEventArgs e)
        {
            _diagramForm.Print(this, true);
            RefreshDocument();
        }

        private void SettingsCompositeItemActivated(object sender, QCompositeEventArgs e)
        {
            _diagramForm.Print(this, false);
            RefreshDocument();
        }

        private void GridCompositeItemActivated(object sender, QCompositeEventArgs e)
        {
            var composite = (QCompositeItem) sender;
            composite.Checked = !composite.Checked;
            Settings.Default.PrintGrid = composite.Checked;
            Settings.Default.Save();
            RefreshDocument();
        }

        private void StretchCompositeItemActivated(object sender, QCompositeEventArgs e)
        {
            var composite = (QCompositeItem)sender;
            composite.Checked = !composite.Checked;
            Settings.Default.PrintAllowStretch = composite.Checked;
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
            MarginTopField.InputBox.Text = Settings.Default.PrintMarginTop.ToString(CultureInfo.InvariantCulture);
            MarginLeftField.InputBox.Text = Settings.Default.PrintMarginLeft.ToString(CultureInfo.InvariantCulture);
            MarginRightField.InputBox.Text = Settings.Default.PrintMarginRight.ToString(CultureInfo.InvariantCulture);
            MarginBottomField.InputBox.Text = Settings.Default.PrintMarginBottom.ToString(CultureInfo.InvariantCulture);
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

        private void CloseCompositeItemActivated(object sender, QCompositeEventArgs e)
        {
            Close();
        }            
    }
}

