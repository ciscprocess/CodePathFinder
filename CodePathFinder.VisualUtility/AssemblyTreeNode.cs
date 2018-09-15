using Mono.Cecil;
using System;
using System.Windows.Forms;

namespace CodePathFinder.VisualUtility
{
    [Serializable]
    public class AssemblyTreeNode : TreeNode
    {
        public AssemblyTreeNode() : base()
        {

        }

        public AssemblyTreeNode(string name, string definitionName) : base(name)
        {
            this.DefinitionName = definitionName;
        }

        public string DefinitionName { get; set; }

        public bool IsMethod { get; set; }

        public override object Clone()
        {
            var cloned = (AssemblyTreeNode)base.Clone();
            cloned.DefinitionName = this.DefinitionName;
            cloned.IsMethod = this.IsMethod;
            return cloned;
        }
    }
}
