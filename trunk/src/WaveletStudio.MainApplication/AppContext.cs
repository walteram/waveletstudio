using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WaveletStudio.MainApplication.Forms;
using WaveletStudio.MainApplication.Properties;

namespace WaveletStudio.MainApplication
{
    public class AppContext : ApplicationContext
    {
        public static AppContext Context;

        public AppContext()
        {
            Context = this;
            if (Settings.Default.RecentFileList == null)
            {
                Settings.Default.RecentFileList = new StringCollection();
                Settings.Default.Save();
            }

            var diagramForm = new DiagramForm();
            MainForm = diagramForm;
            diagramForm.Show();
            LoadLastFile(diagramForm);
            diagramForm.Focus();
        }

        public void LoadLastFile(DiagramForm diagramForm)
        {
            if (!Settings.Default.AutoLoadLastFile || Settings.Default.RecentFileList.Count == 0)
                return;
            var recentFiles = new string[Settings.Default.RecentFileList.Count];
            Settings.Default.RecentFileList.CopyTo(recentFiles, 0);
            foreach (var file in recentFiles)
            {
                var filepath = file;
                if (!Path.IsPathRooted(filepath))
                    filepath = Path.Combine(Utils.AssemblyDirectory, filepath);
                if (!File.Exists(filepath))
                    continue;
                try
                {
                    diagramForm.OpenFile(filepath);
                    break;
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        public void FormClosing(Form form)
        {
            var otherWindow = Application.OpenForms.OfType<Form>().LastOrDefault(it => it != form);
            if (otherWindow == null) 
                return;
            MainForm = otherWindow;
            otherWindow.Focus();
        }
    }
}
