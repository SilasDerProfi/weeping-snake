using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeepingSnake.Game.Player.ComputerPlayer
{
    public interface IComputerPlayer
    {
        Queue<PlayerAction.Action> GenerateInitialActions();


        Player ControlledPlayer { get; set; }
    }
}
