namespace CodePathFinder.VisualUtility
{
    partial class ResultsViewerLanding
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.buttonShowGraph = new System.Windows.Forms.Button();
            this.checkLimitResultPath = new System.Windows.Forms.CheckBox();
            this.numPathLength = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numPathLength)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(13, 13);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(759, 507);
            this.treeView1.TabIndex = 0;
            // 
            // buttonShowGraph
            // 
            this.buttonShowGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonShowGraph.Location = new System.Drawing.Point(13, 526);
            this.buttonShowGraph.Name = "buttonShowGraph";
            this.buttonShowGraph.Size = new System.Drawing.Size(162, 23);
            this.buttonShowGraph.TabIndex = 1;
            this.buttonShowGraph.Text = "Show Graph View";
            this.buttonShowGraph.UseVisualStyleBackColor = true;
            this.buttonShowGraph.Click += new System.EventHandler(this.buttonShowGraph_Click);
            // 
            // checkLimitResultPath
            // 
            this.checkLimitResultPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkLimitResultPath.AutoSize = true;
            this.checkLimitResultPath.Location = new System.Drawing.Point(182, 531);
            this.checkLimitResultPath.Name = "checkLimitResultPath";
            this.checkLimitResultPath.Size = new System.Drawing.Size(105, 17);
            this.checkLimitResultPath.TabIndex = 2;
            this.checkLimitResultPath.Text = "Limit Result Path";
            this.checkLimitResultPath.UseVisualStyleBackColor = true;
            this.checkLimitResultPath.CheckedChanged += new System.EventHandler(this.checkLimitResultPath_CheckedChanged);
            // 
            // numPathLength
            // 
            this.numPathLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numPathLength.Enabled = false;
            this.numPathLength.Location = new System.Drawing.Point(293, 529);
            this.numPathLength.Name = "numPathLength";
            this.numPathLength.Size = new System.Drawing.Size(120, 20);
            this.numPathLength.TabIndex = 3;
            this.numPathLength.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPathLength.ValueChanged += new System.EventHandler(this.numPathLength_ValueChanged);
            // 
            // ResultsViewerLanding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.numPathLength);
            this.Controls.Add(this.checkLimitResultPath);
            this.Controls.Add(this.buttonShowGraph);
            this.Controls.Add(this.treeView1);
            this.Name = "ResultsViewerLanding";
            this.Text = "Results Viewer";
            this.UseWaitCursor = true;
            ((System.ComponentModel.ISupportInitialize)(this.numPathLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button buttonShowGraph;
        private System.Windows.Forms.CheckBox checkLimitResultPath;
        private System.Windows.Forms.NumericUpDown numPathLength;
    }
}