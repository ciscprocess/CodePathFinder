using System.Collections.Generic;

namespace CodePathFinder.CodeAnalysis.AssemblyTree
{
    public interface IAssemblyTreeLoader
    {
        IList<AsmTreeNode> LoadRootNodes();
        IEnumerable<AsmTreeNode> LoadNodeChildrenInteractive(AsmTreeNode node);
        IEnumerable<IList<AsmTreeNode>> FindPathToMethod(string methodNameContains);
    }
}
