namespace WaveletStudio.MainApplication.Forms
{
    partial class BlockPlot
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ShowOutputSignal = new System.Windows.Forms.ComboBox();
            this.NoDataLabel = new System.Windows.Forms.Label();
            this.ShowOutputList = new System.Windows.Forms.ComboBox();
            this.ShowOutputLabel = new System.Windows.Forms.Label();
            this.GraphControl = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // ShowOutputSignal
            // 
            this.ShowOutputSignal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowOutputSignal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ShowOutputSignal.FormattingEnabled = true;
            this.ShowOutputSignal.Location = new System.Drawing.Point(205, 271);
            this.ShowOutputSignal.Name = "ShowOutputSignal";
            this.ShowOutputSignal.Size = new System.Drawing.Size(150, 21);
            this.ShowOutputSignal.TabIndex = 111;
            this.ShowOutputSignal.Visible = false;
            this.ShowOutputSignal.SelectedIndexChanged += new System.EventHandler(this.ShowOutputSignalSelectedIndexChanged);
            // 
            // NoDataLabel
            // 
            this.NoDataLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.NoDataLabel.BackColor = System.Drawing.Color.White;
            this.NoDataLabel.Location = new System.Drawing.Point(62, 49);
            this.NoDataLabel.MinimumSize = new System.Drawing.Size(100, 50);
            this.NoDataLabel.Name = "NoDataLabel";
            this.NoDataLabel.Size = new System.Drawing.Size(245, 156);
            this.NoDataLabel.TabIndex = 110;
            this.NoDataLabel.Text = "Connect the block input to see the graph.";
            this.NoDataLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.NoDataLabel.Visible = false;
            // 
            // ShowOutputList
            // 
            this.ShowOutputList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ShowOutputList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ShowOutputList.FormattingEnabled = true;
            this.ShowOutputList.Location = new System.Drawing.Point(83, 271);
            this.ShowOutputList.Name = "ShowOutputList";
            this.ShowOutputList.Size = new System.Drawing.Size(116, 21);
            this.ShowOutputList.TabIndex = 109;
            this.ShowOutputList.SelectedIndexChanged += new System.EventHandler(this.ShowOutputListSelectedIndexChanged);
            // 
            // ShowOutputLabel
            // 
            this.ShowOutputLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ShowOutputLabel.AutoSize = true;
            this.ShowOutputLabel.Location = new System.Drawing.Point(5, 274);
            this.ShowOutputLabel.Name = "ShowOutputLabel";
            this.ShowOutputLabel.Size = new System.Drawing.Size(72, 13);
            this.ShowOutputLabel.TabIndex = 108;
            this.ShowOutputLabel.Text = "Show Output:";
            // 
            // GraphControl
            // 
            this.GraphControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GraphControl.Location = new System.Drawing.Point(3, 3);
            this.GraphControl.Name = "GraphControl";
            this.GraphControl.ScrollGrace = 0D;
            this.GraphControl.ScrollMaxX = 0D;
            this.GraphControl.ScrollMaxY = 0D;
            this.GraphControl.ScrollMaxY2 = 0D;
            this.GraphControl.ScrollMinX = 0D;
            this.GraphControl.ScrollMinY = 0D;
            this.GraphControl.ScrollMinY2 = 0D;
            this.GraphControl.Size = new System.Drawing.Size(352, 263);
            this.GraphControl.TabIndex = 107;
            this.GraphControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GraphControlMouseDoubleClick);
            // 
            // BlockPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ShowOutputSignal);
            this.Controls.Add(this.NoDataLabel);
            this.Controls.Add(this.ShowOutputList);
            this.Controls.Add(this.ShowOutputLabel);
            this.Controls.Add(this.GraphControl);
            this.Name = "BlockPlot";
            this.Size = new System.Drawing.Size(358, 296);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ShowOutputSignal;
        private System.Windows.Forms.Label NoDataLabel;
        private System.Windows.Forms.ComboBox ShowOutputList;
        private System.Windows.Forms.Label ShowOutputLabel;
        private ZedGraph.ZedGraphControl GraphControl;
    }
}
