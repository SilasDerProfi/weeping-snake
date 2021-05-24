using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game.Person;
using Xunit;

namespace WeepingSnake.Game.Tests.Person
{
    public class PersonTests
    {
        private const string DEFAULT_PASSWORD = "123";

        private static IEnumerable<WeepingSnake.Game.Person.Person> CreateUsers(int count)
        {
            for (int userNumber = 1; userNumber <= count; userNumber++)
            {
                WeepingSnake.Game.Person.Person.Register($"{userNumber}@test", $"u{userNumber}", DEFAULT_PASSWORD, DEFAULT_PASSWORD);
                yield return WeepingSnake.Game.Person.Person.Login($"{userNumber}@test", DEFAULT_PASSWORD);
            }
        }

        [Fact]
        public void TestHighScoreEntries()
        {
            // Arrange
            var users = CreateUsers(3).ToList();

            // Act
            users.First(u => u.Username == "u1").AddPointsFromGame(100);
            users.First(u => u.Username == "u1").AddPointsFromGame(3);
            users.First(u => u.Username == "u2").AddPointsFromGame(10);
            users.First(u => u.Username == "u2").AddPointsFromGame(-6);
            users.First(u => u.Username == "u3").AddPointsFromGame(50);
            users.First(u => u.Username == "u1").AddPointsFromGame(2);
            users.First(u => u.Username == "u3").AddPointsFromGame(-10);
            var highscores = HighscoreEntry.GetHighscoreEntries();

            // Assert
            Assert.Equal(3, highscores.Count);
            Assert.Equal("u1", highscores[0].Username);
            Assert.Equal(100, highscores[0].MaximumPointsInGame);
            Assert.Equal(105, highscores[0].TotalPoints);
            Assert.Equal(3, highscores[0].PlayedGames);

            Assert.Equal("u3", highscores[1].Username);
            Assert.Equal(50, highscores[1].MaximumPointsInGame);
            Assert.Equal(40, highscores[1].TotalPoints);
            Assert.Equal(2, highscores[1].PlayedGames);

            Assert.Equal("u2", highscores[2].Username);
            Assert.Equal(10, highscores[2].MaximumPointsInGame);
            Assert.Equal(4, highscores[2].TotalPoints);
            Assert.Equal(2, highscores[2].PlayedGames);


            // Annihilate
            WeepingSnake.Game.Person.Person.DeleteAll();
        }

        [Fact]
        public void TestUpdatePasswordCorrect()
        {
            // Arrange
            var user = CreateUsers(1).First();

            // Act
            var result = user.ChangePassword("456", "456");

            // Assert
            var newPasswordLogin = WeepingSnake.Game.Person.Person.Login(user.MailAddress.Address, "456");
            Assert.True(result);
            Assert.NotNull(newPasswordLogin);

            // Annihilate
            WeepingSnake.Game.Person.Person.DeleteAll();
        }

        [Fact]
        public void TestUpdatePasswordIncorrect()
        {
            // Arrange
            var user = CreateUsers(1).First();

            // Act
            var result = user.ChangePassword("456", "ABC");

            // Assert
            var newPasswordLogin = WeepingSnake.Game.Person.Person.Login(user.MailAddress.Address, "456");
            Assert.False(result);
            Assert.Null(newPasswordLogin);

            // Annihilate
            WeepingSnake.Game.Person.Person.DeleteAll();
        }

        [Fact]
        public void TestUpdateEMailCorrect()
        {
            // Arrange
            var user = CreateUsers(1).First();

            // Act
            var result = user.ChangeEmail("dr@who");

            // Assert
            var newMailLogin = WeepingSnake.Game.Person.Person.Login("dr@who", DEFAULT_PASSWORD);
            Assert.True(result);
            Assert.NotNull(newMailLogin);

            // Annihilate
            WeepingSnake.Game.Person.Person.DeleteAll();
        }

        [Fact]
        public void TestUpdateEMailIncorrect()
        {
            // Arrange
            var user = CreateUsers(1).First();

            // Act
            var result = user.ChangeEmail("invalid mail");

            // Assert
            var newMailLogin = WeepingSnake.Game.Person.Person.Login("invalid mail", DEFAULT_PASSWORD);
            Assert.False(result);
            Assert.Null(newMailLogin);

            // Annihilate
            WeepingSnake.Game.Person.Person.DeleteAll();
        }

        [Fact]
        public void TestPersonGetById()
        {
            // Arrange
            var user = CreateUsers(1).First();

            // Act
            var requeriedUser = WeepingSnake.Game.Person.Person.GetById(user.PersonId);

            // Assert
            Assert.Equal(user, requeriedUser);

            // Annihilate
            WeepingSnake.Game.Person.Person.DeleteAll();
        }
    }
}
