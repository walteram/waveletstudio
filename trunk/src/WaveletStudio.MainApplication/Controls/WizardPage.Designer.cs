namespace WaveletStudio.MainApplication.Controls
{
    partial class WizardPage
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
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.FlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.TextLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.FlowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.FlowLayoutPanel);
            this.SplitContainer.Panel1MinSize = 10;
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.button1);
            this.SplitContainer.Size = new System.Drawing.Size(732, 447);
            this.SplitContainer.SplitterDistance = 205;
            this.SplitContainer.TabIndex = 0;
            this.SplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer_SplitterMoved);
            // 
            // FlowLayoutPanel
            // 
            this.FlowLayoutPanel.BackColor = System.Drawing.Color.White;
            this.FlowLayoutPanel.Controls.Add(this.TitleLabel);
            this.FlowLayoutPanel.Controls.Add(this.TextLabel);
            this.FlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.FlowLayoutPanel.Margin = new System.Windows.Forms.Padding(10);
            this.FlowLayoutPanel.Name = "FlowLayoutPanel";
            this.FlowLayoutPanel.Size = new System.Drawing.Size(205, 447);
            this.FlowLayoutPanel.TabIndex = 0;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.FlowLayoutPanel.SetFlowBreak(this.TitleLabel, true);
            this.TitleLabel.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(3, 0);
            this.TitleLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 20);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(143, 18);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "The title goes here";
            // 
            // TextLabel
            // 
            this.TextLabel.AutoSize = true;
            this.FlowLayoutPanel.SetFlowBreak(this.TextLabel, true);
            this.TextLabel.Location = new System.Drawing.Point(3, 38);
            this.TextLabel.Name = "TextLabel";
            this.TextLabel.Size = new System.Drawing.Size(102, 26);
            this.TextLabel.TabIndex = 1;
            this.TextLabel.Text = "The text goes here.\r\nHere and here too...";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(67, 144);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // WizardPage
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.SplitContainer);
            this.Name = "WizardPage";
            this.Size = new System.Drawing.Size(732, 447);
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.FlowLayoutPanel.ResumeLayout(false);
            this.FlowLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label TitleLabel;
        public System.Windows.Forms.Label TextLabel;
        public System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel;
        public System.Windows.Forms.SplitContainer SplitContainer;
        private System.Windows.Forms.Button button1;
    }
}
