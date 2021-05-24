using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game.Geometry;
using WeepingSnake.Game.Player;
using Xunit;

namespace WeepingSnake.Game.Tests.Geometry
{
    public class GameCoordinateTests
    {
        [Fact]
        public void TestVonNeumannNeighborhood()
        {
            // Arrange
            var coordinate = new GameCoordinate(4, 4, 1);

            // Act
            var neighborhood = coordinate.VonNeumannNeighborhood();

            // Assert
            Assert.Equal(4, neighborhood.Count());
            Assert.Contains((3, 4), neighborhood);
            Assert.Contains((4, 5), neighborhood);
            Assert.Contains((5, 4), neighborhood);
            Assert.Contains((4, 3), neighborhood);
        }

        [Fact]
        public void TestMooreNeighborhood()
        {
            // Arrange
            var coordinate = new GameCoordinate(4, 4, 1);

            // Act
            var neighborhood = coordinate.MooreNeighborhood();

            // Assert
            Assert.Equal(8, neighborhood.Count());
            Assert.Contains((3, 4), neighborhood);
            Assert.Contains((3, 5), neighborhood);
            Assert.Contains((4, 5), neighborhood);
            Assert.Contains((5, 5), neighborhood);
            Assert.Contains((5, 4), neighborhood);
            Assert.Contains((5, 3), neighborhood);
            Assert.Contains((4, 3), neighborhood);
            Assert.Contains((3, 3), neighborhood);
        }

        [Fact]
        public void TestTranslate()
        {
            // Arrange
            var coordinate = new GameCoordinate(4, 4, 1);
            var direction = new PlayerDirection(2, 0);

            // Act
            var translatedCoordinate = coordinate.Translate(direction);

            // Assert
            Assert.True(coordinate != translatedCoordinate);
            Assert.Equal(6, translatedCoordinate.X);
            Assert.Equal(4, translatedCoordinate.Y);
        }
    }
}
