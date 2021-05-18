﻿using System;
using System.Numerics;
using WeepingSnake.Game.Player;

namespace WeepingSnake.Game.Geometry
{
    public readonly struct GameDistance
    {
        private readonly Vector2 _locationVector;
        private readonly PlayerDirection _directionVector;
        private readonly Player.Player _player;
        private readonly float _endX;
        private readonly float _endY;

        public GameDistance(float startX, float startY, PlayerDirection directionVector, Player.Player player)
        {
            _locationVector = new Vector2(startX, startY);
            _directionVector = directionVector;
            _endX = _locationVector.X + _directionVector.X;
            _endY = _locationVector.Y + _directionVector.Y;
            
            _player = player;

            
            if (_endX < 0)
            {
                if(_player.AssignedGame.GameBoard.IsInfinite)
                {
                    // start at other site, but dont connect on the field
                    throw new NotImplementedException();
                }
                else
                {
                    _endX = 0;
                    _player.Die();
                }
            }

            if (_endX >= _player.AssignedGame.GameBoard.Width)
            {
                if (_player.AssignedGame.GameBoard.IsInfinite)
                {
                    // start at other site, but dont connect on the field
                    throw new NotImplementedException();
                }
                else
                {
                    _endX = _player.AssignedGame.GameBoard.Width - 1;
                    _player.Die();
                }
            }

            if (_endY < 0)
            {
                if (_player.AssignedGame.GameBoard.IsInfinite)
                {
                    // start at other site, but dont connect on the field
                    throw new NotImplementedException();
                }
                else
                {
                    _endY = 0;
                    _player.Die();
                }
            }

            if (_endY >= _player.AssignedGame.GameBoard.Height)
            {
                if (_player.AssignedGame.GameBoard.IsInfinite)
                {
                    // start at other site, but dont connect on the field
                    throw new NotImplementedException();
                }
                else
                {
                    _endY = _player.AssignedGame.GameBoard.Height - 1;
                    _player.Die();
                }
            }
        }

        public float StartX => _locationVector.X;
        public float StartY => _locationVector.Y;
        public float EndX => _endX;
        public float EndY => _endY;


        public Player.Player Player => _player;
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
