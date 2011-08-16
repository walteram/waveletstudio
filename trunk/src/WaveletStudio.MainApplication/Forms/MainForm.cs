using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Qios.DevSuite.Components;
using Qios.DevSuite.Components.Ribbon;
using WaveletStudio.MainApplication.Properties;
using WaveletStudio.ProcessingSteps;
using WaveletStudio.SignalGeneration;
using ZedGraph;

namespace WaveletStudio.MainApplication.Forms
{
    public partial class MainForm : QRibbonForm
    {

        public MainForm()
        {
            InitializeComponent();
            OriginalSignalGraph.GraphPane.IsFontsScaled = false;
            CreatedSignalGraph.GraphPane.IsFontsScaled = false;
        }

        private void MainFormLoad(object sender, System.EventArgs e)
        {
            LoadRibbon();
        }

        private void LoadRibbon()
        {
            LoadSignalTemplates();
        }

        private void LoadSignalTemplates()
        {            
            foreach (var type in Utils.GetTypes("WaveletStudio.SignalGeneration"))
            {
                var signal = (CommonSignalBase)Activator.CreateInstance(type);
                var image = Resources.ResourceManager.GetObject("signals_commonsignals_" + signal.Name.ToLower());
                if (image == null)
                    image = Resources.ResourceManager.GetObject("signals_commonsignals_sine");

                var compositeGroup = new QCompositeGroup{ Configuration = {Direction = QPartDirection.Vertical}};
                var compositeItem = new QCompositeItem();
                compositeGroup.Items.Add(new QCompositeImage
                                        {
                                            Image = (Image)image,                                            
                                        });
                compositeGroup.Items.Add(new QCompositeText
                                        {
                                            Title = signal.Name,
                                            Configuration = {AlignmentHorizontal = QPartAlignment.Centered}
                                        });                
                compositeItem.Items.Add(compositeGroup);
                compositeGroup.ColorScheme.CompositeItemHotBackground1.SetColor(QColorScheme.Global.CurrentTheme, Color.White);
                compositeGroup.ColorScheme.CompositeItemHotBackground2.SetColor(QColorScheme.Global.CurrentTheme, Color.White);
                compositeGroup.ColorScheme.CompositeItemExpandedBackground1.SetColor(QColorScheme.Global.CurrentTheme, Color.White);
                compositeGroup.ColorScheme.CompositeItemExpandedBackground2.SetColor(QColorScheme.Global.CurrentTheme, Color.White);
                compositeGroup.ColorScheme.CompositeItemPressedBackground1.SetColor(QColorScheme.Global.CurrentTheme, Color.White);
                compositeGroup.ColorScheme.CompositeItemPressedBackground2.SetColor(QColorScheme.Global.CurrentTheme, Color.White);
                compositeGroup.ColorScheme.CompositeItemBackground1.SetColor(QColorScheme.Global.CurrentTheme, Color.White);
                compositeGroup.ColorScheme.CompositeItemBackground2.SetColor(QColorScheme.Global.CurrentTheme, Color.White);
                compositeGroup.ColorScheme.CompositeItemDisabledBackground1.SetColor(QColorScheme.Global.CurrentTheme, Color.White);
                compositeGroup.ColorScheme.CompositeItemDisabledBackground2.SetColor(QColorScheme.Global.CurrentTheme, Color.White);
                compositeGroup.ColorScheme.CompositeItemPressedBackground1.SetColor(QColorScheme.Global.CurrentTheme, Color.White);
                compositeGroup.ColorScheme.CompositeItemPressedBackground2.SetColor(QColorScheme.Global.CurrentTheme, Color.White);

                compositeItem.ColorScheme.CompositeItemPressedBackground1.SetColor(QColorScheme.Global.CurrentTheme, Color.White);
                compositeItem.ColorScheme.CompositeItemPressedBackground2.SetColor(QColorScheme.Global.CurrentTheme, Color.White);
                compositeItem.ColorScheme.CompositeItemBackground1.SetColor(QColorScheme.Global.CurrentTheme, Color.White);
                compositeItem.ColorScheme.CompositeItemBackground2.SetColor(QColorScheme.Global.CurrentTheme, Color.White);
                compositeItem.ItemName = signal.Name;                  
                compositeItem.Configuration.Margin = new QMargin(3,3,3,3);
                compositeItem.Configuration.Padding = new QPadding(3,3,3,3);
                compositeItem.Configuration.Appearance.BorderWidth = 3;
                compositeItem.ItemActivated += SignalTemplateSelected;
                SignalTemplatesComposite.Items.Add(compositeItem);
            }
            
        }

        private SignalGenerationForm _signalGenerationForm;

        public ProcessingStepList ProcessingSteps = new ProcessingStepList();

