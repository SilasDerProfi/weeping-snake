using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeepingSnake.Game.Utility.Logging
{
    internal class GameControllerLogger : IDisposable
    {
        private string _logPath;
        private GameController _gameControler;

        internal GameControllerLogger(GameController gameController)
        {
            if (GameConfiguration.IsLoggingEnabled)
            {
                var currentTime = DateTime.Now;
                var currentTimeStamp = $"{currentTime.Year}-{currentTime.Month}-{currentTime.Day}-{currentTime.Hour}-{currentTime.Minute}-{currentTime.Second}-{currentTime.Millisecond}";

                _logPath = Path.Combine(GameConfiguration.DefaultLoggingDirectory, currentTimeStamp + GameConfiguration.DefaultLoggingPathExtension);
                new StreamWriter(_logPath).Close();

                _gameControler = gameController;
            }
        }

        internal void InitializedGame(Game newGame)
        {
            if (GameConfiguration.IsLoggingEnabled)
            {
                var currentTime = DateTime.Now;
                var currentTimeStamp = $"{currentTime.Year}-{currentTime.Month}-{currentTime.Day}-{currentTime.Hour}-{currentTime.Minute}-{currentTime.Second}-{currentTime.Millisecond}";

                using (var outputFile = new StreamWriter(_logPath, true))
                {
                    outputFile.WriteLine(currentTimeStamp + " " + "INITILIZED NEW GAME: " + newGame.GameId + " (SEE MORE DETAILS IN THE GAME-LOG-FILE)");
                }
            }
        }

        internal void StartedGameLoop()
        {
            if (GameConfiguration.IsLoggingEnabled)
            {
                var currentTime = DateTime.Now;
                var currentTimeStamp = $"{currentTime.Year}-{currentTime.Month}-{currentTime.Day}-{currentTime.Hour}-{currentTime.Minute}-{currentTime.Second}-{currentTime.Millisecond}";

                using (var outputFile = new StreamWriter(_logPath, true))
                {
                    outputFile.WriteLine(currentTimeStamp + " " + "STARTED Gameloop");
                }
            }
        }

        internal void JoinedGame(Player.Player player)
        {
            if (GameConfiguration.IsLoggingEnabled)
            {
                var currentTime = DateTime.Now;
                var currentTimeStamp = $"{currentTime.Year}-{currentTime.Month}-{currentTime.Day}-{currentTime.Hour}-{currentTime.Minute}-{currentTime.Second}-{currentTime.Millisecond}";

                using (var outputFile = new StreamWriter(_logPath, true))
                {
                    outputFile.WriteLine(currentTimeStamp + " " + "PLAYER " + player?.PlayerId + " JOINED THE GAME " + player?.AssignedGame?.GameId);
                }
            }
        }

        public void Dispose()
        {
            if (GameConfiguration.IsLoggingEnabled)
            {
                using (var outputFile = new StreamWriter(_logPath, true))
                {
                    outputFile.WriteLine("disposed");
                }
            }

            GC.SuppressFinalize(this);
        }
    }
}
