using System;
using System.Windows.Forms;
using Qios.DevSuite.Components.Ribbon;
using WaveletStudio.Blocks;

namespace WaveletStudio.MainApplication.Forms
{
    public partial class BlockSetupBaseForm : QRibbonForm
    {
        protected readonly BlockBase TempBlock;
        public BlockBase Block { get; set; }
        public bool InputConnectionsChanged { get; private set; }
        public bool OutputConnectionsChanged { get; private set; }
        protected bool HasParameters { get; private set; }

        public BlockSetupBaseForm()
        {
            InitializeComponent();
        }

        protected delegate void EventHandler();

        protected event EventHandler OnBeforeInitializing;

        protected event EventHandler OnAfterInitializing;

        protected event EventHandler OnFieldValueChanged;

        public BlockSetupBaseForm(string title, ref BlockBase block)
        {
            InitializeComponent();
            FormCaption.Text = title;
            if (OnBeforeInitializing != null)
                OnBeforeInitializing();
            TempBlock = block.CloneWithLinks();
            TempBlock.Cascade = false;
            PropertyGrid.SelectedObject = TempBlock;
            PropertyGrid.Refresh();
            HasParameters = TempBlock.HasParameters();
            Block = block;
            if (OnAfterInitializing != null)
                OnAfterInitializing();
        }
        
        private void UseSignalButtonClick(object sender, EventArgs e)
        {
            OutputConnectionsChanged = Block.OutputNodes.Count != TempBlock.OutputNodes.Count;
            InputConnectionsChanged = Block.InputNodes.Count != TempBlock.InputNodes.Count;
            var outputNodes = OutputConnectionsChanged ? TempBlock.OutputNodes : Block.OutputNodes;
            var inputNodes = InputConnectionsChanged ? TempBlock.InputNodes : Block.InputNodes;
            Block = TempBlock.Clone();
            Block.OutputNodes = outputNodes;
            Block.InputNodes = inputNodes;
            foreach (var node in inputNodes)
            {
                node.Root = Block;
            }
            foreach (var node in outputNodes)
            {
                node.Root = Block;
            }
            Block.Cascade = true;
            Block.Execute();
        }

        private void PropertyGridPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (OnFieldValueChanged != null)
                OnFieldValueChanged();
        }      
    }
}
