using WaveletStudio.Designer.Utils;

namespace WaveletStudio.Designer.Forms
{
    partial class FileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileForm));
            this.MenuPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.CloseButton = new System.Windows.Forms.Button();
            this.InformationsButton = new System.Windows.Forms.Button();
            this.NewButton = new System.Windows.Forms.Button();
            this.OpenButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SaveAsButton = new System.Windows.Forms.Button();
            this.PrintButton = new System.Windows.Forms.Button();
            this.ExportButton = new System.Windows.Forms.Button();
            this.GenerateCodeButton = new System.Windows.Forms.Button();
            this.OptionsButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.TabControl = new WaveletStudio.Designer.Utils.NoHeaderTabControl();
            this.InformationsTabPage = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.NotesField = new System.Windows.Forms.TextBox();
            this.ModifiedByLabel = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.AuthorLabel = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.CanvasSizeLabel = new System.Windows.Forms.Label();
            this.FileSizeLabel = new System.Windows.Forms.Label();
            this.ConnectionsLabel = new System.Windows.Forms.Label();
            this.BlocksLabel = new System.Windows.Forms.Label();
            this.LastModificationLabel = new System.Windows.Forms.Label();
            this.CreatedAtLabel = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.OpenTabPage = new System.Windows.Forms.TabPage();
            this.separator1 = new WaveletStudio.Designer.Utils.Separator();
            this.label8 = new System.Windows.Forms.Label();
            this.OpenFromComputerButton = new WaveletStudio.Designer.Utils.FlatButton();
            this.label7 = new System.Windows.Forms.Label();
            this.RecentFilesPannel = new System.Windows.Forms.FlowLayoutPanel();
            this.PrintTabPage = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.PrintPreviewControl = new System.Windows.Forms.PrintPreviewControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ZoomLabel = new System.Windows.Forms.Label();
            this.ZoomPlusButton = new Qios.DevSuite.Components.QButton();
            this.ZoomMinusButton = new Qios.DevSuite.Components.QButton();
            this.ZoomTrackBar = new System.Windows.Forms.TrackBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.separator2 = new WaveletStudio.Designer.Utils.Separator();
            this.panel7 = new System.Windows.Forms.Panel();
            this.OrientationPortraitButton = new WaveletStudio.Designer.Utils.FlatButton();
            this.OrientationLandscapeButton = new WaveletStudio.Designer.Utils.FlatButton();
            this.label11 = new System.Windows.Forms.Label();
            this.PrintNowButton = new WaveletStudio.Designer.Utils.FlatButton();
            this.StretchModelToPage = new System.Windows.Forms.CheckBox();
            this.ShowGridCheckBox = new System.Windows.Forms.CheckBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.MarginBottomField = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.MarginRightField = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.MarginLeftField = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.MarginTopField = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MenuPanel.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.InformationsTabPage.SuspendLayout();
            this.OpenTabPage.SuspendLayout();
            this.PrintTabPage.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomTrackBar)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MarginBottomField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarginRightField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarginLeftField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarginTopField)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuPanel
            // 
            resources.ApplyResources(this.MenuPanel, "MenuPanel");
            this.MenuPanel.BackColor = System.Drawing.Color.Purple;
            this.MenuPanel.Controls.Add(this.CloseButton);
            this.MenuPanel.Controls.Add(this.InformationsButton);
            this.MenuPanel.Controls.Add(this.NewButton);
            this.MenuPanel.Controls.Add(this.OpenButton);
            this.MenuPanel.Controls.Add(this.SaveButton);
            this.MenuPanel.Controls.Add(this.SaveAsButton);
            this.MenuPanel.Controls.Add(this.PrintButton);
            this.MenuPanel.Controls.Add(this.ExportButton);
            this.MenuPanel.Controls.Add(this.GenerateCodeButton);
            this.MenuPanel.Controls.Add(this.OptionsButton);
            this.MenuPanel.Controls.Add(this.ExitButton);
            this.MenuPanel.Name = "MenuPanel";
            // 
            // CloseButton
            // 
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Purple;
            this.CloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            resources.ApplyResources(this.CloseButton, "CloseButton");
            this.CloseButton.Image = global::WaveletStudio.Designer.Resources.Images.ArrowLeft_White;
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.UseVisualStyleBackColor = false;
            // 
            // InformationsButton
            // 
            resources.ApplyResources(this.InformationsButton, "InformationsButton");
            this.InformationsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.InformationsButton.FlatAppearance.BorderSize = 0;
            this.InformationsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.InformationsButton.ForeColor = System.Drawing.Color.White;
            this.InformationsButton.Name = "InformationsButton";
            this.InformationsButton.UseVisualStyleBackColor = false;
            this.InformationsButton.Click += new System.EventHandler(this.InformationsButton_Click);
            // 
            // NewButton
            // 
            resources.ApplyResources(this.NewButton, "NewButton");
            this.NewButton.FlatAppearance.BorderSize = 0;
            this.NewButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.NewButton.ForeColor = System.Drawing.Color.White;
            this.NewButton.Name = "NewButton";
            this.NewButton.UseVisualStyleBackColor = true;
            this.NewButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // OpenButton
            // 
            resources.ApplyResources(this.OpenButton, "OpenButton");
            this.OpenButton.FlatAppearance.BorderSize = 0;
            this.OpenButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.OpenButton.ForeColor = System.Drawing.Color.White;
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // SaveButton
            // 
            resources.ApplyResources(this.SaveButton, "SaveButton");
            this.SaveButton.FlatAppearance.BorderSize = 0;
            this.SaveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.SaveButton.ForeColor = System.Drawing.Color.White;
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // SaveAsButton
            // 
            resources.ApplyResources(this.SaveAsButton, "SaveAsButton");
            this.SaveAsButton.FlatAppearance.BorderSize = 0;
            this.SaveAsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.SaveAsButton.ForeColor = System.Drawing.Color.White;
            this.SaveAsButton.Name = "SaveAsButton";
            this.SaveAsButton.UseVisualStyleBackColor = true;
            this.SaveAsButton.Click += new System.EventHandler(this.SaveAsButton_Click);
            // 
            // PrintButton
            // 
            resources.ApplyResources(this.PrintButton, "PrintButton");
            this.PrintButton.FlatAppearance.BorderSize = 0;
            this.PrintButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.PrintButton.ForeColor = System.Drawing.Color.White;
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.UseVisualStyleBackColor = true;
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // ExportButton
            // 
            resources.ApplyResources(this.ExportButton, "ExportButton");
            this.ExportButton.FlatAppearance.BorderSize = 0;
            this.ExportButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ExportButton.ForeColor = System.Drawing.Color.White;
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // GenerateCodeButton
            // 
            resources.ApplyResources(this.GenerateCodeButton, "GenerateCodeButton");
            this.GenerateCodeButton.FlatAppearance.BorderSize = 0;
            this.GenerateCodeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.GenerateCodeButton.ForeColor = System.Drawing.Color.White;
            this.GenerateCodeButton.Name = "GenerateCodeButton";
            this.GenerateCodeButton.UseVisualStyleBackColor = true;
            this.GenerateCodeButton.Click += new System.EventHandler(this.GenerateCodeButton_Click);
            // 
            // OptionsButton
            // 
            resources.ApplyResources(this.OptionsButton, "OptionsButton");
            this.OptionsButton.FlatAppearance.BorderSize = 0;
            this.OptionsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.OptionsButton.ForeColor = System.Drawing.Color.White;
            this.OptionsButton.Name = "OptionsButton";
            this.OptionsButton.UseVisualStyleBackColor = true;
            this.OptionsButton.Click += new System.EventHandler(this.OptionsButton_Click);
            // 
            // ExitButton
            // 
            resources.ApplyResources(this.ExitButton, "ExitButton");
            this.ExitButton.FlatAppearance.BorderSize = 0;
            this.ExitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ExitButton.ForeColor = System.Drawing.Color.White;
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // TabControl
            // 
            resources.ApplyResources(this.TabControl, "TabControl");
            this.TabControl.Controls.Add(this.InformationsTabPage);
            this.TabControl.Controls.Add(this.OpenTabPage);
            this.TabControl.Controls.Add(this.PrintTabPage);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);            
            // 
            // InformationsTabPage
            // 
            this.InformationsTabPage.Controls.Add(this.label6);
            this.InformationsTabPage.Controls.Add(this.NotesField);
            this.InformationsTabPage.Controls.Add(this.ModifiedByLabel);
            this.InformationsTabPage.Controls.Add(this.label28);
            this.InformationsTabPage.Controls.Add(this.AuthorLabel);
            this.InformationsTabPage.Controls.Add(this.label26);
            this.InformationsTabPage.Controls.Add(this.CanvasSizeLabel);
            this.InformationsTabPage.Controls.Add(this.FileSizeLabel);
            this.InformationsTabPage.Controls.Add(this.ConnectionsLabel);
            this.InformationsTabPage.Controls.Add(this.BlocksLabel);
            this.InformationsTabPage.Controls.Add(this.LastModificationLabel);
            this.InformationsTabPage.Controls.Add(this.CreatedAtLabel);
            this.InformationsTabPage.Controls.Add(this.label17);
            this.InformationsTabPage.Controls.Add(this.label16);
            this.InformationsTabPage.Controls.Add(this.label15);
            this.InformationsTabPage.Controls.Add(this.label14);
            this.InformationsTabPage.Controls.Add(this.label13);
            this.InformationsTabPage.Controls.Add(this.label12);
            this.InformationsTabPage.Controls.Add(this.label10);
            resources.ApplyResources(this.InformationsTabPage, "InformationsTabPage");
            this.InformationsTabPage.Name = "InformationsTabPage";
            this.InformationsTabPage.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label6.Name = "label6";
            // 
            // NotesField
            // 
            this.NotesField.AcceptsReturn = true;
            resources.ApplyResources(this.NotesField, "NotesField");
            this.NotesField.Name = "NotesField";
            this.NotesField.TextChanged += new System.EventHandler(this.NotesLabel_TextChanged);
            // 
            // ModifiedByLabel
            // 
            resources.ApplyResources(this.ModifiedByLabel, "ModifiedByLabel");
            this.ModifiedByLabel.Name = "ModifiedByLabel";
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // AuthorLabel
            // 
            resources.ApplyResources(this.AuthorLabel, "AuthorLabel");
            this.AuthorLabel.Name = "AuthorLabel";
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            // 
            // CanvasSizeLabel
            // 
            resources.ApplyResources(this.CanvasSizeLabel, "CanvasSizeLabel");
            this.CanvasSizeLabel.Name = "CanvasSizeLabel";
            // 
            // FileSizeLabel
            // 
            resources.ApplyResources(this.FileSizeLabel, "FileSizeLabel");
            this.FileSizeLabel.Name = "FileSizeLabel";
            // 
            // ConnectionsLabel
            // 
            resources.ApplyResources(this.ConnectionsLabel, "ConnectionsLabel");
            this.ConnectionsLabel.Name = "ConnectionsLabel";
            // 
            // BlocksLabel
            // 
            resources.ApplyResources(this.BlocksLabel, "BlocksLabel");
            this.BlocksLabel.Name = "BlocksLabel";
            // 
            // LastModificationLabel
            // 
            resources.ApplyResources(this.LastModificationLabel, "LastModificationLabel");
            this.LastModificationLabel.Name = "LastModificationLabel";
            // 
            // CreatedAtLabel
            // 
            resources.ApplyResources(this.CreatedAtLabel, "CreatedAtLabel");
            this.CreatedAtLabel.Name = "CreatedAtLabel";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // OpenTabPage
            // 
            this.OpenTabPage.Controls.Add(this.separator1);
            this.OpenTabPage.Controls.Add(this.label8);
            this.OpenTabPage.Controls.Add(this.OpenFromComputerButton);
            this.OpenTabPage.Controls.Add(this.label7);
            this.OpenTabPage.Controls.Add(this.RecentFilesPannel);
            resources.ApplyResources(this.OpenTabPage, "OpenTabPage");
            this.OpenTabPage.Name = "OpenTabPage";
            this.OpenTabPage.UseVisualStyleBackColor = true;
            // 
            // separator1
            // 
            resources.ApplyResources(this.separator1, "separator1");
            this.separator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.separator1.Name = "separator1";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // OpenFromComputerButton
            // 
            this.OpenFromComputerButton.BackColor = System.Drawing.Color.White;
            this.OpenFromComputerButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.OpenFromComputerButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.OpenFromComputerButton, "OpenFromComputerButton");
            this.OpenFromComputerButton.FocusBackColor = System.Drawing.Color.Thistle;
            this.OpenFromComputerButton.FocusBorderColor = System.Drawing.Color.Empty;
            this.OpenFromComputerButton.FocusBorderSize = 1;
            this.OpenFromComputerButton.FocusForeColor = System.Drawing.SystemColors.ControlText;
            this.OpenFromComputerButton.HasPressedState = false;
            this.OpenFromComputerButton.Image = global::WaveletStudio.Designer.Resources.Images.computer;
            this.OpenFromComputerButton.Name = "OpenFromComputerButton";
            this.OpenFromComputerButton.NormalBackColor = System.Drawing.Color.White;
            this.OpenFromComputerButton.NormalBorderColor = System.Drawing.Color.Empty;
            this.OpenFromComputerButton.NormalBorderSize = 1;
            this.OpenFromComputerButton.NormalForeColor = System.Drawing.SystemColors.ControlText;
            this.OpenFromComputerButton.Pressed = false;
            this.OpenFromComputerButton.PressedBackColor = System.Drawing.Color.Thistle;
            this.OpenFromComputerButton.PressedBorderColor = System.Drawing.Color.Empty;
            this.OpenFromComputerButton.PressedBorderSize = 1;
            this.OpenFromComputerButton.PressedForeColor = System.Drawing.SystemColors.ControlText;
            this.OpenFromComputerButton.UseVisualStyleBackColor = false;
            this.OpenFromComputerButton.Click += new System.EventHandler(this.OpenFromComputerButton_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label7.Name = "label7";
            // 
            // RecentFilesPannel
            // 
            resources.ApplyResources(this.RecentFilesPannel, "RecentFilesPannel");
            this.RecentFilesPannel.Name = "RecentFilesPannel";
            // 
            // PrintTabPage
            // 
            this.PrintTabPage.Controls.Add(this.panel3);
            this.PrintTabPage.Controls.Add(this.panel2);
            resources.ApplyResources(this.PrintTabPage, "PrintTabPage");
            this.PrintTabPage.Name = "PrintTabPage";
            this.PrintTabPage.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.PrintPreviewControl);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // PrintPreviewControl
            // 
            this.PrintPreviewControl.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.PrintPreviewControl, "PrintPreviewControl");
            this.PrintPreviewControl.Name = "PrintPreviewControl";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ZoomLabel);
            this.panel4.Controls.Add(this.ZoomPlusButton);
            this.panel4.Controls.Add(this.ZoomMinusButton);
            this.panel4.Controls.Add(this.ZoomTrackBar);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // ZoomLabel
            // 
            resources.ApplyResources(this.ZoomLabel, "ZoomLabel");
            this.ZoomLabel.Name = "ZoomLabel";
            // 
            // ZoomPlusButton
            // 
            resources.ApplyResources(this.ZoomPlusButton, "ZoomPlusButton");
            this.ZoomPlusButton.Appearance.BackgroundStyle = Qios.DevSuite.Components.QColorStyle.Solid;
            this.ZoomPlusButton.ColorScheme.ButtonBackground1.SetColor("VistaBlack", System.Drawing.Color.Gainsboro, false);
            this.ZoomPlusButton.ColorScheme.ButtonBackground2.SetColor("VistaBlack", System.Drawing.Color.Gainsboro, false);
            this.ZoomPlusButton.Configuration.DrawTextOptions = Qios.DevSuite.Components.QDrawTextOptions.None;
            this.ZoomPlusButton.Configuration.FontDefinition = new Qios.DevSuite.Components.QFontDefinition("Arial", true, false, false, false, 12F);
            this.ZoomPlusButton.Configuration.FontDefinitionFocused = new Qios.DevSuite.Components.QFontDefinition("Arial", true, false, false, false, 12F);
            this.ZoomPlusButton.Configuration.FontDefinitionHot = new Qios.DevSuite.Components.QFontDefinition("Arial", true, false, false, false, 12F);
            this.ZoomPlusButton.Configuration.FontDefinitionPressed = new Qios.DevSuite.Components.QFontDefinition("Arial", true, false, false, false, 12F);
            this.ZoomPlusButton.Configuration.TextConfiguration.Margin = new Qios.DevSuite.Components.QMargin(0, 2, 2, 4);
            this.ZoomPlusButton.Configuration.WrapText = false;
            this.ZoomPlusButton.Image = null;
            this.ZoomPlusButton.Name = "ZoomPlusButton";
            this.ZoomPlusButton.Click += new System.EventHandler(this.ZoomPlusButton_Click);
            // 
            // ZoomMinusButton
            // 
            resources.ApplyResources(this.ZoomMinusButton, "ZoomMinusButton");
            this.ZoomMinusButton.Appearance.BackgroundStyle = Qios.DevSuite.Components.QColorStyle.Solid;
            this.ZoomMinusButton.ColorScheme.ButtonBackground1.SetColor("VistaBlack", System.Drawing.Color.Gainsboro, false);
            this.ZoomMinusButton.ColorScheme.ButtonBackground2.SetColor("VistaBlack", System.Drawing.Color.Gainsboro, false);
            this.ZoomMinusButton.Configuration.DrawTextOptions = Qios.DevSuite.Components.QDrawTextOptions.None;
            this.ZoomMinusButton.Configuration.FocusRectangleMargin = new Qios.DevSuite.Components.QMargin(0, 0, 0, 0);
            this.ZoomMinusButton.Configuration.FontDefinition = new Qios.DevSuite.Components.QFontDefinition("Arial", true, false, false, false, 12F);
            this.ZoomMinusButton.Configuration.FontDefinitionFocused = new Qios.DevSuite.Components.QFontDefinition("Arial", true, false, false, false, 12F);
            this.ZoomMinusButton.Configuration.FontDefinitionHot = new Qios.DevSuite.Components.QFontDefinition("Arial", true, false, false, false, 12F);
            this.ZoomMinusButton.Configuration.FontDefinitionPressed = new Qios.DevSuite.Components.QFontDefinition("Arial", true, false, false, false, 12F);
            this.ZoomMinusButton.Configuration.TextConfiguration.Margin = new Qios.DevSuite.Components.QMargin(0, 2, 2, 4);
            this.ZoomMinusButton.Configuration.WrapText = false;
            this.ZoomMinusButton.Image = null;
            this.ZoomMinusButton.Name = "ZoomMinusButton";
            this.ZoomMinusButton.Click += new System.EventHandler(this.ZoomMinusButton_Click);
            // 
            // ZoomTrackBar
            // 
            resources.ApplyResources(this.ZoomTrackBar, "ZoomTrackBar");
            this.ZoomTrackBar.BackColor = System.Drawing.Color.White;
            this.ZoomTrackBar.LargeChange = 2;
            this.ZoomTrackBar.Maximum = 30;
            this.ZoomTrackBar.Minimum = 1;
            this.ZoomTrackBar.Name = "ZoomTrackBar";
            this.ZoomTrackBar.Value = 10;
            this.ZoomTrackBar.ValueChanged += new System.EventHandler(this.ZoomTrackBar_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.separator2);
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.PrintNowButton);
            this.panel2.Controls.Add(this.StretchModelToPage);
            this.panel2.Controls.Add(this.ShowGridCheckBox);
            this.panel2.Controls.Add(this.panel6);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label18.Name = "label18";
            // 
            // separator2
            // 
            resources.ApplyResources(this.separator2, "separator2");
            this.separator2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.separator2.Name = "separator2";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.OrientationPortraitButton);
            this.panel7.Controls.Add(this.OrientationLandscapeButton);
            this.panel7.Controls.Add(this.label11);
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Name = "panel7";
            // 
            // OrientationPortraitButton
            // 
            this.OrientationPortraitButton.BackColor = System.Drawing.Color.White;
            this.OrientationPortraitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.OrientationPortraitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.OrientationPortraitButton, "OrientationPortraitButton");
            this.OrientationPortraitButton.FocusBackColor = System.Drawing.Color.Thistle;
            this.OrientationPortraitButton.FocusBorderColor = System.Drawing.Color.Empty;
            this.OrientationPortraitButton.FocusBorderSize = 1;
            this.OrientationPortraitButton.FocusForeColor = System.Drawing.SystemColors.ControlText;
            this.OrientationPortraitButton.HasPressedState = true;
            this.OrientationPortraitButton.Image = global::WaveletStudio.Designer.Resources.Images.Portrait;
            this.OrientationPortraitButton.Name = "OrientationPortraitButton";
            this.OrientationPortraitButton.NormalBackColor = System.Drawing.Color.White;
            this.OrientationPortraitButton.NormalBorderColor = System.Drawing.Color.Empty;
            this.OrientationPortraitButton.NormalBorderSize = 1;
            this.OrientationPortraitButton.NormalForeColor = System.Drawing.SystemColors.ControlText;
            this.OrientationPortraitButton.Pressed = false;
            this.OrientationPortraitButton.PressedBackColor = System.Drawing.Color.Thistle;
            this.OrientationPortraitButton.PressedBorderColor = System.Drawing.Color.Empty;
            this.OrientationPortraitButton.PressedBorderSize = 1;
            this.OrientationPortraitButton.PressedForeColor = System.Drawing.SystemColors.ControlText;
            this.OrientationPortraitButton.UseVisualStyleBackColor = false;
            this.OrientationPortraitButton.Click += new System.EventHandler(this.OrientationPortraitButton_Click);
            // 
            // OrientationLandscapeButton
            // 
            this.OrientationLandscapeButton.BackColor = System.Drawing.Color.White;
            this.OrientationLandscapeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.OrientationLandscapeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.OrientationLandscapeButton, "OrientationLandscapeButton");
            this.OrientationLandscapeButton.FocusBackColor = System.Drawing.Color.Thistle;
            this.OrientationLandscapeButton.FocusBorderColor = System.Drawing.Color.Empty;
            this.OrientationLandscapeButton.FocusBorderSize = 1;
            this.OrientationLandscapeButton.FocusForeColor = System.Drawing.SystemColors.ControlText;
            this.OrientationLandscapeButton.HasPressedState = true;
            this.OrientationLandscapeButton.Image = global::WaveletStudio.Designer.Resources.Images.Landscape;
            this.OrientationLandscapeButton.Name = "OrientationLandscapeButton";
            this.OrientationLandscapeButton.NormalBackColor = System.Drawing.Color.White;
            this.OrientationLandscapeButton.NormalBorderColor = System.Drawing.Color.Empty;
            this.OrientationLandscapeButton.NormalBorderSize = 1;
            this.OrientationLandscapeButton.NormalForeColor = System.Drawing.SystemColors.ControlText;
            this.OrientationLandscapeButton.Pressed = false;
            this.OrientationLandscapeButton.PressedBackColor = System.Drawing.Color.Thistle;
            this.OrientationLandscapeButton.PressedBorderColor = System.Drawing.Color.Empty;
            this.OrientationLandscapeButton.PressedBorderSize = 1;
            this.OrientationLandscapeButton.PressedForeColor = System.Drawing.SystemColors.ControlText;
            this.OrientationLandscapeButton.UseVisualStyleBackColor = false;
            this.OrientationLandscapeButton.Click += new System.EventHandler(this.OrientationLandscapeButton_Click);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // PrintNowButton
            // 
            this.PrintNowButton.BackColor = System.Drawing.Color.White;
            this.PrintNowButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.PrintNowButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.PrintNowButton, "PrintNowButton");
            this.PrintNowButton.FocusBackColor = System.Drawing.Color.Thistle;
            this.PrintNowButton.FocusBorderColor = System.Drawing.Color.Empty;
            this.PrintNowButton.FocusBorderSize = 1;
            this.PrintNowButton.FocusForeColor = System.Drawing.SystemColors.ControlText;
            this.PrintNowButton.HasPressedState = false;
            this.PrintNowButton.Image = global::WaveletStudio.Designer.Resources.Images.Print;
            this.PrintNowButton.Name = "PrintNowButton";
            this.PrintNowButton.NormalBackColor = System.Drawing.Color.White;
            this.PrintNowButton.NormalBorderColor = System.Drawing.Color.Empty;
            this.PrintNowButton.NormalBorderSize = 1;
            this.PrintNowButton.NormalForeColor = System.Drawing.SystemColors.ControlText;
            this.PrintNowButton.Pressed = false;
            this.PrintNowButton.PressedBackColor = System.Drawing.Color.Thistle;
            this.PrintNowButton.PressedBorderColor = System.Drawing.Color.Empty;
            this.PrintNowButton.PressedBorderSize = 1;
            this.PrintNowButton.PressedForeColor = System.Drawing.SystemColors.ControlText;
            this.PrintNowButton.UseVisualStyleBackColor = false;
            this.PrintNowButton.Click += new System.EventHandler(this.PrintNowButton_Click);
            // 
            // StretchModelToPage
            // 
            resources.ApplyResources(this.StretchModelToPage, "StretchModelToPage");
            this.StretchModelToPage.Name = "StretchModelToPage";
            this.StretchModelToPage.UseVisualStyleBackColor = true;
            this.StretchModelToPage.CheckedChanged += new System.EventHandler(this.StretchModelToPage_CheckedChanged);
            // 
            // ShowGridCheckBox
            // 
            resources.ApplyResources(this.ShowGridCheckBox, "ShowGridCheckBox");
            this.ShowGridCheckBox.Name = "ShowGridCheckBox";
            this.ShowGridCheckBox.UseVisualStyleBackColor = true;
            this.ShowGridCheckBox.CheckedChanged += new System.EventHandler(this.ShowGridCheckBox_CheckedChanged);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.MarginBottomField);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.MarginRightField);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.MarginLeftField);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.MarginTopField);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.label1);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // MarginBottomField
            // 
            this.MarginBottomField.DecimalPlaces = 1;
            this.MarginBottomField.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            resources.ApplyResources(this.MarginBottomField, "MarginBottomField");
            this.MarginBottomField.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.MarginBottomField.Name = "MarginBottomField";
            this.MarginBottomField.ValueChanged += new System.EventHandler(this.MarginInputBoxTextChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // MarginRightField
            // 
            this.MarginRightField.DecimalPlaces = 1;
            this.MarginRightField.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            resources.ApplyResources(this.MarginRightField, "MarginRightField");
            this.MarginRightField.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.MarginRightField.Name = "MarginRightField";
            this.MarginRightField.ValueChanged += new System.EventHandler(this.MarginInputBoxTextChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // MarginLeftField
            // 
            this.MarginLeftField.DecimalPlaces = 1;
            this.MarginLeftField.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            resources.ApplyResources(this.MarginLeftField, "MarginLeftField");
            this.MarginLeftField.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.MarginLeftField.Name = "MarginLeftField";
            this.MarginLeftField.ValueChanged += new System.EventHandler(this.MarginInputBoxTextChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // MarginTopField
            // 
            this.MarginTopField.DecimalPlaces = 1;
            this.MarginTopField.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            resources.ApplyResources(this.MarginTopField, "MarginTopField");
            this.MarginTopField.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.MarginTopField.Name = "MarginTopField";
            this.MarginTopField.ValueChanged += new System.EventHandler(this.MarginInputBoxTextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // FileForm
            // 
            this.Appearance.BackgroundStyle = Qios.DevSuite.Components.QColorStyle.Solid;
            this.Appearance.BorderWidth = 0;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ColorScheme.RibbonFormBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.MenuPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FileForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FileWindow_Load);
            this.MenuPanel.ResumeLayout(false);
            this.TabControl.ResumeLayout(false);
            this.InformationsTabPage.ResumeLayout(false);
            this.InformationsTabPage.PerformLayout();
            this.OpenTabPage.ResumeLayout(false);
            this.OpenTabPage.PerformLayout();
            this.PrintTabPage.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomTrackBar)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MarginBottomField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarginRightField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarginLeftField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarginTopField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel MenuPanel;
        private NoHeaderTabControl TabControl;
        private System.Windows.Forms.TabPage OpenTabPage;
        private System.Windows.Forms.TabPage PrintTabPage;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PrintPreviewControl PrintPreviewControl;
        private System.Windows.Forms.Label ZoomLabel;
        private Qios.DevSuite.Components.QButton ZoomPlusButton;
        private Qios.DevSuite.Components.QButton ZoomMinusButton;
        private System.Windows.Forms.TrackBar ZoomTrackBar;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.NumericUpDown MarginLeftField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown MarginTopField;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown MarginBottomField;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown MarginRightField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox StretchModelToPage;
        private System.Windows.Forms.CheckBox ShowGridCheckBox;
        private WaveletStudio.Designer.Utils.FlatButton PrintNowButton;
        private System.Windows.Forms.Panel panel7;
        private WaveletStudio.Designer.Utils.FlatButton OrientationLandscapeButton;
        private System.Windows.Forms.Label label11;
        private WaveletStudio.Designer.Utils.FlatButton OrientationPortraitButton;
        private System.Windows.Forms.FlowLayoutPanel RecentFilesPannel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage InformationsTabPage;
        private System.Windows.Forms.Label label8;
        private FlatButton OpenFromComputerButton;
        private System.Windows.Forms.TextBox NotesField;
        private System.Windows.Forms.Label ModifiedByLabel;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label AuthorLabel;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label CanvasSizeLabel;
        private System.Windows.Forms.Label FileSizeLabel;
        private System.Windows.Forms.Label ConnectionsLabel;
        private System.Windows.Forms.Label BlocksLabel;
        private System.Windows.Forms.Label LastModificationLabel;
        private System.Windows.Forms.Label CreatedAtLabel;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private Separator separator1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button InformationsButton;
        private System.Windows.Forms.Button NewButton;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button SaveAsButton;
        private System.Windows.Forms.Button PrintButton;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Button GenerateCodeButton;
        private System.Windows.Forms.Button OptionsButton;
        private System.Windows.Forms.Button ExitButton;
        private Separator separator2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label6;

    }
}