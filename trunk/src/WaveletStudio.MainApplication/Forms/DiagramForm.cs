using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DiagramNet;
using DiagramNet.Elements;
using DiagramNet.Events;
using Qios.DevSuite.Components;
using Qios.DevSuite.Components.Ribbon;
using WaveletStudio.Blocks;
using WaveletStudio.MainApplication.Controls;
using WaveletStudio.MainApplication.Properties;
using WaveletStudio.SignalGeneration;

namespace WaveletStudio.MainApplication.Forms
{
    public partial class DiagramForm : QRibbonForm
    {
        public bool FirstWindow { get; set; }

        private string _currentFile;
        public string CurrentFile
        {
            get { return _currentFile; }
            set
            {
                Text = @"Wavelet Studio - " + (string.IsNullOrEmpty(value) ? "New Document" : Path.GetFileName(value)) + (!Saved ? "*" : "");
                _currentFile = value;
            }
        }

        private bool _saved;
        public bool Saved
        {
            get { return _saved; }
            set
            {
                _saved = value;
                if (_saved && Text.EndsWith("*"))
                    Text = Text.Substring(0, Text.Length - 1);
                else if(!_saved  && !Text.EndsWith("*"))
                    Text = Text + @"*";
                UpdateActions();
            }
        }

        public DiagramForm()
        {
            InitializeComponent();
            AppMenuButton.Visible = false;
            CurrentFile = "";
            ConfigureDesigner();
        }
        
        private void DiagramFormLoad(object sender, EventArgs e)
        {            
            LoadRibbon();
            WindowState = FormWindowState.Maximized;
            AppMenuButton.Visible = true;
            if(!FirstWindow)
                return;

            if (Settings.Default["RecentFileList"] == null)
            {
                Settings.Default["RecentFileList"] = "";
                Settings.Default.Save();                
            }
            var recentFiles = Settings.Default.RecentFileList.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            var lastFile = recentFiles.Count() > 0 ? recentFiles[0] : "";
            if (!Settings.Default.AutoLoadLastFile || lastFile == "" || !File.Exists(lastFile)) 
                return;
            try
            {
                OpenFile(lastFile);
            }
            catch (Exception exception)
            {
                Trace.TraceWarning(exception.Message + exception.StackTrace);
            }
        }

        private void ConfigureDesigner()
        {
            Designer.Document.GridSize = new Size(10, 10);
            Designer.Document.PropertyChanged += PropertyChanged;
            Designer.Document.ElementPropertyChanged += PropertyChanged;
        }

        private void LoadRibbon()
        {
            AppMenuButton.CustomChildWindow = new DiagramFormMainMenu(this);
            LoadBlocks(SignalSourceComposite, BlockBase.ProcessingTypeEnum.LoadSignal);
            LoadSignalTemplates();
            LoadBlocks(SignalTemplatesComposite, BlockBase.ProcessingTypeEnum.CreateSignal);
            LoadBlocks(OperationsFunctionsComposite, BlockBase.ProcessingTypeEnum.Operation);
            LoadBlocks(ExportToFileComposite, BlockBase.ProcessingTypeEnum.Export);            
        }

        private void LoadSignalTemplates()
        {            
            foreach (var type in Utils.GetTypes("WaveletStudio.SignalGeneration"))
            {
                var signal = (CommonSignalBase)Activator.CreateInstance(type);
                var item = QControlUtils.CreateCompositeListItem(type.Name, "img" + signal.Name.ToLower(), ApplicationUtils.GetResourceString(signal.Name), "", 1, QPartDirection.Vertical, QPartAlignment.Centered, Color.White);
                item.ItemActivated += (sender, args) => CreateSignalGenerationBlock(((QCompositeItem)sender).ItemName);
                SignalTemplatesComposite.Items.Add(item);
            }
        }

        private void CreateSignalGenerationBlock(string templateName)
        {
            var block = new GenerateSignalBlock { TemplateName = templateName };
            Designer.Document.Action = DesignerAction.Connect;
            Designer.Document.LinkType = LinkType.RightAngle;
            var diagramBlock = new DiagramBlock(ApplicationUtils.GetResourceImage("img" + templateName + "mini", 30, 20), templateName, block, block.InputNodes.ToArray(), block.OutputNodes.ToArray(), typeof(BlockOutputNode).GetProperty("ShortName"));
            Designer.Document.AddElement(diagramBlock);
            Saved = false;
        }

