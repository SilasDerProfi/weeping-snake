using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeepingSnake.Game.Utility.Extensions;

namespace WeepingSnake.Game
{
    internal sealed class GameLoop : IDisposable
    {
        private readonly List<Game> _games;
        private Task _loopTask;

        internal GameLoop(ref List<Game> games)
        {
            _games = games;
        }

        internal void RunAsync()
        {
            _loopTask?.Dispose();
            _loopTask = Task.Run(() => RunInfinite());
        }

#warning todo: dont use fixed 1000, use the const "round length"
        private void RunInfinite() => Parallel.ForEach(_games.GetInfiniteEnumerator(), game =>
        {
            lock (game)
            {
                game.ApplyOneActionPerPlayer();
                Thread.Sleep(1000);
            }
        });

        public void Dispose() => _loopTask.Dispose();
    }
}
