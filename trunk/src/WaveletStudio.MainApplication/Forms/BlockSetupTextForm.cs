using WaveletStudio.Blocks;

namespace WaveletStudio.MainApplication.Forms
{
    public partial class BlockSetupTextForm : BlockSetupBaseForm
    {
        public BlockSetupTextForm(string title, ref BlockBase block) : base(title, ref block)
        {
            InitializeComponent();
            OnFieldValueChanged += FieldValueChanged;
            FieldValueChanged();
        }

        protected void FieldValueChanged()
        {
            TempBlock.Execute();
            OutputTextBox.Text = TempBlock.GeneratedData == null ? "" : TempBlock.GeneratedData.ToString(); 
        }        
    }
}
