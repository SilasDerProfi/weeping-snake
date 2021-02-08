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

            private GameCoordinate CalculateRandomStarCoordinates()
            {
                throw new NotImplementedException();
                var possiblePosition = new HashSet<GameCoordinate>();

                for (int x = 1; x < _dimensions.Width - 1; x++)
                    for (int y = 1; y < _dimensions.Height - 1; y++)
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

        /// <summary>
        /// via Bresenham's line algorithm
        /// </summary>
        private static IEnumerable<(int, int)> CalculatePointsOnLine(int x0, int y0, int x1, int y1)
        {
            if (Math.Abs(y1 - y0) > Math.Abs(x1 - x0))
                return CalculatePointsOnLine(y0, x0, y1, x1).Select(i => (i.Item2, i.Item1));
            else if (x0 > x1)
                return CalculatePointsOnLine(x1, y1, x0, y0);
            else
                return Bresenham(x0, y0, x1, y1);

            static IEnumerable<(int, int)> Bresenham(int x0, int y0, int x1, int y1)
            {
                int deltaX = x1 - x0;
                int deltaY = Math.Abs(y1 - y0);
                int incrementE = 2 * deltaY;
                int incrementNe = 2 * (deltaY - deltaX);
                int d = 2 * deltaY - deltaX;

                for (int x = x0, y = y0; x <= x1; x++)
                {
                    yield return (x, y);
                    if (d <= 0) d += incrementE;
                    else
                    {
                        d += incrementNe;
                        y++;
                    }
                }
            }
        }
    }
}
