using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game.Player;
using WeepingSnake.Game.Player.ComputerPlayer;
using WeepingSnake.Game.Tests.Game;
using WeepingSnake.Game.Tests.Geometry;
using Xunit;

namespace WeepingSnake.Game.Tests.Player.ComputerPlayer
{
    public class CreateComputerPlayerTests
    {
        [Fact]
        public void TestCreateForGame()
        {
            // Arrange
            var game = new MockGame()
            {
                Players = new List<WeepingSnake.Game.Player.Player>()
            };

            game.JoinFunc = player =>
            {
                game.Players.Add(player);
                return new WeepingSnake.Game.Player.PlayerOrientation();
            };

            // Act
            CreateComputerPlayer.CreateForGame(game);


            // Assert
            Assert.Single(game.Players);
            Assert.False(game.Players[0].IsHuman);
        }

        [Fact]
        public void TestRandomPlayer()
        {
            // Arrange
            var computer = new RandomPlayer();
            var actions = new List<PlayerAction.Action>();
            var player = new MockPlayer()
            {
                AddActionAction = (action) => actions.Add(action)
            };

            // Act
            var initialActions = computer.GenerateInitialActions();
            computer.ControlledPlayer = player;
            computer.AssignedGame_OnLoopTick(new List<WeepingSnake.Game.Geometry.GameDistance>());
            computer.AssignedGame_OnLoopTick(new List<WeepingSnake.Game.Geometry.GameDistance>());

            // Assert
            Assert.Single(initialActions);
            Assert.Equal(2, actions.Count);
            Assert.False(computer.ControlledPlayer.IsHuman);
        }


        [Fact]
        public void TestDoNothingPlayer()
        {
            // Arrange
            var computer = new DoNothingPlayer();
            var player = new MockPlayer();

            // Act
            var initialActions = computer.GenerateInitialActions();
            computer.ControlledPlayer = player;

            // Assert
            Assert.Empty(initialActions);
            Assert.False(computer.ControlledPlayer.IsHuman);
        }


        [Fact]
        public void TestRandomNotKillingItselfPlayer()
        {
            // Arrange
            var computer = new RandomNotKillingItselfPlayer();
            var actions = new List<PlayerAction.Action>();
            var player = new MockPlayer()
            {
                AddActionAction = (action) => actions.Add(action),
                AssignedGame = new MockGame()
                {
                    GameBoard = new MockCoordinateSystem()
                }
            };

            // Act
            var initialActions = computer.GenerateInitialActions();
            computer.ControlledPlayer = player;
            computer.AssignedGame_OnLoopTick(new List<WeepingSnake.Game.Geometry.GameDistance>());
            computer.AssignedGame_OnLoopTick(new List<WeepingSnake.Game.Geometry.GameDistance>());
            computer.AssignedGame_OnLoopTick(new List<WeepingSnake.Game.Geometry.GameDistance>());

            // Assert
            Assert.Empty(initialActions);
            Assert.Equal(3, actions.Count);
            Assert.False(computer.ControlledPlayer.IsHuman);
        }
    }
}
