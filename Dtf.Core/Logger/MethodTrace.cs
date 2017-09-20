using System;
using System.Diagnostics;

namespace Dtf.Core
{
    public class MethodTrace : IDisposable
    {
        private string m_name;

        public MethodTrace()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            m_name = sf.GetMethod().Name;
            Log.Default.LogLine(LogLevel.Trace, m_name);
        }

        public void Dispose()
        {
            Log.Default.LogLine(LogLevel.Trace, m_name);
        }
    }
}