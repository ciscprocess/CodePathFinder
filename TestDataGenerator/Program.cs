using CodePathFinder.CodeAnalysis.PathFinding;
using CodePathFinder.Test.CodeAnalysis.PathFinding;
using CodePathFinder.Test.GraphSerialization;
using CodePathFinder.Test.TestGraphImpl;
using QuickGraph;
using QuickGraph.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace TestDataGenerator
{
    class TestDataProgram
    {
        static void Main(string[] args)
        {
            int nodesPerSide = 12;
            var graph = new BidirectionalGraph<int, Edge<int>>();

            // add start
            graph.AddVertex(0);

            // add odd vertices
            for (int i = 0; i < nodesPerSide; i++)
            {
                graph.AddVertex(i * 2 + 1);
            }

            // add even vertices
            for (int i = 1; i < nodesPerSide + 1; i++)
            {
                graph.AddVertex(i * 2);
            }

            // add end
            graph.AddVertex(nodesPerSide * 2 + 1);

            // conditionally connect odd side
            for (int i = 0; i < nodesPerSide; i++)
            {
                graph.AddEdge(new Edge<int>(0, i * 2 + 1));

                for (int j = i + 1; j < nodesPerSide; j++)
                {
                    graph.AddEdge(new Edge<int>(i * 2 + 1, j * 2 + 1));
                }

                // add edge to final
                graph.AddEdge(new Edge<int>(i * 2 + 1, nodesPerSide * 2 + 1));
            }

            // conditionally connect even side
            for (int i = 1; i < nodesPerSide + 1; i++)
            {
                graph.AddEdge(new Edge<int>(0, i * 2));

                for (int j = i + 1; j < nodesPerSide + 1; j++)
                {
                    graph.AddEdge(new Edge<int>(i * 2, j * 2));
                }

                // add edge to final
                graph.AddEdge(new Edge<int>(i * 2, nodesPerSide * 2 + 1));
            }

            // save graph
            var graphSerializer = new GraphMLSerializer<int, Edge<int>, BidirectionalGraph<int, Edge<int>>>();

            using (var xmlWriter = XmlWriter.Create("graph.xml"))
            {
                graphSerializer.Serialize(xmlWriter, graph, x => x.ToString(), x => x.ToString());
            }

            // generate odd result set
            var datuh = GetForwardConnectedPresenceVectors(nodesPerSide);

            var expectedPaths = new List<List<int>>();
            foreach (var vector in datuh)
            {
                var path = new List<int>();
                var pathEven = new List<int>();
                path.Add(0);
                pathEven.Add(0);

                var index2 = 0;
                foreach (var shouldAdd in vector)
                {
                    if (shouldAdd)
                    {
                        path.Add(index2 * 2 + 1);
                        pathEven.Add((index2 + 1) * 2);
                    }

                    index2++;
                }

                path.Add(nodesPerSide * 2 + 1);
                pathEven.Add(nodesPerSide * 2 + 1);
                expectedPaths.Add(path);
                if (pathEven.Count > 2)
                {
                    expectedPaths.Add(pathEven);
                }
            }

            var search = new GraphSearchResult()
            {
                Start = 0,
                End = nodesPerSide * 2 + 1,
                CodePaths = new GraphCodePathResult[expectedPaths.Count]
            };

            var index = 0;
            foreach (var path in expectedPaths)
            {
                var codePathResult = new GraphCodePathResult();
                var ids = string.Join(",", path);
                codePathResult.Ids = ids;
                search.CodePaths[index] = codePathResult;
                index++;
            }

            var model = new GraphResultsModel()
            {
                Searches = new GraphSearchResult[] { search }
            };

            var serializer = new XmlSerializer(typeof(GraphResultsModel));
            using (var fs = new FileStream("results.xml", FileMode.Create, FileAccess.ReadWrite))
            {
                serializer.Serialize(fs, model);
            }

            Console.ReadKey();
        }

        public static List<bool[]> GetForwardConnectedPresenceVectors(int size)
        {
            var startVector = Enumerable.Range(0, size)
                .Select(x => false)
                .ToArray();

            var stack = new Stack<Tuple<bool[], int>>();
            stack.Push(Tuple.Create(startVector, 0));

            var outVectors = new List<bool[]>();
            while (stack.Count > 0)
            {
                var tuple = stack.Pop();
                var currentVector = tuple.Item1;
                var currentIndex = tuple.Item2;

                if (currentIndex == size)
                {
                    outVectors.Add(currentVector);
                    continue;
                }

                var n1 = currentVector.ToArray();
                n1[currentIndex] = true;

                var n2 = currentVector.ToArray();

                stack.Push(Tuple.Create(n1, currentIndex + 1));
                stack.Push(Tuple.Create(n2, currentIndex + 1));
            }

            return outVectors;
        }
    }
}
