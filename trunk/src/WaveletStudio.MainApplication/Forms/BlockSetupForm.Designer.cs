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
            this.GraphControl = new ZedGraph.ZedGraphControl();
            this.ShowOutputLabel = new System.Windows.Forms.Label();
            this.ShowOutputList = new System.Windows.Forms.ComboBox();
            this.NoDataLabel = new System.Windows.Forms.Label();
            this.ShowOutputSignal = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // GraphControl
            // 
            this.GraphControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GraphControl.Location = new System.Drawing.Point(303, 40);
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
            // ShowOutputLabel
            // 
            this.ShowOutputLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.NoDataLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.NoDataLabel.BackColor = System.Drawing.Color.White;
            this.NoDataLabel.Location = new System.Drawing.Point(437, 163);
            this.NoDataLabel.MinimumSize = new System.Drawing.Size(100, 50);
            this.NoDataLabel.Name = "NoDataLabel";
            this.NoDataLabel.Size = new System.Drawing.Size(220, 50);
            this.NoDataLabel.TabIndex = 105;
            this.NoDataLabel.Text = "Connect the block input to see the graph.";
            this.NoDataLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.NoDataLabel.Visible = false;
            // 
            // ShowOutputSignal
            // 
            this.ShowOutputSignal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
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
            this.Controls.Add(this.GraphControl);
            this.Name = "BlockSetupForm";
            this.Controls.SetChildIndex(this.GraphControl, 0);
            this.Controls.SetChildIndex(this.ShowOutputLabel, 0);
            this.Controls.SetChildIndex(this.ShowOutputList, 0);
            this.Controls.SetChildIndex(this.NoDataLabel, 0);
            this.Controls.SetChildIndex(this.ShowOutputSignal, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl GraphControl;
        private System.Windows.Forms.Label ShowOutputLabel;
        private System.Windows.Forms.ComboBox ShowOutputList;
        private System.Windows.Forms.Label NoDataLabel;
        private System.Windows.Forms.ComboBox ShowOutputSignal;
    }
}