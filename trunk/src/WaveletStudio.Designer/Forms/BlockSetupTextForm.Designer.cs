namespace WaveletStudio.Designer.Forms
{
    partial class BlockSetupTextForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlockSetupTextForm));
            this.OutputTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // PropertyGrid
            // 
            resources.ApplyResources(this.PropertyGrid, "PropertyGrid");
            // 
            // OutputTextBox
            // 
            resources.ApplyResources(this.OutputTextBox, "OutputTextBox");
            this.OutputTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ReadOnly = true;
            // 
            // BlockSetupTextForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.OutputTextBox);
            this.Name = "BlockSetupTextForm";
            this.Controls.SetChildIndex(this.PropertyGrid, 0);
            this.Controls.SetChildIndex(this.OutputTextBox, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox OutputTextBox;
    }
}