using Qios.DevSuite.Components.Ribbon;

namespace WaveletStudio.Designer.Forms
{
    partial class DiagramFormMainMenu : QRibbonMenuWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiagramFormMainMenu));
            this.OptionsButton = new Qios.DevSuite.Components.QCompositeButton();
            this.CloseButton = new Qios.DevSuite.Components.QCompositeButton();
            this.NewMenuItem = new Qios.DevSuite.Components.QCompositeMenuItem();
            this.OpenMenuItem = new Qios.DevSuite.Components.QCompositeMenuItem();
            this.SaveMenuItem = new Qios.DevSuite.Components.QCompositeMenuItem();
            this.SaveAsMenuItem = new Qios.DevSuite.Components.QCompositeMenuItem();
            this.qrsmwMainMenuSaveAs = new Qios.DevSuite.Components.Ribbon.QRibbonSubMenuWindow();
            this.qcliSaveAsWord = new Qios.DevSuite.Components.QCompositeLargeMenuItem();
            this.qcliSaveAsWordTemplate = new Qios.DevSuite.Components.QCompositeLargeMenuItem();
            this.qcliSaveAsWord2003 = new Qios.DevSuite.Components.QCompositeLargeMenuItem();
            this.qcliSaveAsPdf = new Qios.DevSuite.Components.QCompositeLargeMenuItem();
            this.qcliSaveAsOtherDocuments = new Qios.DevSuite.Components.QCompositeLargeMenuItem();
            this.qciSaveAsOther1 = new Qios.DevSuite.Components.QCompositeMenuItem();
            this.qciSaveAsOther2 = new Qios.DevSuite.Components.QCompositeMenuItem();
            this.qCompositeSeparator1 = new Qios.DevSuite.Components.QCompositeSeparator();
            this.qCompositeSeparator2 = new Qios.DevSuite.Components.QCompositeSeparator();
            this.PrintMenuItem = new Qios.DevSuite.Components.QCompositeMenuItem();
            this.PrepateMenuItem = new Qios.DevSuite.Components.QCompositeMenuItem();
            this.PrintPreviewMenuItem = new Qios.DevSuite.Components.QCompositeMenuItem();
            this.OptionsMenuItem = new Qios.DevSuite.Components.QCompositeMenuItem();
            this.Separator1 = new Qios.DevSuite.Components.QCompositeSeparator();
            this.CloseMenuItem = new Qios.DevSuite.Components.QCompositeMenuItem();
            this.qcitRecentDocument = new Qios.DevSuite.Components.QCompositeItemTemplate();
            this.qctDocumentNumber = new Qios.DevSuite.Components.QCompositeText();
            this.qctDocumentName = new Qios.DevSuite.Components.QCompositeText();
            this.qciPin = new Qios.DevSuite.Components.QCompositeImage();
            this.qCompositeText1 = new Qios.DevSuite.Components.QCompositeText();
            this.qCompositeText2 = new Qios.DevSuite.Components.QCompositeText();
            this.qCompositeImage1 = new Qios.DevSuite.Components.QCompositeImage();
            this.qCompositeText3 = new Qios.DevSuite.Components.QCompositeText();
            this.qCompositeText4 = new Qios.DevSuite.Components.QCompositeText();
            this.qCompositeImage2 = new Qios.DevSuite.Components.QCompositeImage();
            this.qCompositeText5 = new Qios.DevSuite.Components.QCompositeText();
            this.qCompositeText6 = new Qios.DevSuite.Components.QCompositeText();
            this.qCompositeImage3 = new Qios.DevSuite.Components.QCompositeImage();
            this.qCompositeText7 = new Qios.DevSuite.Components.QCompositeText();
            this.qCompositeText8 = new Qios.DevSuite.Components.QCompositeText();
            this.qCompositeImage4 = new Qios.DevSuite.Components.QCompositeImage();
            this.qCompositeText9 = new Qios.DevSuite.Components.QCompositeText();
            this.qCompositeText10 = new Qios.DevSuite.Components.QCompositeText();
            this.qCompositeImage5 = new Qios.DevSuite.Components.QCompositeImage();
            this.qCompositeText11 = new Qios.DevSuite.Components.QCompositeText();
            this.qCompositeText12 = new Qios.DevSuite.Components.QCompositeText();
            this.qCompositeImage6 = new Qios.DevSuite.Components.QCompositeImage();
            this.qCompositeText13 = new Qios.DevSuite.Components.QCompositeText();
            this.qCompositeText14 = new Qios.DevSuite.Components.QCompositeText();
            this.qCompositeImage7 = new Qios.DevSuite.Components.QCompositeImage();
            this.qCompositeText15 = new Qios.DevSuite.Components.QCompositeText();
            this.qCompositeText16 = new Qios.DevSuite.Components.QCompositeText();
            this.qCompositeImage8 = new Qios.DevSuite.Components.QCompositeImage();
            this.ExportMenuItem = new Qios.DevSuite.Components.QCompositeMenuItem();
            this.Separator2 = new Qios.DevSuite.Components.QCompositeSeparator();
            this.GenerateCodeMenuItem = new Qios.DevSuite.Components.QCompositeMenuItem();
            this.SuspendLayout();
            // 
            // OptionsButton
            // 
            this.OptionsButton.Configuration.AlignmentHorizontal = Qios.DevSuite.Components.QPartAlignment.Far;
            resources.ApplyResources(this.OptionsButton, "OptionsButton");
            this.OptionsButton.Visible = false;
            // 
            // CloseButton
            // 
            this.CloseButton.Configuration.AlignmentHorizontal = Qios.DevSuite.Components.QPartAlignment.Far;
            resources.ApplyResources(this.CloseButton, "CloseButton");
            this.CloseButton.Visible = false;
            // 
            // NewMenuItem
            // 
            this.NewMenuItem.Configuration.IconConfiguration.IconSize = new System.Drawing.Size(32, 32);
            this.NewMenuItem.Configuration.Padding = new Qios.DevSuite.Components.QPadding(5, 1, 1, 1);
            resources.ApplyResources(this.NewMenuItem, "NewMenuItem");
            this.NewMenuItem.Icon = ((System.Drawing.Icon)(resources.GetObject("NewMenuItem.Icon")));
            this.NewMenuItem.SuppressShortcutToSystem = false;
            this.NewMenuItem.ItemActivated += new Qios.DevSuite.Components.QCompositeEventHandler(this.NewMenuItemItemActivated);
            // 
            // OpenMenuItem
            // 
            this.OpenMenuItem.Configuration.IconConfiguration.IconSize = new System.Drawing.Size(32, 32);
            this.OpenMenuItem.Configuration.Padding = new Qios.DevSuite.Components.QPadding(5, 1, 1, 1);
            resources.ApplyResources(this.OpenMenuItem, "OpenMenuItem");
            this.OpenMenuItem.Icon = ((System.Drawing.Icon)(resources.GetObject("OpenMenuItem.Icon")));
            this.OpenMenuItem.ItemActivated += new Qios.DevSuite.Components.QCompositeEventHandler(this.OpenMenuItemItemActivated);
            // 
            // SaveMenuItem
            // 
            this.SaveMenuItem.Configuration.IconConfiguration.IconSize = new System.Drawing.Size(32, 32);
            this.SaveMenuItem.Configuration.Padding = new Qios.DevSuite.Components.QPadding(5, 1, 1, 1);
            resources.ApplyResources(this.SaveMenuItem, "SaveMenuItem");
            this.SaveMenuItem.Icon = ((System.Drawing.Icon)(resources.GetObject("SaveMenuItem.Icon")));
            this.SaveMenuItem.ItemActivated += new Qios.DevSuite.Components.QCompositeEventHandler(this.SaveMenuItemItemActivated);
            // 
            // SaveAsMenuItem
            // 
            this.SaveAsMenuItem.Configuration.DropDownSeparated = true;
            this.SaveAsMenuItem.Configuration.IconConfiguration.IconSize = new System.Drawing.Size(32, 32);
            this.SaveAsMenuItem.Configuration.Padding = new Qios.DevSuite.Components.QPadding(5, 1, 1, 1);
            resources.ApplyResources(this.SaveAsMenuItem, "SaveAsMenuItem");
            this.SaveAsMenuItem.Icon = ((System.Drawing.Icon)(resources.GetObject("SaveAsMenuItem.Icon")));
            this.SaveAsMenuItem.ItemActivated += new Qios.DevSuite.Components.QCompositeEventHandler(this.SaveAsMenuItemItemActivated);
            // 
            // qrsmwMainMenuSaveAs
            // 
            resources.ApplyResources(this.qrsmwMainMenuSaveAs, "qrsmwMainMenuSaveAs");
            this.qrsmwMainMenuSaveAs.Items.Add(this.qcliSaveAsWord);
            this.qrsmwMainMenuSaveAs.Items.Add(this.qcliSaveAsWordTemplate);
            this.qrsmwMainMenuSaveAs.Items.Add(this.qcliSaveAsWord2003);
            this.qrsmwMainMenuSaveAs.Items.Add(this.qcliSaveAsPdf);
            this.qrsmwMainMenuSaveAs.Items.Add(this.qcliSaveAsOtherDocuments);
            this.qrsmwMainMenuSaveAs.Name = "qrsmwMainMenuSaveAs";
            // 
            // qcliSaveAsWord
            // 
            resources.ApplyResources(this.qcliSaveAsWord, "qcliSaveAsWord");
            this.qcliSaveAsWord.Icon = ((System.Drawing.Icon)(resources.GetObject("qcliSaveAsWord.Icon")));
            // 
            // qcliSaveAsWordTemplate
            // 
            resources.ApplyResources(this.qcliSaveAsWordTemplate, "qcliSaveAsWordTemplate");
            this.qcliSaveAsWordTemplate.Icon = ((System.Drawing.Icon)(resources.GetObject("qcliSaveAsWordTemplate.Icon")));
            // 
            // qcliSaveAsWord2003
            // 
            resources.ApplyResources(this.qcliSaveAsWord2003, "qcliSaveAsWord2003");
            this.qcliSaveAsWord2003.Icon = ((System.Drawing.Icon)(resources.GetObject("qcliSaveAsWord2003.Icon")));
            // 
            // qcliSaveAsPdf
            // 
            resources.ApplyResources(this.qcliSaveAsPdf, "qcliSaveAsPdf");
            this.qcliSaveAsPdf.Icon = ((System.Drawing.Icon)(resources.GetObject("qcliSaveAsPdf.Icon")));
            // 
            // qcliSaveAsOtherDocuments
            // 
            this.qcliSaveAsOtherDocuments.ChildItems.Add(this.qciSaveAsOther1);
            this.qcliSaveAsOtherDocuments.ChildItems.Add(this.qciSaveAsOther2);
            resources.ApplyResources(this.qcliSaveAsOtherDocuments, "qcliSaveAsOtherDocuments");
            this.qcliSaveAsOtherDocuments.Icon = ((System.Drawing.Icon)(resources.GetObject("qcliSaveAsOtherDocuments.Icon")));
            // 
            // qciSaveAsOther1
            // 
            resources.ApplyResources(this.qciSaveAsOther1, "qciSaveAsOther1");
            // 
            // qciSaveAsOther2
            // 
            resources.ApplyResources(this.qciSaveAsOther2, "qciSaveAsOther2");
            // 
            // PrintMenuItem
            // 
            this.PrintMenuItem.Configuration.IconConfiguration.IconSize = new System.Drawing.Size(32, 32);
            this.PrintMenuItem.Configuration.Padding = new Qios.DevSuite.Components.QPadding(5, 1, 1, 1);
            resources.ApplyResources(this.PrintMenuItem, "PrintMenuItem");
            this.PrintMenuItem.Icon = ((System.Drawing.Icon)(resources.GetObject("PrintMenuItem.Icon")));
            this.PrintMenuItem.ItemActivated += new Qios.DevSuite.Components.QCompositeEventHandler(this.PrintMenuItemItemActivated);
            // 
            // PrepateMenuItem
            // 
            this.PrepateMenuItem.Configuration.IconConfiguration.IconSize = new System.Drawing.Size(32, 32);
            this.PrepateMenuItem.Configuration.Padding = new Qios.DevSuite.Components.QPadding(5, 1, 1, 1);
            resources.ApplyResources(this.PrepateMenuItem, "PrepateMenuItem");
            this.PrepateMenuItem.Icon = ((System.Drawing.Icon)(resources.GetObject("PrepateMenuItem.Icon")));
            this.PrepateMenuItem.Visible = false;
            // 
            // PrintPreviewMenuItem
            // 
            this.PrintPreviewMenuItem.Configuration.IconConfiguration.IconSize = new System.Drawing.Size(32, 32);
            this.PrintPreviewMenuItem.Configuration.Padding = new Qios.DevSuite.Components.QPadding(5, 1, 1, 1);
            resources.ApplyResources(this.PrintPreviewMenuItem, "PrintPreviewMenuItem");
            this.PrintPreviewMenuItem.Icon = ((System.Drawing.Icon)(resources.GetObject("PrintPreviewMenuItem.Icon")));
            this.PrintPreviewMenuItem.ItemActivated += new Qios.DevSuite.Components.QCompositeEventHandler(this.PrintPreviewMenuItemItemActivated);
            // 
            // OptionsMenuItem
            // 
            this.OptionsMenuItem.Configuration.IconConfiguration.IconSize = new System.Drawing.Size(32, 32);
            this.OptionsMenuItem.Configuration.Padding = new Qios.DevSuite.Components.QPadding(5, 1, 1, 1);
            resources.ApplyResources(this.OptionsMenuItem, "OptionsMenuItem");
            this.OptionsMenuItem.Icon = ((System.Drawing.Icon)(resources.GetObject("OptionsMenuItem.Icon")));
            this.OptionsMenuItem.ItemActivated += new Qios.DevSuite.Components.QCompositeEventHandler(this.OptionsMenuItemItemActivated);
            // 
            // CloseMenuItem
            // 
            this.CloseMenuItem.Configuration.IconConfiguration.IconSize = new System.Drawing.Size(32, 32);
            this.CloseMenuItem.Configuration.Padding = new Qios.DevSuite.Components.QPadding(5, 1, 1, 1);
            resources.ApplyResources(this.CloseMenuItem, "CloseMenuItem");
            this.CloseMenuItem.Icon = ((System.Drawing.Icon)(resources.GetObject("CloseMenuItem.Icon")));
            this.CloseMenuItem.ItemActivated += new Qios.DevSuite.Components.QCompositeEventHandler(this.CloseMenuItemItemActivated);
            // 
            // qcitRecentDocument
            // 
            this.qcitRecentDocument.Configuration.ShrinkHorizontal = true;
            this.qcitRecentDocument.Configuration.StretchHorizontal = true;
            resources.ApplyResources(this.qcitRecentDocument, "qcitRecentDocument");
            this.qcitRecentDocument.Items.Add(this.qctDocumentNumber);
            this.qcitRecentDocument.Items.Add(this.qctDocumentName);
            this.qcitRecentDocument.Items.Add(this.qciPin);
            // 
            // qctDocumentNumber
            // 
            this.qctDocumentNumber.Configuration.Padding = new Qios.DevSuite.Components.QPadding(5, 0, 0, 5);
            resources.ApplyResources(this.qctDocumentNumber, "qctDocumentNumber");
            // 
            // qctDocumentName
            // 
            this.qctDocumentName.Configuration.DrawTextOptions = Qios.DevSuite.Components.QDrawTextOptions.EndEllipsis;
            this.qctDocumentName.Configuration.ShrinkHorizontal = true;
            this.qctDocumentName.Configuration.StretchHorizontal = true;
            resources.ApplyResources(this.qctDocumentName, "qctDocumentName");
            // 
            // qciPin
            // 
            this.qciPin.Image = ((System.Drawing.Image)(resources.GetObject("qciPin.Image")));
            // 
            // qCompositeText1
            // 
            this.qCompositeText1.Configuration.Padding = new Qios.DevSuite.Components.QPadding(5, 0, 0, 5);
            resources.ApplyResources(this.qCompositeText1, "qCompositeText1");
            // 
            // qCompositeText2
            // 
            this.qCompositeText2.Configuration.StretchHorizontal = true;
            resources.ApplyResources(this.qCompositeText2, "qCompositeText2");
            // 
            // qCompositeImage1
            // 
            this.qCompositeImage1.Image = ((System.Drawing.Image)(resources.GetObject("qCompositeImage1.Image")));
            // 
            // qCompositeText3
            // 
            this.qCompositeText3.Configuration.Padding = new Qios.DevSuite.Components.QPadding(5, 0, 0, 5);
            resources.ApplyResources(this.qCompositeText3, "qCompositeText3");
            // 
            // qCompositeText4
            // 
            this.qCompositeText4.Configuration.StretchHorizontal = true;
            resources.ApplyResources(this.qCompositeText4, "qCompositeText4");
            // 
            // qCompositeImage2
            // 
            this.qCompositeImage2.Image = ((System.Drawing.Image)(resources.GetObject("qCompositeImage2.Image")));
            // 
            // qCompositeText5
            // 
            this.qCompositeText5.Configuration.Padding = new Qios.DevSuite.Components.QPadding(5, 0, 0, 5);
            resources.ApplyResources(this.qCompositeText5, "qCompositeText5");
            // 
            // qCompositeText6
            // 
            this.qCompositeText6.Configuration.StretchHorizontal = true;
            resources.ApplyResources(this.qCompositeText6, "qCompositeText6");
            // 
            // qCompositeImage3
            // 
            this.qCompositeImage3.Image = ((System.Drawing.Image)(resources.GetObject("qCompositeImage3.Image")));
            // 
            // qCompositeText7
            // 
            this.qCompositeText7.Configuration.Padding = new Qios.DevSuite.Components.QPadding(5, 0, 0, 5);
            resources.ApplyResources(this.qCompositeText7, "qCompositeText7");
            // 
            // qCompositeText8
            // 
            this.qCompositeText8.Configuration.StretchHorizontal = true;
            resources.ApplyResources(this.qCompositeText8, "qCompositeText8");
            // 
            // qCompositeImage4
            // 
            this.qCompositeImage4.Image = ((System.Drawing.Image)(resources.GetObject("qCompositeImage4.Image")));
            // 
            // qCompositeText9
            // 
            this.qCompositeText9.Configuration.Padding = new Qios.DevSuite.Components.QPadding(5, 0, 0, 5);
            resources.ApplyResources(this.qCompositeText9, "qCompositeText9");
            // 
            // qCompositeText10
            // 
            this.qCompositeText10.Configuration.StretchHorizontal = true;
            resources.ApplyResources(this.qCompositeText10, "qCompositeText10");
            // 
            // qCompositeImage5
            // 
            this.qCompositeImage5.Image = ((System.Drawing.Image)(resources.GetObject("qCompositeImage5.Image")));
            // 
            // qCompositeText11
            // 
            this.qCompositeText11.Configuration.Padding = new Qios.DevSuite.Components.QPadding(5, 0, 0, 5);
            resources.ApplyResources(this.qCompositeText11, "qCompositeText11");
            // 
            // qCompositeText12
            // 
            this.qCompositeText12.Configuration.StretchHorizontal = true;
            resources.ApplyResources(this.qCompositeText12, "qCompositeText12");
            // 
            // qCompositeImage6
            // 
            this.qCompositeImage6.Image = ((System.Drawing.Image)(resources.GetObject("qCompositeImage6.Image")));
            // 
            // qCompositeText13
            // 
            this.qCompositeText13.Configuration.Padding = new Qios.DevSuite.Components.QPadding(5, 0, 0, 5);
            resources.ApplyResources(this.qCompositeText13, "qCompositeText13");
            // 
            // qCompositeText14
            // 
            this.qCompositeText14.Configuration.StretchHorizontal = true;
            resources.ApplyResources(this.qCompositeText14, "qCompositeText14");
            // 
            // qCompositeImage7
            // 
            this.qCompositeImage7.Image = ((System.Drawing.Image)(resources.GetObject("qCompositeImage7.Image")));
            // 
            // qCompositeText15
            // 
            this.qCompositeText15.Configuration.Padding = new Qios.DevSuite.Components.QPadding(5, 0, 0, 5);
            resources.ApplyResources(this.qCompositeText15, "qCompositeText15");
            // 
            // qCompositeText16
            // 
            this.qCompositeText16.Configuration.DrawTextOptions = Qios.DevSuite.Components.QDrawTextOptions.EndEllipsis;
            this.qCompositeText16.Configuration.ShrinkHorizontal = true;
            this.qCompositeText16.Configuration.StretchHorizontal = true;
            resources.ApplyResources(this.qCompositeText16, "qCompositeText16");
            // 
            // qCompositeImage8
            // 
            this.qCompositeImage8.Image = ((System.Drawing.Image)(resources.GetObject("qCompositeImage8.Image")));
            // 
            // ExportMenuItem
            // 
            this.ExportMenuItem.Configuration.DropDownSeparated = true;
            this.ExportMenuItem.Configuration.IconConfiguration.IconSize = new System.Drawing.Size(32, 32);
            this.ExportMenuItem.Configuration.Padding = new Qios.DevSuite.Components.QPadding(5, 1, 1, 1);
            resources.ApplyResources(this.ExportMenuItem, "ExportMenuItem");
            this.ExportMenuItem.Icon = ((System.Drawing.Icon)(resources.GetObject("ExportMenuItem.Icon")));
            this.ExportMenuItem.ItemActivated += new Qios.DevSuite.Components.QCompositeEventHandler(this.ExportMenuItemItemActivated);
            // 
            // GenerateCodeMenuItem
            // 
            this.GenerateCodeMenuItem.Configuration.IconConfiguration.IconSize = new System.Drawing.Size(32, 32);
            resources.ApplyResources(this.GenerateCodeMenuItem, "GenerateCodeMenuItem");
            this.GenerateCodeMenuItem.Icon = ((System.Drawing.Icon)(resources.GetObject("GenerateCodeMenuItem.Icon")));
            this.GenerateCodeMenuItem.ItemActivated += new Qios.DevSuite.Components.QCompositeEventHandler(this.GenerateCodeMenuItemItemActivated);
            // 
            // DiagramFormMainMenu
            // 
            resources.ApplyResources(this, "$this");
            this.FooterItems.Add(this.OptionsButton);
            this.FooterItems.Add(this.CloseButton);
            this.Items.Add(this.NewMenuItem);
            this.Items.Add(this.OpenMenuItem);
            this.Items.Add(this.SaveMenuItem);
            this.Items.Add(this.SaveAsMenuItem);
            this.Items.Add(this.qCompositeSeparator2);
            this.Items.Add(this.ExportMenuItem);
            this.Items.Add(this.GenerateCodeMenuItem);
            this.Items.Add(this.qCompositeSeparator1);
            this.Items.Add(this.PrintMenuItem);
            this.Items.Add(this.PrintPreviewMenuItem);
            this.Items.Add(this.Separator2);
            this.Items.Add(this.PrepateMenuItem);
            this.Items.Add(this.OptionsMenuItem);
            this.Items.Add(this.Separator1);
            this.Items.Add(this.CloseMenuItem);
            this.Name = "DiagramFormMainMenu";
            this.VisibleChanged += new System.EventHandler(this.DiagramFormMainMenuVisibleChanged);
            this.ResumeLayout(false);

        }
        #endregion 

        #endregion

        private Qios.DevSuite.Components.QCompositeButton OptionsButton;
        private Qios.DevSuite.Components.QCompositeButton CloseButton;
        private Qios.DevSuite.Components.QCompositeMenuItem NewMenuItem;
        private Qios.DevSuite.Components.QCompositeMenuItem OpenMenuItem;
        private Qios.DevSuite.Components.QCompositeMenuItem SaveMenuItem;
        private Qios.DevSuite.Components.QCompositeMenuItem SaveAsMenuItem;
        private Qios.DevSuite.Components.QCompositeSeparator qCompositeSeparator1;
        private Qios.DevSuite.Components.QCompositeSeparator qCompositeSeparator2;
        private Qios.DevSuite.Components.QCompositeMenuItem PrintMenuItem;
        private Qios.DevSuite.Components.QCompositeMenuItem PrepateMenuItem;
        private Qios.DevSuite.Components.QCompositeMenuItem PrintPreviewMenuItem;
        private Qios.DevSuite.Components.QCompositeMenuItem OptionsMenuItem;
        private Qios.DevSuite.Components.QCompositeSeparator Separator1;
        private Qios.DevSuite.Components.QCompositeMenuItem CloseMenuItem;
        private Qios.DevSuite.Components.Ribbon.QRibbonSubMenuWindow qrsmwMainMenuSaveAs;
        private Qios.DevSuite.Components.QCompositeLargeMenuItem qcliSaveAsWord;
        private Qios.DevSuite.Components.QCompositeLargeMenuItem qcliSaveAsWordTemplate;
        private Qios.DevSuite.Components.QCompositeLargeMenuItem qcliSaveAsWord2003;
        private Qios.DevSuite.Components.QCompositeLargeMenuItem qcliSaveAsPdf;
        private Qios.DevSuite.Components.QCompositeLargeMenuItem qcliSaveAsOtherDocuments;
        private Qios.DevSuite.Components.QCompositeMenuItem qciSaveAsOther1;
        private Qios.DevSuite.Components.QCompositeMenuItem qciSaveAsOther2;
        private Qios.DevSuite.Components.QCompositeItemTemplate qcitRecentDocument;
        private Qios.DevSuite.Components.QCompositeText qctDocumentNumber;
        private Qios.DevSuite.Components.QCompositeText qctDocumentName;
        private Qios.DevSuite.Components.QCompositeImage qciPin;
        private Qios.DevSuite.Components.QCompositeText qCompositeText1;
        private Qios.DevSuite.Components.QCompositeText qCompositeText2;
        private Qios.DevSuite.Components.QCompositeImage qCompositeImage1;
        private Qios.DevSuite.Components.QCompositeText qCompositeText3;
        private Qios.DevSuite.Components.QCompositeText qCompositeText4;
        private Qios.DevSuite.Components.QCompositeImage qCompositeImage2;
        private Qios.DevSuite.Components.QCompositeText qCompositeText5;
        private Qios.DevSuite.Components.QCompositeText qCompositeText6;
        private Qios.DevSuite.Components.QCompositeImage qCompositeImage3;
        private Qios.DevSuite.Components.QCompositeText qCompositeText7;
        private Qios.DevSuite.Components.QCompositeText qCompositeText8;
        private Qios.DevSuite.Components.QCompositeImage qCompositeImage4;
        private Qios.DevSuite.Components.QCompositeText qCompositeText9;
        private Qios.DevSuite.Components.QCompositeText qCompositeText10;
        private Qios.DevSuite.Components.QCompositeImage qCompositeImage5;
        private Qios.DevSuite.Components.QCompositeText qCompositeText11;
        private Qios.DevSuite.Components.QCompositeText qCompositeText12;
        private Qios.DevSuite.Components.QCompositeImage qCompositeImage6;
        private Qios.DevSuite.Components.QCompositeText qCompositeText13;
        private Qios.DevSuite.Components.QCompositeText qCompositeText14;
        private Qios.DevSuite.Components.QCompositeImage qCompositeImage7;
        private Qios.DevSuite.Components.QCompositeText qCompositeText15;
        private Qios.DevSuite.Components.QCompositeText qCompositeText16;
        private Qios.DevSuite.Components.QCompositeImage qCompositeImage8;
        private Qios.DevSuite.Components.QCompositeMenuItem ExportMenuItem;
        private Qios.DevSuite.Components.QCompositeSeparator Separator2;
        private Qios.DevSuite.Components.QCompositeMenuItem GenerateCodeMenuItem;
                
    }
}
