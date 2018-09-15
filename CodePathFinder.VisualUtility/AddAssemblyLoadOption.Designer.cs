namespace CodePathFinder.VisualUtility
{
    partial class AddAssemblyLoadOption
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
            this.labelMatchValue = new System.Windows.Forms.Label();
            this.labelIsRegex = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxAttrType = new System.Windows.Forms.ComboBox();
            this.textBoxMatchValue = new System.Windows.Forms.TextBox();
            this.checkBoxIsRegex = new System.Windows.Forms.CheckBox();
            this.checkBoxExclusion = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelMatchValue
            // 
            this.labelMatchValue.AutoSize = true;
            this.labelMatchValue.Location = new System.Drawing.Point(12, 34);
            this.labelMatchValue.Name = "labelMatchValue";
            this.labelMatchValue.Size = new System.Drawing.Size(70, 13);
            this.labelMatchValue.TabIndex = 0;
            this.labelMatchValue.Text = "Match Value:";
            // 
            // labelIsRegex
            // 
            this.labelIsRegex.AutoSize = true;
            this.labelIsRegex.Location = new System.Drawing.Point(12, 59);
            this.labelIsRegex.Name = "labelIsRegex";
            this.labelIsRegex.Size = new System.Drawing.Size(52, 13);
            this.labelIsRegex.TabIndex = 1;
            this.labelIsRegex.Text = "Is Regex:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Is Exclusion: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Attribute Type:";
            // 
            // comboBoxAttrType
            // 
            this.comboBoxAttrType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAttrType.FormattingEnabled = true;
            this.comboBoxAttrType.Location = new System.Drawing.Point(106, 6);
            this.comboBoxAttrType.Name = "comboBoxAttrType";
            this.comboBoxAttrType.Size = new System.Drawing.Size(178, 21);
            this.comboBoxAttrType.TabIndex = 4;
            // 
            // textBoxMatchValue
            // 
            this.textBoxMatchValue.Location = new System.Drawing.Point(106, 31);
            this.textBoxMatchValue.Name = "textBoxMatchValue";
            this.textBoxMatchValue.Size = new System.Drawing.Size(178, 20);
            this.textBoxMatchValue.TabIndex = 5;
            // 
            // checkBoxIsRegex
            // 
            this.checkBoxIsRegex.AutoSize = true;
            this.checkBoxIsRegex.Location = new System.Drawing.Point(106, 58);
            this.checkBoxIsRegex.Name = "checkBoxIsRegex";
            this.checkBoxIsRegex.Size = new System.Drawing.Size(15, 14);
            this.checkBoxIsRegex.TabIndex = 6;
            this.checkBoxIsRegex.UseVisualStyleBackColor = true;
            // 
            // checkBoxExclusion
            // 
            this.checkBoxExclusion.AutoSize = true;
            this.checkBoxExclusion.Enabled = false;
            this.checkBoxExclusion.Location = new System.Drawing.Point(106, 81);
            this.checkBoxExclusion.Name = "checkBoxExclusion";
            this.checkBoxExclusion.Size = new System.Drawing.Size(15, 14);
            this.checkBoxExclusion.TabIndex = 7;
            this.checkBoxExclusion.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(106, 109);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // AddAssemblyLoadOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 144);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.checkBoxExclusion);
            this.Controls.Add(this.checkBoxIsRegex);
            this.Controls.Add(this.textBoxMatchValue);
            this.Controls.Add(this.comboBoxAttrType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelIsRegex);
            this.Controls.Add(this.labelMatchValue);
            this.MaximumSize = new System.Drawing.Size(321, 183);
            this.MinimumSize = new System.Drawing.Size(321, 183);
            this.Name = "AddAssemblyLoadOption";
            this.Text = "Add Assembly Load Filter";
            this.Load += new System.EventHandler(this.AddAssemblyLoadOption_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMatchValue;
        private System.Windows.Forms.Label labelIsRegex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxAttrType;
        private System.Windows.Forms.TextBox textBoxMatchValue;
        private System.Windows.Forms.CheckBox checkBoxIsRegex;
        private System.Windows.Forms.CheckBox checkBoxExclusion;
        private System.Windows.Forms.Button buttonSave;
    }
}