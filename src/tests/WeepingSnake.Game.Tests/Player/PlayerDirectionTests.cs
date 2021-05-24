using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game.Player;
using Xunit;

namespace WeepingSnake.Game.Tests.Player
{
    public class PlayerDirectionTests
    {
        [Fact]
        public void TestLength()
        {
            // Arrange
            PlayerDirection direction;

            // Act
            direction = new PlayerDirection(2, 2);

            // Assert
            var expectedLength = Math.Sqrt(2 * 2 + 2 * 2);
            Assert.Equal(expectedLength, direction.Length, 4);
        }

        [Fact]
        public void TestRandomPlayerDirection()
        {
            // This Test will check if RandomPlayerDirection will return different Directions

            // Arrange
            var firstDirection = PlayerDirection.RandomPlayerDirection();

            // Act
            var otherDirection = firstDirection;
            for(int iterationNo = 0; iterationNo < 1000; iterationNo++)
            {
                otherDirection = PlayerDirection.RandomPlayerDirection();
                
                if (otherDirection != firstDirection)
                    break;
            }

            // Assert
            Assert.NotEqual(otherDirection, firstDirection);
        }

        [Fact]
        public void TestApply()
        {
            // Arrange
            var initialDirection = new PlayerDirection(0, 2);

            // Act Change Nothing
            var changeNothingDirection = initialDirection.Apply(PlayerAction.Action.CHANGE_NOTHING);
            // Assert
            Assert.Equal(initialDirection, changeNothingDirection);


            // Act turn left
            var turnLeftDirection = initialDirection.Apply(PlayerAction.Action.TURN_LEFT);
            // Assert
            Assert.Equal(-2, turnLeftDirection.X, 4);
            Assert.Equal(0, turnLeftDirection.Y, 4);


            // Act turn right
            var turnRightDirection = initialDirection.Apply(PlayerAction.Action.TURN_RIGHT);
            // Assert
            Assert.Equal(2, turnRightDirection.X, 4);
            Assert.Equal(0, turnRightDirection.Y, 4);


            // Act speed up
            var speedUpDirection = initialDirection.Apply(PlayerAction.Action.SPEED_UP);
            // Assert
            Assert.Equal(0, speedUpDirection.X, 4);
            Assert.Equal(3, speedUpDirection.Y, 4);


            // Act slow down
            var slowDownDirection = initialDirection.Apply(PlayerAction.Action.SLOW_DOWN);
            // Assert
            Assert.Equal(0, slowDownDirection.X, 4);
            Assert.Equal(1, slowDownDirection.Y, 4);


            // Act jump
            var jumpDirection = initialDirection.Apply(PlayerAction.Action.JUMP);
            // Assert
            Assert.Equal(initialDirection, jumpDirection);
        }
    }
}
