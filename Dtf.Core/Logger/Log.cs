using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Dta.Core
{
    public class Log
    {
        private string m_name;
        private List<ILogAppender> _logAppenders = new List<ILogAppender>();
        private static Log _defaultLog = new Log("Default");

        public Log(string name)
        {
            m_name = name;
        }

        public void AddAppender(ILogAppender logAppender)
        {
            _logAppenders.Add(logAppender);
        }

        public void Trace(string message, params object[] arguments)
        {
            LogLine(LogLevel.Trace, message, arguments);
        }

        /// <summary>
        /// Logs the specified debug message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="arguments">The arguments.</param>
        public void Debug(string message, params object[] arguments)
        {
            LogLine(LogLevel.Debug, message, arguments);
        }

        /// <summary>
        /// Logs the specified informational message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="arguments">The arguments.</param>
        public void Info(string message, params object[] arguments)
        {
            LogLine(LogLevel.Info, message, arguments);
        }

        /// <summary>
        /// Logs the specified warning message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="arguments">The arguments.</param>
        public void Warning(string message, params object[] arguments)
        {
            LogLine(LogLevel.Warn, message, arguments);
        }

        /// <summary>
        /// Logs the specified error message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="arguments">The arguments.</param>
        public void Error(string message, params object[] arguments)
        {
            LogLine(LogLevel.Error, message, arguments);
        }

        public void LogLine(LogLevel logLevel, string message, params object[] arguments)
        {
            lock (_logAppenders)
            {
                foreach (var appender in _logAppenders)
                {
                    appender.LogLine(logLevel, string.Format(message, arguments));
                }
            }
        }

        public void LogException(Exception ex)
        {
            Error(ex.GetExceptionText());
        }

        /// <summary>
        /// Logs the specified error message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="arguments">The arguments.</param>
        public void Step(string message, params object[] arguments)
        {
            LogLine(LogLevel.Trace, message, arguments);
        }

        public static Log Default
        {
            get
            {
                return _defaultLog;
            }
        }
    }
}
