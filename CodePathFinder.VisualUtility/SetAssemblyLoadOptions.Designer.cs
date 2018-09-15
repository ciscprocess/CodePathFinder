namespace CodePathFinder.VisualUtility
{
    partial class SetAssemblyLoadOptions
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
            this.groupBoxInclude = new System.Windows.Forms.GroupBox();
            this.groupBoxExclude = new System.Windows.Forms.GroupBox();
            this.dataGridViewInclude = new System.Windows.Forms.DataGridView();
            this.dataGridViewExclude = new System.Windows.Forms.DataGridView();
            this.buttonAddInclude = new System.Windows.Forms.Button();
            this.buttonAddExclude = new System.Windows.Forms.Button();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Regex = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.buttonRemoveInclude = new System.Windows.Forms.Button();
            this.buttonRemoveExclude = new System.Windows.Forms.Button();
            this.groupBoxInclude.SuspendLayout();
            this.groupBoxExclude.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInclude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExclude)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxInclude
            // 
            this.groupBoxInclude.Controls.Add(this.buttonRemoveInclude);
            this.groupBoxInclude.Controls.Add(this.buttonAddInclude);
            this.groupBoxInclude.Controls.Add(this.dataGridViewInclude);
            this.groupBoxInclude.Location = new System.Drawing.Point(13, 13);
            this.groupBoxInclude.Name = "groupBoxInclude";
            this.groupBoxInclude.Size = new System.Drawing.Size(365, 440);
            this.groupBoxInclude.TabIndex = 2;
            this.groupBoxInclude.TabStop = false;
            this.groupBoxInclude.Text = "Include";
            // 
            // groupBoxExclude
            // 
            this.groupBoxExclude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxExclude.Controls.Add(this.buttonRemoveExclude);
            this.groupBoxExclude.Controls.Add(this.buttonAddExclude);
            this.groupBoxExclude.Controls.Add(this.dataGridViewExclude);
            this.groupBoxExclude.Location = new System.Drawing.Point(384, 13);
            this.groupBoxExclude.Name = "groupBoxExclude";
            this.groupBoxExclude.Size = new System.Drawing.Size(363, 440);
            this.groupBoxExclude.TabIndex = 3;
            this.groupBoxExclude.TabStop = false;
            this.groupBoxExclude.Text = "Exclude";
            // 
            // dataGridViewInclude
            // 
            this.dataGridViewInclude.AllowUserToAddRows = false;
            this.dataGridViewInclude.AllowUserToDeleteRows = false;
            this.dataGridViewInclude.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInclude.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Value,
            this.Type,
            this.Regex});
            this.dataGridViewInclude.Location = new System.Drawing.Point(7, 20);
            this.dataGridViewInclude.Name = "dataGridViewInclude";
            this.dataGridViewInclude.ReadOnly = true;
            this.dataGridViewInclude.Size = new System.Drawing.Size(347, 384);
            this.dataGridViewInclude.TabIndex = 0;
            // 
            // dataGridViewExclude
            // 
            this.dataGridViewExclude.AllowUserToAddRows = false;
            this.dataGridViewExclude.AllowUserToDeleteRows = false;
            this.dataGridViewExclude.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExclude.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewCheckBoxColumn1});
            this.dataGridViewExclude.Location = new System.Drawing.Point(6, 20);
            this.dataGridViewExclude.Name = "dataGridViewExclude";
            this.dataGridViewExclude.ReadOnly = true;
            this.dataGridViewExclude.Size = new System.Drawing.Size(347, 384);
            this.dataGridViewExclude.TabIndex = 1;
            // 
            // buttonAddInclude
            // 
            this.buttonAddInclude.Location = new System.Drawing.Point(7, 410);
            this.buttonAddInclude.Name = "buttonAddInclude";
            this.buttonAddInclude.Size = new System.Drawing.Size(126, 23);
            this.buttonAddInclude.TabIndex = 1;
            this.buttonAddInclude.Text = "Add";
            this.buttonAddInclude.UseVisualStyleBackColor = true;
            this.buttonAddInclude.Click += new System.EventHandler(this.buttonAddInclude_Click);
            // 
            // buttonAddExclude
            // 
            this.buttonAddExclude.Location = new System.Drawing.Point(6, 410);
            this.buttonAddExclude.Name = "buttonAddExclude";
            this.buttonAddExclude.Size = new System.Drawing.Size(126, 23);
            this.buttonAddExclude.TabIndex = 4;
            this.buttonAddExclude.Text = "Add";
            this.buttonAddExclude.UseVisualStyleBackColor = true;
            this.buttonAddExclude.Click += new System.EventHandler(this.buttonAddExclude_Click);
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // Regex
            // 
            this.Regex.HeaderText = "Regex";
            this.Regex.Name = "Regex";
            this.Regex.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Value";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Type";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "Regex";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly = true;
            // 
            // buttonRemoveInclude
            // 
            this.buttonRemoveInclude.Location = new System.Drawing.Point(139, 410);
            this.buttonRemoveInclude.Name = "buttonRemoveInclude";
            this.buttonRemoveInclude.Size = new System.Drawing.Size(126, 23);
            this.buttonRemoveInclude.TabIndex = 2;
            this.buttonRemoveInclude.Text = "Remove";
            this.buttonRemoveInclude.UseVisualStyleBackColor = true;
            this.buttonRemoveInclude.Click += new System.EventHandler(this.buttonRemoveInclude_Click);
            // 
            // buttonRemoveExclude
            // 
            this.buttonRemoveExclude.Location = new System.Drawing.Point(138, 410);
            this.buttonRemoveExclude.Name = "buttonRemoveExclude";
            this.buttonRemoveExclude.Size = new System.Drawing.Size(126, 23);
            this.buttonRemoveExclude.TabIndex = 5;
            this.buttonRemoveExclude.Text = "Remove";
            this.buttonRemoveExclude.UseVisualStyleBackColor = true;
            this.buttonRemoveExclude.Click += new System.EventHandler(this.buttonRemoveExclude_Click);
            // 
            // SetAssemblyLoadOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 460);
            this.Controls.Add(this.groupBoxExclude);
            this.Controls.Add(this.groupBoxInclude);
            this.MaximumSize = new System.Drawing.Size(767, 499);
            this.MinimumSize = new System.Drawing.Size(767, 499);
            this.Name = "SetAssemblyLoadOptions";
            this.Text = "Set Assembly Load Filters";
            this.groupBoxInclude.ResumeLayout(false);
            this.groupBoxExclude.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInclude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExclude)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxInclude;
        private System.Windows.Forms.GroupBox groupBoxExclude;
        private System.Windows.Forms.DataGridView dataGridViewInclude;
        private System.Windows.Forms.DataGridView dataGridViewExclude;
        private System.Windows.Forms.Button buttonAddInclude;
        private System.Windows.Forms.Button buttonAddExclude;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Regex;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.Button buttonRemoveInclude;
        private System.Windows.Forms.Button buttonRemoveExclude;
    }
}