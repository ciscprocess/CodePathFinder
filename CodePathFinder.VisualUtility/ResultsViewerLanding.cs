using CodePathFinder.CodeAnalysis;
using CodePathFinder.CodeAnalysis.PathFinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CodePathFinder.VisualUtility
{
    public partial class ResultsViewerLanding : Form
    {
        private Method start;
        private Method end;
        private IList<CodePath> filtered;
        private ResultTreeNode resultTreeRoot = null;


        public ResultsViewerLanding(IList<CodePath> results, Method start, Method end)
        {
            InitializeComponent();
            this.start = start;
            this.end = end;
            this.filtered = results;

            ConstructResultTree();
            RenderTreeDynamic();

            this.UseWaitCursor = false;

            this.treeView1.BeforeExpand += TreeView1_BeforeExpand;
        }

        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            var treeNode = e.Node;
            var abstractNode = (ResultTreeNode)treeNode.Tag;
            if (treeNode.Nodes.Count == 1 && treeNode.Nodes[0].Text == "<loading>")
            {
                treeNode.Nodes.Clear();

                foreach (var childAbsNode in  abstractNode.Children)
                {
                    var childNode = treeNode.Nodes.Add(childAbsNode.Method.ToString());
                    childNode.Tag = childAbsNode;
                    childNode.Nodes.Add("<loading>");
                }
            }
        }

        private void ConstructResultTree()
        {
            this.resultTreeRoot = new ResultTreeNode(this.start);
            foreach (var path in this.filtered)
            {
                ResultTreeNode last = this.resultTreeRoot;
                foreach (var node in path)
                {
                    ResultTreeNode found;
                    if ((found = last.Children.FirstOrDefault(x => x.Method == node)) != null)
                    {
                        last = found;
                    }
                    else
                    {
                        var treeNode = new ResultTreeNode(node);
                        last.Children.Add(treeNode);
                        last = treeNode;
                    }
                }
            }
        }

        private void RenderTreeDynamic()
        {
            var rootNode = this.treeView1.Nodes.Add(this.resultTreeRoot.Method.ToString());
            rootNode.Tag = this.resultTreeRoot;
            rootNode.Nodes.Add("<loading>");
        }

        private void buttonShowGraph_Click(object sender, EventArgs e)
        {
            var gView = new ResultViewer(this.filtered, start, end);
            gView.Show();
        }
    }
}
