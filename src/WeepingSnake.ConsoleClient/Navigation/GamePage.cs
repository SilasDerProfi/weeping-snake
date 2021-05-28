using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.ConsoleClient.IO;
using WeepingSnake.Game;
using WeepingSnake.Game.Geometry;
using WeepingSnake.Game.Player;

namespace WeepingSnake.ConsoleClient.Navigation
{
    public class GamePage : UserInterface<(GameController, Player)>
    {
        private readonly List<Player> _knownPlayers = new();

        private Game.Game _currentGame;
        private (int Left, int Top) _gamefieldStartCursorPosition;

        public GamePage((GameController, Player) data, IOHandler ioHandler) : base (data, ioHandler)
        {
            _currentGame = GetPlayer().AssignedGame as Game.Game;
        }

        private GameController GetGameController()
        {
            return Data.Item1;
        }

        private Player GetPlayer()
        {
            return Data.Item2;
        }


        internal override void OpenAndPrintPage()
        {
            ClearOutput();

            _currentGame.OnLoopTick += PrintGameState;

            var nextAction = ProcessInput();
            nextAction();
        }

        protected override Action ProcessInput()
        {
            ConsoleKey pressedKey;
            while (GetPlayer().IsAlive && (pressedKey = InOut.ReadKey()) != ConsoleKey.Escape)
            {
                GetGameController().DoAction(GetPlayer(), pressedKey switch
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
            _currentGame.Leave(GetPlayer());
            InOut.WriteLine();
            InOut.WriteLine("Game Over. Press any key.");
            InOut.ReadKey();

            return () =>
            {
                if (GetPlayer().IsGuest)
                {
                    new StartPage(GetGameController(), InOut).OpenAndPrintPage();
                }
                else
                {
                    new UserPage((GetGameController(), GetPlayer().ControllingPerson), InOut).OpenAndPrintPage();
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
                        _currentGame.Leave(GetPlayer());
                    }
                    else
                    {
                        ClearOutput();
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

            InOut.SetCursorPosition(_gamefieldStartCursorPosition.Left, _gamefieldStartCursorPosition.Top);

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

                var name = player.PlayerId == GetPlayer().PlayerId ? "(you)" : "";
                playerPointString += $"{name,10}";

                InOut.WriteLine(playerPointString);
            }

            InOut.Write(stringField);
        }

        private void ClearOutput()
        {
            InOut.Clear();

            InOut.WriteLine("Turn: [Arrow Left] [Arrow Right] | Change Speed: [Arrow Up] [Arrow Down] | Jump: [Space] | Exit: [Esc]");
            InOut.WriteLine("======================================================================================================");
            _gamefieldStartCursorPosition = InOut.GetCursorPosition();
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
