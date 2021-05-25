using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WeepingSnake.Game.Tests.Player
{
    public class PlayerTests
    {
        [Fact]
        public void TestJoinInvalidGame()
        {
            // Arrange
            var player = new WeepingSnake.Game.Player.Player(null);
            try
            {
                // Act
                player.Die();
                player.Join(null);
            }
            catch (InvalidOperationException ex)
            {
                // Assert
                Assert.Equal("A player can only join a game if he has never participated in a game before.", ex.Message);
            }
            finally
            {
                // Assert
                Assert.True(player.PlayerId != Guid.Empty);
            }
        }
    }
}
