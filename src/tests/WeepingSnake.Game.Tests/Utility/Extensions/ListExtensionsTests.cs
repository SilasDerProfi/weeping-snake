using System;
using System.Collections.Generic;
using System.Linq;
using WeepingSnake.Game.Utility.Extensions;
using Xunit;

namespace WeepingSnake.Game.Tests.Utility.Extensions
{
    public class ListExtensionsTests
    {
        private static List<string> CreateDummyList() => new()
        {
            "This",
            "is",
            "a",
            "dummy",
            "list"
        };

        [Fact]
        public void TestGetInfiniteEnumeratorSublist()
        {
            var dummyList = CreateDummyList();

            var subList = dummyList.GetInfiniteEnumerator().Take(3).ToList();

            Assert.Equal(3, subList.Count);
            Assert.Same(dummyList[0], subList[0]);
            Assert.Same(dummyList[1], subList[1]);
            Assert.Same(dummyList[2], subList[2]);
        }

        [Fact]
        public void TestGetInfiniteEnumeratorDoublelist()
        {
            var dummyList = CreateDummyList();

            var doubleList = dummyList.GetInfiniteEnumerator().Take(10).ToList();

            Assert.Equal(10, doubleList.Count);

            var doubleListFirstHalf = doubleList.GetRange(0, 5);
            var doubleListSecondHalf = doubleList.GetRange(5, 5);

            Assert.Equal(dummyList, doubleListFirstHalf);
            Assert.Equal(dummyList, doubleListSecondHalf);
        }

        [Fact]
        public void TestAddAndReturn()
        {
            var dummyList = CreateDummyList();
            var dummyListCount = dummyList.Count;

            var newListItem = "!";
            var addAndReturnResult = dummyList.AddAndReturn(newListItem);

            Assert.Equal(newListItem, addAndReturnResult);
            Assert.Equal(dummyListCount + 1, dummyList.Count);
        }

        [Fact]
        public void TestGetOrCreate()
        {
            var dummyList = CreateDummyList();
            var dummyListCount = dummyList.Count;

            const string generatedItem = "generated";

            dummyList.GetOrCreate(dummyListCount + 3, () => generatedItem);

            var referenceDummyList = CreateDummyList();

            Assert.Equal(referenceDummyList[0], dummyList[0]);
            Assert.Equal(generatedItem, dummyList[^1]);
            Assert.Equal(generatedItem, dummyList[^2]);
            Assert.Equal(generatedItem, dummyList[^3]);
            Assert.Equal(generatedItem, dummyList[^4]);
            Assert.Equal(referenceDummyList[^1], dummyList[^5]);
        }
    }
}
