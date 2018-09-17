using CodePathFinder.CodeAnalysis;
using CodePathFinder.CodeAnalysis.PathFinding;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;
using Xunit;

namespace CodePathFinder.Test.CodeAnalysis.PathFinding
{
    public class CodePathTest
    {
        [Theory]
        [InlineData("OnlyMethod")]
        [InlineData("Method1", "Method2", "Method3", "Method4", "Method5")]
        public void AddLast_AddMultiple_ShouldHaveProperLength(params string[] parms)
        {
            // arrange
            var sut = new CodePath();
            var list = new List<Method>();
            foreach (var name in parms)
            {
                var mockedMethod = Mock.Create<Method>();
                Mock.Arrange(() => mockedMethod.FullName).Returns(name);
                list.Add(mockedMethod);
            }

            // act
            foreach (var method in list)
            {
                sut.AddLast(method);
            }

            Assert.Equal(parms.Length, sut.Count());

            // assert
            Assert.Equal(parms.Length, sut.Length);
        }

        [Theory]
        [InlineData("OnlyMethod")]
        [InlineData("Method1", "Method2", "Method3", "Method4", "Method5")]
        public void AddLast_AddMultiple_ShouldAddAllNodes(params string[] parms)
        {
            // arrange
            var sut = new CodePath();
            var list = new List<Method>();
            foreach (var name in parms)
            {
                var mockedMethod = Mock.Create<Method>();
                Mock.Arrange(() => mockedMethod.FullName).Returns(name);
                list.Add(mockedMethod);
            }

            // act
            foreach (var method in list)
            {
                sut.AddLast(method);
            }

            Assert.Equal(parms.Length, sut.Count());

            // assert
            foreach (var pair in sut.Zip(parms, (x, y) => new KeyValuePair<Method, String>(x, y)))
            {
                Assert.Equal(pair.Value, pair.Key.FullName);
            }
        }

        [Theory]
        [InlineData("OnlyMethod")]
        [InlineData("Method1", "Method2", "Method3", "Method4", "Method5")]
        public void AddLastNew_AddMultiple_ShouldAddAllNodes(params string[] parms)
        {
            // arrange
            var sut = new CodePath();
            var list = new List<Method>();
            foreach (var name in parms)
            {
                var mockedMethod = Mock.Create<Method>();
                Mock.Arrange(() => mockedMethod.FullName).Returns(name);
                list.Add(mockedMethod);
            }

            // act
            foreach (var method in list)
            {
                sut = sut.AddLastNew(method);
            }


            // assert
            Assert.Equal(parms.Length, sut.Count());
            Assert.Equal(parms.Length, sut.Length);
            foreach (var pair in sut.Zip(parms, (x, y) => new KeyValuePair<Method, String>(x, y)))
            {
                Assert.Equal(pair.Value, pair.Key.FullName);
            }
        }

        [Theory]
        [InlineData("OnlyMethod")]
        [InlineData("Method1", "Method2", "Method3", "Method4", "Method5")]
        public void AddLastNew_AddMultipleTwice_ShouldMaintainOld(params string[] parms)
        {
            // arrange
            var sut = new CodePath();
            var list = new List<Method>();
            foreach (var name in parms)
            {
                var mockedMethod = Mock.Create<Method>();
                Mock.Arrange(() => mockedMethod.FullName).Returns(name);
                list.Add(mockedMethod);
            }

            foreach (var method in list)
            {
                sut.AddLast(method);
            }

            // act
            var newOne = sut;
            foreach (var method in list)
            {
                newOne = newOne.AddLastNew(method);
            }

            newOne = sut;
            foreach (var method in list)
            {
                newOne = newOne.AddLastNew(method);
            }

            // assert
            Assert.Equal(parms.Length, sut.Count());
            Assert.Equal(parms.Length, sut.Length);
            foreach (var pair in sut.Zip(parms, (x, y) => new KeyValuePair<Method, String>(x, y)))
            {
                Assert.Equal(pair.Value, pair.Key.FullName);
            }
        }

