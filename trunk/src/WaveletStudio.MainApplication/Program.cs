using System;
using System.Drawing;
using System.Windows.Forms;
using Qios.DevSuite.Components;
using WaveletStudio.MainApplication.Properties;

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
            //Application.SetCompatibleTextRenderingDefault(false);

            ShowSplashScreen();

            QColorScheme.Global.CurrentTheme = "LunaSilver";
            QColorScheme.Global.InheritCurrentThemeFromWindows = true;            
            Application.Run(new AppContext());            
        }

        private static QTranslucentWindow _splashScreen;

        public static void ShowSplashScreen()
        {
            _splashScreen = new QTranslucentWindow {BackgroundImage = new Bitmap(Resources.imgsplash), TopMost = true};
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
