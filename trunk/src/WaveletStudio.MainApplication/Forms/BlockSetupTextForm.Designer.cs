namespace WaveletStudio.MainApplication.Forms
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
            this.OutputTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.OutputTextBox.Location = new System.Drawing.Point(305, 41);
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ReadOnly = true;
            this.OutputTextBox.Size = new System.Drawing.Size(470, 321);
            this.OutputTextBox.TabIndex = 107;
            this.OutputTextBox.Text = "";
            this.OutputTextBox.WordWrap = false;
            // 
            // TextBlockSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 374);
            this.Controls.Add(this.OutputTextBox);
            this.Name = "TextBlockSetupForm";
            this.Controls.SetChildIndex(this.PropertyGrid, 0);
            this.Controls.SetChildIndex(this.OutputTextBox, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox OutputTextBox;
    }
}