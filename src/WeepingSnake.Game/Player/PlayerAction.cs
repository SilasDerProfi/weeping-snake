using System;
using System.Collections.Generic;
using System.Text;

namespace WeepingSnake.Game.Player
{
    /// <summary>
    /// A concrete action a player wants to do at a concrete time
    /// </summary>
    public sealed class PlayerAction
    {

        public enum Action
        {
            CHANGE_ANGLE,
            SPEED_UP,
            SLOW_DOWN,
            JUMP,
        }
    }
}
