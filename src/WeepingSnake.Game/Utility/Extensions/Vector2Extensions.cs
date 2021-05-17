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

#warning TODO: do not use the 1 hardcoded, but use a const like "speed step"
        public static Vector2 Increase(this Vector2 v)
        {
            var length = v.Length();
            var desiredLength = length + 1;
            var unitvector = new Vector2(v.X / length, v.Y / length);
            return new Vector2(unitvector.X * desiredLength, unitvector.Y * desiredLength);
        }

        public static Vector2 Decrease(this Vector2 v)
        {
            var length = v.Length();
            var desiredLength = length - 1;
            var unitvector = new Vector2(v.X / length, v.Y / length);
            return new Vector2(unitvector.X * desiredLength, unitvector.Y * desiredLength);
        }

    }
}
