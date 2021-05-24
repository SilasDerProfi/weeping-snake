using System;
using System.Numerics;
using WeepingSnake.Game.Geometry;

namespace WeepingSnake.Game.Player
{
    public struct PlayerOrientation
    {
        private readonly GameCoordinate _position;
        private readonly PlayerDirection _direction;

        public PlayerOrientation(GameCoordinate position, PlayerDirection direction)
        {
            _position = position;
            _direction = direction;
        }

        public PlayerOrientation ApplyAndMove(PlayerAction.Action action)
        {
            var newDirection = _direction.Apply(action);
            var newPostion = _position.Translate(newDirection);

            return new PlayerOrientation(newPostion, newDirection);
        }

        public override bool Equals(object obj)
        {
            return obj is PlayerOrientation orientation &&
                   _position.Equals(orientation._position) &&
                   _direction.Equals(orientation._direction);
        }


        public GameCoordinate Position
        {
            get
            {
                return _position;
            }
        }

        public PlayerDirection Direction
        {
            get
            {
                return _direction;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_position, _direction);
        }

        public static bool operator ==(PlayerOrientation left, PlayerOrientation right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PlayerOrientation left, PlayerOrientation right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"{_position} | {_direction}";
        }
    }
}
