using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game.Geometry;
using WeepingSnake.Game.Player;
using Xunit;

namespace WeepingSnake.Game.Tests.Player
{
    public class PlayerOrientationTests
    {
        [Fact]
        public void TestApplyAndMove()
        {
            // Arrange
            var position = new GameCoordinate(2, 2, 1);
            var direction = new PlayerDirection(0, 1);
            var orientation = new PlayerOrientation(position, direction);

            // Act
            var newOrientation = orientation.ApplyAndMove(PlayerAction.Action.CHANGE_NOTHING);

            // Assert
            Assert.True(orientation != newOrientation);
            Assert.Equal(orientation.Direction, newOrientation.Direction);
            Assert.Equal(2, newOrientation.Position.X);
            Assert.Equal(3, newOrientation.Position.Y);
        }

        [Fact]
        public void TestStringRepresentation()
        {
            // Arrange
            var position = new GameCoordinate(2, 2, 1);
            var direction = new PlayerDirection(0, 1);
            var orientation = new PlayerOrientation(position, direction);

            // Act
            var representation = $"{orientation}";

            // Assert
            Assert.Equal($"({2:F2}|{2:F2}|{1:F2}) | (0|1)", representation);
        }
    }
}
