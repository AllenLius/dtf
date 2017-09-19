using System;
using System.Diagnostics;

namespace Dta.Core
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
    public class PerformanceTrace : IDisposable
    {
        private string m_name = string.Empty;
        private Stopwatch m_stopWatch = null;

        public PerformanceTrace(string name = null)
        {
            if (name == null)
            {
                StackTrace st = new StackTrace(1, true);
                var frame = st.GetFrame(0);
                name = string.Format("{0}@{1}:{2}", System.IO.Path.GetFileName(frame.GetFileName()), frame.GetFileLineNumber(), frame.GetMethod().Name);
            }
            m_name = name;
            m_stopWatch = new Stopwatch();
            Log.Default.Trace(string.Format("Begin {0}", m_name));
            m_stopWatch.Start();
        }

        public void Dispose()
        {
            m_stopWatch.Stop();
            Log.Default.Trace(string.Format("End {0} {1}ms, {2}ticks", m_name, m_stopWatch.ElapsedMilliseconds, m_stopWatch.ElapsedTicks));
        }

        /**
         *  @brief  time used
         *  @return
         *  @note
         */
        public TimeSpan Elapsed
        {
            get
            {
                return m_stopWatch.Elapsed;
            }
        }
    }
}
