using System;
using System.Linq;
using System.Collections.Generic;
using WeepingSnake.Game.Utility.Extensions;
using WeepingSnake.Game.Structs;

namespace WeepingSnake.Game
{
    /// <summary>
    /// Manages multiple games and the players participating in them.
    /// </summary>
    public sealed class GameController : IDisposable
    {
        private readonly PlayerRange _allowedPlayerCount;
        private readonly BoardDimensions _boardDimensions;
        private readonly List<Game> _games;
        private readonly GameLoop _gameLoop;

        public GameController(PlayerRange allowedPlayerCount, BoardDimensions boardDimensions)
        {
            _allowedPlayerCount = allowedPlayerCount;
            _boardDimensions = boardDimensions;

            _games = new List<Game>();
            _gameLoop = new GameLoop(ref _games);
            _gameLoop.RunAsync();
        }

        public void Dispose() => _gameLoop.Dispose();

        private Game InitializeGame() => _games.AddAndReturn(new Game(_allowedPlayerCount, _boardDimensions));

        public Player.Player JoinGame() => JoinGame(null, null);

        public Player.Player JoinGame(Person.Person person) => JoinGame(person, null);

        public Player.Player JoinGame(Game game) => JoinGame(null, game);

        public Player.Player JoinGame(Person.Person person, Game game)
        {
            game ??= _games.FirstOrDefault(game => !game.IsFullForHumans()) ?? InitializeGame();

            var player = new Player.Player(person);
            player.Join(game);

            return player;
        }

        public void DoAction(Player.Player player, Player.PlayerAction.Action action) => player.AddAction(action);

        public Player.Player FindPlayer(Guid playerId)
        {
            Player.Player foundPlayer = null;

            foreach(var game in _games)
            {
                foreach(var player in game.Players)
                {
                    if(player.PlayerId == playerId)
                    {
                        foundPlayer = player;
                        break;
                    }
                }
            }

            if(foundPlayer == null)
            {
                return null;
            }
            else
            {
                return foundPlayer;
            }
        }
    }
}
