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
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Qios.DevSuite.Components.Ribbon;
using WaveletStudio.Designer.Properties;
using WaveletStudio.Designer.Utils;
using WaveletStudio.Designer.Resources;

namespace WaveletStudio.Designer.Forms
{
    public partial class FileForm : QRibbonForm
    {
        public enum ActionTaken
        {
            None,
            New,
            Open,
        }

        private readonly DiagramForm _diagramForm;

        public ActionTaken ActionResult { get; set; }

        public string FilePath { get; set; }

        public FileForm(DiagramForm diagramForm)
        {
            InitializeComponent();            
            _diagramForm = diagramForm;
            UpdateFields();
        }

        public void InitInformations()
        {
            TabControl.SelectTab(InformationsTabPage);
        }

        public void InitOpen()
        {
            TabControl.SelectTab(OpenTabPage);
        }

        public void InitPrint()
        {
            TabControl.SelectTab(PrintTabPage);
        }

        private void FileWindow_Load(object sender, EventArgs e)
        {
            ActionResult = ActionTaken.None;
            FilePath = null;
            LoadInformation();
            CreateRecentDocument();
            ZoomTrackBar.Value = 6;
            if (TabControl.SelectedTab == PrintTabPage)
            {
                PrintButton_Click(null, null);
            }
        }

        private void LoadInformation()
        {
            CreatedAtLabel.Text = _diagramForm.DocumentModel.CreatedAt.ToString(CultureInfo.CurrentCulture);
            AuthorLabel.Text = _diagramForm.DocumentModel.Author;
            LastModificationLabel.Text = _diagramForm.DocumentModel.ModifiedAt.HasValue ? _diagramForm.DocumentModel.ModifiedAt.Value.ToString(CultureInfo.CurrentCulture) : "";
            ModifiedByLabel.Text = _diagramForm.DocumentModel.ModifiedBy;
            BlocksLabel.Text = _diagramForm.DocumentModel.BlockCount.ToString(CultureInfo.CurrentCulture);
            ConnectionsLabel.Text = _diagramForm.DocumentModel.ConnectionCount.ToString(CultureInfo.CurrentCulture);
            FileSizeLabel.Text = _diagramForm.DocumentModel.FileSizeText();
            CanvasSizeLabel.Text = _diagramForm.DocumentModel.CanvasSize;
            NotesField.Text = _diagramForm.DocumentModel.Notes;
        }

        internal void InformationsButton_Click(object sender, EventArgs e)
        {
            TabControl.SelectTab(InformationsTabPage);
        }

        internal void NewButton_Click(object sender, EventArgs e)
        {
            ActionResult = ActionTaken.New;
            Close();                                    
        }        

        internal void OpenButton_Click(object sender, EventArgs e)
        {
            TabControl.SelectTab(OpenTabPage);            
        }

        internal void SaveButton_Click(object sender, EventArgs e)
        {
            if (_diagramForm.Save())
            {
                Close();
            }
        }

        internal void SaveAsButton_Click(object sender, EventArgs e)
        {
            if (_diagramForm.SaveAs())
            {
                Close();
            }
        }

        internal void ExportButton_Click(object sender, EventArgs e)
        {
            if (_diagramForm.ExportImage())
            {
                Close();
            }
        }

        internal void OptionsButton_Click(object sender, EventArgs e)
        {

        }

        internal void GenerateCodeButton_Click(object sender, EventArgs e)
        {
            if (_diagramForm.ExportCode())
            {
                Close();
            }
        }
       
        internal void PrintButton_Click(object sender, EventArgs e)
        {
            TabControl.SelectTab(PrintTabPage);
            RefreshDocument();
        }

        private void CreateRecentDocument()
        {
            var recentFileList = RecentFileList.ReadFromFile();
            recentFileList.Reverse();
            RecentFilesPannel.Controls.Clear();

            var countInX = RecentFilesPannel.DisplayRectangle.Width / 210;
            var countInY = RecentFilesPannel.DisplayRectangle.Height / 180;
            var totalCount = countInX * countInY;

            foreach (var item in recentFileList.Take(totalCount))
            {
                var button = CreateOpenRecentFileButton(item);
                RecentFilesPannel.Controls.Add(button);                
            }
        }

        private Button CreateOpenRecentFileButton(RecentFile recentFile)
        {
            var button = new FlatButton
            {
                Image = recentFile.Thumbnail, //.ResizeTo(200, 150),
                Width = 200,
                Height = 170,
                Tag = recentFile.FilePath,
                FlatStyle = FlatStyle.Flat,
                Text = Path.GetFileName(recentFile.FilePath),
                ImageAlign = ContentAlignment.TopCenter,
                TextAlign = ContentAlignment.BottomCenter,
                HasPressedState = false,
                FocusBackColor = Color.Thistle,
                NormalBackColor = Color.FromArgb(245, 239, 245),
                BackColor = Color.FromArgb(245, 239, 245),
                FocusBorderSize = 1,
                Padding = new Padding(3, 3, 3, 3)
            };
            if (button.Text.Length > 27)
            {
                var toolTip = new ToolTip();
                toolTip.SetToolTip(button, button.Text);
                button.Text = button.Text.Substring(0, 20) + @"..." + button.Text.Substring(button.Text.LastIndexOf('.'));                
            }
            button.Click += OpenRecentFileButtonClick;
            return button;
        }

