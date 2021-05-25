using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WeepingSnake.Game.Tests
{
    public class GameControllerTests
    {
        [Fact]
        public void TestJoinAsGuest()
        {
            // Arrange
            var gamecontroller = new GameController(2, 10);

            // Act
            var player1 = gamecontroller.JoinGame();
            var player2 = gamecontroller.JoinGame();
            var player3 = gamecontroller.JoinGame();

            // Assert
            Assert.True(player1.IsGuest);
            Assert.True(player2.IsGuest);
            Assert.True(player3.IsGuest);
            Assert.Equal(player1.AssignedGame.GameId, player2.AssignedGame.GameId);
            Assert.NotEqual(player1.AssignedGame.GameId, player3.AssignedGame.GameId);

            // Annihilate
            gamecontroller.Dispose();
        }

        [Fact]
        public void TestFindPlayer()
        {
            // Arrange
            var gamecontroller = new GameController(2, 10);

            // Act
            var createdPlayer = gamecontroller.JoinGame();
            var searchedPlayer = gamecontroller.FindPlayer(createdPlayer.PlayerId);

            // Assert
            Assert.Equal(createdPlayer, searchedPlayer);

            // Annihilate
            gamecontroller.Dispose();
        }

    }
}
