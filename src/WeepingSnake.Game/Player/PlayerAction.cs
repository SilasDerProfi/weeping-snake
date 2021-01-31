using System;
using System.Collections.Generic;
using System.Text;

namespace WeepingSnake.Game.Player
{
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
