using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePathFinder.CodeAnalysis.AssemblyTree
{
    public class AsmTreeNode
    {
        protected readonly IAssemblyTreeLoader generator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsmTreeNode" /> class
        /// </summary>
        /// <param name="generator"></param>
        public AsmTreeNode(IAssemblyTreeLoader generator, 
            object ilDefinition)
        {
            this.generator = generator;
            this.IlDefinition = ilDefinition;
        }

        public string FullName { get; set; }
        public AssemblyTreeNodeType NodeType { get; set; }
        public object IlDefinition { get; private set; }
        public IEnumerable<AsmTreeNode> ChildNodes 
        {
            get
            {
                return this.generator.LoadNodeChildrenInteractive(this);
            }
        }
    }
}
