using System;
using System.Numerics;
using WeepingSnake.Game.Geometry;

namespace WeepingSnake.Game.Player
{
    public struct PlayerOrientation
    {
        private readonly GameCoordinate _position;
        private readonly Vector2 _direction;

        internal PlayerOrientation(double posX, double posY, uint time, int directionX, int directionY)
        {
            _position = new GameCoordinate(posX, posY, time);
            _direction = new Vector2(directionX, directionY);
        }

        internal PlayerOrientation Apply(PlayerAction.Action action) => throw new NotImplementedException();

        internal void MoveOneTick() => throw new NotImplementedException();

        public override bool Equals(object obj)
        {
            return obj is PlayerOrientation orientation &&
                   _position.Equals(orientation._position) &&
                   _direction.Equals(orientation._direction);
        }


        public GameCoordinate Position => _position;
        public Vector2 Direction => _direction;

        public override int GetHashCode() => HashCode.Combine(_position, _direction);

        public static bool operator ==(PlayerOrientation left, PlayerOrientation right) => left.Equals(right);

        public static bool operator !=(PlayerOrientation left, PlayerOrientation right) => !(left == right);

        public override string ToString() => $"{_position} | {_direction}";
    }
}
