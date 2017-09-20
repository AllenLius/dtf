using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;

namespace Dtf.Core
{
    public class WorkQueue : IDisposable
    {
        private bool m_blocked;
        private PriorityQueue<WorkItem> m_workItems;
        private ThreadStart m_initialization;
        private static TimeSpan m_maximumWorkItemTime = new TimeSpan(0, 0, 1);
        private ManualResetEvent m_resetEvent;
        private Timer m_resilianceTimer;
        private bool m_shutdown;
        private TimeSpan m_timeSpan;
        private Thread m_workerThread;

        public event EventHandler Blocked;

        public event EventHandler Unblocked;

        public event EventHandler WorkCompleted;

        public WorkQueue()
        {
            this.m_workItems = new PriorityQueue<WorkItem>();
            this.m_resetEvent = new ManualResetEvent(false);
            this.m_workerThread = new Thread(new ThreadStart(this.ThreadProcedure));
            this.m_workerThread.SetApartmentState(ApartmentState.STA);
            this.m_workerThread.Start();
            this.m_timeSpan = new TimeSpan(0, 0, 10);
        }

        public WorkQueue(ThreadStart initialization)
            : this()
        {
            Validate.ArgumentNotNull(initialization, "initialization");
            this.m_initialization = initialization;
        }

        public void AbortCurrentWorkItem()
        {
            this.m_workerThread.Abort();
            this.m_workerThread = new Thread(new ThreadStart(this.ThreadProcedure));
            this.m_workerThread.Start();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.m_resilianceTimer.Dispose();
                this.m_shutdown = true;
                this.m_resetEvent.Set();
                this.m_workerThread.Join(this.m_timeSpan);
                this.m_resetEvent.Close();
            }
            this.m_resilianceTimer = null;
            this.m_resetEvent = null;
            this.m_workerThread = null;
        }

        public void Enqueue(WorkItem item)
        {
            this.Enqueue(item, WorkItemPriority.High);
        }

        public void Enqueue(WorkItem item, WorkItemPriority priority)
        {
            Validate.ArgumentNotNull(item, "item");
            lock (m_workItems)
            {
                m_workItems.Enqueue(item, (int)priority);
            }
            this.m_resetEvent.Set();
        }

        private void Execute(WorkItem workItem)
        {
            Validate.ArgumentNotNull(workItem, "workItem");
            try
            {
                this.m_blocked = false;
                this.m_resilianceTimer.Change((int)m_maximumWorkItemTime.TotalMilliseconds, -1);
                workItem.Execute();
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
            finally
            {
                this.m_resilianceTimer.Change(-1, -1);
                if (this.m_blocked)
                {
                    this.m_blocked = false;
                    this.OnUnblocked(null);
                }
            }
        }

        protected virtual void OnBlocked(EventArgs e)
        {
            if (this.Blocked != null)
            {
                this.Blocked(this, e);
            }
        }

        protected virtual void OnException(Exception e)
        {
        }

        protected virtual void OnUnblocked(EventArgs e)
        {
            if (this.Unblocked != null)
            {
                this.Unblocked(this, e);
            }
        }

        protected virtual void OnWorkComplete(EventArgs e)
        {
            if (this.WorkCompleted != null)
            {
                this.WorkCompleted(this, e);
            }
        }

        private void ThreadProcedure()
        {
            bool signalled = false;
            this.m_resilianceTimer = new Timer(new TimerCallback(this.WorkItemTimeout), null, -1, -1);
            if (this.m_initialization != null)
            {
                this.m_initialization();
            }
            while (!this.m_shutdown)
            {
                WorkItem item;
                if ((this.m_workItems.Count != 0))
                {
                    lock (this.m_workItems)
                    {
                        item = this.m_workItems.Dequeue();
                    }
                    this.m_resetEvent.Reset();
                    this.Execute(item);
                }
                else
                {
                    if (signalled)
                    {
                        this.OnWorkComplete(null);
                    }
                    signalled = this.m_resetEvent.WaitOne(this.m_timeSpan, false);
                }
            }
        }

        private void WorkItemTimeout(object status)
        {
            this.m_blocked = true;
            this.OnBlocked(null);
        }
    }
}
