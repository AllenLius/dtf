using System;
using Dtf.Core;

namespace Dtf.Core
{
    public class CallbackLogAppender : ILogAppender
    {
        private Action<LogLevel, string> m_callback;

        public CallbackLogAppender(Action<LogLevel, string> callback)
        {
            m_callback = callback;
        }

        public void LogLine(LogLevel logLevel, string message)
        {
            m_callback(logLevel, message);
        }
    }
}