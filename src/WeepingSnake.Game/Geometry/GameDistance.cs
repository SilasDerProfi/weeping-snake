using System;
using System.Numerics;
using WeepingSnake.Game.Player;

namespace WeepingSnake.Game.Geometry
{
    public readonly struct GameDistance
    {
        private readonly Vector2 _locationVector;
        private readonly PlayerDirection _directionVector;

        public GameDistance(float startX, float startY, PlayerDirection directionVector)
        {
            _locationVector = new Vector2(startX, startY);
            _directionVector = directionVector;
        }

        public float StartX => _locationVector.X;
        public float StartY => _locationVector.Y;
        public float EndX => StartX + _directionVector.X;
        public float EndY => StartY + _directionVector.Y;
        
        public override int GetHashCode() => HashCode.Combine(_locationVector, _directionVector);

        public override bool Equals(object obj) => Equals(obj as GameDistance?);

        public bool Equals(GameDistance? other)
        {
            return _locationVector.Equals(other?._locationVector) && _directionVector.Equals(other?._directionVector);
        }

        public static bool operator ==(GameDistance left, GameDistance right) => left.Equals(right);

        public static bool operator !=(GameDistance left, GameDistance right) => !(left == right);
    }
}
