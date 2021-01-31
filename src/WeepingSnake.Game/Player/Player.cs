using System;

namespace WeepingSnake.Game.Player
{
    public class Player
    {
        private readonly Guid _playerId;
        private readonly Game _game;
        private readonly Person.Person _person;

        public Player(Person.Person person, Game game)
        {
            _playerId = Guid.NewGuid();
            _person = person;
            _game = game;
            game.Join(this);
        }
    }
}
