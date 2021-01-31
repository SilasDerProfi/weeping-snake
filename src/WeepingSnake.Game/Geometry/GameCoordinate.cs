using System;

namespace WeepingSnake.Game.Geometry
{
    /// <summary>
    /// A three-dimensional point. Two dimensions represent the position in 2d space (decimal). the third deminsion is the time (integer).
    /// </summary>
    public struct GameCoordinate
    {
        private readonly double _x;
        private readonly double _y;
        private readonly uint _z;

        public GameCoordinate(double x, double y, uint z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public double X => _x;
        public double Y => _y;
        public uint Z => _z;

        public override bool Equals(object obj)
        {
            return obj is GameCoordinate point &&
                   _x == point._x &&
                   _y == point._y &&
                   _z == point._z;
        }

        public override int GetHashCode() => HashCode.Combine(_x, _y, _z);

        public override string ToString() => $"({_x:F2}|{_y:F2}|{_z:F2})";
        
    }
}
