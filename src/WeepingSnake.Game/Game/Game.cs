using System;
using System.Collections.Generic;
using WeepingSnake.Game.Player;
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
        private readonly Game.Board _board;

        public Game(PlayerRange allowedPlayerCount, BoardDimensions boardDimensions)
        {
            _gameId = Guid.NewGuid();
            _players = new List<Player.Player>();
            _allowedPlayerCount = allowedPlayerCount;
            _board = new Board(boardDimensions);
        }

        public Guid GameId => _gameId;

        internal bool IsFull() => _allowedPlayerCount.Max == _players.Count;

        internal PlayerOrientation Join(Player.Player player)
        {
            if (IsFull())
                throw new ArgumentOutOfRangeException(nameof(player), "A player cannot join a full game.");

            if(!Equals(player.AssignedGame))
                throw new ArgumentException("A player can join only the game assigned to him.", nameof(player));

            _players.Add(player);

            return _board.CalculateRandomStartOrientation();
        }

        internal void ApplyOneActionPerPlayer()
        {
            foreach(var player in _players)
            {
                var action = player.PopAndApplyNextAction();
                _board.ApplyAction(action);
#warning move
            }
            OnLoopTick?.Invoke(this);
        }

        public delegate void LoopTickHandler(Game sender);
        public event LoopTickHandler OnLoopTick;

        public override bool Equals(object obj) => obj is Game game && _gameId.Equals(game._gameId);

        public override int GetHashCode() => HashCode.Combine(_gameId);
    }
}
