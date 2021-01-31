using System;

namespace WeepingSnake.Game.Player
{
    public class Player
    {
        private readonly Guid _playerId;
        private readonly Game _game;
        private readonly Person.Person _person;

        public Player(Game game)
        {
            _playerId = Guid.NewGuid();
            _game = game;
        }
    }
}
