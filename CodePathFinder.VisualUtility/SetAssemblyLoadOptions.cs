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
    public partial class SetAssemblyLoadOptions : Form
    {
        public List<AssemblyMetadataOption> Includes { get; private set; }
        public List<AssemblyMetadataOption> Excludes { get; private set; }

        public SetAssemblyLoadOptions(AssemblyMetadataOption[] options)
        {
            InitializeComponent();
            this.Includes = options.Where(x => !x.Exclude).ToList();
            this.Excludes = options.Where(x => x.Exclude).ToList();

            this.Includes.ForEach(x => 
            {
                this.dataGridViewInclude.Rows.Add(x.Value,
                    x.AttributeType.ToString(),
                    x.IsRegex);
            });

            this.Excludes.ForEach(x =>
            {
                this.dataGridViewExclude.Rows.Add(x.Value,
                    x.AttributeType.ToString(),
                    x.IsRegex);
            });
        }

        private void buttonAddInclude_Click(object sender, EventArgs e)
        {
            var form = new AddAssemblyLoadOption(false);
            form.ShowDialog();

            if (form.Generated != null)
            {
                this.Includes.Add(form.Generated);
                this.dataGridViewInclude.Rows.Add(form.Generated.Value,
                    form.Generated.AttributeType.ToString(),
                    form.Generated.IsRegex);
            }
        }

        private void buttonAddExclude_Click(object sender, EventArgs e)
        {
            var form = new AddAssemblyLoadOption(true);
            form.ShowDialog();

            if (form.Generated != null)
            {
                this.Excludes.Add(form.Generated);
                this.dataGridViewExclude.Rows.Add(form.Generated.Value, 
                    form.Generated.AttributeType.ToString(), 
                    form.Generated.IsRegex);
            }
        }

        private void buttonRemoveInclude_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewInclude.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row!", "Oops");
                return;
            }

            var index = this.dataGridViewInclude.SelectedRows[0].Index;
            this.dataGridViewInclude.Rows.RemoveAt(index);
            this.Includes.RemoveAt(index);
        }

        private void buttonRemoveExclude_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewExclude.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row!", "Oops");
                return;
            }

            var index = this.dataGridViewExclude.SelectedRows[0].Index;
            this.dataGridViewExclude.Rows.RemoveAt(index);
            this.Excludes.RemoveAt(index);
        }
    }
}
