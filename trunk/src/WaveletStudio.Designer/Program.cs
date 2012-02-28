using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Qios.DevSuite.Components;
using WaveletStudio.Designer.Properties;

namespace WaveletStudio.Designer
{
    static class Program 
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] parameters)
        {            
            var language = Settings.Default.Language;
            if (!string.IsNullOrEmpty(language) && language!="-")
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(language);
            }            

            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            ShowSplashScreen();

            if (string.IsNullOrEmpty(Settings.Default.Theme) || Settings.Default.Theme == "-")
            {
                QColorScheme.Global.InheritCurrentThemeFromGlobal = true;
                QColorScheme.Global.InheritCurrentThemeFromWindows = true;
            }
            else
            {
                QColorScheme.Global.CurrentTheme = Settings.Default.Theme;
                QColorScheme.Global.InheritCurrentThemeFromWindows = false;   
            }

            var file = "";
            foreach (var parameter in parameters.Where(File.Exists))
            {
                file = parameter;
            }
            Application.Run(new AppContext(file));             
        }

        private static QTranslucentWindow _splashScreen;

        public static void ShowSplashScreen()
        {
            _splashScreen = new QTranslucentWindow {BackgroundImage = new Bitmap(Resources.imgSplash), TopMost = true};
            _splashScreen.ShowCenteredOnScreen();
            Application.Idle += ApplicationIdle;
        }

        private static void ApplicationIdle(object sender, EventArgs e)
        {
            Application.Idle -= ApplicationIdle;
            if (_splashScreen == null) 
                return;
            _splashScreen.Close();
            _splashScreen = null;
        }
    }
}
