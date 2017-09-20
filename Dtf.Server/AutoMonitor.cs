using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Dtf.Server
{
    public class AutoMonitor : IDisposable
    {
        private object m_obj;
        private bool m_disposed = false;

        public AutoMonitor(object obj, TimeSpan timeout)
        {
            m_obj = obj;
            Entered = Monitor.TryEnter(obj, timeout);
            //_instances.Add(this);
        }

        ~AutoMonitor()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing && Entered)
                {
                    Monitor.Exit(m_obj);
                }
                m_disposed = true;
            }
        }

        public bool Entered { get; private set; }
    }
}