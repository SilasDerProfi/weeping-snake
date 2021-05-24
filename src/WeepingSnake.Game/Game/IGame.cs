using System;
using System.Collections.Generic;
using WeepingSnake.Game.Geometry;
using WeepingSnake.Game.Player;

namespace WeepingSnake.Game
{
    public interface IGame
    {
        List<List<GameDistance>> BoardPaths { get; }
        CoordinateSystem GameBoard { get; }
        Guid GameId { get; }
        bool IsActive { get; }
        List<IPlayer> Players { get; }

        void ApplyOneActionPerPlayer();
        int GetHashCode();
        bool IsFullForHumans();
        bool IsFullForHumansOrBots();
        PlayerOrientation Join(IPlayer player);
        void Leave(IPlayer player);
    }
}