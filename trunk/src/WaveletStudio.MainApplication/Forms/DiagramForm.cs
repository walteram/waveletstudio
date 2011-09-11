using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DiagramNet;
using DiagramNet.Elements;
using DiagramNet.Events;
using Qios.DevSuite.Components;
using Qios.DevSuite.Components.Ribbon;
using WaveletStudio.Blocks;
using WaveletStudio.MainApplication.Controls;
using WaveletStudio.SignalGeneration;

namespace WaveletStudio.MainApplication.Forms
{
    public partial class DiagramForm : QRibbonForm
    {
        public BlockList Blocks = new BlockList();

        public BlockBase CurrentSelectedBlock;

        public DiagramForm()
        {
            InitializeComponent();
        }
        
        private void DiagramFormLoad(object sender, EventArgs e)
        {
            ConfigureDesigner();
            LoadRibbon();
            WindowState = FormWindowState.Maximized;           
        }

        private void ConfigureDesigner()
        {
            Designer.Document.GridSize = new Size(10, 10);
        }

        private void LoadRibbon()
        {
            LoadSignalTemplates();
            LoadBlocks(OperationsFunctionsComposite, BlockBase.ProcessingTypeEnum.Operation);
        }

        private void LoadSignalTemplates()
        {            
            foreach (var type in Utils.GetTypes("WaveletStudio.SignalGeneration"))
            {
                var signal = (CommonSignalBase)Activator.CreateInstance(type);
                var item = QControlUtils.CreateCompositeListItem(type.Name, signal.Name.ToLower(), ApplicationUtils.GetResourceString(signal.Name), "", 1, QPartDirection.Vertical, QPartAlignment.Centered, Color.White);
                item.ItemActivated += (sender, args) => CreateSignalGenerationBlock(((QCompositeItem)sender).ItemName);
                SignalTemplatesComposite.Items.Add(item);
            }
        }

        private void CreateSignalGenerationBlock(string templateName)
        {
            var block = new GenerateSignalBlock { TemplateName = templateName };
            Designer.Document.Action = DesignerAction.Connect;
            Designer.Document.LinkType = LinkType.RightAngle;
            var diagramBlock = new DiagramBlock(ApplicationUtils.GetResourceImage(templateName + "img", 30, 20), templateName, block, block.InputNodes.ToArray(), block.OutputNodes.ToArray(), typeof(BlockOutputNode).GetProperty("ShortName"));
            Designer.Document.AddElement(diagramBlock);
        }

        private void CreateBlock(string itemName)
        {
            var type = Utils.GetType(itemName);
            var block = (BlockBase)Activator.CreateInstance(type);

            Designer.Document.Action = DesignerAction.Connect;
            Designer.Document.LinkType = LinkType.RightAngle;
            var diagramBlock = new DiagramBlock(ApplicationUtils.GetResourceImage(block.Name.ToLower() + "Img", 30, 20), ApplicationUtils.GetResourceString(block.Name), block, block.InputNodes.ToArray(), block.OutputNodes.ToArray(), typeof(BlockOutputNode).GetProperty("ShortName"));
            Designer.Document.AddElement(diagramBlock);
        }

        private void LoadBlocks(QCompositeItemBase compositeGroup, BlockBase.ProcessingTypeEnum processingType)
        {
            foreach (var type in Utils.GetTypes("WaveletStudio.Blocks").Where(t => t.BaseType == typeof(BlockBase)))
            {
                var block = (BlockBase)Activator.CreateInstance(type);
                if (block.ProcessingType != processingType)
                    continue;

                var item = QControlUtils.CreateCompositeListItem(type.FullName, block.Name.ToLower(), ApplicationUtils.GetResourceString(block.Name), "", 1, QPartDirection.Vertical, QPartAlignment.Centered, Color.White);
                
                item.ItemActivated += (sender, args) => CreateBlock(((QCompositeItem)sender).ItemName);
                compositeGroup.Items.Add(item);
            }
        }

        private void DesignerElementConnected(object sender, ElementConnectEventArgs e)
        {
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
                if (e.Link != Designer.Document.Elements[i] && Designer.Document.Elements[i] is BaseLinkElement)
                {
                    var link = (BaseLinkElement)Designer.Document.Elements[i];
                    if (link.Connector2 == e.Link.Connector2)
                    {
                        Designer.Document.DeleteLink(link);
                    }
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
            var setupForm = new BlockSetupForm("Teste", ref block);
            setupForm.ShowDialog();
            if (setupForm.DialogResult == DialogResult.OK)
            {
                diagramBlock.Refresh(ApplicationUtils.GetResourceImage(setupForm.Block.Name.ToLower(), 30, 20), ApplicationUtils.GetResourceString(setupForm.Block.Name), setupForm.Block, setupForm.Block.InputNodes.ToArray(), setupForm.Block.OutputNodes.ToArray(), typeof(BlockOutputNode).GetProperty("ShortName"));
                diagramBlock.Invalidate();
                diagramBlock.State = setupForm.Block;
            }
        }
    }
}

