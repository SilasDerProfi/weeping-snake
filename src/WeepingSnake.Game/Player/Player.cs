using System;

namespace WeepingSnake.Game.Player
{
    public class Player
    {
        private readonly Guid _playerId;
        private readonly Person.Person _person;
        private Game _game;

        public Player(Person.Person person)
        {
            _playerId = Guid.NewGuid();
            _person = person;
        }

        public Game AssignedGame => _game;

        internal void Join(Game game)
        {
            _game = game;
            _game.Join(this);
        }

    }
}
