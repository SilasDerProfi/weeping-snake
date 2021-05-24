using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game.Structs;
using Xunit;

namespace WeepingSnake.Game.Tests.Game.Structs
{
    public class BoardDimensionsTests
    {
        [Fact]
        public void TestConstraints()
        {
            try
            {
                _ = new BoardDimensions(4, 5);
            }
            catch (ArgumentException ex)
            {
                Assert.Equal("The width and height of the game board must be greater than 5.", ex.Message);
            }

            try
            {
                _ = new BoardDimensions(5, 4);
            }
            catch (ArgumentException ex)
            {
                Assert.Equal("The width and height of the game board must be greater than 5.", ex.Message);
            }
        }

        [Fact]
        public void TestOperators()
        {
            // Arrange & Act
            var rangeA = new BoardDimensions(6, 7);
            var rangeB = new BoardDimensions(10, 10);
            var rangeC = new BoardDimensions(6, 7);
            var rangeD = new BoardDimensions(20, 20);
            BoardDimensions rangeE = 20;


            // Assert
            Assert.True(rangeA == rangeC);
            Assert.True(rangeA != rangeB);
            Assert.Equal(rangeD.GetHashCode(), rangeE.GetHashCode());
        }
    }
}
