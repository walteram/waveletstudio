/*  Wavelet Studio Signal Processing Library - www.waveletstudio.net
    Copyright (C) 2011, 2012 Walter V. S. de Amorim - The Wavelet Studio Initiative

    Wavelet Studio is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Wavelet Studio is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DiagramNet;
using DiagramNet.Elements;
using DiagramNet.Events;
using Qios.DevSuite.Components;
using Qios.DevSuite.Components.Ribbon;
using WaveletStudio.Blocks;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.Designer.Documents;
using WaveletStudio.Designer.Properties;
using WaveletStudio.Designer.Resources;
using WaveletStudio.Designer.Utils;
using WaveletStudio.SignalGeneration;

namespace WaveletStudio.Designer.Forms
{
    public partial class DiagramForm : QRibbonForm
    {
        private readonly DiagramFormProperties _propertiesWindow;
        private readonly DiagramFormOutput _outputWindow;
        private readonly FileForm _fileForm;
        private PrinterSettings _printerSettings;

        public DocumentModel DocumentModel { get; private set; }

        private string _currentFile;
        public string CurrentFile
        {
            get { return _currentFile; }
            set 
            {
                _currentFile = value;
                UpdateFormTitle();
            }
        }

        public string CurrentDirectory
        {
            get { return string.IsNullOrEmpty(_currentFile) ? WaveletStudio.Utils.AssemblyDirectory : Path.GetDirectoryName(_currentFile);}
        }

        public DiagramForm()
        {
            InitializeComponent();
            AppMenuButton.Visible = false;
            CurrentFile = string.Empty;
            _outputWindow = new DiagramFormOutput {Owner = this, Size = new Size(300, 300)};
            _outputWindow.DockWindow(RightDockBar);
            _propertiesWindow = new DiagramFormProperties {Owner = this, Size = new Size(300, 300)};
            _propertiesWindow.DockWindow(RightDockBar);
            _propertiesWindow.PropertyGrid.PropertyValueChanged += (o, args) => _outputWindow.Refresh();
            _propertiesWindow.OnPropertyChanged += PropertyValueChanged;
            _outputWindow.DockWindow(RightDockBar);
            _propertiesWindow.DockWindow(_outputWindow, QDockOrientation.Vertical, true);
            _fileForm = new FileForm(this);
            
            Designer.ElementDoubleClick += DesignerElementDoubleClick;
            Designer.ElementMoved += DesignerElementMoved;
            Designer.ElementResized += DesignerElementResized;
            Designer.ElementConnected += DesignerElementConnected;
            Designer.ElementSelection += DesignerElementSelection;
            Designer.LinkRemoved += DesignerLinkRemoved;

            DocumentModel = new DocumentModel
            {
                Document = Designer.Document,
                CreatedAt = DateTime.Now,
                Author = new CurrentUserDiscoverer().Discover(),
                OnSaveChanged = OnSavedChanged
            };
        }

        private void OnSavedChanged()
        {
            UpdateFormTitle();
            UpdateUndoAndRedo();
        }

        private void UpdateUndoAndRedo()
        {
            UndoShortcut.Enabled = Designer.CanUndo;
            RedoShortcut.Enabled = Designer.CanRedo;
        }

        public void UpdateFormTitle()
        {
            var isSaved = DocumentModel == null || DocumentModel.Saved;
            Text = DesignerResources.WaveletStudio + @" ─ " + 
                   (string.IsNullOrEmpty(CurrentFile) ? DesignerResources.NewDocument : Path.GetFileName(CurrentFile)) +
                   (!isSaved ? DesignerResources.NewDocumentChar : "");            
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
            //AppMenuButton.CustomChildWindow = new FileForm(this); //new DiagramFormMainMenu(this);
            LoadBlocks(SignalRibbonPage, BlockBase.ProcessingTypeEnum.LoadSignal);
            LoadSignalTemplates();
            LoadBlocks(OperationsRibbonPage, BlockBase.ProcessingTypeEnum.Operation);
            LoadBlocks(RoutingRibbonPage, BlockBase.ProcessingTypeEnum.Routing);
            LoadBlocks(RoutingRibbonPage, BlockBase.ProcessingTypeEnum.Logic, true);
            LoadBlocks(TransformsRibbonPage, BlockBase.ProcessingTypeEnum.Transform);
            LoadBlocks(ExportDataRibbonPage, BlockBase.ProcessingTypeEnum.Export);
            foreach (var tabPage in Ribbon.Controls.OfType<QRibbonPage>())
            {
                tabPage.Text = tabPage.Text.ToUpperInvariant();
            }
            Ribbon.ActivateNextTabPage(true);
            Ribbon.Refresh();
            Ribbon.ActivatePreviousTabPage(true);
        }

        private void LoadSignalTemplates()
        {
            var composite = SignalRibbonPage.CreateCompositeGroup(DesignerResources.SignalTemplates, true);
            foreach (var type in WaveletStudio.Utils.GetTypes("WaveletStudio.SignalGeneration"))
            {
                var signal = (CommonSignalBase)Activator.CreateInstance(type);
                var item = QControlUtils.CreateCompositeItem(type.Name, "img" + signal.GetAssemblyClassName(), signal.Name);
                item.ItemActivated += (sender, args) => CreateSignalGenerationBlock(((QCompositeItem)sender).ItemName);
                composite.Items.Add(item);
            }
            LoadBlocks(composite, BlockBase.ProcessingTypeEnum.CreateSignal);
        }

        private void CreateSignalGenerationBlock(string templateName)
        {
            var block = new GenerateSignalBlock { TemplateName = templateName };
            Designer.Document.Action = DesignerAction.Connect;
            Designer.Document.LinkType = LinkType.RightAngle;
            var diagramBlock = new DiagramBlock(ApplicationUtils.GetResourceImage("img" + templateName + "Mini", 30, 20), block.Name, block, block.InputNodes.ToArray(), block.OutputNodes.ToArray(), typeof(BlockOutputNode).GetProperty("ShortName"));
            Designer.Document.AddElement(diagramBlock);
            DocumentModel.Touch();            
        }

        private void CreateBlock(string itemName)
        {
            var type = WaveletStudio.Utils.GetType(itemName);
            var block = (BlockBase)Activator.CreateInstance(type);
            block.CurrentDirectory = CurrentDirectory;
            Designer.Document.Action = DesignerAction.Connect;
            Designer.Document.LinkType = LinkType.RightAngle;
            var diagramBlock = new DiagramBlock(ApplicationUtils.GetResourceImage("img" + block.GetAssemblyClassName() + "Mini", 30, 20), ApplicationUtils.GetResourceString(block.Name), block, block.InputNodes.ToArray(), block.OutputNodes.ToArray(), typeof(BlockOutputNode).GetProperty("ShortName"));
            Designer.Document.AddElement(diagramBlock);
            Designer.Document.ClearSelection();
            Designer.Document.SelectElement(diagramBlock);
            if (block.CausesRefresh)
            {
                RefreshSelectedDiagramBlock();
            }
            DocumentModel.Touch();            
        }

        private void LoadBlocks(QRibbonPage page, BlockBase.ProcessingTypeEnum processingType, bool createSeparatorBefore = false)
        {
            var title = DesignerResources.ResourceManager.GetString(BlockBase.GetProcessingTypeName(processingType));
            var composite = page.CreateCompositeGroup(title, createSeparatorBefore);
            LoadBlocks(composite, processingType);
        }

        private void LoadBlocks(QCompositeItemBase composite, BlockBase.ProcessingTypeEnum processingType)
        {
            foreach (var type in WaveletStudio.Utils.GetTypes("WaveletStudio.Blocks").Where(t => t.BaseType == typeof (BlockBase)).OrderBy(BlockBase.GetName))
            {
                var block = (BlockBase) Activator.CreateInstance(type);
                block.CurrentDirectory = CurrentDirectory;
                if (block.ProcessingType != processingType || type == typeof (GenerateSignalBlock))
                {
                    continue;
                }
                var item = QControlUtils.CreateCompositeItem(type.FullName, "img" + block.GetAssemblyClassName(), ApplicationUtils.GetResourceString(block.Name));
                item.ItemActivated += (sender, args) => CreateBlock(((QCompositeItem) sender).ItemName);
                composite.Items.Add(item);
            }
        }

        private void ZoomChanged(object sender, EventArgs e)
        {
            var zoomValue = Convert.ToInt32(Designer.Document.Zoom*10);
            ZoomTrackBar.Value = zoomValue;
            ZoomLabel.Text = string.Format("{0}%", (zoomValue*10));
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

        private void UndoShortcutItemActivated(object sender, QCompositeEventArgs e)
        {
            if (Designer.CanUndo)
                Designer.Undo();
            DocumentModel.Touch();            
        }

        private void RedoShortcutItemActivated(object sender, QCompositeEventArgs e)
        {
            if (Designer.CanRedo)
                Designer.Redo();
            DocumentModel.Touch();            
        }

        public bool Save()
        {
            if (string.IsNullOrEmpty(CurrentFile))
            {
                return SaveAs();                
            }
            try
            {
                new DocumentSerializer().Save(DocumentModel, CurrentFile);
                AddRecentFile(CurrentFile, Designer);
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(string.Format("{0}: \r\n{1}", DesignerResources.FileCouldntBeSaved, exception.Message), DesignerResources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void DiagramFormFormClosing(object sender, FormClosingEventArgs e)
        {
            if (Designer.Document.Elements.Count > 0 && !DocumentModel.Saved)
            {
                var questionResult = MessageBox.Show(DesignerResources.SaveChangesQuestion, DesignerResources.Attention, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (questionResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                if (questionResult == DialogResult.Yes)
                {
                    Save();
                }
            }
            _fileForm.Close();
            AppContext.Context.FormClosing(this);
        }

        public void New()
        {
            var diagramForm = new DiagramForm();
            diagramForm.Show();
            diagramForm.Focus();
        }

        public void OpenFileByPath(string filename)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (!Path.IsPathRooted(filename))
                {
                    filename = Path.Combine(WaveletStudio.Utils.AssemblyDirectory, filename);
                }

                var documentModel = new DocumentSerializer().Load(filename);
                documentModel.OnSaveChanged = OnSavedChanged;

                var diagramForm = Designer.Document.Elements.Count > 0 ? new DiagramForm() : this;
                diagramForm.Cursor = Cursors.WaitCursor;
                diagramForm.DocumentModel = documentModel;                
                diagramForm.Designer.SetDocument(documentModel.Document);
                diagramForm.CurrentFile = filename;
                diagramForm.Show();
                diagramForm.Focus();
                diagramForm.ConfigureDesigner();
                diagramForm.ZoomTrackBar.Value = Convert.ToInt32(Designer.Document.Zoom * 10);
                AddRecentFile(filename, diagramForm.Designer);

                var blockList = new BlockList();
                foreach (var element in diagramForm.Designer.Document.Elements)
                {
                    if (element is DiagramBlock == false)
                        continue;

                    var block = (BlockBase)((DiagramBlock)element).State;
                    block.CurrentDirectory = CurrentDirectory;
                    blockList.Add(block);
                }
                blockList.ExecuteAll();
                diagramForm.Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                MessageBox.Show(string.Format("{0}:{1}{2}", DesignerResources.FileCouldntBeOpened, Environment.NewLine, exception.Message), DesignerResources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        public bool SaveAs()
        {
            var saveDialog = new SaveFileDialog
            {
                SupportMultiDottedExtensions = true,
                Filter = string.Format("{0}|*.wsd", DesignerResources.WaveletStudioDocument), //|Wavelet Studio XML Document|*.wsx",
                RestoreDirectory = true
            };
            if (saveDialog.ShowDialog(this) != DialogResult.OK)
            {
                return false;
            }
            try
            {
                var serializer = new DocumentSerializer();
                serializer.Save(DocumentModel, saveDialog.FileName);
                CurrentFile = saveDialog.FileName;                
                AddRecentFile(CurrentFile, Designer);
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(string.Format("{0}:{1}{2}", DesignerResources.FileCouldntBeSaved, Environment.NewLine, exception.Message), DesignerResources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool ExportImage()
        {
            var saveDialog = new SaveFileDialog
            {
                SupportMultiDottedExtensions = false,
                Filter = string.Format("PNG|*.png|JPG|*.jpg|BMP|*.bmp|EMF|*.emf|EXIF|*.exif|GIF|*.gif|TIFF|*.tiff|WMF|*.wmf"),
                RestoreDirectory = true,
                AddExtension = true
            };
            if (saveDialog.ShowDialog(this) != DialogResult.OK)
            {
                return false;
            }
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
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(string.Format("{0}:{1}{2}", DesignerResources.FileCouldntBeSaved, Environment.NewLine, exception.Message), DesignerResources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void Print(Form mainForm, bool print)
        {
            var printDialog = new PrintDialog { Document = GetPrintDocument() };
            var dialogResult = printDialog.ShowDialog(mainForm);
            if (dialogResult != DialogResult.OK)
                return;
            _printerSettings = printDialog.PrinterSettings;
            if(print)
                printDialog.Document.Print();                
        }

        public void PageSetup(Form mainForm)
        {
            var printDialog = new PageSetupDialog { Document = GetPrintDocument() };
            printDialog.ShowDialog(mainForm);
        }

        public void PrintPreview()
        {
            var printPreviewForm = new PrintPreviewForm(this);
            printPreviewForm.ShowDialog(this);
        }

        public PrintDocument GetPrintDocument(Action onEndPrint = null)
        {
            const decimal millimeters = 39.37m;
            var document = new PrintDocument { OriginAtMargins = true};
            if (_printerSettings != null)
            {
                document.PrinterSettings = _printerSettings;
            }
            document.DefaultPageSettings.Landscape = Settings.Default.PrintLandscape;
            document.DefaultPageSettings.Margins = new Margins(Convert.ToInt32(Settings.Default.PrintMarginLeft * millimeters), Convert.ToInt32(Settings.Default.PrintMarginRight * millimeters), Convert.ToInt32(Settings.Default.PrintMarginTop * millimeters), Convert.ToInt32(Settings.Default.PrintMarginBottom * millimeters));
            document.PrintPage += (o, args) =>
                                      {
                                          args.HasMorePages = Designer.DrawGraphics(args.Graphics, Settings.Default.PrintGrid, false, args.MarginBounds.Left, args.MarginBounds.Top, args.MarginBounds.Width, args.MarginBounds.Height, true, Settings.Default.PrintAllowStretch, 0);
                                      };
            if (onEndPrint != null)
                document.EndPrint += (e, p) => onEndPrint();
            return document;
        }

        private void AddRecentFile(string filename, DiagramNet.Designer designer)
        {      
            var recentFileList = RecentFileList.ReadFromFile();
            recentFileList.RemoveAll(f => f.FilePath.ToLower() == filename.ToLower());
            recentFileList.Add(new RecentFile
            {
                FilePath = filename,
                Thumbnail = designer.GetThumbnail(),
                DateAdded = DateTime.Now
            });
            while (recentFileList.Count > 15)
            {
                recentFileList.RemoveAt(0);
            }
            recentFileList.SaveToFile();
        }        

        private void RibbonHelpButtonActivated(object sender, EventArgs e)
        {
            try
            {
                Process.Start(DesignerResources.SiteUrl);
            }
            catch(Exception)
            {
                MessageBox.Show(string.Format("{0}:\n{1}", DesignerResources.HelpLinkMessage, DesignerResources.SiteUrl), DesignerResources.Help, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DiagramFormKeyUp(object sender, KeyEventArgs e)
        {
            if (!e.Control)
            {
                return;
            }
            if (e.KeyCode == Keys.P)
            {
                _fileForm.InitPrint();
                _fileForm.ShowDialog();
            }
            else if (e.KeyCode == Keys.N)
            {
                var diagramForm = new DiagramForm();
                diagramForm.Show();
            }
            else if (e.KeyCode == Keys.O)
            {
                _fileForm.InitOpen();
                _fileForm.ShowDialog();
            }
            else if (e.KeyCode == Keys.S && e.Shift)
            {
                SaveAs();
            }
            else if (e.KeyCode == Keys.S)
            {
                Save();
            }
            else if (e.KeyCode == Keys.OemMinus)
            {
                Designer.Document.Zoom -= 0.1f;
            }
            else if (e.KeyCode == Keys.Oemplus)
            {
                Designer.Document.Zoom += 0.1f;
            }
        }

        private void ZoomMinusButtonClick(object sender, EventArgs e)
        {
            if (ZoomTrackBar.Value > 1)
            ZoomTrackBar.Value -= 1;
        }

        private void ZoomTrackBarValueChanged(object sender, EventArgs e)
        {
            Designer.Document.Zoom = ZoomTrackBar.Value / 10f;
            ZoomLabel.Text = string.Format("{0}%", (ZoomTrackBar.Value*10));
        }

        private void ZoomPlusButtonClick(object sender, EventArgs e)
        {
            if (ZoomTrackBar.Value<30)
            ZoomTrackBar.Value += 1;
        }
        
        private void DesignerLinkRemoved(object sender, ElementEventArgs e)
        {
            var node1 = (BlockNodeBase)((BaseLinkElement)e.Element).Connector1.State;
            var node2 = (BlockNodeBase)((BaseLinkElement)e.Element).Connector2.State;
            node1.ConnectingNode = null;
            if (node2 != null)
            {
                node2.ConnectingNode = null;
                foreach (var node in node2.Root.OutputNodes)
                {
                    node.Object.Clear();
                }
            }            
            node1.Root.Execute();
            if (node2 != null)
            {
                node2.Root.Execute();
            }
            _outputWindow.Refresh();
        }

        private void DesignerElementConnected(object sender, ElementConnectEventArgs e)
        {
            DocumentModel.Touch();            
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
                MessageBox.Show(DesignerResources.ModelThrewStackOverflow);
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
            _outputWindow.Block = setupForm.Block;
            _outputWindow.Refresh();
            diagramBlock.Refresh(ApplicationUtils.GetResourceImage("img" + setupForm.Block.GetAssemblyClassName() + "Mini", 30, 20), ApplicationUtils.GetResourceString(setupForm.Block.Name), setupForm.Block, setupForm.Block.InputNodes.ToArray(), setupForm.Block.OutputNodes.ToArray(), typeof(BlockOutputNode).GetProperty("ShortName"));
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
                        Designer.Document.DeleteLink(link, false);
                    }
                }
            }            
            diagramBlock.Invalidate();
            diagramBlock.State = setupForm.Block;
            DocumentModel.Touch();            
        }

        private void PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            var descriptor = e.ChangedItem.PropertyDescriptor as GlobalizedPropertyDescriptor;
            if (descriptor == null || !descriptor.CausesRefresh || Designer.Document.SelectedElements.Count == 0)
            {
                return;
            }
            RefreshSelectedDiagramBlock();
        }

        private void RefreshSelectedDiagramBlock()
        {
            var diagramBlock = Designer.Document.SelectedElements[0] as DiagramBlock;
            if (diagramBlock == null)
            {
                return;
            }
            var block = (BlockBase)diagramBlock.State;
            block.CurrentDirectory = CurrentDirectory;
            diagramBlock.Refresh(ApplicationUtils.GetResourceImage("img" + block.GetAssemblyClassName() + "Mini", 30, 20), ApplicationUtils.GetResourceString(block.Name), block, block.InputNodes.ToArray(), block.OutputNodes.ToArray(), typeof(BlockOutputNode).GetProperty("ShortName"));
        }

        private void DesignerElementMoved(object sender, ElementEventArgs e)
        {
            DocumentModel.Touch();            
        }

        private void DesignerElementResized(object sender, ElementEventArgs e)
        {
            DocumentModel.Touch();
        }

        private void DesignerElementSelection(object sender, ElementSelectionEventArgs e)
        {
            if (e.Elements.Count == 0)
            {
                _propertiesWindow.PropertyGrid.SelectedObject = null;
                _propertiesWindow.PropertyGrid.Refresh();
                _outputWindow.Block = null;
                _outputWindow.Refresh();
                return;
            }

            foreach (var element in e.Elements)
            {
                if (!(element is DiagramBlock))
                    continue;
                var diagramBlock = (DiagramBlock)element;
                var block = (BlockBase)diagramBlock.State;
                _propertiesWindow.PropertyGrid.SelectedObject = block;
                _propertiesWindow.PropertyGrid.Refresh();
                _outputWindow.Block = block;
                _outputWindow.Refresh();
                break;
            }
        }

        private void PropertyChanged(object sender, EventArgs e)
        {
            DocumentModel.Touch();           
        }

        public bool ExportCode()
        {
            var saveDialog = new SaveFileDialog
            {
                SupportMultiDottedExtensions = true,
                Filter = string.Format("{0}|*.cs", DesignerResources.CSharpFile),
                RestoreDirectory = true
            };
            var className = Path.GetFileNameWithoutExtension(CurrentFile).RemoveSpecialChars();
            if (string.IsNullOrEmpty(saveDialog.FileName))
            {
                saveDialog.FileName = className + ".cs";
            }
            if (saveDialog.ShowDialog(this) != DialogResult.OK)
            {
                return false;
            }
            try
            {
                var filename = "Model" + className;
                var codeGenerator = new CodeGenerator
                {
                    BlockList = Designer.Document.Elements.GetArray().OfType<DiagramBlock>().Select(it => it.State).OfType<BlockBase>().ToList(),
                    ClassName = filename
                };
                var code = codeGenerator.GenerateCode();                
                File.WriteAllText(saveDialog.FileName, code);
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(string.Format("{0}:{1}{2}", DesignerResources.FileCouldntBeSaved, Environment.NewLine, exception.Message), DesignerResources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void AppMenuButton_ItemActivated(object sender, QCompositeEventArgs e)
        {
            _fileForm.InitInformations();
            _fileForm.ShowDialog();
            if (_fileForm.ActionResult == FileForm.ActionTaken.New)
            {
                New();
            }
            else if (_fileForm.ActionResult == FileForm.ActionTaken.Open)
            {
                OpenFileByPath(_fileForm.FilePath);
            }
        }

        private void DiagramForm_TextChanged(object sender, EventArgs e)
        {

        }

        private void qRibbonCaption1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

