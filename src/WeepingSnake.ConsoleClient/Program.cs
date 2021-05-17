using System;
using System.Collections.Generic;
using WeepingSnake.Game.Geometry;

namespace WeepingSnake.ConsoleClient
{
    static class Program
    {
        static void Main(string[] args)
        {
            var gctrl = new Game.GameController(2, 50);

            var playerA = gctrl.JoinGame();
            playerA.AssignedGame.OnLoopTick += PrintGameState;
            gctrl.DoAction(playerA, Game.Player.PlayerAction.Action.SPEED_UP);
            Console.ReadLine();
        }

        private static void PrintGameState(List<GameDistance> newPaths)
        {
            Console.WriteLine($"New Gamestate with {newPaths.Count} new Paths");
        }
    }
}