        private void SignalTemplateSelected(object sender, QCompositeEventArgs args)
        {
            var templateName = ((QCompositeItem) sender).ItemName;
            var firstTime = _signalGenerationForm == null;

            if (firstTime)
            {
                ShowSignalGenerationForm(templateName);
                return;
            }
            _signalGenerationForm.SignalTemplateName = templateName;            
            SelectSignalGenerationTemplate(_signalGenerationForm.SignalTemplateName);
        }

        private void ShowSignalGenerationForm(string templateName)
        {
            if (_signalGenerationForm == null)            
                _signalGenerationForm = new SignalGenerationForm();
            if (!string.IsNullOrEmpty(templateName))
                _signalGenerationForm.SignalTemplateName = templateName;
            _signalGenerationForm.ShowDialog();
            if (_signalGenerationForm.GeneratedSignal == null || _signalGenerationForm.DialogResult != DialogResult.OK)
                return;
            SelectSignalGenerationTemplate(_signalGenerationForm.SignalTemplateName);
        }

        private void SelectSignalGenerationTemplate(string templateName)
        {
            foreach (var item in SignalTemplatesComposite.Items)
            {
                var compositeItem = (QCompositeItem) item;
                compositeItem.Checked = compositeItem.ItemName == templateName;
            }

            var step = ProcessingSteps.GetStep(GenerateSignalStep.StepKey) as GenerateSignalStep;
            if (step == null)
            {
                ProcessingSteps.RemoveAll(it => it.ProcessingType == ProcessingStepBase.ProcessingTypeEnum.CreateSignal);
                ProcessingSteps.Insert(0, step = new GenerateSignalStep());
            }
            step.Template = _signalGenerationForm.Template;
            ProcessingSteps.Process();
            UpdateGraph(step.Signal, OriginalSignalGraph);
        }

        private void SignalTemplatePanelCaptionShowDialogItemActivated(object sender, QCompositeEventArgs e)
        {
            ShowSignalGenerationForm(null);
        }

        private void UpdateGraph(Signal signal, ZedGraphControl graph)
        {
            var title = "";
            if (graph == OriginalSignalGraph)
                title = "Original Signal";
            else if (graph == OriginalSignalGraph)
                title = "Output";

            var samples = signal.GetSamplesPair();
            var pane = graph.GraphPane;

            if (pane.CurveList.Count > 0)
                pane.CurveList.RemoveAt(0);
            var yAxys = new PointPairList();
            yAxys.AddRange(samples.Select(it => new PointPair(it[1], it[0])));
            pane.AddCurve("", yAxys, Color.Red, SymbolType.None);
            pane.Legend.IsVisible = false;
            pane.Title.IsVisible = title != "";
            pane.Title.Text = title;
            pane.Title.FontSpec = new FontSpec("Arial", 11, Color.Black, true, false, false);
            pane.XAxis.Title.IsVisible = false;
            pane.YAxis.Title.IsVisible = false;
            if (!pane.IsZoomed && samples != null)
            {
                pane.XAxis.Scale.Min = samples.ElementAt(0)[1];
                pane.XAxis.Scale.Max = samples.ElementAt(samples.Count() - 1)[1];
            }
            graph.AxisChange();
            graph.Invalidate();
            graph.Refresh();
        }

        private void Button1Click(object sender, EventArgs e)
        {
            //zedGraphControl1.Font = new Font("Arial", 1);
            
            //zedGraphControl1.GraphPane.Title.FontSpec = new FontSpec("Arial", 5, Color.Black, false,false, false, Color.Transparent, Brushes.Transparent, FillType.None);
            return;

            //var step = new ScalarStep {Operation = ScalarStep.OperationEnum.Multiply, Scalar = 2.1};
            //ProcessingSteps.RemoveAll(it => it.Key == ScalarStep.StepKey);
            //ProcessingSteps.Add(step);

            //var resultSignal = ProcessingSteps.Process();
        }

        private void QCompositeItem1ItemActivated(object sender, QCompositeEventArgs e)
        {

        }

        private void ShowOriginalSignalCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            SplitContainer.Panel1Collapsed = !ShowOriginalSignalCheckBox.Checked;
            SplitContainer.Panel2Collapsed = !ShowCreatedSignal.Checked;
            if (!ShowOriginalSignalCheckBox.Checked && !ShowCreatedSignal.Checked && sender == ShowOriginalSignalCheckBox)
            {
                ShowCreatedSignal.Checked = true;
            }
            else if (!ShowOriginalSignalCheckBox.Checked && !ShowCreatedSignal.Checked && sender == ShowCreatedSignal)
            {
                ShowOriginalSignalCheckBox.Checked = true;
            }
        }

    }
}