        private void OpenRecentFileButtonClick(object sender, EventArgs e)
        {
            var button = (Control)sender;
            var path = button.Tag.ToString();
            _diagramForm.OpenFileByPath(path);
            Close();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        { 
            Close();
            _diagramForm.Close();
        }

        private void ZoomMinusButton_Click(object sender, EventArgs e)
        {
            if (ZoomTrackBar.Value > 1)
            {                
                ZoomTrackBar.Value -= 1;
            }
        }

        private void ZoomTrackBar_ValueChanged(object sender, EventArgs e)
        {
            PrintPreviewControl.Zoom = ZoomTrackBar.Value / 10f;
            ZoomLabel.Text = (ZoomTrackBar.Value * 10) + @"%";
        }

        private void ZoomPlusButton_Click(object sender, EventArgs e)
        {
            if (ZoomTrackBar.Value < 30)
            {
                ZoomTrackBar.Value += 1;
            }
        }

        public void RefreshDocument()
        {
            Application.DoEvents();
            PrintPreviewControl.Document = _diagramForm.GetPrintDocument();
        }

        private void UpdateFields()
        {            
            MarginTopField.Value = Settings.Default.PrintMarginTop;
            MarginLeftField.Value = Settings.Default.PrintMarginLeft;
            MarginRightField.Value = Settings.Default.PrintMarginRight;
            MarginBottomField.Value = Settings.Default.PrintMarginBottom;
            StretchModelToPage.Checked = Settings.Default.PrintAllowStretch;
            ShowGridCheckBox.Checked = Settings.Default.PrintGrid;
            OrientationLandscapeButton.Pressed = Settings.Default.PrintLandscape;
            OrientationPortraitButton.Pressed = !Settings.Default.PrintLandscape;            
        }

        private void MarginInputBoxTextChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                return;
            }
            Settings.Default.PrintMarginTop = MarginTopField.Value;
            Settings.Default.PrintMarginLeft = MarginLeftField.Value;
            Settings.Default.PrintMarginRight = MarginRightField.Value;
            Settings.Default.PrintMarginBottom = MarginBottomField.Value;
            Settings.Default.Save();
            RefreshDocument();
        }

        private void ShowGridCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.PrintGrid = ShowGridCheckBox.Checked;
            Settings.Default.Save();
            RefreshDocument();
        }

        private void StretchModelToPage_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.PrintAllowStretch = StretchModelToPage.Checked;
            Settings.Default.Save();
            RefreshDocument();
        }

        private void PrintNowButton_Click(object sender, EventArgs e)
        {
            _diagramForm.Print(this, true);
        }

        private void OrientationLandscapeButton_Click(object sender, EventArgs e)
        {
            if (OrientationLandscapeButton.Pressed)
            {
                return;
            }
            OrientationLandscapeButton.Pressed = true;
            OrientationPortraitButton.Pressed = false;
            Settings.Default.PrintLandscape = true;
            Settings.Default.Save();
            RefreshDocument();
        }

        private void OrientationPortraitButton_Click(object sender, EventArgs e)
        {
            if (OrientationPortraitButton.Pressed)
            {
                return;
            }
            OrientationLandscapeButton.Pressed = false;
            OrientationPortraitButton.Pressed = true;
            Settings.Default.PrintLandscape = false;
            Settings.Default.Save();
            RefreshDocument();
        }

        private void OpenFromComputerButton_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog
            {
                SupportMultiDottedExtensions = true,
                Filter = string.Format("{0}|*.wsd", DesignerResources.WaveletStudioDocument), //|Wavelet Studio XML Document|*.wsx",
                RestoreDirectory = true
            };
            if (openDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                ActionResult = ActionTaken.Open;
                FilePath = openDialog.FileName;
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(string.Format("{0}:{1}{2}", DesignerResources.FileCouldntBeOpened, Environment.NewLine, exception.Message), DesignerResources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }            
        }

        private void NotesLabel_TextChanged(object sender, EventArgs e)
        {
            _diagramForm.DocumentModel.Notes = NotesField.Text;
            _diagramForm.DocumentModel.Touch();
        }
        
        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentTabText = TabControl.SelectedTab.Text;
            foreach (var button in MenuPanel.Controls.OfType<Button>())
            {
                if (button.Text.Replace("&","") == currentTabText)
                {
                    button.BackColor = Color.FromArgb(64, 0, 64);
                }
                else
                {
                    button.BackColor = Color.Purple;
                }
            }
        }

        protected override void OnPaintNonClientArea(PaintEventArgs e)
        {
            base.OnPaintNonClientArea(e);
            e.Graphics.FillRectangle(new SolidBrush(Color.Purple), 0, 0, 135, Height);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(64, 0, 64)), 0, 0, 8, Height);
        }
    }
}
