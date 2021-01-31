using System;

namespace WeepingSnake.Game.Geometry
{
    public struct GameBoardPoint
    {
        private readonly double _x;
        private readonly double _y;
        private readonly uint _z;

        public GameBoardPoint(double x, double y, uint z)
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
            return obj is GameBoardPoint point &&
                   _x == point._x &&
                   _y == point._y &&
                   _z == point._z;
        }

        public override int GetHashCode() => HashCode.Combine(_x, _y, _z);

        public override string ToString() => $"({_x:F2}|{_y:F2}|{_z:F2})";
        
    }
}
