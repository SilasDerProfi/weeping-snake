using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game.Player;
using WeepingSnake.Game.Structs;

namespace WeepingSnake.Game
{
    public class GameBoard
    {
        private readonly bool _isInfinite;

        public GameBoard(BoardDimensions dimensions)
        {
            _isInfinite = dimensions.IsInfinite;
        }

        internal void ApplyAction(PlayerAction action) => throw new NotImplementedException();
    }
}
