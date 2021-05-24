using System;
using System.Collections.Generic;
using WeepingSnake.Game.Geometry;
using WeepingSnake.Game.Player;

namespace WeepingSnake.Game
{
    public interface IGame
    {
        List<List<GameDistance>> BoardPaths { get; }
        Game.Board GameBoard { get; }
        Guid GameId { get; }
        bool IsActive { get; }
        List<Player.Player> Players { get; }

        void ApplyOneActionPerPlayer();
        int GetHashCode();
        bool IsFullForHumans();
        bool IsFullForHumansOrBots();
        PlayerOrientation Join(Player.Player player);
        void Leave(Player.Player player);
    }
}