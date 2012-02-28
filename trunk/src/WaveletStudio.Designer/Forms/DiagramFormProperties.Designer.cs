using Qios.DevSuite.Components;

namespace WaveletStudio.Designer.Forms
{
    partial class DiagramFormProperties : QDockingWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiagramFormProperties));
            this.PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // PropertyGrid
            // 
            resources.ApplyResources(this.PropertyGrid, "PropertyGrid");
            this.PropertyGrid.Name = "PropertyGrid";
            this.PropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.PropertyGrid.ToolbarVisible = false;
            // 
            // DiagramFormProperties
            // 
            resources.ApplyResources(this, "$this");
            this.CanClose = false;
            this.Controls.Add(this.PropertyGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DiagramFormProperties";
            this.SlidingTime = 200;
            this.WindowGroupName = "";
            this.ResumeLayout(false);

        }
        #endregion 

        public System.Windows.Forms.PropertyGrid PropertyGrid;


        #endregion
    }
}
