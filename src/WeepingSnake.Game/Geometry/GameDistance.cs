using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game.Player;

namespace WeepingSnake.Game.Geometry
{
    public readonly struct GameDistance
    {
        private readonly Vector2 _locationVector;
        private readonly PlayerDirection _directionVector;
    }
}
