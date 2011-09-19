namespace WaveletStudio.MainApplication.Forms
{
    partial class BlockSetupForm
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
            this.ShowOutputLabel = new System.Windows.Forms.Label();
            this.ShowOutputList = new System.Windows.Forms.ComboBox();
            this.NoDataLabel = new System.Windows.Forms.Label();
            this.ShowOutputSignal = new System.Windows.Forms.ComboBox();
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
            // GraphControl
            // 
            this.GraphControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GraphControl.Location = new System.Drawing.Point(305, 41);
            this.GraphControl.Name = "GraphControl";
            this.GraphControl.ScrollGrace = 0D;
            this.GraphControl.ScrollMaxX = 0D;
            this.GraphControl.ScrollMaxY = 0D;
            this.GraphControl.ScrollMaxY2 = 0D;
            this.GraphControl.ScrollMinX = 0D;
            this.GraphControl.ScrollMinY = 0D;
            this.GraphControl.ScrollMinY2 = 0D;
            this.GraphControl.Size = new System.Drawing.Size(471, 295);
            this.GraphControl.TabIndex = 100;
            this.GraphControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GraphControlMouseDoubleClick);
            // 
            // UseSignalButton
            // 
            this.UseSignalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UseSignalButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.UseSignalButton.Location = new System.Drawing.Point(178, 339);
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
            // ShowOutputLabel
            // 
            this.ShowOutputLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowOutputLabel.AutoSize = true;
            this.ShowOutputLabel.Location = new System.Drawing.Point(307, 345);
            this.ShowOutputLabel.Name = "ShowOutputLabel";
            this.ShowOutputLabel.Size = new System.Drawing.Size(72, 13);
            this.ShowOutputLabel.TabIndex = 103;
            this.ShowOutputLabel.Text = "Show Output:";
            // 
            // ShowOutputList
            // 
            this.ShowOutputList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ShowOutputList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ShowOutputList.FormattingEnabled = true;
            this.ShowOutputList.Location = new System.Drawing.Point(385, 342);
            this.ShowOutputList.Name = "ShowOutputList";
            this.ShowOutputList.Size = new System.Drawing.Size(121, 21);
            this.ShowOutputList.TabIndex = 104;
            this.ShowOutputList.SelectedIndexChanged += new System.EventHandler(this.ShowOutputListSelectedIndexChanged);
            // 
            // NoDataLabel
            // 
            this.NoDataLabel.AutoSize = true;
            this.NoDataLabel.BackColor = System.Drawing.Color.Transparent;
            this.NoDataLabel.Location = new System.Drawing.Point(439, 182);
            this.NoDataLabel.Name = "NoDataLabel";
            this.NoDataLabel.Size = new System.Drawing.Size(203, 13);
            this.NoDataLabel.TabIndex = 105;
            this.NoDataLabel.Text = "Connect the block input to see the graph.";
            this.NoDataLabel.Visible = false;
            // 
            // ShowOutputSignal
            // 
            this.ShowOutputSignal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ShowOutputSignal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ShowOutputSignal.FormattingEnabled = true;
            this.ShowOutputSignal.Location = new System.Drawing.Point(512, 342);
            this.ShowOutputSignal.Name = "ShowOutputSignal";
            this.ShowOutputSignal.Size = new System.Drawing.Size(264, 21);
            this.ShowOutputSignal.TabIndex = 106;
            this.ShowOutputSignal.Visible = false;
            this.ShowOutputSignal.SelectedIndexChanged += new System.EventHandler(this.ShowOutputSignalSelectedIndexChanged);
            // 
            // BlockSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 374);
            this.Controls.Add(this.ShowOutputSignal);
            this.Controls.Add(this.NoDataLabel);
            this.Controls.Add(this.ShowOutputList);
            this.Controls.Add(this.ShowOutputLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.UseSignalButton);
            this.Controls.Add(this.FormCaption);
            this.Controls.Add(this.GraphControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BlockSetupForm";
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
        private System.Windows.Forms.Label ShowOutputLabel;
        private System.Windows.Forms.ComboBox ShowOutputList;
        private System.Windows.Forms.Label NoDataLabel;
        private System.Windows.Forms.ComboBox ShowOutputSignal;
    }
}