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
            _currentGame = _player.AssignedGame;
            PrintPage();
        }

        public void PrintPage()
        {
            Console.Clear();

            Console.WriteLine("Turn: [Arrow Left] [Arrow Right] | Change Speed: [Arrow Up] [Arrow Down] | Jump: [Space] | Exit: [Esc]");
            Console.WriteLine("======================================================================================================");
            _gamefieldStartCursorPosition = Console.GetCursorPosition();

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
            Console.WriteLine();
            Console.WriteLine("GGame Over. Press any key.");
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
            string[,] gameField = new string[20, 20];

            foreach (var path in allPlayerPaths)
            {
                if (!path.Player.IsAlive)
                    continue;

                var playerNumber = _knownPlayers.IndexOf(path.Player);

                if (playerNumber == -1)
                {
                    playerNumber = _knownPlayers.Count;
                    _knownPlayers.Add(path.Player);
                }

                var points = PointsInRectangle((int)path.StartX, (int)path.StartY, (int)path.EndX, (int)path.EndY);

                foreach (var point in points)
                {
                    gameField[point.Item1, point.Item2] = $"{playerNumber} ";
                }
            }


            var stringField = "";
            for (int line = 19; line >= 0; line--)
            {
                for (int row = 0; row < 20; row++)
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


        private static IEnumerable<(int, int)> PointsInRectangle(int aX, int aY, int bX, int bY)
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
