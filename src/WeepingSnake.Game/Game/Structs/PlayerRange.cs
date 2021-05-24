using System;

namespace WeepingSnake.Game.Structs
{
    /// <summary>
    /// A struct to encapsulate the allowed number of players per game
    /// </summary>
    public struct PlayerRange
    {
        private readonly ushort _min;
        private readonly ushort _max;

        public PlayerRange(ushort min, ushort max)
        {
            if (min > max || min < 1)
            {
                throw new ArgumentException("At least 1 player must be allowed.");
            }

            _min = min;
            _max = max;
        }


        public int Min
        {
            get
            {
                return _min;
            }
        }

        public int Max
        {
            get
            {
                return _max;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is PlayerRange range &&
                   _min == range._min &&
                   _max == range._max;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_min, _max);
        }

        public override string ToString()
        {
            return $"[{Min} ; {Max}]";
        }

        public static bool operator ==(PlayerRange left, PlayerRange right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PlayerRange left, PlayerRange right)
        {
            return !(left == right);
        }

        public static implicit operator PlayerRange(ushort playerCount)
        {
            return new(playerCount, playerCount);
        }
    }
}
