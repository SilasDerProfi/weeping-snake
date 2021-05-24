using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game.Geometry;
using WeepingSnake.Game.Player;
using WeepingSnake.Game.Tests.Geometry;

namespace WeepingSnake.Game.Tests.Game
{
    public class MockGame : IGame
    {
        public List<List<GameDistance>> BoardPaths { get; set; }

        public CoordinateSystem GameBoard { get; set; }

        public Guid GameId { get; set; }

        public bool IsActive { get; set; }

        public List<WeepingSnake.Game.Player.Player> Players { get; set; }

        public void ApplyOneActionPerPlayer()
        {
            throw new NotImplementedException();
        }

        public bool IsFullForHumans()
        {
            throw new NotImplementedException();
        }

        public bool IsFullForHumansOrBots()
        {
            throw new NotImplementedException();
        }

        public PlayerOrientation Join(WeepingSnake.Game.Player.Player player)
        {
            return JoinFunc?.Invoke(player) ?? new PlayerOrientation();
        }

        public void Leave(WeepingSnake.Game.Player.Player player)
        {
            throw new NotImplementedException();
        }


        public Func<WeepingSnake.Game.Player.Player, PlayerOrientation> JoinFunc { get; set; }
    }
}