        [Theory]
        [InlineData("OnlyMethod")]
        [InlineData("Method1", "Method2", "Method3", "Method4", "Method5")]
        public void AddLastNew_AddMultiple_ShouldMaintainOld(params string[] parms)
        {
            // arrange
            var sut = new CodePath();
            var list = new List<Method>();
            foreach (var name in parms)
            {
                var mockedMethod = Mock.Create<Method>();
                Mock.Arrange(() => mockedMethod.FullName).Returns(name);
                list.Add(mockedMethod);
            }

            foreach (var method in list)
            {
               sut.AddLast(method);
            }

            // act
            var newOne = sut;
            foreach (var method in list)
            {
                newOne = newOne.AddLastNew(method);
            }

            // assert
            Assert.Equal(parms.Length, sut.Count());
            Assert.Equal(parms.Length, sut.Length);
            foreach (var pair in sut.Zip(parms, (x, y) => new KeyValuePair<Method, String>(x, y)))
            {
                Assert.Equal(pair.Value, pair.Key.FullName);
            }
        }

        [Theory]
        [InlineData("Method1", "Method2", "Method3", "Method4", "Method5")]
        public void Concat_ShouldConcatFullChain(params string[] parms)
        {
            // arrange
            var extraPath = new CodePath();
            var sut = new CodePath();
            foreach (var name in parms)
            {
                var mockedMethod = Mock.Create<Method>();
                Mock.Arrange(() => mockedMethod.FullName).Returns(name);
                extraPath.AddLast(mockedMethod);
                sut.AddLast(mockedMethod);
            }

            // act
            sut.Concat(extraPath);

            // assert
            Assert.Equal(parms.Length * 2, sut.Count());
            Assert.Equal(parms.Length * 2, sut.Length);

            var index = 0;
            foreach (var method in sut)
            {
                Assert.Equal(parms[index % parms.Length], method.FullName);
                index++;
            }
        }

        [Theory]
        [InlineData("Method1", "Method2", "Method3", "Method4", "Method5")]
        public void ConcatNew_ShouldConcatFullChain(params string[] parms)
        {
            // arrange
            var extraPath = new CodePath();
            var sut = new CodePath();
            foreach (var name in parms)
            {
                var mockedMethod = Mock.Create<Method>();
                Mock.Arrange(() => mockedMethod.FullName).Returns(name);
                extraPath.AddLast(mockedMethod);
                sut.AddLast(mockedMethod);
            }

            // act
            var newSut = sut.ConcatNew(extraPath);

            // assert
            Assert.Equal(parms.Length * 2, newSut.Count());
            Assert.Equal(parms.Length * 2, newSut.Length);
            var index = 0;
            foreach (var method in newSut)
            {
                Assert.Equal(parms[index % parms.Length], method.FullName);
                index++;
            }
        }

        [Theory]
        [InlineData(
            new string[] { "Method1", "Method2", "Method3", "Method4", "Method5" },
            new string[] { "noman", "goman", "yoman", "soman" })]
        public void ConcatNew_ConcatPartial_ShouldConcatFullChain(params string[][] parms)
        {
            // arrange
            var extraPath = new CodePath();
            var sut = new CodePath();
            foreach (var name in parms[0])
            {
                var mockedMethod = Mock.Create<Method>();
                Mock.Arrange(() => mockedMethod.FullName).Returns(name);
                sut.AddLast(mockedMethod);
            }

            foreach (var name in parms[1])
            {
                var mockedMethod = Mock.Create<Method>();
                Mock.Arrange(() => mockedMethod.FullName).Returns(name);
                extraPath.AddLast(mockedMethod);
            }

            // form expected final path
            var expected = parms[0].Concat(parms[1].Skip(2));

            // act
            var newSut = sut.ConcatNew(extraPath.GetSubPaths().ToList()[1]);

            // assert
            Assert.Equal(parms[0].Length + parms[1].Length - 2, newSut.Count());
            Assert.Equal(parms[0].Length + parms[1].Length - 2, newSut.Length);
            Assert.Equal(expected, newSut.Select(x => x.FullName));
        }

