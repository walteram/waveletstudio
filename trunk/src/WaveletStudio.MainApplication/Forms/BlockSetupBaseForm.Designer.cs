namespace WaveletStudio.MainApplication.Forms
{
    partial class BlockSetupBaseForm
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
            this.UseSignalButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.PropertyGrid = new System.Windows.Forms.PropertyGrid();
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
            this.FormCaption.TabIndex = 0;
            this.FormCaption.Text = "Wavelet Studio";
            // 
            // UseSignalButton
            // 
            this.UseSignalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UseSignalButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.UseSignalButton.Location = new System.Drawing.Point(219, 339);
            this.UseSignalButton.Name = "UseSignalButton";
            this.UseSignalButton.Size = new System.Drawing.Size(75, 23);
            this.UseSignalButton.TabIndex = 102;
            this.UseSignalButton.Text = "&OK";
            this.UseSignalButton.UseVisualStyleBackColor = true;
            this.UseSignalButton.Click += new System.EventHandler(this.UseSignalButtonClick);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(12, 339);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 101;
            this.CancelButton.Text = "&Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // PropertyGrid
            // 
            this.PropertyGrid.Location = new System.Drawing.Point(12, 40);
            this.PropertyGrid.Name = "PropertyGrid";
            this.PropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.PropertyGrid.Size = new System.Drawing.Size(282, 285);
            this.PropertyGrid.TabIndex = 103;
            this.PropertyGrid.ToolbarVisible = false;
            this.PropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGridPropertyValueChanged);
            // 
            // BlockSetupBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 374);
            this.Controls.Add(this.PropertyGrid);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.UseSignalButton);
            this.Controls.Add(this.FormCaption);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 350);
            this.Name = "BlockSetupBaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wavelet Studio";
            ((System.ComponentModel.ISupportInitialize)(this.FormCaption)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Qios.DevSuite.Components.Ribbon.QRibbonCaption FormCaption;
        private System.Windows.Forms.Button UseSignalButton;
        private new System.Windows.Forms.Button CancelButton;
        public System.Windows.Forms.PropertyGrid PropertyGrid;
    }
}