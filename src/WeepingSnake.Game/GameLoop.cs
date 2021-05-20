﻿using System;
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
        private void RunInfinite()
        {
            while(_games.Count == 0)
            {
                Thread.Sleep(1000);
            }

            Parallel.ForEach(_games.GetInfiniteEnumerator(), game =>
            {
                lock (game)
                {
                    game.ApplyOneActionPerPlayer();
                    Thread.Sleep(GameConfiguration.RoundDuration);
                }
            });
        }

        public void Dispose() => _loopTask.Dispose();
    }
}
