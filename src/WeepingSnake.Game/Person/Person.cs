using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace WeepingSnake.Game.Person
{
    public class Person
    {
        private readonly Guid _personId;
        private readonly string _username;
        private MailAddress _mailAddress;

        private string _password;
        private int _playedGames;
        private int _maximumPointsInGame;
        private int _totalPoints;


        private Person(Guid? personId, string username, MailAddress mailAddress, string password, int playedGames, int maximumPointsInGame, int totalPoints)
        {
            _personId = personId ?? Guid.NewGuid();
            _username = username;
            _mailAddress = mailAddress;
            _password = password;
            _playedGames = playedGames;
            _maximumPointsInGame = maximumPointsInGame;
            _totalPoints = totalPoints;
        }

        public static void Register(string emailAddress, string username, string password, string passwordRetyped)
        {
            if (PersonDatabase.Exists(emailAddress))
            {
                return;
            }

            if (password != passwordRetyped || String.IsNullOrEmpty(emailAddress))
            {
                return;
            }

            if (!MailAddress.TryCreate(emailAddress, out var emailAdressObject))
            {
                return;
            }

            var newPerson = new Person(null, username, emailAdressObject, password, 0, 0, 0);

            PersonDatabase.Register(newPerson);
        }


        public static Person Login(string emailAddress, string password)
        {
            if (!PersonDatabase.Exists(emailAddress))
            {
                return null;
            }

            var person = PersonDatabase.GetPerson(emailAddress);

            if (person._password == password)
            {
                return person;
            }
            else
            {
                return null;
            }
        }

        public static Person GetById(Guid personId)
        {
            if (PersonDatabase.Exists(personId))
            {
                var person = PersonDatabase.GetPerson(personId);
                return person;
            }
            else
            {
                return null;
            }
        }

        public MailAddress MailAddress
        {
            get
            {
                return _mailAddress;
            }
            internal set
            {
                _mailAddress = value;
            }
        }

        public Guid PersonId
        {
            get
            {
                return _personId;
            }
        }

        internal string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }
        }

        public int PlayedGames
        {
            get
            {
                return _playedGames;
            }
        }
        public int MaximumPointsInGame
        {
            get
            {
                return _maximumPointsInGame;
            }
        }
        public int TotalPoints
        {
            get
            {
                return _totalPoints;
            }
        }

        public bool ChangeEmail(string newEmailAdress)
        {
            if(MailAddress.TryCreate(newEmailAdress, out var newMail))
            {
                _mailAddress = newMail;
                PersonDatabase.UpdateEmail(_personId, _mailAddress);
                return true;
            }

            return false;
        }

        public bool ChangePassword(string password, string passwordRetyped)
        {
            if(password == passwordRetyped)
            {
                _password = password;
                PersonDatabase.UpdatePassword(_personId, _password);
                return true;
            }

            return false;
        }

        public void AddPointsFromGame(int points)
        {
            _playedGames++;
            _maximumPointsInGame = Math.Max(_maximumPointsInGame, points);
            _totalPoints += points;

            PersonDatabase.Update(this);
        }

        public static void DeleteAll()
        {
            PersonDatabase.DeleteAll();
        }

        internal Person Copy()
        {
            return new Person(_personId, _username, _mailAddress, _password, _playedGames, _maximumPointsInGame, _totalPoints);
        }

        public override bool Equals(object obj)
        {
            return obj is Person person && _personId.Equals(person._personId) && _mailAddress.Equals(person.MailAddress);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_mailAddress, _personId);
        }
    }
}
