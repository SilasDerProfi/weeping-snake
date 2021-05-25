using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WeepingSnake.Game.Tests
{
    public class GameConfigurationTests
    {
        [Fact]
        public void TestCreateCorrectEntry()
        {
            // Arrange & Act
            var entry = new GameConfigurationEntry("Test=90");

            // Assert
            Assert.Equal("Test", entry.Property);
            Assert.Equal("90", entry.Value);
        }

        [Fact]
        public void TestCreateIncorrectEntry()
        {
            var errorOccured = false;
            // Arrange & Act
            try
            {
                _ = new GameConfigurationEntry("invalid=invalid=");
            }
            catch (ApplicationException ex)
            {
                // Assert
                Assert.StartsWith("Wrong Format of the configurationfile", ex.Message);
                errorOccured = true;
            }
            finally
            {
                Assert.True(errorOccured);
            }


        }
    }
}
