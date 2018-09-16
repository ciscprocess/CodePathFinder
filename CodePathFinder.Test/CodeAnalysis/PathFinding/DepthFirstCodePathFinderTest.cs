namespace CodePathFinder.Test.CodeAnalysis.PathFinding
{
    using CodePathFinder.CodeAnalysis;
    using CodePathFinder.CodeAnalysis.PathFinding;
    using GraphSerialization;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using TestGraphImpl;
    using Xunit;

    public class DepthFirstCodePathFinderTest
    {
        [Theory]
        [MemberData(nameof(TestData))]
        public void FindPathsBetweenMethods_FindsAllPaths(IAssemblyGraphAnalyzer graphAnalyzer, 
            Method start, 
            Method end,
            IList<CodePath> expectedList)
        {
            // arrange
            var sut = new DepthFirstCodePathFinder(graphAnalyzer, 1);

            // act
            var actualList = sut.FindPathsBetweenMethods(start, end);

            // assert 
            Assert.Equal(expectedList.Count, actualList.Count);

            foreach (var path in actualList)
            {
                expectedList.Remove(path);
            }

            Assert.Empty(expectedList);
        }

        public static IEnumerable<object[]> TestData
        {
            get
            {
                // test graph set 1
                foreach (var item in ReadGraphParameters("TestGraph1"))
                {
                    yield return item;
                }

                // test graph set 2
                foreach (var item in ReadGraphParameters("TestGraph2"))
                {
                    yield return item;
                }

                // test graph set 3
                foreach (var item in ReadGraphParameters("TestGraph3"))
                {
                    yield return item;
                }

                // test graph set 4
                foreach (var item in ReadGraphParameters("TestGraph4"))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<object[]> ReadGraphParameters(string graphName)
        {
            var graphAnalyzer = new TestDataAssemblyGraphAnalyzer($"Data\\TestGraphs\\{graphName}.Definition.xml");
            var serializer = new XmlSerializer(typeof(GraphResultsModel));

            GraphResultsModel results;
            using (var fs = new FileStream($"Data\\TestGraphs\\{graphName}.Results.xml", FileMode.Open, FileAccess.Read))
            {
                results = (GraphResultsModel)serializer.Deserialize(fs);
            }

            foreach (var search in results.Searches)
            {
                var codePaths = search
                    .CodePaths
                    .Select(x => x.ToCodePath())
                    .ToList();

                yield return new object[] {
                    graphAnalyzer,
                    new MockMethod(search.Start),
                    new MockMethod(search.End),
                    codePaths };
            }
        }
    }
}