        // NOTE: Currently CodePath doesn't support any repeated elements
        //[Theory]
        //[InlineData("Method1", "Method2", "Method3", "Method4", "Method5")]
        //public void ConcatNew_ConcatSelf_HasCorrectProperties(params string[] parms)
        //{
        //    // arrange
        //    var sut = new CodePath();
        //    foreach (var name in parms)
        //    {
        //        var mockedMethod = Mock.Create<Method>();
        //        Mock.Arrange(() => mockedMethod.FullName).Returns(name);
        //        sut.AddLast(mockedMethod);
        //    }

        //    // act
        //    var actual = sut.ConcatNew(sut.GetSubPaths().ToList()[1]);

        //    // assert
        //    var actualCount = actual.Count();
        //    Assert.Equal(parms.Length * 2 - 2, actualCount);
        //    Assert.Equal(parms.Length * 2 - 2, actual.Length);
        //}

        [Theory]
        [InlineData("Method1", "Method2", "Method3", "Method4", "Method5")]
        public void ConcatNew_ShouldMaintainOldChain(params string[] parms)
        {
            // arrange
            var extraPath = new CodePath();
            var sut = new CodePath();
            foreach (var name in parms)
            {
                var mockedMethod = Mock.Create<Method>();
                Mock.Arrange(() => mockedMethod.FullName).Returns(name);
                extraPath.AddLast(mockedMethod);
                sut.AddLast(mockedMethod);
            }

            // act
            sut.ConcatNew(extraPath);

            // assert
            var actualCount = sut.Count();
            Assert.Equal(parms.Length, actualCount);
            Assert.Equal(parms.Length, sut.Length);

            var index = 0;
            foreach (var method in sut)
            {
                Assert.Equal(parms[index++], method.FullName);
            }
        }

        [Theory]
        [InlineData("Method1", "Method2", "Method3", "Method4", "Method5", "Method6")]
        public void GetSubPaths_SubPathsHaveValidProperties(params string[] parms)
        {
            // arrange
            var sut = new CodePath();
            foreach (var name in parms)
            {
                var mockedMethod = Mock.Create<Method>();
                Mock.Arrange(() => mockedMethod.FullName).Returns(name);
                sut.AddLast(mockedMethod);
            }

            // act
            var subPathsList = sut.GetSubPaths().ToList();

            // assert
            for (int i = 0; i < subPathsList.Count; i++)
            {
                var currentPath = subPathsList[i];
                Assert.Equal(sut.Length - i - 1, currentPath.Length);
                Assert.Equal(sut.ElementAt(i + 1), currentPath.FirstMethod);
                Assert.Equal(sut.LastMethod, currentPath.LastMethod);
            }
        }

        [Theory]
        [InlineData("Method1", "Method2", "Method3", "Method4", "Method5")]
        public void Equals_EqualPaths_ShouldReturnTrue(params string[] parms)
        {
            // arrange
            var extraPath = new CodePath();
            var sut1 = new CodePath();
            foreach (var name in parms)
            {
                var mockedMethod = Mock.Create<Method>();
                Mock.Arrange(() => mockedMethod.FullName).Returns(name);
                sut1.AddLast(mockedMethod);
            }

            var sut2 = new CodePath();
            foreach (var name in parms)
            {
                var mockedMethod = Mock.Create<Method>();
                Mock.Arrange(() => mockedMethod.FullName).Returns(name);
                sut2.AddLast(mockedMethod);
            }

            // act
            var actual = sut1.Equals(sut2);

            // assert
            Assert.Equal(parms.Length, sut1.Length);
            Assert.Equal(parms.Length, sut2.Length);
            Assert.True(actual);
        }
    }
}
