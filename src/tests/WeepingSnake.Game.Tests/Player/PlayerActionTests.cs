﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game.Geometry;
using WeepingSnake.Game.Player;
using Xunit;

namespace WeepingSnake.Game.Tests.Player
{
    public class PlayerActionTests
    {
        [Fact]
        public void TestApply()
        {
            // Arrange
            var mockPlayer = new MockPlayer();

            var position = new GameCoordinate(0, 0, 1);
            var direction = new PlayerDirection(4, 0);
            var orientation = new PlayerOrientation(position, direction);
            var playerAction = new PlayerAction(mockPlayer, orientation, PlayerAction.Action.CHANGE_NOTHING);

            // Act
            var result = playerAction.Apply();

            // Assert
            Assert.Equal(4, playerAction.NewOrientation.Position.X);
            Assert.Equal(0, playerAction.NewOrientation.Position.Y);

            Assert.Null(result);
        }
    }
}
