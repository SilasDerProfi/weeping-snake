using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game.Structs;
using Xunit;

namespace WeepingSnake.Game.Tests.Game.Structs
{
    public class PlayerRangeTests
    {
        [Fact]
        public void TestConstraints()
        {
            try
            {
                _ = new PlayerRange(0, 3);
            }
            catch (ArgumentException ex)
            {
                Assert.Equal("At least 1 player must be allowed.", ex.Message);
            }

            try
            {
                _ = new PlayerRange(4, 3);
            }
            catch (ArgumentException ex)
            {
                Assert.Equal("At least 1 player must be allowed.", ex.Message);
            }
        }

        [Fact]
        public void TestOperators()
        {
            // Arrange & Act
            var rangeA = new PlayerRange(1, 4);
            var rangeB = new PlayerRange(2, 4);
            var rangeC = new PlayerRange(1, 4);
            var rangeD = new PlayerRange(4, 4);
            PlayerRange rangeE = 4;


            // Assert
            Assert.True(rangeA == rangeC);
            Assert.True(rangeA != rangeB);
            Assert.Equal(rangeD.GetHashCode(), rangeE.GetHashCode());
        }
    }
}
