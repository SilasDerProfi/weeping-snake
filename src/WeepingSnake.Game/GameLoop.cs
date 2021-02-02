using System;
using System.Collections.Generic;
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
#warning TODO: Check if this Dispose()-Call stops the task correct
            _loopTask?.Dispose();
            _loopTask = Task.Run(() => RunInfinite());
        }

        private void RunInfinite() => Parallel.ForEach(_games.GetInfiniteEnumerator(), game => game.ApplyOneActionPerPlayer());

#warning TODO: Check if this Dispose()-Call stops the task correct
        public void Dispose() => _loopTask.Dispose();
    }
}
