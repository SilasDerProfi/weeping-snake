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
