using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeepingSnake.Game
{
    internal class GameConfigurationEntry
    {
        private string _property;
        private string _value;

        public GameConfigurationEntry(string configurationLine)
        {
            var seperator = configurationLine.IndexOf("=");

            if(configurationLine.IndexOf("=", seperator + 1) != -1 || seperator == 0 || seperator == configurationLine.Length - 1)
            {
                throw new ApplicationException("Wrong Format of the configurationfile");
            }

            var configurationLineValues = configurationLine.Split("=");

            Property = configurationLineValues[0];
            Value = configurationLineValues[1];
        }

        internal string Property
        {
            get
            {
                return _property;
            }
            private set
            {
                _property = value;
            }
        }

        internal string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }
}
