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
        private readonly StreamWriter _outputFile;
        private readonly Game _game;

        internal GameInformationLogger(Game game)
        {
            if (GameConfiguration.IsLoggingEnabled)
            {
                _loggedPaths = new List<Geometry.GameDistance>();
                _logPath = Path.Combine(GameConfiguration.DefaultLoggingDirection, game.GameId.ToString(), GameConfiguration.DefaultLoggingPathExtension);
                _outputFile = new StreamWriter(_logPath);
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
            _outputFile.Write(DateTime.UtcNow.ToLongTimeString());
            _outputFile.Write(" | LOGGING FOR GAME: ");
            _outputFile.WriteLine(_game.GameId);
            _outputFile.WriteLine();
            var boardInformation = String.Format("HEIGHT = {0}; WIDTH = {1}", _game.GameBoard.Height, _game.GameBoard.Width);
            _outputFile.Write("BOARD DIMENSIONS: ");
            _outputFile.WriteLine(boardInformation);
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

            _outputFile.WriteLine(logString);
        }

        public void Dispose()
        {
            _outputFile?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
