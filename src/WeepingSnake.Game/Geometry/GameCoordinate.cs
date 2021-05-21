using System;
using System.Collections.Generic;
using WeepingSnake.Game.Player;

namespace WeepingSnake.Game.Geometry
{
    /// <summary>
    /// A three-dimensional point. Two dimensions represent the position in 2d space (decimal). the third deminsion is the time (integer).
    /// </summary>
    public readonly struct GameCoordinate
    {
        private readonly double _x;
        private readonly double _y;
        private readonly ushort _z;

        public GameCoordinate(double x, double y, ushort z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public double X
        {
            get
            {
                return _x;
            }
        }

        public double Y
        {
            get
            {
                return _y;
            }
        }

        internal GameCoordinate Translate(PlayerDirection direction) => new(X + direction.X, Y + direction.Y, (ushort)(Z + 1));

        public ushort Z
        {
            get
            {
                return _z;
            }
        }

        /// <summary>
        /// Calculates the von Neumann neighborhood (4-neighborhood).
        /// </summary>
        /// <returns>4 neighbors (top, bottom, left, right)</returns>
        public IEnumerable<(double, double)> VonNeumannNeighborhood()
        {
            var west = (X - GameConfiguration.DefaultDistance, Y);
            var north = (X, Y - GameConfiguration.DefaultDistance);
            var east = (X + GameConfiguration.DefaultDistance, Y);
            var south = (X, Y + GameConfiguration.DefaultDistance);

            return new List<(double, double)>() { west, north, east, south };
        }

        /// <summary>
        /// Calculates the von Moore neighborhood (8-neighborhood).
        /// </summary>
        /// <returns>8 neighbors (all surrounding cells)</returns>
        public IEnumerable<(double, double)> MooreNeighborhood()
        {
            var west = (X - GameConfiguration.DefaultDistance, Y);
            var northWest = (X - GameConfiguration.DefaultDistance, Y - GameConfiguration.DefaultDistance);
            var north = (X, Y - GameConfiguration.DefaultDistance);
            var northEast = (X + GameConfiguration.DefaultDistance, Y - GameConfiguration.DefaultDistance);
            var east = (X + GameConfiguration.DefaultDistance, Y);
            var southEast = (X + GameConfiguration.DefaultDistance, Y + GameConfiguration.DefaultDistance);
            var south = (X, Y + GameConfiguration.DefaultDistance);
            var southWest = (X - GameConfiguration.DefaultDistance, Y + GameConfiguration.DefaultDistance);

            return new List<(double, double)>() { west, northWest, north, northEast, east, southEast, south, southWest };
        }

        public override bool Equals(object obj)
        {
            return obj is GameCoordinate point &&
                   _x == point._x &&
                   _y == point._y &&
                   _z == point._z;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_x, _y, _z);
        }

        public override string ToString()
        {
            return $"({_x:F2}|{_y:F2}|{_z:F2})";
        }

        public static bool operator ==(GameCoordinate left, GameCoordinate right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GameCoordinate left, GameCoordinate right)
        {
            return !(left == right);
        }
    }
}
