using CodePathFinder.CodeAnalysis.PathFinding;
using CodePathFinder.Test.CodeAnalysis.PathFinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CodePathFinder.Test.TestGraphImpl
{
    [Serializable]
    public class GraphCodePathResult
    {
        /// <summary>
        /// End node ID
        /// </summary>
        [XmlAttribute(AttributeName = "ids", DataType = "string")]
        public string Ids { get; set; }

        public CodePath ToCodePath()
        {
            var path = new CodePath();
            var idList = this.Ids.Split(',');

            foreach (var id in idList)
            {
                path.AddLast(new MockMethod(int.Parse(id)));
            }

            return path;
        }
    }
}
