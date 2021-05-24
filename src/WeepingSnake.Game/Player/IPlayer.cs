using System;
using System.Collections.Generic;
using WeepingSnake.Game.Geometry;

namespace WeepingSnake.Game.Player
{
    public interface IPlayer
    {
        Game AssignedGame { get; }
        Person.Person ControllingPerson { get; }
        bool IsAlive { get; }
        bool IsGuest { get; }
        bool IsHuman { get; }
        PlayerOrientation Orientation { get; }
        Guid PlayerId { get; }
        int Points { get; set; }

        void AddAction(PlayerAction.Action action);
        void AddActions(Queue<PlayerAction.Action> actions);
        GameDistance? ApplyOrientationAndMove(PlayerOrientation newOrientation);
        void ApplyPointsToPerson();
        void Die();
        void Join(Game game);
        PlayerAction PopNextAction();
    }
}