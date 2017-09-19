namespace Dta.Core
{
    public interface ILogAppender
    {
        void LogLine(LogLevel logLevel, string message);
    }    
}