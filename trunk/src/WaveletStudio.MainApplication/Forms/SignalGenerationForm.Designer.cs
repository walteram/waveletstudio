namespace WaveletStudio.MainApplication.Forms
{
    partial class SignalGenerationForm
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
            this.qRibbonCaption2 = new Qios.DevSuite.Components.Ribbon.QRibbonCaption();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TemplateField = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AmplitudeField = new System.Windows.Forms.NumericUpDown();
            this.StartField = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.FinishField = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.FrequencyField = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.PhaseField = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.OffsetField = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.SamplingRateField = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.IgnoreLastSampleField = new System.Windows.Forms.CheckBox();
            this.GraphControl = new ZedGraph.ZedGraphControl();
            this.UseSignalButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.qRibbonCaption2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmplitudeField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinishField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrequencyField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhaseField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SamplingRateField)).BeginInit();
            this.SuspendLayout();
            // 
            // qRibbonCaption2
            // 
            this.qRibbonCaption2.Configuration.ApplicationButtonAreaConfiguration.Margin = new Qios.DevSuite.Components.QMargin(0, 4, 0, -8);
            this.qRibbonCaption2.Configuration.IconConfiguration.Visible = Qios.DevSuite.Components.QTristateBool.False;
            this.qRibbonCaption2.Location = new System.Drawing.Point(0, 0);
            this.qRibbonCaption2.Name = "qRibbonCaption2";
            this.qRibbonCaption2.Size = new System.Drawing.Size(787, 28);
            this.qRibbonCaption2.TabIndex = 25;
            this.qRibbonCaption2.Text = "Signal Form Generation";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 26);
            this.label1.TabIndex = 26;
            this.label1.Text = "Use this tool to create a new signal based on a template.\r\nSelect and configure t" +
                "he template using the settings bellow.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Template:";
            // 
            // TemplateField
            // 
            this.TemplateField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TemplateField.FormattingEnabled = true;
            this.TemplateField.Location = new System.Drawing.Point(115, 81);
            this.TemplateField.Name = "TemplateField";
            this.TemplateField.Size = new System.Drawing.Size(138, 21);
            this.TemplateField.TabIndex = 28;
            this.TemplateField.SelectedIndexChanged += new System.EventHandler(this.TemplateFieldSelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Amplitude:";
            // 
            // AmplitudeField
            // 
            this.AmplitudeField.DecimalPlaces = 3;
            this.AmplitudeField.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.AmplitudeField.Location = new System.Drawing.Point(115, 108);
            this.AmplitudeField.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.AmplitudeField.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.AmplitudeField.Name = "AmplitudeField";
            this.AmplitudeField.Size = new System.Drawing.Size(138, 20);
            this.AmplitudeField.TabIndex = 30;
            this.AmplitudeField.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.AmplitudeField.ValueChanged += new System.EventHandler(this.FieldValueChanged);
            this.AmplitudeField.Scroll += new System.Windows.Forms.ScrollEventHandler(this.FieldValueChanged);
            // 
            // StartField
            // 
            this.StartField.DecimalPlaces = 3;
            this.StartField.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.StartField.Location = new System.Drawing.Point(115, 212);
            this.StartField.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.StartField.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.StartField.Name = "StartField";
            this.StartField.Size = new System.Drawing.Size(138, 20);
            this.StartField.TabIndex = 32;
            this.StartField.ValueChanged += new System.EventHandler(this.FieldValueChanged);
            this.StartField.Scroll += new System.Windows.Forms.ScrollEventHandler(this.FieldValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Start Time:";
            // 
            // FinishField
            // 
            this.FinishField.DecimalPlaces = 3;
            this.FinishField.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.FinishField.Location = new System.Drawing.Point(115, 238);
            this.FinishField.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.FinishField.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.FinishField.Name = "FinishField";
            this.FinishField.Size = new System.Drawing.Size(138, 20);
            this.FinishField.TabIndex = 34;
            this.FinishField.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.FinishField.ValueChanged += new System.EventHandler(this.FieldValueChanged);
            this.FinishField.Scroll += new System.Windows.Forms.ScrollEventHandler(this.FieldValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 240);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "Finish Time:";
            // 
            // FrequencyField
            // 
            this.FrequencyField.Location = new System.Drawing.Point(115, 134);
            this.FrequencyField.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.FrequencyField.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.FrequencyField.Name = "FrequencyField";
            this.FrequencyField.Size = new System.Drawing.Size(138, 20);
            this.FrequencyField.TabIndex = 36;
            this.FrequencyField.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.FrequencyField.ValueChanged += new System.EventHandler(this.FieldValueChanged);
            this.FrequencyField.Scroll += new System.Windows.Forms.ScrollEventHandler(this.FieldValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Frequency (Hz):";
            // 
            // PhaseField
            // 
            this.PhaseField.DecimalPlaces = 3;
            this.PhaseField.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.PhaseField.Location = new System.Drawing.Point(115, 160);
            this.PhaseField.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PhaseField.Name = "PhaseField";
            this.PhaseField.Size = new System.Drawing.Size(138, 20);
            this.PhaseField.TabIndex = 38;
            this.PhaseField.ValueChanged += new System.EventHandler(this.FieldValueChanged);
            this.PhaseField.Scroll += new System.Windows.Forms.ScrollEventHandler(this.FieldValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 37;
            this.label7.Text = "Phase:";
            // 
            // OffsetField
            // 
            this.OffsetField.DecimalPlaces = 3;
            this.OffsetField.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.OffsetField.Location = new System.Drawing.Point(115, 186);
            this.OffsetField.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.OffsetField.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.OffsetField.Name = "OffsetField";
            this.OffsetField.Size = new System.Drawing.Size(138, 20);
            this.OffsetField.TabIndex = 40;
            this.OffsetField.ValueChanged += new System.EventHandler(this.FieldValueChanged);
            this.OffsetField.Scroll += new System.Windows.Forms.ScrollEventHandler(this.FieldValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 188);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "Offset:";
            // 
            // SamplingRateField
            // 
            this.SamplingRateField.Location = new System.Drawing.Point(115, 264);
            this.SamplingRateField.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.SamplingRateField.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.SamplingRateField.Name = "SamplingRateField";
            this.SamplingRateField.Size = new System.Drawing.Size(138, 20);
            this.SamplingRateField.TabIndex = 42;
            this.SamplingRateField.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.SamplingRateField.ValueChanged += new System.EventHandler(this.FieldValueChanged);
            this.SamplingRateField.Scroll += new System.Windows.Forms.ScrollEventHandler(this.FieldValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 266);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 13);
            this.label9.TabIndex = 41;
            this.label9.Text = "Sampling Rate (Hz):";
            // 
            // IgnoreLastSampleField
            // 
            this.IgnoreLastSampleField.AutoSize = true;
            this.IgnoreLastSampleField.Location = new System.Drawing.Point(13, 295);
            this.IgnoreLastSampleField.Name = "IgnoreLastSampleField";
            this.IgnoreLastSampleField.Size = new System.Drawing.Size(129, 17);
            this.IgnoreLastSampleField.TabIndex = 43;
            this.IgnoreLastSampleField.Text = "Ignore the last sample";
            this.IgnoreLastSampleField.UseVisualStyleBackColor = true;
            this.IgnoreLastSampleField.CheckedChanged += new System.EventHandler(this.FieldValueChanged);
            // 
            // GraphControl
            // 
            this.GraphControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GraphControl.AutoSize = true;
            this.GraphControl.IsAntiAlias = true;
            this.GraphControl.Location = new System.Drawing.Point(305, 41);
            this.GraphControl.Name = "GraphControl";
            this.GraphControl.ScrollGrace = 0D;
            this.GraphControl.ScrollMaxX = 0D;
            this.GraphControl.ScrollMaxY = 0D;
            this.GraphControl.ScrollMaxY2 = 0D;
            this.GraphControl.ScrollMinX = 0D;
            this.GraphControl.ScrollMinY = 0D;
            this.GraphControl.ScrollMinY2 = 0D;
            this.GraphControl.Size = new System.Drawing.Size(471, 321);
            this.GraphControl.TabIndex = 44;
            this.GraphControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GraphControlMouseDoubleClick);
            // 
            // UseSignalButton
            // 
            this.UseSignalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UseSignalButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.UseSignalButton.Location = new System.Drawing.Point(178, 339);
            this.UseSignalButton.Name = "UseSignalButton";
            this.UseSignalButton.Size = new System.Drawing.Size(75, 23);
            this.UseSignalButton.TabIndex = 45;
            this.UseSignalButton.Text = "Use Signal";
            this.UseSignalButton.UseVisualStyleBackColor = true;
            this.UseSignalButton.Click += new System.EventHandler(this.UseSignalButtonClick);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(12, 339);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 46;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // SignalGenerationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 374);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.UseSignalButton);
            this.Controls.Add(this.GraphControl);
            this.Controls.Add(this.IgnoreLastSampleField);
            this.Controls.Add(this.SamplingRateField);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.OffsetField);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.PhaseField);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.FrequencyField);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.FinishField);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.StartField);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AmplitudeField);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TemplateField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.qRibbonCaption2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SignalGenerationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Signal Form Generation";
            this.Load += new System.EventHandler(this.SignalFormGenerationLoad);
            ((System.ComponentModel.ISupportInitialize)(this.qRibbonCaption2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmplitudeField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinishField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrequencyField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhaseField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SamplingRateField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Qios.DevSuite.Components.Ribbon.QRibbonCaption qRibbonCaption2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox TemplateField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown AmplitudeField;
        private System.Windows.Forms.NumericUpDown StartField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown FinishField;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown FrequencyField;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown PhaseField;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown OffsetField;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown SamplingRateField;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox IgnoreLastSampleField;
        private ZedGraph.ZedGraphControl GraphControl;
        private System.Windows.Forms.Button UseSignalButton;
        private System.Windows.Forms.Button CancelButton;
    }
}