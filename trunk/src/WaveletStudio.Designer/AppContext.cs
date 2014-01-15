using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WaveletStudio.Designer.Forms;
using WaveletStudio.Designer.Properties;
using WaveletStudio.Designer.Resources;

namespace WaveletStudio.Designer
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
            //LoadLastFile(diagramForm);
            diagramForm.Focus();
        }

        public AppContext(string filepath)
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
            if (string.IsNullOrEmpty(filepath))
            {
                LoadLastFile(diagramForm);
            }
            else
            {
                try
                {
                    diagramForm.OpenFileByPath(filepath);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(string.Format("{0}:{1}{2}", DesignerResources.FileCouldntBeOpened, Environment.NewLine, exception.Message), DesignerResources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }            
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
                    filepath = Path.Combine(WaveletStudio.Utils.AssemblyDirectory, filepath);
                if (!File.Exists(filepath))
                    continue;
                try
                {
                    diagramForm.OpenFileByPath(filepath);
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
