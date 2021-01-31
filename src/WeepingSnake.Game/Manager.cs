using System;
using System.Linq;
using System.Collections.Generic;
using WeepingSnake.Game.Utility.Extensions;

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

    /// <summary>
    /// A struct to encapsulate the dimensions and the behavior of the game board
    /// </summary>
    public struct BoardDimensions
    {
        private readonly uint _width;
        private readonly uint _height;
        private readonly bool _isInfinite;

        public BoardDimensions(uint width, uint height, bool isInfinite = true)
        {
            if (height == 0 || width == 0)
                throw new ArgumentException("The width and height of the game board must be greater than 0.");

            _width = width;
            _height = height;
            _isInfinite = isInfinite;
        }

        public uint Width => _width;
        public uint Height => _height;
        public bool IsInfinite => _isInfinite;

        public override bool Equals(object obj)
        {
            return obj is BoardDimensions dimensions &&
                   _width == dimensions._width &&
                   _height == dimensions._height &&
                   _isInfinite == dimensions._isInfinite;
        }

        public override int GetHashCode() => HashCode.Combine(_width, _height, _isInfinite);

        public static bool operator ==(BoardDimensions left, BoardDimensions right) => left.Equals(right);

        public static bool operator !=(BoardDimensions left, BoardDimensions right) => !(left == right);
    }

    /// <summary>
    /// A struct to encapsulate the allowed number of players per game
    /// </summary>
    public struct PlayerRange
    {
        private readonly ushort _min;
        private readonly ushort _max;
        
        public PlayerRange(ushort min, ushort max)
        {
            if (min < max || min < 1)
                throw new ArgumentException("At least 1 player must be allowed.");

            _min = min;
            _max = max;
        }


        public int Min => _min;
        public int Max => _max;

        public override bool Equals(object obj)
        {
            return obj is PlayerRange range &&
                   _min == range._min &&
                   _max == range._max;
        }

        public override int GetHashCode() => HashCode.Combine(_min, _max);

        public override string ToString() => $"[{_min}; {_max}]";

        public static bool operator ==(PlayerRange left, PlayerRange right) => left.Equals(right);

        public static bool operator !=(PlayerRange left, PlayerRange right) => !(left == right);
    }
}
