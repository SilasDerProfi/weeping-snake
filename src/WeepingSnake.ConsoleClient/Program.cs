using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using WeepingSnake.ConsoleClient.Navigation;
using WeepingSnake.Game.Geometry;
using WeepingSnake.Game.Structs;

namespace WeepingSnake.ConsoleClient
{
    static class Program
    {
        private static List<object> _knownPlayers = new List<object>();

        static void Main(string[] args)
        {
            var playerRange = new PlayerRange(4, 6);
            var boardDimensions = new BoardDimensions(20, 20);
            var gameController = new Game.GameController(playerRange, boardDimensions);

            var startPage = new StartPage();
            startPage.Open(gameController);




            //var playerA = gctrl.JoinGame();
            //gctrl.DoAction(playerA, Game.Player.PlayerAction.Action.JUMP);
            ////gctrl.DoAction(playerA, Game.Player.PlayerAction.Action.TURN_RIGHT);
            ////gctrl.DoAction(playerA, Game.Player.PlayerAction.Action.TURN_RIGHT);
            ////gctrl.DoAction(playerA, Game.Player.PlayerAction.Action.TURN_RIGHT);
            //var assignedGame = playerA.AssignedGame;
            //assignedGame.OnLoopTick += PrintGameState;

            //ConsoleKeyInfo pressedKey;
            //while (playerA.IsAlive && (pressedKey = Console.ReadKey()).Key != ConsoleKey.Escape)
            //{
            //    gctrl.DoAction(playerA, pressedKey.Key switch
            //    {
            //        ConsoleKey.DownArrow => Game.Player.PlayerAction.Action.SLOW_DOWN,
            //        ConsoleKey.UpArrow => Game.Player.PlayerAction.Action.SPEED_UP,
            //        ConsoleKey.RightArrow => Game.Player.PlayerAction.Action.TURN_RIGHT,
            //        ConsoleKey.LeftArrow => Game.Player.PlayerAction.Action.TURN_LEFT,
            //        ConsoleKey.Spacebar => Game.Player.PlayerAction.Action.JUMP,
            //        _ => Game.Player.PlayerAction.Action.CHANGE_NOTHING
            //    });
            //}

            //assignedGame.OnLoopTick -= PrintGameState;
            //Console.WriteLine("Game Over");
            //Console.Read();
        }

        //[MethodImpl(MethodImplOptions.Synchronized)]
        //private static void PrintGameState(List<GameDistance> allPlayerPaths)
        //{
        //    string[,] gameField = new string[20, 20];

        //    foreach(var path in allPlayerPaths)
        //    {
        //        if (!path.Player.IsAlive)
        //            continue;

        //        var playerNumber = _knownPlayers.IndexOf(path.Player);

        //        if(playerNumber == -1)
        //        {
        //            playerNumber = _knownPlayers.Count;
        //            _knownPlayers.Add(path.Player);
        //        }

        //        var points = PointsInRectangle((int)path.StartX, (int)path.StartY, (int)path.EndX, (int)path.EndY);

        //        foreach(var point in points)
        //        {
        //            gameField[point.Item1, point.Item2] = $"{playerNumber} ";
        //        }
        //    }


        //    var stringField = "";
        //    for(int line = 19; line >= 0; line--)
        //    {
        //        for(int row = 0; row < 20; row++)
        //        {
        //            stringField += gameField[row, line] == null ? "- " : gameField[row, line];
        //        }
        //        stringField += "\b\r\n";
        //    }

        //    Console.Clear();
        //    Console.WriteLine(allPlayerPaths.FirstOrDefault().Player.Points);
        //    Console.Write(stringField);
        //}


        //public static IEnumerable<(int, int)> PointsInRectangle(int aX, int aY, int bX, int bY)
        //{
        //    int xFrom = Math.Min(aX, bX);
        //    int xTo = Math.Max(aX, bX);
        //    int yFrom = Math.Min(aY, bY);
        //    int yTo = Math.Max(aY, bY);

        //    for (int xPosition = xFrom; xPosition <= xTo; xPosition++)
        //        for (int yPosition = yFrom; yPosition <= yTo; yPosition++)
        //            yield return (xPosition, yPosition);
        //}
    }
}
