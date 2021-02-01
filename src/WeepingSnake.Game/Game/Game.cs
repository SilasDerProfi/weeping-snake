using System;
using System.Collections.Generic;
using WeepingSnake.Game.Structs;

namespace WeepingSnake.Game
{
    /// <summary>
    /// Represents a single running game
    /// </summary>
    public sealed partial class Game
    {
        private readonly Guid _gameId;
        private readonly List<Player.Player> _players;
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

        internal bool PlayerCanJoin() => _allowedPlayerCount.Max - _players.Count > 0;

        internal void Join(Player.Player player)
        {
            if (!PlayerCanJoin())
                throw new ArgumentOutOfRangeException(nameof(player), "A player cannot join a crowded game.");

            if(!Equals(player.AssignedGame))
                throw new ArgumentException("A player can join only the game assigned to him.", nameof(player));

            _players.Add(player);
        }

        internal void DoTick() => throw new NotImplementedException();

        public override bool Equals(object obj) => obj is Game game && _gameId.Equals(game._gameId);

        public override int GetHashCode() => HashCode.Combine(_gameId);
    }
}
