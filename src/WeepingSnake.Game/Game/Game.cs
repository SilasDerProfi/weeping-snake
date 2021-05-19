using System;
using System.Collections.Generic;
using System.Linq;
using WeepingSnake.Game.Geometry;
using WeepingSnake.Game.Player;
using WeepingSnake.Game.Player.ComputerPlayer;
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

            for(int playerNo = 0; playerNo < allowedPlayerCount.Max; playerNo++)
            {
                var bot = new Player.Player(new RandomPlayer());
                bot.Join(this);
            }

        }

        public Guid GameId => _gameId;

        public Board GameBoard => _board;

        public List<List<GameDistance>> BoardPaths => _board.Paths;

        internal bool IsFullForHumans()
        {
            return _allowedPlayerCount.Max == _players.Count(player => player.IsHuman);
        }

        internal bool IsFullForHumansOrBots()
        {
            return _allowedPlayerCount.Max == _players.Count();
        }

        internal PlayerOrientation Join(Player.Player player)
        {
            if (IsFullForHumans() && player.IsHuman || IsFullForHumansOrBots() && !player.IsHuman)
                throw new ArgumentOutOfRangeException(nameof(player), "A player cannot join a full game.");

            if(!Equals(player.AssignedGame))
                throw new ArgumentException("A player can join only the game assigned to him.", nameof(player));

            if (player.IsHuman && IsFullForHumansOrBots())
            {
                var firstFoundBot = _players.FirstOrDefault(player => !player.IsHuman);
                firstFoundBot.Die();
                _players.Remove(firstFoundBot);
            }

            _players.Add(player);

            return _board.CalculateRandomStartOrientation();
        }

        internal void Leave(Player.Player player)
        {
            if (_players.Contains(player))
            {
                _players.Remove(player);
            }
            else
            {
                throw new ArgumentException("A player can leave only the game he is participating.", nameof(player));
            }
        }

        internal void ApplyOneActionPerPlayer()
        {
            for(int playerIndex = _players.Count - 1; playerIndex >= 0; playerIndex--)
            {
                var player = _players[playerIndex];

                var action = player.PopNextAction();
                _board.ApplyAction(action);
            }

            var currentRoundNumber = _board.Paths.Count;
            var playerLinesStartingRoundIndex = Math.Max(0, currentRoundNumber - 6);
            var roundsWithPlayerLines = currentRoundNumber - playerLinesStartingRoundIndex;


            OnLoopTick?.Invoke(_board.Paths.GetRange(playerLinesStartingRoundIndex, roundsWithPlayerLines).SelectMany(paths => paths).ToList());
        }

        public delegate void LoopTickHandler(List<GameDistance> playerPaths);
        public event LoopTickHandler OnLoopTick;

        public override bool Equals(object obj) => obj is Game game && _gameId.Equals(game._gameId);

        public override int GetHashCode() => HashCode.Combine(_gameId);
    }
}
