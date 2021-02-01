using System;

namespace WeepingSnake.Game.Structs
{
    /// <summary>
    /// A struct to encapsulate the dimensions and the behavior of the game board
    /// </summary>
    public struct BoardDimensions
    {
        private readonly uint _width;
        private readonly uint _height;
        private readonly bool _isInfinite;

        public BoardDimensions(uint width, uint height, bool isInfinite = true)
        {
            if (height == 0 || width == 0)
                throw new ArgumentException("The width and height of the game board must be greater than 0.");

            _width = width;
            _height = height;
            _isInfinite = isInfinite;
        }

        public uint Width => _width;
        public uint Height => _height;
        public bool IsInfinite => _isInfinite;

        public override bool Equals(object obj)
        {
            return obj is BoardDimensions dimensions &&
                   _width == dimensions._width &&
                   _height == dimensions._height &&
                   _isInfinite == dimensions._isInfinite;
        }

        public override int GetHashCode() => HashCode.Combine(_width, _height, _isInfinite);

        public static bool operator ==(BoardDimensions left, BoardDimensions right) => left.Equals(right);

        public static bool operator !=(BoardDimensions left, BoardDimensions right) => !(left == right);
    }
}
