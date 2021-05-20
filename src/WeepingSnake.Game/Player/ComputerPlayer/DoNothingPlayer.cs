using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeepingSnake.Game.Player.ComputerPlayer
{
    internal class DoNothingPlayer : IComputerPlayer
    {
        private Player _controlledPlayer;

        public Queue<PlayerAction.Action> GenerateInitialActions()
        {
            return new Queue<PlayerAction.Action>();
        }

        public Player ControlledPlayer
        { 
            get => _controlledPlayer; 
            set => _controlledPlayer = value; 
        }
    }
}
