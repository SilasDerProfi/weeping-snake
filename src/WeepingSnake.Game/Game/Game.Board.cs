using System;
using System.Collections.Generic;
using System.Numerics;
using WeepingSnake.Game.Geometry;
using WeepingSnake.Game.Player;
using WeepingSnake.Game.Structs;

namespace WeepingSnake.Game
{
    public sealed partial class Game
    {
        private sealed class Board
        {
            private readonly BoardDimensions _dimensions;
            private readonly List<List<Vector2>> _paths;

            internal Board(BoardDimensions dimensions)
            {
                _dimensions = dimensions;
                _paths = new List<List<Vector2>>();
            }

            internal void ApplyAction(PlayerAction action) => throw new NotImplementedException();

            internal PlayerOrientation CalculateRandomStartOrientation()
            {
                var direction = PlayerDirection.RandomPlayerDirection();
                var position = CalculateRandomStarCoordinates();

                return new PlayerOrientation(position, direction);
            }

            // Random start position with a padding
            private GameCoordinate CalculateRandomStarCoordinates() => throw new NotImplementedException();
        }
    }
}
