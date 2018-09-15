using CodePathFinder.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodePathFinder.VisualUtility
{
    public partial class AddAssemblyLoadOption : Form
    {
        public AssemblyMetadataOption Generated { get; private set; }
        public bool IsExclusion { get; private set; }

        public AddAssemblyLoadOption(bool isExclusion)
        {
            InitializeComponent();
            this.IsExclusion = isExclusion;
        }

        private void AddAssemblyLoadOption_Load(object sender, EventArgs e)
        {
            var enums = Enum.GetNames(typeof(AssemblyMetadataOption.AssemblyMetadataAttribute));

            foreach (var name in enums)
            {
                this.comboBoxAttrType.Items.Add(name);
            }

            this.checkBoxExclusion.Checked = this.IsExclusion;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.comboBoxAttrType.SelectedItem as string) ||
                string.IsNullOrWhiteSpace(this.textBoxMatchValue.Text))
            {
                MessageBox.Show("Please enter all required values.", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var type = Enum.Parse(typeof(AssemblyMetadataOption.AssemblyMetadataAttribute),
                (string)this.comboBoxAttrType.SelectedItem);

            this.Generated = new AssemblyMetadataOption
            {
                 AttributeType = (AssemblyMetadataOption.AssemblyMetadataAttribute)type,
                 Value = this.textBoxMatchValue.Text,
                 IsRegex = this.checkBoxIsRegex.Checked,
                 Exclude = this.checkBoxExclusion.Checked
            };

            this.Close();
        }
    }
}
