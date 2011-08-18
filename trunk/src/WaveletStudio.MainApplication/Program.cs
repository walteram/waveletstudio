using System;
using System.Windows.Forms;
using Qios.DevSuite.Components;
using WaveletStudio.MainApplication.Forms;
using WaveletStudio.ProcessingSteps;
using WaveletStudio.SignalGeneration;

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
            //QColorScheme.Global.CurrentTheme = "LunaSilver";
            
            var template = new Sawtooth
            {
                Amplitude = 2,
                Frequency = 5,
                Phase = 0,
                Offset = 0,
                Start = 0,
                Finish = 1,
                SamplingRate = 120,
                IgnoreLastSample = true
            };
            template.ExecuteSampler();
            var previous = new GenerateSignalStep();// {Template = template};
            var step = new ScalarStep();
            previous.Process(null);
            step.Process(previous);

            //Application.Run(new SignalOperationForm(step, previous));

            //new SignalOperationForm(step, previous).Show();

            //Application.Run(new SignalOperationForm("Alow mundô!!", previous, null));
            Application.Run(new MainForm());
        }
    }
}