        private void CreateBlock(string itemName)
        {
            var type = Utils.GetType(itemName);
            var block = (BlockBase)Activator.CreateInstance(type);
            Designer.Document.Action = DesignerAction.Connect;
            Designer.Document.LinkType = LinkType.RightAngle;
            var diagramBlock = new DiagramBlock(ApplicationUtils.GetResourceImage("img" + block.Name.ToLower() + "mini", 30, 20), ApplicationUtils.GetResourceString(block.Name), block, block.InputNodes.ToArray(), block.OutputNodes.ToArray(), typeof(BlockOutputNode).GetProperty("ShortName"));
            Designer.Document.AddElement(diagramBlock);
            Saved = false;
        }

        private void LoadBlocks(QCompositeItemBase compositeGroup, BlockBase.ProcessingTypeEnum processingType)
        {
            foreach (var type in Utils.GetTypes("WaveletStudio.Blocks").Where(t => t.BaseType == typeof(BlockBase)))
            {
                var block = (BlockBase)Activator.CreateInstance(type);
                if (block.ProcessingType != processingType || type == typeof(GenerateSignalBlock))
                    continue;

                var item = QControlUtils.CreateCompositeListItem(type.FullName, "img" + block.Name.ToLower(), ApplicationUtils.GetResourceString(block.Name), "", 1, QPartDirection.Vertical, QPartAlignment.Centered, Color.White);
                
                item.ItemActivated += (sender, args) => CreateBlock(((QCompositeItem)sender).ItemName);
                compositeGroup.Items.Add(item);
            }
        }

        private void DesignerElementConnected(object sender, ElementConnectEventArgs e)
        {
            Saved = false;
            if (e.Link.Connector1.IsStart == e.Link.Connector2.IsStart)
            {
                Designer.Document.DeleteLink(e.Link);
                return;
            }            
            if (e.Link.Connector1.IsStart)
            {
                var con1 = e.Link.Connector1;
                e.Link.Connector1 = e.Link.Connector2;
                e.Link.Connector2 = con1;
                e.Link.Invalidate();
            }
            for (var i = Designer.Document.Elements.Count - 1; i >= 0; i--)
            {
                if (e.Link == Designer.Document.Elements[i] || !(Designer.Document.Elements[i] is BaseLinkElement))
                    continue;
                var link = (BaseLinkElement)Designer.Document.Elements[i];
                if (link.Connector2 == e.Link.Connector2)
                {
                    Designer.Document.DeleteLink(link);
                }
            }

            var node1 = (BlockNodeBase) e.Link.Connector1.State;
            var node2 = (BlockNodeBase) e.Link.Connector2.State;         
            node1.ConnectTo(ref node2);
            
            try
            {
                node1.Root.Execute();
            }
            catch (StackOverflowException)
            {
                MessageBox.Show(@"The model throws a stack overflow exception.");
            }            
        }
        
        private void DesignerElementDoubleClick(object sender, ElementEventArgs e)
        {
            if (!(e.Element is DiagramBlock)) 
                return;
            var diagramBlock = (DiagramBlock) e.Element;
            var block = (BlockBase) diagramBlock.State;
            
            BlockSetupBaseForm setupForm;
            if(block.ProcessingType == BlockBase.ProcessingTypeEnum.Export)
                setupForm = new TextBlockSetupForm(ApplicationUtils.GetResourceString(block.Name), ref block);
            else
                setupForm = new BlockSetupForm(ApplicationUtils.GetResourceString(block.Name), ref block);
            setupForm.ShowDialog();
            if (setupForm.DialogResult != DialogResult.OK) 
                return;
            diagramBlock.Refresh(ApplicationUtils.GetResourceImage("img" + setupForm.Block.Name.ToLower() + "mini", 30, 20), ApplicationUtils.GetResourceString(setupForm.Block.Name), setupForm.Block, setupForm.Block.InputNodes.ToArray(), setupForm.Block.OutputNodes.ToArray(), typeof(BlockOutputNode).GetProperty("ShortName"));
            diagramBlock.Invalidate();
            diagramBlock.State = setupForm.Block;
            Saved = false;
        }

