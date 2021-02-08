using System;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;
using WeepingSnake.Game.Geometry;
using WeepingSnake.Game.Player;
using WeepingSnake.Game.Structs;

namespace WeepingSnake.Game
{
    public sealed partial class Game
    {
        public sealed class Board : CoordinateSystem
        {
            private readonly bool _isInfinite;
            private readonly List<List<Vector2>> _paths;

            internal Board(BoardDimensions dimensions) : base(dimensions)
            {
                _isInfinite = dimensions.IsInfinite;
                _paths = new List<List<Vector2>>();
            }

            internal void ApplyAction(PlayerAction action) => throw new NotImplementedException();

            internal PlayerOrientation CalculateRandomStartOrientation()
            {
                var direction = PlayerDirection.RandomPlayerDirection();
                var position = CalculateRandomStartCoordinates();

                return new PlayerOrientation(position, direction);
            }

            private GameCoordinate CalculateRandomStartCoordinates()
            {
                throw new NotImplementedException();
                var possiblePosition = new HashSet<GameCoordinate>();

                for (int x = 1; x < Width - 1; x++)
                    for (int y = 1; y < Height - 1; y++)
                        possiblePosition.Add(new GameCoordinate(x, y, (uint)_paths.Count));

                foreach(var vector in _paths[^1])
                {
                    // get affected positions from vector
                    _ = CalculatePointsOnLine(0, 0, 0, 0);

                    // add a padding
                        // around all found affected positions

                    // remove the positions from the possible start positions
                }

                // retrun a remaining start position (random or the one in the largest cluster nearest to the center)
            }
        }
    }
}
