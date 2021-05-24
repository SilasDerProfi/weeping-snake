using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WeepingSnake.Game
{
    public static class GameConfiguration
    {
        // If you Change the deafult-Values, you should mention the Tests!
        
        private static int _defaultDistance = 1;
        private static int _minSpeedIncrement = 1;
        private static int _minimumRotationAngle = 90;
        private static int _roundDuration = 1000;
        private static string _defaultLoggingDirectory = "";
        private static string _defaultLoggingExtension = ".weepingsnake.log";
        private static bool _isLoggingEnabled = false;

        static GameConfiguration()
        {
            const string configFilePath = "weepingsnake.config";
            if (System.IO.File.Exists(configFilePath))
            {
                var configLines = System.IO.File.ReadAllLines(configFilePath);

                foreach(var line in configLines)
                {
                    var configEntry = new GameConfigurationEntry(line);

                    var typeOfThisClass = typeof(GameConfiguration);

                    var b = typeOfThisClass.GetProperties();

                    foreach (var property in b)
                    {
                        if(property.Name == configEntry.Property)
                        {
                            if (int.TryParse(configEntry.Value, out var intValue))
                            {
                                property.SetValue(null, intValue);
                            }
                            else if (bool.TryParse(configEntry.Value, out var boolValue))
                            {
                                property.SetValue(null, boolValue);
                            }
                            else
                            {
                                property.SetValue(null, configEntry.Value);
                            }
                        }
                    }
                }
            }
        }


        public static int DefaultDistance
        {
            get
            {
                return _defaultDistance;
            }
            private set
            {
                _defaultDistance = value;
            }
        }

        public static int MinimumRotationAngle
        {
            get
            {
                return _minimumRotationAngle;
            }
            private set
            {
                _minimumRotationAngle = value;
            }
        }
        public static int MinSpeedIncrement
        {
            get
            {
                return _minSpeedIncrement;
            }
            private set
            {
                _minSpeedIncrement = value;
            }
        }

        public static int RoundDuration
        {
            get
            {
                return _roundDuration;
            }
            private set
            {
                _roundDuration = value;
            }
        }

        public static string DefaultLoggingDirectory
        {
            get
            {
                return _defaultLoggingDirectory;
            }
            private set
            {
                _defaultLoggingDirectory = value;
            }
        }

        public static string DefaultLoggingPathExtension
        {
            get
            {
                return _defaultLoggingExtension;
            }
            private set
            {
                _defaultLoggingExtension = value;
            }
        }

        public static bool IsLoggingEnabled
        {
            get
            {
                return _isLoggingEnabled;
            }
            private set
            {
                _isLoggingEnabled = value;
            }
        }
    }
}
