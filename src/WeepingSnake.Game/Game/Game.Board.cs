using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game.Player;
using WeepingSnake.Game.Structs;

namespace WeepingSnake.Game
{
    public sealed partial class Game
    {
        private class Board
        {
            private readonly bool _isInfinite;

            internal Board(BoardDimensions dimensions)
            {
                _isInfinite = dimensions.IsInfinite;
            }

            internal void ApplyAction(PlayerAction action) => throw new NotImplementedException();
        }
    }
}
