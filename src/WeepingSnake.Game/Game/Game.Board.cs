using System;
using WeepingSnake.Game.Player;
using WeepingSnake.Game.Structs;

namespace WeepingSnake.Game
{
    public sealed partial class Game
    {
        private sealed class Board
        {
            private readonly bool _isInfinite;

            internal Board(BoardDimensions dimensions)
            {
                _isInfinite = dimensions.IsInfinite;
            }

            internal void ApplyAction(PlayerAction action) => throw new NotImplementedException();

            internal PlayerOrientation GetOrientationForPlayer(Player.Player player) => throw new NotImplementedException();
        }
    }
}
