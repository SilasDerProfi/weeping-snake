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

            internal PlayerOrientation GetRandomStartOrientation(Player.Player player)
            {
                var random = new Random(player.GetHashCode());

                // Random direction vector with Length=1
                var directionX = (float)random.NextDouble() * 2 - 1;
                var directionY = (float)Math.Sqrt(1 - directionX * directionX);
                var direction = new Vector2(directionX, directionY);

                // Random start position with a padding
                var position = CalculateRandomStarCoordinates();

                return new PlayerOrientation(position, direction);
            }

            private GameCoordinate CalculateRandomStarCoordinates() => throw new NotImplementedException();
        }
    }
}
