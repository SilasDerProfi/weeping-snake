using System;
using WeepingSnake.Game.Geometry;

namespace WeepingSnake.Game.Structs
{
    /// <summary>
    /// A struct to encapsulate the dimensions and the behavior of the game board
    /// </summary>
    public struct BoardDimensions : ICoordinateSystemDimensions
    {
        private readonly uint _width;
        private readonly uint _height;

        public BoardDimensions(uint width, uint height)
        {
            if (height < 5 || width < 5)
                throw new ArgumentException("The width and height of the game board must be greater than 5.");

            _width = width;
            _height = height;
        }

        public uint Width
        {
            get
            {
                return _width;
            }
        }

        public uint Height
        {
            get
            {
                return _height;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is BoardDimensions dimensions &&
                   _width == dimensions._width &&
                   _height == dimensions._height;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Width, Height);
        }

        public static bool operator ==(BoardDimensions left, BoardDimensions right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BoardDimensions left, BoardDimensions right)
        {
            return !(left == right);
        }

        public static implicit operator BoardDimensions(uint dimensions)
        {
            return new(dimensions, dimensions);
        }
    }
}
