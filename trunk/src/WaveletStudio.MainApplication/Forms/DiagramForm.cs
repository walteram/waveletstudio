using System;
using System.Drawing;
using System.Drawing.Imaging;
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
        private readonly DiagramFormProperties _propertiesWindow;
        private readonly DiagramFormOutput _outputWindow;
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

        public string CurrentDirectory
        {
            get { return string.IsNullOrEmpty(_currentFile) ? Utils.AssemblyDirectory : Path.GetDirectoryName(_currentFile);}
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
            _outputWindow = new DiagramFormOutput {Owner = this, Size = new Size(300, 300)};
            _outputWindow.DockWindow(RightDockBar);
            _propertiesWindow = new DiagramFormProperties {Owner = this, Size = new Size(300, 300)};
            _propertiesWindow.DockWindow(RightDockBar);
            _propertiesWindow.PropertyGrid.PropertyValueChanged += (o, args) => _outputWindow.BlockPlot.Refresh();
            _outputWindow.DockWindow(RightDockBar);
            _propertiesWindow.DockWindow(_outputWindow, QDockOrientation.Vertical, true);

            Designer.ElementDoubleClick += DesignerElementDoubleClick;
            Designer.ElementMoved += DesignerElementMoved;
            Designer.ElementResized += DesignerElementResized;
            Designer.ElementConnected += DesignerElementConnected;
            Designer.ElementSelection += DesignerElementSelection;
        }
    
        private void DiagramFormLoad(object sender, EventArgs e)
        {
            ConfigureDesigner();
            LoadRibbon();            
            WindowState = FormWindowState.Maximized;
            AppMenuButton.Visible = true;                                   
        }

        private void ConfigureDesigner()
        {
            Designer.Document.GridSize = new Size(10, 10);
            Designer.Document.PropertyChanged += PropertyChanged;
            Designer.Document.ZoomChanged += ZoomChanged;
            Designer.Document.ElementPropertyChanged += PropertyChanged;
        }

        private void LoadRibbon()
        {
            AppMenuButton.CustomChildWindow = new DiagramFormMainMenu(this);
            LoadBlocks(SignalSourceComposite, BlockBase.ProcessingTypeEnum.LoadSignal);
            LoadSignalTemplates();
            LoadBlocks(SignalTemplatesComposite, BlockBase.ProcessingTypeEnum.CreateSignal);
            LoadBlocks(OperationsFunctionsComposite, BlockBase.ProcessingTypeEnum.Operation);
            LoadBlocks(RoutingComposite, BlockBase.ProcessingTypeEnum.Routing);
            LoadBlocks(TransformsComposite, BlockBase.ProcessingTypeEnum.Transform);
            LoadBlocks(ExportToFileComposite, BlockBase.ProcessingTypeEnum.Export);
            Ribbon.ActivateNextTabPage(true);
            Ribbon.Refresh();
            Ribbon.ActivatePreviousTabPage(true);
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
            block.CurrentDirectory = CurrentDirectory;
            Designer.Document.Action = DesignerAction.Connect;
            Designer.Document.LinkType = LinkType.RightAngle;
            var diagramBlock = new DiagramBlock(ApplicationUtils.GetResourceImage("img" + block.Name.ToLower() + "mini", 30, 20), ApplicationUtils.GetResourceString(block.Name), block, block.InputNodes.ToArray(), block.OutputNodes.ToArray(), typeof(BlockOutputNode).GetProperty("ShortName"));
            Designer.Document.AddElement(diagramBlock);
            Saved = false;
        }

        private void LoadBlocks(QCompositeItemBase compositeGroup, BlockBase.ProcessingTypeEnum processingType)
        {
            foreach (var type in Utils.GetTypes("WaveletStudio.Blocks").Where(t => t.BaseType == typeof(BlockBase)).OrderBy(BlockBase.GetName))
            {
                var block = (BlockBase)Activator.CreateInstance(type);
                block.CurrentDirectory = CurrentDirectory;
                if (block.ProcessingType != processingType || type == typeof(GenerateSignalBlock))
                    continue;

                var item = QControlUtils.CreateCompositeListItem(type.FullName, "img" + block.Name.ToLower(), ApplicationUtils.GetResourceString(block.Name), "", 1, QPartDirection.Vertical, QPartAlignment.Centered, Color.White);
                
                item.ItemActivated += (sender, args) => CreateBlock(((QCompositeItem)sender).ItemName);
                compositeGroup.Items.Add(item);
            }
        }

        private void ZoomChanged(object sender, EventArgs e)
        {
            var zoomValue = Convert.ToInt32(Designer.Document.Zoom*100);
            ZoomTrackBar.Value = zoomValue;
            ZoomLabel.Text = zoomValue + @"%";
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
                AddRecentFile(openDialog.FileName);
            }
            catch (Exception exception)
            {
                MessageBox.Show(@"The file couldn't be opened:" + Environment.NewLine + exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            diagramForm.ConfigureDesigner();
            AddRecentFile(filename);

            var blockList = new BlockList();
            foreach (var element in diagramForm.Designer.Document.Elements)
            {
                if(element is DiagramBlock == false)
                    continue;

                var block = (BlockBase)((DiagramBlock)element).State;
                block.CurrentDirectory = CurrentDirectory;
                blockList.Add(block);
            }   
            blockList.ExecuteAll();
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
                MessageBox.Show(@"The file couldn't be opened:" + Environment.NewLine + exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportImage()
        {
            var saveDialog = new SaveFileDialog
            {
                SupportMultiDottedExtensions = false,
                Filter = @"PNG|*.png|JPG|*.jpg|BMP|*.bmp|EMF|*.emf|EXIF|*.exif|GIF|*.gif|TIFF|*.tiff|WMF|*.wmf",
                RestoreDirectory = true,
                AddExtension = true
            };
            if (saveDialog.ShowDialog(this) != DialogResult.OK)
                return;
            try
            {
                var extension = (Path.GetExtension(saveDialog.FileName) ?? "").Replace(".","").ToLower();
                var format = ImageFormat.Png;
                if (extension == "jpg" || extension == "jpeg")
                    format = ImageFormat.Jpeg;
                else if (extension == "bmp")
                    format = ImageFormat.Bmp;
                else if (extension == "emf")
                    format = ImageFormat.Emf;
                else if (extension == "exif")
                    format = ImageFormat.Exif;
                else if (extension == "gif")
                    format = ImageFormat.Gif;
                else if (extension == "tiff" || extension == "tif")
                    format = ImageFormat.Tiff;
                else if (extension == "wmf" || extension == "wmf")
                    format = ImageFormat.Wmf;

                
                var bmp = Designer.GetImage(false, format == ImageFormat.Gif || format == ImageFormat.Bmp);
                var encoder = ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == format.Guid);
                if (encoder != null && format != ImageFormat.Emf && format != ImageFormat.Wmf)
                {
                    var encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                    bmp.Save(saveDialog.FileName, encoder, encoderParameters);
                }
                else
                {
                    bmp.Save(saveDialog.FileName, format);   
                }                
            }
            catch (Exception exception)
            {
                MessageBox.Show(@"The file couldn't be opened:" + Environment.NewLine + exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddRecentFile(string filename)
        {
            if ((Path.GetDirectoryName(filename) + "").ToLower() == Utils.AssemblyDirectory.ToLower())
                filename = Path.GetFileName(filename) + "";
            if (Settings.Default.RecentFileList.Contains(filename))
                Settings.Default.RecentFileList.Remove(filename);
            Settings.Default.RecentFileList.Insert(0, filename);
            Settings.Default.Save();
        }

        private void DiagramFormFormClosing(object sender, FormClosingEventArgs e)
        {
            AppContext.Context.FormClosing(this);
        }

        private void RibbonHelpButtonActivated(object sender, EventArgs e)
        {
            const string target = "http://www.waveletstudio.net";
            try
            {
                System.Diagnostics.Process.Start(target);
            }
            catch(Exception)
            {
                MessageBox.Show(@"You can get help in this application at the folowing site: " + '\n' + target, @"Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DiagramFormKeyUp(object sender, KeyEventArgs e)
        {
            var menu = (DiagramFormMainMenu) AppMenuButton.CustomChildWindow;
            if (!e.Control) return;
            if (e.KeyCode == Keys.N)
                menu.NewMenuItemItemActivated(sender, null);
            else if (e.KeyCode == Keys.O)
                menu.OpenMenuItemItemActivated(sender, null);
            else if (e.KeyCode == Keys.S && e.Shift)
                menu.SaveAsMenuItemItemActivated(sender, null);
            else if (e.KeyCode == Keys.S)
                menu.SaveMenuItemItemActivated(sender, null);
            else if (e.KeyCode == Keys.OemMinus)
                Designer.Document.Zoom -= 0.1f;
            else if (e.KeyCode == Keys.Oemplus)
                Designer.Document.Zoom += 0.1f;
        }

        private void ZoomMinusButtonClick(object sender, EventArgs e)
        {
            ZoomTrackBar.Value -= 10;
        }

        private void ZoomTrackBarValueChanged(object sender, EventArgs e)
        {
            Designer.Document.Zoom = ZoomTrackBar.Value / 100f;
            ZoomLabel.Text = ZoomTrackBar.Value + @"%";
        }

        private void ZoomPlusButtonClick(object sender, EventArgs e)
        {
            ZoomTrackBar.Value += 10;
        }

        private void DesignerElementConnected(object sender, ElementConnectEventArgs e)
        {
            Saved = false;
            var node1 = (BlockNodeBase)e.Link.Connector1.State;
            var node2 = (BlockNodeBase)e.Link.Connector2.State;
            node1.ConnectTo(ref node2);
            try
            {
                node1.Root.Execute();
                Designer.Document.SelectElement(e.Link.Connector2);
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
            var diagramBlock = (DiagramBlock)e.Element;
            var block = (BlockBase)diagramBlock.State;
            block.CurrentDirectory = CurrentDirectory;

            BlockSetupBaseForm setupForm;
            if (block.ProcessingType == BlockBase.ProcessingTypeEnum.Export)
                setupForm = new BlockSetupTextForm(ApplicationUtils.GetResourceString(block.Name), ref block);
            else
                setupForm = new BlockSetupPlotForm(ApplicationUtils.GetResourceString(block.Name), ref block);
            setupForm.ShowDialog(this);
            if (setupForm.DialogResult != DialogResult.OK)
                return;

            _propertiesWindow.PropertyGrid.SelectedObject = setupForm.Block;
            _propertiesWindow.PropertyGrid.Refresh();
            _outputWindow.BlockPlot.Block = setupForm.Block;
            _outputWindow.BlockPlot.Refresh();
            diagramBlock.Refresh(ApplicationUtils.GetResourceImage("img" + setupForm.Block.Name.ToLower() + "mini", 30, 20), ApplicationUtils.GetResourceString(setupForm.Block.Name), setupForm.Block, setupForm.Block.InputNodes.ToArray(), setupForm.Block.OutputNodes.ToArray(), typeof(BlockOutputNode).GetProperty("ShortName"));
            if (setupForm.InputConnectionsChanged || setupForm.OutputConnectionsChanged)
            {
                var links = Designer.Document.Elements.GetArray();
                foreach (var element in links)
                {
                    var link = element as BaseLinkElement;
                    if (link == null) 
                        continue;
                    if (link.Connector1.ParentElement == diagramBlock || link.Connector2.ParentElement == diagramBlock)  //if (setupForm.OutputConnectionsChanged && link.Connector1.ParentElement == diagramBlock || setupForm.InputConnectionsChanged && link.Connector2.ParentElement == diagramBlock)
                    {
                        Designer.Document.DeleteLink(link);
                    }
                }
            }            
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

        private void DesignerElementSelection(object sender, ElementSelectionEventArgs e)
        {
            if (e.Elements.Count == 0)
                return;

            foreach (var element in e.Elements)
            {
                if (!(element is DiagramBlock))
                    continue;
                var diagramBlock = (DiagramBlock)element;
                var block = (BlockBase)diagramBlock.State;
                _propertiesWindow.PropertyGrid.SelectedObject = block;
                _propertiesWindow.PropertyGrid.Refresh();
                _outputWindow.BlockPlot.Block = block;
                _outputWindow.BlockPlot.Refresh();
                break;
            }
        }

        private void PropertyChanged(object sender, EventArgs e)
        {
            Saved = false;
        }        
    }
}

