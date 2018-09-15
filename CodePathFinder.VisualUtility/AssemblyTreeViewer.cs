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
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security.Cryptography;
    using System.Text;
    using System.Windows.Forms;

    public partial class AssemblyTreeViewer : UserControl
    {
        /// <summary>
        /// Default path to domain assemblies
        /// </summary>
        private const string DefaultAsmPath =
            @".\assemblies";

        /// <summary>
        /// Precomputed tree nodes
        /// </summary>
        private static List<TreeNode> AssemblyNodes = new List<TreeNode>();

        /// <summary>
        /// Loads assmblies for use in the app
        /// </summary>
        private IAssemblyLoader<AssemblyDefinition, MonoCecilMethod> assemblyLoader;

        /// <summary>
        /// Loads tree structure from assemblies
        /// </summary>
        private IAssemblyTreeLoader treeLoader;

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
        }

        private void TreeAssemblyViewer_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            var node = e.Node;
            if (node.Nodes.Count == 1 && node.Nodes[0].Text == "<loading>")
            {
                node.Nodes.Clear();
                var asmNode = (AsmTreeNode)node.Tag;
                foreach (var child in this.treeLoader.LoadNodeChildrenInteractive(asmNode))
                {
                    var addedNode = node.Nodes.Add(child.FullName);
                    addedNode.Tag = child;

                    if (child.NodeType != AssemblyTreeNodeType.Method)
                    {
                        addedNode.Nodes.Add("<loading>");
                    }
                }
            }
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
    }
}
