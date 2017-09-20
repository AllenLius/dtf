using System;
using Dtf.Core;

namespace Dtf.Core
{
    public class ConsoleLogAppender : ILogAppender
    {
        public void LogLine(LogLevel logLevel, string message)
        {
            var color = Console.ForegroundColor;
            SetConsole(logLevel);
            string datetime = DateTime.Now.ToString("yyyy/M/d hh:m:s");
            string outMsg = String.Format("{0} {1} {2}", datetime, logLevel.Name, message);
            Console.WriteLine(message);
            Console.ForegroundColor = color;
        }

        private static void SetConsole(LogLevel logLevel)
        {
            if (logLevel == LogLevel.Debug)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (logLevel == LogLevel.Error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (logLevel == LogLevel.Fatal)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (logLevel == LogLevel.Info)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (logLevel == LogLevel.Trace)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (logLevel == LogLevel.Warn)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
        }
    }
}