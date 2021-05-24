using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeepingSnake.Game.Player.ComputerPlayer
{
    public static class CreateComputerPlayer
    {
        public static void CreateForGame(Game gameToParticipate)
        {
            var randomNumber = new Random(Guid.NewGuid().GetHashCode()).Next(0, 100);

            IComputerPlayer computerType;

            if(randomNumber < 10)
            {
                computerType =  new DoNothingPlayer();
            }
            else if (randomNumber < 30)
            {
                computerType = new RandomPlayer();
            }
            else
            {
                computerType = new RandomNotKillingItselfPlayer();
            }

            var computerPlayer = new Player(null, false);

            var initialComputerActions = computerType.GenerateInitialActions();

            computerPlayer.AddActions(initialComputerActions);

            computerPlayer.Join(gameToParticipate);
            
            computerType.ControlledPlayer = computerPlayer;
        }
    }
}
