using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeepingSnake.Game.Player.ComputerPlayer
{
    public class DoNothingPlayer : IComputerPlayer
    {
        private IPlayer _controlledPlayer;

        public Queue<PlayerAction.Action> GenerateInitialActions()
        {
            return new Queue<PlayerAction.Action>();
        }

        public IPlayer ControlledPlayer
        {
            get
            {
                return _controlledPlayer;
            }
            set
            {
                _controlledPlayer = value;
            }
        }
    }
}
