using System;
using System.Windows.Forms;
using Qios.DevSuite.Components;
using WaveletStudio.MainApplication.Forms;

namespace WaveletStudio.MainApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");           

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            QColorScheme.Global.InheritCurrentThemeFromWindows = false;
            QColorScheme.Global.CurrentTheme = "LunaBlue";

            Application.Run(new MainForm());
        }
    }
}
