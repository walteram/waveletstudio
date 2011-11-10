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

        private void DiagramFormMainMenuVisibleChanged(object sender, System.EventArgs e)
        {
            CreateRecentDocument();
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
                compositeItem.Items.Add(new QCompositeText { Title = @"&" + i });
                compositeItem.Items.Add(new QCompositeText { Title = file, Configuration = { ShrinkHorizontal = true, StretchHorizontal = true } });
                compositeItem.ItemActivated += (sender, args) => _diagramForm.OpenFile(((QCompositeItem)sender).ItemName);
                DocumentItems.Add(compositeItem);
            }
        }
    }
}
