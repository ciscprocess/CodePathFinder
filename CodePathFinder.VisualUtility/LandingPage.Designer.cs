namespace CodePathFinder.VisualUtility
{
    partial class LandingPage
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
            this.groupStartingMethod = new System.Windows.Forms.GroupBox();
            this.assemblyTreeViewer1 = new AssemblyTreeViewer();
            this.groupEndingMethod = new System.Windows.Forms.GroupBox();
            this.assemblyTreeViewer2 = new AssemblyTreeViewer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelStartMethod = new System.Windows.Forms.Label();
            this.labelEndMethod = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.labelAssemblyLocationPath = new System.Windows.Forms.Label();
            this.textAsmLocation = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.buttonLoadAssemblies = new System.Windows.Forms.Button();
            this.buttonFindPaths = new System.Windows.Forms.Button();
            this.folderAsmBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.loadingPanel = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonSetFilters = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.loadingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupStartingMethod
            // 
            this.groupStartingMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupStartingMethod.Location = new System.Drawing.Point(3, 3);
            this.groupStartingMethod.Name = "groupStartingMethod";
            this.groupStartingMethod.Size = new System.Drawing.Size(475, 314);
            this.groupStartingMethod.TabIndex = 0;
            this.groupStartingMethod.TabStop = false;
            this.groupStartingMethod.Text = "Starting Method";
            // 
            // assemblyTreeViewer1
            // 
            this.assemblyTreeViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.assemblyTreeViewer1.Location = new System.Drawing.Point(9, 20);
            this.assemblyTreeViewer1.Name = "assemblyTreeViewer1";
            this.assemblyTreeViewer1.Size = new System.Drawing.Size(460, 288);
            this.assemblyTreeViewer1.TabIndex = 0;

            // 
            // assemblyTreeViewer2
            // 
            this.assemblyTreeViewer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.assemblyTreeViewer2.Location = new System.Drawing.Point(6, 20);
            this.assemblyTreeViewer2.Name = "assemblyTreeViewer2";
            this.assemblyTreeViewer2.Size = new System.Drawing.Size(481, 288);
            this.assemblyTreeViewer2.TabIndex = 1;
            // 
            // groupEndingMethod
            // 
            this.groupEndingMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupEndingMethod.Location = new System.Drawing.Point(484, 3);
            this.groupEndingMethod.Name = "groupEndingMethod";
            this.groupEndingMethod.Size = new System.Drawing.Size(493, 314);
            this.groupEndingMethod.TabIndex = 1;
            this.groupEndingMethod.TabStop = false;
            this.groupEndingMethod.Text = "Ending Method";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.18367F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.81633F));
            this.tableLayoutPanel1.Controls.Add(this.groupStartingMethod, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupEndingMethod, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 70);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(980, 320);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // labelStartMethod
            // 
            this.labelStartMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStartMethod.AutoSize = true;
            this.labelStartMethod.Location = new System.Drawing.Point(13, 400);
            this.labelStartMethod.Name = "labelStartMethod";
            this.labelStartMethod.Size = new System.Drawing.Size(35, 13);
            this.labelStartMethod.TabIndex = 3;
            this.labelStartMethod.Text = "Start: ";
            // 
            // labelEndMethod
            // 
            this.labelEndMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelEndMethod.AutoSize = true;
            this.labelEndMethod.Location = new System.Drawing.Point(13, 423);
            this.labelEndMethod.Name = "labelEndMethod";
            this.labelEndMethod.Size = new System.Drawing.Size(32, 13);
            this.labelEndMethod.TabIndex = 4;
            this.labelEndMethod.Text = "End: ";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(54, 397);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(802, 20);
            this.textBox1.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(54, 420);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(802, 20);
            this.textBox2.TabIndex = 6;
            // 
            // labelAssemblyLocationPath
            // 
            this.labelAssemblyLocationPath.AutoSize = true;
            this.labelAssemblyLocationPath.Location = new System.Drawing.Point(13, 13);
            this.labelAssemblyLocationPath.Name = "labelAssemblyLocationPath";
            this.labelAssemblyLocationPath.Size = new System.Drawing.Size(86, 13);
            this.labelAssemblyLocationPath.TabIndex = 7;
            this.labelAssemblyLocationPath.Text = "Assembly Folder:";
            // 
            // textAsmLocation
            // 
            this.textAsmLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textAsmLocation.Enabled = false;
            this.textAsmLocation.Location = new System.Drawing.Point(105, 10);
            this.textAsmLocation.Name = "textAsmLocation";
            this.textAsmLocation.Size = new System.Drawing.Size(751, 20);
            this.textAsmLocation.TabIndex = 8;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(862, 8);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(119, 23);
            this.buttonBrowse.TabIndex = 9;
            this.buttonBrowse.Text = "Browse...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // buttonLoadAssemblies
            // 
            this.buttonLoadAssemblies.Location = new System.Drawing.Point(16, 36);
            this.buttonLoadAssemblies.Name = "buttonLoadAssemblies";
            this.buttonLoadAssemblies.Size = new System.Drawing.Size(119, 23);
            this.buttonLoadAssemblies.TabIndex = 10;
            this.buttonLoadAssemblies.Text = "Load Assemblies";
            this.buttonLoadAssemblies.UseVisualStyleBackColor = true;
            this.buttonLoadAssemblies.Click += new System.EventHandler(this.buttonLoadAssemblies_Click);
            // 
            // buttonFindPaths
            // 
            this.buttonFindPaths.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFindPaths.Location = new System.Drawing.Point(862, 397);
            this.buttonFindPaths.Name = "buttonFindPaths";
            this.buttonFindPaths.Size = new System.Drawing.Size(119, 43);
            this.buttonFindPaths.TabIndex = 11;
            this.buttonFindPaths.Text = "Find Paths";
            this.buttonFindPaths.UseVisualStyleBackColor = true;
            // 
            // loadingPanel
            // 
            this.loadingPanel.Controls.Add(this.progressBar1);
            this.loadingPanel.Controls.Add(this.labelStatus);
            this.loadingPanel.Location = new System.Drawing.Point(310, 56);
            this.loadingPanel.Name = "loadingPanel";
            this.loadingPanel.Size = new System.Drawing.Size(359, 153);
            this.loadingPanel.TabIndex = 12;
            this.loadingPanel.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(19, 83);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(326, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(15, 33);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(16, 13);
            this.labelStatus.TabIndex = 0;
            this.labelStatus.Text = "...";
            // 
            // buttonSetFilters
            // 
            this.buttonSetFilters.Location = new System.Drawing.Point(141, 36);
            this.buttonSetFilters.Name = "buttonSetFilters";
            this.buttonSetFilters.Size = new System.Drawing.Size(112, 23);
            this.buttonSetFilters.TabIndex = 13;
            this.buttonSetFilters.Text = "Set Assembly Filters";
            this.buttonSetFilters.UseVisualStyleBackColor = true;
            this.buttonSetFilters.Click += new System.EventHandler(this.buttonSetFilters_Click);
            // 
            // LandingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 445);
            this.Controls.Add(this.buttonSetFilters);
            this.Controls.Add(this.loadingPanel);
            this.Controls.Add(this.buttonFindPaths);
            this.Controls.Add(this.buttonLoadAssemblies);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textAsmLocation);
            this.Controls.Add(this.labelAssemblyLocationPath);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelEndMethod);
            this.Controls.Add(this.labelStartMethod);
            this.Controls.Add(this.tableLayoutPanel1);
            this.groupStartingMethod.Controls.Add(this.assemblyTreeViewer1);
            this.groupEndingMethod.Controls.Add(this.assemblyTreeViewer2);
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "LandingPage";
            this.Text = "Path Finder Landing";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.loadingPanel.ResumeLayout(false);
            this.loadingPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupStartingMethod;
        private System.Windows.Forms.GroupBox groupEndingMethod;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelStartMethod;
        private System.Windows.Forms.Label labelEndMethod;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private AssemblyTreeViewer assemblyTreeViewer1;
        private AssemblyTreeViewer assemblyTreeViewer2;
        private System.Windows.Forms.Label labelAssemblyLocationPath;
        private System.Windows.Forms.TextBox textAsmLocation;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonLoadAssemblies;
        private System.Windows.Forms.Button buttonFindPaths;
        private System.Windows.Forms.FolderBrowserDialog folderAsmBrowserDialog;
        private System.Windows.Forms.Panel loadingPanel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonSetFilters;
    }
}

