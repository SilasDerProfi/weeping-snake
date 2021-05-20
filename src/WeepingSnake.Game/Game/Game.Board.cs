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

            internal bool IsInfinite => _isInfinite;

            internal void ApplyAction(PlayerAction action)
            {
                var roundNumber = action.NewOrientation.Position.Z;

                var resultingPath = action.Apply();
                
                if (!resultingPath.HasValue)
                    return;

                var newPath = resultingPath.Value;


                var currentRoundPathList = _paths.GetOrCreate(roundNumber - 1, () => new List<GameDistance>());
                currentRoundPathList.Add(newPath);

                var newPathPoints = CalculatePointsOnLine(newPath);

#warning for rounds odler than 5: die or ignore
                for(int i = 2; i <= Math.Min(6, _paths.Count); i++)
                {
                    _paths[^i].ForEach(path =>
                    {
                        if (path.Player.IsAlive)
                        {
                            var oldPathPoints = CalculatePointsOnLine(path);
                            if (oldPathPoints.Intersect(newPathPoints).Any())
                            {
                                if (path.Player == newPath.Player)
                                {
                                    path.Player.Points -= 10;
                                }
                                else
                                {
                                    newPath.Player.Points += 2;
                                    path.Player.Points -= 1;
                                }
                            }
                        }
                    });
                }
            }

            internal PlayerOrientation CalculateRandomStartOrientation()
            {
                var direction = PlayerDirection.RandomPlayerDirection();
                var position = CalculateRandomStartCoordinates();

                return new PlayerOrientation(position, direction);
            }

            private GameCoordinate CalculateRandomStartCoordinates()
            {
                var zPosition = (ushort) _paths.Count;
                var possiblePositions = new HashSet<GameCoordinate>();

                for (var x = GameConfiguration.DefaultDistance; x < Width - GameConfiguration.DefaultDistance; x++)
                    for (var y = GameConfiguration.DefaultDistance; y < Height - GameConfiguration.DefaultDistance; y++)
                        possiblePositions.Add(new GameCoordinate(x, y, zPosition));

                if (zPosition > 0)
                {
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
                }

                return possiblePositions.Random();
            }

            internal List<List<GameDistance>> Paths => _paths;
        }
    }
}
