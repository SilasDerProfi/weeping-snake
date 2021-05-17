using System;
using System.Linq;
using System.Collections.Generic;
using WeepingSnake.Game.Geometry;
using WeepingSnake.Game.Player;
using WeepingSnake.Game.Structs;
using WeepingSnake.Game.Utility.Extensions;

namespace WeepingSnake.Game
{
    public sealed partial class Game
    {
        public sealed class Board : CoordinateSystem
        {
            private readonly bool _isInfinite;
            private readonly List<List<GameDistance>> _paths;

            internal Board(BoardDimensions dimensions) : base(dimensions)
            {
                _isInfinite = dimensions.IsInfinite;
                _paths = new List<List<GameDistance>>();
            }

            internal void ApplyAction(PlayerAction action)
            {
                // alter the player
                throw new NotImplementedException();
            }

            internal PlayerOrientation CalculateRandomStartOrientation()
            {
                var direction = PlayerDirection.RandomPlayerDirection();
                var position = CalculateRandomStartCoordinates();

                return new PlayerOrientation(position, direction);
            }

            private GameCoordinate CalculateRandomStartCoordinates()
            {
                var zPosition = (uint) _paths.Count;
                var possiblePositions = new HashSet<GameCoordinate>();

                for (var x = 1; x < Width - 1; x++)
                    for (var y = 1; y < Height - 1; y++)
                        possiblePositions.Add(new GameCoordinate(x, y, zPosition));

                foreach (var pathPoints in _paths[^1].Select(vector => CalculatePointsOnLine(0, 0, 0, 0)))
                {
                    foreach (var (pathX, pathY) in pathPoints)
                    {
                        var coordinate = new GameCoordinate(pathX, pathY, zPosition);
                        possiblePositions.Remove(coordinate);

                        foreach (var (neighborX, neighborY) in coordinate.MooreNeighborhood())
                        {
                            var neighbor = new GameCoordinate(neighborX, neighborY, zPosition);
                            possiblePositions.Remove(neighbor);
                        }
                    }
                }

#warning TODO: select the one in the largest cluster (Lloyd) nearest to the center
                return possiblePositions.Random();
            }
        }
    }
}
