using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game.Utility.Extensions;

namespace WeepingSnake.Game.Tests.Utility.Extensions
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void TestRandom()
        {
            var dummyEnumerable = Enumerable.Range(0, 100);

            var randomValues = new HashSet<int>();
            var selectedRandomValuesCount = 0;
            
            do
            {
                var randomValue = dummyEnumerable.Random();
                randomValues.Add(randomValue);
                selectedRandomValuesCount++;

            } while (randomValues.Count < 2 || selectedRandomValuesCount < 50);


            Assert.True(randomValues.Count > 1, "For 50 random values from a list of 100 elements, at least two should be different.");
        }
    }
}
