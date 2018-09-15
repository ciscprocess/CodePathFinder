using CodePathFinder.CodeAnalysis;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph.Serialization;
using System.Xml;
using CodePathFinder.Test.CodeAnalysis.PathFinding;

namespace CodePathFinder.Test.GraphSerialization
{
    public class TestDataAssemblyGraphAnalyzer : IAssemblyGraphAnalyzer
    {
        /// <summary>
        /// Directed graph with integer-ID edges
        /// </summary>
        private BidirectionalGraph<int, Edge<int>> graph;

        /// <summary>
        /// GraphML serializer
        /// </summary>
        private GraphMLDeserializer<int, Edge<int>, BidirectionalGraph<int, Edge<int>>> deserializer;

        public TestDataAssemblyGraphAnalyzer(string testDataFile)
        {
            this.graph = new BidirectionalGraph<int, Edge<int>>();
            this.deserializer = new GraphMLDeserializer<int, Edge<int>, BidirectionalGraph<int, Edge<int>>>();
            using (var xmlReader = XmlReader.Create(testDataFile))
            {
                this.deserializer.Deserialize(xmlReader, 
                    this.graph, 
                    id => int.Parse(id), 
                    (int src, int dst, string id) => new Edge<int>(src, dst));
            }
        }

        public IList<Method> GetMethodNeighbors(Method method)
        {
            var outEdges = this.graph.OutEdges(int.Parse(method.FullName));
            var list = new List<Method>();

            foreach (var edge in outEdges)
            {
                list.Add(new MockMethod(edge.Target));
            }

            return list;
        }
    }
}
