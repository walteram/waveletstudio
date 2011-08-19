namespace WaveletStudio.MainApplication.Forms
{
    partial class SignalOperationForm
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
            this.FormCaption = new Qios.DevSuite.Components.Ribbon.QRibbonCaption();
            this.GraphControl = new ZedGraph.ZedGraphControl();
            this.UseSignalButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FormCaption)).BeginInit();
            this.SuspendLayout();
            // 
            // FormCaption
            // 
            this.FormCaption.Configuration.ApplicationButtonAreaConfiguration.Margin = new Qios.DevSuite.Components.QMargin(0, 4, 0, -8);
            this.FormCaption.Configuration.IconConfiguration.Visible = Qios.DevSuite.Components.QTristateBool.False;
            this.FormCaption.Location = new System.Drawing.Point(0, 0);
            this.FormCaption.Name = "FormCaption";
            this.FormCaption.Size = new System.Drawing.Size(787, 28);
            this.FormCaption.TabIndex = 25;
            this.FormCaption.Text = "Wavelet Studio";
            // 
            // GraphControl
            // 
            this.GraphControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GraphControl.AutoSize = true;
            this.GraphControl.IsAntiAlias = true;
            this.GraphControl.Location = new System.Drawing.Point(305, 41);
            this.GraphControl.Name = "GraphControl";
            this.GraphControl.ScrollGrace = 0D;
            this.GraphControl.ScrollMaxX = 0D;
            this.GraphControl.ScrollMaxY = 0D;
            this.GraphControl.ScrollMaxY2 = 0D;
            this.GraphControl.ScrollMinX = 0D;
            this.GraphControl.ScrollMinY = 0D;
            this.GraphControl.ScrollMinY2 = 0D;
            this.GraphControl.Size = new System.Drawing.Size(471, 321);
            this.GraphControl.TabIndex = 44;
            this.GraphControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GraphControlMouseDoubleClick);
            // 
            // UseSignalButton
            // 
            this.UseSignalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UseSignalButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.UseSignalButton.Location = new System.Drawing.Point(178, 339);
            this.UseSignalButton.Name = "UseSignalButton";
            this.UseSignalButton.Size = new System.Drawing.Size(75, 23);
            this.UseSignalButton.TabIndex = 45;
            this.UseSignalButton.Text = "&OK";
            this.UseSignalButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(12, 339);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 46;
            this.CancelButton.Text = "&Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // SignalOperationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 374);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.UseSignalButton);
            this.Controls.Add(this.FormCaption);
            this.Controls.Add(this.GraphControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SignalOperationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wavelet Studio";
            ((System.ComponentModel.ISupportInitialize)(this.FormCaption)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Qios.DevSuite.Components.Ribbon.QRibbonCaption FormCaption;
        private ZedGraph.ZedGraphControl GraphControl;
        private System.Windows.Forms.Button UseSignalButton;
        private new System.Windows.Forms.Button CancelButton;
    }
}