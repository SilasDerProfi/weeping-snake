using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeepingSnake.Game.Player.ComputerPlayer
{
    public static class ComputerPlayer
    {
        public static IComputerPlayer GetAnyComputerPlayer()
        {
            var randomNumber = new Random(Guid.NewGuid().GetHashCode()).Next(0, 100);

            if(randomNumber < 10)
            {
                return new DoNothingPlayer();
            }
            else if (randomNumber < 30)
            {
                return new RandomPlayer();
            }
            else
            {
                return new RandomNotKillingItselfPlayer();
            }
        }
    }
}
