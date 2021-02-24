using System;
using System.Numerics;

namespace WeepingSnake.Game.Player
{
    public readonly struct PlayerDirection
    {
        private readonly Vector2 _direction;
        private readonly float _length;

        public PlayerDirection(float directionX, float directionY) : this()
        {
            _direction.X = directionX;
            _direction.Y = directionY;
            _length = _direction.Length();
        }

        public float X => _direction.X;
        public float Y => _direction.Y;

        public float Length => _length;

        /// <summary>
        /// Calculates a random direction vector with Length = 1.
        /// </summary>
        public static PlayerDirection RandomPlayerDirection()
        {
            var random = new Random();
            var directionX = (float)random.NextDouble() * 2 - 1;
            var directionY = (float)Math.Sqrt(1 - directionX * directionX);

            return new PlayerDirection(directionX, directionY);
        }

        public override bool Equals(object obj)
        {
            return obj is PlayerDirection direction && 
                   _direction.Equals(direction._direction);
        }

        public override int GetHashCode() => HashCode.Combine(_direction, Length);

        public static bool operator ==(PlayerDirection left, PlayerDirection right) => left.Equals(right);

        public static bool operator !=(PlayerDirection left, PlayerDirection right) => !(left == right);
    }
}