        private void DesignerElementMoved(object sender, ElementEventArgs e)
        {
            Saved = false;
        }

        private void DesignerElementResized(object sender, ElementEventArgs e)
        {
            Saved = false;
        }

        private void PropertyChanged(object sender, EventArgs e)
        {
            Saved = false;
        }

        private void CopyElementShortcutItemActivated(object sender, QCompositeEventArgs e)
        {
            Designer.Copy();
        }

        private void PasteElementShortcutItemActivated(object sender, QCompositeEventArgs e)
        {
            Designer.Paste();
        }

        private void CutElementShortcutItemActivated(object sender, QCompositeEventArgs e)
        {
            Designer.Cut();
        }

        private void UpdateActions()
        {
            UndoShortcut.Enabled = Designer.CanUndo;
            RedoShortcut.Enabled = Designer.CanRedo;
        }

        private void UndoShortcutItemActivated(object sender, QCompositeEventArgs e)
        {
            if (Designer.CanUndo)
                Designer.Undo();            
            Saved = false;
        }

        private void RedoShortcutItemActivated(object sender, QCompositeEventArgs e)
        {
            if (Designer.CanRedo)
                Designer.Redo();
            Saved = false;
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(CurrentFile))
            {
                SaveAs();
                return;
            }
            try
            {
                Designer.SaveBinary(CurrentFile);
                AddRecentFile(CurrentFile);
            }
            catch (Exception exception)
            {
                MessageBox.Show(@"The file couldn't be saved: \r\n" + exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CloseWindow()
        {
            if (Designer.Document.Elements.Count > 0 && !Saved)
            {
                var questionResult = MessageBox.Show(@"Would you like to save your changes before closing?", @"Atention",
                                                     MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (questionResult == DialogResult.Cancel)
                    return;
                if (questionResult == DialogResult.Yes)
                    Save();
            }
            Close();
        }

        public void OpenFile()
        {
            var openDialog = new OpenFileDialog
            {
                SupportMultiDottedExtensions = true,
                Filter = @"Wavelet Studio Document|*.wsd", //|Wavelet Studio XML Document|*.wsx",
                RestoreDirectory = true
            };
            if (openDialog.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                OpenFile(openDialog.FileName);
            }
            catch (Exception exception)
            {
                MessageBox.Show(@"The file couldn't be opened:" + "\r\n" + exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OpenFile(string filename)
        {
            if (!Path.IsPathRooted(filename))
                filename = Path.Combine(Utils.AssemblyDirectory, filename);
            var diagramForm = Designer.Document.Elements.Count > 0 ? new DiagramForm() : this;
            diagramForm.Designer.OpenBinary(filename);
            diagramForm.CurrentFile = filename;
            diagramForm.Show();
            diagramForm.Focus();
            AddRecentFile(filename);
        }

        public void SaveAs()
        {
            var saveDialog = new SaveFileDialog
            {
                SupportMultiDottedExtensions = true,
                Filter = @"Wavelet Studio Document|*.wsd", //|Wavelet Studio XML Document|*.wsx",
                RestoreDirectory = true
            };
            if (saveDialog.ShowDialog(this) != DialogResult.OK)
                return;
            try
            {
                Designer.SaveBinary(saveDialog.FileName);
                CurrentFile = saveDialog.FileName;
                AddRecentFile(CurrentFile);
            }
            catch (Exception exception)
            {
                MessageBox.Show(@"The file couldn't be opened:" + "\r\n" + exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddRecentFile(string filename)
        {
            if ((Path.GetDirectoryName(filename) + "").ToLower() == Utils.AssemblyDirectory.ToLower())
                filename = Path.GetFileName(filename) + "";
            if (Settings.Default.RecentFileList.Contains("|" + filename + "|"))
                Settings.Default["RecentFileList"] = Settings.Default["RecentFileList"].ToString().Replace("|" + filename + "|", "");
            Settings.Default["RecentFileList"] = "|" + filename + "|" + Settings.Default["RecentFileList"];
            Settings.Default.Save();
        }
    }
}

