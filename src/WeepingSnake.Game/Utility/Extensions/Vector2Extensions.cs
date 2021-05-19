using System;
using System.Numerics;

namespace WeepingSnake.Game.Utility.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 RotateLeft(this Vector2 v, int degrees)
        {
            var radians = Math.PI / 180 * degrees;

            return new Vector2(
                (float)(v.X * Math.Cos(radians) - v.Y * Math.Sin(radians)),
                (float)(v.X * Math.Sin(radians) + v.Y * Math.Cos(radians)));
        }

        public static Vector2 RotateRight(this Vector2 v, int degrees) => RotateLeft(v, -degrees);

        public static Vector2 Increase(this Vector2 v)
        {
            var length = v.Length();
            var desiredLength = length + GameConfiguration.MinSpeedIncrement;

            var unitvector = new Vector2(v.X / length, v.Y / length);
            return new Vector2(unitvector.X * desiredLength, unitvector.Y * desiredLength);
        }

        public static Vector2 Decrease(this Vector2 v)
        {
            var length = v.Length();
            var desiredLength = length - GameConfiguration.MinSpeedIncrement;
            if (desiredLength <= 0)
                desiredLength = GameConfiguration.DefaultDistance;

            var unitvector = new Vector2(v.X / length, v.Y / length);
            return new Vector2(unitvector.X * desiredLength, unitvector.Y * desiredLength);
        }

        public static Vector2 UnitVector(this Vector2 v)
        {
            var length = v.Length();
            return new Vector2(v.X / length, v.Y / length);
        }

        public static Vector2 DefaultDistanceVector(this Vector2 v)
        {
            var unitVector = v.UnitVector();
            return new Vector2(unitVector.X * GameConfiguration.DefaultDistance, unitVector.Y * GameConfiguration.DefaultDistance);
        }

    }
}
