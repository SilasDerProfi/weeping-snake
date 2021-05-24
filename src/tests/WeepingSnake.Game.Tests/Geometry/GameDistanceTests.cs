using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game.Geometry;
using WeepingSnake.Game.Player;
using WeepingSnake.Game.Tests.Game;
using WeepingSnake.Game.Tests.Player;
using Xunit;

namespace WeepingSnake.Game.Tests.Geometry
{
    public class GameDistanceTests
    {
        private MockPlayer CreateMockPlayer()
        {
            var player = new MockPlayer()
            {
                IsAlive = true,
                AssignedGame = new MockGame()
                {
                    GameBoard = new MockCoordinateSystem()
                },
                Actions = new Dictionary<string, Action>
                {
                    { "Die",  () => _ = "" }
                }
            };

            return player;
        }


        [Fact]
        public void TestCreateCenter()
        {
            // Arrange
            var direction = new PlayerDirection(4, 0);
            var player = CreateMockPlayer();

            // Act
            var gameDistance = new GameDistance(2, 2, direction, player);

            // Assert
            Assert.Equal(3, gameDistance.StartX);
            Assert.Equal(2, gameDistance.StartY);
            Assert.Equal(6, gameDistance.EndX);
            Assert.Equal(2, gameDistance.EndY);
        }

        [Fact]
        public void TestCreateTopLeft()
        {
            // Arrange
            var direction = new PlayerDirection(-4, 0);
            var player = CreateMockPlayer();

            // Act
            var gameDistance = new GameDistance(-10, 50, direction, player);

            // Assert
            Assert.Equal(-11, gameDistance.StartX);
            Assert.Equal(50, gameDistance.StartY);
            Assert.Equal(0, gameDistance.EndX);
            Assert.Equal(50, gameDistance.EndY);
        }

        [Fact]
        public void TestCreateBottomRight()
        {
            // Arrange
            var direction = new PlayerDirection(4, 0);
            var player = CreateMockPlayer();

            // Act
            var gameDistance = new GameDistance(50, -10, direction, player);

            // Assert
            Assert.Equal(51, gameDistance.StartX);
            Assert.Equal(-10, gameDistance.StartY);
            Assert.Equal(9, gameDistance.EndX);
            Assert.Equal(-10, gameDistance.EndY);
        }

        [Fact]
        public void TestCreateTopCenter()
        {
            // Arrange
            var direction = new PlayerDirection(0, 4);
            var player = CreateMockPlayer();

            // Act
            var gameDistance = new GameDistance(5, 20, direction, player);

            // Assert
            Assert.Equal(5, gameDistance.StartX);
            Assert.Equal(21, gameDistance.StartY);
            Assert.Equal(5, gameDistance.EndX);
            Assert.Equal(9, gameDistance.EndY);
        }

        [Fact]
        public void TestCreateBottomCenter()
        {
            // Arrange
            var direction = new PlayerDirection(0, -4);
            var player = CreateMockPlayer();

            // Act
            var gameDistance = new GameDistance(5, -1, direction, player);

            // Assert
            Assert.Equal(5, gameDistance.StartX);
            Assert.Equal(-2, gameDistance.StartY);
            Assert.Equal(5, gameDistance.EndX);
            Assert.Equal(0, gameDistance.EndY);
        }
    }
}
