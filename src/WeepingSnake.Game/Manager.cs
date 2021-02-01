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
    public sealed class Manager
    {
        private readonly PlayerRange _allowedPlayerCount;
        private readonly BoardDimensions _boardDimensions;
        private readonly List<Game> _games;

        public Manager(PlayerRange allowedPlayerCount, BoardDimensions boardDimensions)
        {
            _allowedPlayerCount = allowedPlayerCount;
            _boardDimensions = boardDimensions;
            _games = new List<Game>();
        }


        public Game CreateGame() => _games.AddAndReturn(new Game(_allowedPlayerCount, _boardDimensions));

        public Player.Player JoinGame() => JoinGame(null, null);

        public Player.Player JoinGame(Person.Person person) => JoinGame(person, null);

        public Player.Player JoinGame(Game game) => JoinGame(null, game);

        public Player.Player JoinGame(Person.Person person, Game game)
        {
            game ??= _games.FirstOrDefault(game => game.PlayerCanJoin()) ?? CreateGame();

            return new Player.Player(person, game);
        }
    }
}
