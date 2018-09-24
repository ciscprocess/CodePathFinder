using CodePathFinder.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePathFinder.VisualUtility
{
    class ResultTreeNode
    {
        public ResultTreeNode(Method method)
        {
            this.Method = method;
            this.Children = new List<ResultTreeNode>();
        }

        public Method Method { get; private set; }
        public string DisplayName => Method.ToString();
        public IList<ResultTreeNode> Children { get; private set; }
    }
}
