using DiagramNet.Events;
using Qios.DevSuite.Components;

namespace WaveletStudio.MainApplication.Forms
{
    partial class PrintPreviewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintPreviewForm));
            this.Caption = new Qios.DevSuite.Components.Ribbon.QRibbonCaption();
            this.Ribbon = new Qios.DevSuite.Components.Ribbon.QRibbon();
            this.PreviewOptionsPage = new Qios.DevSuite.Components.Ribbon.QRibbonPage();
            this.PrintPanel = new Qios.DevSuite.Components.Ribbon.QRibbonPanel();
            this.PrintComposite = new Qios.DevSuite.Components.QCompositeGroup();
            this.PrintCompositeItem = new Qios.DevSuite.Components.QCompositeItem();
            this.PrintGroup = new Qios.DevSuite.Components.QCompositeGroup();
            this.PrintInnerGroup = new Qios.DevSuite.Components.QCompositeGroup();
            this.qCompositeImage1 = new Qios.DevSuite.Components.QCompositeImage();
            this.PrintText = new Qios.DevSuite.Components.QCompositeText();
            this.OptionsPanel = new Qios.DevSuite.Components.Ribbon.QRibbonPanel();
            this.SettingsComposite = new Qios.DevSuite.Components.QCompositeItem();
            this.qCompositeGroup6 = new Qios.DevSuite.Components.QCompositeGroup();
            this.qCompositeGroup7 = new Qios.DevSuite.Components.QCompositeGroup();
            this.qCompositeImage5 = new Qios.DevSuite.Components.QCompositeImage();
            this.qCompositeText8 = new Qios.DevSuite.Components.QCompositeText();
            this.GridComposite = new Qios.DevSuite.Components.QCompositeItem();
            this.qCompositeGroup2 = new Qios.DevSuite.Components.QCompositeGroup();
            this.GridInnerGroup = new Qios.DevSuite.Components.QCompositeGroup();
            this.qCompositeImage2 = new Qios.DevSuite.Components.QCompositeImage();
            this.qCompositeText1 = new Qios.DevSuite.Components.QCompositeText();
            this.StretchComposite = new Qios.DevSuite.Components.QCompositeItem();
            this.qCompositeGroup4 = new Qios.DevSuite.Components.QCompositeGroup();
            this.StretchInnerGroup = new Qios.DevSuite.Components.QCompositeGroup();
            this.qCompositeImage3 = new Qios.DevSuite.Components.QCompositeImage();
            this.qCompositeText2 = new Qios.DevSuite.Components.QCompositeText();
            this.MarginsPanel = new Qios.DevSuite.Components.Ribbon.QRibbonPanel();
            this.qRibbonItemGroup1 = new Qios.DevSuite.Components.Ribbon.QRibbonItemGroup();
            this.qCompositeItem1 = new Qios.DevSuite.Components.QCompositeItem();
            this.qCompositeItem2 = new Qios.DevSuite.Components.QCompositeItem();
            this.qCompositeText7 = new Qios.DevSuite.Components.QCompositeText();
            this.MarginTopField = new Qios.DevSuite.Components.QCompositeItemInputBox();
            this.qCompositeItem3 = new Qios.DevSuite.Components.QCompositeItem();
            this.qCompositeText4 = new Qios.DevSuite.Components.QCompositeText();
            this.MarginLeftField = new Qios.DevSuite.Components.QCompositeItemInputBox();
            this.qCompositeText5 = new Qios.DevSuite.Components.QCompositeText();
            this.MarginRightField = new Qios.DevSuite.Components.QCompositeItemInputBox();
            this.qCompositeItem4 = new Qios.DevSuite.Components.QCompositeItem();
            this.qCompositeText6 = new Qios.DevSuite.Components.QCompositeText();
            this.MarginBottomField = new Qios.DevSuite.Components.QCompositeItemInputBox();
            this.qRibbonPanel1 = new Qios.DevSuite.Components.Ribbon.QRibbonPanel();
            this.CloseComposite = new Qios.DevSuite.Components.QCompositeItem();
            this.qCompositeGroup1 = new Qios.DevSuite.Components.QCompositeGroup();
            this.qCompositeGroup3 = new Qios.DevSuite.Components.QCompositeGroup();
            this.qCompositeImage4 = new Qios.DevSuite.Components.QCompositeImage();
            this.qCompositeText3 = new Qios.DevSuite.Components.QCompositeText();
            this.CenterPanel = new System.Windows.Forms.Panel();
            this.PrintPreviewControl = new System.Windows.Forms.PrintPreviewControl();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.ZoomLabel = new System.Windows.Forms.Label();
            this.ZoomPlusButton = new Qios.DevSuite.Components.QButton();
            this.ZoomMinusButton = new Qios.DevSuite.Components.QButton();
            this.ZoomTrackBar = new System.Windows.Forms.TrackBar();
            this.diagramFormProperties1 = new WaveletStudio.MainApplication.Forms.DiagramFormProperties();
            ((System.ComponentModel.ISupportInitialize)(this.Caption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ribbon)).BeginInit();
            this.Ribbon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewOptionsPage)).BeginInit();
            this.CenterPanel.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // Caption
            // 
            resources.ApplyResources(this.Caption, "Caption");
            this.Caption.Configuration.ApplicationButtonAreaConfiguration.Margin = new Qios.DevSuite.Components.QMargin(0, 4, 0, -8);
            this.Caption.Configuration.IconConfiguration.Visible = Qios.DevSuite.Components.QTristateBool.False;
            this.Caption.Name = "Caption";
            // 
            // Ribbon
            // 
            resources.ApplyResources(this.Ribbon, "Ribbon");
            this.Ribbon.ActiveTabPage = this.PreviewOptionsPage;
            this.Ribbon.Controls.Add(this.PreviewOptionsPage);
            this.Ribbon.Cursor = System.Windows.Forms.Cursors.Default;
            this.Ribbon.Form = this;
            this.Ribbon.Name = "Ribbon";
            this.Ribbon.PersistGuid = new System.Guid("f549a281-ecb8-4c57-ab72-08ae6e49b352");
            this.Ribbon.TabStripConfiguration.HelpButtonVisible = false;
            // 
            // PreviewOptionsPage
            // 
            resources.ApplyResources(this.PreviewOptionsPage, "PreviewOptionsPage");
            this.PreviewOptionsPage.ButtonOrder = 0;
            this.PreviewOptionsPage.Items.Add(this.PrintPanel);
            this.PreviewOptionsPage.Items.Add(this.OptionsPanel);
            this.PreviewOptionsPage.Items.Add(this.MarginsPanel);
            this.PreviewOptionsPage.Items.Add(this.qRibbonPanel1);
            this.PreviewOptionsPage.Name = "PreviewOptionsPage";
            this.PreviewOptionsPage.PersistGuid = new System.Guid("ea85f321-5f5d-4be5-b84e-e09b73b23a86");
            // 
            // PrintPanel
            // 
            this.PrintPanel.Configuration.CaptionConfiguration.ShowDialogConfiguration.Visible = Qios.DevSuite.Components.QTristateBool.False;
            resources.ApplyResources(this.PrintPanel, "PrintPanel");
            this.PrintPanel.Items.Add(this.PrintComposite);
            // 
            // PrintComposite
            // 
            this.PrintComposite.ColorScheme.ButtonPressedBackground1.SetColor("Default", System.Drawing.Color.Empty, false);
            this.PrintComposite.ColorScheme.ButtonPressedBackground1.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.PrintComposite.ColorScheme.ButtonPressedBackground1.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.PrintComposite.ColorScheme.ButtonPressedBackground1.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.PrintComposite.ColorScheme.ButtonPressedBackground1.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.PrintComposite.ColorScheme.ButtonPressedBackground2.SetColor("Default", System.Drawing.Color.Empty, false);
            this.PrintComposite.ColorScheme.ButtonPressedBackground2.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.PrintComposite.ColorScheme.ButtonPressedBackground2.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.PrintComposite.ColorScheme.ButtonPressedBackground2.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.PrintComposite.ColorScheme.ButtonPressedBackground2.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.PrintComposite.ColorScheme.CompositeItemBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.CompositeItemBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.CompositeItemBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.CompositeItemBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.CompositeItemBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.CompositeItemBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.CompositeItemBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.CompositeItemBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.CompositeItemBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.CompositeItemBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.CompositeItemHotBackground1.SetColor("Default", System.Drawing.Color.Transparent, false);
            this.PrintComposite.ColorScheme.CompositeItemHotBackground1.SetColor("LunaBlue", System.Drawing.Color.Transparent, false);
            this.PrintComposite.ColorScheme.CompositeItemHotBackground1.SetColor("LunaOlive", System.Drawing.Color.Transparent, false);
            this.PrintComposite.ColorScheme.CompositeItemHotBackground1.SetColor("LunaSilver", System.Drawing.Color.Transparent, false);
            this.PrintComposite.ColorScheme.CompositeItemHotBackground1.SetColor("VistaBlack", System.Drawing.Color.Transparent, false);
            this.PrintComposite.ColorScheme.CompositeItemHotBackground2.SetColor("Default", System.Drawing.Color.Transparent, false);
            this.PrintComposite.ColorScheme.CompositeItemHotBackground2.SetColor("LunaBlue", System.Drawing.Color.Transparent, false);
            this.PrintComposite.ColorScheme.CompositeItemHotBackground2.SetColor("LunaOlive", System.Drawing.Color.Transparent, false);
            this.PrintComposite.ColorScheme.CompositeItemHotBackground2.SetColor("LunaSilver", System.Drawing.Color.Transparent, false);
            this.PrintComposite.ColorScheme.CompositeItemHotBackground2.SetColor("VistaBlack", System.Drawing.Color.Transparent, false);
            this.PrintComposite.ColorScheme.CompositeItemHotBorder.SetColor("Default", System.Drawing.Color.Transparent, false);
            this.PrintComposite.ColorScheme.CompositeItemHotBorder.SetColor("LunaBlue", System.Drawing.Color.Transparent, false);
            this.PrintComposite.ColorScheme.CompositeItemHotBorder.SetColor("LunaOlive", System.Drawing.Color.Transparent, false);
            this.PrintComposite.ColorScheme.CompositeItemHotBorder.SetColor("LunaSilver", System.Drawing.Color.Transparent, false);
            this.PrintComposite.ColorScheme.CompositeItemHotBorder.SetColor("VistaBlack", System.Drawing.Color.Transparent, false);
            this.PrintComposite.ColorScheme.CompositeItemPressedBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.CompositeItemPressedBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.CompositeItemPressedBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.CompositeItemPressedBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.PrintComposite.ColorScheme.Scope = Qios.DevSuite.Components.QColorSchemeScope.All;
            this.PrintComposite.Configuration.ShrinkHorizontal = true;
            this.PrintComposite.Configuration.ShrinkVertical = true;
            this.PrintComposite.Configuration.StretchHorizontal = true;
            this.PrintComposite.Items.Add(this.PrintCompositeItem);
            // 
            // PrintCompositeItem
            // 
            this.PrintCompositeItem.ColorScheme.CompositeItemBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemHotBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemHotBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemHotBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemHotBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemHotBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemHotBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemHotBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemHotBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemHotBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemHotBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemPressedBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemPressedBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemPressedBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.PrintCompositeItem.ColorScheme.CompositeItemPressedBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.PrintCompositeItem.Configuration.AlignmentHorizontal = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.PrintCompositeItem.Configuration.Direction = Qios.DevSuite.Components.QPartDirection.Vertical;
            this.PrintCompositeItem.Configuration.Padding = new Qios.DevSuite.Components.QPadding(0, 0, 0, 0);
            this.PrintCompositeItem.Configuration.ShrinkVertical = true;
            resources.ApplyResources(this.PrintCompositeItem, "PrintCompositeItem");
            this.PrintCompositeItem.Items.Add(this.PrintGroup);
            this.PrintCompositeItem.ItemActivated += new Qios.DevSuite.Components.QCompositeEventHandler(this.PrintCompositeItemItemActivated);
            // 
            // PrintGroup
            // 
            this.PrintGroup.Configuration.Padding = new Qios.DevSuite.Components.QPadding(4, 4, 4, 4);
            this.PrintGroup.Configuration.ShrinkVertical = true;
            this.PrintGroup.Items.Add(this.PrintInnerGroup);
            // 
            // PrintInnerGroup
            // 
            this.PrintInnerGroup.ColorScheme.CompositeGroupBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeGroupBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeGroupBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeGroupBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeGroupBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeGroupBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeGroupBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeGroupBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeGroupBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeGroupBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeItemHotBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeItemHotBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeItemHotBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeItemHotBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeItemHotBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeItemHotBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeItemHotBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeItemHotBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeItemHotBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeItemPressedBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeItemPressedBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeItemPressedBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeScrollButtonHotBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeScrollButtonHotBorder.SetColor("Default", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeScrollButtonHotBorder.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeScrollButtonHotBorder.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeScrollButtonHotBorder.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeScrollButtonHotBorder.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.PrintInnerGroup.ColorScheme.CompositeScrollButtonPressedBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.PrintInnerGroup.Configuration.AlignmentHorizontal = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.PrintInnerGroup.Configuration.Direction = Qios.DevSuite.Components.QPartDirection.Vertical;
            this.PrintInnerGroup.Configuration.Padding = new Qios.DevSuite.Components.QPadding(4, 4, 4, 4);
            this.PrintInnerGroup.Items.Add(this.qCompositeImage1);
            this.PrintInnerGroup.Items.Add(this.PrintText);
            // 
            // qCompositeImage1
            // 
            this.qCompositeImage1.Configuration.AlignmentHorizontal = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.qCompositeImage1.Configuration.MaximumSize = new System.Drawing.Size(64, 48);
            this.qCompositeImage1.Image = global::WaveletStudio.MainApplication.Properties.Resources.iconPrint;
            // 
            // PrintText
            // 
            this.PrintText.Configuration.ContentAlignmentHorizontal = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.PrintText.Configuration.FontDefinition = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.PrintText.Configuration.FontDefinitionExpanded = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.PrintText.Configuration.FontDefinitionHot = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.PrintText.Configuration.FontDefinitionPressed = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.PrintText.Configuration.StretchHorizontal = true;
            resources.ApplyResources(this.PrintText, "PrintText");
            // 
            // OptionsPanel
            // 
            this.OptionsPanel.Configuration.CaptionConfiguration.ShowDialogConfiguration.Visible = Qios.DevSuite.Components.QTristateBool.False;
            resources.ApplyResources(this.OptionsPanel, "OptionsPanel");
            this.OptionsPanel.Items.Add(this.SettingsComposite);
            this.OptionsPanel.Items.Add(this.GridComposite);
            this.OptionsPanel.Items.Add(this.StretchComposite);
            // 
            // SettingsComposite
            // 
            this.SettingsComposite.ColorScheme.CompositeItemBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.SettingsComposite.ColorScheme.CompositeItemBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.SettingsComposite.ColorScheme.CompositeItemBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.SettingsComposite.ColorScheme.CompositeItemBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.SettingsComposite.ColorScheme.CompositeItemBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.SettingsComposite.ColorScheme.CompositeItemBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.SettingsComposite.ColorScheme.CompositeItemBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.SettingsComposite.ColorScheme.CompositeItemBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.SettingsComposite.ColorScheme.CompositeItemBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.SettingsComposite.ColorScheme.CompositeItemBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.SettingsComposite.ColorScheme.CompositeItemHotBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.SettingsComposite.ColorScheme.CompositeItemHotBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.SettingsComposite.ColorScheme.CompositeItemHotBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.SettingsComposite.ColorScheme.CompositeItemHotBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.SettingsComposite.ColorScheme.CompositeItemHotBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.SettingsComposite.ColorScheme.CompositeItemHotBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.SettingsComposite.ColorScheme.CompositeItemHotBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.SettingsComposite.ColorScheme.CompositeItemHotBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.SettingsComposite.ColorScheme.CompositeItemHotBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.SettingsComposite.ColorScheme.CompositeItemHotBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.SettingsComposite.Configuration.AlignmentHorizontal = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.SettingsComposite.Configuration.Direction = Qios.DevSuite.Components.QPartDirection.Vertical;
            this.SettingsComposite.Configuration.Padding = new Qios.DevSuite.Components.QPadding(0, 0, 0, 0);
            this.SettingsComposite.Configuration.ShrinkVertical = true;
            resources.ApplyResources(this.SettingsComposite, "SettingsComposite");
            this.SettingsComposite.Items.Add(this.qCompositeGroup6);
            this.SettingsComposite.ItemActivated += new Qios.DevSuite.Components.QCompositeEventHandler(this.SettingsCompositeItemActivated);
            // 
            // qCompositeGroup6
            // 
            this.qCompositeGroup6.Configuration.Padding = new Qios.DevSuite.Components.QPadding(4, 4, 4, 4);
            this.qCompositeGroup6.Configuration.ShrinkVertical = true;
            this.qCompositeGroup6.Items.Add(this.qCompositeGroup7);
            // 
            // qCompositeGroup7
            // 
            this.qCompositeGroup7.ColorScheme.CompositeGroupBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeGroupBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeGroupBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeGroupBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeGroupBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeGroupBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeGroupBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeGroupBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeGroupBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeGroupBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeItemHotBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeItemHotBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeItemHotBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeItemHotBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeItemHotBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeItemHotBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeItemHotBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeItemHotBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeItemHotBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeItemPressedBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeItemPressedBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeItemPressedBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeScrollButtonHotBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeScrollButtonHotBorder.SetColor("Default", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeScrollButtonHotBorder.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeScrollButtonHotBorder.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeScrollButtonHotBorder.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeScrollButtonHotBorder.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.qCompositeGroup7.ColorScheme.CompositeScrollButtonPressedBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.qCompositeGroup7.Configuration.Direction = Qios.DevSuite.Components.QPartDirection.Vertical;
            this.qCompositeGroup7.Configuration.Padding = new Qios.DevSuite.Components.QPadding(4, 4, 4, 4);
            this.qCompositeGroup7.Items.Add(this.qCompositeImage5);
            this.qCompositeGroup7.Items.Add(this.qCompositeText8);
            // 
            // qCompositeImage5
            // 
            this.qCompositeImage5.Configuration.AlignmentHorizontal = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.qCompositeImage5.Configuration.MaximumSize = new System.Drawing.Size(64, 48);
            this.qCompositeImage5.Image = global::WaveletStudio.MainApplication.Properties.Resources.iconPrintSettings;
            // 
            // qCompositeText8
            // 
            this.qCompositeText8.Configuration.ContentAlignmentHorizontal = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.qCompositeText8.Configuration.FontDefinition = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.qCompositeText8.Configuration.FontDefinitionExpanded = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.qCompositeText8.Configuration.FontDefinitionHot = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.qCompositeText8.Configuration.FontDefinitionPressed = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.qCompositeText8.Configuration.StretchHorizontal = true;
            resources.ApplyResources(this.qCompositeText8, "qCompositeText8");
            // 
            // GridComposite
            // 
            this.GridComposite.Checked = true;
            this.GridComposite.ColorScheme.CompositeItemBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.GridComposite.ColorScheme.CompositeItemBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.GridComposite.ColorScheme.CompositeItemBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.GridComposite.ColorScheme.CompositeItemBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.GridComposite.ColorScheme.CompositeItemBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.GridComposite.ColorScheme.CompositeItemBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.GridComposite.ColorScheme.CompositeItemBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.GridComposite.ColorScheme.CompositeItemBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.GridComposite.ColorScheme.CompositeItemBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.GridComposite.ColorScheme.CompositeItemBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.GridComposite.ColorScheme.CompositeItemHotBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.GridComposite.ColorScheme.CompositeItemHotBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.GridComposite.ColorScheme.CompositeItemHotBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.GridComposite.ColorScheme.CompositeItemHotBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.GridComposite.ColorScheme.CompositeItemHotBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.GridComposite.ColorScheme.CompositeItemHotBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.GridComposite.ColorScheme.CompositeItemHotBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.GridComposite.ColorScheme.CompositeItemHotBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.GridComposite.ColorScheme.CompositeItemHotBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.GridComposite.ColorScheme.CompositeItemHotBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.GridComposite.Configuration.AlignmentHorizontal = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.GridComposite.Configuration.Direction = Qios.DevSuite.Components.QPartDirection.Vertical;
            this.GridComposite.Configuration.Padding = new Qios.DevSuite.Components.QPadding(0, 0, 0, 0);
            this.GridComposite.Configuration.ShrinkVertical = true;
            resources.ApplyResources(this.GridComposite, "GridComposite");
            this.GridComposite.Items.Add(this.qCompositeGroup2);
            this.GridComposite.ItemActivated += new Qios.DevSuite.Components.QCompositeEventHandler(this.GridCompositeItemActivated);
            // 
            // qCompositeGroup2
            // 
            this.qCompositeGroup2.Configuration.Padding = new Qios.DevSuite.Components.QPadding(4, 4, 4, 4);
            this.qCompositeGroup2.Configuration.ShrinkVertical = true;
            this.qCompositeGroup2.Items.Add(this.GridInnerGroup);
            // 
            // GridInnerGroup
            // 
            this.GridInnerGroup.ColorScheme.CompositeGroupBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeGroupBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeGroupBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeGroupBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeGroupBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeGroupBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeGroupBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeGroupBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeGroupBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeGroupBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeItemHotBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeItemHotBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeItemHotBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeItemHotBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeItemHotBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeItemHotBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeItemHotBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeItemHotBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeItemHotBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeItemPressedBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeItemPressedBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeItemPressedBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeScrollButtonHotBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeScrollButtonHotBorder.SetColor("Default", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeScrollButtonHotBorder.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeScrollButtonHotBorder.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeScrollButtonHotBorder.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeScrollButtonHotBorder.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.GridInnerGroup.ColorScheme.CompositeScrollButtonPressedBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.GridInnerGroup.Configuration.Direction = Qios.DevSuite.Components.QPartDirection.Vertical;
            this.GridInnerGroup.Configuration.Padding = new Qios.DevSuite.Components.QPadding(4, 4, 4, 4);
            this.GridInnerGroup.Items.Add(this.qCompositeImage2);
            this.GridInnerGroup.Items.Add(this.qCompositeText1);
            // 
            // qCompositeImage2
            // 
            this.qCompositeImage2.Configuration.MaximumSize = new System.Drawing.Size(64, 48);
            this.qCompositeImage2.Image = global::WaveletStudio.MainApplication.Properties.Resources.iconGrid;
            // 
            // qCompositeText1
            // 
            this.qCompositeText1.Configuration.ContentAlignmentHorizontal = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.qCompositeText1.Configuration.FontDefinition = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.qCompositeText1.Configuration.FontDefinitionExpanded = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.qCompositeText1.Configuration.FontDefinitionHot = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.qCompositeText1.Configuration.FontDefinitionPressed = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.qCompositeText1.Configuration.StretchHorizontal = true;
            resources.ApplyResources(this.qCompositeText1, "qCompositeText1");
            // 
            // StretchComposite
            // 
            this.StretchComposite.Checked = true;
            this.StretchComposite.ColorScheme.CompositeItemBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.StretchComposite.ColorScheme.CompositeItemBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.StretchComposite.ColorScheme.CompositeItemBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.StretchComposite.ColorScheme.CompositeItemBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.StretchComposite.ColorScheme.CompositeItemBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.StretchComposite.ColorScheme.CompositeItemBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.StretchComposite.ColorScheme.CompositeItemBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.StretchComposite.ColorScheme.CompositeItemBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.StretchComposite.ColorScheme.CompositeItemBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.StretchComposite.ColorScheme.CompositeItemBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.StretchComposite.ColorScheme.CompositeItemHotBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.StretchComposite.ColorScheme.CompositeItemHotBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.StretchComposite.ColorScheme.CompositeItemHotBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.StretchComposite.ColorScheme.CompositeItemHotBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.StretchComposite.ColorScheme.CompositeItemHotBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.StretchComposite.ColorScheme.CompositeItemHotBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.StretchComposite.ColorScheme.CompositeItemHotBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.StretchComposite.ColorScheme.CompositeItemHotBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.StretchComposite.ColorScheme.CompositeItemHotBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.StretchComposite.ColorScheme.CompositeItemHotBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.StretchComposite.Configuration.AlignmentHorizontal = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.StretchComposite.Configuration.Direction = Qios.DevSuite.Components.QPartDirection.Vertical;
            this.StretchComposite.Configuration.Padding = new Qios.DevSuite.Components.QPadding(0, 0, 0, 0);
            this.StretchComposite.Configuration.ShrinkVertical = true;
            resources.ApplyResources(this.StretchComposite, "StretchComposite");
            this.StretchComposite.Items.Add(this.qCompositeGroup4);
            this.StretchComposite.ItemActivated += new Qios.DevSuite.Components.QCompositeEventHandler(this.StretchCompositeItemActivated);
            // 
            // qCompositeGroup4
            // 
            this.qCompositeGroup4.Configuration.Padding = new Qios.DevSuite.Components.QPadding(4, 4, 4, 4);
            this.qCompositeGroup4.Configuration.ShrinkVertical = true;
            this.qCompositeGroup4.Items.Add(this.StretchInnerGroup);
            // 
            // StretchInnerGroup
            // 
            this.StretchInnerGroup.ColorScheme.CompositeGroupBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeGroupBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeGroupBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeGroupBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeGroupBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeGroupBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeGroupBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeGroupBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeGroupBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeGroupBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeItemHotBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeItemHotBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeItemHotBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeItemHotBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeItemHotBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeItemHotBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeItemHotBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeItemHotBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeItemHotBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeItemPressedBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeItemPressedBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeItemPressedBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeScrollButtonHotBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeScrollButtonHotBorder.SetColor("Default", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeScrollButtonHotBorder.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeScrollButtonHotBorder.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeScrollButtonHotBorder.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeScrollButtonHotBorder.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.StretchInnerGroup.ColorScheme.CompositeScrollButtonPressedBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.StretchInnerGroup.Configuration.Direction = Qios.DevSuite.Components.QPartDirection.Vertical;
            this.StretchInnerGroup.Configuration.Padding = new Qios.DevSuite.Components.QPadding(4, 4, 4, 4);
            this.StretchInnerGroup.Items.Add(this.qCompositeImage3);
            this.StretchInnerGroup.Items.Add(this.qCompositeText2);
            // 
            // qCompositeImage3
            // 
            this.qCompositeImage3.Configuration.AlignmentHorizontal = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.qCompositeImage3.Configuration.MaximumSize = new System.Drawing.Size(64, 48);
            this.qCompositeImage3.Image = global::WaveletStudio.MainApplication.Properties.Resources.iconStretch;
            // 
            // qCompositeText2
            // 
            this.qCompositeText2.Configuration.ContentAlignmentHorizontal = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.qCompositeText2.Configuration.FontDefinition = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.qCompositeText2.Configuration.FontDefinitionExpanded = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.qCompositeText2.Configuration.FontDefinitionHot = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.qCompositeText2.Configuration.FontDefinitionPressed = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.qCompositeText2.Configuration.StretchHorizontal = true;
            resources.ApplyResources(this.qCompositeText2, "qCompositeText2");
            // 
            // MarginsPanel
            // 
            this.MarginsPanel.ColorScheme.RibbonItemActiveBackground1.ColorReference = "@CompositeItemBackground1";
            this.MarginsPanel.ColorScheme.RibbonItemActiveBackground2.ColorReference = "@CompositeItemBackground2";
            this.MarginsPanel.ColorScheme.RibbonItemHotBackground1.ColorReference = "@CompositeItemBackground1";
            this.MarginsPanel.ColorScheme.RibbonItemHotBackground2.ColorReference = "@CompositeItemBackground2";
            this.MarginsPanel.ColorScheme.RibbonItemHotBorder.SetColor("Default", System.Drawing.Color.Empty, false);
            this.MarginsPanel.Configuration.CaptionConfiguration.ShowDialogConfiguration.Visible = Qios.DevSuite.Components.QTristateBool.False;
            resources.ApplyResources(this.MarginsPanel, "MarginsPanel");
            this.MarginsPanel.Items.Add(this.qRibbonItemGroup1);
            this.MarginsPanel.Items.Add(this.qCompositeItem1);
            // 
            // qCompositeItem1
            // 
            this.qCompositeItem1.ColorScheme.CompositeItemBorder.SetColor("Default", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemBorder.SetColor("LunaBlue", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemBorder.SetColor("LunaOlive", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemBorder.SetColor("LunaSilver", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemBorder.SetColor("VistaBlack", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemExpandedBackground1.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemExpandedBackground1.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemExpandedBackground1.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemExpandedBackground1.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemExpandedBackground1.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemExpandedBackground2.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemExpandedBackground2.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemExpandedBackground2.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemExpandedBackground2.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemExpandedBackground2.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemExpandedBorder.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemExpandedBorder.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemExpandedBorder.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemExpandedBorder.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemExpandedBorder.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemHotBackground1.SetColor("Default", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemHotBackground1.SetColor("LunaBlue", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemHotBackground1.SetColor("LunaOlive", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemHotBackground1.SetColor("LunaSilver", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemHotBackground1.SetColor("VistaBlack", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemHotBackground2.SetColor("Default", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemHotBackground2.SetColor("LunaBlue", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemHotBackground2.SetColor("LunaOlive", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemHotBackground2.SetColor("LunaSilver", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemHotBackground2.SetColor("VistaBlack", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemHotBorder.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemHotBorder.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemHotBorder.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemHotBorder.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemHotBorder.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem1.ColorScheme.CompositeItemPressedBackground1.SetColor("Default", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaBlue", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaOlive", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaSilver", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemPressedBackground1.SetColor("VistaBlack", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemPressedBackground2.SetColor("Default", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaBlue", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaOlive", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaSilver", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.ColorScheme.CompositeItemPressedBackground2.SetColor("VistaBlack", System.Drawing.Color.Transparent, false);
            this.qCompositeItem1.Configuration.Direction = Qios.DevSuite.Components.QPartDirection.Vertical;
            this.qCompositeItem1.Configuration.Padding = new Qios.DevSuite.Components.QPadding(1, 5, 1, 1);
            this.qCompositeItem1.Configuration.StretchVertical = true;
            resources.ApplyResources(this.qCompositeItem1, "qCompositeItem1");
            this.qCompositeItem1.Items.Add(this.qCompositeItem2);
            this.qCompositeItem1.Items.Add(this.qCompositeItem3);
            this.qCompositeItem1.Items.Add(this.qCompositeItem4);
            // 
            // qCompositeItem2
            // 
            this.qCompositeItem2.ColorScheme.CompositeItemHotBackground1.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemHotBackground1.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemHotBackground1.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemHotBackground1.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemHotBackground1.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemHotBackground2.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemHotBackground2.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemHotBackground2.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemHotBackground2.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemHotBackground2.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemHotBorder.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemHotBorder.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemHotBorder.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemHotBorder.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemHotBorder.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemPressedBackground1.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemPressedBackground1.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemPressedBackground2.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemPressedBackground2.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemPressedBorder.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemPressedBorder.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemPressedBorder.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemPressedBorder.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.ColorScheme.CompositeItemPressedBorder.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem2.Configuration.Padding = new Qios.DevSuite.Components.QPadding(43, 1, 1, 1);
            this.qCompositeItem2.Configuration.StretchHorizontal = true;
            resources.ApplyResources(this.qCompositeItem2, "qCompositeItem2");
            this.qCompositeItem2.Items.Add(this.qCompositeText7);
            this.qCompositeItem2.Items.Add(this.MarginTopField);
            // 
            // qCompositeText7
            // 
            this.qCompositeText7.Configuration.ContentAlignmentVertical = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.qCompositeText7.Configuration.StretchVertical = true;
            resources.ApplyResources(this.qCompositeText7, "qCompositeText7");
            // 
            // MarginTopField
            // 
            this.MarginTopField.ControlSize = new System.Drawing.Size(45, 19);
            // 
            // 
            // 
            this.MarginTopField.InputBox.AccessibleDescription = resources.GetString("MarginTopField.InputBox.AccessibleDescription");
            this.MarginTopField.InputBox.AccessibleName = resources.GetString("MarginTopField.InputBox.AccessibleName");
            this.MarginTopField.InputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("MarginTopField.InputBox.Anchor")));
            this.MarginTopField.InputBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MarginTopField.InputBox.BackgroundImage")));
            this.MarginTopField.InputBox.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("MarginTopField.InputBox.BackgroundImageLayout")));
            this.MarginTopField.InputBox.CueText = resources.GetString("MarginTopField.InputBox.CueText");
            this.MarginTopField.InputBox.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("MarginTopField.InputBox.Dock")));
            this.MarginTopField.InputBox.Font = ((System.Drawing.Font)(resources.GetObject("MarginTopField.InputBox.Font")));
            this.MarginTopField.InputBox.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("MarginTopField.InputBox.ImeMode")));
            this.MarginTopField.InputBox.Location = ((System.Drawing.Point)(resources.GetObject("MarginTopField.InputBox.Location")));
            this.MarginTopField.InputBox.MaxLength = ((int)(resources.GetObject("MarginTopField.InputBox.MaxLength")));
            this.MarginTopField.InputBox.Multiline = ((bool)(resources.GetObject("MarginTopField.InputBox.Multiline")));
            this.MarginTopField.InputBox.Name = "";
            this.MarginTopField.InputBox.PasswordChar = ((char)(resources.GetObject("MarginTopField.InputBox.PasswordChar")));
            this.MarginTopField.InputBox.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("MarginTopField.InputBox.RightToLeft")));
            this.MarginTopField.InputBox.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("MarginTopField.InputBox.ScrollBars")));
            this.MarginTopField.InputBox.Size = ((System.Drawing.Size)(resources.GetObject("MarginTopField.InputBox.Size")));
            this.MarginTopField.InputBox.TabIndex = ((int)(resources.GetObject("MarginTopField.InputBox.TabIndex")));
            this.MarginTopField.InputBox.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("MarginTopField.InputBox.TextAlign")));
            this.MarginTopField.InputBox.ToolTipText = resources.GetString("MarginTopField.InputBox.ToolTipText");
            this.MarginTopField.InputBox.WordWrap = ((bool)(resources.GetObject("MarginTopField.InputBox.WordWrap")));
            this.MarginTopField.InputBox.Leave += new System.EventHandler(this.MarginInputBoxTextChanged);
            // 
            // qCompositeItem3
            // 
            this.qCompositeItem3.ColorScheme.CompositeItemHotBackground1.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemHotBackground1.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemHotBackground1.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemHotBackground1.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemHotBackground1.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemHotBackground2.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemHotBackground2.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemHotBackground2.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemHotBackground2.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemHotBackground2.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemHotBorder.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemHotBorder.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemHotBorder.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemHotBorder.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemHotBorder.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemPressedBackground1.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemPressedBackground1.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemPressedBackground2.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemPressedBackground2.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemPressedBorder.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemPressedBorder.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemPressedBorder.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemPressedBorder.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.ColorScheme.CompositeItemPressedBorder.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem3.Configuration.StretchHorizontal = true;
            resources.ApplyResources(this.qCompositeItem3, "qCompositeItem3");
            this.qCompositeItem3.Items.Add(this.qCompositeText4);
            this.qCompositeItem3.Items.Add(this.MarginLeftField);
            this.qCompositeItem3.Items.Add(this.qCompositeText5);
            this.qCompositeItem3.Items.Add(this.MarginRightField);
            // 
            // qCompositeText4
            // 
            this.qCompositeText4.Configuration.ContentAlignmentVertical = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.qCompositeText4.Configuration.StretchVertical = true;
            resources.ApplyResources(this.qCompositeText4, "qCompositeText4");
            // 
            // MarginLeftField
            // 
            this.MarginLeftField.ControlSize = new System.Drawing.Size(45, 19);
            // 
            // 
            // 
            this.MarginLeftField.InputBox.AccessibleDescription = resources.GetString("MarginLeftField.InputBox.AccessibleDescription");
            this.MarginLeftField.InputBox.AccessibleName = resources.GetString("MarginLeftField.InputBox.AccessibleName");
            this.MarginLeftField.InputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("MarginLeftField.InputBox.Anchor")));
            this.MarginLeftField.InputBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MarginLeftField.InputBox.BackgroundImage")));
            this.MarginLeftField.InputBox.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("MarginLeftField.InputBox.BackgroundImageLayout")));
            this.MarginLeftField.InputBox.CueText = resources.GetString("MarginLeftField.InputBox.CueText");
            this.MarginLeftField.InputBox.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("MarginLeftField.InputBox.Dock")));
            this.MarginLeftField.InputBox.Font = ((System.Drawing.Font)(resources.GetObject("MarginLeftField.InputBox.Font")));
            this.MarginLeftField.InputBox.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("MarginLeftField.InputBox.ImeMode")));
            this.MarginLeftField.InputBox.Location = ((System.Drawing.Point)(resources.GetObject("MarginLeftField.InputBox.Location")));
            this.MarginLeftField.InputBox.MaxLength = ((int)(resources.GetObject("MarginLeftField.InputBox.MaxLength")));
            this.MarginLeftField.InputBox.Multiline = ((bool)(resources.GetObject("MarginLeftField.InputBox.Multiline")));
            this.MarginLeftField.InputBox.Name = "";
            this.MarginLeftField.InputBox.PasswordChar = ((char)(resources.GetObject("MarginLeftField.InputBox.PasswordChar")));
            this.MarginLeftField.InputBox.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("MarginLeftField.InputBox.RightToLeft")));
            this.MarginLeftField.InputBox.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("MarginLeftField.InputBox.ScrollBars")));
            this.MarginLeftField.InputBox.Size = ((System.Drawing.Size)(resources.GetObject("MarginLeftField.InputBox.Size")));
            this.MarginLeftField.InputBox.TabIndex = ((int)(resources.GetObject("MarginLeftField.InputBox.TabIndex")));
            this.MarginLeftField.InputBox.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("MarginLeftField.InputBox.TextAlign")));
            this.MarginLeftField.InputBox.ToolTipText = resources.GetString("MarginLeftField.InputBox.ToolTipText");
            this.MarginLeftField.InputBox.WordWrap = ((bool)(resources.GetObject("MarginLeftField.InputBox.WordWrap")));
            this.MarginLeftField.InputBox.Leave += new System.EventHandler(this.MarginInputBoxTextChanged);
            // 
            // qCompositeText5
            // 
            this.qCompositeText5.Configuration.ContentAlignmentVertical = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.qCompositeText5.Configuration.Margin = new Qios.DevSuite.Components.QMargin(6, 0, 0, 0);
            this.qCompositeText5.Configuration.StretchVertical = true;
            resources.ApplyResources(this.qCompositeText5, "qCompositeText5");
            // 
            // MarginRightField
            // 
            this.MarginRightField.ControlSize = new System.Drawing.Size(45, 19);
            // 
            // 
            // 
            this.MarginRightField.InputBox.AccessibleDescription = resources.GetString("MarginRightField.InputBox.AccessibleDescription");
            this.MarginRightField.InputBox.AccessibleName = resources.GetString("MarginRightField.InputBox.AccessibleName");
            this.MarginRightField.InputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("MarginRightField.InputBox.Anchor")));
            this.MarginRightField.InputBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MarginRightField.InputBox.BackgroundImage")));
            this.MarginRightField.InputBox.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("MarginRightField.InputBox.BackgroundImageLayout")));
            this.MarginRightField.InputBox.CueText = resources.GetString("MarginRightField.InputBox.CueText");
            this.MarginRightField.InputBox.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("MarginRightField.InputBox.Dock")));
            this.MarginRightField.InputBox.Font = ((System.Drawing.Font)(resources.GetObject("MarginRightField.InputBox.Font")));
            this.MarginRightField.InputBox.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("MarginRightField.InputBox.ImeMode")));
            this.MarginRightField.InputBox.Location = ((System.Drawing.Point)(resources.GetObject("MarginRightField.InputBox.Location")));
            this.MarginRightField.InputBox.MaxLength = ((int)(resources.GetObject("MarginRightField.InputBox.MaxLength")));
            this.MarginRightField.InputBox.Multiline = ((bool)(resources.GetObject("MarginRightField.InputBox.Multiline")));
            this.MarginRightField.InputBox.Name = "";
            this.MarginRightField.InputBox.PasswordChar = ((char)(resources.GetObject("MarginRightField.InputBox.PasswordChar")));
            this.MarginRightField.InputBox.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("MarginRightField.InputBox.RightToLeft")));
            this.MarginRightField.InputBox.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("MarginRightField.InputBox.ScrollBars")));
            this.MarginRightField.InputBox.Size = ((System.Drawing.Size)(resources.GetObject("MarginRightField.InputBox.Size")));
            this.MarginRightField.InputBox.TabIndex = ((int)(resources.GetObject("MarginRightField.InputBox.TabIndex")));
            this.MarginRightField.InputBox.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("MarginRightField.InputBox.TextAlign")));
            this.MarginRightField.InputBox.ToolTipText = resources.GetString("MarginRightField.InputBox.ToolTipText");
            this.MarginRightField.InputBox.WordWrap = ((bool)(resources.GetObject("MarginRightField.InputBox.WordWrap")));
            this.MarginRightField.InputBox.Leave += new System.EventHandler(this.MarginInputBoxTextChanged);
            // 
            // qCompositeItem4
            // 
            this.qCompositeItem4.ColorScheme.CompositeItemHotBackground1.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemHotBackground1.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemHotBackground1.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemHotBackground1.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemHotBackground1.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemHotBackground2.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemHotBackground2.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemHotBackground2.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemHotBackground2.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemHotBackground2.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemHotBorder.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemHotBorder.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemHotBorder.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemHotBorder.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemHotBorder.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemPressedBackground1.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemPressedBackground1.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemPressedBackground2.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemPressedBackground2.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemPressedBorder.SetColor("Default", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemPressedBorder.SetColor("LunaBlue", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemPressedBorder.SetColor("LunaOlive", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemPressedBorder.SetColor("LunaSilver", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.ColorScheme.CompositeItemPressedBorder.SetColor("VistaBlack", System.Drawing.Color.Empty, false);
            this.qCompositeItem4.Configuration.Padding = new Qios.DevSuite.Components.QPadding(26, 1, 1, 1);
            this.qCompositeItem4.Configuration.StretchHorizontal = true;
            resources.ApplyResources(this.qCompositeItem4, "qCompositeItem4");
            this.qCompositeItem4.Items.Add(this.qCompositeText6);
            this.qCompositeItem4.Items.Add(this.MarginBottomField);
            // 
            // qCompositeText6
            // 
            this.qCompositeText6.Configuration.ContentAlignmentVertical = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.qCompositeText6.Configuration.StretchVertical = true;
            resources.ApplyResources(this.qCompositeText6, "qCompositeText6");
            // 
            // MarginBottomField
            // 
            this.MarginBottomField.ControlSize = new System.Drawing.Size(45, 19);
            // 
            // 
            // 
            this.MarginBottomField.InputBox.AccessibleDescription = resources.GetString("MarginBottomField.InputBox.AccessibleDescription");
            this.MarginBottomField.InputBox.AccessibleName = resources.GetString("MarginBottomField.InputBox.AccessibleName");
            this.MarginBottomField.InputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("MarginBottomField.InputBox.Anchor")));
            this.MarginBottomField.InputBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MarginBottomField.InputBox.BackgroundImage")));
            this.MarginBottomField.InputBox.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("MarginBottomField.InputBox.BackgroundImageLayout")));
            this.MarginBottomField.InputBox.CueText = resources.GetString("MarginBottomField.InputBox.CueText");
            this.MarginBottomField.InputBox.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("MarginBottomField.InputBox.Dock")));
            this.MarginBottomField.InputBox.Font = ((System.Drawing.Font)(resources.GetObject("MarginBottomField.InputBox.Font")));
            this.MarginBottomField.InputBox.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("MarginBottomField.InputBox.ImeMode")));
            this.MarginBottomField.InputBox.Location = ((System.Drawing.Point)(resources.GetObject("MarginBottomField.InputBox.Location")));
            this.MarginBottomField.InputBox.MaxLength = ((int)(resources.GetObject("MarginBottomField.InputBox.MaxLength")));
            this.MarginBottomField.InputBox.Multiline = ((bool)(resources.GetObject("MarginBottomField.InputBox.Multiline")));
            this.MarginBottomField.InputBox.Name = "";
            this.MarginBottomField.InputBox.PasswordChar = ((char)(resources.GetObject("MarginBottomField.InputBox.PasswordChar")));
            this.MarginBottomField.InputBox.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("MarginBottomField.InputBox.RightToLeft")));
            this.MarginBottomField.InputBox.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("MarginBottomField.InputBox.ScrollBars")));
            this.MarginBottomField.InputBox.Size = ((System.Drawing.Size)(resources.GetObject("MarginBottomField.InputBox.Size")));
            this.MarginBottomField.InputBox.TabIndex = ((int)(resources.GetObject("MarginBottomField.InputBox.TabIndex")));
            this.MarginBottomField.InputBox.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("MarginBottomField.InputBox.TextAlign")));
            this.MarginBottomField.InputBox.ToolTipText = resources.GetString("MarginBottomField.InputBox.ToolTipText");
            this.MarginBottomField.InputBox.WordWrap = ((bool)(resources.GetObject("MarginBottomField.InputBox.WordWrap")));
            this.MarginBottomField.InputBox.Leave += new System.EventHandler(this.MarginInputBoxTextChanged);
            // 
            // qRibbonPanel1
            // 
            resources.ApplyResources(this.qRibbonPanel1, "qRibbonPanel1");
            this.qRibbonPanel1.Items.Add(this.CloseComposite);
            // 
            // CloseComposite
            // 
            this.CloseComposite.ColorScheme.CompositeItemBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.CloseComposite.ColorScheme.CompositeItemBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.CloseComposite.ColorScheme.CompositeItemBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.CloseComposite.ColorScheme.CompositeItemBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.CloseComposite.ColorScheme.CompositeItemBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.CloseComposite.ColorScheme.CompositeItemBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.CloseComposite.ColorScheme.CompositeItemBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.CloseComposite.ColorScheme.CompositeItemBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.CloseComposite.ColorScheme.CompositeItemBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.CloseComposite.ColorScheme.CompositeItemBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.CloseComposite.ColorScheme.CompositeItemHotBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.CloseComposite.ColorScheme.CompositeItemHotBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.CloseComposite.ColorScheme.CompositeItemHotBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.CloseComposite.ColorScheme.CompositeItemHotBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.CloseComposite.ColorScheme.CompositeItemHotBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.CloseComposite.ColorScheme.CompositeItemHotBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.CloseComposite.ColorScheme.CompositeItemHotBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.CloseComposite.ColorScheme.CompositeItemHotBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.CloseComposite.ColorScheme.CompositeItemHotBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.CloseComposite.ColorScheme.CompositeItemHotBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.CloseComposite.Configuration.AlignmentHorizontal = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.CloseComposite.Configuration.Direction = Qios.DevSuite.Components.QPartDirection.Vertical;
            this.CloseComposite.Configuration.Padding = new Qios.DevSuite.Components.QPadding(0, 0, 0, 0);
            this.CloseComposite.Configuration.ShrinkVertical = true;
            this.CloseComposite.Configuration.StretchHorizontal = true;
            resources.ApplyResources(this.CloseComposite, "CloseComposite");
            this.CloseComposite.Items.Add(this.qCompositeGroup1);
            this.CloseComposite.ItemActivated += new Qios.DevSuite.Components.QCompositeEventHandler(this.CloseCompositeItemActivated);
            // 
            // qCompositeGroup1
            // 
            this.qCompositeGroup1.Configuration.Padding = new Qios.DevSuite.Components.QPadding(4, 4, 4, 4);
            this.qCompositeGroup1.Configuration.ShrinkVertical = true;
            this.qCompositeGroup1.Configuration.StretchHorizontal = true;
            this.qCompositeGroup1.Items.Add(this.qCompositeGroup3);
            // 
            // qCompositeGroup3
            // 
            this.qCompositeGroup3.ColorScheme.CompositeGroupBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeGroupBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeGroupBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeGroupBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeGroupBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeGroupBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeGroupBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeGroupBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeGroupBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeGroupBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeItemHotBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeItemHotBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeItemHotBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeItemHotBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeItemHotBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeItemHotBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeItemHotBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeItemHotBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeItemHotBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeItemPressedBackground1.SetColor("Default", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeItemPressedBackground1.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeItemPressedBackground1.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeItemPressedBackground2.SetColor("Default", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeItemPressedBackground2.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeScrollButtonHotBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeScrollButtonHotBorder.SetColor("Default", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeScrollButtonHotBorder.SetColor("LunaBlue", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeScrollButtonHotBorder.SetColor("LunaOlive", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeScrollButtonHotBorder.SetColor("LunaSilver", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeScrollButtonHotBorder.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.qCompositeGroup3.ColorScheme.CompositeScrollButtonPressedBackground2.SetColor("VistaBlack", System.Drawing.Color.White, false);
            this.qCompositeGroup3.Configuration.AlignmentHorizontal = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.qCompositeGroup3.Configuration.Direction = Qios.DevSuite.Components.QPartDirection.Vertical;
            this.qCompositeGroup3.Configuration.Padding = new Qios.DevSuite.Components.QPadding(4, 4, 4, 4);
            this.qCompositeGroup3.Items.Add(this.qCompositeImage4);
            this.qCompositeGroup3.Items.Add(this.qCompositeText3);
            // 
            // qCompositeImage4
            // 
            this.qCompositeImage4.Configuration.AlignmentHorizontal = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.qCompositeImage4.Configuration.MaximumSize = new System.Drawing.Size(64, 48);
            this.qCompositeImage4.Image = global::WaveletStudio.MainApplication.Properties.Resources.iconClose;
            // 
            // qCompositeText3
            // 
            this.qCompositeText3.Configuration.ContentAlignmentHorizontal = Qios.DevSuite.Components.QPartAlignment.Centered;
            this.qCompositeText3.Configuration.FontDefinition = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.qCompositeText3.Configuration.FontDefinitionExpanded = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.qCompositeText3.Configuration.FontDefinitionHot = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.qCompositeText3.Configuration.FontDefinitionPressed = new Qios.DevSuite.Components.QFontDefinition(null, true, false, false, false, -1F);
            this.qCompositeText3.Configuration.StretchHorizontal = true;
            resources.ApplyResources(this.qCompositeText3, "qCompositeText3");
            // 
            // CenterPanel
            // 
            resources.ApplyResources(this.CenterPanel, "CenterPanel");
            this.CenterPanel.Controls.Add(this.PrintPreviewControl);
            this.CenterPanel.Controls.Add(this.BottomPanel);
            this.CenterPanel.Name = "CenterPanel";
            // 
            // PrintPreviewControl
            // 
            resources.ApplyResources(this.PrintPreviewControl, "PrintPreviewControl");
            this.PrintPreviewControl.Name = "PrintPreviewControl";
            // 
            // BottomPanel
            // 
            resources.ApplyResources(this.BottomPanel, "BottomPanel");
            this.BottomPanel.Controls.Add(this.ZoomLabel);
            this.BottomPanel.Controls.Add(this.ZoomPlusButton);
            this.BottomPanel.Controls.Add(this.ZoomMinusButton);
            this.BottomPanel.Controls.Add(this.ZoomTrackBar);
            this.BottomPanel.Name = "BottomPanel";
            // 
            // ZoomLabel
            // 
            resources.ApplyResources(this.ZoomLabel, "ZoomLabel");
            this.ZoomLabel.Name = "ZoomLabel";
            // 
            // ZoomPlusButton
            // 
            resources.ApplyResources(this.ZoomPlusButton, "ZoomPlusButton");
            this.ZoomPlusButton.Configuration.DrawTextOptions = Qios.DevSuite.Components.QDrawTextOptions.None;
            this.ZoomPlusButton.Configuration.FontDefinition = new Qios.DevSuite.Components.QFontDefinition("Arial", true, false, false, false, 12F);
            this.ZoomPlusButton.Configuration.FontDefinitionFocused = new Qios.DevSuite.Components.QFontDefinition("Arial", true, false, false, false, 12F);
            this.ZoomPlusButton.Configuration.FontDefinitionHot = new Qios.DevSuite.Components.QFontDefinition("Arial", true, false, false, false, 12F);
            this.ZoomPlusButton.Configuration.FontDefinitionPressed = new Qios.DevSuite.Components.QFontDefinition("Arial", true, false, false, false, 12F);
            this.ZoomPlusButton.Configuration.TextConfiguration.Margin = new Qios.DevSuite.Components.QMargin(0, 2, 2, 4);
            this.ZoomPlusButton.Configuration.WrapText = false;
            this.ZoomPlusButton.Image = null;
            this.ZoomPlusButton.Name = "ZoomPlusButton";
            this.ZoomPlusButton.Click += new System.EventHandler(this.ZoomPlusButtonClick);
            // 
            // ZoomMinusButton
            // 
            resources.ApplyResources(this.ZoomMinusButton, "ZoomMinusButton");
            this.ZoomMinusButton.Configuration.DrawTextOptions = Qios.DevSuite.Components.QDrawTextOptions.None;
            this.ZoomMinusButton.Configuration.FontDefinition = new Qios.DevSuite.Components.QFontDefinition("Arial", true, false, false, false, 12F);
            this.ZoomMinusButton.Configuration.FontDefinitionFocused = new Qios.DevSuite.Components.QFontDefinition("Arial", true, false, false, false, 12F);
            this.ZoomMinusButton.Configuration.FontDefinitionHot = new Qios.DevSuite.Components.QFontDefinition("Arial", true, false, false, false, 12F);
            this.ZoomMinusButton.Configuration.FontDefinitionPressed = new Qios.DevSuite.Components.QFontDefinition("Arial", true, false, false, false, 12F);
            this.ZoomMinusButton.Configuration.TextConfiguration.Margin = new Qios.DevSuite.Components.QMargin(0, 2, 2, 4);
            this.ZoomMinusButton.Configuration.WrapText = false;
            this.ZoomMinusButton.Image = null;
            this.ZoomMinusButton.Name = "ZoomMinusButton";
            this.ZoomMinusButton.Click += new System.EventHandler(this.ZoomMinusButtonClick);
            // 
            // ZoomTrackBar
            // 
            resources.ApplyResources(this.ZoomTrackBar, "ZoomTrackBar");
            this.ZoomTrackBar.LargeChange = 2;
            this.ZoomTrackBar.Maximum = 30;
            this.ZoomTrackBar.Minimum = 1;
            this.ZoomTrackBar.Name = "ZoomTrackBar";
            this.ZoomTrackBar.Value = 10;
            this.ZoomTrackBar.ValueChanged += new System.EventHandler(this.ZoomTrackBarValueChanged);
            // 
            // diagramFormProperties1
            // 
            resources.ApplyResources(this.diagramFormProperties1, "diagramFormProperties1");
            this.diagramFormProperties1.CanClose = false;
            this.diagramFormProperties1.CanDockBottom = false;
            this.diagramFormProperties1.CanDockOnOtherControlBottom = false;
            this.diagramFormProperties1.CanDockOnOtherControlTop = false;
            this.diagramFormProperties1.CanDockTop = false;
            this.diagramFormProperties1.DockPosition = Qios.DevSuite.Components.QDockPosition.None;
            this.diagramFormProperties1.Icon = ((System.Drawing.Icon)(resources.GetObject("diagramFormProperties1.Icon")));
            this.diagramFormProperties1.Name = "diagramFormProperties1";
            this.diagramFormProperties1.Owner = null;
            this.diagramFormProperties1.PersistGuid = new System.Guid("1915c329-99d5-4532-a105-ca14e4e34bdb");
            this.diagramFormProperties1.ShowInTaskbarUndocked = true;
            this.diagramFormProperties1.SlidingTime = 200;
            this.diagramFormProperties1.WindowGroupName = "";
            // 
            // PrintPreviewForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CenterPanel);
            this.Controls.Add(this.Ribbon);
            this.Controls.Add(this.Caption);
            this.KeyPreview = true;
            this.Name = "PrintPreviewForm";
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DiagramFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.Caption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ribbon)).EndInit();
            this.Ribbon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PreviewOptionsPage)).EndInit();
            this.CenterPanel.ResumeLayout(false);
            this.BottomPanel.ResumeLayout(false);
            this.BottomPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomTrackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Qios.DevSuite.Components.Ribbon.QRibbonCaption Caption;
        private Qios.DevSuite.Components.Ribbon.QRibbon Ribbon;
        private Qios.DevSuite.Components.Ribbon.QRibbonPage PreviewOptionsPage;
        private Qios.DevSuite.Components.Ribbon.QRibbonPanel PrintPanel;
        private Qios.DevSuite.Components.QCompositeGroup PrintComposite;
        private Qios.DevSuite.Components.Ribbon.QRibbonPanel OptionsPanel;
        private System.Windows.Forms.Panel CenterPanel;        
        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.Label ZoomLabel;
        private Qios.DevSuite.Components.QButton ZoomPlusButton;
        private Qios.DevSuite.Components.QButton ZoomMinusButton;
        private System.Windows.Forms.TrackBar ZoomTrackBar;
        private System.Windows.Forms.PrintPreviewControl PrintPreviewControl;
        private Qios.DevSuite.Components.QCompositeItem PrintCompositeItem;
        private DiagramFormProperties diagramFormProperties1;
        private Qios.DevSuite.Components.QCompositeGroup PrintGroup;
        private Qios.DevSuite.Components.QCompositeGroup PrintInnerGroup;
        private Qios.DevSuite.Components.QCompositeImage qCompositeImage1;
        private Qios.DevSuite.Components.QCompositeText PrintText;
        private QCompositeItem StretchComposite;
        private QCompositeGroup qCompositeGroup4;
        private QCompositeGroup StretchInnerGroup;
        private QCompositeImage qCompositeImage3;
        private QCompositeText qCompositeText2;
        private Qios.DevSuite.Components.Ribbon.QRibbonPanel MarginsPanel;
        private Qios.DevSuite.Components.Ribbon.QRibbonItemGroup qRibbonItemGroup1;
        private QCompositeItem qCompositeItem1;
        private QCompositeItem qCompositeItem2;
        private QCompositeText qCompositeText7;
        private QCompositeItemInputBox MarginTopField;
        private QCompositeItem qCompositeItem3;
        private QCompositeText qCompositeText4;
        private QCompositeItemInputBox MarginLeftField;
        private QCompositeText qCompositeText5;
        private QCompositeItemInputBox MarginRightField;
        private QCompositeItem qCompositeItem4;
        private QCompositeText qCompositeText6;
        private QCompositeItemInputBox MarginBottomField;
        private QCompositeItem SettingsComposite;
        private QCompositeGroup qCompositeGroup6;
        private QCompositeGroup qCompositeGroup7;
        private QCompositeImage qCompositeImage5;
        private QCompositeText qCompositeText8;
        private QCompositeItem GridComposite;
        private QCompositeGroup qCompositeGroup2;
        private QCompositeGroup GridInnerGroup;
        private QCompositeImage qCompositeImage2;
        private QCompositeText qCompositeText1;
        private Qios.DevSuite.Components.Ribbon.QRibbonPanel qRibbonPanel1;
        private QCompositeItem CloseComposite;
        private QCompositeGroup qCompositeGroup1;
        private QCompositeGroup qCompositeGroup3;
        private QCompositeImage qCompositeImage4;
        private QCompositeText qCompositeText3;              
    }
}