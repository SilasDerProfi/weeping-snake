using System;
using System.Numerics;
using WeepingSnake.Game.Utility.Extensions;
using Xunit;

namespace WeepingSnake.Game.Tests.Utility.Extensions
{
    public class Vector2ExtenstionsTests
    {
        [Fact]
        public void TestRotateRight()
        {
            var vector = new Vector2(0, 1);

            var rotate0Vector = vector.RotateRight(0);
            Assert.Equal(0, rotate0Vector.X, 5);
            Assert.Equal(1, rotate0Vector.Y, 5);

            var rotate45Vector = vector.RotateRight(45);
            Assert.Equal(vector.Length(), rotate45Vector.Length(), 5);

            var rotate45VectorTwice = rotate45Vector.RotateRight(45);
            Assert.Equal(vector.Length(), rotate45VectorTwice.Length(), 5);
            Assert.Equal(1, rotate45VectorTwice.X, 5);
            Assert.Equal(0, rotate45VectorTwice.Y, 5);

            var rotate90Vector = vector.RotateRight(90);
            Assert.Equal(1, rotate90Vector.X, 5);
            Assert.Equal(0, rotate90Vector.Y, 5);

            var rotate180Vector = vector.RotateRight(180);
            Assert.Equal(0, rotate180Vector.X, 5);
            Assert.Equal(-1, rotate180Vector.Y, 5);

            var rotate360Vector = vector.RotateRight(360);
            Assert.Equal(0, rotate360Vector.X, 5);
            Assert.Equal(1, rotate360Vector.Y, 5);

            var rotate450Vector = vector.RotateRight(450);
            Assert.Equal(1, rotate450Vector.X, 5);
            Assert.Equal(0, rotate450Vector.Y, 5);
        }

        [Fact]
        public void TestRotateLeft()
        {
            var vector = new Vector2(0, 1);

            var rotate0Vector = vector.RotateLeft(0);
            Assert.Equal(0, rotate0Vector.X, 5);
            Assert.Equal(1, rotate0Vector.Y, 5);

            var rotate45Vector = vector.RotateLeft(45);
            Assert.Equal(vector.Length(), rotate45Vector.Length(), 5);

            var rotate45VectorTwice = rotate45Vector.RotateLeft(45);
            Assert.Equal(vector.Length(), rotate45VectorTwice.Length(), 5);
            Assert.Equal(-1, rotate45VectorTwice.X, 5);
            Assert.Equal(0, rotate45VectorTwice.Y, 5);

            var rotate90Vector = vector.RotateLeft(90);
            Assert.Equal(-1, rotate90Vector.X, 5);
            Assert.Equal(0, rotate90Vector.Y, 5);

            var rotate180Vector = vector.RotateLeft(180);
            Assert.Equal(0, rotate180Vector.X, 5);
            Assert.Equal(-1, rotate180Vector.Y, 5);

            var rotate360Vector = vector.RotateLeft(360);
            Assert.Equal(0, rotate360Vector.X, 5);
            Assert.Equal(1, rotate360Vector.Y, 5);

            var rotate450Vector = vector.RotateLeft(450);
            Assert.Equal(-1, rotate450Vector.X, 5);
            Assert.Equal(0, rotate450Vector.Y, 5);
        }

        [Fact]
        public void TestIncrease()
        {
            var vector = new Vector2(1, 0);
            var decreasedVector = vector.Increase();
            var expectedVector = new Vector2(2, 0);

            Assert.Equal(expectedVector, decreasedVector);
        }

        [Fact]
        public void TestDecrease()
        {
            var vector = new Vector2(0, -2);
            var decreasedVector = vector.Decrease();
            var expectedVector = new Vector2(0, -1);

            Assert.Equal(expectedVector, decreasedVector);
        }
    }
}
