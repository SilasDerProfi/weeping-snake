using System;
using System.Collections.Generic;
using System.Numerics;
using WeepingSnake.Game.Geometry;
using WeepingSnake.Game.Player.ComputerPlayer;

namespace WeepingSnake.Game.Player
{
    public class Player : IPlayer
    {
        private readonly Guid _playerId;
        private readonly Person.Person _person;
        private readonly Queue<PlayerAction.Action> _undoneActions;
        private PlayerOrientation _orientation;
        private Game _game;
        private int _points;
        private readonly bool _isHuman;
        private bool _isAlive = true;

        public Player(Person.Person person, bool isHuman = true)
        {
            _playerId = Guid.NewGuid();
            _person = person;
            _undoneActions = new Queue<PlayerAction.Action>();
            _isHuman = isHuman;
        }

        public Game AssignedGame
        {
            get
            {
                return _game;
            }
        }

        public int Points
        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
            }
        }

        public bool IsHuman
        {
            get
            {
                return _isHuman;
            }
        }

        public bool IsAlive
        {
            get
            {
                return _isAlive;
            }
        }

        public Guid PlayerId
        {
            get
            {
                return _playerId;
            }
        }

        public bool IsGuest
        {
            get
            {
                return _person == null;
            }
        }

        public Person.Person ControllingPerson
        {
            get
            {
                return _person;
            }
        }

        public PlayerOrientation Orientation
        {
            get
            {
                return _orientation;
            }
        }

        public void Join(Game game)
        {
            if (!_isAlive || _game != null)
            {
                throw new InvalidOperationException("A player can only join a game if he has never participated in a game before.");
            }

            _game = game;
            _orientation = _game.Join(this);
            _points = 0;
        }

        public void AddAction(PlayerAction.Action action)
        {
            _undoneActions.Enqueue(action);
        }

        public void AddActions(Queue<PlayerAction.Action> actions)
        {
            while (actions.TryDequeue(out var action))
            {
                _undoneActions.Enqueue(action);
            }
        }

        public PlayerAction PopNextAction()
        {
            _undoneActions.TryDequeue(out var nextAction);

            return new PlayerAction(this, _orientation, nextAction);
        }

        public void Die()
        {
            _isAlive = false;

            if (_game != null)
            {
                _game.Leave(this);
            }

            _game = null;
        }

        public GameDistance? ApplyOrientationAndMove(PlayerOrientation newOrientation)
        {
            if (_isAlive)
            {
                var locationVector = new Vector2((float)_orientation.Position.X, (float)_orientation.Position.Y);
                var directionVector = new Vector2(newOrientation.Direction.X, newOrientation.Direction.Y);

                _orientation = newOrientation;

                return new GameDistance(locationVector, directionVector, this);
            }

            return null;
        }

        public void ApplyPointsToPerson()
        {
            if (_person != null)
            {
                _person.AddPointsFromGame(_points);
            }

            _points = 0;
            _isAlive = false;
        }

        public override bool Equals(object obj)
        {
            return obj is Player player && _playerId.Equals(player._playerId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_playerId);
        }
    }
}
