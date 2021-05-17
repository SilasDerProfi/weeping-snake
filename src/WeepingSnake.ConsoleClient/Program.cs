using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using WeepingSnake.Game.Geometry;

namespace WeepingSnake.ConsoleClient
{
    static class Program
    {
        static void Main(string[] args)
        {
            var gctrl = new Game.GameController(1, 50);

            var playerA = gctrl.JoinGame();
            playerA.AssignedGame.OnLoopTick += PrintGameState;

            gctrl.DoAction(playerA, Game.Player.PlayerAction.Action.CHANGE_NOTHING);
            gctrl.DoAction(playerA, Game.Player.PlayerAction.Action.SPEED_UP);
            gctrl.DoAction(playerA, Game.Player.PlayerAction.Action.TURN_LEFT);
            gctrl.DoAction(playerA, Game.Player.PlayerAction.Action.TURN_RIGHT);
            gctrl.DoAction(playerA, Game.Player.PlayerAction.Action.SPEED_UP);

            Console.ReadLine();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void PrintGameState(List<GameDistance> newPaths)
        {
            Console.WriteLine($"New Gamestate with {newPaths.Count} new Paths:");
            newPaths.ForEach(p => Console.WriteLine($"- ({(int)p.StartX};{(int)p.StartY}) - ({(int)p.EndX};{(int)p.EndY})"));
            
        }
    }
}
