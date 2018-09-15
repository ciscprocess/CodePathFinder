using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePathFinder.CodeAnalysis.AssemblyTree
{
    public interface IAssemblyTreeLoader
    {
        IList<AsmTreeNode> LoadRootNodes();
        IEnumerable<AsmTreeNode> LoadNodeChildrenInteractive(AsmTreeNode node);
    }
}
