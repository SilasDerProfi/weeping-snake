using System;

namespace WeepingSnake.Game
{
    /// <summary>
    /// Represents one game
    /// </summary>
    public sealed class Game
    {
        private readonly Guid _gameId;

        public Game()
        {
            _gameId = Guid.NewGuid();
        }

        public Guid GameId => _gameId;

        public Player.Player Participate(Guid? personId = null) => throw new NotImplementedException();
    }
}
