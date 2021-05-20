using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeepingSnake.Game.Person
{
    public class HighscoreEntry
    {
        private readonly string _username;
        private readonly int _playedGames;
        private readonly int _maximumPointsInGame;
        private readonly int _totalPoints;

        public HighscoreEntry(string username, int playedGames, int maximumPointsInGame, int totalPoints)
        {
            _username = username;
            _playedGames = playedGames;
            _maximumPointsInGame = maximumPointsInGame;
            _totalPoints = totalPoints;
        }

        public string Username
        {
            get
            {
                return _username;
            }
        }

        public int PlayedGames
        {
            get
            {
                return _playedGames;
            }
        }

        public int MaximumPointsInGame
        {
            get
            {
                return _maximumPointsInGame;
            }
        }

        public int TotalPoints
        {
            get
            {
                return _totalPoints;
            }
        }
    }
}
