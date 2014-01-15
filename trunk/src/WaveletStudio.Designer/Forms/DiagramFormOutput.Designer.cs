namespace WaveletStudio.Designer.Forms
{
    partial class DiagramFormOutput
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiagramFormOutput));
            this.ShowOutputSignal = new System.Windows.Forms.ComboBox();
            this.NoDataLabel = new System.Windows.Forms.Label();
            this.ShowOutputList = new System.Windows.Forms.ComboBox();
            this.ShowOutputLabel = new System.Windows.Forms.Label();
            this.GraphControl = new ZedGraph.ZedGraphControl();
            this.qTranslucentWindowComponent1 = new Qios.DevSuite.Components.QTranslucentWindowComponent(this.components);
            this.SuspendLayout();
            // 
            // ShowOutputSignal
            // 
            resources.ApplyResources(this.ShowOutputSignal, "ShowOutputSignal");
            this.ShowOutputSignal.BackColor = System.Drawing.Color.White;
            this.ShowOutputSignal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ShowOutputSignal.FormattingEnabled = true;
            this.ShowOutputSignal.Name = "ShowOutputSignal";
            this.ShowOutputSignal.SelectedIndexChanged += new System.EventHandler(this.ShowOutputSignalSelectedIndexChanged);
            // 
            // NoDataLabel
            // 
            resources.ApplyResources(this.NoDataLabel, "NoDataLabel");
            this.NoDataLabel.BackColor = System.Drawing.Color.White;
            this.NoDataLabel.Name = "NoDataLabel";
            // 
            // ShowOutputList
            // 
            resources.ApplyResources(this.ShowOutputList, "ShowOutputList");
            this.ShowOutputList.BackColor = System.Drawing.Color.White;
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
            // qTranslucentWindowComponent1
            // 
            resources.ApplyResources(this.qTranslucentWindowComponent1, "qTranslucentWindowComponent1");
            // 
            // DiagramFormOutput
            // 
            resources.ApplyResources(this, "$this");
            this.Appearance.BackgroundStyle = Qios.DevSuite.Components.QColorStyle.Solid;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ColorScheme.DockBarBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.ColorScheme.DockBarBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.ColorScheme.DockBarBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.ColorScheme.DockBarBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.ColorScheme.DockBarBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.ColorScheme.DockBarBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.ColorScheme.DockBarBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.ColorScheme.DockBarBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.ColorScheme.DockBarBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.ColorScheme.DockBarBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.ColorScheme.DockBarButtonBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.ColorScheme.DockBarButtonBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.ColorScheme.DockBarButtonBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.ColorScheme.DockBarButtonBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.ColorScheme.DockBarButtonBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.ColorScheme.DockBarButtonBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.ColorScheme.DockBarButtonBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.ColorScheme.DockContainerBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.ColorScheme.DockContainerBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.ColorScheme.DockContainerBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.ColorScheme.DockContainerBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.ColorScheme.DockContainerBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.ColorScheme.DockContainerBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.ColorScheme.DockContainerBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.ColorScheme.DockContainerBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.ColorScheme.DockContainerBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.ColorScheme.DockContainerBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowTabButton1.SetColor("Default", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowTabButton1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowTabButton1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowTabButton1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowTabButton1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowTabButton2.SetColor("Default", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowTabButton2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowTabButton2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowTabButton2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowTabButton2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowTabStrip1.SetColor("Default", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowTabStrip1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowTabStrip2.SetColor("Default", System.Drawing.Color.White, false);
            this.ColorScheme.DockingWindowTabStrip2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.ColorScheme.RibbonCaptionBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.ColorScheme.RibbonCaptionBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.ColorScheme.RibbonCaptionBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.ColorScheme.RibbonCaptionBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.ColorScheme.RibbonCaptionBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.ColorScheme.Scope = Qios.DevSuite.Components.QColorSchemeScope.All;
            this.Controls.Add(this.ShowOutputSignal);
            this.Controls.Add(this.NoDataLabel);
            this.Controls.Add(this.ShowOutputList);
            this.Controls.Add(this.ShowOutputLabel);
            this.Controls.Add(this.GraphControl);
            this.Name = "DiagramFormOutput";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ShowOutputSignal;
        private System.Windows.Forms.Label NoDataLabel;
        private System.Windows.Forms.ComboBox ShowOutputList;
        private System.Windows.Forms.Label ShowOutputLabel;
        private ZedGraph.ZedGraphControl GraphControl;
        private Qios.DevSuite.Components.QTranslucentWindowComponent qTranslucentWindowComponent1;
    }
}
