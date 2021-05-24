using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game;
using WeepingSnake.Game.Geometry;
using WeepingSnake.Game.Player;

namespace WeepingSnake.ConsoleClient.Navigation
{
    public class GamePage : IUserInterface<(GameController, Player)>
    {
        private readonly List<Player> _knownPlayers = new();

        private GameController _gameController;
        private Player _player;
        private Game.Game _currentGame;
        private (int Left, int Top) _gamefieldStartCursorPosition;

        public void Open((GameController, Player) data)
        {
            _gameController = data.Item1;
            _player = data.Item2;
            _currentGame = _player.AssignedGame as Game.Game;
            PrintPage();
        }

        public void PrintPage()
        {
            ClearConsole();

            _currentGame.OnLoopTick += PrintGameState;

            var nextAction = ProcessInput();
            nextAction();
        }

        public Action ProcessInput()
        {
            ConsoleKeyInfo pressedKey;
            while (_player.IsAlive && (pressedKey = Console.ReadKey()).Key != ConsoleKey.Escape)
            {
                _gameController.DoAction(_player, pressedKey.Key switch
                {
                    ConsoleKey.DownArrow => PlayerAction.Action.SLOW_DOWN,
                    ConsoleKey.UpArrow => PlayerAction.Action.SPEED_UP,
                    ConsoleKey.RightArrow => PlayerAction.Action.TURN_RIGHT,
                    ConsoleKey.LeftArrow => PlayerAction.Action.TURN_LEFT,
                    ConsoleKey.Spacebar => PlayerAction.Action.JUMP,
                    _ => PlayerAction.Action.CHANGE_NOTHING
                });
            }

            _currentGame.OnLoopTick -= PrintGameState;
            _currentGame.Leave(_player);
            Console.WriteLine();
            Console.WriteLine("Game Over. Press any key.");
            Console.ReadKey();

            return () =>
            {
                if (_player.IsGuest)
                {
                    new StartPage().Open(_gameController);
                }
                else
                {
                    new UserPage().Open((_gameController, _player.ControllingPerson));
                }
            };
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void PrintGameState(List<GameDistance> allPlayerPaths)
        {
            string[,] gameField = new string[_currentGame.GameBoard.Width, _currentGame.GameBoard.Height];

            foreach (var path in allPlayerPaths)
            {
                if (!path.Player.IsAlive)
                {
                    continue;
                }

                var playerNumber = _knownPlayers.IndexOf(path.Player as Player);

                if (playerNumber == -1)
                {
                    if (_knownPlayers.Count == 10)
                    {
                        _currentGame.Leave(_player);
                    }
                    else
                    {
                        ClearConsole();
                        playerNumber = _knownPlayers.Count;
                        _knownPlayers.Add(path.Player as Player);
                    }
                }

                var points = PointsInRectangle((int)path.StartX, (int)path.StartY, (int)path.EndX, (int)path.EndY);

                foreach (var point in points)
                {
                    gameField[point.Item1, point.Item2] = $"{playerNumber} ";
                }
            }


            var stringField = "";
            for (int line = ((int)_currentGame.GameBoard.Height - 1); line >= 0; line--)
            {
                for (int row = 0; row < _currentGame.GameBoard.Width; row++)
                {
                    stringField += gameField[row, line] == null ? "- " : gameField[row, line];
                }

                stringField += "\b\r\n";
            }

            Console.SetCursorPosition(_gamefieldStartCursorPosition.Left, _gamefieldStartCursorPosition.Top);

            for(int playerNumber = 0; playerNumber < _knownPlayers.Count; playerNumber++)
            {
                var player = _knownPlayers[playerNumber];
                var playerPointString = $"Player{playerNumber,4}:";


                if (player.IsAlive)
                {
                    playerPointString += $"{player.Points + " Points",15}";
                }
                else
                {
                    playerPointString += $"{"DEAD",15}";
                }

                var name = player.PlayerId == _player.PlayerId ? "(you)" : "";
                playerPointString += $"{name,10}";

                Console.WriteLine(playerPointString);
            }

            Console.Write(stringField);
        }

        private void ClearConsole()
        {
            Console.Clear();

            Console.WriteLine("Turn: [Arrow Left] [Arrow Right] | Change Speed: [Arrow Up] [Arrow Down] | Jump: [Space] | Exit: [Esc]");
            Console.WriteLine("======================================================================================================");
            _gamefieldStartCursorPosition = Console.GetCursorPosition();
        }

        public static IEnumerable<(int, int)> PointsInRectangle(int aX, int aY, int bX, int bY)
        {
            int xFrom = Math.Min(aX, bX);
            int xTo = Math.Max(aX, bX);
            int yFrom = Math.Min(aY, bY);
            int yTo = Math.Max(aY, bY);

            for (int xPosition = xFrom; xPosition <= xTo; xPosition++)
            {
                for (int yPosition = yFrom; yPosition <= yTo; yPosition++)
                {
                    yield return (xPosition, yPosition);
                }
            }
        }
    }
}
