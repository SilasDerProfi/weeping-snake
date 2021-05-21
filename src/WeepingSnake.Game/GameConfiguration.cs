using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeepingSnake.Game
{
    public static class GameConfiguration
    {
        public static int DefaultDistance
        {
            get
            {
                return 1;
            }
        }

        public static int MinimumRotationAngle
        {
            get
            {
                return 90;
            }
        }
        public static int MinSpeedIncrement
        {
            get
            {
                return 1;
            }
        }

        public static int RoundDuration
        {
            get
            {
                return 1000;
            }
        }

        public static string DefaultLoggingDirection
        {
            get
            {
                return "";
            }
        }

        public static string DefaultLoggingPathExtension
        {
            get
            {
                return ".weepingsnake.log";
            }
        }

        public static bool IsLoggingEnabled
        {
            get
            {
                return false;
            }
        }
    }
}
