using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WeepingSnake.Game.Person
{
    internal static class PersonDatabase
    {
        private static readonly HashSet<Person> _registeredPersons = new();

        internal static bool Exists(string emailAddress)
        {
            foreach(var person in _registeredPersons)
            {
                if (person.MailAddress.Address == emailAddress)
                    return true;
            }

            return false;
        }

        internal static bool Exists(Guid personId)
        {
            foreach (var person in _registeredPersons)
            {
                if (person.PersonId == personId)
                    return true;
            }

            return false;
        }

        internal static void Register(Person person)
        {
            if(Exists(person.MailAddress.Address))
            {
                return;
            }

            _registeredPersons.Add(person);
        }

        internal static Person GetPerson(string emailAddress)
        {
            foreach (var person in _registeredPersons)
            {
                if (person.MailAddress.Address == emailAddress)
                    return person.Copy();
            }

            return null;
        }

        internal static Person GetPerson(Guid personId)
        {
            foreach (var person in _registeredPersons)
            {
                if (person.PersonId == personId)
                    return person.Copy();
            }

            return null;
        }

        internal static void UpdateEmail(Guid personId, MailAddress mailAddress)
        {
            foreach (var person in _registeredPersons)
            {
                if (person.PersonId == personId)
                {
                    person.MailAddress = mailAddress;
                    return;
                }
            }
        }

        internal static void UpdatePassword(Guid personId, string password)
        {
            foreach (var person in _registeredPersons)
            {
                if (person.PersonId == personId)
                {
                    person.Password = password;
                    return;
                }
            }
        }

        internal static void Update(Person person)
        {
            Person equilvalentInDatabase = null;
            
            foreach (var databasePerson in _registeredPersons)
            {
                if (databasePerson.PersonId == person.PersonId)
                {
                    equilvalentInDatabase = databasePerson;
                    break;
                }
            }

            _registeredPersons.Remove(equilvalentInDatabase);
            _registeredPersons.Add(person.Copy());
        }


        internal static IEnumerable<HighscoreEntry> Highscores()
        {
            var ordererdByHighscore = _registeredPersons.OrderByDescending(p => p.MaximumPointsInGame);

            foreach(var person in ordererdByHighscore)
            {
                var highScoreEntry = new HighscoreEntry(person.Username, person.PlayedGames, person.MaximumPointsInGame, person.TotalPoints);
                yield return highScoreEntry;
            }
        }
    }
}
