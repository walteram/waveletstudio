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
using System.Windows.Forms;
using Qios.DevSuite.Components.Ribbon;
using WaveletStudio.Blocks;

namespace WaveletStudio.Designer.Forms
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
