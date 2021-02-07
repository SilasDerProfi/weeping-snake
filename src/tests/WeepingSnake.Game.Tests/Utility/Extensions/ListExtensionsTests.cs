using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game.Utility.Extensions;
using Xunit;

namespace WeepingSnake.Game.Tests.Utility.Extensions
{
    public class ListExtensionsTests
    {
        private static List<string> CreateDummyList() => new List<string>()
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
    }
}
