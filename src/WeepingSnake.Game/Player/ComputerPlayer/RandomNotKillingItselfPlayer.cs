using System;
using System.Collections.Generic;
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
            var board = _controlledPlayer?.AssignedGame?.GameBoard;

            if (board != null)
            {
                var randomAction = Enum.GetValues<PlayerAction.Action>().Random();

                int retries;
                for (retries = 0; retries < 100; retries++)
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
