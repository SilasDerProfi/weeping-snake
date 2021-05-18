using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using WeepingSnake.Game.Geometry;

namespace WeepingSnake.ConsoleClient
{
    static class Program
    {
        private static string[,] gameField = new string[20,20];

        static void Main(string[] args)
        {
            var gctrl = new Game.GameController(1, new Game.Structs.BoardDimensions(20, 20, false));

            var playerA = gctrl.JoinGame();
            playerA.AssignedGame.OnLoopTick += PrintGameState;

            ConsoleKeyInfo pressedKey;
            while ((pressedKey = Console.ReadKey()).Key != ConsoleKey.Escape)
            {
                gctrl.DoAction(playerA, pressedKey.Key switch
                {
                    ConsoleKey.DownArrow => Game.Player.PlayerAction.Action.SLOW_DOWN,
                    ConsoleKey.UpArrow => Game.Player.PlayerAction.Action.SPEED_UP,
                    ConsoleKey.RightArrow => Game.Player.PlayerAction.Action.TURN_RIGHT,
                    ConsoleKey.LeftArrow => Game.Player.PlayerAction.Action.TURN_LEFT,
                    ConsoleKey.Spacebar => Game.Player.PlayerAction.Action.JUMP,
                    _ => Game.Player.PlayerAction.Action.CHANGE_NOTHING
                });
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void PrintGameState(List<GameDistance> newPaths)
        {
            var points = newPaths.SelectMany(path => PointsInRectangle((int)path.StartX, (int)path.StartY, (int)path.EndX, (int)path.EndY));

            foreach(var point in points)
                gameField[point.Item1, point.Item2] = "#";

            var stringField = "";
            for(int line = 19; line >= 0; line--)
            {
                for(int row = 0; row < 20; row++)
                {
                    stringField += gameField[row, line] == null ? "- " : "# ";
                }
                stringField += "\b\r\n";
            }

            Console.Clear();
            Console.WriteLine($"Points: {newPaths.FirstOrDefault().Player.Points}");
            Console.Write(stringField);
        }


        public static IEnumerable<(int, int)> PointsInRectangle(int aX, int aY, int bX, int bY)
        {
            int xFrom = Math.Min(aX, bX);
            int xTo = Math.Max(aX, bX);
            int yFrom = Math.Min(aY, bY);
            int yTo = Math.Max(aY, bY);

            for (int xPosition = xFrom; xPosition <= xTo; xPosition++)
                for (int yPosition = yFrom; yPosition <= yTo; yPosition++)
                    yield return (xPosition, yPosition);
        }
    }
}
