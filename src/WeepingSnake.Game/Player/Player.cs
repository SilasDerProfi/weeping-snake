using System;
using System.Collections.Generic;
using System.Numerics;
using WeepingSnake.Game.Geometry;
using WeepingSnake.Game.Player.ComputerPlayer;

namespace WeepingSnake.Game.Player
{
    public class Player
    {
        private readonly Guid _playerId;
        private readonly Person.Person _person;
        private readonly IComputerPlayer _computerPlayer;
        private readonly Queue<PlayerAction.Action> _undoneActions;
        private PlayerOrientation _orientation;
        private Game _game;
        private int _points;
        private bool _isHuman;
        private bool _isAlive = true;

        public Player(Person.Person person)
        {
            _playerId = Guid.NewGuid();
            _person = person;
            _undoneActions = new Queue<PlayerAction.Action>();
            _isHuman = true;
        }

        public Player(IComputerPlayer computerPlayer)
        {
            _computerPlayer = computerPlayer;
            _undoneActions = computerPlayer.GenerateInitialActions();
            _isHuman = false;
        }

        public Game AssignedGame => _game;

        public int Points
        {
            get => _points;
            set => _points = value;
        }
        public bool IsHuman => _isHuman;

        internal void Join(Game game)
        {
            _game = game;
            _orientation = _game.Join(this);
            _points = 0;
            
            if(!IsHuman)
                _computerPlayer.ControlledPlayer = this;
        }

        internal void AddAction(PlayerAction.Action action) => _undoneActions.Enqueue(action);

        internal PlayerAction PopNextAction()
        {
            _undoneActions.TryDequeue(out var nextAction);

            return new PlayerAction(this, _orientation, nextAction);
        }

        internal void Die()
        {
            _isAlive = false;
            _game.Leave(this);

            if(_person != null)
            {
#warning todo apply points
            }

            _game = null;
        }

        internal GameDistance ApplyOrientationAndMove(PlayerOrientation newOrientation)
        {
            var locationVector = new Vector2((float)_orientation.Position.X, (float)_orientation.Position.Y);
            var directionVector = new Vector2(newOrientation.Direction.X, newOrientation.Direction.Y);

            _orientation = newOrientation;

            return new GameDistance(locationVector, directionVector, this);
        }
    }
}
