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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlockPlot));
            this.ShowOutputSignal = new System.Windows.Forms.ComboBox();
            this.NoDataLabel = new System.Windows.Forms.Label();
            this.ShowOutputList = new System.Windows.Forms.ComboBox();
            this.ShowOutputLabel = new System.Windows.Forms.Label();
            this.GraphControl = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // ShowOutputSignal
            // 
            resources.ApplyResources(this.ShowOutputSignal, "ShowOutputSignal");
            this.ShowOutputSignal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ShowOutputSignal.FormattingEnabled = true;
            this.ShowOutputSignal.Name = "ShowOutputSignal";
            this.ShowOutputSignal.SelectedIndexChanged += new System.EventHandler(this.ShowOutputSignalSelectedIndexChanged);
            // 
            // NoDataLabel
            // 
            resources.ApplyResources(this.NoDataLabel, "NoDataLabel");
            this.NoDataLabel.BackColor = System.Drawing.Color.White;
            this.NoDataLabel.MinimumSize = new System.Drawing.Size(100, 50);
            this.NoDataLabel.Name = "NoDataLabel";
            // 
            // ShowOutputList
            // 
            resources.ApplyResources(this.ShowOutputList, "ShowOutputList");
            this.ShowOutputList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ShowOutputList.FormattingEnabled = true;
            this.ShowOutputList.Name = "ShowOutputList";
            this.ShowOutputList.SelectedIndexChanged += new System.EventHandler(this.ShowOutputListSelectedIndexChanged);
            // 
            // ShowOutputLabel
            // 
            resources.ApplyResources(this.ShowOutputLabel, "ShowOutputLabel");
            this.ShowOutputLabel.Name = "ShowOutputLabel";
            // 
            // GraphControl
            // 
            resources.ApplyResources(this.GraphControl, "GraphControl");
            this.GraphControl.Name = "GraphControl";
            this.GraphControl.ScrollGrace = 0D;
            this.GraphControl.ScrollMaxX = 0D;
            this.GraphControl.ScrollMaxY = 0D;
            this.GraphControl.ScrollMaxY2 = 0D;
            this.GraphControl.ScrollMinX = 0D;
            this.GraphControl.ScrollMinY = 0D;
            this.GraphControl.ScrollMinY2 = 0D;
            this.GraphControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GraphControlMouseDoubleClick);
            // 
            // BlockPlot
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ShowOutputSignal);
            this.Controls.Add(this.NoDataLabel);
            this.Controls.Add(this.ShowOutputList);
            this.Controls.Add(this.ShowOutputLabel);
            this.Controls.Add(this.GraphControl);
            this.Name = "BlockPlot";
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
