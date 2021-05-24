using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.ConsoleClient.Navigation;
using Xunit;

namespace WeepingSnake.ConsoleClient.Tests.Navigation
{
    public class GamePageTests
    {
        [Fact]
        public void TestPointsInRectangle()
        {
            var actualA = GamePage.PointsInRectangle(-2, 0, 2, 1).OrderBy(p => p.Item1).ThenBy(p => p.Item2).ToList();
            var expectedA = new List<(int, int)>()
            {
                (-2,0),(-2,1), (-1,0), (-1,1), (0,0), (0,1), (1,0), (1,1), (2,0), (2,1)
            };
            Assert.Equal(expectedA, actualA);
        }
    }
}
