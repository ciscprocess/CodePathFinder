namespace CodePathFinder.VisualUtility
{
    partial class StartSearchPage
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
            this.tabViewSwitcher = new CodePathFinder.VisualUtility.TablessControl();
            this.view1 = new System.Windows.Forms.TabPage();
            this.labelSummary = new System.Windows.Forms.Label();
            this.panelStartButtons = new System.Windows.Forms.Panel();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textEndMethod = new System.Windows.Forms.TextBox();
            this.textStartMethod = new System.Windows.Forms.TextBox();
            this.labelEndMethod = new System.Windows.Forms.Label();
            this.labelStartMethod = new System.Windows.Forms.Label();
            this.numericDepthLImit = new System.Windows.Forms.NumericUpDown();
            this.labelPathDepth = new System.Windows.Forms.Label();
            this.checkLimitPathDepth = new System.Windows.Forms.CheckBox();
            this.labelLimitPathDepth = new System.Windows.Forms.Label();
            this.view2 = new System.Windows.Forms.TabPage();
            this.buttonAbort = new System.Windows.Forms.Button();
            this.pictureSpinner = new System.Windows.Forms.PictureBox();
            this.labelSearchStatus = new System.Windows.Forms.Label();
            this.view3 = new System.Windows.Forms.TabPage();
            this.tabViewSwitcher.SuspendLayout();
            this.view1.SuspendLayout();
            this.panelStartButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDepthLImit)).BeginInit();
            this.view2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // tabViewSwitcher
            // 
            this.tabViewSwitcher.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabViewSwitcher.Controls.Add(this.view1);
            this.tabViewSwitcher.Controls.Add(this.view2);
            this.tabViewSwitcher.Controls.Add(this.view3);
            this.tabViewSwitcher.Location = new System.Drawing.Point(-3, -8);
            this.tabViewSwitcher.Name = "tabViewSwitcher";
            this.tabViewSwitcher.SelectedIndex = 0;
            this.tabViewSwitcher.Size = new System.Drawing.Size(697, 417);
            this.tabViewSwitcher.TabIndex = 0;
            // 
            // view1
            // 
            this.view1.Controls.Add(this.labelSummary);
            this.view1.Controls.Add(this.panelStartButtons);
            this.view1.Controls.Add(this.textEndMethod);
            this.view1.Controls.Add(this.textStartMethod);
            this.view1.Controls.Add(this.labelEndMethod);
            this.view1.Controls.Add(this.labelStartMethod);
            this.view1.Controls.Add(this.numericDepthLImit);
            this.view1.Controls.Add(this.labelPathDepth);
            this.view1.Controls.Add(this.checkLimitPathDepth);
            this.view1.Controls.Add(this.labelLimitPathDepth);
            this.view1.Location = new System.Drawing.Point(4, 22);
            this.view1.Name = "view1";
            this.view1.Padding = new System.Windows.Forms.Padding(3);
            this.view1.Size = new System.Drawing.Size(689, 391);
            this.view1.TabIndex = 0;
            this.view1.Text = "view1";
            this.view1.UseVisualStyleBackColor = true;
            // 
            // labelSummary
            // 
            this.labelSummary.AutoSize = true;
            this.labelSummary.Location = new System.Drawing.Point(12, 11);
            this.labelSummary.Name = "labelSummary";
            this.labelSummary.Size = new System.Drawing.Size(493, 13);
            this.labelSummary.TabIndex = 11;
            this.labelSummary.Text = "Please verify the parameters below before beginning the search. You may also canc" +
    "el if you so choose.";
            // 
            // panelStartButtons
            // 
            this.panelStartButtons.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panelStartButtons.Controls.Add(this.buttonStart);
            this.panelStartButtons.Controls.Add(this.buttonCancel);
            this.panelStartButtons.Location = new System.Drawing.Point(261, 349);
            this.panelStartButtons.Name = "panelStartButtons";
            this.panelStartButtons.Size = new System.Drawing.Size(164, 31);
            this.panelStartButtons.TabIndex = 10;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(3, 3);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 8;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(84, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // textEndMethod
            // 
            this.textEndMethod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEndMethod.Location = new System.Drawing.Point(100, 73);
            this.textEndMethod.Name = "textEndMethod";
            this.textEndMethod.Size = new System.Drawing.Size(581, 20);
            this.textEndMethod.TabIndex = 7;
            // 
            // textStartMethod
            // 
            this.textStartMethod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textStartMethod.Location = new System.Drawing.Point(100, 45);
            this.textStartMethod.Name = "textStartMethod";
            this.textStartMethod.Size = new System.Drawing.Size(581, 20);
            this.textStartMethod.TabIndex = 6;
            // 
            // labelEndMethod
            // 
            this.labelEndMethod.AutoSize = true;
            this.labelEndMethod.Location = new System.Drawing.Point(9, 76);
            this.labelEndMethod.Name = "labelEndMethod";
            this.labelEndMethod.Size = new System.Drawing.Size(82, 13);
            this.labelEndMethod.TabIndex = 5;
            this.labelEndMethod.Text = "Ending Method:";
            // 
            // labelStartMethod
            // 
            this.labelStartMethod.AutoSize = true;
            this.labelStartMethod.Location = new System.Drawing.Point(9, 48);
            this.labelStartMethod.Name = "labelStartMethod";
            this.labelStartMethod.Size = new System.Drawing.Size(88, 13);
            this.labelStartMethod.TabIndex = 4;
            this.labelStartMethod.Text = "Starting Method: ";
            // 
            // numericDepthLImit
            // 
            this.numericDepthLImit.Enabled = false;
            this.numericDepthLImit.Location = new System.Drawing.Point(57, 138);
            this.numericDepthLImit.Name = "numericDepthLImit";
            this.numericDepthLImit.Size = new System.Drawing.Size(94, 20);
            this.numericDepthLImit.TabIndex = 3;
            // 
            // labelPathDepth
            // 
            this.labelPathDepth.AutoSize = true;
            this.labelPathDepth.Location = new System.Drawing.Point(9, 140);
            this.labelPathDepth.Name = "labelPathDepth";
            this.labelPathDepth.Size = new System.Drawing.Size(42, 13);
            this.labelPathDepth.TabIndex = 2;
            this.labelPathDepth.Text = "Depth: ";
            // 
            // checkLimitPathDepth
            // 
            this.checkLimitPathDepth.AutoSize = true;
            this.checkLimitPathDepth.Location = new System.Drawing.Point(103, 116);
            this.checkLimitPathDepth.Name = "checkLimitPathDepth";
            this.checkLimitPathDepth.Size = new System.Drawing.Size(15, 14);
            this.checkLimitPathDepth.TabIndex = 1;
            this.checkLimitPathDepth.UseVisualStyleBackColor = true;
            this.checkLimitPathDepth.CheckedChanged += new System.EventHandler(this.CheckLimitPathDepth_CheckedChanged);
            // 
            // labelLimitPathDepth
            // 
            this.labelLimitPathDepth.AutoSize = true;
            this.labelLimitPathDepth.Location = new System.Drawing.Point(9, 116);
            this.labelLimitPathDepth.Name = "labelLimitPathDepth";
            this.labelLimitPathDepth.Size = new System.Drawing.Size(88, 13);
            this.labelLimitPathDepth.TabIndex = 0;
            this.labelLimitPathDepth.Text = "Limit Path Depth:";
            // 
            // view2
            // 
            this.view2.Controls.Add(this.buttonAbort);
            this.view2.Controls.Add(this.pictureSpinner);
            this.view2.Controls.Add(this.labelSearchStatus);
            this.view2.Location = new System.Drawing.Point(4, 22);
            this.view2.Name = "view2";
            this.view2.Padding = new System.Windows.Forms.Padding(3);
            this.view2.Size = new System.Drawing.Size(689, 391);
            this.view2.TabIndex = 1;
            this.view2.Text = "view2";
            this.view2.UseVisualStyleBackColor = true;
            // 
            // buttonAbort
            // 
            this.buttonAbort.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonAbort.Location = new System.Drawing.Point(309, 358);
            this.buttonAbort.Name = "buttonAbort";
            this.buttonAbort.Size = new System.Drawing.Size(75, 23);
            this.buttonAbort.TabIndex = 2;
            this.buttonAbort.Text = "Abort";
            this.buttonAbort.UseVisualStyleBackColor = true;
            this.buttonAbort.Click += new System.EventHandler(this.ButtonAbort_Click);
            // 
            // pictureSpinner
            // 
            this.pictureSpinner.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureSpinner.Image = global::CodePathFinder.VisualUtility.Properties.Resources.Spinner_1s_200px;
            this.pictureSpinner.Location = new System.Drawing.Point(236, 90);
            this.pictureSpinner.Name = "pictureSpinner";
            this.pictureSpinner.Size = new System.Drawing.Size(203, 196);
            this.pictureSpinner.TabIndex = 1;
            this.pictureSpinner.TabStop = false;
            // 
            // labelSearchStatus
            // 
            this.labelSearchStatus.AutoSize = true;
            this.labelSearchStatus.Location = new System.Drawing.Point(11, 18);
            this.labelSearchStatus.Name = "labelSearchStatus";
            this.labelSearchStatus.Size = new System.Drawing.Size(632, 13);
            this.labelSearchStatus.TabIndex = 0;
            this.labelSearchStatus.Text = "Constructing the partial call graph. This may take up to a minute, depending on t" +
    "he number of assemblies and complexity of the code.";
            // 
            // view3
            // 
            this.view3.Location = new System.Drawing.Point(4, 22);
            this.view3.Name = "view3";
            this.view3.Padding = new System.Windows.Forms.Padding(3);
            this.view3.Size = new System.Drawing.Size(689, 391);
            this.view3.TabIndex = 2;
            this.view3.Text = "view3";
            this.view3.UseVisualStyleBackColor = true;
            // 
            // StartSearchPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 406);
            this.Controls.Add(this.tabViewSwitcher);
            this.MinimumSize = new System.Drawing.Size(710, 445);
            this.Name = "StartSearchPage";
            this.Text = "Start Search";
            this.tabViewSwitcher.ResumeLayout(false);
            this.view1.ResumeLayout(false);
            this.view1.PerformLayout();
            this.panelStartButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericDepthLImit)).EndInit();
            this.view2.ResumeLayout(false);
            this.view2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSpinner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage view1;
        private System.Windows.Forms.Panel panelStartButtons;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textEndMethod;
        private System.Windows.Forms.TextBox textStartMethod;
        private System.Windows.Forms.Label labelEndMethod;
        private System.Windows.Forms.Label labelStartMethod;
        private System.Windows.Forms.NumericUpDown numericDepthLImit;
        private System.Windows.Forms.Label labelPathDepth;
        private System.Windows.Forms.CheckBox checkLimitPathDepth;
        private System.Windows.Forms.Label labelLimitPathDepth;
        private System.Windows.Forms.TabPage view2;
        private System.Windows.Forms.TabPage view3;
        private TablessControl tabViewSwitcher;
        private System.Windows.Forms.Label labelSummary;
        private System.Windows.Forms.Label labelSearchStatus;
        private System.Windows.Forms.PictureBox pictureSpinner;
        private System.Windows.Forms.Button buttonAbort;
    }
}