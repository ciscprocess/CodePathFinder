using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CodePathFinder.Test.TestGraphImpl
{
    [XmlRoot(ElementName = "graphResults")]
    public class GraphResultsModel
    {
        [XmlArray("searches")]
        [XmlArrayItem("search", typeof(GraphSearchResult))]
        public GraphSearchResult[] Searches { get; set; }
    }
}
