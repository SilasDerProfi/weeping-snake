using System;
using System.Numerics;

namespace WeepingSnake.Game.Player
{
    public struct PlayerDirection
    {
        private readonly Vector2 _direction;

        public PlayerDirection(float directionX, float directionY) : this()
        {
            _direction.X = directionX;
            _direction.Y = directionY;
        }

        public double X => _direction.X;
        public double Y => _direction.Y;

        public float Length() => _direction.Length();
        
        public static PlayerDirection RandomPlayerDirection()
        {
            var random = new Random();

            // Random direction vector with Length=1
            var directionX = (float)random.NextDouble() * 2 - 1;
            var directionY = (float)Math.Sqrt(1 - directionX * directionX);

            return new PlayerDirection(directionX, directionY);
        }

        public override bool Equals(object obj)
        {
            return obj is PlayerDirection direction &&
                   _direction.Equals(direction._direction);
        }

        public override int GetHashCode() => HashCode.Combine(_direction, 23);

        public static bool operator ==(PlayerDirection left, PlayerDirection right) => left.Equals(right);

        public static bool operator !=(PlayerDirection left, PlayerDirection right) => !(left == right);
    }
}
