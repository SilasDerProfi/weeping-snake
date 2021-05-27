using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WeepingSnake.Game.Utility.Extensions;

namespace WeepingSnake.Game.Player.ComputerPlayer
{
    public class RandomNotKillingItselfPlayer : IComputerPlayer
    {
        private IPlayer _controlledPlayer;

        public Queue<PlayerAction.Action> GenerateInitialActions()
        {
            var queue = new Queue<PlayerAction.Action>();
            return queue;
        }

        public IPlayer ControlledPlayer
        {
            get
            {
                return _controlledPlayer;
            }
            set
            {
                if (_controlledPlayer?.AssignedGame as Game != null)
                {
                    (_controlledPlayer.AssignedGame as Game).OnLoopTick -= AssignedGame_OnLoopTick;
                }

                _controlledPlayer = value;

                if (_controlledPlayer.AssignedGame as Game != null)
                {
                    (_controlledPlayer.AssignedGame as Game).OnLoopTick += AssignedGame_OnLoopTick;
                }
            }
        }

        public void AssignedGame_OnLoopTick(List<Geometry.GameDistance> newPaths)
        {
            var actions = GetActionsInRandomOrder();
            var action = actions.FirstOrDefault(IsActionValidForBoardDimensions);

            _controlledPlayer.AddAction(action);
        }

        private PlayerAction.Action[] GetActionsInRandomOrder()
        {
            var actions = Enum.GetValues<PlayerAction.Action>();
            var random = new Random();

            return actions.OrderBy(a => random.Next()).ToArray();
        }

        private bool IsActionValidForBoardDimensions(PlayerAction.Action action)
        {
            var board = _controlledPlayer.AssignedGame?.GameBoard;
            var position = _controlledPlayer.Orientation.ApplyAndMove(action).Position;

            return board != null && position.X >= 0 && position.Y >= 0 && position.X < board.Width &&
                   position.Y < board.Height;
        }
    }
}
