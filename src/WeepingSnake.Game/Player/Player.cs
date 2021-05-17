using System;
using System.Collections.Generic;
using WeepingSnake.Game.Geometry;

namespace WeepingSnake.Game.Player
{
    public class Player
    {
        private readonly Guid _playerId;
        private readonly Person.Person _person;
        private readonly Queue<PlayerAction.Action> _undoneActions;
        private PlayerOrientation _orientation;
        private Game _game;

        public Player(Person.Person person)
        {
            _playerId = Guid.NewGuid();
            _person = person;
            _undoneActions = new Queue<PlayerAction.Action>();
        }

        public Game AssignedGame => _game;

        internal void Join(Game game)
        {
            _game = game;
            _orientation = _game.Join(this);
        }

        internal void AddAction(PlayerAction.Action action) => _undoneActions.Enqueue(action);

        internal PlayerAction PopNextAction()
        {
            _undoneActions.TryDequeue(out var nextAction);

            return new PlayerAction(this, _orientation, nextAction);
        }

        internal GameDistance ApplyOrientationAndMove(PlayerOrientation newOrientation)
        {
            var oldX = (float)_orientation.Position.X;
            var oldY = (float)_orientation.Position.Y;
            var currentDirection = newOrientation.Direction;

            _orientation = newOrientation;
            
            return new GameDistance(oldX, oldY, currentDirection);
        }
    }
}
