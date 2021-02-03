using System;
using System.Collections.Generic;

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

        internal PlayerAction PopAndApplyNextAction()
        {
            _undoneActions.TryDequeue(out var nextAction);

            var playerAction = new PlayerAction(_orientation, nextAction);

            _orientation = playerAction.NewOrientation;

            return playerAction;
        }
    }
}
