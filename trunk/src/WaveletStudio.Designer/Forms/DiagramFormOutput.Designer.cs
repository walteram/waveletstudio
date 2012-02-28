using Qios.DevSuite.Components;

namespace WaveletStudio.Designer.Forms
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
            this.BlockPlot = new BlockPlot();
            this.SuspendLayout();
            // 
            // BlockPlot
            // 
            resources.ApplyResources(this.BlockPlot, "BlockPlot");
            this.BlockPlot.Block = null;
            this.BlockPlot.Name = "BlockPlot";
            // 
            // DiagramFormOutput
            // 
            resources.ApplyResources(this, "$this");
            this.CanClose = false;
            this.CanDockOnFormBorder = true;
            this.Controls.Add(this.BlockPlot);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DiagramFormOutput";
            this.SlidingTime = 200;
            this.WindowGroupName = "";
            this.ResumeLayout(false);

        }
        #endregion 

        public BlockPlot BlockPlot;





        #endregion
    }
}
