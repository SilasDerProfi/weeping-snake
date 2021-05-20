using System;
using System.Collections.Generic;
using WeepingSnake.Game.Utility.Extensions;

namespace WeepingSnake.Game.Player.ComputerPlayer
{
    class RandomNotKillingItselfPlayer : IComputerPlayer
    {
        private Player _controlledPlayer;

        public Queue<PlayerAction.Action> GenerateInitialActions()
        {
            var queue = new Queue<PlayerAction.Action>();
            queue.Enqueue(Enum.GetValues<PlayerAction.Action>().Random());
            return queue;
        }

        public Player ControlledPlayer
        {
            get
            {
                return _controlledPlayer;
            }
            set
            {
                if (_controlledPlayer?.AssignedGame != null)
                {
                    _controlledPlayer.AssignedGame.OnLoopTick -= AssignedGame_OnLoopTick;
                }

                _controlledPlayer = value;

                if (_controlledPlayer.AssignedGame != null)
                {
                    _controlledPlayer.AssignedGame.OnLoopTick += AssignedGame_OnLoopTick;
                }
            }
        }

        private void AssignedGame_OnLoopTick(List<Geometry.GameDistance> newPaths)
        {

            var board = _controlledPlayer?.AssignedGame?.GameBoard;

            if (board != null)
            {
                var randomAction = Enum.GetValues<PlayerAction.Action>().Random();

                for (int retries = 0; retries < 100; retries++)
                {
                    var newOrientation = _controlledPlayer.Orientation.ApplyAndMove(randomAction);

                    if (newOrientation.Position.X >= 0 && newOrientation.Position.Y >= 0 && newOrientation.Position.X < board.Width && newOrientation.Position.Y < board.Height)
                    {
                        break;
                    }
                    else
                    {
                        randomAction = Enum.GetValues<PlayerAction.Action>().Random();
                    }
                }

                _controlledPlayer.AddAction(randomAction);
            }
        }
    }
}
