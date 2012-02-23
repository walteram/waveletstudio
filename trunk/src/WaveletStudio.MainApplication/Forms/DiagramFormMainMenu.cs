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
using System.IO;
using Qios.DevSuite.Components;
using WaveletStudio.MainApplication.Properties;

namespace WaveletStudio.MainApplication.Forms
{
    public partial class DiagramFormMainMenu
    {
        private readonly DiagramForm _diagramForm;

        public DiagramFormMainMenu()
        {
            InitializeComponent();
        }

        public DiagramFormMainMenu(DiagramForm diagramForm)
        {
            InitializeComponent();
            _diagramForm = diagramForm;
        }

        public void NewMenuItemItemActivated(object sender, QCompositeEventArgs e)
        {
            var diagramForm = new DiagramForm();
            diagramForm.Show();
        }

        public void SaveAsMenuItemItemActivated(object sender, QCompositeEventArgs e)
        {
            _diagramForm.SaveAs();
        }

        public void SaveMenuItemItemActivated(object sender, QCompositeEventArgs e)
        {
            _diagramForm.Save();
        }

        public void OpenMenuItemItemActivated(object sender, QCompositeEventArgs e)
        {
            _diagramForm.OpenFile();
        }

        public void CloseMenuItemItemActivated(object sender, QCompositeEventArgs e)
        {
            _diagramForm.CloseWindow();
        }

        private void DiagramFormMainMenuVisibleChanged(object sender, EventArgs e)
        {
            CreateRecentDocument();
        }

        private void ExportMenuItemItemActivated(object sender, QCompositeEventArgs e)
        {
            _diagramForm.ExportImage();
        }

        private void CreateRecentDocument()
        {
            DocumentItems.Clear();
            var i = 0;
            foreach (var file in Settings.Default.RecentFileList)
            {
                if (!File.Exists(file) && !File.Exists(Path.Combine(Utils.AssemblyDirectory, file))) 
                    continue;
                i++;
                var compositeItem = new QCompositeItem
                                    {
                                        Configuration = {ShrinkHorizontal = true, StretchHorizontal = true},
                                        HotkeyText = i.ToString(),
                                        ItemName = file
                                    };
                compositeItem.Items.Add(new QCompositeText { Title = string.Format(@"&{0}", i) });
                compositeItem.Items.Add(new QCompositeText { Title = file, Configuration = { ShrinkHorizontal = true, StretchHorizontal = true } });
                compositeItem.ItemActivated += (sender, args) => _diagramForm.OpenFile(((QCompositeItem)sender).ItemName);
                DocumentItems.Add(compositeItem);
            }
        }

        public void PrintMenuItemItemActivated(object sender, QCompositeEventArgs e)
        {
            _diagramForm.Print(_diagramForm, true);                   
        }

        public void PrintPreviewMenuItemItemActivated(object sender, QCompositeEventArgs e)
        {
            _diagramForm.PrintPreview();            
        }
    }
}
