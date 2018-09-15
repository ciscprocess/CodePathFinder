namespace CodePathFinder.Test.TestGraphImpl
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class GraphSearchResult
    {
        /// <summary>
        /// Start node ID
        /// </summary>
        [XmlAttribute(AttributeName = "start", DataType = "int")]
        public int Start { get; set; }

        /// <summary>
        /// End node ID
        /// </summary>
        [XmlAttribute(AttributeName = "end", DataType = "int")]
        public int End { get; set; }

        [XmlArray("codePaths")]
        [XmlArrayItem("codePath", typeof(GraphCodePathResult))]
        public GraphCodePathResult[] CodePaths { get; set; }
    }
}
