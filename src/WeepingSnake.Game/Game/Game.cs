using System;
using System.Collections.Generic;

namespace WeepingSnake.Game
{
    /// <summary>
    /// Represents one game
    /// </summary>
    public sealed class Game
    {
        private readonly Guid _gameId;
        private List<Player.Player> _players;
        private readonly PlayerRange _allowedPlayerCount;
        private readonly BoardDimensions _boardDimensions;


        public Game(PlayerRange allowedPlayerCount, BoardDimensions boardDimensions)
        {
            _gameId = Guid.NewGuid();
            _players = new List<Player.Player>();
            _allowedPlayerCount = allowedPlayerCount;
            _boardDimensions = boardDimensions;
        }

        public Guid GameId => _gameId;

        public Player.Player Participate(Guid? personId = null) => throw new NotImplementedException();

        internal bool PlayerCanJoin() => _allowedPlayerCount.Max - _players.Count > 0;
    }
}
