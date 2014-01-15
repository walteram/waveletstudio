namespace WaveletStudio.Designer.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlockSetupBaseForm));
            this.FormCaption = new Qios.DevSuite.Components.Ribbon.QRibbonCaption();
            this.UseSignalButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.PropertyGrid = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.FormCaption)).BeginInit();
            this.SuspendLayout();
            // 
            // FormCaption
            // 
            resources.ApplyResources(this.FormCaption, "FormCaption");
            this.FormCaption.ColorScheme.RibbonCaptionApplicationText.SetColor("VistaBlack", System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48))))), false);
            this.FormCaption.ColorScheme.RibbonCaptionBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.FormCaption.ColorScheme.RibbonCaptionBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.FormCaption.ColorScheme.RibbonCaptionInactiveApplicationText.SetColor("VistaBlack", System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48))))), false);
            this.FormCaption.ColorScheme.RibbonCaptionInactiveBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.FormCaption.ColorScheme.RibbonCaptionInactiveBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.FormCaption.Configuration.ApplicationButtonAreaConfiguration.Margin = new Qios.DevSuite.Components.QMargin(0, 4, 0, -8);
            this.FormCaption.Configuration.IconConfiguration.Visible = Qios.DevSuite.Components.QTristateBool.False;
            this.FormCaption.Name = "FormCaption";
            // 
            // UseSignalButton
            // 
            resources.ApplyResources(this.UseSignalButton, "UseSignalButton");
            this.UseSignalButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.UseSignalButton.Name = "UseSignalButton";
            this.UseSignalButton.UseVisualStyleBackColor = true;
            this.UseSignalButton.Click += new System.EventHandler(this.UseSignalButtonClick);
            // 
            // CancelButton
            // 
            resources.ApplyResources(this.CancelButton, "CancelButton");
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // PropertyGrid
            // 
            resources.ApplyResources(this.PropertyGrid, "PropertyGrid");
            this.PropertyGrid.Name = "PropertyGrid";
            this.PropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.PropertyGrid.ToolbarVisible = false;
            this.PropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGridPropertyValueChanged);
            // 
            // BlockSetupBaseForm
            // 
            resources.ApplyResources(this, "$this");
            this.Appearance.Shape = new Qios.DevSuite.Components.QShape(Qios.DevSuite.Components.QBaseShapeType.RectangleShapedWindow);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ColorScheme.RibbonFormBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.ColorScheme.RibbonFormBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.ColorScheme.RibbonFormInactiveBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.ColorScheme.RibbonFormInactiveBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.Controls.Add(this.PropertyGrid);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.UseSignalButton);
            this.Controls.Add(this.FormCaption);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BlockSetupBaseForm";
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