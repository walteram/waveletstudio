using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Qios.DevSuite.Components;
using Qios.DevSuite.Components.Ribbon;
using WaveletStudio.Blocks;
using WaveletStudio.MainApplication.Controls;
using WaveletStudio.SignalGeneration;
using ZedGraph;

namespace WaveletStudio.MainApplication.Forms
{
    public partial class MainForm : QRibbonForm
    {
        public BlockList Blocks = new BlockList();

        public BlockBase CurrentSelectedBlock; 

        public MainForm()
        {
            InitializeComponent();

            SignalTemplatePanel.CaptionShowDialogItemActivated += (sender, args) => ShowSignalGenerationForm(null, true);
            ConfigureGraph(OriginalSignalGraph, "Original Signal");
            ConfigureGraph(CreatedSignalGraph, "Created Signal");
            PlotComplex();
        }


        private void PlotComplex()
        {
            var yAxys = new PointPairList();
            yAxys.Add(3, 2);
            yAxys.Add(3, -2);
            yAxys.Add(-3, -2);
            yAxys.Add(-3, 2);
            yAxys.Add(3, 2);
            var pane = CreatedSignalGraph.GraphPane;
            pane.AddCurve("", yAxys, Color.Red, SymbolType.None);
            CreatedSignalGraph.AxisChange();
            CreatedSignalGraph.Invalidate();
            CreatedSignalGraph.Refresh();
        }

        private void ConfigureGraph(ZedGraphControl graph, string title)
        {
            var pane = graph.GraphPane;
            graph.ContextMenuBuilder += (sender, strip, pt, state) => strip.Items.RemoveByKey("set_default");
            pane.IsFontsScaled = false;
            pane.Title.FontSpec = new FontSpec("Arial", 11, Color.Black, true, false, false){Border = new Border(false, Color.Transparent, 0)};
            pane.Title.Text = ApplicationUtils.GetResourceString(title);
            pane.Legend.IsVisible = false;
            pane.XAxis.Title.IsVisible = false;
            pane.YAxis.Title.IsVisible = false;            
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            LoadRibbon();
            WindowState = FormWindowState.Maximized;
        }

        private void LoadRibbon()
        {
            LoadSignalTemplates();
            LoadSteps(OperationsFunctionsComposite, BlockBase.ProcessingTypeEnum.Operation);
        }

        private void LoadSignalTemplates()
        {            
            foreach (var type in Utils.GetTypes("WaveletStudio.SignalGeneration"))
            {
                var signal = (CommonSignalBase)Activator.CreateInstance(type);
                var item = QControlUtils.CreateCompositeListItem(signal.Name, signal.Name.ToLower(), ApplicationUtils.GetResourceString(signal.Name), "", 1, QPartDirection.Vertical, QPartAlignment.Centered, Color.White);
                item.ItemActivated += (sender, args) => ShowSignalGenerationForm(((QCompositeItem)sender).ItemName, false);
                SignalTemplatesComposite.Items.Add(item);
            }
        }

        private void LoadSteps(QCompositeItemBase compositeGroup, BlockBase.ProcessingTypeEnum processingType)
        {
            foreach (var type in Utils.GetTypes("WaveletStudio.ProcessingSteps").Where(t => t.BaseType == typeof(BlockBase)))
            {
                var step = (BlockBase)Activator.CreateInstance(type);
                if (step.ProcessingType != processingType)
                    continue;

                var item = QControlUtils.CreateCompositeListItem(type.FullName, step.Name.ToLower(), ApplicationUtils.GetResourceString(step.Name), "", 1, QPartDirection.Vertical, QPartAlignment.Centered, Color.White);
                
                item.ItemActivated += (sender, args) => ShowOperationForm(((QCompositeItem)sender).ItemName, true);
                compositeGroup.Items.Add(item);
            }
        }

        private void ShowSignalGenerationForm(string templateName, bool forceShowForm, BlockBase step = null)
        {
            var currentStep = step ?? Blocks.FirstOrDefault(it => it.Key == GenerateSignalBlock.StepKey);
            if (currentStep != null)
            {
                step = GenerateSignalBlock.Clone(currentStep);
            }
            else
            {
                step = new GenerateSignalBlock();
                forceShowForm = true;
            }
            if (!string.IsNullOrEmpty(templateName))
                ((GenerateSignalBlock)step).TemplateName = templateName;

            if (forceShowForm)
            {
                var form = new BlockSetupForm(ApplicationUtils.GetResourceString("signaltemplates"), ref step, null);
                form.ShowDialog();
                if (form.DialogResult != DialogResult.OK)
                    return;
                templateName = ((GenerateSignalBlock)step).TemplateName;
            }
            foreach (QCompositeItem item in SignalTemplatesComposite.Items)
            {
                item.Checked = item.ItemName == templateName;
            }
            Blocks.RemoveAll(it => it.ProcessingType == BlockBase.ProcessingTypeEnum.CreateSignal);
            Blocks.Insert(0, step);
            UpdateForm();
        }

