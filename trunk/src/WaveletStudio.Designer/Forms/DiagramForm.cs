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
using WaveletStudio.Designer.Controls;
using WaveletStudio.Designer.Properties;
using WaveletStudio.SignalGeneration;

namespace WaveletStudio.Designer.Forms
{
    public partial class DiagramForm : QRibbonForm
    {
        private readonly DiagramFormProperties _propertiesWindow;
        private readonly DiagramFormOutput _outputWindow;
        private PrinterSettings _printerSettings;

        private string _currentFile;
        public string CurrentFile
        {
            get { return _currentFile; }
            set
            {
                Text = string.Format("{0} - {1}{2}", Resources.WaveletStudio, (string.IsNullOrEmpty(value) ? Resources.NewDocument : Path.GetFileName(value)), (!Saved ? Resources.NewDocumentChar : ""));
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
                if (_saved && Text.EndsWith(Resources.NewDocumentChar))
                    Text = Text.Substring(0, Text.Length - 1);
                else if (!_saved && !Text.EndsWith(Resources.NewDocumentChar))
                    Text = Text + Resources.NewDocumentChar;
                UpdateActions();
            }
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
            _propertiesWindow.PropertyGrid.PropertyValueChanged += (o, args) => _outputWindow.BlockPlot.Refresh();
            _propertiesWindow.OnPropertyChanged += PropertyValueChanged;
            _outputWindow.DockWindow(RightDockBar);
            _propertiesWindow.DockWindow(_outputWindow, QDockOrientation.Vertical, true);

            Designer.ElementDoubleClick += DesignerElementDoubleClick;
            Designer.ElementMoved += DesignerElementMoved;
            Designer.ElementResized += DesignerElementResized;
            Designer.ElementConnected += DesignerElementConnected;
            Designer.ElementSelection += DesignerElementSelection;
            Designer.LinkRemoved += DesignerLinkRemoved;

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
            LoadBlocks(LogicComposite, BlockBase.ProcessingTypeEnum.Logic);
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
                var item = QControlUtils.CreateCompositeListItem(type.Name, "img" + signal.GetAssemblyClassName(), signal.Name, "", 1, QPartDirection.Vertical, QPartAlignment.Centered, Color.White);
                item.ItemActivated += (sender, args) => CreateSignalGenerationBlock(((QCompositeItem)sender).ItemName);
                SignalTemplatesComposite.Items.Add(item);
            }
        }

        private void CreateSignalGenerationBlock(string templateName)
        {
            var block = new GenerateSignalBlock { TemplateName = templateName };
            Designer.Document.Action = DesignerAction.Connect;
            Designer.Document.LinkType = LinkType.RightAngle;
            var diagramBlock = new DiagramBlock(ApplicationUtils.GetResourceImage("img" + templateName + "Mini", 30, 20), block.Name, block, block.InputNodes.ToArray(), block.OutputNodes.ToArray(), typeof(BlockOutputNode).GetProperty("ShortName"));
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
            var diagramBlock = new DiagramBlock(ApplicationUtils.GetResourceImage("img" + block.GetAssemblyClassName() + "Mini", 30, 20), ApplicationUtils.GetResourceString(block.Name), block, block.InputNodes.ToArray(), block.OutputNodes.ToArray(), typeof(BlockOutputNode).GetProperty("ShortName"));
            Designer.Document.AddElement(diagramBlock);
            Designer.Document.ClearSelection();
            Designer.Document.SelectElement(diagramBlock);
            if (block.CausesRefresh)
            {
                RefreshSelectedDiagramBlock();
            }
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

                var item = QControlUtils.CreateCompositeListItem(type.FullName, "img" + block.GetAssemblyClassName(), ApplicationUtils.GetResourceString(block.Name), "", 1, QPartDirection.Vertical, QPartAlignment.Centered, Color.White);
                
                item.ItemActivated += (sender, args) => CreateBlock(((QCompositeItem)sender).ItemName);
                compositeGroup.Items.Add(item);
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
                MessageBox.Show(string.Format("{0}: \r\n{1}", Resources.FileCouldntBeSaved, exception.Message), Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CloseWindow()
        {
            if (Designer.Document.Elements.Count > 0 && !Saved)
            {
                var questionResult = MessageBox.Show(Resources.SaveChangesQuestion, Resources.Attention, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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
                Filter = string.Format("{0}|*.wsd", Resources.WaveletStudioDocument), //|Wavelet Studio XML Document|*.wsx",
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
                MessageBox.Show(string.Format("{0}:{1}{2}", Resources.FileCouldntBeOpened, Environment.NewLine, exception.Message), Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            diagramForm.ZoomTrackBar.Value = Convert.ToInt32(Designer.Document.Zoom * 10);
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
                Filter = string.Format("{0}|*.wsd", Resources.WaveletStudioDocument), //|Wavelet Studio XML Document|*.wsx",
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
                MessageBox.Show(string.Format("{0}:{1}{2}", Resources.FileCouldntBeSaved, Environment.NewLine, exception.Message), Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportImage()
        {
            var saveDialog = new SaveFileDialog
            {
                SupportMultiDottedExtensions = false,
                Filter = string.Format("PNG|*.png|JPG|*.jpg|BMP|*.bmp|EMF|*.emf|EXIF|*.exif|GIF|*.gif|TIFF|*.tiff|WMF|*.wmf"),
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
                MessageBox.Show(string.Format("{0}:{1}{2}", Resources.FileCouldntBeSaved, Environment.NewLine, exception.Message), Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                document.PrinterSettings = _printerSettings;
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
            try
            {
                System.Diagnostics.Process.Start(Resources.Site);
            }
            catch(Exception)
            {
                MessageBox.Show(string.Format("{0}:\n{1}", Resources.HelpLinkMessage, Resources.Site), Resources.Help, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DiagramFormKeyUp(object sender, KeyEventArgs e)
        {
            var menu = (DiagramFormMainMenu) AppMenuButton.CustomChildWindow;
            if (!e.Control) return;
            if (e.Shift && e.KeyCode == Keys.P)
                menu.PrintPreviewMenuItemItemActivated(sender, null);
            else if (e.KeyCode == Keys.P)
                menu.PrintMenuItemItemActivated(sender, null);
            else if (e.KeyCode == Keys.N)
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
            node2.ConnectingNode = null;
            foreach (var node in node2.Root.OutputNodes)
            {
                node.Object.Clear();
            }
            node1.Root.Execute();
            node2.Root.Execute();
            _outputWindow.BlockPlot.Refresh();
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
                MessageBox.Show(Resources.ModelThrewStackOverflow);
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
            Saved = false;
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
            Saved = false;
        }

        private void DesignerElementResized(object sender, ElementEventArgs e)
        {
            Saved = false;
        }

        private void DesignerElementSelection(object sender, ElementSelectionEventArgs e)
        {
            if (e.Elements.Count == 0)
            {
                _propertiesWindow.PropertyGrid.SelectedObject = null;
                _propertiesWindow.PropertyGrid.Refresh();
                _outputWindow.BlockPlot.Block = null;
                _outputWindow.BlockPlot.Refresh();
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

