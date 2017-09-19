using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dta.Core
{
    public abstract class WorkItem : IDisposable
    {
        private WorkQueue m_queue;

        protected WorkItem(WorkQueue queue)
        {
            this.m_queue = queue;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.m_queue = null;
        }

        public abstract void Execute();

        protected WorkQueue Queue
        {
            get
            {
                return this.m_queue;
            }
        }
    }
}
