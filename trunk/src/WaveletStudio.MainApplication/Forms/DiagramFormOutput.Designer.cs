using Qios.DevSuite.Components;
using Qios.DevSuite.Components.Ribbon;

namespace WaveletStudio.MainApplication.Forms
{
    partial class DiagramFormOutput : QDockingWindow
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

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiagramFormOutput));
            this.BlockPlot = new WaveletStudio.MainApplication.Forms.BlockPlot();
            this.SuspendLayout();
            // 
            // BlockPlot
            // 
            this.BlockPlot.Block = null;
            this.BlockPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BlockPlot.Location = new System.Drawing.Point(0, 0);
            this.BlockPlot.Name = "BlockPlot";
            this.BlockPlot.Size = new System.Drawing.Size(366, 304);
            this.BlockPlot.TabIndex = 0;
            // 
            // DiagramFormOutput
            // 
            this.CanClose = false;
            this.CanDockOnFormBorder = true;
            this.Controls.Add(this.BlockPlot);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DiagramFormOutput";
            this.Size = new System.Drawing.Size(366, 320);
            this.SlidingTime = 200;
            this.Text = "Output";
            this.WindowGroupName = "";
            this.ResumeLayout(false);

        }
        #endregion 

        public BlockPlot BlockPlot;





        #endregion
    }
}
