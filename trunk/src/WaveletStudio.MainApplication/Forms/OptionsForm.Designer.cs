using DiagramNet.Events;
using Qios.DevSuite.Components;

namespace WaveletStudio.MainApplication.Forms
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.Caption = new Qios.DevSuite.Components.Ribbon.QRibbonCaption();
            this.qsContentArea = new Qios.DevSuite.Components.QShape();
            this.qsLeftTabStrip = new Qios.DevSuite.Components.QShape();
            this.qsTabButtonInactive = new Qios.DevSuite.Components.QShape();
            this.qsTabButton = new Qios.DevSuite.Components.QShape();
            this.qsNavigationArea = new Qios.DevSuite.Components.QShape();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CancelChangesButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.TabControl = new Qios.DevSuite.Components.QTabControl();
            this.LanguagePage = new Qios.DevSuite.Components.QTabPage();
            this.AutoLoadLastFileField = new System.Windows.Forms.CheckBox();
            this.ThemeList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LanguageList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.diagramFormProperties1 = new WaveletStudio.MainApplication.Forms.DiagramFormProperties();
            ((System.ComponentModel.ISupportInitialize)(this.Caption)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TabControl)).BeginInit();
            this.TabControl.SuspendLayout();
            this.LanguagePage.SuspendLayout();
            this.SuspendLayout();
            // 
            // Caption
            // 
            this.Caption.Configuration.ApplicationButtonAreaConfiguration.Margin = new Qios.DevSuite.Components.QMargin(0, 4, 0, -8);
            this.Caption.Configuration.IconConfiguration.Visible = Qios.DevSuite.Components.QTristateBool.False;
            resources.ApplyResources(this.Caption, "Caption");
            this.Caption.Name = "Caption";
            // 
            // qsContentArea
            // 
            this.qsContentArea.BaseShapeType = Qios.DevSuite.Components.QBaseShapeType.SquareContent;
            this.qsContentArea.ContentBounds = new System.Drawing.Rectangle(-1, 4, 47, 43);
            this.qsContentArea.Items.Add(new Qios.DevSuite.Components.QShapeItem(-2F, 49F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left))), false));
            this.qsContentArea.Items.Add(new Qios.DevSuite.Components.QShapeItem(-2F, 0F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left))), true));
            this.qsContentArea.Items.Add(new Qios.DevSuite.Components.QShapeItem(49F, 0F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))), true));
            this.qsContentArea.Items.Add(new Qios.DevSuite.Components.QShapeItem(49F, 49F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right))), true));
            this.qsContentArea.Size = new System.Drawing.Size(50, 50);
            // 
            // qsLeftTabStrip
            // 
            this.qsLeftTabStrip.BaseShapeType = Qios.DevSuite.Components.QBaseShapeType.SquareTabStrip;
            this.qsLeftTabStrip.ContentBounds = new System.Drawing.Rectangle(9, 0, 300, 100);
            this.qsLeftTabStrip.Items.Add(new Qios.DevSuite.Components.QShapeItem(0F, 99F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left))), true));
            this.qsLeftTabStrip.Items.Add(new Qios.DevSuite.Components.QShapeItem(0F, 11F, 0F, 5F, 3F, 1F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left))), true));
            this.qsLeftTabStrip.Items.Add(new Qios.DevSuite.Components.QShapeItem(9F, 1F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left))), true));
            this.qsLeftTabStrip.Items.Add(new Qios.DevSuite.Components.QShapeItem(309F, 1F, 315F, 1F, 317F, 4F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))), true));
            this.qsLeftTabStrip.Items.Add(new Qios.DevSuite.Components.QShapeItem(318F, 7F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))), true));
            this.qsLeftTabStrip.Items.Add(new Qios.DevSuite.Components.QShapeItem(348F, 88F, 350F, 93F, 353F, 95F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right))), true));
            this.qsLeftTabStrip.Items.Add(new Qios.DevSuite.Components.QShapeItem(354F, 95F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right))), true));
            this.qsLeftTabStrip.Items.Add(new Qios.DevSuite.Components.QShapeItem(449F, 95F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right))), true));
            this.qsLeftTabStrip.Items.Add(new Qios.DevSuite.Components.QShapeItem(449F, 99F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right))), true));
            this.qsLeftTabStrip.Size = new System.Drawing.Size(450, 100);
            // 
            // qsTabButtonInactive
            // 
            this.qsTabButtonInactive.BaseShapeType = Qios.DevSuite.Components.QBaseShapeType.SquareTab;
            this.qsTabButtonInactive.ContentBounds = new System.Drawing.Rectangle(0, 0, 100, 20);
            this.qsTabButtonInactive.Items.Add(new Qios.DevSuite.Components.QShapeItem(0F, 20F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left))), false));
            this.qsTabButtonInactive.Items.Add(new Qios.DevSuite.Components.QShapeItem(0F, 15.25F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left))), true));
            this.qsTabButtonInactive.Items.Add(new Qios.DevSuite.Components.QShapeItem(0F, 4.25F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left))), false));
            this.qsTabButtonInactive.Items.Add(new Qios.DevSuite.Components.QShapeItem(0F, 0F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left))), false));
            this.qsTabButtonInactive.Items.Add(new Qios.DevSuite.Components.QShapeItem(100F, 0F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))), false));
            this.qsTabButtonInactive.Items.Add(new Qios.DevSuite.Components.QShapeItem(100F, 4.5F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))), true));
            this.qsTabButtonInactive.Items.Add(new Qios.DevSuite.Components.QShapeItem(100F, 15.25F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right))), false));
            this.qsTabButtonInactive.Items.Add(new Qios.DevSuite.Components.QShapeItem(100F, 20F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right))), false));
            this.qsTabButtonInactive.Precision = 2;
            // 
            // qsTabButton
            // 
            this.qsTabButton.BaseShapeType = Qios.DevSuite.Components.QBaseShapeType.RoundedTab;
            this.qsTabButton.ContentBounds = new System.Drawing.Rectangle(0, 0, 100, 20);
            this.qsTabButton.Items.Add(new Qios.DevSuite.Components.QShapeItem(95F, 0F, 100F, 0F, 100F, 4F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))), true));
            this.qsTabButton.Items.Add(new Qios.DevSuite.Components.QShapeItem(100F, 8F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))), true));
            this.qsTabButton.Items.Add(new Qios.DevSuite.Components.QShapeItem(100F, 13F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right))), true));
            this.qsTabButton.Items.Add(new Qios.DevSuite.Components.QShapeItem(100F, 20F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right))), false));
            this.qsTabButton.Items.Add(new Qios.DevSuite.Components.QShapeItem(0F, 20F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left))), true));
            this.qsTabButton.Items.Add(new Qios.DevSuite.Components.QShapeItem(0F, 13F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left))), true));
            this.qsTabButton.Items.Add(new Qios.DevSuite.Components.QShapeItem(0F, 8F, 0F, 4F, 0F, 0F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left))), true));
            this.qsTabButton.Items.Add(new Qios.DevSuite.Components.QShapeItem(5F, 0F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left))), true));
            // 
            // qsNavigationArea
            // 
            this.qsNavigationArea.BaseShapeType = Qios.DevSuite.Components.QBaseShapeType.SquareTabStripNavigationArea;
            this.qsNavigationArea.Items.Add(new Qios.DevSuite.Components.QShapeItem(0F, 20F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left))), true));
            this.qsNavigationArea.Items.Add(new Qios.DevSuite.Components.QShapeItem(0F, 1F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left))), false));
            this.qsNavigationArea.Items.Add(new Qios.DevSuite.Components.QShapeItem(100F, 1F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))), false));
            this.qsNavigationArea.Items.Add(new Qios.DevSuite.Components.QShapeItem(100F, 20F, ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right))), false));
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CancelChangesButton);
            this.panel1.Controls.Add(this.SaveButton);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // CancelChangesButton
            // 
            this.CancelChangesButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.CancelChangesButton, "CancelChangesButton");
            this.CancelChangesButton.Name = "CancelChangesButton";
            this.CancelChangesButton.UseVisualStyleBackColor = true;
            this.CancelChangesButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // SaveButton
            // 
            resources.ApplyResources(this.SaveButton, "SaveButton");
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButtonClick);
            // 
            // TabControl
            // 
            this.TabControl.ActiveTabPage = this.LanguagePage;
            this.TabControl.Appearance.GradientAngle = 45;
            this.TabControl.ColorScheme.TabButtonActiveBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabButtonActiveBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabButtonActiveBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabButtonActiveBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabButtonActiveBackground1.SetColor("HighContrast", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabButtonActiveBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabButtonActiveBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabButtonActiveBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabButtonActiveBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabButtonActiveBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabButtonActiveBackground2.SetColor("HighContrast", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabButtonActiveBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabButtonActiveBorder.SetColor("Default", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabButtonActiveBorder.SetColor("LunaBlue", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabButtonActiveBorder.SetColor("LunaOlive", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabButtonActiveBorder.SetColor("LunaSilver", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabButtonActiveBorder.SetColor("VistaBlack", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabButtonBackground1.ColorReference = "@Transparent";
            this.TabControl.ColorScheme.TabButtonBackground2.ColorReference = "@Transparent";
            this.TabControl.ColorScheme.TabButtonBorder.SetColor("Default", System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(234)))), ((int)(((byte)(226))))), false);
            this.TabControl.ColorScheme.TabButtonBorder.SetColor("LunaBlue", System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(234)))), ((int)(((byte)(226))))), false);
            this.TabControl.ColorScheme.TabButtonBorder.SetColor("LunaOlive", System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(234)))), ((int)(((byte)(226))))), false);
            this.TabControl.ColorScheme.TabButtonBorder.SetColor("LunaSilver", System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(234)))), ((int)(((byte)(226))))), false);
            this.TabControl.ColorScheme.TabButtonBorder.SetColor("HighContrast", System.Drawing.SystemColors.Control, false);
            this.TabControl.ColorScheme.TabButtonBorder.SetColor("VistaBlack", System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(234)))), ((int)(((byte)(226))))), false);
            this.TabControl.ColorScheme.TabButtonHotBackground1.ColorReference = "@Transparent";
            this.TabControl.ColorScheme.TabButtonHotBackground2.SetColor("HighContrast", System.Drawing.SystemColors.ActiveCaption, false);
            this.TabControl.ColorScheme.TabButtonHotBorder.SetColor("Default", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabButtonHotBorder.SetColor("LunaBlue", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabButtonHotBorder.SetColor("LunaOlive", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabButtonHotBorder.SetColor("LunaSilver", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabButtonHotBorder.SetColor("VistaBlack", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabControlBackground1.ColorReference = "@Transparent";
            this.TabControl.ColorScheme.TabControlBackground2.ColorReference = "@Transparent";
            this.TabControl.ColorScheme.TabControlContentBackground1.ColorReference = "@Transparent";
            this.TabControl.ColorScheme.TabControlContentBackground2.ColorReference = "@Transparent";
            this.TabControl.ColorScheme.TabControlContentBorder.SetColor("Default", System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(157)))), ((int)(((byte)(169))))), false);
            this.TabControl.ColorScheme.TabControlContentBorder.SetColor("LunaBlue", System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(157)))), ((int)(((byte)(169))))), false);
            this.TabControl.ColorScheme.TabControlContentBorder.SetColor("LunaOlive", System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(157)))), ((int)(((byte)(169))))), false);
            this.TabControl.ColorScheme.TabControlContentBorder.SetColor("LunaSilver", System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(157)))), ((int)(((byte)(169))))), false);
            this.TabControl.ColorScheme.TabControlContentBorder.SetColor("HighContrast", System.Drawing.SystemColors.ControlDark, false);
            this.TabControl.ColorScheme.TabControlContentBorder.SetColor("VistaBlack", System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(157)))), ((int)(((byte)(169))))), false);
            this.TabControl.ColorScheme.TabPageBackground1.SetColor("Default", System.Drawing.Color.Transparent, false);
            this.TabControl.ColorScheme.TabPageBackground1.SetColor("LunaBlue", System.Drawing.Color.Transparent, false);
            this.TabControl.ColorScheme.TabPageBackground1.SetColor("LunaOlive", System.Drawing.Color.Transparent, false);
            this.TabControl.ColorScheme.TabPageBackground1.SetColor("LunaSilver", System.Drawing.Color.Transparent, false);
            this.TabControl.ColorScheme.TabPageBackground1.SetColor("VistaBlack", System.Drawing.Color.Transparent, false);
            this.TabControl.ColorScheme.TabPageBackground2.SetColor("Default", System.Drawing.Color.Transparent, false);
            this.TabControl.ColorScheme.TabPageBackground2.SetColor("LunaBlue", System.Drawing.Color.Transparent, false);
            this.TabControl.ColorScheme.TabPageBackground2.SetColor("LunaOlive", System.Drawing.Color.Transparent, false);
            this.TabControl.ColorScheme.TabPageBackground2.SetColor("LunaSilver", System.Drawing.Color.Transparent, false);
            this.TabControl.ColorScheme.TabPageBackground2.SetColor("VistaBlack", System.Drawing.Color.Transparent, false);
            this.TabControl.ColorScheme.TabPageBorder.SetColor("Default", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabPageBorder.SetColor("LunaBlue", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabPageBorder.SetColor("LunaOlive", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabPageBorder.SetColor("LunaSilver", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabPageBorder.SetColor("VistaBlack", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabStripBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabStripBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabStripBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabStripBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabStripBackground1.SetColor("HighContrast", System.Drawing.SystemColors.Window, false);
            this.TabControl.ColorScheme.TabStripBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabStripBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabStripBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabStripBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabStripBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabStripBackground2.SetColor("HighContrast", System.Drawing.SystemColors.Window, false);
            this.TabControl.ColorScheme.TabStripBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.TabControl.ColorScheme.TabStripBorder.SetColor("Default", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabStripBorder.SetColor("LunaBlue", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabStripBorder.SetColor("LunaOlive", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabStripBorder.SetColor("LunaSilver", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabStripBorder.SetColor("VistaBlack", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabStripNavigationAreaBackground1.ColorReference = "@Transparent";
            this.TabControl.ColorScheme.TabStripNavigationAreaBackground2.ColorReference = "@Transparent";
            this.TabControl.ColorScheme.TabStripNavigationAreaBorder.SetColor("Default", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabStripNavigationAreaBorder.SetColor("LunaBlue", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabStripNavigationAreaBorder.SetColor("LunaOlive", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabStripNavigationAreaBorder.SetColor("LunaSilver", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.ColorScheme.TabStripNavigationAreaBorder.SetColor("VistaBlack", System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(167)))), ((int)(((byte)(180))))), false);
            this.TabControl.Configuration.ContentAppearance.BorderWidth = 0;
            this.TabControl.Configuration.ContentAppearance.Shape = this.qsContentArea;
            this.TabControl.Controls.Add(this.LanguagePage);
            this.TabControl.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.TabControl, "TabControl");
            this.TabControl.FocusTabButtons = false;
            this.TabControl.Name = "TabControl";
            this.TabControl.PersistGuid = new System.Guid("d71a5cf7-2a3a-469f-a114-58bfdbd1b9ad");
            this.TabControl.TabStripLeftConfiguration.Appearance.GradientAngle = 180;
            this.TabControl.TabStripLeftConfiguration.Appearance.Shape = this.qsLeftTabStrip;
            this.TabControl.TabStripLeftConfiguration.ButtonAreaClip = true;
            this.TabControl.TabStripLeftConfiguration.ButtonAreaMargin = new Qios.DevSuite.Components.QMargin(0, 0, 0, 0);
            this.TabControl.TabStripLeftConfiguration.ButtonConfiguration.Appearance.Shape = this.qsTabButtonInactive;
            this.TabControl.TabStripLeftConfiguration.ButtonConfiguration.AppearanceActive.BackgroundStyle = Qios.DevSuite.Components.QColorStyle.Solid;
            this.TabControl.TabStripLeftConfiguration.ButtonConfiguration.AppearanceActive.Shape = this.qsTabButton;
            this.TabControl.TabStripLeftConfiguration.ButtonConfiguration.AppearanceHot.BackgroundStyle = Qios.DevSuite.Components.QColorStyle.Solid;
            this.TabControl.TabStripLeftConfiguration.ButtonConfiguration.AppearanceHot.Shape = this.qsTabButton;
            this.TabControl.TabStripLeftConfiguration.ButtonConfiguration.MinimumSize = ((System.Drawing.Size)(resources.GetObject("resource.MinimumSize")));
            this.TabControl.TabStripLeftConfiguration.ButtonConfiguration.Padding = new Qios.DevSuite.Components.QPadding(10, 1, 1, 3);
            this.TabControl.TabStripLeftConfiguration.ButtonSpacing = 1;
            this.TabControl.TabStripLeftConfiguration.FontStyleHot = new Qios.DevSuite.Components.QFontStyle(false, false, false, false);
            this.TabControl.TabStripLeftConfiguration.NavigationAreaAppearance.Shape = this.qsNavigationArea;
            this.TabControl.TabStripLeftConfiguration.NavigationAreaMargin = new Qios.DevSuite.Components.QMargin(-30, 0, 0, 0);
            this.TabControl.TabStripLeftConfiguration.NavigationAreaPadding = new Qios.DevSuite.Components.QPadding(0, 0, 5, 0);
            this.TabControl.TabStripLeftConfiguration.StripVisibleWithoutButtons = true;
            this.TabControl.WrapTabButtonNavigationAround = false;
            // 
            // LanguagePage
            // 
            this.LanguagePage.Appearance.BorderWidth = 0;
            this.LanguagePage.Appearance.ShowBorders = true;
            this.LanguagePage.ButtonDockStyle = Qios.DevSuite.Components.QTabButtonDockStyle.Left;
            this.LanguagePage.ButtonOrder = 0;
            this.LanguagePage.ColorScheme.TabPageBorder.SetColor("Default", System.Drawing.Color.Transparent, false);
            this.LanguagePage.Controls.Add(this.AutoLoadLastFileField);
            this.LanguagePage.Controls.Add(this.ThemeList);
            this.LanguagePage.Controls.Add(this.label2);
            this.LanguagePage.Controls.Add(this.LanguageList);
            this.LanguagePage.Controls.Add(this.label1);
            resources.ApplyResources(this.LanguagePage, "LanguagePage");
            this.LanguagePage.Name = "LanguagePage";
            this.LanguagePage.PersistGuid = new System.Guid("8ee57f14-b8f8-47a0-bbbb-2c9c4e535893");
            // 
            // AutoLoadLastFileField
            // 
            resources.ApplyResources(this.AutoLoadLastFileField, "AutoLoadLastFileField");
            this.AutoLoadLastFileField.Name = "AutoLoadLastFileField";
            this.AutoLoadLastFileField.UseVisualStyleBackColor = true;
            // 
            // ThemeList
            // 
            this.ThemeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ThemeList.FormattingEnabled = true;
            this.ThemeList.Items.AddRange(new object[] {
            resources.GetString("ThemeList.Items"),
            resources.GetString("ThemeList.Items1"),
            resources.GetString("ThemeList.Items2"),
            resources.GetString("ThemeList.Items3"),
            resources.GetString("ThemeList.Items4")});
            resources.ApplyResources(this.ThemeList, "ThemeList");
            this.ThemeList.Name = "ThemeList";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // LanguageList
            // 
            this.LanguageList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LanguageList.FormattingEnabled = true;
            resources.ApplyResources(this.LanguageList, "LanguageList");
            this.LanguageList.Name = "LanguageList";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // diagramFormProperties1
            // 
            this.diagramFormProperties1.CanClose = false;
            this.diagramFormProperties1.CanDockBottom = false;
            this.diagramFormProperties1.CanDockOnOtherControlBottom = false;
            this.diagramFormProperties1.CanDockOnOtherControlTop = false;
            this.diagramFormProperties1.CanDockTop = false;
            this.diagramFormProperties1.DockPosition = Qios.DevSuite.Components.QDockPosition.None;
            this.diagramFormProperties1.Icon = ((System.Drawing.Icon)(resources.GetObject("diagramFormProperties1.Icon")));
            resources.ApplyResources(this.diagramFormProperties1, "diagramFormProperties1");
            this.diagramFormProperties1.Name = "diagramFormProperties1";
            this.diagramFormProperties1.Owner = null;
            this.diagramFormProperties1.PersistGuid = new System.Guid("1915c329-99d5-4532-a105-ca14e4e34bdb");
            this.diagramFormProperties1.ShowInTaskbarUndocked = true;
            this.diagramFormProperties1.SlidingTime = 200;
            this.diagramFormProperties1.WindowGroupName = "";
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.SaveButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Caption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.OptionsFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.Caption)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TabControl)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.LanguagePage.ResumeLayout(false);
            this.LanguagePage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Qios.DevSuite.Components.Ribbon.QRibbonCaption Caption;
        private DiagramFormProperties diagramFormProperties1;
        private QShape qsContentArea;
        private QShape qsLeftTabStrip;
        private QShape qsTabButtonInactive;
        private QShape qsTabButton;
        private QShape qsNavigationArea;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CancelChangesButton;
        private System.Windows.Forms.Button SaveButton;
        private QTabControl TabControl;
        private QTabPage LanguagePage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox LanguageList;
        private System.Windows.Forms.ComboBox ThemeList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox AutoLoadLastFileField;              
    }
}