        private void ShowOperationForm(string stepFullName, bool forceShowForm, BlockBase step = null, BlockBase previousStep = null)
        {
            if(!CheckOriginalSignal())
                return;
            var inserting = false;
            if (previousStep == null)
            {
                previousStep = CurrentSelectedBlock ?? Blocks.Last();
            }            
            if (step == null)
            {
                step = (BlockBase)Activator.CreateInstance(Utils.GetType(stepFullName));
                inserting = true;
            }            
            if (!forceShowForm) 
                return;
            var form = new BlockSetupForm(ApplicationUtils.GetResourceString("signaltemplates"), ref step, previousStep);
            form.ShowDialog();
            if (inserting && form.DialogResult == DialogResult.OK)
            {
                Blocks.Insert(previousStep.Index+1, step);
            }
            if (form.DialogResult != DialogResult.OK)
            {
                Blocks[step.Index] = form.Step.Clone();
            }            
            
            UpdateForm();
        }

        private bool CheckOriginalSignal()
        {
            if (Blocks.Count == 0)
            {
                MessageBox.Show(ApplicationUtils.GetResourceString("errorinvalidoriginalsignal"), @"Wavelet Studio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private void UpdateGraph(Signal signal, ZedGraphControl graph)
        {
            var pane = graph.GraphPane;
            if (signal == null || signal.Samples == null)
            {
                pane.CurveList.Clear();
                return;
            }
            var samples = signal.GetSamplesPair();                        
            if (pane.CurveList.Count > 0)
                pane.CurveList.RemoveAt(0);
            var yAxys = new PointPairList();
            yAxys.AddRange(samples.Select(it => new PointPair(it[1], it[0])));
            pane.AddCurve("", yAxys, Color.Red, SymbolType.None);
            if (!pane.IsZoomed && samples != null)
            {
                pane.XAxis.Scale.Min = samples.ElementAt(0)[1];
                pane.XAxis.Scale.Max = samples.ElementAt(samples.Count() - 1)[1];
            }
            graph.AxisChange();
            graph.Invalidate();
            graph.Refresh();
        }

        private void UpdateForm()
        {/*
            Blocks.Process();
            UpdateProcessingStepsList();

            var firstStep = Blocks.FirstOrDefault();
            if (firstStep != null)
                UpdateGraph(firstStep.Signal, OriginalSignalGraph);
            else
                OriginalSignalGraph.GraphPane.CurveList.Clear();
            
            var lastStep = Blocks.LastOrDefault();
            if (lastStep != null)
                UpdateGraph(lastStep.Signal, CreatedSignalGraph);
            else
                CreatedSignalGraph.GraphPane.CurveList.Clear();*/
        }

        private void UpdateProcessingStepsList()
        {
            StepsComposite.Items.Clear();
            foreach (var step in Blocks)
            {
                var item = QControlUtils.CreateCompositeListItem(step.Id.ToString(), step.Name.ToLower().Replace(" ", ""), step.Name, step.Description.Replace(";", "\r\n"), 1, QPartDirection.Horizontal, QPartAlignment.Near, Color.White, 60, 45);
                item.ItemActivated += StepSelected;
                StepsComposite.Items.Add(item);
            }
        }

        private void ShowSignalCheckBoxCheckedChanged(object sender, EventArgs e)
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

        private void StepSelected(object sender, QCompositeEventArgs args)
        {
            var stepId = Guid.Parse(((QCompositeItem)sender).ItemName);
            var step = Blocks.FirstOrDefault(it => it.Id == stepId);
            var previousStep = Blocks.FirstOrDefault(it => it.Index == step.Index - 1);

            if (step is GenerateSignalBlock)
            {
                ShowSignalGenerationForm(null, true);
            }
            else if (step.ProcessingType == BlockBase.ProcessingTypeEnum.Operation)
            {
                ShowOperationForm(null, true, step, previousStep);
            }
        }
    }
}

