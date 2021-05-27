using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WeepingSnake.Game.Utility.Logging
{
    internal class GameInformationLogger : IDisposable
    {
        private List<Geometry.GameDistance> _loggedPaths;
        private string _logPath;
        private readonly Game _game;

        internal GameInformationLogger(Game game)
        {
            if (GameConfiguration.IsLoggingEnabled)
            {
                _loggedPaths = new List<Geometry.GameDistance>();
                _logPath = Path.Combine(GameConfiguration.DefaultLoggingDirectory, game.GameId.ToString() + GameConfiguration.DefaultLoggingPathExtension);
                new StreamWriter(_logPath).Close();
                _game = game;

                LogGameInformation();

                game.OnLoopTick += Log_GameRound;
            }
        }

        ~GameInformationLogger()
        {
            Dispose();
        }


        private void LogGameInformation()
        {
            using (var outputFile = new StreamWriter(_logPath, true))
            {
                outputFile.Write(DateTime.UtcNow.ToLongTimeString());
                outputFile.Write(" | LOGGING FOR GAME: ");
                outputFile.WriteLine(_game.GameId);
                outputFile.WriteLine();
                var boardInformation = String.Format("HEIGHT = {0}; WIDTH = {1}", _game.GameBoard.Height, _game.GameBoard.Width);
                outputFile.Write("BOARD DIMENSIONS: ");
                outputFile.WriteLine(boardInformation);
            }
        }


        private void Log_GameRound(List<Geometry.GameDistance> playerPaths)
        {
            foreach(var playerPath in playerPaths)
            {
                if (_loggedPaths.Contains(playerPath))
                {
                    continue;
                }
                else
                {
                    LogPath(playerPath);
                    _loggedPaths.Add(playerPath);
                }
            }
        }

        private void LogPath(Geometry.GameDistance playerPath)
        {
            var logText = new StringBuilder();

            logText.Append(DateTime.UtcNow.ToLongTimeString());
            logText.Append("| PLAYER PATH FOR PLAYER: ");
            logText.Append(playerPath.Player.PlayerId);

            logText.AppendFormat(" [ ({0};{1}) | ({2};{3}) ]",
                playerPath.StartX,
                playerPath.StartY,
                playerPath.EndX, 
                playerPath.EndY);

            var logString = logText.ToString();
            using (var outputFile = new StreamWriter(_logPath, true))
            {
                outputFile.WriteLine(logString);
            }
        }

        public void Dispose()
        {
            if (GameConfiguration.IsLoggingEnabled)
            {
                using (var outputFile = new StreamWriter(_logPath, true))
                {
                    outputFile.Write(DateTime.UtcNow.ToLongTimeString());
                    outputFile.WriteLine("disposed");
                }
            }

            GC.SuppressFinalize(this);
        }
    }
}
