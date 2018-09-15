using CodePathFinder.CodeAnalysis;
using CodePathFinder.CodeAnalysis.PathFinding;
using System;
using System.Collections;
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
    public partial class ResultsViewerLanding : Form
    {
        private Method start;
        private Method end;
        private IList<CodePath> unfiltered;
        private IList<CodePath> filtered;

        public ResultsViewerLanding(IList<CodePath> results, Method start, Method end)
        {
            InitializeComponent();
            this.start = start;
            this.end = end;
            this.unfiltered = results;
            this.filtered = results;

            RenderTree();
            ResetCursor(this.Controls);
            this.UseWaitCursor = false;
        }

        void ResetCursor(IEnumerable theControls)
        {
            foreach (Control control in theControls)
            {
                if (control.HasChildren)
                {
                    ResetCursor(control.Controls);
                }
                else
                    control.Cursor = Cursors.Arrow;
            }
        }

        private void RenderTree()
        {
            this.treeView1.Nodes.Clear();
            this.treeView1.Nodes.Add(start.ToString());

            foreach (var path in this.filtered)
            {
                TreeNode last = null;
                foreach (var node in path)
                {
                    if (last == null)
                    {
                        last = this.treeView1.Nodes.Cast<TreeNode>().First(x => x.Text == node.ToString());
                    }
                    else
                    {
                        TreeNode found;
                        if ((found = last.Nodes.Cast<TreeNode>().FirstOrDefault(x => x.Text == node.ToString())) != null)
                        {
                            last = found;
                        }
                        else
                        {
                            last = last.Nodes.Add(node.ToString());
                        }
                    }
                }
            }
        }

        private void checkLimitResultPath_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkLimitResultPath.Checked)
            {
                this.numPathLength.Enabled = true;
                this.filtered = this.unfiltered.Where(x => x.Count() < this.numPathLength.Value).ToList();
                RenderTree();
            }
            else
            {
                this.numPathLength.Enabled = false;
                this.filtered = this.unfiltered.ToList();
                RenderTree();
            }
        }

        private void numPathLength_ValueChanged(object sender, EventArgs e)
        {
            this.filtered = this.unfiltered.Where(x => x.Count() < this.numPathLength.Value).ToList();
            this.RenderTree();
        }

        private void buttonShowGraph_Click(object sender, EventArgs e)
        {
            var gView = new ResultViewer(this.filtered, start, end);
            gView.Show();
        }
    }
}
