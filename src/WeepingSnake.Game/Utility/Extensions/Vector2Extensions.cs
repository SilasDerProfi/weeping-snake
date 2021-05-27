using System;
using System.Numerics;

namespace WeepingSnake.Game.Utility.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 RotateLeft(this Vector2 vector, int degrees)
        {
            var radians = Math.PI / 180 * degrees;

            return new Vector2(
                (float)(vector.X * Math.Cos(radians) - vector.Y * Math.Sin(radians)),
                (float)(vector.X * Math.Sin(radians) + vector.Y * Math.Cos(radians)));
        }

        public static Vector2 RotateRight(this Vector2 vector, int degrees) => RotateLeft(vector, -degrees);

        public static Vector2 Increase(this Vector2 vector)
        {
            return Add(vector, GameConfiguration.MinSpeedIncrement);
        }

        public static Vector2 Decrease(this Vector2 vector)
        {
            return Add(vector, -GameConfiguration.MinSpeedIncrement);
        }

        public static Vector2 Add(this Vector2 vector, int lengthSummand)
        {
            var length = vector.Length();
            var desiredLength = length + lengthSummand;
            if (desiredLength <= 0)
                desiredLength = GameConfiguration.DefaultDistance;

            return vector.ChangeLengthTo(desiredLength);
        }


        public static Vector2 UnitVector(this Vector2 v)
        {
            const int unitVectorLength = 1;
            return v.ChangeLengthTo(unitVectorLength);
        }

        public static Vector2 DefaultDistanceVector(this Vector2 vector)
        {
            return vector.ChangeLengthTo(GameConfiguration.DefaultDistance);
        }

        public static Vector2 ChangeLengthTo(this Vector2 vector, float length)
        {
            var actualLength = vector.Length();
            var unitVector = new Vector2(vector.X / actualLength, vector.Y / actualLength);

            return new Vector2(unitVector.X * length, unitVector.Y * length);
        }

    }
}
