using System;
using System.Linq;
using System.Collections.Generic;
using WeepingSnake.Game.Utility.Extensions;
using WeepingSnake.Game.Structs;
using WeepingSnake.Game.Utility.Logging;

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
        private readonly GameControllerLogger _logger;

        public GameController(PlayerRange allowedPlayerCount, BoardDimensions boardDimensions)
        {
            _allowedPlayerCount = allowedPlayerCount;
            _boardDimensions = boardDimensions;

            _games = new List<Game>();
            _gameLoop = new GameLoop(ref _games);
            _logger = new GameControllerLogger(this);
        }

        public void Dispose()
        {
            _gameLoop.Dispose();
            _logger.Dispose();
            
            foreach(var game in _games)
            {
                game.Dispose();
            }
        }
        private Game InitializeGame()
        {
            var newGame = _games.AddAndReturn(new Game(_allowedPlayerCount, _boardDimensions));
            
            _logger.InitializedGame(newGame);

            if (_games.Count == 1)
            {
                _gameLoop.RunAsync();
                _logger.StartedGameLoop();
            }

            return newGame;
        }

        public Player.Player JoinGame() => JoinGame(null, null);

        public Player.Player JoinGame(Person.Person person) => JoinGame(person, null);

        public Player.Player JoinGame(Game game) => JoinGame(null, game);

        public Player.Player JoinGame(Person.Person person, Game game)
        {
            if(game is null)
            {
                foreach(var possibleGame in _games)
                {
                    if(!possibleGame.IsFullForHumans() && possibleGame.IsActive)
                    {
                        game = possibleGame;
                        break;
                    }
                }

                if(game is null)
                {
                    game = InitializeGame();
                }
            }


            var player = new Player.Player(person);
            player.Join(game);

            _logger.JoinedGame(player);

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
                        foundPlayer = player as Player.Player;
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
