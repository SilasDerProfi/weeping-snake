using System;
using System.Numerics;
using WeepingSnake.Game.Utility.Extensions;

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
#warning TODO: do not use 1 hardcoded, but use a const like "default speed  step"
            var defaultDirection = new Vector2(0, 1);
            var random = new Random();
#warning TODO: do not use 90 degree hardcoded, but use a const like "minimum rotation angle"
            var rotations = random.Next(0, 360 / 90);
            var rotatedDirection = defaultDirection.RotateRight(90 * rotations);
            return new PlayerDirection(rotatedDirection.X, rotatedDirection.Y);
        }

#warning TODO: do not use 90 degree hardcoded, but use a const like "default rotation angle"
        internal PlayerDirection Apply(PlayerAction.Action action) => action switch
        {
            PlayerAction.Action.CHANGE_NOTHING => new(X, Y),
            PlayerAction.Action.TURN_LEFT => _direction.RotateLeft(90),
            PlayerAction.Action.TURN_RIGHT => _direction.RotateRight(90),
            PlayerAction.Action.SPEED_UP => _direction.Increase(),
            PlayerAction.Action.SLOW_DOWN => _direction.Decrease(),
            PlayerAction.Action.JUMP => new(X, Y),
            _ => throw new ArgumentOutOfRangeException(nameof(action), $"Not expected ${nameof(action)} value: {action}"),
        };

        public override bool Equals(object obj)
        {
            return obj is PlayerDirection direction && 
                   _direction.Equals(direction._direction);
        }

        public override int GetHashCode() => HashCode.Combine(_direction, Length);

        public static bool operator ==(PlayerDirection left, PlayerDirection right) => left.Equals(right);

        public static bool operator !=(PlayerDirection left, PlayerDirection right) => !(left == right);

        public static implicit operator PlayerDirection(Vector2 direction) => new(direction.X, direction.Y);
    }
}
