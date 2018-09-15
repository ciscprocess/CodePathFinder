using CodePathFinder.CodeAnalysis;
using CodePathFinder.CodeAnalysis.AssemblyTree;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePathFinder.MonoCecilImpl.CodeAnalysis.AssemblyTree
{
    public class MonoCecilAssemblyTreeLoader : IAssemblyTreeLoader
    {
        private readonly IAssemblyLoader<AssemblyDefinition, MonoCecilMethod> assemblyLoader;
        private readonly AssemblyMetadataOption[] options;
        private readonly IList<string> namespaceCache;

        public MonoCecilAssemblyTreeLoader(
            IAssemblyLoader<AssemblyDefinition, MonoCecilMethod> assemblyLoader,
            AssemblyMetadataOption[] options)
        {
            this.assemblyLoader = assemblyLoader;
            this.options = options;
            this.namespaceCache = new List<string>();
        }

        public IEnumerable<AsmTreeNode> LoadNodeChildrenInteractive(AsmTreeNode node)
        {
            if (node.NodeType == AssemblyTreeNodeType.Assembly)
            {
                var assembly = (AssemblyDefinition)node.IlDefinition;
                var set = new HashSet<string>();
                return assembly
                    .MainModule
                    .GetTypes()
                    .OrderBy(x => x.Namespace)
                    .Select(x => new AsmTreeNode(this, assembly)
                    {
                        NodeType = AssemblyTreeNodeType.Namespace,
                        FullName = x.Namespace
                    })
                    .Where(x => 
                    {
                        var inSet = set.Contains(x.FullName);
                        set.Add(x.FullName);
                        return !inSet && !string.IsNullOrWhiteSpace(x.FullName);
                    });
            }
            else if (node.NodeType == AssemblyTreeNodeType.Namespace)
            {
                return WalkNamespaceChildren(node);
            }
            else if (node.NodeType == AssemblyTreeNodeType.Type)
            {
                return WalkTypeChildren(node);
            }
            else
            {
                return Enumerable.Empty<AsmTreeNode>();
            }
        }

        private IEnumerable<AsmTreeNode> WalkTypeChildren(AsmTreeNode node)
        {
            var type = (TypeDefinition)node.IlDefinition;
            foreach (var method in type.Methods)
            {
                var wrappedMethod = new MonoCecilMethod(method);
                yield return new AsmTreeNode(this, wrappedMethod)
                {
                    FullName = wrappedMethod.ToString(),
                    NodeType = AssemblyTreeNodeType.Method
                };
            }
        }

        private IEnumerable<AsmTreeNode> WalkNamespaceChildren(AsmTreeNode node)
        {
            var assembly = (AssemblyDefinition)node.IlDefinition;
            foreach (var type in assembly.MainModule.Types)
            {
                if (type.Namespace == node.FullName)
                {
                    yield return new AsmTreeNode(this, type)
                    {
                        FullName = type.FullName,
                        NodeType = AssemblyTreeNodeType.Type
                    };
                }
            }
        }

        public IList<AsmTreeNode> LoadRootNodes()
        {
            var assemblies = this.assemblyLoader.LoadDomainAssemblies(options);
            var list = new List<AsmTreeNode>();

            foreach (var assembly in assemblies)
            {
                list.Add(new AsmTreeNode(this, assembly)
                {
                    FullName = assembly.MainModule.Name,
                    NodeType = AssemblyTreeNodeType.Assembly
                });
            }

            return list;
        }
    }
}
