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

        public double Length => _direction.Length();
        public double X => _direction.X;
        public double Y => _direction.Y;

        public static PlayerDirection GetRandomPlayerDirection()
        {
            var random = new Random();

            // Random direction vector with Length=1
            var directionX = (float)random.NextDouble() * 2 - 1;
            var directionY = (float)Math.Sqrt(1 - directionX * directionX);

            return new PlayerDirection(directionX, directionY);
        }
    }
}
