using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game.Player;
using WeepingSnake.Game.Tests.Player;
using Xunit;

namespace WeepingSnake.Game.Tests.Game
{
    public class GameTests
    {
        [Fact]
        public void TestJoinWrongGame()
        {
            // Arrange
            var game = new WeepingSnake.Game.Game(2, 40);
            var player = new MockPlayer()
            {
                IsHuman = true
            };

            // Act
            try
            {
                game.Join(player);
            }
            catch (ArgumentException ex)
            {
                // Assert
                Assert.StartsWith("A player can join only the game assigned to him.", ex.Message);
                return;
            }
            finally
            {
                // Assert
                Assert.DoesNotContain(player, game.Players);
                Assert.True(game.IsActive);
            }
        }

        [Fact]
        public void TestJoinCorrectGame()
        {
            // Arrange
            var game = new WeepingSnake.Game.Game(2, 40);
            var player = new MockPlayer()
            {
                AssignedGame = game,
                IsHuman = true
            };

            // Act
            game.Join(player);

            // Assert
            Assert.Contains(player, game.Players);
            Assert.Equal(game.Players[1].GetHashCode(), player.GetHashCode());
            Assert.Equal(game.GetHashCode(), player.AssignedGame.GetHashCode());
        }

        [Fact]
        public void TestJoinFullGame()
        {
            // Arrange
            var game = new WeepingSnake.Game.Game(2, 40);
            var player = new MockPlayer()
            {
                AssignedGame = game
            };

            // Act
            try
            {
                game.Join(player);
            }
            catch (ArgumentException ex)
            {
                // Assert
                Assert.StartsWith("A player cannot join a full game.", ex.Message);
                return;
            }
            finally
            {
                // Assert
                Assert.DoesNotContain(player, game.Players);
            }
        }


        [Fact]
        public void TestLeave()
        {
            // Arrange
            var game = new WeepingSnake.Game.Game(2, 40);
            var player = new MockPlayer()
            {
                AssignedGame = game,
                IsHuman = true,
                IsAlive = true
            };
            game.Join(player);

            // Act
            var computer = game.Players.FirstOrDefault(player => !player.IsHuman);
            computer.Die();

            // Assert
            Assert.DoesNotContain(computer, game.Players);
            Assert.Equal(2, game.Players.Count);
        }

        [Fact]
        public void TestApplyOneActionPerPlayer()
        {
            // Arrange
            var game = new WeepingSnake.Game.Game(2, 40);

            // Act
            game.ApplyOneActionPerPlayer();

            // Assert
            Assert.Equal(1, game.Players[0].Orientation.Position.Z);
            Assert.Equal(1, game.Players[1].Orientation.Position.Z);
            Assert.Single(game.BoardPaths);
            Assert.Equal(2, game.BoardPaths[0].Count);

        }
    }
}
