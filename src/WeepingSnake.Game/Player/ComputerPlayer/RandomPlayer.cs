using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game.Utility.Extensions;

namespace WeepingSnake.Game.Player.ComputerPlayer
{
    internal class RandomPlayer : IComputerPlayer
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

        private void AssignedGame_OnLoopTick(List<Geometry.GameDistance> newPaths)
        {
            var randomAction = Enum.GetValues<PlayerAction.Action>().Random();

            _controlledPlayer.AddAction(randomAction);
        }
    }
}
