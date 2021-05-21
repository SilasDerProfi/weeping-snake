using System;
using System.Numerics;
using WeepingSnake.Game.Player;
using WeepingSnake.Game.Utility.Extensions;

namespace WeepingSnake.Game.Geometry
{
    public readonly struct GameDistance
    {
        private readonly Vector2 _locationVector;
        private readonly PlayerDirection _directionVector;
        private readonly Player.Player _player;
        private readonly float _endX;
        private readonly float _endY;


        public GameDistance(Vector2 locationVector, Vector2 directionVector, Player.Player player)
            : this(locationVector.X, locationVector.Y, directionVector, player)
        {

        }

        public GameDistance(float startX, float startY, PlayerDirection directionVector, Player.Player player)
        {
            _locationVector = new Vector2((int)startX, (int)startY);
            _directionVector = directionVector;
            _endX = (int)(_locationVector.X + _directionVector.X);
            _endY = (int)(_locationVector.Y + _directionVector.Y);

            if (_directionVector.Length != 0)
            {
                _locationVector += _directionVector.DirectionVector.DefaultDistanceVector();
            }

            _player = player;

            if (player.IsAlive)
            {
                if ((int)_endX < 0)
                {
                    _endX = 0;
                    _player.Die();
                    return;
                }

                if ((int)_endX >= _player.AssignedGame.GameBoard.Width)
                {
                    _endX = _player.AssignedGame.GameBoard.Width - 1;
                    _player.Die();
                    return;
                }

                if ((int)_endY < 0)
                {
                    _endY = 0;
                    _player.Die();
                    return;
                }

                if ((int)_endY >= _player.AssignedGame.GameBoard.Height)
                {
                    _endY = _player.AssignedGame.GameBoard.Height - 1;
                    _player.Die();
                    return;
                }
            }

        }

        public float StartX
        {
            get
            {
                return _locationVector.X;
            }
        }

        public float StartY
        {
            get
            {
                return _locationVector.Y;
            }
        }

        public float EndX
        {
            get
            {
                return _endX;
            }
        }

        public float EndY
        {
            get
            {
                return _endY;
            }
        }


        public Player.Player Player
        {
            get
            {
                return _player;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_locationVector, _directionVector);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as GameDistance?);
        }

        public bool Equals(GameDistance? other)
        {
            return _locationVector.Equals(other?._locationVector) && _directionVector.Equals(other?._directionVector);
        }

        public static bool operator ==(GameDistance left, GameDistance right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GameDistance left, GameDistance right)
        {
            return !(left == right);
        }
    }
}
