namespace CodePathFinder.VisualUtility
{
    partial class AssemblyTreeViewer
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
            this.panelBody = new System.Windows.Forms.Panel();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.treeAssemblyViewer = new System.Windows.Forms.TreeView();
            this.panelSearching = new System.Windows.Forms.Panel();
            this.labelText = new System.Windows.Forms.Label();
            this.panelBody.SuspendLayout();
            this.panelSearching.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBody
            // 
            this.panelBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBody.Controls.Add(this.panelSearching);
            this.panelBody.Controls.Add(this.textBoxSearch);
            this.panelBody.Controls.Add(this.treeAssemblyViewer);
            this.panelBody.Location = new System.Drawing.Point(0, 0);
            this.panelBody.Name = "panelBody";
            this.panelBody.Size = new System.Drawing.Size(746, 426);
            this.panelBody.TabIndex = 0;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Location = new System.Drawing.Point(3, 0);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(739, 20);
            this.textBoxSearch.TabIndex = 1;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // treeAssemblyViewer
            // 
            this.treeAssemblyViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeAssemblyViewer.Location = new System.Drawing.Point(3, 26);
            this.treeAssemblyViewer.Name = "treeAssemblyViewer";
            this.treeAssemblyViewer.Size = new System.Drawing.Size(739, 397);
            this.treeAssemblyViewer.TabIndex = 0;
            // 
            // panelSearching
            // 
            this.panelSearching.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearching.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearching.Controls.Add(this.labelText);
            this.panelSearching.Location = new System.Drawing.Point(33, 54);
            this.panelSearching.Name = "panelSearching";
            this.panelSearching.Size = new System.Drawing.Size(677, 335);
            this.panelSearching.TabIndex = 2;
            // 
            // labelText
            // 
            this.labelText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelText.AutoSize = true;
            this.labelText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelText.Location = new System.Drawing.Point(204, 148);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(241, 25);
            this.labelText.TabIndex = 0;
            this.labelText.Text = "Searching... please wait";
            // 
            // AssemblyTreeViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelBody);
            this.Name = "AssemblyTreeViewer";
            this.Size = new System.Drawing.Size(746, 426);
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.panelSearching.ResumeLayout(false);
            this.panelSearching.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.TreeView treeAssemblyViewer;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Panel panelSearching;
        private System.Windows.Forms.Label labelText;
    }
}
