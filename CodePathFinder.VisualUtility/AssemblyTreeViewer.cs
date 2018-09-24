namespace CodePathFinder.VisualUtility
{
    using CodeAnalysis;
    using CodeAnalysis.AssemblyTree;
    using Mono.Cecil;
    using MonoCecilImpl;
    using MonoCecilImpl.CodeAnalysis;
    using MonoCecilImpl.CodeAnalysis.AssemblyTree;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class AssemblyTreeViewer : UserControl
    {
        /// <summary>
        /// Default path to domain assemblies
        /// </summary>
        private const string DefaultAsmPath =
            @".\assemblies";

        /// <summary>
        /// Loads assmblies for use in the app
        /// </summary>
        private IAssemblyLoader<AssemblyDefinition, MonoCecilMethod> assemblyLoader;

        /// <summary>
        /// Loads tree structure from assemblies
        /// </summary>
        private IAssemblyTreeLoader treeLoader;

        private IEnumerable<IList<AsmTreeNode>> foundNodeResults = null;
        private IEnumerator<IList<AsmTreeNode>> currentEnumerator = null;

        private string lastSearch = null;

        private bool isInitialized = false;

        /// <summary>
        /// Click handler
        /// </summary>
        public Action<MonoCecilMethod> Handler { get; set; } = (MonoCecilMethod x) => { };


        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyTreeViewer" /> class 
        /// </summary>
        public AssemblyTreeViewer()
        {
            InitializeComponent();
            ReloadAssemblies(DefaultAsmPath, new AssemblyMetadataOption[0]);

            this.treeAssemblyViewer.AfterSelect += TreeAssemblyViewer_AfterSelect;
            this.treeAssemblyViewer.BeforeExpand += TreeAssemblyViewer_BeforeExpand;
            this.treeAssemblyViewer.DrawMode = TreeViewDrawMode.OwnerDrawText;
            this.treeAssemblyViewer.DrawNode += treeView_DrawNode;


            this.textBoxSearch.KeyPress += TextBoxSearch_KeyPress;
            this.panelSearching.Visible = false;
        }

        private void treeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node == null) return;

            // if treeview's HideSelection property is "True", 
            // this will always returns "False" on unfocused treeview
            var selected = (e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected;
            var unfocused = !e.Node.TreeView.Focused;

            // we need to do owner drawing only on a selected node
            // and when the treeview is unfocused, else let the OS do it for us
            if (selected && unfocused)
            {
                var font = e.Node.NodeFont ?? e.Node.TreeView.Font;
                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, SystemColors.HighlightText, TextFormatFlags.GlyphOverhangPadding);
            }
            else
            {
                e.DrawDefault = true;
            }
        }

        private void TextBoxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                var currentSearch = this.textBoxSearch.Text;
                if (!this.isInitialized ||
                    string.IsNullOrWhiteSpace(currentSearch))
                {
                    return;
                }

                this.panelSearching.Visible = true;
                Task.Run(() =>
                {
                    var hasResults = false;
                    if (currentEnumerator != null)
                    {
                        hasResults = currentEnumerator.MoveNext();
                    }

                    if (!hasResults || currentSearch != lastSearch)
                    {
                        lastSearch = currentSearch;

                        this.foundNodeResults = this.treeLoader.FindPathToMethod(currentSearch);
                        currentEnumerator = this.foundNodeResults.GetEnumerator();
                        if (!currentEnumerator.MoveNext())
                        {
                            return null;
                        }
                    }

                    return currentEnumerator.Current;
                }).ContinueWith(t =>
                {
                    var path = t.Result;
                    this.Invoke(new Action(() => 
                    {
                        this.panelSearching.Visible = false;

                        if (path == null || path.Count == 0)
                        {
                            MessageBox.Show("No results found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        var asmNode = this.treeAssemblyViewer
                            .Nodes
                            .Cast<TreeNode>()
                            .First(x => x.Text == path[0].FullName);

                        asmNode.Expand();

                        var nsNode = asmNode
                            .Nodes
                            .Cast<TreeNode>()
                            .First(x => x.Text == path[1].FullName);

                        nsNode.Expand();

                        var typeNode = nsNode
                            .Nodes
                            .Cast<TreeNode>()
                            .First(x => x.Text == path[2].FullName);

                        typeNode.Expand();

                        var methodNode = typeNode
                            .Nodes
                            .Cast<TreeNode>()
                            .First(x => x.Text == path[3].FullName);

                        this.treeAssemblyViewer.HideSelection = false;
                        this.treeAssemblyViewer.SelectedNode = methodNode;
                        methodNode.EnsureVisible();
                    }));
                });
            }
        }

        public void ReloadAssemblies(string path, AssemblyMetadataOption[] options)
        {
            if (!Directory.Exists(path))
            {
                return;
            }

            this.treeAssemblyViewer.Nodes.Clear();

            this.assemblyLoader = new MonoCecilAssemblyLoader(path);
            this.treeLoader = new MonoCecilAssemblyTreeLoader(assemblyLoader, options);
            var rootNodes = this.treeLoader.LoadRootNodes();

            foreach (var rootNode in rootNodes)
            {
                var treeNode = this.treeAssemblyViewer.Nodes.Add(rootNode.FullName);
                treeNode.Tag = rootNode;
                treeNode.Nodes.Add("<loading>");
            }

            this.isInitialized = true;
        }

        private void TreeAssemblyViewer_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            AddChildrenForNode(e.Node);
        }

        private void TreeAssemblyViewer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is AsmTreeNode)
            {
                var asmTreeNode = e.Node.Tag as AsmTreeNode;
                if (asmTreeNode.NodeType == AssemblyTreeNodeType.Method)
                {
                    Handler((MonoCecilMethod)asmTreeNode.IlDefinition);
                }
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
        }

        private IEnumerable<TreeNode> SearchAllNodes(TreeNodeCollection nodes, string searchText)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag is AsmTreeNode asmTreeNode &&
                    asmTreeNode.NodeType == AssemblyTreeNodeType.Method &&
                    asmTreeNode.FullName.ToLower().Contains(searchText.ToLower()))
                {
                    yield return node;
                }

                IEnumerable<TreeNode> foundNodes;
                if (AddChildrenForNode(node) &&
                    (foundNodes = SearchAllNodes(node.Nodes, searchText)) != null)
                {
                    foreach (var foundNode in foundNodes)
                    {
                        yield return foundNode;
                    }
                }
            }
        }

        private bool AddChildrenForNode(TreeNode node)
        {
            var found = false;

            if (node.Nodes.Count == 1 && node.Nodes[0].Text == "<loading>")
            {
                node.Nodes.Clear();
                var asmNode = (AsmTreeNode)node.Tag;
                foreach (var child in this.treeLoader.LoadNodeChildrenInteractive(asmNode))
                {
                    found = true;
                    var addedNode = node.Nodes.Add(child.FullName);
                    addedNode.Tag = child;

                    if (child.NodeType != AssemblyTreeNodeType.Method)
                    {
                        addedNode.Nodes.Add("<loading>");
                    }
                }
            }
            else if (node.Nodes.Count >= 1 && node.Nodes[0].Text != "<loading>")
            {
                found = true;
            }

            return found;
        }
    }
}
